using compute.vulkan;
using System;
using System.Collections.Generic;

namespace compute
{
    public struct Interface
    {
        public Interface(string name, ShaderInterfaceType shaderType, InterfaceClass @class, InterfaceType type, int binding, int set, int location)
        {
            Name = name;
            ShaderType = shaderType;
            InterfaceClass = @class;
            InterfaceType = type;
            BindingPoint = binding;
            DescriptorSet = set;
            Location = location;
        }

        public string Name { get; }

        public ShaderInterfaceType ShaderType { get; }

        public InterfaceClass InterfaceClass { get; }

        public InterfaceType InterfaceType { get; }

        public int BindingPoint { get; }

        public int DescriptorSet { get; }

        public int Location { get; }

        internal DescriptorType DescriptorType => StorageDescriptor.Get(InterfaceClass, InterfaceType);

        public override bool Equals(object obj)
        {
            return obj is Interface @interface &&
                   EqualityComparer<ShaderInterfaceType>.Default.Equals(ShaderType, @interface.ShaderType) &&
                   InterfaceClass == @interface.InterfaceClass &&
                   InterfaceType == @interface.InterfaceType &&
                   BindingPoint == @interface.BindingPoint &&
                   DescriptorSet == @interface.DescriptorSet &&
                   Location == @interface.Location &&
                   DescriptorType == @interface.DescriptorType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ShaderType, InterfaceClass, InterfaceType, BindingPoint, DescriptorSet, Location, DescriptorType);
        }

        static class StorageDescriptor
        {
            private static TwoKeyDictionary<InterfaceClass, InterfaceType, DescriptorType> sDict;

            static StorageDescriptor()
            {
                sDict = new TwoKeyDictionary<InterfaceClass, InterfaceType, DescriptorType>();
                sDict[InterfaceClass.UniformConstant, InterfaceType.OpTypeSampler] = DescriptorType.Sampler;
                sDict[InterfaceClass.UniformConstant, InterfaceType.OpTypeImageS1] = DescriptorType.SampledImage;
                sDict[InterfaceClass.UniformConstant, InterfaceType.OpTypeImageS2] = DescriptorType.StorageImage;
                sDict[InterfaceClass.UniformConstant, InterfaceType.OpTypeSampledImage] = DescriptorType.CombinedImageSampler;
                sDict[InterfaceClass.UniformConstant, InterfaceType.OpTypeImageS1DimBuffer] = DescriptorType.UniformTexelBuffer;
                sDict[InterfaceClass.UniformConstant, InterfaceType.OpTypeImageS2DimBuffer] = DescriptorType.StorageTexelBuffer;
                sDict[InterfaceClass.Uniform, InterfaceType.OpTypeStruct] = DescriptorType.UniformBuffer;
                sDict[InterfaceClass.Uniform, InterfaceType.OpTypeStruct] = DescriptorType.StorageBuffer;
                sDict[InterfaceClass.StorageBuffer, InterfaceType.OpTypeStruct] = DescriptorType.StorageBuffer;
                sDict[InterfaceClass.UniformConstant, InterfaceType.OpTypeImageS2DimSubpassData] = DescriptorType.InputAttachment;
                sDict[InterfaceClass.Uniform, InterfaceType.OpTypeStruct] = DescriptorType.UniformBufferDynamic;
                sDict[InterfaceClass.UniformConstant, InterfaceType.OpTypeAccelerationStructureKHR] = DescriptorType.StorageBufferDynamic;
            }

            public static DescriptorType Get(InterfaceClass @class, InterfaceType type)
            {
                if (!sDict.ContainsKey(@class, type))
                    throw new NotSupportedException($"Current spirv version {Settings.Verstion} not support get resource type from StorageClass:{Enum.GetName(typeof(InterfaceClass), @class)}, OP:{Enum.GetName(typeof(InterfaceType), type)}.");

                return sDict[@class, type];
            }
        }
    }
}
