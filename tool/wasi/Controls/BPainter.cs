using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace addin.controls.renderer
{
    class BPainter
    {
		[DllImport("gdi32")]
		private extern static int SetDIBitsToDevice(HandleRef hDC, int xDest, int yDest, int dwWidth, int dwHeight, int XSrc, int YSrc, int uStartScan, int cScanLines, ref int lpvBits, ref BITMAPINFO lpbmi, uint fuColorUse);

		[StructLayout(LayoutKind.Sequential)]
		private struct BITMAPINFOHEADER
		{
			public int bihSize;
			public int bihWidth;
			public int bihHeight;
			public short bihPlanes;
			public short bihBitCount;
			public int bihCompression;
			public int bihSizeImage;
			public double bihXPelsPerMeter;
			public double bihClrUsed;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct BITMAPINFO
		{
			public BITMAPINFOHEADER biHeader;
			public int biColors;
		}

		private int _width;
		private int _height;
		private int[] _pArray;
		private GCHandle _gcHandle;
		private BITMAPINFO _BI;

		public int Width { get { return _width; } }
		public int Height { get { return _height; } }

		~BPainter()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (_gcHandle.IsAllocated)
				_gcHandle.Free();
			GC.SuppressFinalize(this);
		}

		private void Realloc(int width, int height)
		{
			if (_gcHandle.IsAllocated)
				_gcHandle.Free();

			_width = width;
			_height = height;

			_pArray = new int[_width * _height];
			_gcHandle = GCHandle.Alloc(_pArray, GCHandleType.Pinned);
			_BI = new BITMAPINFO
			{
				biHeader =
				{
					bihBitCount = 32,
					bihPlanes = 1,
					bihSize = 40,
					bihWidth = _width,
					bihHeight = -_height,
					bihSizeImage = (_width * _height) << 2
				}
			};
		}

		public void Paint(HandleRef hRef, Bitmap bitmap, Rectangle dst)
		{
			try
			{
				if (bitmap == null || bitmap.Width == 0 || bitmap.Height == 0 || dst.Width == 0 || dst.Height == 0)
					return;

				if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
					return;

				var width = Math.Min(bitmap.Width, dst.Width);
				var height = Math.Min(bitmap.Height, dst.Height);

				if (width != _width || height != _height)
					Realloc(width, height);

				BitmapData BD = bitmap.LockBits(new Rectangle(0, 0, width, height),
												ImageLockMode.ReadOnly,
												PixelFormat.Format32bppArgb);

				Marshal.Copy(BD.Scan0, _pArray, 0, width * height);
				SetDIBitsToDevice(hRef, dst.X, dst.Y, width, height, 0, 0, 0, height, ref _pArray[0], ref _BI, 0);
				bitmap.UnlockBits(BD);
			}
			catch 
			{
			}
		}
	}
}
