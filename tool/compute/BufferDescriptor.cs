using System;
using System.Collections.Generic;
using compute.environment;

namespace compute
{
    public struct BufferDescriptor : ISurfaceDescriptor
    {
        internal BufferDescriptor(SurfaceDescriptorUsage usage, Interface @interface, VulkanBuffer buffer)
        {
            Usage = usage;
            Interface = @interface;
            Buffer=  buffer;
        }

        public SurfaceDescriptorUsage Usage { get; }

        public Interface Interface { get; }

        public int Count => Buffer.Count;

        internal VulkanBuffer Buffer { get; }

        public override bool Equals(object obj)
        {
            return obj is BufferDescriptor descriptor &&
                   EqualityComparer<Interface>.Default.Equals(Interface, descriptor.Interface) &&
                   Count == descriptor.Count &&
                   Usage == descriptor.Usage;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Interface, Count, Usage);
        }
    }
}
