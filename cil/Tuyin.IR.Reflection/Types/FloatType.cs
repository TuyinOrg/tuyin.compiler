using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Tuyin.IR.Reflection.Types
{
    /*
     * [RadixType.Two]十进制小数的二进制表示：
     * 参考博客：https://blog.csdn.net/zhengyanan815/article/details/78550073
     * 小数点后面：4个位占用一个数字【十进制9就是1001】
     * 符号位(Sign) : 0代表正，1代表为负【占1位】
     * 指数位（Exponent）:用于存储科学计数法中的指数数据，并且采用移位存储【占8位】
     * 尾数部分（Mantissa）：尾数部分【占23位】
     * 单精度float:N共32位，其中S占1位，E占8位，M占23位。因此小数点后最多精确到23/4=6位 
     * 双精度double:N共32位，其中S占1位，E占11位，M占52位。因此小数点后最多精确到52/4=13位 
     * 十进制小数的二进制表示：【法则--整数部分：除基取余，逆序拼接。小数部分：乘基取整，顺序拼接】
     * 整数部分：除以2，取出余数，商继续除以2，直到得到0为止，将取出的余数逆序。可以使用栈Stack
     * 小数部分：乘以2，然后取出整数部分，将剩下的小数部分继续乘以2，然后再取整数部分，一直取到小数部分为零为止。如果永远不为零，则按要求保留足够位数的小数，最后一位做0舍1入。将取出的整数顺序排列。可以使用队列Queue
     */
    public unsafe class FloatType : Type
    {
        public override string Name { get; }

        public enum RadixType : byte
        {
            Two = 2,
            Ten = 10,
        }

        public static readonly FloatType Binary16 = new FloatType(RadixType.Two, 16, 15, 1, 10, 5, 0, 10, "Half");
        public static readonly FloatType Binary32 = new FloatType(RadixType.Two, 32, 31, 1, 22, 8, 0, 22, "Single");
        public static readonly FloatType Binary64 = new FloatType(RadixType.Two, 64, 63, 1, 52, 11, 0, 52, "Double");
        public static readonly FloatType Binary128 = new FloatType(RadixType.Two, 128, 127, 1, 112, 15, 0, 112, "Quadruple"); // fp128
        public static readonly FloatType Binary256 = new FloatType(RadixType.Two, 256, 255, 1, 236, 19, 0, 236, "Octuple");

        public readonly RadixType Radix;
        public readonly int ByteSize, BitSize;
        public readonly int SignOffset, SignLength;
        public readonly int ExponentOffset, ExponentLength;
        public readonly int MantissaOffset, MantissaLength;

        public readonly byte[] SignMask;
        public readonly byte[] ExponentMask;
        public readonly byte[] MantissaMask;

        public override uint BitsSize => (uint)BitSize;

        public int Numerator { get; }

        public int Denominator { get; }

        public FloatType(int numerator, int denominator)
        {
            Radix = RadixType.Two;
            Numerator = numerator;
            Denominator = denominator;
            Name = $"f{Numerator}.{Denominator}";

            throw new NotImplementedException("not implemente num/den to float type data.");
        }

        public FloatType(RadixType radix, int size,
            int signOffset, int signLength,
            int exponentOffset, int exponentLength,
            int mantissaOffset, int mantissaLength,
            string name)
        {
            Radix = radix;
            ByteSize = size;
            BitSize = size * 8;
            SignOffset = signOffset;
            SignLength = signLength;
            ExponentOffset = exponentOffset;
            ExponentLength = exponentLength;
            MantissaOffset = mantissaOffset;
            MantissaLength = mantissaLength;
            Name = name;

            // Validate arguments
            ThrowWhenOutOfRange(signOffset, signLength, BitSize);
            ThrowWhenOutOfRange(exponentOffset, exponentLength, BitSize);
            ThrowWhenOutOfRange(mantissaOffset, mantissaLength, BitSize);

            // Comute masks
            SignMask = new byte[size];
            ComputeMask(SignMask, signOffset, signLength);
            ExponentMask = new byte[size];
            ComputeMask(ExponentMask, exponentOffset, exponentLength);
            MantissaMask = new byte[size];
            ComputeMask(MantissaMask, mantissaOffset, mantissaLength);
        }

        private static void ThrowWhenOutOfRange(int bitOffset, int bitCount, int bitLength)
        {
            if (bitOffset < 0)
                throw new ArgumentOutOfRangeException(nameof(bitOffset));
            if (bitCount < 0)
                throw new ArgumentOutOfRangeException(nameof(bitCount));
            if (bitOffset + bitCount > bitLength)
                throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// Computes the bit mask for the storage array.
        /// </summary>
        /// <param name="storage">The storage array representing the bits of the floating point number.</param>
        /// <param name="bitOffset">Number of bits from the 0th bit to the 1st bit to set.</param>
        /// <param name="bitCount">Number of bits from the offset bit to set.</param>
        private static void ComputeMask(in Span<byte> storage, int bitOffset, int bitCount)
        {
            int bitLength = storage.Length * 8;
            if (bitCount > bitLength)
                bitCount = bitLength;
            if (bitCount == bitLength)
            {
                if (bitOffset < 0)
                {
                    // (1 << (count + offset))- 1;
                    OneLeftShfitByNMinusOne(storage, bitCount + bitOffset);
                }
                else //if (offset >= 0)
                {
                    // 0xFF... << offset
                    FillMax(storage);
                    LeftShift(storage, bitOffset);
                }
            }
            else if (bitOffset < 0)
            {
                int totalBits = bitCount - bitOffset;
                if (totalBits <= 0)
                {
                    // Leave empty, nothing to fill: out of range
                }
                else
                {
                    // (1 << total) - 1
                    OneLeftShfitByNMinusOne(storage, totalBits);
                }
            }
            else
            {
                // ((1 << count) - 1) << offset
                OneLeftShfitByNMinusOne(storage, bitCount);
                LeftShift(storage, bitOffset);
            }
        }

        /// <summary>
        /// Performs following integer arithmetic operation on byte arrays: storage = storage << n.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void LeftShift(in Span<byte> storage, int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n));
            fixed (byte* storagePtr = &MemoryMarshal.GetReference(storage))
            {
                switch (storage.Length)
                {
                    case 1:
                        *storagePtr <<= 1;
                        break;
                    case 2:
                        *(ushort*)storagePtr <<= 1;
                        break;
                    case 4:
                        *(uint*)storagePtr <<= 1;
                        break;
                    case 8:
                        *(ulong*)storagePtr <<= 1;
                        break;
                    default:
                        byte carryMask = 0x00;
                        for (int i = 0; i < storage.Length; i++)
                        {
                            // Shift and apply carry bit
                            byte temp = (byte)((storagePtr[i] << 1 | carryMask) & 0xFF);
                            // Carry HI bit to next LO
                            carryMask = (byte)(storagePtr[i] >> 7 & 0xFF);
                            storagePtr[i] = temp;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Performs following integer arithmetic operation on byte arrays: (1 << n) - 1.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void OneLeftShfitByNMinusOne(in Span<byte> storage, int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n));
            fixed (byte* storagePtr = &MemoryMarshal.GetReference(storage))
            {
                switch (storage.Length)
                {
                    case 1:
                        *storagePtr = (byte)((1 << n) - 1 & 0xFF);
                        break;
                    case 2:
                        *(ushort*)storagePtr = (ushort)((1 << n) - 1 & 0xFFFF);
                        break;
                    case 4:
                        *(uint*)storagePtr = (1u << n) - 1 & 0xFFFFFFFF;
                        break;
                    case 8:
                        *(ulong*)storagePtr = (1ul << n) - 1 & 0xFFFFFFFFFFFFFFFF;
                        break;
                    default:
                        int bytes = n / 8,
                            remainder = n % 8;
                        for (int i = 0; i < bytes; i++)
                            storagePtr[i] = 0xFF;
                        if (remainder != 0)
                            storagePtr[bytes + 1] = (byte)((1 << remainder) - 1);
                        break;
                }
            }
        }
        /// <summary>
        /// Sets all bytes to 0xFF.
        /// </summary>
        /// <param name="storage"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void FillMax(in Span<byte> storage)
        {
            fixed (byte* storagePtr = &MemoryMarshal.GetReference(storage))
            {
                switch (storage.Length)
                {
                    case 1:
                        *storagePtr = 0xFF;
                        break;
                    case 2:
                        *(ushort*)storagePtr = 0xFFFF;
                        break;
                    case 4:
                        *(uint*)storagePtr = 0xFFFFFFFF;
                        break;
                    case 8:
                        *(ulong*)storagePtr = 0xFFFFFFFFFFFFFFFF;
                        break;
                    default:
                        for (int i = 0; i < storage.Length; i++)
                            storagePtr[i] = 0xFF;
                        break;
                }
            }
        }
    }

}
