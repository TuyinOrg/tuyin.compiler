// ==========================================================
// FreeImage 3 .NET wrapper
// Original FreeImage 3 functions and .NET compatible derived functions
//
// Design and implementation by
// - Jean-Philippe Goerke (jpgoerke@users.sourceforge.net)
// - Carsten Klein (cklein05@users.sourceforge.net)
//
// Contributors:
// - David Boland (davidboland@vodafone.ie)
//
// Main reference : MSDN Knowlede Base
//
// This file is part of FreeImage 3
//
// COVERED CODE IS PROVIDED UNDER THIS LICENSE ON AN "AS IS" BASIS, WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, WITHOUT LIMITATION, WARRANTIES
// THAT THE COVERED CODE IS FREE OF DEFECTS, MERCHANTABLE, FIT FOR A PARTICULAR PURPOSE
// OR NON-INFRINGING. THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE COVERED
// CODE IS WITH YOU. SHOULD ANY COVERED CODE PROVE DEFECTIVE IN ANY RESPECT, YOU (NOT
// THE INITIAL DEVELOPER OR ANY OTHER CONTRIBUTOR) ASSUME THE COST OF ANY NECESSARY
// SERVICING, REPAIR OR CORRECTION. THIS DISCLAIMER OF WARRANTY CONSTITUTES AN ESSENTIAL
// PART OF THIS LICENSE. NO USE OF ANY COVERED CODE IS AUTHORIZED HEREUNDER EXCEPT UNDER
// THIS DISCLAIMER.
//
// Use at your own risk!
// ==========================================================

// ==========================================================
// CVS
// $Revision: 1.9 $
// $Date: 2009/09/15 11:41:37 $
// $Id: FreeImageStaticImports.cs,v 1.9 2009/09/15 11:41:37 cklein05 Exp $
// ==========================================================

using System;
using System.Runtime.InteropServices;
using compute.environment.imaging.Plugins;
using compute.environment.imaging.IO;

namespace compute.environment.imaging
{
    static partial class FreeImage
    {
#if NET462 || NET461 || NET46 || NET452 || NET451 || NET45 || NET40 || NET35 || NET20
        // this isn't really valid... could be Mono on another platform... how do we tell in this case?
        public const bool IsWindows = true;
#else
        public static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#endif

        private static readonly bool SupportsUnicode = IsWindows;

        #region Constants

        /// <summary>
        /// Filename of the FreeImage library.
        /// </summary>
        private const string FreeImageLibrary = "FreeImage";

        /// <summary>
        /// Number of bytes to shift left within a 4 byte block.
        /// </summary>
        public const int FI_RGBA_RED = 2;

        /// <summary>
        /// Number of bytes to shift left within a 4 byte block.
        /// </summary>
        public const int FI_RGBA_GREEN = 1;

        /// <summary>
        /// Number of bytes to shift left within a 4 byte block.
        /// </summary>
        public const int FI_RGBA_BLUE = 0;

        /// <summary>
        /// Number of bytes to shift left within a 4 byte block.
        /// </summary>
        public const int FI_RGBA_ALPHA = 3;

        /// <summary>
        /// Mask indicating the position of the given color.
        /// </summary>
        public const uint FI_RGBA_RED_MASK = 0x00FF0000;

        /// <summary>
        /// Mask indicating the position of the given color.
        /// </summary>
        public const uint FI_RGBA_GREEN_MASK = 0x0000FF00;

        /// <summary>
        /// Mask indicating the position of the given color.
        /// </summary>
        public const uint FI_RGBA_BLUE_MASK = 0x000000FF;

        /// <summary>
        /// Mask indicating the position of the given color.
        /// </summary>
        public const uint FI_RGBA_ALPHA_MASK = 0xFF000000;

        /// <summary>
        /// Number of bits to shift left within a 32 bit block.
        /// </summary>
        public const int FI_RGBA_RED_SHIFT = 16;

        /// <summary>
        /// Number of bits to shift left within a 32 bit block.
        /// </summary>
        public const int FI_RGBA_GREEN_SHIFT = 8;

        /// <summary>
        /// Number of bits to shift left within a 32 bit block.
        /// </summary>
        public const int FI_RGBA_BLUE_SHIFT = 0;

        /// <summary>
        /// Number of bits to shift left within a 32 bit block.
        /// </summary>
        public const int FI_RGBA_ALPHA_SHIFT = 24;

        /// <summary>
        /// Mask indicating the position of color components of a 32 bit color.
        /// </summary>
        public const uint FI_RGBA_RGB_MASK = (FI_RGBA_RED_MASK | FI_RGBA_GREEN_MASK | FI_RGBA_BLUE_MASK);

        /// <summary>
        /// Mask indicating the position of the given color.
        /// </summary>
        public const int FI16_555_RED_MASK = 0x7C00;

        /// <summary>
        /// Mask indicating the position of the given color.
        /// </summary>
        public const int FI16_555_GREEN_MASK = 0x03E0;

        /// <summary>
        /// Mask indicating the position of the given color.
        /// </summary>
        public const int FI16_555_BLUE_MASK = 0x001F;

        /// <summary>
        /// Number of bits to shift left within a 16 bit block.
        /// </summary>
        public const int FI16_555_RED_SHIFT = 10;

        /// <summary>
        /// Number of bits to shift left within a 16 bit block.
        /// </summary>
        public const int FI16_555_GREEN_SHIFT = 5;

        /// <summary>
        /// Number of bits to shift left within a 16 bit block.
        /// </summary>
        public const int FI16_555_BLUE_SHIFT = 0;

        /// <summary>
        /// Mask indicating the position of the given color.
        /// </summary>
        public const int FI16_565_RED_MASK = 0xF800;

        /// <summary>
        /// Mask indicating the position of the given color.
        /// </summary>
        public const int FI16_565_GREEN_MASK = 0x07E0;

        /// <summary>
        /// Mask indicating the position of the given color.
        /// </summary>
        public const int FI16_565_BLUE_MASK = 0x001F;

        /// <summary>
        /// Number of bits to shift left within a 16 bit block.
        /// </summary>
        public const int FI16_565_RED_SHIFT = 11;

        /// <summary>
        /// Number of bits to shift left within a 16 bit block.
        /// </summary>
        public const int FI16_565_GREEN_SHIFT = 5;

        /// <summary>
        /// Number of bits to shift left within a 16 bit block.
        /// </summary>
        public const int FI16_565_BLUE_SHIFT = 0;

        #endregion

        #region General functions


        /// <summary>
        /// Returns a string containing the current version of the library.
        /// </summary>
        /// <returns>The current version of the library.</returns>
        public static unsafe string GetVersion() { return PtrToStr(GetVersion_()); }

        /// <summary>
        /// Returns a string containing a standard copyright message.
        /// </summary>
        /// <returns>A standard copyright message.</returns>
        public static unsafe string GetCopyrightMessage() { return PtrToStr(GetCopyrightMessage_()); }

        #endregion

        #region Bitmap management functions




        /// <summary>
        /// Decodes a bitmap, allocates memory for it and returns it as a FIBITMAP.
        /// </summary>
        /// <param name="fif">Type of the bitmap.</param>
        /// <param name="filename">Name of the file to decode.</param>
        /// <param name="flags">Flags to enable or disable plugin-features.</param>
        /// <returns>Handle to a FreeImage bitmap.</returns>
        public static FIBITMAP Load(FREE_IMAGE_FORMAT fif, string filename, FREE_IMAGE_LOAD_FLAGS flags)
        {
            if (SupportsUnicode)
            {
                return LoadU(fif, filename, flags);
            }
            else
            {
                return LoadA(fif, filename, flags);
            }
        }

        /// <summary>
        /// Saves a previosly loaded FIBITMAP to a file.
        /// </summary>
        /// <param name="fif">Type of the bitmap.</param>
        /// <param name="dib">Handle to a FreeImage bitmap.</param>
        /// <param name="filename">Name of the file to save to.</param>
        /// <param name="flags">Flags to enable or disable plugin-features.</param>
        /// <returns>Returns true on success, false on failure.</returns>
        public static bool Save(FREE_IMAGE_FORMAT fif, FIBITMAP dib, string filename, FREE_IMAGE_SAVE_FLAGS flags)
        {
            if (SupportsUnicode)
            {
                return SaveU(fif, dib, filename, flags);
            }
            else
            {
                return SaveA(fif, dib, filename, flags);
            }
        }

        #endregion

        #region Plugin functions

        /// <summary>
        /// Returns the string that was used to register a plugin from the system assigned <see cref="FREE_IMAGE_FORMAT"/>.
        /// </summary>
        /// <param name="fif">The assigned <see cref="FREE_IMAGE_FORMAT"/>.</param>
        /// <returns>The string that was used to register the plugin.</returns>
        public static unsafe string GetFormatFromFIF(FREE_IMAGE_FORMAT fif) { return PtrToStr(GetFormatFromFIF_(fif)); }

        /// <summary>
        /// Returns a comma-delimited file extension list describing the bitmap formats the given plugin can read and/or write.
        /// </summary>
        /// <param name="fif">The desired <see cref="FREE_IMAGE_FORMAT"/>.</param>
        /// <returns>A comma-delimited file extension list.</returns>
        public static unsafe string GetFIFExtensionList(FREE_IMAGE_FORMAT fif) { return PtrToStr(GetFIFExtensionList_(fif)); }

        /// <summary>
        /// Returns a descriptive string that describes the bitmap formats the given plugin can read and/or write.
        /// </summary>
        /// <param name="fif">The desired <see cref="FREE_IMAGE_FORMAT"/>.</param>
        /// <returns>A descriptive string that describes the bitmap formats.</returns>
        public static unsafe string GetFIFDescription(FREE_IMAGE_FORMAT fif) { return PtrToStr(GetFIFDescription_(fif)); }

        /// <summary>
        /// Returns a regular expression string that can be used by a regular expression engine to identify the bitmap.
        /// FreeImageQt makes use of this function.
        /// </summary>
        /// <param name="fif">The desired <see cref="FREE_IMAGE_FORMAT"/>.</param>
        /// <returns>A regular expression string.</returns>
        public static unsafe string GetFIFRegExpr(FREE_IMAGE_FORMAT fif) { return PtrToStr(GetFIFRegExpr_(fif)); }

        /// <summary>
        /// Given a <see cref="FREE_IMAGE_FORMAT"/> identifier, returns a MIME content type string (MIME stands for Multipurpose Internet Mail Extension).
        /// </summary>
        /// <param name="fif">The desired <see cref="FREE_IMAGE_FORMAT"/>.</param>
        /// <returns>A MIME content type string.</returns>
        public static unsafe string GetFIFMimeType(FREE_IMAGE_FORMAT fif) { return PtrToStr(GetFIFMimeType_(fif)); }

        /// <summary>
        /// This function takes a filename or a file-extension and returns the plugin that can
        /// read/write files with that extension in the form of a <see cref="FREE_IMAGE_FORMAT"/> identifier.
        /// </summary>
        /// <param name="filename">The filename or -extension.</param>
        /// <returns>The <see cref="FREE_IMAGE_FORMAT"/> of the plugin.</returns>
        public static FREE_IMAGE_FORMAT GetFIFFromFilename(string filename)
        {
            if (SupportsUnicode)
            {
                return GetFIFFromFilenameU(filename);
            }
            else
            {
                return GetFIFFromFilenameA(filename);
            }
        }

        #endregion

        #region Filetype functions

        /// <summary>
        /// Orders FreeImage to analyze the bitmap signature.
        /// </summary>
        /// <param name="filename">Name of the file to analyze.</param>
        /// <param name="size">Reserved parameter - use 0.</param>
        /// <returns>Type of the bitmap.</returns>
        public static FREE_IMAGE_FORMAT GetFileType(string filename, int size)
        {
            if (SupportsUnicode)
            {
                return GetFileTypeU(filename, size);
            }
            else
            {
                return GetFileTypeA(filename, size);
            }
        }

        #endregion

        #region ICC profile functions

        /// <summary>
        /// Retrieves the <see cref="FIICCPROFILE"/> data of the bitmap.
        /// This function can also be called safely, when the original format does not support profiles.
        /// </summary>
        /// <param name="dib">Handle to a FreeImage bitmap.</param>
        /// <returns>The <see cref="FIICCPROFILE"/> data of the bitmap.</returns>
        public static FIICCPROFILE GetICCProfileEx(FIBITMAP dib) { unsafe { return *(FIICCPROFILE*)FreeImage.GetICCProfile(dib); } }

        #endregion

        #region Tag accessors

        /// <summary>
        /// Returns the tag field name (unique inside a metadata model).
        /// </summary>
        /// <param name="tag">The tag field.</param>
        /// <returns>The field name.</returns>
        public static unsafe string GetTagKey(FITAG tag) { return PtrToStr(GetTagKey_(tag)); }

        /// <summary>
        /// Returns the tag description.
        /// </summary>
        /// <param name="tag">The tag field.</param>
        /// <returns>The description or NULL if unavailable.</returns>
        public static unsafe string GetTagDescription(FITAG tag) { return PtrToStr(GetTagDescription_(tag)); }

        #endregion

        #region Metadata helper functions

        /// <summary>
        /// Converts a FreeImage tag structure to a string that represents the interpreted tag value.
        /// The function is not thread safe.
        /// </summary>
        /// <param name="model">The metadata model.</param>
        /// <param name="tag">The interpreted tag value.</param>
        /// <param name="Make">Reserved.</param>
        /// <returns>The representing string.</returns>
        public static unsafe string TagToString(FREE_IMAGE_MDMODEL model, FITAG tag, uint Make) { return PtrToStr(TagToString_(model, tag, Make)); }

        #endregion

        #region Rotation and flipping

        /// <summary>
        /// Performs a lossless rotation or flipping on a JPEG file.
        /// </summary>
        /// <param name="src_file">Source file.</param>
        /// <param name="dst_file">Destination file; can be the source file; will be overwritten.</param>
        /// <param name="operation">The operation to apply.</param>
        /// <param name="perfect">To avoid lossy transformation, you can set the perfect parameter to true.</param>
        /// <returns>Returns true on success, false on failure.</returns>
        public static bool JPEGTransform(string src_file, string dst_file,
            FREE_IMAGE_JPEG_OPERATION operation, bool perfect)
        {
            if (SupportsUnicode)
            {
                return JPEGTransformU(src_file, dst_file, operation, perfect);
            }
            else
            {
                return JPEGTransformA(src_file, dst_file, operation, perfect);
            }
        }

        #endregion

        #region Copy / Paste / Composite routines


        /// <summary>
        /// Performs a lossless crop on a JPEG file.
        /// </summary>
        /// <param name="src_file">Source filename.</param>
        /// <param name="dst_file">Destination filename.</param>
        /// <param name="left">Specifies the left position of the cropped rectangle.</param>
        /// <param name="top">Specifies the top position of the cropped rectangle.</param>
        /// <param name="right">Specifies the right position of the cropped rectangle.</param>
        /// <param name="bottom">Specifies the bottom position of the cropped rectangle.</param>
        /// <returns>Returns true on success, false on failure.</returns>
        public static bool JPEGCrop(string src_file, string dst_file, int left, int top, int right, int bottom)
        {
            if (SupportsUnicode)
            {
                return JPEGCropU(src_file, dst_file, left, top, right, bottom);
            }
            else
            {
                return JPEGCropA(src_file, dst_file, left, top, right, bottom);
            }
        }

        #endregion
    }
}