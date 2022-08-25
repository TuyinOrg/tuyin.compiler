using compute.environment.imaging.IO;
using compute.environment.imaging.Plugins;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace compute.environment.imaging
{
    unsafe static partial class FreeImage
    {
        private readonly static FreeImageLibrary sExternLibrary = new FreeImageLibrary();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean GetHistogramDelegate0(FIBITMAP dib, Int32[] histo, FREE_IMAGE_COLOR_CHANNEL channel);
        private static readonly GetHistogramDelegate0 GetHistogram0 = sExternLibrary.GetStaticProc<GetHistogramDelegate0>("FreeImage_GetHistogram");
        public static Boolean GetHistogram(FIBITMAP dib, Int32[] histo, FREE_IMAGE_COLOR_CHANNEL channel) { return GetHistogram0(dib, histo, channel); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP GetChannelDelegate0(FIBITMAP dib, FREE_IMAGE_COLOR_CHANNEL channel);
        private static readonly GetChannelDelegate0 GetChannel0 = sExternLibrary.GetStaticProc<GetChannelDelegate0>("FreeImage_GetChannel");
        public static FIBITMAP GetChannel(FIBITMAP dib, FREE_IMAGE_COLOR_CHANNEL channel) { return GetChannel0(dib, channel); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SetChannelDelegate0(FIBITMAP dib, FIBITMAP dib8, FREE_IMAGE_COLOR_CHANNEL channel);
        private static readonly SetChannelDelegate0 SetChannel0 = sExternLibrary.GetStaticProc<SetChannelDelegate0>("FreeImage_SetChannel");
        public static Boolean SetChannel(FIBITMAP dib, FIBITMAP dib8, FREE_IMAGE_COLOR_CHANNEL channel) { return SetChannel0(dib, dib8, channel); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP GetComplexChannelDelegate0(FIBITMAP src, FREE_IMAGE_COLOR_CHANNEL channel);
        private static readonly GetComplexChannelDelegate0 GetComplexChannel0 = sExternLibrary.GetStaticProc<GetComplexChannelDelegate0>("FreeImage_GetComplexChannel");
        public static FIBITMAP GetComplexChannel(FIBITMAP src, FREE_IMAGE_COLOR_CHANNEL channel) { return GetComplexChannel0(src, channel); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SetComplexChannelDelegate0(FIBITMAP dst, FIBITMAP src, FREE_IMAGE_COLOR_CHANNEL channel);
        private static readonly SetComplexChannelDelegate0 SetComplexChannel0 = sExternLibrary.GetStaticProc<SetComplexChannelDelegate0>("FreeImage_SetComplexChannel");
        public static Boolean SetComplexChannel(FIBITMAP dst, FIBITMAP src, FREE_IMAGE_COLOR_CHANNEL channel) { return SetComplexChannel0(dst, src, channel); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP CopyDelegate0(FIBITMAP dib, Int32 left, Int32 top, Int32 right, Int32 bottom);
        private static readonly CopyDelegate0 Copy0 = sExternLibrary.GetStaticProc<CopyDelegate0>("FreeImage_Copy");
        public static FIBITMAP Copy(FIBITMAP dib, Int32 left, Int32 top, Int32 right, Int32 bottom) { return Copy0(dib, left, top, right, bottom); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean PasteDelegate0(FIBITMAP dst, FIBITMAP src, Int32 left, Int32 top, Int32 alpha);
        private static readonly PasteDelegate0 Paste0 = sExternLibrary.GetStaticProc<PasteDelegate0>("FreeImage_Paste");
        public static Boolean Paste(FIBITMAP dst, FIBITMAP src, Int32 left, Int32 top, Int32 alpha) { return Paste0(dst, src, left, top, alpha); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP CompositeDelegate0(FIBITMAP fg, Boolean useFileBkg, ref RGBQUAD appBkColor, FIBITMAP bg);
        private static readonly CompositeDelegate0 Composite0 = sExternLibrary.GetStaticProc<CompositeDelegate0>("FreeImage_Composite");
        public static FIBITMAP Composite(FIBITMAP fg, Boolean useFileBkg, ref RGBQUAD appBkColor, FIBITMAP bg) { return Composite0(fg, useFileBkg, ref appBkColor, bg); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP CompositeDelegate1(FIBITMAP fg, Boolean useFileBkg, RGBQUAD[] appBkColor, FIBITMAP bg);
        private static readonly CompositeDelegate1 Composite1 = sExternLibrary.GetStaticProc<CompositeDelegate1>("FreeImage_Composite");
        public static FIBITMAP Composite(FIBITMAP fg, Boolean useFileBkg, RGBQUAD[] appBkColor, FIBITMAP bg) { return Composite1(fg, useFileBkg, appBkColor, bg); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean JPEGCropADelegate0(String src_file, String dst_file, Int32 left, Int32 top, Int32 right, Int32 bottom);
        private static readonly JPEGCropADelegate0 JPEGCropA0 = sExternLibrary.GetStaticProc<JPEGCropADelegate0>("FreeImage_JPEGCrop");
        private static Boolean JPEGCropA(String src_file, String dst_file, Int32 left, Int32 top, Int32 right, Int32 bottom) { return JPEGCropA0(src_file, dst_file, left, top, right, bottom); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean JPEGCropUDelegate0(String src_file, String dst_file, Int32 left, Int32 top, Int32 right, Int32 bottom);
        private static readonly JPEGCropUDelegate0 JPEGCropU0 = sExternLibrary.GetStaticProc<JPEGCropUDelegate0>("FreeImage_JPEGCropU");
        private static Boolean JPEGCropU(String src_file, String dst_file, Int32 left, Int32 top, Int32 right, Int32 bottom) { return JPEGCropU0(src_file, dst_file, left, top, right, bottom); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean PreMultiplyWithAlphaDelegate0(FIBITMAP dib);
        private static readonly PreMultiplyWithAlphaDelegate0 PreMultiplyWithAlpha0 = sExternLibrary.GetStaticProc<PreMultiplyWithAlphaDelegate0>("FreeImage_PreMultiplyWithAlpha");
        public static Boolean PreMultiplyWithAlpha(FIBITMAP dib) { return PreMultiplyWithAlpha0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP MultigridPoissonSolverDelegate0(FIBITMAP Laplacian, Int32 ncycle);
        private static readonly MultigridPoissonSolverDelegate0 MultigridPoissonSolver0 = sExternLibrary.GetStaticProc<MultigridPoissonSolverDelegate0>("FreeImage_MultigridPoissonSolver");
        public static FIBITMAP MultigridPoissonSolver(FIBITMAP Laplacian, Int32 ncycle) { return MultigridPoissonSolver0(Laplacian, ncycle); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 GetAdjustColorsLookupTableDelegate0(Byte[] lookUpTable, Double brightness, Double contrast, Double gamma, Boolean invert);
        private static readonly GetAdjustColorsLookupTableDelegate0 GetAdjustColorsLookupTable0 = sExternLibrary.GetStaticProc<GetAdjustColorsLookupTableDelegate0>("FreeImage_GetAdjustColorsLookupTable");
        public static Int32 GetAdjustColorsLookupTable(Byte[] lookUpTable, Double brightness, Double contrast, Double gamma, Boolean invert) { return GetAdjustColorsLookupTable0(lookUpTable, brightness, contrast, gamma, invert); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean AdjustColorsDelegate0(FIBITMAP dib, Double brightness, Double contrast, Double gamma, Boolean invert);
        private static readonly AdjustColorsDelegate0 AdjustColors0 = sExternLibrary.GetStaticProc<AdjustColorsDelegate0>("FreeImage_AdjustColors");
        public static Boolean AdjustColors(FIBITMAP dib, Double brightness, Double contrast, Double gamma, Boolean invert) { return AdjustColors0(dib, brightness, contrast, gamma, invert); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 ApplyColorMappingDelegate0(FIBITMAP dib, RGBQUAD[] srccolors, RGBQUAD[] dstcolors, UInt32 count, Boolean ignore_alpha, Boolean swap);
        private static readonly ApplyColorMappingDelegate0 ApplyColorMapping0 = sExternLibrary.GetStaticProc<ApplyColorMappingDelegate0>("FreeImage_ApplyColorMapping");
        public static UInt32 ApplyColorMapping(FIBITMAP dib, RGBQUAD[] srccolors, RGBQUAD[] dstcolors, UInt32 count, Boolean ignore_alpha, Boolean swap) { return ApplyColorMapping0(dib, srccolors, dstcolors, count, ignore_alpha, swap); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 SwapColorsDelegate0(FIBITMAP dib, ref RGBQUAD color_a, ref RGBQUAD color_b, Boolean ignore_alpha);
        private static readonly SwapColorsDelegate0 SwapColors0 = sExternLibrary.GetStaticProc<SwapColorsDelegate0>("FreeImage_SwapColors");
        public static UInt32 SwapColors(FIBITMAP dib, ref RGBQUAD color_a, ref RGBQUAD color_b, Boolean ignore_alpha) { return SwapColors0(dib, ref color_a, ref color_b, ignore_alpha); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 ApplyPaletteIndexMappingDelegate0(FIBITMAP dib, Byte[] srcindices, Byte[] dstindices, UInt32 count, Boolean swap);
        private static readonly ApplyPaletteIndexMappingDelegate0 ApplyPaletteIndexMapping0 = sExternLibrary.GetStaticProc<ApplyPaletteIndexMappingDelegate0>("FreeImage_ApplyPaletteIndexMapping");
        public static UInt32 ApplyPaletteIndexMapping(FIBITMAP dib, Byte[] srcindices, Byte[] dstindices, UInt32 count, Boolean swap) { return ApplyPaletteIndexMapping0(dib, srcindices, dstindices, count, swap); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 SwapPaletteIndicesDelegate0(FIBITMAP dib, ref Byte index_a, ref Byte index_b);
        private static readonly SwapPaletteIndicesDelegate0 SwapPaletteIndices0 = sExternLibrary.GetStaticProc<SwapPaletteIndicesDelegate0>("FreeImage_SwapPaletteIndices");
        public static UInt32 SwapPaletteIndices(FIBITMAP dib, ref Byte index_a, ref Byte index_b) { return SwapPaletteIndices0(dib, ref index_a, ref index_b); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean FillBackgroundDelegate0(FIBITMAP dib, IntPtr color, FREE_IMAGE_COLOR_OPTIONS options);
        private static readonly FillBackgroundDelegate0 FillBackground0 = sExternLibrary.GetStaticProc<FillBackgroundDelegate0>("FreeImage_FillBackground");
        internal static Boolean FillBackground(FIBITMAP dib, IntPtr color, FREE_IMAGE_COLOR_OPTIONS options) { return FillBackground0(dib, color, options); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SetTagLengthDelegate0(FITAG tag, UInt32 length);
        private static readonly SetTagLengthDelegate0 SetTagLength0 = sExternLibrary.GetStaticProc<SetTagLengthDelegate0>("FreeImage_SetTagLength");
        public static Boolean SetTagLength(FITAG tag, UInt32 length) { return SetTagLength0(tag, length); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SetTagValueDelegate0(FITAG tag, Byte[] value);
        private static readonly SetTagValueDelegate0 SetTagValue0 = sExternLibrary.GetStaticProc<SetTagValueDelegate0>("FreeImage_SetTagValue");
        public static Boolean SetTagValue(FITAG tag, Byte[] value) { return SetTagValue0(tag, value); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIMETADATA FindFirstMetadataDelegate0(FREE_IMAGE_MDMODEL model, FIBITMAP dib, out FITAG tag);
        private static readonly FindFirstMetadataDelegate0 FindFirstMetadata0 = sExternLibrary.GetStaticProc<FindFirstMetadataDelegate0>("FreeImage_FindFirstMetadata");
        public static FIMETADATA FindFirstMetadata(FREE_IMAGE_MDMODEL model, FIBITMAP dib, out FITAG tag) { return FindFirstMetadata0(model, dib, out tag); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean FindNextMetadataDelegate0(FIMETADATA mdhandle, out FITAG tag);
        private static readonly FindNextMetadataDelegate0 FindNextMetadata0 = sExternLibrary.GetStaticProc<FindNextMetadataDelegate0>("FreeImage_FindNextMetadata");
        public static Boolean FindNextMetadata(FIMETADATA mdhandle, out FITAG tag) { return FindNextMetadata0(mdhandle, out tag); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FindCloseMetadata_Delegate0(FIMETADATA mdhandle);
        private static readonly FindCloseMetadata_Delegate0 FindCloseMetadata_0 = sExternLibrary.GetStaticProc<FindCloseMetadata_Delegate0>("FreeImage_FindCloseMetadata");
        private static void FindCloseMetadata_(FIMETADATA mdhandle) { FindCloseMetadata_0(mdhandle); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean GetMetadataDelegate0(FREE_IMAGE_MDMODEL model, FIBITMAP dib, String key, out FITAG tag);
        private static readonly GetMetadataDelegate0 GetMetadata0 = sExternLibrary.GetStaticProc<GetMetadataDelegate0>("FreeImage_GetMetadata");
        public static Boolean GetMetadata(FREE_IMAGE_MDMODEL model, FIBITMAP dib, String key, out FITAG tag) { return GetMetadata0(model, dib, key, out tag); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SetMetadataDelegate0(FREE_IMAGE_MDMODEL model, FIBITMAP dib, String key, FITAG tag);
        private static readonly SetMetadataDelegate0 SetMetadata0 = sExternLibrary.GetStaticProc<SetMetadataDelegate0>("FreeImage_SetMetadata");
        public static Boolean SetMetadata(FREE_IMAGE_MDMODEL model, FIBITMAP dib, String key, FITAG tag) { return SetMetadata0(model, dib, key, tag); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetMetadataCountDelegate0(FREE_IMAGE_MDMODEL model, FIBITMAP dib);
        private static readonly GetMetadataCountDelegate0 GetMetadataCount0 = sExternLibrary.GetStaticProc<GetMetadataCountDelegate0>("FreeImage_GetMetadataCount");
        public static UInt32 GetMetadataCount(FREE_IMAGE_MDMODEL model, FIBITMAP dib) { return GetMetadataCount0(model, dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean CloneMetadataDelegate0(FIBITMAP dst, FIBITMAP src);
        private static readonly CloneMetadataDelegate0 CloneMetadata0 = sExternLibrary.GetStaticProc<CloneMetadataDelegate0>("FreeImage_CloneMetadata");
        public static Boolean CloneMetadata(FIBITMAP dst, FIBITMAP src) { return CloneMetadata0(dst, src); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Byte* TagToString_Delegate0(FREE_IMAGE_MDMODEL model, FITAG tag, UInt32 Make);
        private static readonly TagToString_Delegate0 TagToString_0 = sExternLibrary.GetStaticProc<TagToString_Delegate0>("FreeImage_TagToString");
        private static Byte* TagToString_(FREE_IMAGE_MDMODEL model, FITAG tag, UInt32 Make) { return TagToString_0(model, tag, Make); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP RotateClassicDelegate0(FIBITMAP dib, Double angle);
        private static readonly RotateClassicDelegate0 RotateClassic0 = sExternLibrary.GetStaticProc<RotateClassicDelegate0>("FreeImage_RotateClassic");
        public static FIBITMAP RotateClassic(FIBITMAP dib, Double angle) { return RotateClassic0(dib, angle); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP RotateDelegate0(FIBITMAP dib, Double angle, IntPtr backgroundColor);
        private static readonly RotateDelegate0 Rotate0 = sExternLibrary.GetStaticProc<RotateDelegate0>("FreeImage_Rotate");
        internal static FIBITMAP Rotate(FIBITMAP dib, Double angle, IntPtr backgroundColor) { return Rotate0(dib, angle, backgroundColor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP RotateExDelegate0(FIBITMAP dib, Double angle, Double x_shift, Double y_shift, Double x_origin, Double y_origin, Boolean use_mask);
        private static readonly RotateExDelegate0 RotateEx0 = sExternLibrary.GetStaticProc<RotateExDelegate0>("FreeImage_RotateEx");
        public static FIBITMAP RotateEx(FIBITMAP dib, Double angle, Double x_shift, Double y_shift, Double x_origin, Double y_origin, Boolean use_mask) { return RotateEx0(dib, angle, x_shift, y_shift, x_origin, y_origin, use_mask); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean FlipHorizontalDelegate0(FIBITMAP dib);
        private static readonly FlipHorizontalDelegate0 FlipHorizontal0 = sExternLibrary.GetStaticProc<FlipHorizontalDelegate0>("FreeImage_FlipHorizontal");
        public static Boolean FlipHorizontal(FIBITMAP dib) { return FlipHorizontal0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean FlipVerticalDelegate0(FIBITMAP dib);
        private static readonly FlipVerticalDelegate0 FlipVertical0 = sExternLibrary.GetStaticProc<FlipVerticalDelegate0>("FreeImage_FlipVertical");
        public static Boolean FlipVertical(FIBITMAP dib) { return FlipVertical0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean JPEGTransformADelegate0(String src_file, String dst_file, FREE_IMAGE_JPEG_OPERATION operation, Boolean perfect);
        private static readonly JPEGTransformADelegate0 JPEGTransformA0 = sExternLibrary.GetStaticProc<JPEGTransformADelegate0>("FreeImage_JPEGTransform");
        private static Boolean JPEGTransformA(String src_file, String dst_file, FREE_IMAGE_JPEG_OPERATION operation, Boolean perfect) { return JPEGTransformA0(src_file, dst_file, operation, perfect); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean JPEGTransformUDelegate0(String src_file, String dst_file, FREE_IMAGE_JPEG_OPERATION operation, Boolean perfect);
        private static readonly JPEGTransformUDelegate0 JPEGTransformU0 = sExternLibrary.GetStaticProc<JPEGTransformUDelegate0>("FreeImage_JPEGTransformU");
        private static Boolean JPEGTransformU(String src_file, String dst_file, FREE_IMAGE_JPEG_OPERATION operation, Boolean perfect) { return JPEGTransformU0(src_file, dst_file, operation, perfect); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP RescaleDelegate0(FIBITMAP dib, Int32 dst_width, Int32 dst_height, FREE_IMAGE_FILTER filter);
        private static readonly RescaleDelegate0 Rescale0 = sExternLibrary.GetStaticProc<RescaleDelegate0>("FreeImage_Rescale");
        public static FIBITMAP Rescale(FIBITMAP dib, Int32 dst_width, Int32 dst_height, FREE_IMAGE_FILTER filter) { return Rescale0(dib, dst_width, dst_height, filter); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP MakeThumbnailDelegate0(FIBITMAP dib, Int32 max_pixel_size, Boolean convert);
        private static readonly MakeThumbnailDelegate0 MakeThumbnail0 = sExternLibrary.GetStaticProc<MakeThumbnailDelegate0>("FreeImage_MakeThumbnail");
        public static FIBITMAP MakeThumbnail(FIBITMAP dib, Int32 max_pixel_size, Boolean convert) { return MakeThumbnail0(dib, max_pixel_size, convert); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP EnlargeCanvasDelegate0(FIBITMAP dib, Int32 left, Int32 top, Int32 right, Int32 bottom, IntPtr color, FREE_IMAGE_COLOR_OPTIONS options);
        private static readonly EnlargeCanvasDelegate0 EnlargeCanvas0 = sExternLibrary.GetStaticProc<EnlargeCanvasDelegate0>("FreeImage_EnlargeCanvas");
        internal static FIBITMAP EnlargeCanvas(FIBITMAP dib, Int32 left, Int32 top, Int32 right, Int32 bottom, IntPtr color, FREE_IMAGE_COLOR_OPTIONS options) { return EnlargeCanvas0(dib, left, top, right, bottom, color, options); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean AdjustCurveDelegate0(FIBITMAP dib, Byte[] lookUpTable, FREE_IMAGE_COLOR_CHANNEL channel);
        private static readonly AdjustCurveDelegate0 AdjustCurve0 = sExternLibrary.GetStaticProc<AdjustCurveDelegate0>("FreeImage_AdjustCurve");
        public static Boolean AdjustCurve(FIBITMAP dib, Byte[] lookUpTable, FREE_IMAGE_COLOR_CHANNEL channel) { return AdjustCurve0(dib, lookUpTable, channel); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean AdjustGammaDelegate0(FIBITMAP dib, Double gamma);
        private static readonly AdjustGammaDelegate0 AdjustGamma0 = sExternLibrary.GetStaticProc<AdjustGammaDelegate0>("FreeImage_AdjustGamma");
        public static Boolean AdjustGamma(FIBITMAP dib, Double gamma) { return AdjustGamma0(dib, gamma); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean AdjustBrightnessDelegate0(FIBITMAP dib, Double percentage);
        private static readonly AdjustBrightnessDelegate0 AdjustBrightness0 = sExternLibrary.GetStaticProc<AdjustBrightnessDelegate0>("FreeImage_AdjustBrightness");
        public static Boolean AdjustBrightness(FIBITMAP dib, Double percentage) { return AdjustBrightness0(dib, percentage); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean AdjustContrastDelegate0(FIBITMAP dib, Double percentage);
        private static readonly AdjustContrastDelegate0 AdjustContrast0 = sExternLibrary.GetStaticProc<AdjustContrastDelegate0>("FreeImage_AdjustContrast");
        public static Boolean AdjustContrast(FIBITMAP dib, Double percentage) { return AdjustContrast0(dib, percentage); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean InvertDelegate0(FIBITMAP dib);
        private static readonly InvertDelegate0 Invert0 = sExternLibrary.GetStaticProc<InvertDelegate0>("FreeImage_Invert");
        public static Boolean Invert(FIBITMAP dib) { return Invert0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ConvertToTypeDelegate0(FIBITMAP src, FREE_IMAGE_TYPE dst_type, Boolean scale_linear);
        private static readonly ConvertToTypeDelegate0 ConvertToType0 = sExternLibrary.GetStaticProc<ConvertToTypeDelegate0>("FreeImage_ConvertToType");
        public static FIBITMAP ConvertToType(FIBITMAP src, FREE_IMAGE_TYPE dst_type, Boolean scale_linear) { return ConvertToType0(src, dst_type, scale_linear); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ToneMappingDelegate0(FIBITMAP dib, FREE_IMAGE_TMO tmo, Double first_param, Double second_param);
        private static readonly ToneMappingDelegate0 ToneMapping0 = sExternLibrary.GetStaticProc<ToneMappingDelegate0>("FreeImage_ToneMapping");
        public static FIBITMAP ToneMapping(FIBITMAP dib, FREE_IMAGE_TMO tmo, Double first_param, Double second_param) { return ToneMapping0(dib, tmo, first_param, second_param); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP TmoDrago03Delegate0(FIBITMAP src, Double gamma, Double exposure);
        private static readonly TmoDrago03Delegate0 TmoDrago030 = sExternLibrary.GetStaticProc<TmoDrago03Delegate0>("FreeImage_TmoDrago03");
        public static FIBITMAP TmoDrago03(FIBITMAP src, Double gamma, Double exposure) { return TmoDrago030(src, gamma, exposure); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP TmoReinhard05Delegate0(FIBITMAP src, Double intensity, Double contrast);
        private static readonly TmoReinhard05Delegate0 TmoReinhard050 = sExternLibrary.GetStaticProc<TmoReinhard05Delegate0>("FreeImage_TmoReinhard05");
        public static FIBITMAP TmoReinhard05(FIBITMAP src, Double intensity, Double contrast) { return TmoReinhard050(src, intensity, contrast); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP TmoFattal02Delegate0(FIBITMAP src, Double color_saturation, Double attenuation);
        private static readonly TmoFattal02Delegate0 TmoFattal020 = sExternLibrary.GetStaticProc<TmoFattal02Delegate0>("FreeImage_TmoFattal02");
        public static FIBITMAP TmoFattal02(FIBITMAP src, Double color_saturation, Double attenuation) { return TmoFattal020(src, color_saturation, attenuation); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 ZLibCompressDelegate0(Byte[] target, UInt32 target_size, Byte[] source, UInt32 source_size);
        private static readonly ZLibCompressDelegate0 ZLibCompress0 = sExternLibrary.GetStaticProc<ZLibCompressDelegate0>("FreeImage_ZLibCompress");
        public static UInt32 ZLibCompress(Byte[] target, UInt32 target_size, Byte[] source, UInt32 source_size) { return ZLibCompress0(target, target_size, source, source_size); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 ZLibUncompressDelegate0(Byte[] target, UInt32 target_size, Byte[] source, UInt32 source_size);
        private static readonly ZLibUncompressDelegate0 ZLibUncompress0 = sExternLibrary.GetStaticProc<ZLibUncompressDelegate0>("FreeImage_ZLibUncompress");
        public static UInt32 ZLibUncompress(Byte[] target, UInt32 target_size, Byte[] source, UInt32 source_size) { return ZLibUncompress0(target, target_size, source, source_size); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 ZLibGZipDelegate0(Byte[] target, UInt32 target_size, Byte[] source, UInt32 source_size);
        private static readonly ZLibGZipDelegate0 ZLibGZip0 = sExternLibrary.GetStaticProc<ZLibGZipDelegate0>("FreeImage_ZLibGZip");
        public static UInt32 ZLibGZip(Byte[] target, UInt32 target_size, Byte[] source, UInt32 source_size) { return ZLibGZip0(target, target_size, source, source_size); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 ZLibGUnzipDelegate0(Byte[] target, UInt32 target_size, Byte[] source, UInt32 source_size);
        private static readonly ZLibGUnzipDelegate0 ZLibGUnzip0 = sExternLibrary.GetStaticProc<ZLibGUnzipDelegate0>("FreeImage_ZLibGUnzip");
        public static UInt32 ZLibGUnzip(Byte[] target, UInt32 target_size, Byte[] source, UInt32 source_size) { return ZLibGUnzip0(target, target_size, source, source_size); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 ZLibCRC32Delegate0(UInt32 crc, Byte[] source, UInt32 source_size);
        private static readonly ZLibCRC32Delegate0 ZLibCRC320 = sExternLibrary.GetStaticProc<ZLibCRC32Delegate0>("FreeImage_ZLibCRC32");
        public static UInt32 ZLibCRC32(UInt32 crc, Byte[] source, UInt32 source_size) { return ZLibCRC320(crc, source, source_size); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FITAG CreateTagDelegate0();
        private static readonly CreateTagDelegate0 CreateTag0 = sExternLibrary.GetStaticProc<CreateTagDelegate0>("FreeImage_CreateTag");
        public static FITAG CreateTag() { return CreateTag0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void DeleteTagDelegate0(FITAG tag);
        private static readonly DeleteTagDelegate0 DeleteTag0 = sExternLibrary.GetStaticProc<DeleteTagDelegate0>("FreeImage_DeleteTag");
        public static void DeleteTag(FITAG tag) { DeleteTag0(tag); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FITAG CloneTagDelegate0(FITAG tag);
        private static readonly CloneTagDelegate0 CloneTag0 = sExternLibrary.GetStaticProc<CloneTagDelegate0>("FreeImage_CloneTag");
        public static FITAG CloneTag(FITAG tag) { return CloneTag0(tag); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Byte* GetTagKey_Delegate0(FITAG tag);
        private static readonly GetTagKey_Delegate0 GetTagKey_0 = sExternLibrary.GetStaticProc<GetTagKey_Delegate0>("FreeImage_GetTagKey");
        private static Byte* GetTagKey_(FITAG tag) { return GetTagKey_0(tag); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Byte* GetTagDescription_Delegate0(FITAG tag);
        private static readonly GetTagDescription_Delegate0 GetTagDescription_0 = sExternLibrary.GetStaticProc<GetTagDescription_Delegate0>("FreeImage_GetTagDescription");
        private static Byte* GetTagDescription_(FITAG tag) { return GetTagDescription_0(tag); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt16 GetTagIDDelegate0(FITAG tag);
        private static readonly GetTagIDDelegate0 GetTagID0 = sExternLibrary.GetStaticProc<GetTagIDDelegate0>("FreeImage_GetTagID");
        public static UInt16 GetTagID(FITAG tag) { return GetTagID0(tag); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FREE_IMAGE_MDTYPE GetTagTypeDelegate0(FITAG tag);
        private static readonly GetTagTypeDelegate0 GetTagType0 = sExternLibrary.GetStaticProc<GetTagTypeDelegate0>("FreeImage_GetTagType");
        public static FREE_IMAGE_MDTYPE GetTagType(FITAG tag) { return GetTagType0(tag); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetTagCountDelegate0(FITAG tag);
        private static readonly GetTagCountDelegate0 GetTagCount0 = sExternLibrary.GetStaticProc<GetTagCountDelegate0>("FreeImage_GetTagCount");
        public static UInt32 GetTagCount(FITAG tag) { return GetTagCount0(tag); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetTagLengthDelegate0(FITAG tag);
        private static readonly GetTagLengthDelegate0 GetTagLength0 = sExternLibrary.GetStaticProc<GetTagLengthDelegate0>("FreeImage_GetTagLength");
        public static UInt32 GetTagLength(FITAG tag) { return GetTagLength0(tag); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetTagValueDelegate0(FITAG tag);
        private static readonly GetTagValueDelegate0 GetTagValue0 = sExternLibrary.GetStaticProc<GetTagValueDelegate0>("FreeImage_GetTagValue");
        public static IntPtr GetTagValue(FITAG tag) { return GetTagValue0(tag); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SetTagKeyDelegate0(FITAG tag, String key);
        private static readonly SetTagKeyDelegate0 SetTagKey0 = sExternLibrary.GetStaticProc<SetTagKeyDelegate0>("FreeImage_SetTagKey");
        public static Boolean SetTagKey(FITAG tag, String key) { return SetTagKey0(tag, key); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SetTagDescriptionDelegate0(FITAG tag, String description);
        private static readonly SetTagDescriptionDelegate0 SetTagDescription0 = sExternLibrary.GetStaticProc<SetTagDescriptionDelegate0>("FreeImage_SetTagDescription");
        public static Boolean SetTagDescription(FITAG tag, String description) { return SetTagDescription0(tag, description); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SetTagIDDelegate0(FITAG tag, UInt16 id);
        private static readonly SetTagIDDelegate0 SetTagID0 = sExternLibrary.GetStaticProc<SetTagIDDelegate0>("FreeImage_SetTagID");
        public static Boolean SetTagID(FITAG tag, UInt16 id) { return SetTagID0(tag, id); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SetTagTypeDelegate0(FITAG tag, FREE_IMAGE_MDTYPE type);
        private static readonly SetTagTypeDelegate0 SetTagType0 = sExternLibrary.GetStaticProc<SetTagTypeDelegate0>("FreeImage_SetTagType");
        public static Boolean SetTagType(FITAG tag, FREE_IMAGE_MDTYPE type) { return SetTagType0(tag, type); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SetTagCountDelegate0(FITAG tag, UInt32 count);
        private static readonly SetTagCountDelegate0 SetTagCount0 = sExternLibrary.GetStaticProc<SetTagCountDelegate0>("FreeImage_SetTagCount");
        public static Boolean SetTagCount(FITAG tag, UInt32 count) { return SetTagCount0(tag, count); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean GetBackgroundColorDelegate0(FIBITMAP dib, out RGBQUAD bkcolor);
        private static readonly GetBackgroundColorDelegate0 GetBackgroundColor0 = sExternLibrary.GetStaticProc<GetBackgroundColorDelegate0>("FreeImage_GetBackgroundColor");
        public static Boolean GetBackgroundColor(FIBITMAP dib, out RGBQUAD bkcolor) { return GetBackgroundColor0(dib, out bkcolor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SetBackgroundColorDelegate0(FIBITMAP dib, ref RGBQUAD bkcolor);
        private static readonly SetBackgroundColorDelegate0 SetBackgroundColor0 = sExternLibrary.GetStaticProc<SetBackgroundColorDelegate0>("FreeImage_SetBackgroundColor");
        public static Boolean SetBackgroundColor(FIBITMAP dib, ref RGBQUAD bkcolor) { return SetBackgroundColor0(dib, ref bkcolor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SetBackgroundColorDelegate1(FIBITMAP dib, RGBQUAD[] bkcolor);
        private static readonly SetBackgroundColorDelegate1 SetBackgroundColor1 = sExternLibrary.GetStaticProc<SetBackgroundColorDelegate1>("FreeImage_SetBackgroundColor");
        public static Boolean SetBackgroundColor(FIBITMAP dib, RGBQUAD[] bkcolor) { return SetBackgroundColor1(dib, bkcolor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetTransparentIndexDelegate0(FIBITMAP dib, Int32 index);
        private static readonly SetTransparentIndexDelegate0 SetTransparentIndex0 = sExternLibrary.GetStaticProc<SetTransparentIndexDelegate0>("FreeImage_SetTransparentIndex");
        public static void SetTransparentIndex(FIBITMAP dib, Int32 index) { SetTransparentIndex0(dib, index); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 GetTransparentIndexDelegate0(FIBITMAP dib);
        private static readonly GetTransparentIndexDelegate0 GetTransparentIndex0 = sExternLibrary.GetStaticProc<GetTransparentIndexDelegate0>("FreeImage_GetTransparentIndex");
        public static Int32 GetTransparentIndex(FIBITMAP dib) { return GetTransparentIndex0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetICCProfileDelegate0(FIBITMAP dib);
        private static readonly GetICCProfileDelegate0 GetICCProfile0 = sExternLibrary.GetStaticProc<GetICCProfileDelegate0>("FreeImage_GetICCProfile");
        public static IntPtr GetICCProfile(FIBITMAP dib) { return GetICCProfile0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr CreateICCProfileDelegate0(FIBITMAP dib, Byte[] data, Int32 size);
        private static readonly CreateICCProfileDelegate0 CreateICCProfile0 = sExternLibrary.GetStaticProc<CreateICCProfileDelegate0>("FreeImage_CreateICCProfile");
        public static IntPtr CreateICCProfile(FIBITMAP dib, Byte[] data, Int32 size) { return CreateICCProfile0(dib, data, size); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void DestroyICCProfileDelegate0(FIBITMAP dib);
        private static readonly DestroyICCProfileDelegate0 DestroyICCProfile0 = sExternLibrary.GetStaticProc<DestroyICCProfileDelegate0>("FreeImage_DestroyICCProfile");
        public static void DestroyICCProfile(FIBITMAP dib) { DestroyICCProfile0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ConvertTo4BitsDelegate0(FIBITMAP dib);
        private static readonly ConvertTo4BitsDelegate0 ConvertTo4Bits0 = sExternLibrary.GetStaticProc<ConvertTo4BitsDelegate0>("FreeImage_ConvertTo4Bits");
        public static FIBITMAP ConvertTo4Bits(FIBITMAP dib) { return ConvertTo4Bits0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ConvertTo8BitsDelegate0(FIBITMAP dib);
        private static readonly ConvertTo8BitsDelegate0 ConvertTo8Bits0 = sExternLibrary.GetStaticProc<ConvertTo8BitsDelegate0>("FreeImage_ConvertTo8Bits");
        public static FIBITMAP ConvertTo8Bits(FIBITMAP dib) { return ConvertTo8Bits0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ConvertToGreyscaleDelegate0(FIBITMAP dib);
        private static readonly ConvertToGreyscaleDelegate0 ConvertToGreyscale0 = sExternLibrary.GetStaticProc<ConvertToGreyscaleDelegate0>("FreeImage_ConvertToGreyscale");
        public static FIBITMAP ConvertToGreyscale(FIBITMAP dib) { return ConvertToGreyscale0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ConvertTo16Bits555Delegate0(FIBITMAP dib);
        private static readonly ConvertTo16Bits555Delegate0 ConvertTo16Bits5550 = sExternLibrary.GetStaticProc<ConvertTo16Bits555Delegate0>("FreeImage_ConvertTo16Bits555");
        public static FIBITMAP ConvertTo16Bits555(FIBITMAP dib) { return ConvertTo16Bits5550(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ConvertTo16Bits565Delegate0(FIBITMAP dib);
        private static readonly ConvertTo16Bits565Delegate0 ConvertTo16Bits5650 = sExternLibrary.GetStaticProc<ConvertTo16Bits565Delegate0>("FreeImage_ConvertTo16Bits565");
        public static FIBITMAP ConvertTo16Bits565(FIBITMAP dib) { return ConvertTo16Bits5650(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ConvertTo24BitsDelegate0(FIBITMAP dib);
        private static readonly ConvertTo24BitsDelegate0 ConvertTo24Bits0 = sExternLibrary.GetStaticProc<ConvertTo24BitsDelegate0>("FreeImage_ConvertTo24Bits");
        public static FIBITMAP ConvertTo24Bits(FIBITMAP dib) { return ConvertTo24Bits0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ConvertTo32BitsDelegate0(FIBITMAP dib);
        private static readonly ConvertTo32BitsDelegate0 ConvertTo32Bits0 = sExternLibrary.GetStaticProc<ConvertTo32BitsDelegate0>("FreeImage_ConvertTo32Bits");
        public static FIBITMAP ConvertTo32Bits(FIBITMAP dib) { return ConvertTo32Bits0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ColorQuantizeDelegate0(FIBITMAP dib, FREE_IMAGE_QUANTIZE quantize);
        private static readonly ColorQuantizeDelegate0 ColorQuantize0 = sExternLibrary.GetStaticProc<ColorQuantizeDelegate0>("FreeImage_ColorQuantize");
        public static FIBITMAP ColorQuantize(FIBITMAP dib, FREE_IMAGE_QUANTIZE quantize) { return ColorQuantize0(dib, quantize); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ColorQuantizeExDelegate0(FIBITMAP dib, FREE_IMAGE_QUANTIZE quantize, Int32 PaletteSize, Int32 ReserveSize, RGBQUAD[] ReservePalette);
        private static readonly ColorQuantizeExDelegate0 ColorQuantizeEx0 = sExternLibrary.GetStaticProc<ColorQuantizeExDelegate0>("FreeImage_ColorQuantizeEx");
        public static FIBITMAP ColorQuantizeEx(FIBITMAP dib, FREE_IMAGE_QUANTIZE quantize, Int32 PaletteSize, Int32 ReserveSize, RGBQUAD[] ReservePalette) { return ColorQuantizeEx0(dib, quantize, PaletteSize, ReserveSize, ReservePalette); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ThresholdDelegate0(FIBITMAP dib, Byte t);
        private static readonly ThresholdDelegate0 Threshold0 = sExternLibrary.GetStaticProc<ThresholdDelegate0>("FreeImage_Threshold");
        public static FIBITMAP Threshold(FIBITMAP dib, Byte t) { return Threshold0(dib, t); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP DitherDelegate0(FIBITMAP dib, FREE_IMAGE_DITHER algorithm);
        private static readonly DitherDelegate0 Dither0 = sExternLibrary.GetStaticProc<DitherDelegate0>("FreeImage_Dither");
        public static FIBITMAP Dither(FIBITMAP dib, FREE_IMAGE_DITHER algorithm) { return Dither0(dib, algorithm); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ConvertFromRawBitsDelegate0(IntPtr bits, Int32 width, Int32 height, Int32 pitch, UInt32 bpp, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask, Boolean topdown);
        private static readonly ConvertFromRawBitsDelegate0 ConvertFromRawBits0 = sExternLibrary.GetStaticProc<ConvertFromRawBitsDelegate0>("FreeImage_ConvertFromRawBits");
        public static FIBITMAP ConvertFromRawBits(IntPtr bits, Int32 width, Int32 height, Int32 pitch, UInt32 bpp, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask, Boolean topdown) { return ConvertFromRawBits0(bits, width, height, pitch, bpp, red_mask, green_mask, blue_mask, topdown); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ConvertFromRawBitsDelegate1(Byte[] bits, Int32 width, Int32 height, Int32 pitch, UInt32 bpp, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask, Boolean topdown);
        private static readonly ConvertFromRawBitsDelegate1 ConvertFromRawBits1 = sExternLibrary.GetStaticProc<ConvertFromRawBitsDelegate1>("FreeImage_ConvertFromRawBits");
        public static FIBITMAP ConvertFromRawBits(Byte[] bits, Int32 width, Int32 height, Int32 pitch, UInt32 bpp, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask, Boolean topdown) { return ConvertFromRawBits1(bits, width, height, pitch, bpp, red_mask, green_mask, blue_mask, topdown); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void ConvertToRawBitsDelegate0(IntPtr bits, FIBITMAP dib, Int32 pitch, UInt32 bpp, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask, Boolean topdown);
        private static readonly ConvertToRawBitsDelegate0 ConvertToRawBits0 = sExternLibrary.GetStaticProc<ConvertToRawBitsDelegate0>("FreeImage_ConvertToRawBits");
        public static void ConvertToRawBits(IntPtr bits, FIBITMAP dib, Int32 pitch, UInt32 bpp, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask, Boolean topdown) { ConvertToRawBits0(bits, dib, pitch, bpp, red_mask, green_mask, blue_mask, topdown); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void ConvertToRawBitsDelegate1(Byte[] bits, FIBITMAP dib, Int32 pitch, UInt32 bpp, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask, Boolean topdown);
        private static readonly ConvertToRawBitsDelegate1 ConvertToRawBits1 = sExternLibrary.GetStaticProc<ConvertToRawBitsDelegate1>("FreeImage_ConvertToRawBits");
        public static void ConvertToRawBits(Byte[] bits, FIBITMAP dib, Int32 pitch, UInt32 bpp, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask, Boolean topdown) { ConvertToRawBits1(bits, dib, pitch, bpp, red_mask, green_mask, blue_mask, topdown); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ConvertToRGBFDelegate0(FIBITMAP dib);
        private static readonly ConvertToRGBFDelegate0 ConvertToRGBF0 = sExternLibrary.GetStaticProc<ConvertToRGBFDelegate0>("FreeImage_ConvertToRGBF");
        public static FIBITMAP ConvertToRGBF(FIBITMAP dib) { return ConvertToRGBF0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP ConvertToStandardTypeDelegate0(FIBITMAP src, Boolean scale_linear);
        private static readonly ConvertToStandardTypeDelegate0 ConvertToStandardType0 = sExternLibrary.GetStaticProc<ConvertToStandardTypeDelegate0>("FreeImage_ConvertToStandardType");
        public static FIBITMAP ConvertToStandardType(FIBITMAP src, Boolean scale_linear) { return ConvertToStandardType0(src, scale_linear); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FREE_IMAGE_TYPE GetImageTypeDelegate0(FIBITMAP dib);
        private static readonly GetImageTypeDelegate0 GetImageType0 = sExternLibrary.GetStaticProc<GetImageTypeDelegate0>("FreeImage_GetImageType");
        public static FREE_IMAGE_TYPE GetImageType(FIBITMAP dib) { return GetImageType0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetColorsUsedDelegate0(FIBITMAP dib);
        private static readonly GetColorsUsedDelegate0 GetColorsUsed0 = sExternLibrary.GetStaticProc<GetColorsUsedDelegate0>("FreeImage_GetColorsUsed");
        public static UInt32 GetColorsUsed(FIBITMAP dib) { return GetColorsUsed0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetBPPDelegate0(FIBITMAP dib);
        private static readonly GetBPPDelegate0 GetBPP0 = sExternLibrary.GetStaticProc<GetBPPDelegate0>("FreeImage_GetBPP");
        public static UInt32 GetBPP(FIBITMAP dib) { return GetBPP0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetWidthDelegate0(FIBITMAP dib);
        private static readonly GetWidthDelegate0 GetWidth0 = sExternLibrary.GetStaticProc<GetWidthDelegate0>("FreeImage_GetWidth");
        public static UInt32 GetWidth(FIBITMAP dib) { return GetWidth0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetHeightDelegate0(FIBITMAP dib);
        private static readonly GetHeightDelegate0 GetHeight0 = sExternLibrary.GetStaticProc<GetHeightDelegate0>("FreeImage_GetHeight");
        public static UInt32 GetHeight(FIBITMAP dib) { return GetHeight0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetLineDelegate0(FIBITMAP dib);
        private static readonly GetLineDelegate0 GetLine0 = sExternLibrary.GetStaticProc<GetLineDelegate0>("FreeImage_GetLine");
        public static UInt32 GetLine(FIBITMAP dib) { return GetLine0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetPitchDelegate0(FIBITMAP dib);
        private static readonly GetPitchDelegate0 GetPitch0 = sExternLibrary.GetStaticProc<GetPitchDelegate0>("FreeImage_GetPitch");
        public static UInt32 GetPitch(FIBITMAP dib) { return GetPitch0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetDIBSizeDelegate0(FIBITMAP dib);
        private static readonly GetDIBSizeDelegate0 GetDIBSize0 = sExternLibrary.GetStaticProc<GetDIBSizeDelegate0>("FreeImage_GetDIBSize");
        public static UInt32 GetDIBSize(FIBITMAP dib) { return GetDIBSize0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetPaletteDelegate0(FIBITMAP dib);
        private static readonly GetPaletteDelegate0 GetPalette0 = sExternLibrary.GetStaticProc<GetPaletteDelegate0>("FreeImage_GetPalette");
        public static IntPtr GetPalette(FIBITMAP dib) { return GetPalette0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetDotsPerMeterXDelegate0(FIBITMAP dib);
        private static readonly GetDotsPerMeterXDelegate0 GetDotsPerMeterX0 = sExternLibrary.GetStaticProc<GetDotsPerMeterXDelegate0>("FreeImage_GetDotsPerMeterX");
        public static UInt32 GetDotsPerMeterX(FIBITMAP dib) { return GetDotsPerMeterX0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetDotsPerMeterYDelegate0(FIBITMAP dib);
        private static readonly GetDotsPerMeterYDelegate0 GetDotsPerMeterY0 = sExternLibrary.GetStaticProc<GetDotsPerMeterYDelegate0>("FreeImage_GetDotsPerMeterY");
        public static UInt32 GetDotsPerMeterY(FIBITMAP dib) { return GetDotsPerMeterY0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetDotsPerMeterXDelegate0(FIBITMAP dib, UInt32 res);
        private static readonly SetDotsPerMeterXDelegate0 SetDotsPerMeterX0 = sExternLibrary.GetStaticProc<SetDotsPerMeterXDelegate0>("FreeImage_SetDotsPerMeterX");
        public static void SetDotsPerMeterX(FIBITMAP dib, UInt32 res) { SetDotsPerMeterX0(dib, res); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetDotsPerMeterYDelegate0(FIBITMAP dib, UInt32 res);
        private static readonly SetDotsPerMeterYDelegate0 SetDotsPerMeterY0 = sExternLibrary.GetStaticProc<SetDotsPerMeterYDelegate0>("FreeImage_SetDotsPerMeterY");
        public static void SetDotsPerMeterY(FIBITMAP dib, UInt32 res) { SetDotsPerMeterY0(dib, res); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetInfoHeaderDelegate0(FIBITMAP dib);
        private static readonly GetInfoHeaderDelegate0 GetInfoHeader0 = sExternLibrary.GetStaticProc<GetInfoHeaderDelegate0>("FreeImage_GetInfoHeader");
        public static IntPtr GetInfoHeader(FIBITMAP dib) { return GetInfoHeader0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetInfoDelegate0(FIBITMAP dib);
        private static readonly GetInfoDelegate0 GetInfo0 = sExternLibrary.GetStaticProc<GetInfoDelegate0>("FreeImage_GetInfo");
        public static IntPtr GetInfo(FIBITMAP dib) { return GetInfo0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FREE_IMAGE_COLOR_TYPE GetColorTypeDelegate0(FIBITMAP dib);
        private static readonly GetColorTypeDelegate0 GetColorType0 = sExternLibrary.GetStaticProc<GetColorTypeDelegate0>("FreeImage_GetColorType");
        public static FREE_IMAGE_COLOR_TYPE GetColorType(FIBITMAP dib) { return GetColorType0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetRedMaskDelegate0(FIBITMAP dib);
        private static readonly GetRedMaskDelegate0 GetRedMask0 = sExternLibrary.GetStaticProc<GetRedMaskDelegate0>("FreeImage_GetRedMask");
        public static UInt32 GetRedMask(FIBITMAP dib) { return GetRedMask0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetGreenMaskDelegate0(FIBITMAP dib);
        private static readonly GetGreenMaskDelegate0 GetGreenMask0 = sExternLibrary.GetStaticProc<GetGreenMaskDelegate0>("FreeImage_GetGreenMask");
        public static UInt32 GetGreenMask(FIBITMAP dib) { return GetGreenMask0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetBlueMaskDelegate0(FIBITMAP dib);
        private static readonly GetBlueMaskDelegate0 GetBlueMask0 = sExternLibrary.GetStaticProc<GetBlueMaskDelegate0>("FreeImage_GetBlueMask");
        public static UInt32 GetBlueMask(FIBITMAP dib) { return GetBlueMask0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetTransparencyCountDelegate0(FIBITMAP dib);
        private static readonly GetTransparencyCountDelegate0 GetTransparencyCount0 = sExternLibrary.GetStaticProc<GetTransparencyCountDelegate0>("FreeImage_GetTransparencyCount");
        public static UInt32 GetTransparencyCount(FIBITMAP dib) { return GetTransparencyCount0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetTransparencyTableDelegate0(FIBITMAP dib);
        private static readonly GetTransparencyTableDelegate0 GetTransparencyTable0 = sExternLibrary.GetStaticProc<GetTransparencyTableDelegate0>("FreeImage_GetTransparencyTable");
        public static IntPtr GetTransparencyTable(FIBITMAP dib) { return GetTransparencyTable0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetTransparentDelegate0(FIBITMAP dib, Boolean enabled);
        private static readonly SetTransparentDelegate0 SetTransparent0 = sExternLibrary.GetStaticProc<SetTransparentDelegate0>("FreeImage_SetTransparent");
        public static void SetTransparent(FIBITMAP dib, Boolean enabled) { SetTransparent0(dib, enabled); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetTransparencyTableDelegate0(FIBITMAP dib, Byte[] table, Int32 count);
        private static readonly SetTransparencyTableDelegate0 SetTransparencyTable0 = sExternLibrary.GetStaticProc<SetTransparencyTableDelegate0>("FreeImage_SetTransparencyTable");
        internal static void SetTransparencyTable(FIBITMAP dib, Byte[] table, Int32 count) { SetTransparencyTable0(dib, table, count); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean IsTransparentDelegate0(FIBITMAP dib);
        private static readonly IsTransparentDelegate0 IsTransparent0 = sExternLibrary.GetStaticProc<IsTransparentDelegate0>("FreeImage_IsTransparent");
        public static Boolean IsTransparent(FIBITMAP dib) { return IsTransparent0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean HasBackgroundColorDelegate0(FIBITMAP dib);
        private static readonly HasBackgroundColorDelegate0 HasBackgroundColor0 = sExternLibrary.GetStaticProc<HasBackgroundColorDelegate0>("FreeImage_HasBackgroundColor");
        public static Boolean HasBackgroundColor(FIBITMAP dib) { return HasBackgroundColor0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean FIFSupportsICCProfilesDelegate0(FREE_IMAGE_FORMAT fif);
        private static readonly FIFSupportsICCProfilesDelegate0 FIFSupportsICCProfiles0 = sExternLibrary.GetStaticProc<FIFSupportsICCProfilesDelegate0>("FreeImage_FIFSupportsICCProfiles");
        public static Boolean FIFSupportsICCProfiles(FREE_IMAGE_FORMAT fif) { return FIFSupportsICCProfiles0(fif); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIMULTIBITMAP OpenMultiBitmapDelegate0(FREE_IMAGE_FORMAT fif, String filename, Boolean create_new, Boolean read_only, Boolean keep_cache_in_memory, FREE_IMAGE_LOAD_FLAGS flags);
        private static readonly OpenMultiBitmapDelegate0 OpenMultiBitmap0 = sExternLibrary.GetStaticProc<OpenMultiBitmapDelegate0>("FreeImage_OpenMultiBitmap");
        public static FIMULTIBITMAP OpenMultiBitmap(FREE_IMAGE_FORMAT fif, String filename, Boolean create_new, Boolean read_only, Boolean keep_cache_in_memory, FREE_IMAGE_LOAD_FLAGS flags) { return OpenMultiBitmap0(fif, filename, create_new, read_only, keep_cache_in_memory, flags); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIMULTIBITMAP OpenMultiBitmapFromHandleDelegate0(FREE_IMAGE_FORMAT fif, ref FreeImageIO io, fi_handle handle, FREE_IMAGE_LOAD_FLAGS flags);
        private static readonly OpenMultiBitmapFromHandleDelegate0 OpenMultiBitmapFromHandle0 = sExternLibrary.GetStaticProc<OpenMultiBitmapFromHandleDelegate0>("FreeImage_OpenMultiBitmapFromHandle");
        public static FIMULTIBITMAP OpenMultiBitmapFromHandle(FREE_IMAGE_FORMAT fif, ref FreeImageIO io, fi_handle handle, FREE_IMAGE_LOAD_FLAGS flags) { return OpenMultiBitmapFromHandle0(fif, ref io, handle, flags); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean CloseMultiBitmap_Delegate0(FIMULTIBITMAP bitmap, FREE_IMAGE_SAVE_FLAGS flags);
        private static readonly CloseMultiBitmap_Delegate0 CloseMultiBitmap_0 = sExternLibrary.GetStaticProc<CloseMultiBitmap_Delegate0>("FreeImage_CloseMultiBitmap");
        private static Boolean CloseMultiBitmap_(FIMULTIBITMAP bitmap, FREE_IMAGE_SAVE_FLAGS flags) { return CloseMultiBitmap_0(bitmap, flags); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 GetPageCountDelegate0(FIMULTIBITMAP bitmap);
        private static readonly GetPageCountDelegate0 GetPageCount0 = sExternLibrary.GetStaticProc<GetPageCountDelegate0>("FreeImage_GetPageCount");
        public static Int32 GetPageCount(FIMULTIBITMAP bitmap) { return GetPageCount0(bitmap); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void AppendPageDelegate0(FIMULTIBITMAP bitmap, FIBITMAP data);
        private static readonly AppendPageDelegate0 AppendPage0 = sExternLibrary.GetStaticProc<AppendPageDelegate0>("FreeImage_AppendPage");
        public static void AppendPage(FIMULTIBITMAP bitmap, FIBITMAP data) { AppendPage0(bitmap, data); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void InsertPageDelegate0(FIMULTIBITMAP bitmap, Int32 page, FIBITMAP data);
        private static readonly InsertPageDelegate0 InsertPage0 = sExternLibrary.GetStaticProc<InsertPageDelegate0>("FreeImage_InsertPage");
        public static void InsertPage(FIMULTIBITMAP bitmap, Int32 page, FIBITMAP data) { InsertPage0(bitmap, page, data); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void DeletePageDelegate0(FIMULTIBITMAP bitmap, Int32 page);
        private static readonly DeletePageDelegate0 DeletePage0 = sExternLibrary.GetStaticProc<DeletePageDelegate0>("FreeImage_DeletePage");
        public static void DeletePage(FIMULTIBITMAP bitmap, Int32 page) { DeletePage0(bitmap, page); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP LockPageDelegate0(FIMULTIBITMAP bitmap, Int32 page);
        private static readonly LockPageDelegate0 LockPage0 = sExternLibrary.GetStaticProc<LockPageDelegate0>("FreeImage_LockPage");
        public static FIBITMAP LockPage(FIMULTIBITMAP bitmap, Int32 page) { return LockPage0(bitmap, page); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void UnlockPageDelegate0(FIMULTIBITMAP bitmap, FIBITMAP data, Boolean changed);
        private static readonly UnlockPageDelegate0 UnlockPage0 = sExternLibrary.GetStaticProc<UnlockPageDelegate0>("FreeImage_UnlockPage");
        public static void UnlockPage(FIMULTIBITMAP bitmap, FIBITMAP data, Boolean changed) { UnlockPage0(bitmap, data, changed); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean MovePageDelegate0(FIMULTIBITMAP bitmap, Int32 target, Int32 source);
        private static readonly MovePageDelegate0 MovePage0 = sExternLibrary.GetStaticProc<MovePageDelegate0>("FreeImage_MovePage");
        public static Boolean MovePage(FIMULTIBITMAP bitmap, Int32 target, Int32 source) { return MovePage0(bitmap, target, source); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean GetLockedPageNumbersDelegate0(FIMULTIBITMAP bitmap, Int32[] pages, ref Int32 count);
        private static readonly GetLockedPageNumbersDelegate0 GetLockedPageNumbers0 = sExternLibrary.GetStaticProc<GetLockedPageNumbersDelegate0>("FreeImage_GetLockedPageNumbers");
        public static Boolean GetLockedPageNumbers(FIMULTIBITMAP bitmap, Int32[] pages, ref Int32 count) { return GetLockedPageNumbers0(bitmap, pages, ref count); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FREE_IMAGE_FORMAT GetFileTypeADelegate0(String filename, Int32 size);
        private static readonly GetFileTypeADelegate0 GetFileTypeA0 = sExternLibrary.GetStaticProc<GetFileTypeADelegate0>("FreeImage_GetFileType");
        private static FREE_IMAGE_FORMAT GetFileTypeA(String filename, Int32 size) { return GetFileTypeA0(filename, size); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FREE_IMAGE_FORMAT GetFileTypeUDelegate0(String filename, Int32 size);
        private static readonly GetFileTypeUDelegate0 GetFileTypeU0 = sExternLibrary.GetStaticProc<GetFileTypeUDelegate0>("FreeImage_GetFileTypeU");
        private static FREE_IMAGE_FORMAT GetFileTypeU(String filename, Int32 size) { return GetFileTypeU0(filename, size); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FREE_IMAGE_FORMAT GetFileTypeFromHandleDelegate0(ref FreeImageIO io, fi_handle handle, Int32 size);
        private static readonly GetFileTypeFromHandleDelegate0 GetFileTypeFromHandle0 = sExternLibrary.GetStaticProc<GetFileTypeFromHandleDelegate0>("FreeImage_GetFileTypeFromHandle");
        public static FREE_IMAGE_FORMAT GetFileTypeFromHandle(ref FreeImageIO io, fi_handle handle, Int32 size) { return GetFileTypeFromHandle0(ref io, handle, size); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FREE_IMAGE_FORMAT GetFileTypeFromMemoryDelegate0(FIMEMORY stream, Int32 size);
        private static readonly GetFileTypeFromMemoryDelegate0 GetFileTypeFromMemory0 = sExternLibrary.GetStaticProc<GetFileTypeFromMemoryDelegate0>("FreeImage_GetFileTypeFromMemory");
        public static FREE_IMAGE_FORMAT GetFileTypeFromMemory(FIMEMORY stream, Int32 size) { return GetFileTypeFromMemory0(stream, size); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean IsLittleEndianDelegate0();
        private static readonly IsLittleEndianDelegate0 IsLittleEndian0 = sExternLibrary.GetStaticProc<IsLittleEndianDelegate0>("FreeImage_IsLittleEndian");
        public static Boolean IsLittleEndian() { return IsLittleEndian0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean LookupX11ColorDelegate0(String szColor, out Byte nRed, out Byte nGreen, out Byte nBlue);
        private static readonly LookupX11ColorDelegate0 LookupX11Color0 = sExternLibrary.GetStaticProc<LookupX11ColorDelegate0>("FreeImage_LookupX11Color");
        public static Boolean LookupX11Color(String szColor, out Byte nRed, out Byte nGreen, out Byte nBlue) { return LookupX11Color0(szColor, out nRed, out nGreen, out nBlue); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean LookupSVGColorDelegate0(String szColor, out Byte nRed, out Byte nGreen, out Byte nBlue);
        private static readonly LookupSVGColorDelegate0 LookupSVGColor0 = sExternLibrary.GetStaticProc<LookupSVGColorDelegate0>("FreeImage_LookupSVGColor");
        public static Boolean LookupSVGColor(String szColor, out Byte nRed, out Byte nGreen, out Byte nBlue) { return LookupSVGColor0(szColor, out nRed, out nGreen, out nBlue); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetBitsDelegate0(FIBITMAP dib);
        private static readonly GetBitsDelegate0 GetBits0 = sExternLibrary.GetStaticProc<GetBitsDelegate0>("FreeImage_GetBits");
        public static IntPtr GetBits(FIBITMAP dib) { return GetBits0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetScanLineDelegate0(FIBITMAP dib, Int32 scanline);
        private static readonly GetScanLineDelegate0 GetScanLine0 = sExternLibrary.GetStaticProc<GetScanLineDelegate0>("FreeImage_GetScanLine");
        public static IntPtr GetScanLine(FIBITMAP dib, Int32 scanline) { return GetScanLine0(dib, scanline); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean GetPixelIndexDelegate0(FIBITMAP dib, UInt32 x, UInt32 y, out Byte value);
        private static readonly GetPixelIndexDelegate0 GetPixelIndex0 = sExternLibrary.GetStaticProc<GetPixelIndexDelegate0>("FreeImage_GetPixelIndex");
        public static Boolean GetPixelIndex(FIBITMAP dib, UInt32 x, UInt32 y, out Byte value) { return GetPixelIndex0(dib, x, y, out value); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean GetPixelColorDelegate0(FIBITMAP dib, UInt32 x, UInt32 y, out RGBQUAD value);
        private static readonly GetPixelColorDelegate0 GetPixelColor0 = sExternLibrary.GetStaticProc<GetPixelColorDelegate0>("FreeImage_GetPixelColor");
        public static Boolean GetPixelColor(FIBITMAP dib, UInt32 x, UInt32 y, out RGBQUAD value) { return GetPixelColor0(dib, x, y, out value); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SetPixelIndexDelegate0(FIBITMAP dib, UInt32 x, UInt32 y, ref Byte value);
        private static readonly SetPixelIndexDelegate0 SetPixelIndex0 = sExternLibrary.GetStaticProc<SetPixelIndexDelegate0>("FreeImage_SetPixelIndex");
        public static Boolean SetPixelIndex(FIBITMAP dib, UInt32 x, UInt32 y, ref Byte value) { return SetPixelIndex0(dib, x, y, ref value); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SetPixelColorDelegate0(FIBITMAP dib, UInt32 x, UInt32 y, ref RGBQUAD value);
        private static readonly SetPixelColorDelegate0 SetPixelColor0 = sExternLibrary.GetStaticProc<SetPixelColorDelegate0>("FreeImage_SetPixelColor");
        public static Boolean SetPixelColor(FIBITMAP dib, UInt32 x, UInt32 y, ref RGBQUAD value) { return SetPixelColor0(dib, x, y, ref value); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SeekMemoryDelegate0(FIMEMORY stream, Int32 offset, SeekOrigin origin);
        private static readonly SeekMemoryDelegate0 SeekMemory0 = sExternLibrary.GetStaticProc<SeekMemoryDelegate0>("FreeImage_SeekMemory");
        public static Boolean SeekMemory(FIMEMORY stream, Int32 offset, SeekOrigin origin) { return SeekMemory0(stream, offset, origin); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean AcquireMemoryDelegate0(FIMEMORY stream, ref IntPtr data, ref UInt32 size_in_bytes);
        private static readonly AcquireMemoryDelegate0 AcquireMemory0 = sExternLibrary.GetStaticProc<AcquireMemoryDelegate0>("FreeImage_AcquireMemory");
        public static Boolean AcquireMemory(FIMEMORY stream, ref IntPtr data, ref UInt32 size_in_bytes) { return AcquireMemory0(stream, ref data, ref size_in_bytes); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 ReadMemoryDelegate0(Byte[] buffer, UInt32 size, UInt32 count, FIMEMORY stream);
        private static readonly ReadMemoryDelegate0 ReadMemory0 = sExternLibrary.GetStaticProc<ReadMemoryDelegate0>("FreeImage_ReadMemory");
        public static UInt32 ReadMemory(Byte[] buffer, UInt32 size, UInt32 count, FIMEMORY stream) { return ReadMemory0(buffer, size, count, stream); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 WriteMemoryDelegate0(Byte[] buffer, UInt32 size, UInt32 count, FIMEMORY stream);
        private static readonly WriteMemoryDelegate0 WriteMemory0 = sExternLibrary.GetStaticProc<WriteMemoryDelegate0>("FreeImage_WriteMemory");
        public static UInt32 WriteMemory(Byte[] buffer, UInt32 size, UInt32 count, FIMEMORY stream) { return WriteMemory0(buffer, size, count, stream); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIMULTIBITMAP LoadMultiBitmapFromMemoryDelegate0(FREE_IMAGE_FORMAT fif, FIMEMORY stream, FREE_IMAGE_LOAD_FLAGS flags);
        private static readonly LoadMultiBitmapFromMemoryDelegate0 LoadMultiBitmapFromMemory0 = sExternLibrary.GetStaticProc<LoadMultiBitmapFromMemoryDelegate0>("FreeImage_LoadMultiBitmapFromMemory");
        public static FIMULTIBITMAP LoadMultiBitmapFromMemory(FREE_IMAGE_FORMAT fif, FIMEMORY stream, FREE_IMAGE_LOAD_FLAGS flags) { return LoadMultiBitmapFromMemory0(fif, stream, flags); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FREE_IMAGE_FORMAT RegisterLocalPluginDelegate0(InitProc proc_address, String format, String description, String extension, String regexpr);
        private static readonly RegisterLocalPluginDelegate0 RegisterLocalPlugin0 = sExternLibrary.GetStaticProc<RegisterLocalPluginDelegate0>("FreeImage_RegisterLocalPlugin");
        public static FREE_IMAGE_FORMAT RegisterLocalPlugin(InitProc proc_address, String format, String description, String extension, String regexpr) { return RegisterLocalPlugin0(proc_address, format, description, extension, regexpr); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FREE_IMAGE_FORMAT RegisterExternalPluginDelegate0(String path, String format, String description, String extension, String regexpr);
        private static readonly RegisterExternalPluginDelegate0 RegisterExternalPlugin0 = sExternLibrary.GetStaticProc<RegisterExternalPluginDelegate0>("FreeImage_RegisterExternalPlugin");
        public static FREE_IMAGE_FORMAT RegisterExternalPlugin(String path, String format, String description, String extension, String regexpr) { return RegisterExternalPlugin0(path, format, description, extension, regexpr); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 GetFIFCountDelegate0();
        private static readonly GetFIFCountDelegate0 GetFIFCount0 = sExternLibrary.GetStaticProc<GetFIFCountDelegate0>("FreeImage_GetFIFCount");
        public static Int32 GetFIFCount() { return GetFIFCount0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 SetPluginEnabledDelegate0(FREE_IMAGE_FORMAT fif, Boolean enable);
        private static readonly SetPluginEnabledDelegate0 SetPluginEnabled0 = sExternLibrary.GetStaticProc<SetPluginEnabledDelegate0>("FreeImage_SetPluginEnabled");
        public static Int32 SetPluginEnabled(FREE_IMAGE_FORMAT fif, Boolean enable) { return SetPluginEnabled0(fif, enable); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 IsPluginEnabledDelegate0(FREE_IMAGE_FORMAT fif);
        private static readonly IsPluginEnabledDelegate0 IsPluginEnabled0 = sExternLibrary.GetStaticProc<IsPluginEnabledDelegate0>("FreeImage_IsPluginEnabled");
        public static Int32 IsPluginEnabled(FREE_IMAGE_FORMAT fif) { return IsPluginEnabled0(fif); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FREE_IMAGE_FORMAT GetFIFFromFormatDelegate0(String format);
        private static readonly GetFIFFromFormatDelegate0 GetFIFFromFormat0 = sExternLibrary.GetStaticProc<GetFIFFromFormatDelegate0>("FreeImage_GetFIFFromFormat");
        public static FREE_IMAGE_FORMAT GetFIFFromFormat(String format) { return GetFIFFromFormat0(format); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FREE_IMAGE_FORMAT GetFIFFromMimeDelegate0(String mime);
        private static readonly GetFIFFromMimeDelegate0 GetFIFFromMime0 = sExternLibrary.GetStaticProc<GetFIFFromMimeDelegate0>("FreeImage_GetFIFFromMime");
        public static FREE_IMAGE_FORMAT GetFIFFromMime(String mime) { return GetFIFFromMime0(mime); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Byte* GetFormatFromFIF_Delegate0(FREE_IMAGE_FORMAT fif);
        private static readonly GetFormatFromFIF_Delegate0 GetFormatFromFIF_0 = sExternLibrary.GetStaticProc<GetFormatFromFIF_Delegate0>("FreeImage_GetFormatFromFIF");
        private static Byte* GetFormatFromFIF_(FREE_IMAGE_FORMAT fif) { return GetFormatFromFIF_0(fif); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Byte* GetFIFExtensionList_Delegate0(FREE_IMAGE_FORMAT fif);
        private static readonly GetFIFExtensionList_Delegate0 GetFIFExtensionList_0 = sExternLibrary.GetStaticProc<GetFIFExtensionList_Delegate0>("FreeImage_GetFIFExtensionList");
        private static Byte* GetFIFExtensionList_(FREE_IMAGE_FORMAT fif) { return GetFIFExtensionList_0(fif); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Byte* GetFIFDescription_Delegate0(FREE_IMAGE_FORMAT fif);
        private static readonly GetFIFDescription_Delegate0 GetFIFDescription_0 = sExternLibrary.GetStaticProc<GetFIFDescription_Delegate0>("FreeImage_GetFIFDescription");
        private static Byte* GetFIFDescription_(FREE_IMAGE_FORMAT fif) { return GetFIFDescription_0(fif); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Byte* GetFIFRegExpr_Delegate0(FREE_IMAGE_FORMAT fif);
        private static readonly GetFIFRegExpr_Delegate0 GetFIFRegExpr_0 = sExternLibrary.GetStaticProc<GetFIFRegExpr_Delegate0>("FreeImage_GetFIFRegExpr");
        private static Byte* GetFIFRegExpr_(FREE_IMAGE_FORMAT fif) { return GetFIFRegExpr_0(fif); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Byte* GetFIFMimeType_Delegate0(FREE_IMAGE_FORMAT fif);
        private static readonly GetFIFMimeType_Delegate0 GetFIFMimeType_0 = sExternLibrary.GetStaticProc<GetFIFMimeType_Delegate0>("FreeImage_GetFIFMimeType");
        private static Byte* GetFIFMimeType_(FREE_IMAGE_FORMAT fif) { return GetFIFMimeType_0(fif); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FREE_IMAGE_FORMAT GetFIFFromFilenameADelegate0(String filename);
        private static readonly GetFIFFromFilenameADelegate0 GetFIFFromFilenameA0 = sExternLibrary.GetStaticProc<GetFIFFromFilenameADelegate0>("FreeImage_GetFIFFromFilename");
        private static FREE_IMAGE_FORMAT GetFIFFromFilenameA(String filename) { return GetFIFFromFilenameA0(filename); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FREE_IMAGE_FORMAT GetFIFFromFilenameUDelegate0(String filename);
        private static readonly GetFIFFromFilenameUDelegate0 GetFIFFromFilenameU0 = sExternLibrary.GetStaticProc<GetFIFFromFilenameUDelegate0>("FreeImage_GetFIFFromFilenameU");
        private static FREE_IMAGE_FORMAT GetFIFFromFilenameU(String filename) { return GetFIFFromFilenameU0(filename); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean FIFSupportsReadingDelegate0(FREE_IMAGE_FORMAT fif);
        private static readonly FIFSupportsReadingDelegate0 FIFSupportsReading0 = sExternLibrary.GetStaticProc<FIFSupportsReadingDelegate0>("FreeImage_FIFSupportsReading");
        public static Boolean FIFSupportsReading(FREE_IMAGE_FORMAT fif) { return FIFSupportsReading0(fif); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean FIFSupportsWritingDelegate0(FREE_IMAGE_FORMAT fif);
        private static readonly FIFSupportsWritingDelegate0 FIFSupportsWriting0 = sExternLibrary.GetStaticProc<FIFSupportsWritingDelegate0>("FreeImage_FIFSupportsWriting");
        public static Boolean FIFSupportsWriting(FREE_IMAGE_FORMAT fif) { return FIFSupportsWriting0(fif); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean FIFSupportsExportBPPDelegate0(FREE_IMAGE_FORMAT fif, Int32 bpp);
        private static readonly FIFSupportsExportBPPDelegate0 FIFSupportsExportBPP0 = sExternLibrary.GetStaticProc<FIFSupportsExportBPPDelegate0>("FreeImage_FIFSupportsExportBPP");
        public static Boolean FIFSupportsExportBPP(FREE_IMAGE_FORMAT fif, Int32 bpp) { return FIFSupportsExportBPP0(fif, bpp); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean FIFSupportsExportTypeDelegate0(FREE_IMAGE_FORMAT fif, FREE_IMAGE_TYPE type);
        private static readonly FIFSupportsExportTypeDelegate0 FIFSupportsExportType0 = sExternLibrary.GetStaticProc<FIFSupportsExportTypeDelegate0>("FreeImage_FIFSupportsExportType");
        public static Boolean FIFSupportsExportType(FREE_IMAGE_FORMAT fif, FREE_IMAGE_TYPE type) { return FIFSupportsExportType0(fif, type); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void InitialiseDelegate0(Boolean load_local_plugins_only);
        private static readonly InitialiseDelegate0 Initialise0 = sExternLibrary.GetStaticProc<InitialiseDelegate0>("FreeImage_Initialise");
        private static void Initialise(Boolean load_local_plugins_only) { Initialise0(load_local_plugins_only); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void DeInitialiseDelegate0();
        private static readonly DeInitialiseDelegate0 DeInitialise0 = sExternLibrary.GetStaticProc<DeInitialiseDelegate0>("FreeImage_DeInitialise");
        private static void DeInitialise() { DeInitialise0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Byte* GetVersion_Delegate0();
        private static readonly GetVersion_Delegate0 GetVersion_0 = sExternLibrary.GetStaticProc<GetVersion_Delegate0>("FreeImage_GetVersion");
        private static Byte* GetVersion_() { return GetVersion_0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Byte* GetCopyrightMessage_Delegate0();
        private static readonly GetCopyrightMessage_Delegate0 GetCopyrightMessage_0 = sExternLibrary.GetStaticProc<GetCopyrightMessage_Delegate0>("FreeImage_GetCopyrightMessage");
        private static Byte* GetCopyrightMessage_() { return GetCopyrightMessage_0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void OutputMessageProcDelegate0(FREE_IMAGE_FORMAT fif, String message);
        private static readonly OutputMessageProcDelegate0 OutputMessageProc0 = sExternLibrary.GetStaticProc<OutputMessageProcDelegate0>("FreeImage_OutputMessageProc");
        public static void OutputMessageProc(FREE_IMAGE_FORMAT fif, String message) { OutputMessageProc0(fif, message); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetOutputMessageDelegate0(OutputMessageFunction omf);
        private static readonly SetOutputMessageDelegate0 SetOutputMessage0 = sExternLibrary.GetStaticProc<SetOutputMessageDelegate0>("FreeImage_SetOutputMessage");
        internal static void SetOutputMessage(OutputMessageFunction omf) { SetOutputMessage0(omf); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP AllocateDelegate0(Int32 width, Int32 height, Int32 bpp, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask);
        private static readonly AllocateDelegate0 Allocate0 = sExternLibrary.GetStaticProc<AllocateDelegate0>("FreeImage_Allocate");
        public static FIBITMAP Allocate(Int32 width, Int32 height, Int32 bpp, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask) { return Allocate0(width, height, bpp, red_mask, green_mask, blue_mask); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP AllocateTDelegate0(FREE_IMAGE_TYPE type, Int32 width, Int32 height, Int32 bpp, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask);
        private static readonly AllocateTDelegate0 AllocateT0 = sExternLibrary.GetStaticProc<AllocateTDelegate0>("FreeImage_AllocateT");
        public static FIBITMAP AllocateT(FREE_IMAGE_TYPE type, Int32 width, Int32 height, Int32 bpp, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask) { return AllocateT0(type, width, height, bpp, red_mask, green_mask, blue_mask); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP AllocateExDelegate0(Int32 width, Int32 height, Int32 bpp, IntPtr color, FREE_IMAGE_COLOR_OPTIONS options, RGBQUAD[] palette, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask);
        private static readonly AllocateExDelegate0 AllocateEx0 = sExternLibrary.GetStaticProc<AllocateExDelegate0>("FreeImage_AllocateEx");
        internal static FIBITMAP AllocateEx(Int32 width, Int32 height, Int32 bpp, IntPtr color, FREE_IMAGE_COLOR_OPTIONS options, RGBQUAD[] palette, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask) { return AllocateEx0(width, height, bpp, color, options, palette, red_mask, green_mask, blue_mask); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP AllocateExTDelegate0(FREE_IMAGE_TYPE type, Int32 width, Int32 height, Int32 bpp, IntPtr color, FREE_IMAGE_COLOR_OPTIONS options, RGBQUAD[] palette, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask);
        private static readonly AllocateExTDelegate0 AllocateExT0 = sExternLibrary.GetStaticProc<AllocateExTDelegate0>("FreeImage_AllocateExT");
        internal static FIBITMAP AllocateExT(FREE_IMAGE_TYPE type, Int32 width, Int32 height, Int32 bpp, IntPtr color, FREE_IMAGE_COLOR_OPTIONS options, RGBQUAD[] palette, UInt32 red_mask, UInt32 green_mask, UInt32 blue_mask) { return AllocateExT0(type, width, height, bpp, color, options, palette, red_mask, green_mask, blue_mask); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP CloneDelegate0(FIBITMAP dib);
        private static readonly CloneDelegate0 Clone0 = sExternLibrary.GetStaticProc<CloneDelegate0>("FreeImage_Clone");
        public static FIBITMAP Clone(FIBITMAP dib) { return Clone0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void UnloadDelegate0(FIBITMAP dib);
        private static readonly UnloadDelegate0 Unload0 = sExternLibrary.GetStaticProc<UnloadDelegate0>("FreeImage_Unload");
        public static void Unload(FIBITMAP dib) { Unload0(dib); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP LoadADelegate0(FREE_IMAGE_FORMAT fif, String filename, FREE_IMAGE_LOAD_FLAGS flags);
        private static readonly LoadADelegate0 LoadA0 = sExternLibrary.GetStaticProc<LoadADelegate0>("FreeImage_Load");
        private static FIBITMAP LoadA(FREE_IMAGE_FORMAT fif, String filename, FREE_IMAGE_LOAD_FLAGS flags) { return LoadA0(fif, filename, flags); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP LoadUDelegate0(FREE_IMAGE_FORMAT fif, String filename, FREE_IMAGE_LOAD_FLAGS flags);
        private static readonly LoadUDelegate0 LoadU0 = sExternLibrary.GetStaticProc<LoadUDelegate0>("FreeImage_LoadU");
        private static FIBITMAP LoadU(FREE_IMAGE_FORMAT fif, String filename, FREE_IMAGE_LOAD_FLAGS flags) { return LoadU0(fif, filename, flags); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP LoadFromHandleDelegate0(FREE_IMAGE_FORMAT fif, ref FreeImageIO io, fi_handle handle, FREE_IMAGE_LOAD_FLAGS flags);
        private static readonly LoadFromHandleDelegate0 LoadFromHandle0 = sExternLibrary.GetStaticProc<LoadFromHandleDelegate0>("FreeImage_LoadFromHandle");
        public static FIBITMAP LoadFromHandle(FREE_IMAGE_FORMAT fif, ref FreeImageIO io, fi_handle handle, FREE_IMAGE_LOAD_FLAGS flags) { return LoadFromHandle0(fif, ref io, handle, flags); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SaveADelegate0(FREE_IMAGE_FORMAT fif, FIBITMAP dib, String filename, FREE_IMAGE_SAVE_FLAGS flags);
        private static readonly SaveADelegate0 SaveA0 = sExternLibrary.GetStaticProc<SaveADelegate0>("FreeImage_Save");
        private static Boolean SaveA(FREE_IMAGE_FORMAT fif, FIBITMAP dib, String filename, FREE_IMAGE_SAVE_FLAGS flags) { return SaveA0(fif, dib, filename, flags); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SaveUDelegate0(FREE_IMAGE_FORMAT fif, FIBITMAP dib, String filename, FREE_IMAGE_SAVE_FLAGS flags);
        private static readonly SaveUDelegate0 SaveU0 = sExternLibrary.GetStaticProc<SaveUDelegate0>("FreeImage_SaveU");
        private static Boolean SaveU(FREE_IMAGE_FORMAT fif, FIBITMAP dib, String filename, FREE_IMAGE_SAVE_FLAGS flags) { return SaveU0(fif, dib, filename, flags); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SaveToHandleDelegate0(FREE_IMAGE_FORMAT fif, FIBITMAP dib, ref FreeImageIO io, fi_handle handle, FREE_IMAGE_SAVE_FLAGS flags);
        private static readonly SaveToHandleDelegate0 SaveToHandle0 = sExternLibrary.GetStaticProc<SaveToHandleDelegate0>("FreeImage_SaveToHandle");
        public static Boolean SaveToHandle(FREE_IMAGE_FORMAT fif, FIBITMAP dib, ref FreeImageIO io, fi_handle handle, FREE_IMAGE_SAVE_FLAGS flags) { return SaveToHandle0(fif, dib, ref io, handle, flags); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIMEMORY OpenMemoryDelegate0(IntPtr data, UInt32 size_in_bytes);
        private static readonly OpenMemoryDelegate0 OpenMemory0 = sExternLibrary.GetStaticProc<OpenMemoryDelegate0>("FreeImage_OpenMemory");
        public static FIMEMORY OpenMemory(IntPtr data, UInt32 size_in_bytes) { return OpenMemory0(data, size_in_bytes); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIMEMORY OpenMemoryExDelegate0(Byte[] data, UInt32 size_in_bytes);
        private static readonly OpenMemoryExDelegate0 OpenMemoryEx0 = sExternLibrary.GetStaticProc<OpenMemoryExDelegate0>("FreeImage_OpenMemory");
        internal static FIMEMORY OpenMemoryEx(Byte[] data, UInt32 size_in_bytes) { return OpenMemoryEx0(data, size_in_bytes); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void CloseMemoryDelegate0(FIMEMORY stream);
        private static readonly CloseMemoryDelegate0 CloseMemory0 = sExternLibrary.GetStaticProc<CloseMemoryDelegate0>("FreeImage_CloseMemory");
        public static void CloseMemory(FIMEMORY stream) { CloseMemory0(stream); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FIBITMAP LoadFromMemoryDelegate0(FREE_IMAGE_FORMAT fif, FIMEMORY stream, FREE_IMAGE_LOAD_FLAGS flags);
        private static readonly LoadFromMemoryDelegate0 LoadFromMemory0 = sExternLibrary.GetStaticProc<LoadFromMemoryDelegate0>("FreeImage_LoadFromMemory");
        public static FIBITMAP LoadFromMemory(FREE_IMAGE_FORMAT fif, FIMEMORY stream, FREE_IMAGE_LOAD_FLAGS flags) { return LoadFromMemory0(fif, stream, flags); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean SaveToMemoryDelegate0(FREE_IMAGE_FORMAT fif, FIBITMAP dib, FIMEMORY stream, FREE_IMAGE_SAVE_FLAGS flags);
        private static readonly SaveToMemoryDelegate0 SaveToMemory0 = sExternLibrary.GetStaticProc<SaveToMemoryDelegate0>("FreeImage_SaveToMemory");
        public static Boolean SaveToMemory(FREE_IMAGE_FORMAT fif, FIBITMAP dib, FIMEMORY stream, FREE_IMAGE_SAVE_FLAGS flags) { return SaveToMemory0(fif, dib, stream, flags); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 TellMemoryDelegate0(FIMEMORY stream);
        private static readonly TellMemoryDelegate0 TellMemory0 = sExternLibrary.GetStaticProc<TellMemoryDelegate0>("FreeImage_TellMemory");
        public static Int32 TellMemory(FIMEMORY stream) { return TellMemory0(stream); }


    }
}
