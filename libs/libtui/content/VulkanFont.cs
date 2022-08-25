using FreeType;
using libtui.drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using PixelMode = FreeType.PixelMode;

namespace libtui.content
{
    class VulkanFont : IDisposable
    {
		private static bool hasCheckedForMono;
		private static bool isRunningOnMono;
		private static System.Reflection.FieldInfo monoPaletteFlagsField;

		private VulkanContext mDevice;
		private Face mFace;
		private Library mLibrary;

		private VulkanFont(VulkanContext ctx, Library library, Face face) 
        {
			mDevice = ctx;
			mLibrary = library;
			mFace = face;
        }

		/// <summary>
		/// Render the string into a bitmap with <see cref="SystemColors.ControlText"/> text color and a transparent background.
		/// </summary>
		/// <param name="text">The string to render.</param>
		internal virtual VulkanImage RenderString(string text)
		{
			return RenderString(text, Color.Black, Color.Transparent);
		}

		/// <summary>
		/// Render the string into a bitmap with a transparent background.
		/// </summary>
		/// <param name="text">The string to render.</param>
		/// <param name="foreColor">The color of the text.</param>
		/// <returns></returns>
		internal virtual VulkanImage RenderString(string text, Color foreColor)
		{
			return RenderString(text, foreColor, Color.Transparent);
		}

		/// <summary>
		/// Render the string into a bitmap with an opaque background.
		/// </summary>
		/// <param name="text">The string to render.</param>
		/// <param name="foreColor">The color of the text.</param>
		/// <param name="backColor">The color of the background behind the text.</param>
		/// <returns></returns>
		internal virtual VulkanImage RenderString(string text, Color foreColor, Color backColor)
		{
			var measuredChars = new List<DebugChar>();
			var renderedChars = new List<DebugChar>();
			float penX = 0, penY = 0;
			float stringWidth = 0; // the measured width of the string
			float stringHeight = 0; // the measured height of the string
			float overrun = 0;
			float underrun = 0;
			float kern = 0;
			int spacingError = 0;
			bool trackingUnderrun = true;
			int rightEdge = 0; // tracking rendered right side for debugging

			// Bottom and top are both positive for simplicity.
			// Drawing in .Net has 0,0 at the top left corner, with positive X to the right
			// and positive Y downward.
			// Glyph metrics have an origin typically on the left side and at baseline
			// of the visual data, but can draw parts of the glyph in any quadrant, and
			// even move the origin (via kerning).
			float top = 0, bottom = 0;

			// Measure the size of the string before rendering it. We need to do this so
			// we can create the proper size of bitmap (canvas) to draw the characters on.
			for (int i = 0; i < text.Length; i++)
			{
				#region Load character
				char c = text[i];

				// Look up the glyph index for this character.
				uint glyphIndex = mFace.GetCharIndex(c);

				// Load the glyph into the font's glyph slot. There is usually only one slot in the font.
				mFace.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);

				// Refer to the diagram entitled "Glyph Metrics" at http://www.freetype.org/freetype2/docs/tutorial/step2.html.
				// There is also a glyph diagram included in this example (glyph-dims.svg).
				// The metrics below are for the glyph loaded in the slot.
				float gAdvanceX = (float)mFace.Glyph.Advance.X; // same as the advance in metrics
				float gBearingX = (float)mFace.Glyph.Metrics.HorizontalBearingX;
				float gWidth = mFace.Glyph.Metrics.Width.ToSingle();
				var rc = new DebugChar(c, gAdvanceX, gBearingX, gWidth);
				#endregion
				#region Underrun
				// Negative bearing would cause clipping of the first character
				// at the left boundary, if not accounted for.
				// A positive bearing would cause empty space.
				underrun += -(gBearingX);
				if (stringWidth == 0)
					stringWidth += underrun;
				if (trackingUnderrun)
					rc.Underrun = underrun;
				if (trackingUnderrun && underrun <= 0)
				{
					underrun = 0;
					trackingUnderrun = false;
				}
				#endregion
				#region Overrun
				// Accumulate overrun, which coould cause clipping at the right side of characters near
				// the end of the string (typically affects fonts with slanted characters)
				if (gBearingX + gWidth > 0 || gAdvanceX > 0)
				{
					overrun -= Math.Max(gBearingX + gWidth, gAdvanceX);
					if (overrun <= 0) overrun = 0;
				}
				overrun += (float)(gBearingX == 0 && gWidth == 0 ? 0 : gBearingX + gWidth - gAdvanceX);
				// On the last character, apply whatever overrun we have to the overall width.
				// Positive overrun prevents clipping, negative overrun prevents extra space.
				if (i == text.Length - 1)
					stringWidth += overrun;
				rc.Overrun = overrun; // accumulating (per above)
				#endregion

				#region Top/Bottom
				// If this character goes higher or lower than any previous character, adjust
				// the overall height of the bitmap.
				float glyphTop = (float)mFace.Glyph.Metrics.HorizontalBearingY;
				float glyphBottom = (float)(mFace.Glyph.Metrics.Height - mFace.Glyph.Metrics.HorizontalBearingY);
				if (glyphTop > top)
					top = glyphTop;
				if (glyphBottom > bottom)
					bottom = glyphBottom;
				#endregion

				// Accumulate the distance between the origin of each character (simple width).
				stringWidth += gAdvanceX;
				rc.RightEdge = stringWidth;
				measuredChars.Add(rc);

				#region Kerning (for NEXT character)
				// Calculate kern for the NEXT character (if any)
				// The kern value adjusts the origin of the next character (positive or negative).
				if (mFace.HasKerning && i < text.Length - 1)
				{
					char cNext = text[i + 1];
					kern = (float)mFace.GetKerning(glyphIndex, mFace.GetCharIndex(cNext), KerningMode.Default).X;
					// sanity check for some fonts that have kern way out of whack
					if (kern > gAdvanceX * 5 || kern < -(gAdvanceX * 5))
						kern = 0;
					rc.Kern = kern;
					stringWidth += kern;
				}

				#endregion
			}

			stringHeight = top + bottom;

			// If any dimension is 0, we can't create a bitmap
			if (stringWidth == 0 || stringHeight == 0)
				return null;

			// Create a new bitmap that fits the string.
			var imgWidth = (int)Math.Ceiling(stringWidth);
			var imgHeight = (int)Math.Ceiling(stringHeight);

			trackingUnderrun = true;
			underrun = 0;
			overrun = 0;
			stringWidth = 0;

			var renderMode = RenderMode.Normal;

			// Draw the string into the bitmap.
			// A lot of this is a repeat of the measuring steps, but this time we have
			// an actual bitmap to work with (both canvas and bitmaps in the glyph slot).

			IndexBitmap[] bitmap = new IndexBitmap[text.Length];
			for (int i = 0; i < text.Length; i++)
			{
				#region Load character
				char c = text[i];

				// Same as when we were measuring, except RenderGlyph() causes the glyph data
				// to be converted to a bitmap.
				uint glyphIndex = mFace.GetCharIndex(c);
				mFace.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);
				mFace.Glyph.RenderGlyph(renderMode);
				FTBitmap ftbmp = mFace.Glyph.Bitmap;

				float gAdvanceX = (float)mFace.Glyph.Advance.X;
				float gBearingX = (float)mFace.Glyph.Metrics.HorizontalBearingX;
				float gWidth = (float)mFace.Glyph.Metrics.Width;

				var rc = new DebugChar(c, gAdvanceX, gBearingX, gWidth);
				#endregion

				#region Underrun
				// Underrun
				underrun += -(gBearingX);
				if (penX == 0)
					penX += underrun;
				if (trackingUnderrun)
					rc.Underrun = underrun;
				if (trackingUnderrun && underrun <= 0)
				{
					underrun = 0;
					trackingUnderrun = false;
				}
				#endregion

				#region Draw glyph
				// Whitespace characters sometimes have a bitmap of zero size, but a non-zero advance.
				// We can't draw a 0-size bitmap, but the pen position will still get advanced (below).
				if ((ftbmp.Width > 0 && ftbmp.Rows > 0))
				{
					// Get a bitmap that .Net can draw (GDI+ in this case).
					var rcSize = GetFTBitmapSize(ftbmp, renderMode);
					rc.Width = rcSize.Width;
					rc.BearingX = mFace.Glyph.BitmapLeft;
					int x = (int)Math.Round(penX + mFace.Glyph.BitmapLeft);
					int y = (int)Math.Round(penY + top - (float)mFace.Glyph.Metrics.HorizontalBearingY);
					//Not using g.DrawImage because some characters come out blurry/clipped. (Is this still true?)

					bitmap[i] = DrawToIndexBitmap(ftbmp, foreColor);

					rc.Overrun = mFace.Glyph.BitmapLeft + rcSize.Width - gAdvanceX;
					// Check if we are aligned properly on the right edge (for debugging)
					rightEdge = Math.Max(rightEdge, x + (int)rcSize.Width);
					spacingError = imgWidth - rightEdge;
				}
				else
				{
					rightEdge = (int)(penX + gAdvanceX);
					spacingError = imgWidth - rightEdge;
				}
				#endregion

				#region Overrun
				if (gBearingX + gWidth > 0 || gAdvanceX > 0)
				{
					overrun -= Math.Max(gBearingX + gWidth, gAdvanceX);
					if (overrun <= 0) overrun = 0;
				}
				overrun += (float)(gBearingX == 0 && gWidth == 0 ? 0 : gBearingX + gWidth - gAdvanceX);
				if (i == text.Length - 1) penX += overrun;
				rc.Overrun = overrun;
				#endregion

				// Advance pen positions for drawing the next character.
				penX += (float)mFace.Glyph.Advance.X; // same as Metrics.HorizontalAdvance?
				penY += (float)mFace.Glyph.Advance.Y;

				rc.RightEdge = penX;
				spacingError = imgWidth - (int)Math.Round(rc.RightEdge);
				renderedChars.Add(rc);

				#region Kerning (for NEXT character)
				// Adjust for kerning between this character and the next.
				if (mFace.HasKerning && i < text.Length - 1)
				{
					char cNext = text[i + 1];
					kern = (float)mFace.GetKerning(glyphIndex, mFace.GetCharIndex(cNext), KerningMode.Default).X;
					if (kern > gAdvanceX * 5 || kern < -(gAdvanceX * 5))
						kern = 0;
					rc.Kern = kern;
					penX += (float)kern;
				}
				#endregion

			}

			bool printedHeader = false;
			if (spacingError != 0)
			{
				for (int i = 0; i < renderedChars.Count; i++)
				{
					//if (measuredChars[i].RightEdge != renderedChars[i].RightEdge)
					//{
					if (!printedHeader)
						DebugChar.PrintHeader();
					printedHeader = true;
					Debug.Print(measuredChars[i].ToString());
					Debug.Print(renderedChars[i].ToString());
					//}
				}
				string msg = string.Format("Right edge: {0,3} ({1}) {2}",
					spacingError,
					spacingError == 0 ? "perfect" : spacingError > 0 ? "space  " : "clipped",
					mFace.FamilyName);

				Debug.Print(msg);
				//throw new ApplicationException(msg);
			}

			var mipmap = new TextureData.Mipmap();
			//mipmap.Data

			var tex2D = new TextureData();
			//tex2D.Format = Vulkan.Format.
			tex2D.Mipmaps = new TextureData.Mipmap[1];
			tex2D.Mipmaps[0] = mipmap;

			return VulkanImage.Texture2D(mDevice, tex2D);
		}


		internal static VulkanFont FromFace(VulkanContext ctx, Library library, Face face) 
        {
            return new VulkanFont(ctx, library, face);
        }

		private static Size GetFTBitmapSize(FTBitmap bitmap, RenderMode mode) 
		{
            switch (mode)
            {
                case RenderMode.Lcd: return new Size(bitmap.Width / 3, bitmap.Rows);
				case RenderMode.VerticalLcd: return new Size(bitmap.Width, bitmap.Rows / 3);
				default: return new Size(bitmap.Width, bitmap.Rows);
			}
        }

        private static IndexBitmap DrawToIndexBitmap(FTBitmap b, Color color)
		{
			if (b.IsDisposed)
				throw new ObjectDisposedException("FTBitmap", "Cannot access a disposed object.");

			if (b.Width == 0 || b.Rows == 0)
				throw new InvalidOperationException("Invalid image size - one or both dimensions are 0.");

			switch (b.PixelMode)
			{
				case PixelMode.Mono:
					{
						var data = new byte[b.Width * b.Rows];
						for (int i = 0; i < b.Rows; i++)
							Copy(b.Buffer, i * b.Pitch, ref data[0], i * b.Width, b.Width);

						Color[] palette = new Color[2];
						palette[0] = new Color(color, 0);
						palette[1] = new Color(color, 255);

						return new IndexBitmap(PixelFormat.Format1bppIndexed, palette, data, b.Width, b.Rows);
					}

				case PixelMode.Gray4:
					{
						var data = new byte[b.Width * b.Rows];
						for (int i = 0; i < b.Rows; i++)
							Copy(b.Buffer, i * b.Pitch, ref data[0], i * b.Width, b.Width);

						const int interval = 17;

						Color[] palette = new Color[255 / interval];
						for (int i = 0; i < palette.Length; i++)
						{
							float a = (i * interval) / 255f;
							palette[i] = new Color((int)(color.R * a), (int)(color.G * a), (int)(color.B * a), i * interval);
						}

						return new IndexBitmap(PixelFormat.Format4bppIndexed, palette, data, b.Width, b.Rows);
					}

				case PixelMode.Gray:
					{
						var data = new byte[b.Width * b.Rows];
						for (int i = 0; i < b.Rows; i++)
							Copy(b.Buffer, i * b.Pitch, ref data[0], i * b.Width, b.Width);

						const int interval = 1;

						Color[] palette = new Color[255 / interval];
						for (int i = 0; i < palette.Length; i++)
						{
							float a = (i * interval) / 255f;
							palette[i] = new Color((int)(color.R * a), (int)(color.G * a), (int)(color.B * a), i * interval);
						}

						return new IndexBitmap(PixelFormat.Format8bppIndexed, palette, data, b.Width, b.Rows);
					}
				/*
				case PixelMode.Lcd:
					{
						//TODO apply color
						int bmpWidth = b.Width / 3;
						Bitmap bmp = new Bitmap(bmpWidth, b.Rows, PixelFormat.Format24bppRgb);
						var locked = bmp.LockBits(new Rectangle(0, 0, bmpWidth, b.Rows), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

						for (int i = 0; i < b.Rows; i++)
							Copy(b.Buffer, i * b.Pitch, locked.Scan0, i * locked.Stride, locked.Stride);

						bmp.UnlockBits(locked);

						return new Size(bmpWidth, b.Rows);
					}
				case PixelMode.VerticalLcd:
					{
						int bmpHeight = b.Rows / 3;
						Bitmap bmp = new Bitmap(b.Width, bmpHeight, PixelFormat.Format24bppRgb);
						var locked = bmp.LockBits(new Rectangle(0, 0, b.Width, bmpHeight), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
						for (int i = 0; i < bmpHeight; i++)
							Copy(b.Buffer, i * b.Pitch, locked.Scan0, i * locked.Stride, b.Width);
						bmp.UnlockBits(locked);

						return bmp;
					}
				*/

				default:
					throw new InvalidOperationException("Font bitmap does not support this pixel mode.");
			}
		}

		/// <summary>
		/// A method to copy data from one pointer to another, byte by byte.
		/// </summary>
		/// <param name="source">The source pointer.</param>
		/// <param name="sourceOffset">An offset into the source buffer.</param>
		/// <param name="destination">The destination pointer.</param>
		/// <param name="destinationOffset">An offset into the destination buffer.</param>
		/// <param name="count">The number of bytes to copy.</param>
		static unsafe void Copy(IntPtr source, int sourceOffset, ref byte destination, int destinationOffset, int count)
		{
			byte* src = (byte*)source + sourceOffset;
			byte* dst = (byte*)destination + destinationOffset;
			byte* end = dst + count;

			while (dst != end)
				*dst++ = *src++;
		}

		public void Dispose()
        {
            mFace.Dispose();
        }

		struct IndexBitmap
		{
			internal IndexBitmap(PixelFormat pixelFormat, Color[] colorPalette, byte[] buffer, int width, int height)
			{
				PixelFormat = pixelFormat;
				ColorPalette = colorPalette;
				Buffer = buffer;
				Size = new Size(width, height);
			}

			public PixelFormat PixelFormat { get; }

			public Color[] ColorPalette { get; }

			public byte[] Buffer { get; }

			public Size Size { get; }

			public Color GetPixel(int x, int y)
			{
				return ColorPalette[Buffer[x + y * Size.Width]];
			}
		}

		private class DebugChar
		{
			public char Char { get; set; }
			public float AdvanceX { get; set; }
			public float BearingX { get; set; }
			public float Width { get; set; }
			public float Underrun { get; set; }
			public float Overrun { get; set; }
			public float Kern { get; set; }
			public float RightEdge { get; set; }
			internal DebugChar(char c, float advanceX, float bearingX, float width)
			{
				this.Char = c; this.AdvanceX = advanceX; this.BearingX = bearingX; this.Width = width;
			}

			public override string ToString()
			{
				return string.Format("'{0}' {1,5:F0} {2,5:F0} {3,5:F0} {4,5:F0} {5,5:F0} {6,5:F0} {7,5:F0}",
					this.Char, this.AdvanceX, this.BearingX, this.Width, this.Underrun, this.Overrun,
					this.Kern, this.RightEdge);
			}
			public static void PrintHeader()
			{
				Debug.Print("    {1,5} {2,5} {3,5} {4,5} {5,5} {6,5} {7,5}",
					"", "adv", "bearing", "wid", "undrn", "ovrrn", "kern", "redge");
			}
		}

    }
}
