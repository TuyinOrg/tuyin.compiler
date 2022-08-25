using compute.environment;
using compute.environment.content;
using System;

namespace compute.drawing
{
    /// <summary>
    /// 映射图形
    /// </summary>
    struct VulkanImageBlock : IImage
    {
        private Rectangle _bounds;
        private VulkanImage _vulkanImage;

        internal VulkanImageBlock(VulkanImage vulkanImage, Rectangle bounds)
        {
            _vulkanImage = vulkanImage;
            _bounds = bounds;
            PixelFormat = Loader.GetVulkanImagePixelFormat(vulkanImage);
        }

        public PixelFormat PixelFormat { get; }

        public Size Size => _bounds.Size;

        public unsafe Color GetPixel(int x, int y)
        {
            if (PixelFormat == PixelFormat.Undefined)
                throw new NotSupportedException("GetPixel not support this image's format.");

            x = _bounds.X + x;
            y = _bounds.Y + y;

            var imageInfo = _vulkanImage.Image.GetSparseMemoryRequirements()[0];
            var format = imageInfo.FormatProperties.ImageGranularity;
            var address = (byte*)_vulkanImage.Memory.Map((x + y * format.Width) * format.Depth, format.Depth);

            Color clr = Color.Transparent;

            // Get color components count
            int cCount = format.Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * format.Width) + x) * cCount;

            if (i > imageInfo.ImageMipTailSize / format.Depth)
                throw new IndexOutOfRangeException();

            if (format.Depth == 32) // For 32 bpp get Red, Green, Blue and Alpha
            {
                byte b = *address;
                byte g = *(address + 1);
                byte r = *(address + 2);
                byte a = *(address + 3);
                clr = Color.FromArgb(a, r, g, b);
            }
            if (format.Depth == 24) // For 24 bpp get Red, Green and Blue
            {
                byte b = *(address + 1);
                byte g = *(address + 2);
                byte r = *(address + 3);
                clr = Color.FromArgb(r, g, b);
            }
            if (format.Depth == 8)
            // For 8 bpp get color value (Red, Green and Blue values are the same)
            {
                byte c = *(address + 0);
                clr = Color.FromArgb(c, c, c);
            }
            return clr;
        }

        unsafe ReadOnlySpan<byte> IStaticResource.Compile()
        {
            var imageInfo = _vulkanImage.Image.GetSparseMemoryRequirements()[0];
            var format = imageInfo.FormatProperties.ImageGranularity;
            var length = _bounds.Width * _bounds.Height * format.Depth;
            var offset = (_bounds.X + _bounds.Y * format.Width) * format.Depth;

            var address = (byte*)_vulkanImage.Memory.Map(offset, length);

            /*
            var resultBytes = new byte[length];
            fixed (byte* buffer = resultBytes)
            {
                FastBuffer.ParallelBlockCopyLR(address, buffer, resultBytes.Length, 56 * 56 * 32);
            }
            _vulkanImage.Memory.Unmap();
            */

            return new ReadOnlySpan<byte>(address, length);
        }
    }
}
