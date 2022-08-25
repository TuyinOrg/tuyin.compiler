using System;
using System.Collections.Generic;
using compute.vulkan;

namespace compute
{
    public struct ImageDescriptor : ISurfaceDescriptor
    {
        internal ImageDescriptor(SurfaceDescriptorUsage usage, Interface @interface, Image image, Sampler sampler)
        {
            Usage = usage;
            Interface = @interface;
            Image = image;
            Sampler = sampler;
        }

        public SurfaceDescriptorUsage Usage { get; }

        public Interface Interface { get; }

        internal Image Image { get; }

        internal Sampler Sampler { get; }

        public override bool Equals(object obj)
        {
            return obj is ImageDescriptor descriptor &&
                   Usage == descriptor.Usage &&
                   EqualityComparer<Interface>.Default.Equals(Interface, descriptor.Interface);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Usage, Interface);
        }
    }
}
