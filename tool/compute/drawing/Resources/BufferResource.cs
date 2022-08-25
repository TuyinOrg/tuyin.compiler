using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace compute.drawing
{
    struct BufferResource : IStaticResource
    {
        private int mByteSize;

        public BufferResource(int byteSize) 
        {
            mByteSize = byteSize;
        }

        public ReadOnlySpan<byte> Compile()
        {
            throw new NotImplementedException();
        }
    }

    struct BufferResource<T> : IStaticResource where T : struct
    {
        public Type ItemType => typeof(T);

        public IntPtr Start { get; }

        public IntPtr End { get; }

        public BufferResource(T[] items) 
        {
            Start = Marshal.UnsafeAddrOfPinnedArrayElement(items, 0);
            End = Marshal.UnsafeAddrOfPinnedArrayElement(items, items.Length - 1);
        }

        public unsafe ReadOnlySpan<byte> Compile()
        {
            int stride = Unsafe.SizeOf<T>();
            long size = End.ToInt64() - Start.ToInt64() + stride;
            byte[] bytes = new byte[size];

            void* srcPtr = (void*)Start;
            void* dstPtr = Unsafe.AsPointer(ref bytes[0]);
            Buffer.MemoryCopy(srcPtr, dstPtr, size, size);

            return bytes;
        }

        public override bool Equals(object obj)
        {
            return obj is BufferResource<T> resource &&
                   EqualityComparer<Type>.Default.Equals(ItemType, resource.ItemType) &&
                   Start.Equals(resource.Start) &&
                   End.Equals(resource.End);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ItemType, Start, End);
        }
    }
}
