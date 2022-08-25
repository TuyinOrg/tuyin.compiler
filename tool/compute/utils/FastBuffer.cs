using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace compute.utils
{
    /// <summary>
    /// 高性能的数据拷贝对象
    /// </summary>
    static class FastBuffer
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static void UnsafeBlockCopyRL(ref byte src, ref byte dst, int count)
        {
            UnsafeBlockCopyRL((byte*)src, (byte*)dst, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static void UnsafeBlockCopyRL(byte* src, byte* dst, int count)
        {
        SMALLTABLE:
            switch (count)
            {
                case 0:
                    return;
                case 1:
                    *dst = *src;
                    return;
                case 2:
                    *(short*)dst = *(short*)src;
                    return;
                case 3:
                    *(dst + 2) = *(src + 2);
                    *(short*)(dst + 0) = *(short*)(src + 0);
                    return;
                case 4:
                    *(int*)dst = *(int*)src;
                    return;
                case 5:
                    *(dst + 4) = *(src + 4);
                    *(int*)(dst + 0) = *(int*)(src + 0);
                    return;
                case 6:
                    *(short*)(dst + 4) = *(short*)(src + 4);
                    *(int*)(dst + 0) = *(int*)(src + 0);
                    return;
                case 7:
                    *(dst + 6) = *(src + 6);
                    *(short*)(dst + 4) = *(short*)(src + 4);
                    *(int*)(dst + 0) = *(int*)(src + 0);
                    return;
                case 8:
                    *(long*)dst = *(long*)src;
                    return;
                case 9:
                    *(dst + 8) = *(src + 8);
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    return;
                case 10:
                    *(short*)(dst + 8) = *(short*)(src + 8);
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    return;
                case 11:
                    *(dst + 10) = *(src + 10);
                    *(short*)(dst + 8) = *(short*)(src + 8);
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    return;
                case 12:
                    *(int*)(dst + 8) = *(int*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 13:
                    *(dst + 12) = *(src + 12);
                    *(int*)(dst + 8) = *(int*)(src + 8);
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    return;
                case 14:
                    *(short*)(dst + 12) = *(short*)(src + 12);
                    *(int*)(dst + 8) = *(int*)(src + 8);
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    return;
                case 15:
                    *(dst + 14) = *(src + 14);
                    *(short*)(dst + 12) = *(short*)(src + 12);
                    *(int*)(dst + 8) = *(int*)(src + 8);
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    return;
                case 16:
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 17:
                    *(dst + 16) = *(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 18:
                    *(short*)(dst + 16) = *(short*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 19:
                    *(dst + 18) = *(src + 18);
                    *(short*)(dst + 16) = *(short*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 20:
                    *(int*)(dst + 16) = *(int*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;

                case 21:
                    *(dst + 20) = *(src + 20);
                    *(int*)(dst + 16) = *(int*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 22:
                    *(short*)(dst + 20) = *(short*)(src + 20);
                    *(int*)(dst + 16) = *(int*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 23:
                    *(dst + 22) = *(src + 22);
                    *(short*)(dst + 20) = *(short*)(src + 20);
                    *(int*)(dst + 16) = *(int*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 24:
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 25:
                    *(dst + 24) = *(src + 24);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 26:
                    *(short*)(dst + 24) = *(short*)(src + 24);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 27:
                    *(dst + 26) = *(src + 26);
                    *(short*)(dst + 24) = *(short*)(src + 24);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 28:
                    *(int*)(dst + 24) = *(int*)(src + 24);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 29:
                    *(dst + 28) = *(src + 28);
                    *(int*)(dst + 24) = *(int*)(src + 24);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 30:
                    *(short*)(dst + 28) = *(short*)(src + 28);
                    *(int*)(dst + 24) = *(int*)(src + 24);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 31:
                    *(dst + 30) = *(src + 30);
                    *(short*)(dst + 28) = *(short*)(src + 28);
                    *(int*)(dst + 24) = *(int*)(src + 24);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                case 32:
                    *(long*)(dst + 24) = *(long*)(src + 24);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)dst = *(long*)src;
                    return;
                default:
                    break;
            }

            long* lpSrc = (long*)(src + count - 8);
            long* ldSrc = (long*)(dst + count - 8);
            while (count >= 64)
            {
                *(ldSrc - 0) = *(lpSrc - 0);
                *(ldSrc - 1) = *(lpSrc - 1);
                *(ldSrc - 2) = *(lpSrc - 2);
                *(ldSrc - 3) = *(lpSrc - 3);
                *(ldSrc - 4) = *(lpSrc - 4);
                *(ldSrc - 5) = *(lpSrc - 5);
                *(ldSrc - 6) = *(lpSrc - 6);
                *(ldSrc - 7) = *(lpSrc - 7);

                if (count == 64)
                    return;

                count -= 64;
                lpSrc -= 8;
                ldSrc -= 8;
            }

            if (count > 32)
            {
                *(ldSrc - 0) = *(lpSrc - 0);
                *(ldSrc - 1) = *(lpSrc - 1);
                *(ldSrc - 2) = *(lpSrc - 2);
                *(ldSrc - 3) = *(lpSrc - 3);

                count -= 32;
                lpSrc -= 4;
                ldSrc -= 4;
            }
            
            goto SMALLTABLE;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static void UnsafeBlockCopyLR(ref byte src, ref byte dst, int count)
        {
            UnsafeBlockCopyLR((byte*)src, (byte*)dst, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static void UnsafeBlockCopyLR(byte* src, byte* dst, int count)
        {
        SMALLTABLE:
            switch (count)
            {
                case 0:
                    return;
                case 1:
                    *dst = *src;
                    return;
                case 2:
                    *(short*)dst = *(short*)src;
                    return;
                case 3:
                    *(short*)(dst + 0) = *(short*)(src + 0);
                    *(dst + 2) = *(src + 2);
                    return;
                case 4:
                    *(int*)dst = *(int*)src;
                    return;
                case 5:
                    *(int*)(dst + 0) = *(int*)(src + 0);
                    *(dst + 4) = *(src + 4);
                    return;
                case 6:
                    *(int*)(dst + 0) = *(int*)(src + 0);
                    *(short*)(dst + 4) = *(short*)(src + 4);
                    return;
                case 7:
                    *(int*)(dst + 0) = *(int*)(src + 0);
                    *(short*)(dst + 4) = *(short*)(src + 4);
                    *(dst + 6) = *(src + 6);
                    return;
                case 8:
                    *(long*)dst = *(long*)src;
                    return;
                case 9:
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    *(dst + 8) = *(src + 8);
                    return;
                case 10:
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    *(short*)(dst + 8) = *(short*)(src + 8);
                    return;
                case 11:
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    *(short*)(dst + 8) = *(short*)(src + 8);
                    *(dst + 10) = *(src + 10);
                    return;
                case 12:
                    *(long*)dst = *(long*)src;
                    *(int*)(dst + 8) = *(int*)(src + 8);
                    return;
                case 13:
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    *(int*)(dst + 8) = *(int*)(src + 8);
                    *(dst + 12) = *(src + 12);
                    return;
                case 14:
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    *(int*)(dst + 8) = *(int*)(src + 8);
                    *(short*)(dst + 12) = *(short*)(src + 12);
                    return;
                case 15:
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    *(int*)(dst + 8) = *(int*)(src + 8);
                    *(short*)(dst + 12) = *(short*)(src + 12);
                    *(dst + 14) = *(src + 14);
                    return;
                case 16:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    return;
                case 17:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(dst + 16) = *(src + 16);
                    return;
                case 18:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(short*)(dst + 16) = *(short*)(src + 16);
                    return;
                case 19:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(short*)(dst + 16) = *(short*)(src + 16);
                    *(dst + 18) = *(src + 18);
                    return;
                case 20:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(int*)(dst + 16) = *(int*)(src + 16);
                    return;

                case 21:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(int*)(dst + 16) = *(int*)(src + 16);
                    *(dst + 20) = *(src + 20);
                    return;
                case 22:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(int*)(dst + 16) = *(int*)(src + 16);
                    *(short*)(dst + 20) = *(short*)(src + 20);
                    return;
                case 23:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(int*)(dst + 16) = *(int*)(src + 16);
                    *(short*)(dst + 20) = *(short*)(src + 20);
                    *(dst + 22) = *(src + 22);
                    return;
                case 24:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    return;
                case 25:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(dst + 24) = *(src + 24);
                    return;
                case 26:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(short*)(dst + 24) = *(short*)(src + 24);
                    return;
                case 27:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(short*)(dst + 24) = *(short*)(src + 24);
                    *(dst + 26) = *(src + 26);
                    return;
                case 28:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(int*)(dst + 24) = *(int*)(src + 24);
                    return;
                case 29:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(int*)(dst + 24) = *(int*)(src + 24);
                    *(dst + 28) = *(src + 28);
                    return;
                case 30:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(int*)(dst + 24) = *(int*)(src + 24);
                    *(short*)(dst + 28) = *(short*)(src + 28);
                    return;
                case 31:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(int*)(dst + 24) = *(int*)(src + 24);
                    *(short*)(dst + 28) = *(short*)(src + 28);
                    *(dst + 30) = *(src + 30);
                    return;
                case 32:
                    *(long*)dst = *(long*)src;
                    *(long*)(dst + 8) = *(long*)(src + 8);
                    *(long*)(dst + 16) = *(long*)(src + 16);
                    *(long*)(dst + 24) = *(long*)(src + 24);
                    return;
                default:
                    break;
            }

            long* lpSrc = (long*)src;
            long* ldSrc = (long*)dst;
            while (count >= 64)
            {
                *(ldSrc + 0) = *(lpSrc + 0);
                *(ldSrc + 1) = *(lpSrc + 1);
                *(ldSrc + 2) = *(lpSrc + 2);
                *(ldSrc + 3) = *(lpSrc + 3);
                *(ldSrc + 4) = *(lpSrc + 4);
                *(ldSrc + 5) = *(lpSrc + 5);
                *(ldSrc + 6) = *(lpSrc + 6);
                *(ldSrc + 7) = *(lpSrc + 7);
                if (count == 64)
                    return;
                count -= 64;
                lpSrc += 8;
                ldSrc += 8;
            }
            if (count > 32)
            {
                *(ldSrc + 0) = *(lpSrc + 0);
                *(ldSrc + 1) = *(lpSrc + 1);
                *(ldSrc + 2) = *(lpSrc + 2);
                *(ldSrc + 3) = *(lpSrc + 3);
                count -= 32;
                lpSrc += 4;
                ldSrc += 4;
            }

            src = (byte*)lpSrc;
            dst = (byte*)ldSrc;
            goto SMALLTABLE;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static void ParallelBlockCopyRL(ref byte src, ref byte dst, int count, int threshold = 512)
        {
            ParallelBlockCopyRL((byte*)src, (byte*)dst, count, threshold);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static void ParallelBlockCopyRL(byte* src, byte* dst, int count, int threshold = 512) 
        {
            var parallelCount = count / threshold;
            var singleCount = count % threshold;

            if (singleCount > 0)
            {
                var index = parallelCount * threshold;
                UnsafeBlockCopyRL(src + index, dst + index, singleCount);
            }

            if (parallelCount > 0) 
            {
                Parallel.For(0, parallelCount, index => 
                {
                    var startIndex = index * threshold;
                    UnsafeBlockCopyRL(src + startIndex, dst + startIndex, threshold);
                });
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static void ParallelBlockCopyLR(ref byte src, ref byte dst, int count, int threshold = 512)
        {
            ParallelBlockCopyLR((byte*)src, (byte*)dst, count, threshold);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static void ParallelBlockCopyLR(byte* src, byte* dst, int count, int threshold = 512) 
        {
            var parallelCount = count / threshold;
            var singleCount = count % threshold;

            if (parallelCount > 0)
            {
                Parallel.For(0, parallelCount, index =>
                {
                    var startIndex = index * threshold;
                    UnsafeBlockCopyLR(src + startIndex, dst + startIndex, threshold);
                });
            }

            if (singleCount > 0)
            {
                var index = parallelCount * threshold;
                UnsafeBlockCopyLR(src + index, dst + index, singleCount);
            }
        }
    }
}
