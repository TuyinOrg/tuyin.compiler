using System;
using System.Runtime.InteropServices;

namespace Tuyin.IR.Analysis.Data
{
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 8)]
    public struct Union8
    {
        [FieldOffset(0)]
        public bool Boolean0;

        [FieldOffset(0)]
        public byte Byte0;

        [FieldOffset(1)]
        public byte Byte1;

        [FieldOffset(2)]
        public byte Byte2;

        [FieldOffset(3)]
        public byte Byte3;

        [FieldOffset(4)]
        public byte Byte4;

        [FieldOffset(5)]
        public byte Byte5;

        [FieldOffset(6)]
        public byte Byte6;

        [FieldOffset(7)]
        public byte Byte7;

        [FieldOffset(0)]
        public sbyte SByte0;

        [FieldOffset(1)]
        public sbyte SByte1;

        [FieldOffset(2)]
        public sbyte SByte2;

        [FieldOffset(3)]
        public sbyte SByte3;

        [FieldOffset(4)]
        public sbyte SByte4;

        [FieldOffset(5)]
        public sbyte SByte5;

        [FieldOffset(6)]
        public sbyte SByte6;

        [FieldOffset(7)]
        public sbyte SByte7;

        [FieldOffset(0)]
        public short Short0;

        [FieldOffset(2)]
        public short Short1;

        [FieldOffset(4)]
        public short Short2;

        [FieldOffset(6)]
        public short Short3;

        [FieldOffset(0)]
        public ushort Ushort0;

        [FieldOffset(2)]
        public ushort Ushort1;

        [FieldOffset(4)]
        public ushort Ushort2;

        [FieldOffset(6)]
        public ushort Ushort3;

        [FieldOffset(0)]
        public int Int0;

        [FieldOffset(4)]
        public int Int1;

        [FieldOffset(0)]
        public uint Uint0;

        [FieldOffset(4)]
        public uint Uint1;

        [FieldOffset(0)]
        public long Long0;

        [FieldOffset(0)]
        public ulong Ulong0;

        [FieldOffset(0)]
        public float Single0;

        [FieldOffset(1)]
        public float Single1;

        [FieldOffset(0)]
        public double Double0;

        [FieldOffset(0)]
        public IntPtr IntPtr0;

        [FieldOffset(4)]
        public IntPtr IntPtr1;

        public static implicit operator Union8(bool v) => new Union8() { Boolean0 = v };
        public static implicit operator Union8(sbyte v) => new Union8() { SByte0 = v };
        public static implicit operator Union8(byte v) => new Union8() { Byte0 = v };
        public static implicit operator Union8(ushort v) => new Union8() { Ushort0 = v };
        public static implicit operator Union8(short v) => new Union8() { Short0 = v };
        public static implicit operator Union8(uint v) => new Union8() { Uint0 = v };
        public static implicit operator Union8(int v) => new Union8() { Int0 = v };
        public static implicit operator Union8(ulong v) => new Union8() { Ulong0 = v };
        public static implicit operator Union8(long v) => new Union8() { Long0 = v };
        public static implicit operator Union8(float v) => new Union8() { Single0 = v };
        public static implicit operator Union8(double v) => new Union8() { Double0 = v };
    }
}
