using System;
using System.Runtime.InteropServices;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Nodes;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Operands
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct NumberLiteral : IEquatable<NumberLiteral>
    {
        public bool Equals(NumberLiteral other)
        {
            return _type == other._type && _doubleValue.Equals(other._doubleValue);
        }

        public override bool Equals(object obj)
        {
            return obj is NumberLiteral other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) _type * 397) ^ _doubleValue.GetHashCode();
            }
        }

        public static bool operator ==(NumberLiteral left, NumberLiteral right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(NumberLiteral left, NumberLiteral right)
        {
            return !left.Equals(right);
        }

        public static readonly NumberLiteral Undef;

        private const int DataOffset = 0;
        private const int TypeOffset = sizeof(double);

        private enum ValueType
        {
            Zero,
            Float,
            Double,
            Int,
            UInt
        }

        [FieldOffset(TypeOffset)] private ValueType _type;
        [FieldOffset(DataOffset)] private double _doubleValue;
        [FieldOffset(DataOffset)] private float _floatValue;
        [FieldOffset(DataOffset)] private int _intValue;
        [FieldOffset(DataOffset)] private uint _uintValue;

        public SpirvTypeBase Type
        {
            get
            {
                switch (_type)
                {
                    case ValueType.Zero:
                        return null;
                    case ValueType.Float:
                        return SpirvTypeBase.Float;
                    case ValueType.Double:
                        return SpirvTypeBase.Double;
                    case ValueType.Int:
                        return SpirvTypeBase.Int;
                    case ValueType.UInt:
                        return SpirvTypeBase.UInt;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public override string ToString()
        {
            switch (_type)
            {
                case ValueType.Zero:
                    return "0";
                case ValueType.Float:
                    return $"{_floatValue:G17}";
                case ValueType.Double:
                    return $"{_doubleValue:G17}";
                case ValueType.Int:
                    return $"{_intValue}";
                case ValueType.UInt:
                    return $"{_uintValue}";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static implicit operator NumberLiteral(double value)
        {
            var res = new NumberLiteral();
            res._type = ValueType.Double;
            res._doubleValue = value;
            return res;
        }

        public static implicit operator NumberLiteral(float value)
        {
            var res = new NumberLiteral();
            res._type = ValueType.Float;
            res._floatValue = value;
            return res;
        }

        public static implicit operator NumberLiteral(int value)
        {
            var res = new NumberLiteral();
            res._type = ValueType.Int;
            res._intValue = value;
            return res;
        }

        public static implicit operator NumberLiteral(uint value)
        {
            var res = new NumberLiteral();
            res._type = ValueType.UInt;
            res._uintValue = value;
            return res;
        }

        public static implicit operator NumberLiteral(Spv.LiteralContextDependentNumber value)
        {
            var val = value?.Value;
            if (val?.Type == null) return NumberLiteral.Undef;
            var type = val.Type;
            switch (type.OpCode)
            {
                case Op.OpTypeInt:
                    var opTypeInt = (OpTypeInt) type;
                    if (opTypeInt.Signedness == 0)
                    {
                        switch (opTypeInt.Width)
                        {
                            case 32: return val.ToInt32();
                        }
                    }
                    else
                    {
                        switch (opTypeInt.Width)
                        {
                            case 32: return val.ToUInt32();
                        }
                    }
                    throw new NotImplementedException(type+" not implemented yet.");
                case Op.OpTypeFloat:
                    var opTypeFloat = (OpTypeFloat)type;
                    switch (opTypeFloat.Width)
                    {
                        case 32: return val.ToFloat();
                        case 64: return val.ToDouble();
                    }
                    throw new NotImplementedException(type + " not implemented yet.");
                default:
                    throw new NotImplementedException(type + " not implemented yet.");
            }
        }

        public static implicit operator double(NumberLiteral value)
        {
            switch (value._type)
            {
                case ValueType.Zero:
                    return 0;
                case ValueType.Float:
                    return value._floatValue;
                case ValueType.Double:
                    return value._doubleValue;
                case ValueType.Int:
                    return value._intValue;
                case ValueType.UInt:
                    return value._uintValue;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static implicit operator float(NumberLiteral value)
        {
            switch (value._type)
            {
                case ValueType.Zero:
                    return 0;
                case ValueType.Float:
                    return value._floatValue;
                case ValueType.Double:
                    return (float) value._doubleValue;
                case ValueType.Int:
                    return value._intValue;
                case ValueType.UInt:
                    return value._uintValue;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public static implicit operator Constant(NumberLiteral value)
        {
            return new Constant(value.Type, value);
        }
        public static implicit operator int(NumberLiteral value)
        {
            switch (value._type)
            {
                case ValueType.Zero:
                    return 0;
                case ValueType.Float:
                    return (int) value._floatValue;
                case ValueType.Double:
                    return (int) value._doubleValue;
                case ValueType.Int:
                    return value._intValue;
                case ValueType.UInt:
                    return (int) value._uintValue;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static implicit operator uint(NumberLiteral value)
        {
            switch (value._type)
            {
                case ValueType.Zero:
                    return 0;
                case ValueType.Float:
                    return (uint) value._floatValue;
                case ValueType.Double:
                    return (uint) value._doubleValue;
                case ValueType.Int:
                    return (uint) value._intValue;
                case ValueType.UInt:
                    return value._uintValue;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public static implicit operator byte[](NumberLiteral value)
        {
            switch (value._type)
            {
                case ValueType.Zero:
                    return null;
                case ValueType.Float:
                    return BitConverter.GetBytes(value._floatValue);
                case ValueType.Double:
                    return BitConverter.GetBytes(value._doubleValue);
                case ValueType.Int:
                    return BitConverter.GetBytes(value._intValue);
                case ValueType.UInt:
                    return BitConverter.GetBytes(value._uintValue);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public LiteralContextDependentNumber ToLiteral(Func<Node,Instruction> typeConverter)
        {
            return new LiteralContextDependentNumber(new Value(this, (TypeInstruction)typeConverter(Type)));
        }
    }
}