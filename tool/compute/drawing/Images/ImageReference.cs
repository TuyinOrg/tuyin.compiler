using compute.environment;
using System;
using System.Collections.Generic;

namespace compute.drawing.images
{
    struct ImageReference : IStaticResource
    {
        public VulkanImage VulkanImage { get; }

        public Rectangle Block { get; }

        public ImageReference(VulkanImage vulkanImage, Rectangle block)
        {
            VulkanImage = vulkanImage;
            Block = block;
        }

        public override bool Equals(object obj)
        {
            return obj is ImageReference reference &&
                   EqualityComparer<VulkanImage>.Default.Equals(VulkanImage, reference.VulkanImage) &&
                   Block.Equals(reference.Block);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(VulkanImage, Block);
        }

        ReadOnlySpan<byte> IStaticResource.Compile()
        {
            throw new NotImplementedException();
        }
    }
}
