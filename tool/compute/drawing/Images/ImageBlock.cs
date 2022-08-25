using compute.utils;
using System;

namespace compute.drawing.images
{
    struct ImageBlock : IImage
    {
        private Rectangle _bounds;
        private IImage _image;

        internal ImageBlock(IImage image, Rectangle bounds)
        {
            _image = image;
            _bounds = bounds;
        }

        public PixelFormat PixelFormat => _image.PixelFormat;

        public Size Size => _bounds.Size;

        public unsafe ReadOnlySpan<byte> Compile()
        {
            var depth = (int)_image.PixelFormat >> 8;
            var parentBytes = _image.Compile();
            var resultBytes = new byte[_bounds.Width * _bounds.Height * depth];
            var dstStride = depth * _bounds.Width;
            var srcStride = depth * _image.Width;

            for (var i = 0; i < _bounds.Height; i++)
            {
                var srcIndex = i * srcStride;
                var dstIndex = i * dstStride;

                fixed (byte* src = &parentBytes[srcIndex], dst = &resultBytes[dstIndex])
                    FastBuffer.UnsafeBlockCopyLR(src, dst, dstStride);
            }

            return resultBytes;
        }

        public unsafe Color GetPixel(int x, int y)
        {
            if (PixelFormat == PixelFormat.Undefined)
                throw new NotSupportedException("GetPixel not support this image's format.");

            return _image.GetPixel(_bounds.X + x, _bounds.Y + y);
        }
    }
}
