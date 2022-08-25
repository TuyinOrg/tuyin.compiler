using compute.environment;
using compute.vulkan;

namespace compute.drawing
{
    /// <summary>
    /// 图形接口
    /// 在闲置时根据策略会被ASTC压缩纹理或基于block块的通用计算资源代替
    /// 注意:该策略会平衡显存开销，显存带宽IO，和显卡计算能力
    /// </summary>
    public interface IImage : IStaticResource
    {
        PixelFormat PixelFormat { get; }

        Size Size { get; }

        int Depth => (int)PixelFormat >> 8;

        int Width => Size.Width;

        int Height => Size.Height;

        Color GetPixel(int x, int y);
    }
}
