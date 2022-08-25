using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeVector : SpirvTypeBase
    {
        internal TypeVector(SpirvTypeBase componentType, uint componentCount)
        {
            ComponentCount = componentCount;
            ComponentType = componentType;
            VectorType = GetType(ComponentType, ComponentCount);
        }

        public override uint Alignment
        {
            get
            {
                switch (ComponentCount)
                {
                    case 3:
                        return ComponentType.SizeInBytes * 4;
                    case 2:
                    case 4:
                    default:
                        return ComponentType.SizeInBytes * ComponentCount;
                }
            }
        }

        public override uint SizeInBytes => ComponentType.SizeInBytes * ComponentCount;

        public uint ComponentCount { get; }

        public SpirvTypeBase ComponentType { get; }

        public VectorType VectorType { get; }

        public static VectorType GetType(SpirvTypeBase componentType, uint componentCount)
        {
            switch (componentType.TypeCategory)
            {
                case SpirvTypeCategory.Float:
                {
                    var floatType = (TypeFloat)componentType;
                    switch (floatType.FloatType)
                    {
                        case FloatType.Float:
                            switch (componentCount)
                            {
                                case 2:
                                    return VectorType.Vec2;
                                case 3:
                                    return VectorType.Vec3;
                                case 4:
                                    return VectorType.Vec4;
                            }
                            break;
                        case FloatType.Double:
                            switch (componentCount)
                            {
                                case 2:
                                    return VectorType.Dvec2;
                                case 3:
                                    return VectorType.Dvec3;
                                case 4:
                                    return VectorType.Dvec4;
                            }
                            break;
                    }
                    break;
                }
                case SpirvTypeCategory.Int:
                {
                    var intType = (TypeInt)componentType;
                    switch (intType.IntType)
                    {
                        case IntType.Int:
                            switch (componentCount)
                            {
                                case 2:
                                    return VectorType.Ivec2;
                                case 3:
                                    return VectorType.Ivec3;
                                case 4:
                                    return VectorType.Ivec4;
                            }
                            break;
                        case IntType.UInt:
                            switch (componentCount)
                            {
                                case 2:
                                    return VectorType.Uvec2;
                                case 3:
                                    return VectorType.Uvec3;
                                case 4:
                                    return VectorType.Uvec4;
                            }
                            break;
                        }
                    break;
                }
                case SpirvTypeCategory.Bool:
                {
                    switch (componentCount)
                    {
                        case 2:
                            return VectorType.Bvec2;
                        case 3:
                            return VectorType.Bvec3;
                        case 4:
                            return VectorType.Bvec4;
                    }

                    break;
                }
            }

            return VectorType.Unknown;
        }

        public override string ToString()
        {
            switch (VectorType)
            {
                case VectorType.Bvec2: return "bvec2";
                case VectorType.Bvec3: return "bvec3";
                case VectorType.Bvec4: return "bvec4";
                case VectorType.Ivec2: return "ivec2";
                case VectorType.Ivec3: return "ivec3";
                case VectorType.Ivec4: return "ivec4";
                case VectorType.Uvec2: return "uvec2";
                case VectorType.Uvec3: return "uvec3";
                case VectorType.Uvec4: return "uvec4";
                case VectorType.Dvec2: return "dvec2";
                case VectorType.Dvec3: return "dvec3";
                case VectorType.Dvec4: return "dvec4";
                case VectorType.Vec2: return "vec2";
                case VectorType.Vec3: return "vec3";
                case VectorType.Vec4: return "vec4";
            }

            return ComponentType + "vec" + ComponentCount;
        }
    }
}