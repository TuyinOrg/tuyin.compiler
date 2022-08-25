using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Toe.SPIRV;
using Toe.SPIRV.Reflection;
using Toe.SPIRV.Reflection.Nodes;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;
using static compute.ShaderInterfaceType;
using static Toe.SPIRV.Spv.ExecutionMode;

namespace compute.reflection
{
    public static class ShaderGenerater
    {
        public static Shader CreateShader(string fileName) 
        {
            return CreateShader(File.ReadAllBytes(fileName));
        }

        public static Shader CreateShader(byte[] shaderBytes) 
        {
            var shader = Toe.SPIRV.Shader.Parse(shaderBytes);
            var reflect = new ShaderReflection(shader);
            
            var structs = reflect.Structures
             .Select(_ => ConvertStruct(_))
             .ToArray();

            var entryDict = reflect.ExecutionModeInstructions.ToDictionary(x => x.DebugName);

            // 生成入口信息
            var entryPoints = new ShaderEntryPoint[reflect.EntryPointInstructions.Count];
            for (int i = 0; i < reflect.EntryPointInstructions.Count; i++)
            {
                var entryPoint = reflect.EntryPointInstructions[i];

                var size = new ShaderEntryPointSize(1, 1, 1);
                var execution = entryDict.ContainsKey(entryPoint.Name) ? entryDict[entryPoint.Name] : null;
                if (execution != null)
                {
                    switch (execution.Mode)
                    {
                        case LocalSizeHintIdImpl localSizeHintId:
                        case LocalSizeIdImpl localSizeId:
                            throw new NotSupportedException($"{Settings.Verstion} not support execution mode '{execution}'.");
                        case LocalSizeHintImpl localSizeHint:
                            size = new ShaderEntryPointSize((int)localSizeHint.xsize, (int)localSizeHint.ysize, (int)localSizeHint.zsize);
                            break;
                        case LocalSizeImpl localSize:
                            size = new ShaderEntryPointSize((int)localSize.xsize, (int)localSize.ysize, (int)localSize.zsize);
                            break;
                    }
                }

                var collector = new InterfaceCollector();
                var interfaces = collector.Visit(entryPoint).Select(x =>
                    new Interface(
                        x.DebugName,
                        ConvertType(x.ResultType),
                        (InterfaceClass)x.StorageClass.Value,
                        ResolveSprivType(x.ResultType),
                        (int)x.BindingPoint,
                        (int)x.DescriptorSet,
                        (int)x.Location)).ToArray();

                entryPoints[i] = new ShaderEntryPoint((ShaderExecutionMode)entryPoint.ExecutionModel.Value, size, entryPoint.Name, interfaces);
            }

            return new Shader(shaderBytes, structs, entryPoints);
        }

        class InterfaceCollector : NodeVisitor
        {
            public IList<Variable> Inputs { get; } = new PrintableList<Variable>();
            public IList<Variable> Outputs { get; } = new PrintableList<Variable>();
            public IList<Variable> Uniforms { get; } = new PrintableList<Variable>();
            public IList<Variable> UniformConstants { get; } = new PrintableList<Variable>();

            public IEnumerable<Variable> Visit(EntryPoint point)
            {
                base.Visit(point);
                return GetInterfaces();
            }

            protected override void VisitVariable(Variable node)
            {
                switch (node.StorageClass.Value)
                {
                    case StorageClass.Enumerant.Input:
                        Inputs.Add(node);
                        break;
                    case StorageClass.Enumerant.Output:
                        Outputs.Add(node);
                        break;
                    case StorageClass.Enumerant.Uniform:
                        Uniforms.Add(node);
                        break;
                    case StorageClass.Enumerant.UniformConstant:
                        UniformConstants.Add(node);
                        break;
                }

                base.VisitVariable(node);
            }

            public IEnumerable<Variable> GetInterfaces()
            {
                return Inputs.Concat(Outputs).Concat(Uniforms).Concat(UniformConstants);
            }
        }

        static InterfaceType ResolveSprivType(SpirvTypeBase type)
        {
            switch (type.OpCode)
            {
                case Op.OpTypeSampler: return InterfaceType.OpTypeSampler;
                case Op.OpTypeImage:
                    {
                        var img = type as TypeImage;
                        switch (img.Dim.Value)
                        {
                            case Dim.Enumerant.Dim1D:
                            case Dim.Enumerant.Dim2D:
                            case Dim.Enumerant.Dim3D:
                            case Dim.Enumerant.Cube:
                            case Dim.Enumerant.Rect:
                                switch (img.Sampled)
                                {
                                    case 1: return InterfaceType.OpTypeImageS1;
                                    case 2: return InterfaceType.OpTypeImageS2;
                                }
                                break;
                            case Dim.Enumerant.Buffer:
                                switch (img.Sampled)
                                {
                                    case 1: return InterfaceType.OpTypeImageS1DimBuffer;
                                    case 2: return InterfaceType.OpTypeImageS2DimBuffer;
                                }
                                break;
                            case Dim.Enumerant.SubpassData:
                                switch (img.Sampled)
                                {
                                    case 2: return InterfaceType.OpTypeImageS2DimSubpassData;
                                }
                                break;
                        }

                        break;
                    }
                case Op.OpSampledImage:
                    {
                        var img = type as TypeSampledImage;
                        var imgOp = ResolveSprivType(img.ImageType);
                        if (imgOp == InterfaceType.OpTypeImageS1)
                            return InterfaceType.OpTypeSampledImage;

                        break;
                    }
                case Op.OpTypeStruct:
                    return InterfaceType.OpTypeStruct;
                case Op.OpTypeAccelerationStructureNV:
                    return InterfaceType.OpTypeAccelerationStructureKHR;
            }

            return InterfaceType.Unknown;
        }

        internal static ShaderInterfaceType ConvertType(SpirvTypeBase type)
        {
            switch (type.TypeCategory)
            {
                case SpirvTypeCategory.Void: return ShaderInterfaceType.Void;
                case SpirvTypeCategory.Array: return ConvertArrayField(string.Empty, 0, type as TypeArray).Type;
                case SpirvTypeCategory.Float: return ConvertFloatField(string.Empty, 0, type as TypeFloat).Type;
                case SpirvTypeCategory.Int: return ConvertIntField(string.Empty, 0, type as TypeInt).Type;
                case SpirvTypeCategory.Vector: return ConvertVectorField(string.Empty, 0, type as TypeVector).Type;
                case SpirvTypeCategory.Matrix: return ConvertMatrixField(string.Empty, 0, type as TypeMatrix).Type;
                case SpirvTypeCategory.Struct: return ConvertStruct(type as TypeStruct);
                case SpirvTypeCategory.Pointer: return new Pointer(ConvertType((type as TypePointer).Type));
            }

            return new ShaderInterfaceType(Nameof(type), Sizeof(type));
        }

        private static ShaderInterfaceType ConvertStruct(TypeStruct structure)
        {
            var fields = new Field[structure.Fields.Count];
            for (var memberIndex = 0; memberIndex < structure.Fields.Count; memberIndex++)
            {
                var field = structure.Fields[memberIndex];
                fields[memberIndex] = ConvertStructField(field.Name, field.ByteOffset.Value, field.Type);
            }
            return new ShaderInterfaceType(Nameof(structure), Sizeof(structure), fields);
        }

        private static Field ConvertStructField(string fieldName, uint byteOffset, SpirvTypeBase fieldType)
        {
            switch (fieldType.TypeCategory)
            {
                case SpirvTypeCategory.Void: return new Field(ShaderInterfaceType.Void, fieldName, byteOffset);
                case SpirvTypeCategory.Array: return ConvertArrayField(fieldName, byteOffset, fieldType as TypeArray);
                case SpirvTypeCategory.Float: return ConvertFloatField(fieldName, byteOffset, fieldType as TypeFloat);
                case SpirvTypeCategory.Int: return ConvertIntField(fieldName, byteOffset, fieldType as TypeInt);
                case SpirvTypeCategory.Vector: return ConvertVectorField(fieldName, byteOffset, fieldType as TypeVector);
                case SpirvTypeCategory.Matrix: return ConvertMatrixField(fieldName, byteOffset, fieldType as TypeMatrix);
                case SpirvTypeCategory.Struct: return new Field(ConvertStruct(fieldType as TypeStruct), fieldName, byteOffset);
                case SpirvTypeCategory.Pointer: return ConvertPointerField(fieldName, byteOffset, fieldType as TypePointer);
            }

            return new Field(new ShaderInterfaceType(Nameof(fieldType), Sizeof(fieldType)), fieldName, byteOffset);
        }

        private static Field ConvertMatrixField(string fieldName, uint byteOffset, TypeMatrix fieldType)
        {
            var fields = new Field[fieldType.ColumnCount];
            for (uint i = 0; i < fieldType.ColumnCount; ++i)
            {
                if (fieldType.MatrixOrientation == MatrixOrientation.ColMajor)
                    fields[i] = ConvertStructField(fieldName + "Col" + i, byteOffset + i * fieldType.ColumnStride, fieldType.ColumnType);
                else
                    fields[i] = ConvertStructField(fieldName + "Row" + i, byteOffset + i * fieldType.ColumnStride, fieldType.ColumnType);
            }

            return new Field(new ShaderInterfaceType(Nameof(fieldType), Sizeof(fieldType), fields), fieldName, byteOffset);
        }

        private static Field ConvertVectorField(string fieldName, uint byteOffset, TypeVector fieldType)
        {
            var fields = new Field[fieldType.ComponentCount];
            for (uint i = 0; i < fieldType.ComponentCount; ++i)
            {
                string letter = null;
                switch (i)
                {
                    case 0: letter = "X"; break;
                    case 1: letter = "Y"; break;
                    case 2: letter = "Z"; break;
                    case 3: letter = "W"; break;
                    default: letter = "_" + i; break;
                }
                fields[i] = ConvertStructField(fieldName + letter, byteOffset + i * fieldType.ComponentType.Alignment, fieldType.ComponentType);
            }

            return new Field(new ShaderInterfaceType(Nameof(fieldType), Sizeof(fieldType), fields), fieldName, byteOffset);
        }

        private static Field ConvertFloatField(string fieldName, uint byteOffset, TypeFloat floatType)
        {
            ShaderInterfaceType actualName = null;
            switch (floatType.Width)
            {
                case 32:
                    actualName = Float;
                    break;
                case 64:
                    actualName = ShaderInterfaceType.Double;
                    break;
            }

            if (actualName != null)
                return new Field(actualName, fieldName, byteOffset);

            throw new NotSupportedException($"float field size '{floatType.Width}'.");
        }

        private static Field ConvertIntField(string fieldName, uint byteOffset, TypeInt intType)
        {
            ShaderInterfaceType actualName = null;
            if (intType.Signed)
            {
                switch (intType.Width)
                {
                    case 8:
                        actualName = ShaderInterfaceType.SByte;
                        break;
                    case 16:
                        actualName = Short;
                        break;
                    case 32:
                        actualName = Int;
                        break;
                    case 64:
                        actualName = Long;
                        break;
                }

            }
            else
            {
                switch (intType.Width)
                {
                    case 8:
                        actualName = ShaderInterfaceType.Byte;
                        break;
                    case 16:
                        actualName = Ushort;
                        break;
                    case 32:
                        actualName = Uint;
                        break;
                    case 64:
                        actualName = Ulong;
                        break;
                }
            }

            if (actualName != null)
                return new Field(actualName, fieldName, byteOffset);

            throw new NotSupportedException($"int field size '{intType.Width}'.");
        }

        private static Field ConvertPointerField(string fieldName, uint byteOffset, TypePointer pointerType)
        {
            return new Field(ConvertType(pointerType), fieldName, byteOffset);
        }

        private static Field ConvertArrayField(string fieldName, uint byteOffset, TypeArray arrayType)
        {
            var fields = new Field[arrayType.Length];
            var sep = char.IsDigit(fieldName[fieldName.Length - 1]) ? "_" : "";
            for (uint i = 0; i < arrayType.Length; ++i)
                fields[i] = ConvertStructField(fieldName + sep + i, byteOffset + i * arrayType.ArrayStride, arrayType.ElementType);

            return new Field(new ShaderInterfaceType(Nameof(arrayType), Sizeof(arrayType), fields), fieldName, byteOffset);
        }

        private static string Nameof(SpirvTypeBase type)
        {
            switch (type.TypeCategory)
            {
                case SpirvTypeCategory.Void: return ShaderInterfaceType.Void.Name;
                case SpirvTypeCategory.Array:
                    {
                        var arrayType = type as TypeArray;
                        return $"{Nameof(arrayType.ElementType)}[{ arrayType.Length}]";
                    };
                case SpirvTypeCategory.Float:
                    {
                        var floatType = type as TypeFloat;
                        switch (floatType.Width)
                        {
                            case 32: return Float.Name;
                            case 64: return ShaderInterfaceType.Double.Name;
                        }

                        throw new NotSupportedException($"float field size '{floatType.Width}'.");
                    }
                case SpirvTypeCategory.Int:
                    {
                        TypeInt intType = type as TypeInt;
                        if (intType.Signed)
                        {
                            switch (intType.Width)
                            {
                                case 8: return ShaderInterfaceType.SByte.Name;
                                case 16: return Short.Name;
                                case 32: return Int.Name;
                                case 64: return Long.Name;
                            }

                        }
                        else
                        {
                            switch (intType.Width)
                            {
                                case 8: return ShaderInterfaceType.Byte.Name;
                                case 16: return Ushort.Name;
                                case 32: return Uint.Name;
                                case 64: return Ulong.Name;
                            }
                        }

                        throw new NotSupportedException($"int field size '{intType.Width}'.");
                    }
                case SpirvTypeCategory.Vector:
                    {
                        var vectorType = type as TypeVector;

                        return $"Vector{vectorType.ComponentCount}";
                    }
                case SpirvTypeCategory.Matrix:
                    {
                        var matrixType = type as TypeMatrix;
                        return $"Matrix{matrixType.ColumnStride}x{matrixType.ColumnCount / matrixType.ColumnStride}";
                    };
                case SpirvTypeCategory.Pointer:
                    {
                        var pointerType = type as TypePointer;
                        return $"{Nameof(pointerType.Type)}*";
                    };
                case SpirvTypeCategory.Sampler:
                    return "Sampler";
                case SpirvTypeCategory.Image:
                    return "Image";
                case SpirvTypeCategory.SampledImage:
                    return "SampledImage";
            }

            return type.ToString();
        }

        private static uint Sizeof(SpirvTypeBase type)
        {

            switch (type.TypeCategory)
            {
                case SpirvTypeCategory.Void: return 4;
                case SpirvTypeCategory.Array:
                    {
                        var arrayType = type as TypeArray;
                        return Sizeof(arrayType.ElementType) * arrayType.Length;
                    };
                case SpirvTypeCategory.Float:
                    {
                        var floatType = type as TypeFloat;
                        return floatType.Width / 8;
                    }
                case SpirvTypeCategory.Int:
                    {
                        TypeInt intType = type as TypeInt;
                        return intType.Width / 8;
                    }
                case SpirvTypeCategory.Vector:
                    {
                        var vectorType = type as TypeVector;
                        switch (vectorType.VectorType)
                        {
                            case VectorType.Vec2: return 8;
                            case VectorType.Vec3: return 12;
                            case VectorType.Vec4: return 16;
                            case VectorType.Ivec2: return 8;
                            case VectorType.Ivec3: return 12;
                            case VectorType.Ivec4: return 16;
                            case VectorType.Uvec2: return 8;
                            case VectorType.Uvec3: return 12;
                            case VectorType.Uvec4: return 16;
                            case VectorType.Bvec2: return 2;
                            case VectorType.Bvec3: return 3;
                            case VectorType.Bvec4: return 4;
                        }

                        return vectorType.ComponentCount;
                    }
                case SpirvTypeCategory.Matrix:
                    {
                        var matrixType = type as TypeMatrix;
                        return matrixType.ColumnCount * Sizeof(matrixType.ColumnType);
                    };
                case SpirvTypeCategory.Pointer: return 4;
                case SpirvTypeCategory.Sampler: return 4;
                case SpirvTypeCategory.Image: return 4;
                case SpirvTypeCategory.SampledImage: return 4;
                case SpirvTypeCategory.Struct:
                    {
                        var maxSize = 0u;
                        var structType = ConvertType(type);
                        for (var i = 0; i < structType.Fields.Length; i++)
                        {
                            var field = structType.Fields[i];
                            var size = field.Offset + field.Type.Size;
                            maxSize = Math.Max(size, maxSize);
                        }

                        return maxSize;
                    };
            }

            return type.SizeInBytes;
        }
    }
}
