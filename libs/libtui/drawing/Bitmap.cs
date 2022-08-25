using libtui.content;

namespace libtui.drawing
{
    public sealed class Bitmap : Image
    {
        private VulkanImage mVulkanImage;

        public override PixelFormat PixelFormat => Loader.GetVulkanImagePixelFormat(mVulkanImage);

        public override Size Size 
        {
            get 
            {
                var imageInfo = mVulkanImage.Image.GetSparseMemoryRequirements()[0];
                var format = imageInfo.FormatProperties.ImageGranularity;
                return new Size(format.Width, format.Height);
            }
        }

        internal Bitmap(VulkanImage vulkanImage)
        {
            this.mVulkanImage = vulkanImage;
        }

        public Bitmap(int width, int height)
        {
        }

        public static Bitmap FromFile(string path)
        {
            return new Bitmap(App.Content.Load<VulkanImage>(path));
        }
    }
}
