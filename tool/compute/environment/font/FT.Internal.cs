#region MIT License
/*Copyright (c) 2012-2016 Robert Rouhani <robert.rouhani@gmail.com>

compute.environment.font based on Tao.FreeType, Copyright (c) 2003-2007 Tao Framework Team

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/
#endregion

using compute.environment.font.Cache;
using compute.environment.font.Internal;
using compute.environment.font.PostScript;
using compute.environment.font.PostScript.Internal;
using compute.environment.font.TrueType;
using compute.environment.font.TrueType.Internal;
using System;
using System.Runtime.InteropServices;

namespace compute.environment.font
{
    /// <content>
    /// This file contains all the raw FreeType2 function signatures.
    /// </content>
    unsafe static partial class FT
	{
		static bool? isMacOS;

		/// <summary>
		/// Returns true if the current .net platform is macOS.
		/// </summary>
		internal static bool IsMacOS
		{
			get
			{
				if (isMacOS != null)
					return isMacOS.Value;
				else
				{
					lock (typeof(FT))
					{
						if (isMacOS == null) // repeat the test
						{
							isMacOS = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
						}
					}
				}

				return isMacOS.Value;
			}
		}

		/// <summary>
		/// Defines the calling convention for P/Invoking the native freetype methods.
		/// </summary>
		private const CallingConvention CallConvention = CallingConvention.Cdecl;

		#region Externs

		private static readonly FreeTypeLibrary sExternLibrary = new FreeTypeLibrary();

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FTC_CMapCache_NewDelegate0(IntPtr manager, out IntPtr acache);
		private static readonly FTC_CMapCache_NewDelegate0 FTC_CMapCache_New0 = sExternLibrary.GetStaticProc<FTC_CMapCache_NewDelegate0>("FTC_CMapCache_New");
		internal static Error FTC_CMapCache_New(IntPtr manager, out IntPtr acache) { return FTC_CMapCache_New0(manager, out acache); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 FTC_CMapCache_LookupDelegate0(IntPtr cache, IntPtr face_id, Int32 cmap_index, UInt32 char_code);
		private static readonly FTC_CMapCache_LookupDelegate0 FTC_CMapCache_Lookup0 = sExternLibrary.GetStaticProc<FTC_CMapCache_LookupDelegate0>("FTC_CMapCache_Lookup");
		internal static UInt32 FTC_CMapCache_Lookup(IntPtr cache, IntPtr face_id, Int32 cmap_index, UInt32 char_code) { return FTC_CMapCache_Lookup0(cache, face_id, cmap_index, char_code); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FTC_ImageCache_NewDelegate0(IntPtr manager, out IntPtr acache);
		private static readonly FTC_ImageCache_NewDelegate0 FTC_ImageCache_New0 = sExternLibrary.GetStaticProc<FTC_ImageCache_NewDelegate0>("FTC_ImageCache_New");
		internal static Error FTC_ImageCache_New(IntPtr manager, out IntPtr acache) { return FTC_ImageCache_New0(manager, out acache); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FTC_ImageCache_LookupDelegate0(IntPtr cache, IntPtr type, UInt32 gindex, out IntPtr aglyph, out IntPtr anode);
		private static readonly FTC_ImageCache_LookupDelegate0 FTC_ImageCache_Lookup0 = sExternLibrary.GetStaticProc<FTC_ImageCache_LookupDelegate0>("FTC_ImageCache_Lookup");
		internal static Error FTC_ImageCache_Lookup(IntPtr cache, IntPtr type, UInt32 gindex, out IntPtr aglyph, out IntPtr anode) { return FTC_ImageCache_Lookup0(cache, type, gindex, out aglyph, out anode); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FTC_ImageCache_LookupScalerDelegate0(IntPtr cache, IntPtr scaler, LoadFlags load_flags, UInt32 gindex, out IntPtr aglyph, out IntPtr anode);
		private static readonly FTC_ImageCache_LookupScalerDelegate0 FTC_ImageCache_LookupScaler0 = sExternLibrary.GetStaticProc<FTC_ImageCache_LookupScalerDelegate0>("FTC_ImageCache_LookupScaler");
		internal static Error FTC_ImageCache_LookupScaler(IntPtr cache, IntPtr scaler, LoadFlags load_flags, UInt32 gindex, out IntPtr aglyph, out IntPtr anode) { return FTC_ImageCache_LookupScaler0(cache, scaler, load_flags, gindex, out aglyph, out anode); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FTC_SBitCache_NewDelegate0(IntPtr manager, out IntPtr acache);
		private static readonly FTC_SBitCache_NewDelegate0 FTC_SBitCache_New0 = sExternLibrary.GetStaticProc<FTC_SBitCache_NewDelegate0>("FTC_SBitCache_New");
		internal static Error FTC_SBitCache_New(IntPtr manager, out IntPtr acache) { return FTC_SBitCache_New0(manager, out acache); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FTC_SBitCache_LookupDelegate0(IntPtr cache, IntPtr type, UInt32 gindex, out IntPtr sbit, out IntPtr anode);
		private static readonly FTC_SBitCache_LookupDelegate0 FTC_SBitCache_Lookup0 = sExternLibrary.GetStaticProc<FTC_SBitCache_LookupDelegate0>("FTC_SBitCache_Lookup");
		internal static Error FTC_SBitCache_Lookup(IntPtr cache, IntPtr type, UInt32 gindex, out IntPtr sbit, out IntPtr anode) { return FTC_SBitCache_Lookup0(cache, type, gindex, out sbit, out anode); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FTC_SBitCache_LookupScalerDelegate0(IntPtr cache, IntPtr scaler, LoadFlags load_flags, UInt32 gindex, out IntPtr sbit, out IntPtr anode);
		private static readonly FTC_SBitCache_LookupScalerDelegate0 FTC_SBitCache_LookupScaler0 = sExternLibrary.GetStaticProc<FTC_SBitCache_LookupScalerDelegate0>("FTC_SBitCache_LookupScaler");
		internal static Error FTC_SBitCache_LookupScaler(IntPtr cache, IntPtr scaler, LoadFlags load_flags, UInt32 gindex, out IntPtr sbit, out IntPtr anode) { return FTC_SBitCache_LookupScaler0(cache, scaler, load_flags, gindex, out sbit, out anode); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_OpenType_ValidateDelegate0(IntPtr face, OpenTypeValidationFlags validation_flags, out IntPtr base_table, out IntPtr gdef_table, out IntPtr gpos_table, out IntPtr gsub_table, out IntPtr jsft_table);
		private static readonly FT_OpenType_ValidateDelegate0 FT_OpenType_Validate0 = sExternLibrary.GetStaticProc<FT_OpenType_ValidateDelegate0>("FT_OpenType_Validate");
		internal static Error FT_OpenType_Validate(IntPtr face, OpenTypeValidationFlags validation_flags, out IntPtr base_table, out IntPtr gdef_table, out IntPtr gpos_table, out IntPtr gsub_table, out IntPtr jsft_table) { return FT_OpenType_Validate0(face, validation_flags, out base_table, out gdef_table, out gpos_table, out gsub_table, out jsft_table); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_OpenType_FreeDelegate0(IntPtr face, IntPtr table);
		private static readonly FT_OpenType_FreeDelegate0 FT_OpenType_Free0 = sExternLibrary.GetStaticProc<FT_OpenType_FreeDelegate0>("FT_OpenType_Free");
		internal static void FT_OpenType_Free(IntPtr face, IntPtr table) { FT_OpenType_Free0(face, table); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate EngineType FT_Get_TrueType_Engine_TypeDelegate0(IntPtr library);
		private static readonly FT_Get_TrueType_Engine_TypeDelegate0 FT_Get_TrueType_Engine_Type0 = sExternLibrary.GetStaticProc<FT_Get_TrueType_Engine_TypeDelegate0>("FT_Get_TrueType_Engine_Type");
		internal static EngineType FT_Get_TrueType_Engine_Type(IntPtr library) { return FT_Get_TrueType_Engine_Type0(library); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_TrueTypeGX_ValidateDelegate0(IntPtr face, TrueTypeValidationFlags validation_flags, Byte[][] tables, UInt32 tableLength);
		private static readonly FT_TrueTypeGX_ValidateDelegate0 FT_TrueTypeGX_Validate0 = sExternLibrary.GetStaticProc<FT_TrueTypeGX_ValidateDelegate0>("FT_TrueTypeGX_Validate");
		internal static Error FT_TrueTypeGX_Validate(IntPtr face, TrueTypeValidationFlags validation_flags, Byte[][] tables, UInt32 tableLength) { return FT_TrueTypeGX_Validate0(face, validation_flags, tables, tableLength); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_TrueTypeGX_FreeDelegate0(IntPtr face, IntPtr table);
		private static readonly FT_TrueTypeGX_FreeDelegate0 FT_TrueTypeGX_Free0 = sExternLibrary.GetStaticProc<FT_TrueTypeGX_FreeDelegate0>("FT_TrueTypeGX_Free");
		internal static Error FT_TrueTypeGX_Free(IntPtr face, IntPtr table) { return FT_TrueTypeGX_Free0(face, table); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_ClassicKern_ValidateDelegate0(IntPtr face, ClassicKernValidationFlags validation_flags, out IntPtr ckern_table);
		private static readonly FT_ClassicKern_ValidateDelegate0 FT_ClassicKern_Validate0 = sExternLibrary.GetStaticProc<FT_ClassicKern_ValidateDelegate0>("FT_ClassicKern_Validate");
		internal static Error FT_ClassicKern_Validate(IntPtr face, ClassicKernValidationFlags validation_flags, out IntPtr ckern_table) { return FT_ClassicKern_Validate0(face, validation_flags, out ckern_table); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_ClassicKern_FreeDelegate0(IntPtr face, IntPtr table);
		private static readonly FT_ClassicKern_FreeDelegate0 FT_ClassicKern_Free0 = sExternLibrary.GetStaticProc<FT_ClassicKern_FreeDelegate0>("FT_ClassicKern_Free");
		internal static Error FT_ClassicKern_Free(IntPtr face, IntPtr table) { return FT_ClassicKern_Free0(face, table); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Add_ModuleDelegate0(IntPtr library, IntPtr clazz);
		private static readonly FT_Add_ModuleDelegate0 FT_Add_Module0 = sExternLibrary.GetStaticProc<FT_Add_ModuleDelegate0>("FT_Add_Module");
		internal static Error FT_Add_Module(IntPtr library, IntPtr clazz) { return FT_Add_Module0(library, clazz); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_Get_ModuleDelegate0(IntPtr library, String module_name);
		private static readonly FT_Get_ModuleDelegate0 FT_Get_Module0 = sExternLibrary.GetStaticProc<FT_Get_ModuleDelegate0>("FT_Get_Module");
		internal static IntPtr FT_Get_Module(IntPtr library, String module_name) { return FT_Get_Module0(library, module_name); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Remove_ModuleDelegate0(IntPtr library, IntPtr module);
		private static readonly FT_Remove_ModuleDelegate0 FT_Remove_Module0 = sExternLibrary.GetStaticProc<FT_Remove_ModuleDelegate0>("FT_Remove_Module");
		internal static Error FT_Remove_Module(IntPtr library, IntPtr module) { return FT_Remove_Module0(library, module); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Property_SetDelegate0(IntPtr library, String module_name, String property_name, IntPtr value);
		private static readonly FT_Property_SetDelegate0 FT_Property_Set0 = sExternLibrary.GetStaticProc<FT_Property_SetDelegate0>("FT_Property_Set");
		internal static Error FT_Property_Set(IntPtr library, String module_name, String property_name, IntPtr value) { return FT_Property_Set0(library, module_name, property_name, value); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Property_GetDelegate0(IntPtr library, String module_name, String property_name, IntPtr value);
		private static readonly FT_Property_GetDelegate0 FT_Property_Get0 = sExternLibrary.GetStaticProc<FT_Property_GetDelegate0>("FT_Property_Get");
		internal static Error FT_Property_Get(IntPtr library, String module_name, String property_name, IntPtr value) { return FT_Property_Get0(library, module_name, property_name, value); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Reference_LibraryDelegate0(IntPtr library);
		private static readonly FT_Reference_LibraryDelegate0 FT_Reference_Library0 = sExternLibrary.GetStaticProc<FT_Reference_LibraryDelegate0>("FT_Reference_Library");
		internal static Error FT_Reference_Library(IntPtr library) { return FT_Reference_Library0(library); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_New_LibraryDelegate0(IntPtr memory, out IntPtr alibrary);
		private static readonly FT_New_LibraryDelegate0 FT_New_Library0 = sExternLibrary.GetStaticProc<FT_New_LibraryDelegate0>("FT_New_Library");
		internal static Error FT_New_Library(IntPtr memory, out IntPtr alibrary) { return FT_New_Library0(memory, out alibrary); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Done_LibraryDelegate0(IntPtr library);
		private static readonly FT_Done_LibraryDelegate0 FT_Done_Library0 = sExternLibrary.GetStaticProc<FT_Done_LibraryDelegate0>("FT_Done_Library");
		internal static Error FT_Done_Library(IntPtr library) { return FT_Done_Library0(library); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Set_Debug_HookDelegate0(IntPtr library, UInt32 hook_index, IntPtr debug_hook);
		private static readonly FT_Set_Debug_HookDelegate0 FT_Set_Debug_Hook0 = sExternLibrary.GetStaticProc<FT_Set_Debug_HookDelegate0>("FT_Set_Debug_Hook");
		internal static void FT_Set_Debug_Hook(IntPtr library, UInt32 hook_index, IntPtr debug_hook) { FT_Set_Debug_Hook0(library, hook_index, debug_hook); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Add_Default_ModulesDelegate0(IntPtr library);
		private static readonly FT_Add_Default_ModulesDelegate0 FT_Add_Default_Modules0 = sExternLibrary.GetStaticProc<FT_Add_Default_ModulesDelegate0>("FT_Add_Default_Modules");
		internal static void FT_Add_Default_Modules(IntPtr library) { FT_Add_Default_Modules0(library); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_Get_RendererDelegate0(IntPtr library, GlyphFormat format);
		private static readonly FT_Get_RendererDelegate0 FT_Get_Renderer0 = sExternLibrary.GetStaticProc<FT_Get_RendererDelegate0>("FT_Get_Renderer");
		internal static IntPtr FT_Get_Renderer(IntPtr library, GlyphFormat format) { return FT_Get_Renderer0(library, format); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Set_RendererDelegate0(IntPtr library, IntPtr renderer, UInt32 num_params, IntPtr parameters);
		private static readonly FT_Set_RendererDelegate0 FT_Set_Renderer0 = sExternLibrary.GetStaticProc<FT_Set_RendererDelegate0>("FT_Set_Renderer");
		internal static Error FT_Set_Renderer(IntPtr library, IntPtr renderer, UInt32 num_params, IntPtr parameters) { return FT_Set_Renderer0(library, renderer, num_params, parameters); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Stream_OpenGzipDelegate0(IntPtr stream, IntPtr source);
		private static readonly FT_Stream_OpenGzipDelegate0 FT_Stream_OpenGzip0 = sExternLibrary.GetStaticProc<FT_Stream_OpenGzipDelegate0>("FT_Stream_OpenGzip");
		internal static Error FT_Stream_OpenGzip(IntPtr stream, IntPtr source) { return FT_Stream_OpenGzip0(stream, source); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Gzip_UncompressDelegate0(IntPtr memory, IntPtr output, ref IntPtr output_len, IntPtr input, IntPtr input_len);
		private static readonly FT_Gzip_UncompressDelegate0 FT_Gzip_Uncompress0 = sExternLibrary.GetStaticProc<FT_Gzip_UncompressDelegate0>("FT_Gzip_Uncompress");
		internal static Error FT_Gzip_Uncompress(IntPtr memory, IntPtr output, ref IntPtr output_len, IntPtr input, IntPtr input_len) { return FT_Gzip_Uncompress0(memory, output, ref output_len, input, input_len); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Stream_OpenLZWDelegate0(IntPtr stream, IntPtr source);
		private static readonly FT_Stream_OpenLZWDelegate0 FT_Stream_OpenLZW0 = sExternLibrary.GetStaticProc<FT_Stream_OpenLZWDelegate0>("FT_Stream_OpenLZW");
		internal static Error FT_Stream_OpenLZW(IntPtr stream, IntPtr source) { return FT_Stream_OpenLZW0(stream, source); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Stream_OpenBzip2Delegate0(IntPtr stream, IntPtr source);
		private static readonly FT_Stream_OpenBzip2Delegate0 FT_Stream_OpenBzip20 = sExternLibrary.GetStaticProc<FT_Stream_OpenBzip2Delegate0>("FT_Stream_OpenBzip2");
		internal static Error FT_Stream_OpenBzip2(IntPtr stream, IntPtr source) { return FT_Stream_OpenBzip20(stream, source); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Library_SetLcdFilterDelegate0(IntPtr library, LcdFilter filter);
		private static readonly FT_Library_SetLcdFilterDelegate0 FT_Library_SetLcdFilter0 = sExternLibrary.GetStaticProc<FT_Library_SetLcdFilterDelegate0>("FT_Library_SetLcdFilter");
		internal static Error FT_Library_SetLcdFilter(IntPtr library, LcdFilter filter) { return FT_Library_SetLcdFilter0(library, filter); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Library_SetLcdFilterWeightsDelegate0(IntPtr library, Byte[] weights);
		private static readonly FT_Library_SetLcdFilterWeightsDelegate0 FT_Library_SetLcdFilterWeights0 = sExternLibrary.GetStaticProc<FT_Library_SetLcdFilterWeightsDelegate0>("FT_Library_SetLcdFilterWeights");
		internal static Error FT_Library_SetLcdFilterWeights(IntPtr library, Byte[] weights) { return FT_Library_SetLcdFilterWeights0(library, weights); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FTC_Manager_NewDelegate0(IntPtr library, UInt32 max_faces, UInt32 max_sizes, UInt64 maxBytes, FaceRequester requester, IntPtr req_data, out IntPtr amanager);
		private static readonly FTC_Manager_NewDelegate0 FTC_Manager_New0 = sExternLibrary.GetStaticProc<FTC_Manager_NewDelegate0>("FTC_Manager_New");
		internal static Error FTC_Manager_New(IntPtr library, UInt32 max_faces, UInt32 max_sizes, UInt64 maxBytes, FaceRequester requester, IntPtr req_data, out IntPtr amanager) { return FTC_Manager_New0(library, max_faces, max_sizes, maxBytes, requester, req_data, out amanager); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FTC_Manager_ResetDelegate0(IntPtr manager);
		private static readonly FTC_Manager_ResetDelegate0 FTC_Manager_Reset0 = sExternLibrary.GetStaticProc<FTC_Manager_ResetDelegate0>("FTC_Manager_Reset");
		internal static void FTC_Manager_Reset(IntPtr manager) { FTC_Manager_Reset0(manager); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FTC_Manager_DoneDelegate0(IntPtr manager);
		private static readonly FTC_Manager_DoneDelegate0 FTC_Manager_Done0 = sExternLibrary.GetStaticProc<FTC_Manager_DoneDelegate0>("FTC_Manager_Done");
		internal static void FTC_Manager_Done(IntPtr manager) { FTC_Manager_Done0(manager); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FTC_Manager_LookupFaceDelegate0(IntPtr manager, IntPtr face_id, out IntPtr aface);
		private static readonly FTC_Manager_LookupFaceDelegate0 FTC_Manager_LookupFace0 = sExternLibrary.GetStaticProc<FTC_Manager_LookupFaceDelegate0>("FTC_Manager_LookupFace");
		internal static Error FTC_Manager_LookupFace(IntPtr manager, IntPtr face_id, out IntPtr aface) { return FTC_Manager_LookupFace0(manager, face_id, out aface); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FTC_Manager_LookupSizeDelegate0(IntPtr manager, IntPtr scaler, out IntPtr asize);
		private static readonly FTC_Manager_LookupSizeDelegate0 FTC_Manager_LookupSize0 = sExternLibrary.GetStaticProc<FTC_Manager_LookupSizeDelegate0>("FTC_Manager_LookupSize");
		internal static Error FTC_Manager_LookupSize(IntPtr manager, IntPtr scaler, out IntPtr asize) { return FTC_Manager_LookupSize0(manager, scaler, out asize); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FTC_Node_UnrefDelegate0(IntPtr node, IntPtr manager);
		private static readonly FTC_Node_UnrefDelegate0 FTC_Node_Unref0 = sExternLibrary.GetStaticProc<FTC_Node_UnrefDelegate0>("FTC_Node_Unref");
		internal static void FTC_Node_Unref(IntPtr node, IntPtr manager) { FTC_Node_Unref0(node, manager); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FTC_Manager_RemoveFaceIDDelegate0(IntPtr manager, IntPtr face_id);
		private static readonly FTC_Manager_RemoveFaceIDDelegate0 FTC_Manager_RemoveFaceID0 = sExternLibrary.GetStaticProc<FTC_Manager_RemoveFaceIDDelegate0>("FTC_Manager_RemoveFaceID");
		internal static void FTC_Manager_RemoveFaceID(IntPtr manager, IntPtr face_id) { FTC_Manager_RemoveFaceID0(manager, face_id); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_AdvancesDelegate0(IntPtr face, UInt32 start, UInt32 count, LoadFlags load_flags, out IntPtr padvance);
		private static readonly FT_Get_AdvancesDelegate0 FT_Get_Advances0 = sExternLibrary.GetStaticProc<FT_Get_AdvancesDelegate0>("FT_Get_Advances");
		internal static Error FT_Get_Advances(IntPtr face, UInt32 start, UInt32 count, LoadFlags load_flags, out IntPtr padvance) { return FT_Get_Advances0(face, start, count, load_flags, out padvance); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Bitmap_NewDelegate0(IntPtr abitmap);
		private static readonly FT_Bitmap_NewDelegate0 FT_Bitmap_New0 = sExternLibrary.GetStaticProc<FT_Bitmap_NewDelegate0>("FT_Bitmap_New");
		internal static void FT_Bitmap_New(IntPtr abitmap) { FT_Bitmap_New0(abitmap); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Bitmap_CopyDelegate0(IntPtr library, IntPtr source, IntPtr target);
		private static readonly FT_Bitmap_CopyDelegate0 FT_Bitmap_Copy0 = sExternLibrary.GetStaticProc<FT_Bitmap_CopyDelegate0>("FT_Bitmap_Copy");
		internal static Error FT_Bitmap_Copy(IntPtr library, IntPtr source, IntPtr target) { return FT_Bitmap_Copy0(library, source, target); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Bitmap_EmboldenDelegate0(IntPtr library, IntPtr bitmap, IntPtr xStrength, IntPtr yStrength);
		private static readonly FT_Bitmap_EmboldenDelegate0 FT_Bitmap_Embolden0 = sExternLibrary.GetStaticProc<FT_Bitmap_EmboldenDelegate0>("FT_Bitmap_Embolden");
		internal static Error FT_Bitmap_Embolden(IntPtr library, IntPtr bitmap, IntPtr xStrength, IntPtr yStrength) { return FT_Bitmap_Embolden0(library, bitmap, xStrength, yStrength); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Bitmap_ConvertDelegate0(IntPtr library, IntPtr source, IntPtr target, Int32 alignment);
		private static readonly FT_Bitmap_ConvertDelegate0 FT_Bitmap_Convert0 = sExternLibrary.GetStaticProc<FT_Bitmap_ConvertDelegate0>("FT_Bitmap_Convert");
		internal static Error FT_Bitmap_Convert(IntPtr library, IntPtr source, IntPtr target, Int32 alignment) { return FT_Bitmap_Convert0(library, source, target, alignment); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_GlyphSlot_Own_BitmapDelegate0(IntPtr slot);
		private static readonly FT_GlyphSlot_Own_BitmapDelegate0 FT_GlyphSlot_Own_Bitmap0 = sExternLibrary.GetStaticProc<FT_GlyphSlot_Own_BitmapDelegate0>("FT_GlyphSlot_Own_Bitmap");
		internal static Error FT_GlyphSlot_Own_Bitmap(IntPtr slot) { return FT_GlyphSlot_Own_Bitmap0(slot); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Bitmap_DoneDelegate0(IntPtr library, IntPtr bitmap);
		private static readonly FT_Bitmap_DoneDelegate0 FT_Bitmap_Done0 = sExternLibrary.GetStaticProc<FT_Bitmap_DoneDelegate0>("FT_Bitmap_Done");
		internal static Error FT_Bitmap_Done(IntPtr library, IntPtr bitmap) { return FT_Bitmap_Done0(library, bitmap); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate StrokerBorder FT_Outline_GetInsideBorderDelegate0(IntPtr outline);
		private static readonly FT_Outline_GetInsideBorderDelegate0 FT_Outline_GetInsideBorder0 = sExternLibrary.GetStaticProc<FT_Outline_GetInsideBorderDelegate0>("FT_Outline_GetInsideBorder");
		internal static StrokerBorder FT_Outline_GetInsideBorder(IntPtr outline) { return FT_Outline_GetInsideBorder0(outline); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate StrokerBorder FT_Outline_GetOutsideBorderDelegate0(IntPtr outline);
		private static readonly FT_Outline_GetOutsideBorderDelegate0 FT_Outline_GetOutsideBorder0 = sExternLibrary.GetStaticProc<FT_Outline_GetOutsideBorderDelegate0>("FT_Outline_GetOutsideBorder");
		internal static StrokerBorder FT_Outline_GetOutsideBorder(IntPtr outline) { return FT_Outline_GetOutsideBorder0(outline); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Stroker_NewDelegate0(IntPtr library, out IntPtr astroker);
		private static readonly FT_Stroker_NewDelegate0 FT_Stroker_New0 = sExternLibrary.GetStaticProc<FT_Stroker_NewDelegate0>("FT_Stroker_New");
		internal static Error FT_Stroker_New(IntPtr library, out IntPtr astroker) { return FT_Stroker_New0(library, out astroker); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Stroker_SetDelegate0(IntPtr stroker, Int32 radius, StrokerLineCap line_cap, StrokerLineJoin line_join, IntPtr miter_limit);
		private static readonly FT_Stroker_SetDelegate0 FT_Stroker_Set0 = sExternLibrary.GetStaticProc<FT_Stroker_SetDelegate0>("FT_Stroker_Set");
		internal static void FT_Stroker_Set(IntPtr stroker, Int32 radius, StrokerLineCap line_cap, StrokerLineJoin line_join, IntPtr miter_limit) { FT_Stroker_Set0(stroker, radius, line_cap, line_join, miter_limit); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Stroker_RewindDelegate0(IntPtr stroker);
		private static readonly FT_Stroker_RewindDelegate0 FT_Stroker_Rewind0 = sExternLibrary.GetStaticProc<FT_Stroker_RewindDelegate0>("FT_Stroker_Rewind");
		internal static void FT_Stroker_Rewind(IntPtr stroker) { FT_Stroker_Rewind0(stroker); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Stroker_ParseOutlineDelegate0(IntPtr stroker, IntPtr outline, Boolean opened);
		private static readonly FT_Stroker_ParseOutlineDelegate0 FT_Stroker_ParseOutline0 = sExternLibrary.GetStaticProc<FT_Stroker_ParseOutlineDelegate0>("FT_Stroker_ParseOutline");
		internal static Error FT_Stroker_ParseOutline(IntPtr stroker, IntPtr outline, Boolean opened) { return FT_Stroker_ParseOutline0(stroker, outline, opened); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Stroker_BeginSubPathDelegate0(IntPtr stroker, ref FTVector to, Boolean open);
		private static readonly FT_Stroker_BeginSubPathDelegate0 FT_Stroker_BeginSubPath0 = sExternLibrary.GetStaticProc<FT_Stroker_BeginSubPathDelegate0>("FT_Stroker_BeginSubPath");
		internal static Error FT_Stroker_BeginSubPath(IntPtr stroker, ref FTVector to, Boolean open) { return FT_Stroker_BeginSubPath0(stroker, ref to, open); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Stroker_EndSubPathDelegate0(IntPtr stroker);
		private static readonly FT_Stroker_EndSubPathDelegate0 FT_Stroker_EndSubPath0 = sExternLibrary.GetStaticProc<FT_Stroker_EndSubPathDelegate0>("FT_Stroker_EndSubPath");
		internal static Error FT_Stroker_EndSubPath(IntPtr stroker) { return FT_Stroker_EndSubPath0(stroker); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Stroker_LineToDelegate0(IntPtr stroker, ref FTVector to);
		private static readonly FT_Stroker_LineToDelegate0 FT_Stroker_LineTo0 = sExternLibrary.GetStaticProc<FT_Stroker_LineToDelegate0>("FT_Stroker_LineTo");
		internal static Error FT_Stroker_LineTo(IntPtr stroker, ref FTVector to) { return FT_Stroker_LineTo0(stroker, ref to); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Stroker_ConicToDelegate0(IntPtr stroker, ref FTVector control, ref FTVector to);
		private static readonly FT_Stroker_ConicToDelegate0 FT_Stroker_ConicTo0 = sExternLibrary.GetStaticProc<FT_Stroker_ConicToDelegate0>("FT_Stroker_ConicTo");
		internal static Error FT_Stroker_ConicTo(IntPtr stroker, ref FTVector control, ref FTVector to) { return FT_Stroker_ConicTo0(stroker, ref control, ref to); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Stroker_CubicToDelegate0(IntPtr stroker, ref FTVector control1, ref FTVector control2, ref FTVector to);
		private static readonly FT_Stroker_CubicToDelegate0 FT_Stroker_CubicTo0 = sExternLibrary.GetStaticProc<FT_Stroker_CubicToDelegate0>("FT_Stroker_CubicTo");
		internal static Error FT_Stroker_CubicTo(IntPtr stroker, ref FTVector control1, ref FTVector control2, ref FTVector to) { return FT_Stroker_CubicTo0(stroker, ref control1, ref control2, ref to); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Stroker_GetBorderCountsDelegate0(IntPtr stroker, StrokerBorder border, out UInt32 anum_points, out UInt32 anum_contours);
		private static readonly FT_Stroker_GetBorderCountsDelegate0 FT_Stroker_GetBorderCounts0 = sExternLibrary.GetStaticProc<FT_Stroker_GetBorderCountsDelegate0>("FT_Stroker_GetBorderCounts");
		internal static Error FT_Stroker_GetBorderCounts(IntPtr stroker, StrokerBorder border, out UInt32 anum_points, out UInt32 anum_contours) { return FT_Stroker_GetBorderCounts0(stroker, border, out anum_points, out anum_contours); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Stroker_ExportBorderDelegate0(IntPtr stroker, StrokerBorder border, IntPtr outline);
		private static readonly FT_Stroker_ExportBorderDelegate0 FT_Stroker_ExportBorder0 = sExternLibrary.GetStaticProc<FT_Stroker_ExportBorderDelegate0>("FT_Stroker_ExportBorder");
		internal static void FT_Stroker_ExportBorder(IntPtr stroker, StrokerBorder border, IntPtr outline) { FT_Stroker_ExportBorder0(stroker, border, outline); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Stroker_GetCountsDelegate0(IntPtr stroker, out UInt32 anum_points, out UInt32 anum_contours);
		private static readonly FT_Stroker_GetCountsDelegate0 FT_Stroker_GetCounts0 = sExternLibrary.GetStaticProc<FT_Stroker_GetCountsDelegate0>("FT_Stroker_GetCounts");
		internal static Error FT_Stroker_GetCounts(IntPtr stroker, out UInt32 anum_points, out UInt32 anum_contours) { return FT_Stroker_GetCounts0(stroker, out anum_points, out anum_contours); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Stroker_ExportDelegate0(IntPtr stroker, IntPtr outline);
		private static readonly FT_Stroker_ExportDelegate0 FT_Stroker_Export0 = sExternLibrary.GetStaticProc<FT_Stroker_ExportDelegate0>("FT_Stroker_Export");
		internal static void FT_Stroker_Export(IntPtr stroker, IntPtr outline) { FT_Stroker_Export0(stroker, outline); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Stroker_DoneDelegate0(IntPtr stroker);
		private static readonly FT_Stroker_DoneDelegate0 FT_Stroker_Done0 = sExternLibrary.GetStaticProc<FT_Stroker_DoneDelegate0>("FT_Stroker_Done");
		internal static void FT_Stroker_Done(IntPtr stroker) { FT_Stroker_Done0(stroker); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Glyph_StrokeDelegate0(ref IntPtr pglyph, IntPtr stoker, Boolean destroy);
		private static readonly FT_Glyph_StrokeDelegate0 FT_Glyph_Stroke0 = sExternLibrary.GetStaticProc<FT_Glyph_StrokeDelegate0>("FT_Glyph_Stroke");
		internal static Error FT_Glyph_Stroke(ref IntPtr pglyph, IntPtr stoker, Boolean destroy) { return FT_Glyph_Stroke0(ref pglyph, stoker, destroy); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Glyph_StrokeBorderDelegate0(ref IntPtr pglyph, IntPtr stoker, Boolean inside, Boolean destroy);
		private static readonly FT_Glyph_StrokeBorderDelegate0 FT_Glyph_StrokeBorder0 = sExternLibrary.GetStaticProc<FT_Glyph_StrokeBorderDelegate0>("FT_Glyph_StrokeBorder");
		internal static Error FT_Glyph_StrokeBorder(ref IntPtr pglyph, IntPtr stoker, Boolean inside, Boolean destroy) { return FT_Glyph_StrokeBorder0(ref pglyph, stoker, inside, destroy); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_List_FindDelegate0(IntPtr list, IntPtr data);
		private static readonly FT_List_FindDelegate0 FT_List_Find0 = sExternLibrary.GetStaticProc<FT_List_FindDelegate0>("FT_List_Find");
		internal static IntPtr FT_List_Find(IntPtr list, IntPtr data) { return FT_List_Find0(list, data); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_List_AddDelegate0(IntPtr list, IntPtr node);
		private static readonly FT_List_AddDelegate0 FT_List_Add0 = sExternLibrary.GetStaticProc<FT_List_AddDelegate0>("FT_List_Add");
		internal static void FT_List_Add(IntPtr list, IntPtr node) { FT_List_Add0(list, node); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_List_InsertDelegate0(IntPtr list, IntPtr node);
		private static readonly FT_List_InsertDelegate0 FT_List_Insert0 = sExternLibrary.GetStaticProc<FT_List_InsertDelegate0>("FT_List_Insert");
		internal static void FT_List_Insert(IntPtr list, IntPtr node) { FT_List_Insert0(list, node); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_List_RemoveDelegate0(IntPtr list, IntPtr node);
		private static readonly FT_List_RemoveDelegate0 FT_List_Remove0 = sExternLibrary.GetStaticProc<FT_List_RemoveDelegate0>("FT_List_Remove");
		internal static void FT_List_Remove(IntPtr list, IntPtr node) { FT_List_Remove0(list, node); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_List_UpDelegate0(IntPtr list, IntPtr node);
		private static readonly FT_List_UpDelegate0 FT_List_Up0 = sExternLibrary.GetStaticProc<FT_List_UpDelegate0>("FT_List_Up");
		internal static void FT_List_Up(IntPtr list, IntPtr node) { FT_List_Up0(list, node); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_List_IterateDelegate0(IntPtr list, ListIterator iterator, IntPtr user);
		private static readonly FT_List_IterateDelegate0 FT_List_Iterate0 = sExternLibrary.GetStaticProc<FT_List_IterateDelegate0>("FT_List_Iterate");
		internal static Error FT_List_Iterate(IntPtr list, ListIterator iterator, IntPtr user) { return FT_List_Iterate0(list, iterator, user); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_List_FinalizeDelegate0(IntPtr list, ListDestructor destroy, IntPtr memory, IntPtr user);
		private static readonly FT_List_FinalizeDelegate0 FT_List_Finalize0 = sExternLibrary.GetStaticProc<FT_List_FinalizeDelegate0>("FT_List_Finalize");
		internal static void FT_List_Finalize(IntPtr list, ListDestructor destroy, IntPtr memory, IntPtr user) { FT_List_Finalize0(list, destroy, memory, user); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Outline_NewDelegate0(IntPtr library, UInt32 numPoints, Int32 numContours, out IntPtr anoutline);
		private static readonly FT_Outline_NewDelegate0 FT_Outline_New0 = sExternLibrary.GetStaticProc<FT_Outline_NewDelegate0>("FT_Outline_New");
		internal static Error FT_Outline_New(IntPtr library, UInt32 numPoints, Int32 numContours, out IntPtr anoutline) { return FT_Outline_New0(library, numPoints, numContours, out anoutline); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Outline_New_InternalDelegate0(IntPtr memory, UInt32 numPoints, Int32 numContours, out IntPtr anoutline);
		private static readonly FT_Outline_New_InternalDelegate0 FT_Outline_New_Internal0 = sExternLibrary.GetStaticProc<FT_Outline_New_InternalDelegate0>("FT_Outline_New_Internal");
		internal static Error FT_Outline_New_Internal(IntPtr memory, UInt32 numPoints, Int32 numContours, out IntPtr anoutline) { return FT_Outline_New_Internal0(memory, numPoints, numContours, out anoutline); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Outline_DoneDelegate0(IntPtr library, IntPtr outline);
		private static readonly FT_Outline_DoneDelegate0 FT_Outline_Done0 = sExternLibrary.GetStaticProc<FT_Outline_DoneDelegate0>("FT_Outline_Done");
		internal static Error FT_Outline_Done(IntPtr library, IntPtr outline) { return FT_Outline_Done0(library, outline); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Outline_Done_InternalDelegate0(IntPtr memory, IntPtr outline);
		private static readonly FT_Outline_Done_InternalDelegate0 FT_Outline_Done_Internal0 = sExternLibrary.GetStaticProc<FT_Outline_Done_InternalDelegate0>("FT_Outline_Done_Internal");
		internal static Error FT_Outline_Done_Internal(IntPtr memory, IntPtr outline) { return FT_Outline_Done_Internal0(memory, outline); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Outline_CopyDelegate0(IntPtr source, ref IntPtr target);
		private static readonly FT_Outline_CopyDelegate0 FT_Outline_Copy0 = sExternLibrary.GetStaticProc<FT_Outline_CopyDelegate0>("FT_Outline_Copy");
		internal static Error FT_Outline_Copy(IntPtr source, ref IntPtr target) { return FT_Outline_Copy0(source, ref target); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Outline_TranslateDelegate0(IntPtr outline, Int32 xOffset, Int32 yOffset);
		private static readonly FT_Outline_TranslateDelegate0 FT_Outline_Translate0 = sExternLibrary.GetStaticProc<FT_Outline_TranslateDelegate0>("FT_Outline_Translate");
		internal static void FT_Outline_Translate(IntPtr outline, Int32 xOffset, Int32 yOffset) { FT_Outline_Translate0(outline, xOffset, yOffset); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Outline_TransformDelegate0(IntPtr outline, ref FTMatrix matrix);
		private static readonly FT_Outline_TransformDelegate0 FT_Outline_Transform0 = sExternLibrary.GetStaticProc<FT_Outline_TransformDelegate0>("FT_Outline_Transform");
		internal static void FT_Outline_Transform(IntPtr outline, ref FTMatrix matrix) { FT_Outline_Transform0(outline, ref matrix); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Outline_EmboldenDelegate0(IntPtr outline, IntPtr strength);
		private static readonly FT_Outline_EmboldenDelegate0 FT_Outline_Embolden0 = sExternLibrary.GetStaticProc<FT_Outline_EmboldenDelegate0>("FT_Outline_Embolden");
		internal static Error FT_Outline_Embolden(IntPtr outline, IntPtr strength) { return FT_Outline_Embolden0(outline, strength); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Outline_EmboldenXYDelegate0(IntPtr outline, Int32 xstrength, Int32 ystrength);
		private static readonly FT_Outline_EmboldenXYDelegate0 FT_Outline_EmboldenXY0 = sExternLibrary.GetStaticProc<FT_Outline_EmboldenXYDelegate0>("FT_Outline_EmboldenXY");
		internal static Error FT_Outline_EmboldenXY(IntPtr outline, Int32 xstrength, Int32 ystrength) { return FT_Outline_EmboldenXY0(outline, xstrength, ystrength); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Outline_ReverseDelegate0(IntPtr outline);
		private static readonly FT_Outline_ReverseDelegate0 FT_Outline_Reverse0 = sExternLibrary.GetStaticProc<FT_Outline_ReverseDelegate0>("FT_Outline_Reverse");
		internal static void FT_Outline_Reverse(IntPtr outline) { FT_Outline_Reverse0(outline); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Outline_CheckDelegate0(IntPtr outline);
		private static readonly FT_Outline_CheckDelegate0 FT_Outline_Check0 = sExternLibrary.GetStaticProc<FT_Outline_CheckDelegate0>("FT_Outline_Check");
		internal static Error FT_Outline_Check(IntPtr outline) { return FT_Outline_Check0(outline); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Outline_Get_BBoxDelegate0(IntPtr outline, out BBox abbox);
		private static readonly FT_Outline_Get_BBoxDelegate0 FT_Outline_Get_BBox0 = sExternLibrary.GetStaticProc<FT_Outline_Get_BBoxDelegate0>("FT_Outline_Get_BBox");
		internal static Error FT_Outline_Get_BBox(IntPtr outline, out BBox abbox) { return FT_Outline_Get_BBox0(outline, out abbox); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Outline_DecomposeDelegate0(IntPtr outline, ref OutlineFuncsRec func_interface, IntPtr user);
		private static readonly FT_Outline_DecomposeDelegate0 FT_Outline_Decompose0 = sExternLibrary.GetStaticProc<FT_Outline_DecomposeDelegate0>("FT_Outline_Decompose");
		internal static Error FT_Outline_Decompose(IntPtr outline, ref OutlineFuncsRec func_interface, IntPtr user) { return FT_Outline_Decompose0(outline, ref func_interface, user); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Outline_Get_CBoxDelegate0(IntPtr outline, out BBox acbox);
		private static readonly FT_Outline_Get_CBoxDelegate0 FT_Outline_Get_CBox0 = sExternLibrary.GetStaticProc<FT_Outline_Get_CBoxDelegate0>("FT_Outline_Get_CBox");
		internal static void FT_Outline_Get_CBox(IntPtr outline, out BBox acbox) { FT_Outline_Get_CBox0(outline, out acbox); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Outline_Get_BitmapDelegate0(IntPtr library, IntPtr outline, IntPtr abitmap);
		private static readonly FT_Outline_Get_BitmapDelegate0 FT_Outline_Get_Bitmap0 = sExternLibrary.GetStaticProc<FT_Outline_Get_BitmapDelegate0>("FT_Outline_Get_Bitmap");
		internal static Error FT_Outline_Get_Bitmap(IntPtr library, IntPtr outline, IntPtr abitmap) { return FT_Outline_Get_Bitmap0(library, outline, abitmap); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Outline_RenderDelegate0(IntPtr library, IntPtr outline, IntPtr @params);
		private static readonly FT_Outline_RenderDelegate0 FT_Outline_Render0 = sExternLibrary.GetStaticProc<FT_Outline_RenderDelegate0>("FT_Outline_Render");
		internal static Error FT_Outline_Render(IntPtr library, IntPtr outline, IntPtr @params) { return FT_Outline_Render0(library, outline, @params); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Orientation FT_Outline_Get_OrientationDelegate0(IntPtr outline);
		private static readonly FT_Outline_Get_OrientationDelegate0 FT_Outline_Get_Orientation0 = sExternLibrary.GetStaticProc<FT_Outline_Get_OrientationDelegate0>("FT_Outline_Get_Orientation");
		internal static Orientation FT_Outline_Get_Orientation(IntPtr outline) { return FT_Outline_Get_Orientation0(outline); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_AdvanceDelegate0(IntPtr face, UInt32 gIndex, LoadFlags load_flags, out IntPtr padvance);
		private static readonly FT_Get_AdvanceDelegate0 FT_Get_Advance0 = sExternLibrary.GetStaticProc<FT_Get_AdvanceDelegate0>("FT_Get_Advance");
		internal static Error FT_Get_Advance(IntPtr face, UInt32 gIndex, LoadFlags load_flags, out IntPtr padvance) { return FT_Get_Advance0(face, gIndex, load_flags, out padvance); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_PFR_MetricsDelegate0(IntPtr face, out UInt32 aoutline_resolution, out UInt32 ametrics_resolution, out IntPtr ametrics_x_scale, out IntPtr ametrics_y_scale);
		private static readonly FT_Get_PFR_MetricsDelegate0 FT_Get_PFR_Metrics0 = sExternLibrary.GetStaticProc<FT_Get_PFR_MetricsDelegate0>("FT_Get_PFR_Metrics");
		internal static Error FT_Get_PFR_Metrics(IntPtr face, out UInt32 aoutline_resolution, out UInt32 ametrics_resolution, out IntPtr ametrics_x_scale, out IntPtr ametrics_y_scale) { return FT_Get_PFR_Metrics0(face, out aoutline_resolution, out ametrics_resolution, out ametrics_x_scale, out ametrics_y_scale); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_PFR_KerningDelegate0(IntPtr face, UInt32 left, UInt32 right, out FTVector avector);
		private static readonly FT_Get_PFR_KerningDelegate0 FT_Get_PFR_Kerning0 = sExternLibrary.GetStaticProc<FT_Get_PFR_KerningDelegate0>("FT_Get_PFR_Kerning");
		internal static Error FT_Get_PFR_Kerning(IntPtr face, UInt32 left, UInt32 right, out FTVector avector) { return FT_Get_PFR_Kerning0(face, left, right, out avector); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_PFR_AdvanceDelegate0(IntPtr face, UInt32 gindex, out Int32 aadvance);
		private static readonly FT_Get_PFR_AdvanceDelegate0 FT_Get_PFR_Advance0 = sExternLibrary.GetStaticProc<FT_Get_PFR_AdvanceDelegate0>("FT_Get_PFR_Advance");
		internal static Error FT_Get_PFR_Advance(IntPtr face, UInt32 gindex, out Int32 aadvance) { return FT_Get_PFR_Advance0(face, gindex, out aadvance); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_WinFNT_HeaderDelegate0(IntPtr face, out IntPtr aheader);
		private static readonly FT_Get_WinFNT_HeaderDelegate0 FT_Get_WinFNT_Header0 = sExternLibrary.GetStaticProc<FT_Get_WinFNT_HeaderDelegate0>("FT_Get_WinFNT_Header");
		internal static Error FT_Get_WinFNT_Header(IntPtr face, out IntPtr aheader) { return FT_Get_WinFNT_Header0(face, out aheader); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_Get_X11_Font_FormatDelegate0(IntPtr face);
		private static readonly FT_Get_X11_Font_FormatDelegate0 FT_Get_X11_Font_Format0 = sExternLibrary.GetStaticProc<FT_Get_X11_Font_FormatDelegate0>("FT_Get_X11_Font_Format");
		internal static IntPtr FT_Get_X11_Font_Format(IntPtr face) { return FT_Get_X11_Font_Format0(face); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Gasp FT_Get_GaspDelegate0(IntPtr face, UInt32 ppem);
		private static readonly FT_Get_GaspDelegate0 FT_Get_Gasp0 = sExternLibrary.GetStaticProc<FT_Get_GaspDelegate0>("FT_Get_Gasp");
		internal static Gasp FT_Get_Gasp(IntPtr face, UInt32 ppem) { return FT_Get_Gasp0(face, ppem); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_MulDivDelegate0(IntPtr a, IntPtr b, IntPtr c);
		private static readonly FT_MulDivDelegate0 FT_MulDiv0 = sExternLibrary.GetStaticProc<FT_MulDivDelegate0>("FT_MulDiv");
		internal static IntPtr FT_MulDiv(IntPtr a, IntPtr b, IntPtr c) { return FT_MulDiv0(a, b, c); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_MulFixDelegate0(IntPtr a, IntPtr b);
		private static readonly FT_MulFixDelegate0 FT_MulFix0 = sExternLibrary.GetStaticProc<FT_MulFixDelegate0>("FT_MulFix");
		internal static IntPtr FT_MulFix(IntPtr a, IntPtr b) { return FT_MulFix0(a, b); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_DivFixDelegate0(IntPtr a, IntPtr b);
		private static readonly FT_DivFixDelegate0 FT_DivFix0 = sExternLibrary.GetStaticProc<FT_DivFixDelegate0>("FT_DivFix");
		internal static IntPtr FT_DivFix(IntPtr a, IntPtr b) { return FT_DivFix0(a, b); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_RoundFixDelegate0(IntPtr a);
		private static readonly FT_RoundFixDelegate0 FT_RoundFix0 = sExternLibrary.GetStaticProc<FT_RoundFixDelegate0>("FT_RoundFix");
		internal static IntPtr FT_RoundFix(IntPtr a) { return FT_RoundFix0(a); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_CeilFixDelegate0(IntPtr a);
		private static readonly FT_CeilFixDelegate0 FT_CeilFix0 = sExternLibrary.GetStaticProc<FT_CeilFixDelegate0>("FT_CeilFix");
		internal static IntPtr FT_CeilFix(IntPtr a) { return FT_CeilFix0(a); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_FloorFixDelegate0(IntPtr a);
		private static readonly FT_FloorFixDelegate0 FT_FloorFix0 = sExternLibrary.GetStaticProc<FT_FloorFixDelegate0>("FT_FloorFix");
		internal static IntPtr FT_FloorFix(IntPtr a) { return FT_FloorFix0(a); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Vector_TransformDelegate0(ref FTVector vec, ref FTMatrix matrix);
		private static readonly FT_Vector_TransformDelegate0 FT_Vector_Transform0 = sExternLibrary.GetStaticProc<FT_Vector_TransformDelegate0>("FT_Vector_Transform");
		internal static void FT_Vector_Transform(ref FTVector vec, ref FTMatrix matrix) { FT_Vector_Transform0(ref vec, ref matrix); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Matrix_MultiplyDelegate0(ref FTMatrix a, ref FTMatrix b);
		private static readonly FT_Matrix_MultiplyDelegate0 FT_Matrix_Multiply0 = sExternLibrary.GetStaticProc<FT_Matrix_MultiplyDelegate0>("FT_Matrix_Multiply");
		internal static void FT_Matrix_Multiply(ref FTMatrix a, ref FTMatrix b) { FT_Matrix_Multiply0(ref a, ref b); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Matrix_InvertDelegate0(ref FTMatrix matrix);
		private static readonly FT_Matrix_InvertDelegate0 FT_Matrix_Invert0 = sExternLibrary.GetStaticProc<FT_Matrix_InvertDelegate0>("FT_Matrix_Invert");
		internal static Error FT_Matrix_Invert(ref FTMatrix matrix) { return FT_Matrix_Invert0(ref matrix); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_SinDelegate0(IntPtr angle);
		private static readonly FT_SinDelegate0 FT_Sin0 = sExternLibrary.GetStaticProc<FT_SinDelegate0>("FT_Sin");
		internal static IntPtr FT_Sin(IntPtr angle) { return FT_Sin0(angle); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_CosDelegate0(IntPtr angle);
		private static readonly FT_CosDelegate0 FT_Cos0 = sExternLibrary.GetStaticProc<FT_CosDelegate0>("FT_Cos");
		internal static IntPtr FT_Cos(IntPtr angle) { return FT_Cos0(angle); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_TanDelegate0(IntPtr angle);
		private static readonly FT_TanDelegate0 FT_Tan0 = sExternLibrary.GetStaticProc<FT_TanDelegate0>("FT_Tan");
		internal static IntPtr FT_Tan(IntPtr angle) { return FT_Tan0(angle); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_Atan2Delegate0(IntPtr x, IntPtr y);
		private static readonly FT_Atan2Delegate0 FT_Atan20 = sExternLibrary.GetStaticProc<FT_Atan2Delegate0>("FT_Atan2");
		internal static IntPtr FT_Atan2(IntPtr x, IntPtr y) { return FT_Atan20(x, y); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_Angle_DiffDelegate0(IntPtr angle1, IntPtr angle2);
		private static readonly FT_Angle_DiffDelegate0 FT_Angle_Diff0 = sExternLibrary.GetStaticProc<FT_Angle_DiffDelegate0>("FT_Angle_Diff");
		internal static IntPtr FT_Angle_Diff(IntPtr angle1, IntPtr angle2) { return FT_Angle_Diff0(angle1, angle2); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Vector_UnitDelegate0(out FTVector vec, IntPtr angle);
		private static readonly FT_Vector_UnitDelegate0 FT_Vector_Unit0 = sExternLibrary.GetStaticProc<FT_Vector_UnitDelegate0>("FT_Vector_Unit");
		internal static void FT_Vector_Unit(out FTVector vec, IntPtr angle) { FT_Vector_Unit0(out vec, angle); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Vector_RotateDelegate0(ref FTVector vec, IntPtr angle);
		private static readonly FT_Vector_RotateDelegate0 FT_Vector_Rotate0 = sExternLibrary.GetStaticProc<FT_Vector_RotateDelegate0>("FT_Vector_Rotate");
		internal static void FT_Vector_Rotate(ref FTVector vec, IntPtr angle) { FT_Vector_Rotate0(ref vec, angle); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_Vector_LengthDelegate0(ref FTVector vec);
		private static readonly FT_Vector_LengthDelegate0 FT_Vector_Length0 = sExternLibrary.GetStaticProc<FT_Vector_LengthDelegate0>("FT_Vector_Length");
		internal static IntPtr FT_Vector_Length(ref FTVector vec) { return FT_Vector_Length0(ref vec); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Vector_PolarizeDelegate0(ref FTVector vec, out IntPtr length, out IntPtr angle);
		private static readonly FT_Vector_PolarizeDelegate0 FT_Vector_Polarize0 = sExternLibrary.GetStaticProc<FT_Vector_PolarizeDelegate0>("FT_Vector_Polarize");
		internal static void FT_Vector_Polarize(ref FTVector vec, out IntPtr length, out IntPtr angle) { FT_Vector_Polarize0(ref vec, out length, out angle); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Vector_From_PolarDelegate0(out FTVector vec, IntPtr length, IntPtr angle);
		private static readonly FT_Vector_From_PolarDelegate0 FT_Vector_From_Polar0 = sExternLibrary.GetStaticProc<FT_Vector_From_PolarDelegate0>("FT_Vector_From_Polar");
		internal static void FT_Vector_From_Polar(out FTVector vec, IntPtr length, IntPtr angle) { FT_Vector_From_Polar0(out vec, length, angle); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_New_SizeDelegate0(IntPtr face, out IntPtr size);
		private static readonly FT_New_SizeDelegate0 FT_New_Size0 = sExternLibrary.GetStaticProc<FT_New_SizeDelegate0>("FT_New_Size");
		internal static Error FT_New_Size(IntPtr face, out IntPtr size) { return FT_New_Size0(face, out size); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Done_SizeDelegate0(IntPtr size);
		private static readonly FT_Done_SizeDelegate0 FT_Done_Size0 = sExternLibrary.GetStaticProc<FT_Done_SizeDelegate0>("FT_Done_Size");
		internal static Error FT_Done_Size(IntPtr size) { return FT_Done_Size0(size); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Activate_SizeDelegate0(IntPtr size);
		private static readonly FT_Activate_SizeDelegate0 FT_Activate_Size0 = sExternLibrary.GetStaticProc<FT_Activate_SizeDelegate0>("FT_Activate_Size");
		internal static Error FT_Activate_Size(IntPtr size) { return FT_Activate_Size0(size); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_Multi_MasterDelegate0(IntPtr face, out IntPtr amaster);
		private static readonly FT_Get_Multi_MasterDelegate0 FT_Get_Multi_Master0 = sExternLibrary.GetStaticProc<FT_Get_Multi_MasterDelegate0>("FT_Get_Multi_Master");
		internal static Error FT_Get_Multi_Master(IntPtr face, out IntPtr amaster) { return FT_Get_Multi_Master0(face, out amaster); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_MM_VarDelegate0(IntPtr face, out IntPtr amaster);
		private static readonly FT_Get_MM_VarDelegate0 FT_Get_MM_Var0 = sExternLibrary.GetStaticProc<FT_Get_MM_VarDelegate0>("FT_Get_MM_Var");
		internal static Error FT_Get_MM_Var(IntPtr face, out IntPtr amaster) { return FT_Get_MM_Var0(face, out amaster); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Set_MM_Design_CoordinatesDelegate0(IntPtr face, UInt32 num_coords, IntPtr coords);
		private static readonly FT_Set_MM_Design_CoordinatesDelegate0 FT_Set_MM_Design_Coordinates0 = sExternLibrary.GetStaticProc<FT_Set_MM_Design_CoordinatesDelegate0>("FT_Set_MM_Design_Coordinates");
		internal static Error FT_Set_MM_Design_Coordinates(IntPtr face, UInt32 num_coords, IntPtr coords) { return FT_Set_MM_Design_Coordinates0(face, num_coords, coords); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Set_Var_Design_CoordinatesDelegate0(IntPtr face, UInt32 num_coords, IntPtr coords);
		private static readonly FT_Set_Var_Design_CoordinatesDelegate0 FT_Set_Var_Design_Coordinates0 = sExternLibrary.GetStaticProc<FT_Set_Var_Design_CoordinatesDelegate0>("FT_Set_Var_Design_Coordinates");
		internal static Error FT_Set_Var_Design_Coordinates(IntPtr face, UInt32 num_coords, IntPtr coords) { return FT_Set_Var_Design_Coordinates0(face, num_coords, coords); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Set_MM_Blend_CoordinatesDelegate0(IntPtr face, UInt32 num_coords, IntPtr coords);
		private static readonly FT_Set_MM_Blend_CoordinatesDelegate0 FT_Set_MM_Blend_Coordinates0 = sExternLibrary.GetStaticProc<FT_Set_MM_Blend_CoordinatesDelegate0>("FT_Set_MM_Blend_Coordinates");
		internal static Error FT_Set_MM_Blend_Coordinates(IntPtr face, UInt32 num_coords, IntPtr coords) { return FT_Set_MM_Blend_Coordinates0(face, num_coords, coords); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Set_Var_Blend_CoordinatesDelegate0(IntPtr face, UInt32 num_coords, IntPtr coords);
		private static readonly FT_Set_Var_Blend_CoordinatesDelegate0 FT_Set_Var_Blend_Coordinates0 = sExternLibrary.GetStaticProc<FT_Set_Var_Blend_CoordinatesDelegate0>("FT_Set_Var_Blend_Coordinates");
		internal static Error FT_Set_Var_Blend_Coordinates(IntPtr face, UInt32 num_coords, IntPtr coords) { return FT_Set_Var_Blend_Coordinates0(face, num_coords, coords); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_Get_Sfnt_TableDelegate0(IntPtr face, SfntTag tag);
		private static readonly FT_Get_Sfnt_TableDelegate0 FT_Get_Sfnt_Table0 = sExternLibrary.GetStaticProc<FT_Get_Sfnt_TableDelegate0>("FT_Get_Sfnt_Table");
		internal static IntPtr FT_Get_Sfnt_Table(IntPtr face, SfntTag tag) { return FT_Get_Sfnt_Table0(face, tag); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Load_Sfnt_TableDelegate0(IntPtr face, UInt32 tag, Int32 offset, IntPtr buffer, ref UInt32 length);
		private static readonly FT_Load_Sfnt_TableDelegate0 FT_Load_Sfnt_Table0 = sExternLibrary.GetStaticProc<FT_Load_Sfnt_TableDelegate0>("FT_Load_Sfnt_Table");
		internal static Error FT_Load_Sfnt_Table(IntPtr face, UInt32 tag, Int32 offset, IntPtr buffer, ref UInt32 length) { return FT_Load_Sfnt_Table0(face, tag, offset, buffer, ref length); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Sfnt_Table_InfoDelegate0(IntPtr face, UInt32 table_index, SfntTag* tag, out UInt32 length);
		private static unsafe readonly FT_Sfnt_Table_InfoDelegate0 FT_Sfnt_Table_Info0 = sExternLibrary.GetStaticProc<FT_Sfnt_Table_InfoDelegate0>("FT_Sfnt_Table_Info");
		internal static Error FT_Sfnt_Table_Info(IntPtr face, UInt32 table_index, SfntTag* tag, out UInt32 length) { return FT_Sfnt_Table_Info0(face, table_index, tag, out length); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 FT_Get_CMap_Language_IDDelegate0(IntPtr charmap);
		private static readonly FT_Get_CMap_Language_IDDelegate0 FT_Get_CMap_Language_ID0 = sExternLibrary.GetStaticProc<FT_Get_CMap_Language_IDDelegate0>("FT_Get_CMap_Language_ID");
		internal static UInt32 FT_Get_CMap_Language_ID(IntPtr charmap) { return FT_Get_CMap_Language_ID0(charmap); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 FT_Get_CMap_FormatDelegate0(IntPtr charmap);
		private static readonly FT_Get_CMap_FormatDelegate0 FT_Get_CMap_Format0 = sExternLibrary.GetStaticProc<FT_Get_CMap_FormatDelegate0>("FT_Get_CMap_Format");
		internal static Int32 FT_Get_CMap_Format(IntPtr charmap) { return FT_Get_CMap_Format0(charmap); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean FT_Has_PS_Glyph_NamesDelegate0(IntPtr face);
		private static readonly FT_Has_PS_Glyph_NamesDelegate0 FT_Has_PS_Glyph_Names0 = sExternLibrary.GetStaticProc<FT_Has_PS_Glyph_NamesDelegate0>("FT_Has_PS_Glyph_Names");
		internal static Boolean FT_Has_PS_Glyph_Names(IntPtr face) { return FT_Has_PS_Glyph_Names0(face); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_PS_Font_InfoDelegate0(IntPtr face, out FontInfoRec afont_info);
		private static readonly FT_Get_PS_Font_InfoDelegate0 FT_Get_PS_Font_Info0 = sExternLibrary.GetStaticProc<FT_Get_PS_Font_InfoDelegate0>("FT_Get_PS_Font_Info");
		internal static Error FT_Get_PS_Font_Info(IntPtr face, out FontInfoRec afont_info) { return FT_Get_PS_Font_Info0(face, out afont_info); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_PS_Font_PrivateDelegate0(IntPtr face, out PrivateRec afont_private);
		private static readonly FT_Get_PS_Font_PrivateDelegate0 FT_Get_PS_Font_Private0 = sExternLibrary.GetStaticProc<FT_Get_PS_Font_PrivateDelegate0>("FT_Get_PS_Font_Private");
		internal static Error FT_Get_PS_Font_Private(IntPtr face, out PrivateRec afont_private) { return FT_Get_PS_Font_Private0(face, out afont_private); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 FT_Get_PS_Font_ValueDelegate0(IntPtr face, DictionaryKeys key, UInt32 idx, ref IntPtr value, Int32 value_len);
		private static readonly FT_Get_PS_Font_ValueDelegate0 FT_Get_PS_Font_Value0 = sExternLibrary.GetStaticProc<FT_Get_PS_Font_ValueDelegate0>("FT_Get_PS_Font_Value");
		internal static Int32 FT_Get_PS_Font_Value(IntPtr face, DictionaryKeys key, UInt32 idx, ref IntPtr value, Int32 value_len) { return FT_Get_PS_Font_Value0(face, key, idx, ref value, value_len); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 FT_Get_Sfnt_Name_CountDelegate0(IntPtr face);
		private static readonly FT_Get_Sfnt_Name_CountDelegate0 FT_Get_Sfnt_Name_Count0 = sExternLibrary.GetStaticProc<FT_Get_Sfnt_Name_CountDelegate0>("FT_Get_Sfnt_Name_Count");
		internal static UInt32 FT_Get_Sfnt_Name_Count(IntPtr face) { return FT_Get_Sfnt_Name_Count0(face); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_Sfnt_NameDelegate0(IntPtr face, UInt32 idx, out SfntNameRec aname);
		private static readonly FT_Get_Sfnt_NameDelegate0 FT_Get_Sfnt_Name0 = sExternLibrary.GetStaticProc<FT_Get_Sfnt_NameDelegate0>("FT_Get_Sfnt_Name");
		internal static Error FT_Get_Sfnt_Name(IntPtr face, UInt32 idx, out SfntNameRec aname) { return FT_Get_Sfnt_Name0(face, idx, out aname); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_BDF_Charset_IDDelegate0(IntPtr face, out String acharset_encoding, out String acharset_registry);
		private static readonly FT_Get_BDF_Charset_IDDelegate0 FT_Get_BDF_Charset_ID0 = sExternLibrary.GetStaticProc<FT_Get_BDF_Charset_IDDelegate0>("FT_Get_BDF_Charset_ID");
		internal static Error FT_Get_BDF_Charset_ID(IntPtr face, out String acharset_encoding, out String acharset_registry) { return FT_Get_BDF_Charset_ID0(face, out acharset_encoding, out acharset_registry); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_BDF_PropertyDelegate0(IntPtr face, String prop_name, out IntPtr aproperty);
		private static readonly FT_Get_BDF_PropertyDelegate0 FT_Get_BDF_Property0 = sExternLibrary.GetStaticProc<FT_Get_BDF_PropertyDelegate0>("FT_Get_BDF_Property");
		internal static Error FT_Get_BDF_Property(IntPtr face, String prop_name, out IntPtr aproperty) { return FT_Get_BDF_Property0(face, prop_name, out aproperty); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_CID_Registry_Ordering_SupplementDelegate0(IntPtr face, out String registry, out String ordering, out Int32 aproperty);
		private static readonly FT_Get_CID_Registry_Ordering_SupplementDelegate0 FT_Get_CID_Registry_Ordering_Supplement0 = sExternLibrary.GetStaticProc<FT_Get_CID_Registry_Ordering_SupplementDelegate0>("FT_Get_CID_Registry_Ordering_Supplement");
		internal static Error FT_Get_CID_Registry_Ordering_Supplement(IntPtr face, out String registry, out String ordering, out Int32 aproperty) { return FT_Get_CID_Registry_Ordering_Supplement0(face, out registry, out ordering, out aproperty); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_CID_Is_Internally_CID_KeyedDelegate0(IntPtr face, out Byte is_cid);
		private static readonly FT_Get_CID_Is_Internally_CID_KeyedDelegate0 FT_Get_CID_Is_Internally_CID_Keyed0 = sExternLibrary.GetStaticProc<FT_Get_CID_Is_Internally_CID_KeyedDelegate0>("FT_Get_CID_Is_Internally_CID_Keyed");
		internal static Error FT_Get_CID_Is_Internally_CID_Keyed(IntPtr face, out Byte is_cid) { return FT_Get_CID_Is_Internally_CID_Keyed0(face, out is_cid); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_CID_From_Glyph_IndexDelegate0(IntPtr face, UInt32 glyph_index, out UInt32 cid);
		private static readonly FT_Get_CID_From_Glyph_IndexDelegate0 FT_Get_CID_From_Glyph_Index0 = sExternLibrary.GetStaticProc<FT_Get_CID_From_Glyph_IndexDelegate0>("FT_Get_CID_From_Glyph_Index");
		internal static Error FT_Get_CID_From_Glyph_Index(IntPtr face, UInt32 glyph_index, out UInt32 cid) { return FT_Get_CID_From_Glyph_Index0(face, glyph_index, out cid); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Set_CharmapDelegate0(IntPtr face, IntPtr charmap);
		private static readonly FT_Set_CharmapDelegate0 FT_Set_Charmap0 = sExternLibrary.GetStaticProc<FT_Set_CharmapDelegate0>("FT_Set_Charmap");
		internal static Error FT_Set_Charmap(IntPtr face, IntPtr charmap) { return FT_Set_Charmap0(face, charmap); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 FT_Get_Charmap_IndexDelegate0(IntPtr charmap);
		private static readonly FT_Get_Charmap_IndexDelegate0 FT_Get_Charmap_Index0 = sExternLibrary.GetStaticProc<FT_Get_Charmap_IndexDelegate0>("FT_Get_Charmap_Index");
		internal static Int32 FT_Get_Charmap_Index(IntPtr charmap) { return FT_Get_Charmap_Index0(charmap); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 FT_Get_Char_IndexDelegate0(IntPtr face, UInt32 charcode);
		private static readonly FT_Get_Char_IndexDelegate0 FT_Get_Char_Index0 = sExternLibrary.GetStaticProc<FT_Get_Char_IndexDelegate0>("FT_Get_Char_Index");
		internal static UInt32 FT_Get_Char_Index(IntPtr face, UInt32 charcode) { return FT_Get_Char_Index0(face, charcode); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 FT_Get_First_CharDelegate0(IntPtr face, out UInt32 agindex);
		private static readonly FT_Get_First_CharDelegate0 FT_Get_First_Char0 = sExternLibrary.GetStaticProc<FT_Get_First_CharDelegate0>("FT_Get_First_Char");
		internal static UInt32 FT_Get_First_Char(IntPtr face, out UInt32 agindex) { return FT_Get_First_Char0(face, out agindex); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 FT_Get_Next_CharDelegate0(IntPtr face, UInt32 char_code, out UInt32 agindex);
		private static readonly FT_Get_Next_CharDelegate0 FT_Get_Next_Char0 = sExternLibrary.GetStaticProc<FT_Get_Next_CharDelegate0>("FT_Get_Next_Char");
		internal static UInt32 FT_Get_Next_Char(IntPtr face, UInt32 char_code, out UInt32 agindex) { return FT_Get_Next_Char0(face, char_code, out agindex); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 FT_Get_Name_IndexDelegate0(IntPtr face, IntPtr glyph_name);
		private static readonly FT_Get_Name_IndexDelegate0 FT_Get_Name_Index0 = sExternLibrary.GetStaticProc<FT_Get_Name_IndexDelegate0>("FT_Get_Name_Index");
		internal static UInt32 FT_Get_Name_Index(IntPtr face, IntPtr glyph_name) { return FT_Get_Name_Index0(face, glyph_name); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_SubGlyph_InfoDelegate0(IntPtr glyph, UInt32 sub_index, out Int32 p_index, out SubGlyphFlags p_flags, out Int32 p_arg1, out Int32 p_arg2, out FTMatrix p_transform);
		private static readonly FT_Get_SubGlyph_InfoDelegate0 FT_Get_SubGlyph_Info0 = sExternLibrary.GetStaticProc<FT_Get_SubGlyph_InfoDelegate0>("FT_Get_SubGlyph_Info");
		internal static Error FT_Get_SubGlyph_Info(IntPtr glyph, UInt32 sub_index, out Int32 p_index, out SubGlyphFlags p_flags, out Int32 p_arg1, out Int32 p_arg2, out FTMatrix p_transform) { return FT_Get_SubGlyph_Info0(glyph, sub_index, out p_index, out p_flags, out p_arg1, out p_arg2, out p_transform); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate EmbeddingTypes FT_Get_FSType_FlagsDelegate0(IntPtr face);
		private static readonly FT_Get_FSType_FlagsDelegate0 FT_Get_FSType_Flags0 = sExternLibrary.GetStaticProc<FT_Get_FSType_FlagsDelegate0>("FT_Get_FSType_Flags");
		internal static EmbeddingTypes FT_Get_FSType_Flags(IntPtr face) { return FT_Get_FSType_Flags0(face); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 FT_Face_GetCharVariantIndexDelegate0(IntPtr face, UInt32 charcode, UInt32 variantSelector);
		private static readonly FT_Face_GetCharVariantIndexDelegate0 FT_Face_GetCharVariantIndex0 = sExternLibrary.GetStaticProc<FT_Face_GetCharVariantIndexDelegate0>("FT_Face_GetCharVariantIndex");
		internal static UInt32 FT_Face_GetCharVariantIndex(IntPtr face, UInt32 charcode, UInt32 variantSelector) { return FT_Face_GetCharVariantIndex0(face, charcode, variantSelector); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 FT_Face_GetCharVariantIsDefaultDelegate0(IntPtr face, UInt32 charcode, UInt32 variantSelector);
		private static readonly FT_Face_GetCharVariantIsDefaultDelegate0 FT_Face_GetCharVariantIsDefault0 = sExternLibrary.GetStaticProc<FT_Face_GetCharVariantIsDefaultDelegate0>("FT_Face_GetCharVariantIsDefault");
		internal static Int32 FT_Face_GetCharVariantIsDefault(IntPtr face, UInt32 charcode, UInt32 variantSelector) { return FT_Face_GetCharVariantIsDefault0(face, charcode, variantSelector); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_Face_GetVariantSelectorsDelegate0(IntPtr face);
		private static readonly FT_Face_GetVariantSelectorsDelegate0 FT_Face_GetVariantSelectors0 = sExternLibrary.GetStaticProc<FT_Face_GetVariantSelectorsDelegate0>("FT_Face_GetVariantSelectors");
		internal static IntPtr FT_Face_GetVariantSelectors(IntPtr face) { return FT_Face_GetVariantSelectors0(face); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_Face_GetVariantsOfCharDelegate0(IntPtr face, UInt32 charcode);
		private static readonly FT_Face_GetVariantsOfCharDelegate0 FT_Face_GetVariantsOfChar0 = sExternLibrary.GetStaticProc<FT_Face_GetVariantsOfCharDelegate0>("FT_Face_GetVariantsOfChar");
		internal static IntPtr FT_Face_GetVariantsOfChar(IntPtr face, UInt32 charcode) { return FT_Face_GetVariantsOfChar0(face, charcode); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_Face_GetCharsOfVariantDelegate0(IntPtr face, UInt32 variantSelector);
		private static readonly FT_Face_GetCharsOfVariantDelegate0 FT_Face_GetCharsOfVariant0 = sExternLibrary.GetStaticProc<FT_Face_GetCharsOfVariantDelegate0>("FT_Face_GetCharsOfVariant");
		internal static IntPtr FT_Face_GetCharsOfVariant(IntPtr face, UInt32 variantSelector) { return FT_Face_GetCharsOfVariant0(face, variantSelector); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_GlyphDelegate0(IntPtr slot, out IntPtr aglyph);
		private static readonly FT_Get_GlyphDelegate0 FT_Get_Glyph0 = sExternLibrary.GetStaticProc<FT_Get_GlyphDelegate0>("FT_Get_Glyph");
		internal static Error FT_Get_Glyph(IntPtr slot, out IntPtr aglyph) { return FT_Get_Glyph0(slot, out aglyph); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Glyph_CopyDelegate0(IntPtr source, out IntPtr target);
		private static readonly FT_Glyph_CopyDelegate0 FT_Glyph_Copy0 = sExternLibrary.GetStaticProc<FT_Glyph_CopyDelegate0>("FT_Glyph_Copy");
		internal static Error FT_Glyph_Copy(IntPtr source, out IntPtr target) { return FT_Glyph_Copy0(source, out target); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Glyph_TransformDelegate0(IntPtr glyph, ref FTMatrix matrix, ref FTVector delta);
		private static readonly FT_Glyph_TransformDelegate0 FT_Glyph_Transform0 = sExternLibrary.GetStaticProc<FT_Glyph_TransformDelegate0>("FT_Glyph_Transform");
		internal static Error FT_Glyph_Transform(IntPtr glyph, ref FTMatrix matrix, ref FTVector delta) { return FT_Glyph_Transform0(glyph, ref matrix, ref delta); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Glyph_Get_CBoxDelegate0(IntPtr glyph, GlyphBBoxMode bbox_mode, out BBox acbox);
		private static readonly FT_Glyph_Get_CBoxDelegate0 FT_Glyph_Get_CBox0 = sExternLibrary.GetStaticProc<FT_Glyph_Get_CBoxDelegate0>("FT_Glyph_Get_CBox");
		internal static void FT_Glyph_Get_CBox(IntPtr glyph, GlyphBBoxMode bbox_mode, out BBox acbox) { FT_Glyph_Get_CBox0(glyph, bbox_mode, out acbox); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Glyph_To_BitmapDelegate0(ref IntPtr the_glyph, RenderMode render_mode, ref FTVector26Dot6 origin, Boolean destroy);
		private static readonly FT_Glyph_To_BitmapDelegate0 FT_Glyph_To_Bitmap0 = sExternLibrary.GetStaticProc<FT_Glyph_To_BitmapDelegate0>("FT_Glyph_To_Bitmap");
		internal static Error FT_Glyph_To_Bitmap(ref IntPtr the_glyph, RenderMode render_mode, ref FTVector26Dot6 origin, Boolean destroy) { return FT_Glyph_To_Bitmap0(ref the_glyph, render_mode, ref origin, destroy); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Done_GlyphDelegate0(IntPtr glyph);
		private static readonly FT_Done_GlyphDelegate0 FT_Done_Glyph0 = sExternLibrary.GetStaticProc<FT_Done_GlyphDelegate0>("FT_Done_Glyph");
		internal static void FT_Done_Glyph(IntPtr glyph) { FT_Done_Glyph0(glyph); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_New_Face_From_FONDDelegate0(IntPtr library, IntPtr fond, Int32 face_index, out IntPtr aface);
		private static readonly FT_New_Face_From_FONDDelegate0 FT_New_Face_From_FOND0 = sExternLibrary.GetStaticProc<FT_New_Face_From_FONDDelegate0>("FT_New_Face_From_FOND");
		internal static Error FT_New_Face_From_FOND(IntPtr library, IntPtr fond, Int32 face_index, out IntPtr aface) { return FT_New_Face_From_FOND0(library, fond, face_index, out aface); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_GetFile_From_Mac_NameDelegate0(String fontName, out IntPtr pathSpec, out Int32 face_index);
		private static readonly FT_GetFile_From_Mac_NameDelegate0 FT_GetFile_From_Mac_Name0 = sExternLibrary.GetStaticProc<FT_GetFile_From_Mac_NameDelegate0>("FT_GetFile_From_Mac_Name");
		internal static Error FT_GetFile_From_Mac_Name(String fontName, out IntPtr pathSpec, out Int32 face_index) { return FT_GetFile_From_Mac_Name0(fontName, out pathSpec, out face_index); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_GetFile_From_Mac_ATS_NameDelegate0(String fontName, out IntPtr pathSpec, out Int32 face_index);
		private static readonly FT_GetFile_From_Mac_ATS_NameDelegate0 FT_GetFile_From_Mac_ATS_Name0 = sExternLibrary.GetStaticProc<FT_GetFile_From_Mac_ATS_NameDelegate0>("FT_GetFile_From_Mac_ATS_Name");
		internal static Error FT_GetFile_From_Mac_ATS_Name(String fontName, out IntPtr pathSpec, out Int32 face_index) { return FT_GetFile_From_Mac_ATS_Name0(fontName, out pathSpec, out face_index); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_GetFilePath_From_Mac_ATS_NameDelegate0(String fontName, IntPtr path, Int32 maxPathSize, out Int32 face_index);
		private static readonly FT_GetFilePath_From_Mac_ATS_NameDelegate0 FT_GetFilePath_From_Mac_ATS_Name0 = sExternLibrary.GetStaticProc<FT_GetFilePath_From_Mac_ATS_NameDelegate0>("FT_GetFilePath_From_Mac_ATS_Name");
		internal static Error FT_GetFilePath_From_Mac_ATS_Name(String fontName, IntPtr path, Int32 maxPathSize, out Int32 face_index) { return FT_GetFilePath_From_Mac_ATS_Name0(fontName, path, maxPathSize, out face_index); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_New_Face_From_FSSpecDelegate0(IntPtr library, IntPtr spec, Int32 face_index, out IntPtr aface);
		private static readonly FT_New_Face_From_FSSpecDelegate0 FT_New_Face_From_FSSpec0 = sExternLibrary.GetStaticProc<FT_New_Face_From_FSSpecDelegate0>("FT_New_Face_From_FSSpec");
		internal static Error FT_New_Face_From_FSSpec(IntPtr library, IntPtr spec, Int32 face_index, out IntPtr aface) { return FT_New_Face_From_FSSpec0(library, spec, face_index, out aface); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_New_Face_From_FSRefDelegate0(IntPtr library, IntPtr @ref, Int32 face_index, out IntPtr aface);
		private static readonly FT_New_Face_From_FSRefDelegate0 FT_New_Face_From_FSRef0 = sExternLibrary.GetStaticProc<FT_New_Face_From_FSRefDelegate0>("FT_New_Face_From_FSRef");
		internal static Error FT_New_Face_From_FSRef(IntPtr library, IntPtr @ref, Int32 face_index, out IntPtr aface) { return FT_New_Face_From_FSRef0(library, @ref, face_index, out aface); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Library_VersionDelegate0(IntPtr library, out Int32 amajor, out Int32 aminor, out Int32 apatch);
		private static readonly FT_Library_VersionDelegate0 FT_Library_Version0 = sExternLibrary.GetStaticProc<FT_Library_VersionDelegate0>("FT_Library_Version");
		internal static void FT_Library_Version(IntPtr library, out Int32 amajor, out Int32 aminor, out Int32 apatch) { FT_Library_Version0(library, out amajor, out aminor, out apatch); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean FT_Face_CheckTrueTypePatentsDelegate0(IntPtr face);
		private static readonly FT_Face_CheckTrueTypePatentsDelegate0 FT_Face_CheckTrueTypePatents0 = sExternLibrary.GetStaticProc<FT_Face_CheckTrueTypePatentsDelegate0>("FT_Face_CheckTrueTypePatents");
		internal static Boolean FT_Face_CheckTrueTypePatents(IntPtr face) { return FT_Face_CheckTrueTypePatents0(face); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean FT_Face_SetUnpatentedHintingDelegate0(IntPtr face, Boolean value);
		private static readonly FT_Face_SetUnpatentedHintingDelegate0 FT_Face_SetUnpatentedHinting0 = sExternLibrary.GetStaticProc<FT_Face_SetUnpatentedHintingDelegate0>("FT_Face_SetUnpatentedHinting");
		internal static Boolean FT_Face_SetUnpatentedHinting(IntPtr face, Boolean value) { return FT_Face_SetUnpatentedHinting0(face, value); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Init_FreeTypeDelegate0(out IntPtr alibrary);
		private static readonly FT_Init_FreeTypeDelegate0 FT_Init_FreeType0 = sExternLibrary.GetStaticProc<FT_Init_FreeTypeDelegate0>("FT_Init_FreeType");
		internal static Error FT_Init_FreeType(out IntPtr alibrary) { return FT_Init_FreeType0(out alibrary); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Done_FreeTypeDelegate0(IntPtr library);
		private static readonly FT_Done_FreeTypeDelegate0 FT_Done_FreeType0 = sExternLibrary.GetStaticProc<FT_Done_FreeTypeDelegate0>("FT_Done_FreeType");
		internal static Error FT_Done_FreeType(IntPtr library) { return FT_Done_FreeType0(library); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_New_FaceDelegate0(IntPtr library, String filepathname, Int32 face_index, out IntPtr aface);
		private static readonly FT_New_FaceDelegate0 FT_New_Face0 = sExternLibrary.GetStaticProc<FT_New_FaceDelegate0>("FT_New_Face");
		internal static Error FT_New_Face(IntPtr library, String filepathname, Int32 face_index, out IntPtr aface) { return FT_New_Face0(library, filepathname, face_index, out aface); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_New_Memory_FaceDelegate0(IntPtr library, IntPtr file_base, Int32 file_size, Int32 face_index, out IntPtr aface);
		private static readonly FT_New_Memory_FaceDelegate0 FT_New_Memory_Face0 = sExternLibrary.GetStaticProc<FT_New_Memory_FaceDelegate0>("FT_New_Memory_Face");
		internal static Error FT_New_Memory_Face(IntPtr library, IntPtr file_base, Int32 file_size, Int32 face_index, out IntPtr aface) { return FT_New_Memory_Face0(library, file_base, file_size, face_index, out aface); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Open_FaceDelegate0(IntPtr library, IntPtr args, Int32 face_index, out IntPtr aface);
		private static readonly FT_Open_FaceDelegate0 FT_Open_Face0 = sExternLibrary.GetStaticProc<FT_Open_FaceDelegate0>("FT_Open_Face");
		internal static Error FT_Open_Face(IntPtr library, IntPtr args, Int32 face_index, out IntPtr aface) { return FT_Open_Face0(library, args, face_index, out aface); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Attach_FileDelegate0(IntPtr face, String filepathname);
		private static readonly FT_Attach_FileDelegate0 FT_Attach_File0 = sExternLibrary.GetStaticProc<FT_Attach_FileDelegate0>("FT_Attach_File");
		internal static Error FT_Attach_File(IntPtr face, String filepathname) { return FT_Attach_File0(face, filepathname); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Attach_StreamDelegate0(IntPtr face, IntPtr parameters);
		private static readonly FT_Attach_StreamDelegate0 FT_Attach_Stream0 = sExternLibrary.GetStaticProc<FT_Attach_StreamDelegate0>("FT_Attach_Stream");
		internal static Error FT_Attach_Stream(IntPtr face, IntPtr parameters) { return FT_Attach_Stream0(face, parameters); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Reference_FaceDelegate0(IntPtr face);
		private static readonly FT_Reference_FaceDelegate0 FT_Reference_Face0 = sExternLibrary.GetStaticProc<FT_Reference_FaceDelegate0>("FT_Reference_Face");
		internal static Error FT_Reference_Face(IntPtr face) { return FT_Reference_Face0(face); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Done_FaceDelegate0(IntPtr face);
		private static readonly FT_Done_FaceDelegate0 FT_Done_Face0 = sExternLibrary.GetStaticProc<FT_Done_FaceDelegate0>("FT_Done_Face");
		internal static Error FT_Done_Face(IntPtr face) { return FT_Done_Face0(face); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Select_SizeDelegate0(IntPtr face, Int32 strike_index);
		private static readonly FT_Select_SizeDelegate0 FT_Select_Size0 = sExternLibrary.GetStaticProc<FT_Select_SizeDelegate0>("FT_Select_Size");
		internal static Error FT_Select_Size(IntPtr face, Int32 strike_index) { return FT_Select_Size0(face, strike_index); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Request_SizeDelegate0(IntPtr face, IntPtr req);
		private static readonly FT_Request_SizeDelegate0 FT_Request_Size0 = sExternLibrary.GetStaticProc<FT_Request_SizeDelegate0>("FT_Request_Size");
		internal static Error FT_Request_Size(IntPtr face, IntPtr req) { return FT_Request_Size0(face, req); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Set_Char_SizeDelegate0(IntPtr face, IntPtr char_width, IntPtr char_height, UInt32 horz_resolution, UInt32 vert_resolution);
		private static readonly FT_Set_Char_SizeDelegate0 FT_Set_Char_Size0 = sExternLibrary.GetStaticProc<FT_Set_Char_SizeDelegate0>("FT_Set_Char_Size");
		internal static Error FT_Set_Char_Size(IntPtr face, IntPtr char_width, IntPtr char_height, UInt32 horz_resolution, UInt32 vert_resolution) { return FT_Set_Char_Size0(face, char_width, char_height, horz_resolution, vert_resolution); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Set_Pixel_SizesDelegate0(IntPtr face, UInt32 pixel_width, UInt32 pixel_height);
		private static readonly FT_Set_Pixel_SizesDelegate0 FT_Set_Pixel_Sizes0 = sExternLibrary.GetStaticProc<FT_Set_Pixel_SizesDelegate0>("FT_Set_Pixel_Sizes");
		internal static Error FT_Set_Pixel_Sizes(IntPtr face, UInt32 pixel_width, UInt32 pixel_height) { return FT_Set_Pixel_Sizes0(face, pixel_width, pixel_height); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Load_GlyphDelegate0(IntPtr face, UInt32 glyph_index, Int32 load_flags);
		private static readonly FT_Load_GlyphDelegate0 FT_Load_Glyph0 = sExternLibrary.GetStaticProc<FT_Load_GlyphDelegate0>("FT_Load_Glyph");
		internal static Error FT_Load_Glyph(IntPtr face, UInt32 glyph_index, Int32 load_flags) { return FT_Load_Glyph0(face, glyph_index, load_flags); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Load_CharDelegate0(IntPtr face, UInt32 char_code, Int32 load_flags);
		private static readonly FT_Load_CharDelegate0 FT_Load_Char0 = sExternLibrary.GetStaticProc<FT_Load_CharDelegate0>("FT_Load_Char");
		internal static Error FT_Load_Char(IntPtr face, UInt32 char_code, Int32 load_flags) { return FT_Load_Char0(face, char_code, load_flags); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FT_Set_TransformDelegate0(IntPtr face, IntPtr matrix, IntPtr delta);
		private static readonly FT_Set_TransformDelegate0 FT_Set_Transform0 = sExternLibrary.GetStaticProc<FT_Set_TransformDelegate0>("FT_Set_Transform");
		internal static void FT_Set_Transform(IntPtr face, IntPtr matrix, IntPtr delta) { FT_Set_Transform0(face, matrix, delta); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Render_GlyphDelegate0(IntPtr slot, RenderMode render_mode);
		private static readonly FT_Render_GlyphDelegate0 FT_Render_Glyph0 = sExternLibrary.GetStaticProc<FT_Render_GlyphDelegate0>("FT_Render_Glyph");
		internal static Error FT_Render_Glyph(IntPtr slot, RenderMode render_mode) { return FT_Render_Glyph0(slot, render_mode); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_KerningDelegate0(IntPtr face, UInt32 left_glyph, UInt32 right_glyph, UInt32 kern_mode, out FTVector26Dot6 akerning);
		private static readonly FT_Get_KerningDelegate0 FT_Get_Kerning0 = sExternLibrary.GetStaticProc<FT_Get_KerningDelegate0>("FT_Get_Kerning");
		internal static Error FT_Get_Kerning(IntPtr face, UInt32 left_glyph, UInt32 right_glyph, UInt32 kern_mode, out FTVector26Dot6 akerning) { return FT_Get_Kerning0(face, left_glyph, right_glyph, kern_mode, out akerning); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_Track_KerningDelegate0(IntPtr face, IntPtr point_size, Int32 degree, out IntPtr akerning);
		private static readonly FT_Get_Track_KerningDelegate0 FT_Get_Track_Kerning0 = sExternLibrary.GetStaticProc<FT_Get_Track_KerningDelegate0>("FT_Get_Track_Kerning");
		internal static Error FT_Get_Track_Kerning(IntPtr face, IntPtr point_size, Int32 degree, out IntPtr akerning) { return FT_Get_Track_Kerning0(face, point_size, degree, out akerning); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Get_Glyph_NameDelegate0(IntPtr face, UInt32 glyph_index, IntPtr buffer, UInt32 buffer_max);
		private static readonly FT_Get_Glyph_NameDelegate0 FT_Get_Glyph_Name0 = sExternLibrary.GetStaticProc<FT_Get_Glyph_NameDelegate0>("FT_Get_Glyph_Name");
		internal static Error FT_Get_Glyph_Name(IntPtr face, UInt32 glyph_index, IntPtr buffer, UInt32 buffer_max) { return FT_Get_Glyph_Name0(face, glyph_index, buffer, buffer_max); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr FT_Get_Postscript_NameDelegate0(IntPtr face);
		private static readonly FT_Get_Postscript_NameDelegate0 FT_Get_Postscript_Name0 = sExternLibrary.GetStaticProc<FT_Get_Postscript_NameDelegate0>("FT_Get_Postscript_Name");
		internal static IntPtr FT_Get_Postscript_Name(IntPtr face) { return FT_Get_Postscript_Name0(face); }
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Error FT_Select_CharmapDelegate0(IntPtr face, Encoding encoding);
		private static readonly FT_Select_CharmapDelegate0 FT_Select_Charmap0 = sExternLibrary.GetStaticProc<FT_Select_CharmapDelegate0>("FT_Select_Charmap");
		internal static Error FT_Select_Charmap(IntPtr face, Encoding encoding) { return FT_Select_Charmap0(face, encoding); }


		#endregion
	}
}
