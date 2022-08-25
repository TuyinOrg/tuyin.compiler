using System;

namespace Tuyin.IR.Reflection.Types
{
    public class PrimitiveType : Type, IEquatable<PrimitiveType>
    {
        public static readonly PrimitiveType Float32 = new PrimitiveType(PrimitiveTypes.f32);
        public static readonly PrimitiveType Float64 = new PrimitiveType(PrimitiveTypes.f64);
        public static readonly PrimitiveType Int8 = new PrimitiveType(PrimitiveTypes.i8);
        public static readonly PrimitiveType Uint8 = new PrimitiveType(PrimitiveTypes.u8);
        public static readonly PrimitiveType Int16 = new PrimitiveType(PrimitiveTypes.i16);
        public static readonly PrimitiveType Uint16 = new PrimitiveType(PrimitiveTypes.u16);
        public static readonly PrimitiveType Int32 = new PrimitiveType(PrimitiveTypes.i32);
        public static readonly PrimitiveType Uint32 = new PrimitiveType(PrimitiveTypes.u32);
        public static readonly PrimitiveType Int64 = new PrimitiveType(PrimitiveTypes.i64);
        public static readonly PrimitiveType Uint64 = new PrimitiveType(PrimitiveTypes.u64);
        public static readonly PrimitiveType Void = new PrimitiveType(PrimitiveTypes.@void);
        public static readonly PrimitiveType Boolean = new PrimitiveType(PrimitiveTypes.@bool);
        public static readonly PrimitiveType Anonymous = new PrimitiveType(PrimitiveTypes.@bool);
        public static readonly PrimitiveType Char = new PrimitiveType(PrimitiveTypes.@bool);
        public static readonly PrimitiveType String = new PrimitiveType(PrimitiveTypes.@bool);

        internal readonly PrimitiveTypes Type;

        internal PrimitiveType(PrimitiveTypes type)
        {
            Type = type;
        }

        public override string Name => Type switch
        {
            PrimitiveTypes.i8 => "i8",
            PrimitiveTypes.i16 => "i16",
            PrimitiveTypes.i32 => "i32",
            PrimitiveTypes.i64 => "i64",
            PrimitiveTypes.u8 => "u8",
            PrimitiveTypes.u16 => "u16",
            PrimitiveTypes.u32 => "u32",
            PrimitiveTypes.u64 => "u64",
            PrimitiveTypes.f32 => "f32",
            PrimitiveTypes.f64 => "f64",
            PrimitiveTypes.@bool => "bool",
            PrimitiveTypes.@void => "void",
            _ => throw new NotImplementedException()
        };

        public override uint BitsSize => Type switch
        {
            PrimitiveTypes.i8 => 8,
            PrimitiveTypes.i16 => 16,
            PrimitiveTypes.i32 => 32,
            PrimitiveTypes.i64 => 64,
            PrimitiveTypes.u8 => 8,
            PrimitiveTypes.u16 => 16,
            PrimitiveTypes.u32 => 32,
            PrimitiveTypes.u64 => 64,
            PrimitiveTypes.f32 => 512,
            PrimitiveTypes.f64 => 1024,
            PrimitiveTypes.@bool => 1,
            PrimitiveTypes.@void => 32,
            _ => 0
        };

        public int ComparerValue => Type switch
        {
            PrimitiveTypes.i8 => 8,
            PrimitiveTypes.i16 => 16,
            PrimitiveTypes.i32 => 32,
            PrimitiveTypes.i64 => 64,
            PrimitiveTypes.u8 => 8 - 1,
            PrimitiveTypes.u16 => 16 - 1,
            PrimitiveTypes.u32 => 32 - 1,
            PrimitiveTypes.u64 => 64 - 1,
            PrimitiveTypes.f32 => 512,
            PrimitiveTypes.f64 => 1024,
            PrimitiveTypes.@bool => 1,
            PrimitiveTypes.@void => 32,
            _ => 0
        };

        public override bool IsPrimitive => true;

        public override bool IsFloatingPoint => 
            Type == PrimitiveTypes.f32 || 
            Type == PrimitiveTypes.f64;

        public override bool IsIntegral =>
            IsSigned || 
            IsUnsigned;

        public override bool IsSigned =>
            Type == PrimitiveTypes.i8 ||
            Type == PrimitiveTypes.i16 ||
            Type == PrimitiveTypes.i32 ||
            Type == PrimitiveTypes.i64;

        public override bool IsUnsigned =>
            Type == PrimitiveTypes.u8 ||
            Type == PrimitiveTypes.u16 ||
            Type == PrimitiveTypes.u32 ||
            Type == PrimitiveTypes.u64;

        public override bool Equals(object obj)
        {
            if (obj is PrimitiveType other)
            {
                return this.Type == other.Type;
            }
            return false;
        }

        public static bool operator ==(PrimitiveType a, PrimitiveType b)
        {
            return a.Type == b.Type;
        }

        public static bool operator !=(PrimitiveType a, PrimitiveType b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return Type switch
            {
                PrimitiveTypes.i8 => "i8",
                PrimitiveTypes.i16 => "i16",
                PrimitiveTypes.i32 => "i32",
                PrimitiveTypes.i64 => "i64",
                PrimitiveTypes.u8 => "i8",
                PrimitiveTypes.u16 => "i16",
                PrimitiveTypes.u32 => "i32",
                PrimitiveTypes.u64 => "i64",
                PrimitiveTypes.f32 => "float",
                PrimitiveTypes.f64 => "double",
                PrimitiveTypes.@bool => "i1",
                PrimitiveTypes.@void => "void",
                PrimitiveTypes.vararg => "...",
                _ => throw new Exception("Unable to parse invalid primitive type. Type: " + Type.ToString()),
            };
        }

        public bool Equals(PrimitiveType other)
        {
            return other is not null &&
                   Type == other.Type &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Name);
        }
    }

}
