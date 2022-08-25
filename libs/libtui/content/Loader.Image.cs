using libtui.drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Vulkan;
using static FreeImage.FreeImage;
using Image = libtui.drawing.Image;

namespace libtui.content
{
    internal static partial class Loader
    {
        private readonly static Dictionary<PixelFormat, Format> sPixelVulkanFormat = new Dictionary<PixelFormat, Format>() 
        {
            //{ PixelFormat.Format1bppIndexed, Format.Undefined },
            //{ PixelFormat.Format4bppIndexed, Format.Undefined },
            { PixelFormat.Format8bppIndexed, Format.R8UNorm },
            //{ PixelFormat.Format16bppRgb565,Format.Undefined },
            //{ PixelFormat.Format16bppRgb555, Format.Undefined },  // 需要填充
            { PixelFormat.Format24bppRgb, Format.R8G8B8UNorm },
            { PixelFormat.Format32bppArgb, Format.A8B8G8R8SNormPack32 }
        };

        private readonly static Dictionary<Format, PixelFormat> sVulkanPixelFormat = sPixelVulkanFormat.ToDictionary(x => x.Value, x => x.Key);

        public unsafe static VulkanImage LoadUncompressedVulkanImage(Window window, VulkanContext ctx, string path)
        {
            var dib = LoadEx(path);
            var format = GetPixelFormat(dib);

            if (!sPixelVulkanFormat.ContainsKey(format) || sPixelVulkanFormat[format] == Format.Undefined)
                throw new NotSupportedException($"Image's pixel format '{format}' is not had been supported.");

            var vf = sPixelVulkanFormat[format];
            var depth = (int)GetBPP(dib);
            var width = (int)GetWidth(dib);
            var height = (int)GetHeight(dib);

            /* 读取单像素          
            RGBQUAD quad = default;
            if (!GetPixelColor(dib, 0, 0, out quad))
                throw new ArgumentException($"File path '{path}' is not vaild imaging file.");
            */

            var data = new TextureData
            {
                Mipmaps = new TextureData.Mipmap[1],
                Format = vf
            };

            var mipmap = new TextureData.Mipmap
            {
                Size = width * height,
                Extent = new Extent3D(width, height, depth)
            };

            mipmap.Data = new byte[mipmap.Size * depth / 8];
            Marshal.Copy(GetBits(dib), mipmap.Data, 0, mipmap.Data.Length);
            data.Mipmaps[0] = mipmap;
            Unload(dib);

            return VulkanImage.Texture2D(ctx, data);
        }

        public static Format GetVulkanFormat(Image image) => sPixelVulkanFormat.ContainsKey(image.PixelFormat) ? sPixelVulkanFormat[image.PixelFormat] : Format.Undefined;

        public static PixelFormat GetVulkanImagePixelFormat(VulkanImage image) => sVulkanPixelFormat.ContainsKey(image.Format) ? sVulkanPixelFormat[image.Format] : PixelFormat.Undefined;
    }
}
