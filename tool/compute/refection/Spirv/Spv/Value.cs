using System;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;

namespace Toe.SPIRV.Spv
{
    internal class Value
    {
        public Value(byte[] bytes, TypeInstruction type)
        {
            Bytes = bytes;
            Type = type;
        }

        public byte[] Bytes { get; }

        public TypeInstruction Type { get; }

        public virtual uint GetWordCount()
        {
            return ((uint)Bytes.Length+3)/4;
        }

        public virtual void Write(WordWriter writer)
        {
            writer.Write(Bytes);
        }

        public override string ToString()
        {
            if (Type.OpCode == Op.OpTypeBool) return ToBool().ToString();
            if (Type.OpCode == Op.OpTypeFloat)
            {
                if (Type.SizeInWords == 1) return ToFloat().ToString();
                if (Type.SizeInWords == 2) return ToDouble().ToString();
            }
            else if (Type.OpCode == Op.OpTypeInt)
            {
                var op = (OpTypeInt) Type;
                if (op.Signedness == 0)
                {
                    if (op.Width == 8) return ToInt8().ToString();
                    if (op.Width == 16) return ToInt16().ToString();
                    if (op.Width == 32) return ToInt32().ToString();
                    if (op.Width == 64) return ToInt64().ToString();
                }
                else
                {
                    if (op.Width == 8) return ToUInt8().ToString();
                    if (op.Width == 16) return ToUInt16().ToString();
                    if (op.Width == 32) return ToUInt32().ToString();
                    if (op.Width == 64) return ToUInt64().ToString();
                }
            }
            else if (Type.OpCode == Op.OpTypeVector)
            {
                var op = (OpTypeVector) Type;
            }

            return Type.OpCode + "(" + BitConverter.ToString(Bytes) + ")";
        }

        public float ToFloat(uint offset = 0)
        {
            return BitConverter.ToSingle(Bytes, (int) offset);
        }

        public bool ToBool(uint offset = 0)
        {
            return BitConverter.ToBoolean(Bytes, (int) offset);
        }

        public double ToDouble(uint offset = 0)
        {
            return BitConverter.ToDouble(Bytes, (int) offset);
        }

        public long ToInt64(uint offset = 0)
        {
            return BitConverter.ToInt64(Bytes, (int) offset);
        }

        public int ToInt32(uint offset = 0)
        {
            return BitConverter.ToInt32(Bytes, (int) offset);
        }

        public short ToInt16(uint offset = 0)
        {
            return BitConverter.ToInt16(Bytes, (int) offset);
        }

        public sbyte ToInt8(uint offset = 0)
        {
            return (sbyte) Bytes[(int) offset];
        }

        public ulong ToUInt64(uint offset = 0)
        {
            return BitConverter.ToUInt64(Bytes, (int) offset);
        }

        public uint ToUInt32(uint offset = 0)
        {
            return BitConverter.ToUInt32(Bytes, (int) offset);
        }

        public ushort ToUInt16(uint offset = 0)
        {
            return BitConverter.ToUInt16(Bytes, (int) offset);
        }

        public byte ToUInt8(uint offset = 0)
        {
            return Bytes[(int) offset];
        }
    }
}