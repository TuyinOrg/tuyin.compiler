using System;

namespace compute.drawing
{
    /// <summary>
    /// 位图
    /// 注意:推荐使用RGB16+指定透明背景色,性能与ETC1压缩纹理相差无几
    /// </summary>
    struct Bitmap : IImage
    {
        public PixelFormat PixelFormat { get; }

        public Size Size { get; }

        public byte[] Data { get; }

        internal Bitmap(PixelFormat pixelFormat, Size size, byte[] data)
        {
            PixelFormat = pixelFormat;
            Size = size;
            Data = data;
        }

        public Color GetPixel(int x, int y)
        {
            throw new NotImplementedException();
        }

        ReadOnlySpan<byte> IStaticResource.Compile()
        {
            return Data;
        }
    }
}
