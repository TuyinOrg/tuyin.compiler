using System;

namespace Toe.SPIRV.Reflection.Types
{
    internal abstract class SpirvTypeBase: Node
    {
        public static readonly TypeVoid Void;
        public static readonly TypeFloat Half;
        public static readonly TypeFloat Float;
        public static readonly TypeFloat Double;
        public static readonly TypeBool Bool;
        public static readonly TypeInt SByte;
        public static readonly TypeInt Byte;
        public static readonly TypeInt Short;
        public static readonly TypeInt UShort;
        public static readonly TypeInt Int;
        public static readonly TypeInt UInt;
        public static readonly TypeVector Vec2;
        public static readonly TypeVector Vec3;
        public static readonly TypeVector Vec4;
        public static readonly TypeVector Ivec2;
        public static readonly TypeVector Ivec3;
        public static readonly TypeVector Ivec4;
        public static readonly TypeVector Uvec2;
        public static readonly TypeVector Uvec3;
        public static readonly TypeVector Uvec4;
        public static readonly TypeVector Dvec2;
        public static readonly TypeVector Dvec3;
        public static readonly TypeVector Dvec4;
        public static readonly TypeVector Bvec2;
        public static readonly TypeVector Bvec3;
        public static readonly TypeVector Bvec4;
        public static readonly TypeMatrixDeclaration Mat2Base;
        public static readonly TypeMatrixDeclaration Mat3Base;
        public static readonly TypeMatrixDeclaration Mat4Base;
        public static readonly TypeMatrixLayout Mat2;
        public static readonly TypeMatrixLayout Mat3;
        public static readonly TypeMatrixLayout Mat4;

        static SpirvTypeBase()
        {
            Void = new TypeVoid();
            Half = new TypeFloat(16);
            Float = new TypeFloat(32);
            Double = new TypeFloat(64);
            Bool = new TypeBool();
            SByte = new TypeInt(8, true);
            Byte = new TypeInt(8, false);
            Short = new TypeInt(16, true);
            UShort = new TypeInt(16, false);
            Int = new TypeInt(32, true);
            UInt = new TypeInt(32, false);
            Vec2 = new TypeVector(Float, 2);
            Vec3 = new TypeVector(Float, 3);
            Vec4 = new TypeVector(Float, 4);
            Ivec2 = new TypeVector(Int, 2);
            Ivec3 = new TypeVector(Int, 3);
            Ivec4 = new TypeVector(Int, 4);
            Uvec2 = new TypeVector(UInt, 2);
            Uvec3 = new TypeVector(UInt, 3);
            Uvec4 = new TypeVector(UInt, 4);
            Bvec2 = new TypeVector(Bool, 2);
            Bvec3 = new TypeVector(Bool, 3);
            Bvec4 = new TypeVector(Bool, 4);
            Dvec2 = new TypeVector(Double, 2);
            Dvec3 = new TypeVector(Double, 3);
            Dvec4 = new TypeVector(Double, 4);
            Mat2Base = new TypeMatrixDeclaration(Vec2, 2);
            Mat3Base = new TypeMatrixDeclaration(Vec3, 3);
            Mat4Base = new TypeMatrixDeclaration(Vec4, 4);
            Mat2 = new TypeMatrixLayout(Mat2Base, MatrixOrientation.ColMajor, 16);
            Mat3 = new TypeMatrixLayout(Mat3Base, MatrixOrientation.ColMajor, 16);
            Mat4 = new TypeMatrixLayout(Mat4Base, MatrixOrientation.ColMajor, 16);
        }

        public virtual uint SizeInBytes => throw new NotImplementedException("SizeInBytes is not implemented at "+this.GetType().Name);

        public virtual uint Alignment => SizeInBytes;

        public abstract SpirvTypeCategory TypeCategory { get; }

        public static TypeVector ResolveVector(SpirvTypeBase componentType, uint componentCount)
        {
            switch (componentType.TypeCategory)
            {
                case SpirvTypeCategory.Float:
                {
                    var sprivFloat = (TypeFloat) componentType;
                    switch (sprivFloat.FloatType)
                    {
                        case FloatType.Float:
                            switch (componentCount)
                            {
                                case 2: return Vec2;
                                case 3: return Vec3;
                                case 4: return Vec4;
                            }
                            break;
                        case FloatType.Double:
                            switch (componentCount)
                            {
                                case 2: return Dvec2;
                                case 3: return Dvec3;
                                case 4: return Dvec4;
                            }
                            break; 
                    }
                    break;
                }
                case SpirvTypeCategory.Int:
                {
                    var sprivInt = (TypeInt)componentType;
                    switch (sprivInt.IntType)
                    {
                        case IntType.Int:
                            switch (componentCount)
                            {
                                case 2: return Ivec2;
                                case 3: return Ivec3;
                                case 4: return Ivec4;
                            }
                            break;
                        case IntType.UInt:
                            switch (componentCount)
                            {
                                case 2: return Uvec2;
                                case 3: return Uvec3;
                                case 4: return Uvec4;
                            }
                            break;
                    }
                        break;
                }
                case SpirvTypeCategory.Bool:
                {
                    switch (componentCount)
                    {
                        case 2: return Bvec2;
                        case 3: return Bvec3;
                        case 4: return Bvec4;
                    }

                    break;
                }
            }

            return new TypeVector(componentType, componentCount);
        }

        public static SpirvTypeBase ResolveMatrix(TypeVector columnType, uint columnCount)
        {
            switch (columnType.VectorType)
            {
                case VectorType.Vec2:
                    if (columnCount == 2) return Mat2;
                    break;
                case VectorType.Vec3:
                    if (columnCount == 3) return Mat3;
                    break;
                case VectorType.Vec4:
                    if (columnCount == 4) return Mat4;
                    break;
            }

            return new TypeMatrixDeclaration((TypeVector) columnType, columnCount);
        }

        public override string ToString()
        {
            return this.TypeCategory.ToString();
        }
    }
}