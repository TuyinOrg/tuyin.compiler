using System;
using System.Collections.Generic;

namespace Toe.SPIRV.Spv
{
    internal abstract partial class Capability : ValueEnum
    {
        public enum Enumerant
        {
            Matrix = 0,
            [Capability(Capability.Enumerant.Matrix)]
            Shader = 1,
            [Capability(Capability.Enumerant.Shader)]
            Geometry = 2,
            [Capability(Capability.Enumerant.Shader)]
            Tessellation = 3,
            Addresses = 4,
            Linkage = 5,
            Kernel = 6,
            [Capability(Capability.Enumerant.Kernel)]
            Vector16 = 7,
            [Capability(Capability.Enumerant.Kernel)]
            Float16Buffer = 8,
            Float16 = 9,
            Float64 = 10,
            Int64 = 11,
            [Capability(Capability.Enumerant.Int64)]
            Int64Atomics = 12,
            [Capability(Capability.Enumerant.Kernel)]
            ImageBasic = 13,
            [Capability(Capability.Enumerant.ImageBasic)]
            ImageReadWrite = 14,
            [Capability(Capability.Enumerant.ImageBasic)]
            ImageMipmap = 15,
            [Capability(Capability.Enumerant.Kernel)]
            Pipes = 17,
            Groups = 18,
            [Capability(Capability.Enumerant.Kernel)]
            DeviceEnqueue = 19,
            [Capability(Capability.Enumerant.Kernel)]
            LiteralSampler = 20,
            [Capability(Capability.Enumerant.Shader)]
            AtomicStorage = 21,
            Int16 = 22,
            [Capability(Capability.Enumerant.Tessellation)]
            TessellationPointSize = 23,
            [Capability(Capability.Enumerant.Geometry)]
            GeometryPointSize = 24,
            [Capability(Capability.Enumerant.Shader)]
            ImageGatherExtended = 25,
            [Capability(Capability.Enumerant.Shader)]
            StorageImageMultisample = 27,
            [Capability(Capability.Enumerant.Shader)]
            UniformBufferArrayDynamicIndexing = 28,
            [Capability(Capability.Enumerant.Shader)]
            SampledImageArrayDynamicIndexing = 29,
            [Capability(Capability.Enumerant.Shader)]
            StorageBufferArrayDynamicIndexing = 30,
            [Capability(Capability.Enumerant.Shader)]
            StorageImageArrayDynamicIndexing = 31,
            [Capability(Capability.Enumerant.Shader)]
            ClipDistance = 32,
            [Capability(Capability.Enumerant.Shader)]
            CullDistance = 33,
            [Capability(Capability.Enumerant.SampledCubeArray)]
            ImageCubeArray = 34,
            [Capability(Capability.Enumerant.Shader)]
            SampleRateShading = 35,
            [Capability(Capability.Enumerant.SampledRect)]
            ImageRect = 36,
            [Capability(Capability.Enumerant.Shader)]
            SampledRect = 37,
            [Capability(Capability.Enumerant.Addresses)]
            GenericPointer = 38,
            Int8 = 39,
            [Capability(Capability.Enumerant.Shader)]
            InputAttachment = 40,
            [Capability(Capability.Enumerant.Shader)]
            SparseResidency = 41,
            [Capability(Capability.Enumerant.Shader)]
            MinLod = 42,
            Sampled1D = 43,
            [Capability(Capability.Enumerant.Sampled1D)]
            Image1D = 44,
            [Capability(Capability.Enumerant.Shader)]
            SampledCubeArray = 45,
            SampledBuffer = 46,
            [Capability(Capability.Enumerant.SampledBuffer)]
            ImageBuffer = 47,
            [Capability(Capability.Enumerant.Shader)]
            ImageMSArray = 48,
            [Capability(Capability.Enumerant.Shader)]
            StorageImageExtendedFormats = 49,
            [Capability(Capability.Enumerant.Shader)]
            ImageQuery = 50,
            [Capability(Capability.Enumerant.Shader)]
            DerivativeControl = 51,
            [Capability(Capability.Enumerant.Shader)]
            InterpolationFunction = 52,
            [Capability(Capability.Enumerant.Shader)]
            TransformFeedback = 53,
            [Capability(Capability.Enumerant.Geometry)]
            GeometryStreams = 54,
            [Capability(Capability.Enumerant.Shader)]
            StorageImageReadWithoutFormat = 55,
            [Capability(Capability.Enumerant.Shader)]
            StorageImageWriteWithoutFormat = 56,
            [Capability(Capability.Enumerant.Geometry)]
            MultiViewport = 57,
            [Capability(Capability.Enumerant.DeviceEnqueue)]
            SubgroupDispatch = 58,
            [Capability(Capability.Enumerant.Kernel)]
            NamedBarrier = 59,
            [Capability(Capability.Enumerant.Pipes)]
            PipeStorage = 60,
            GroupNonUniform = 61,
            [Capability(Capability.Enumerant.GroupNonUniform)]
            GroupNonUniformVote = 62,
            [Capability(Capability.Enumerant.GroupNonUniform)]
            GroupNonUniformArithmetic = 63,
            [Capability(Capability.Enumerant.GroupNonUniform)]
            GroupNonUniformBallot = 64,
            [Capability(Capability.Enumerant.GroupNonUniform)]
            GroupNonUniformShuffle = 65,
            [Capability(Capability.Enumerant.GroupNonUniform)]
            GroupNonUniformShuffleRelative = 66,
            [Capability(Capability.Enumerant.GroupNonUniform)]
            GroupNonUniformClustered = 67,
            [Capability(Capability.Enumerant.GroupNonUniform)]
            GroupNonUniformQuad = 68,
            ShaderLayer = 69,
            ShaderViewportIndex = 70,
            SubgroupBallotKHR = 4423,
            [Capability(Capability.Enumerant.Shader)]
            DrawParameters = 4427,
            SubgroupVoteKHR = 4431,
            StorageBuffer16BitAccess = 4433,
            StorageUniformBufferBlock16 = 4433,
            [Capability(Capability.Enumerant.StorageBuffer16BitAccess)]
            [Capability(Capability.Enumerant.StorageUniformBufferBlock16)]
            UniformAndStorageBuffer16BitAccess = 4434,
            [Capability(Capability.Enumerant.StorageBuffer16BitAccess)]
            [Capability(Capability.Enumerant.StorageUniformBufferBlock16)]
            StorageUniform16 = 4434,
            StoragePushConstant16 = 4435,
            StorageInputOutput16 = 4436,
            DeviceGroup = 4437,
            [Capability(Capability.Enumerant.Shader)]
            MultiView = 4439,
            [Capability(Capability.Enumerant.Shader)]
            VariablePointersStorageBuffer = 4441,
            [Capability(Capability.Enumerant.VariablePointersStorageBuffer)]
            VariablePointers = 4442,
            AtomicStorageOps = 4445,
            SampleMaskPostDepthCoverage = 4447,
            StorageBuffer8BitAccess = 4448,
            [Capability(Capability.Enumerant.StorageBuffer8BitAccess)]
            UniformAndStorageBuffer8BitAccess = 4449,
            StoragePushConstant8 = 4450,
            DenormPreserve = 4464,
            DenormFlushToZero = 4465,
            SignedZeroInfNanPreserve = 4466,
            RoundingModeRTE = 4467,
            RoundingModeRTZ = 4468,
            [Capability(Capability.Enumerant.Shader)]
            RayQueryProvisionalKHR = 4471,
            [Capability(Capability.Enumerant.RayQueryProvisionalKHR)]
            [Capability(Capability.Enumerant.RayTracingProvisionalKHR)]
            RayTraversalPrimitiveCullingProvisionalKHR = 4478,
            [Capability(Capability.Enumerant.Shader)]
            Float16ImageAMD = 5008,
            [Capability(Capability.Enumerant.Shader)]
            ImageGatherBiasLodAMD = 5009,
            [Capability(Capability.Enumerant.Shader)]
            FragmentMaskAMD = 5010,
            [Capability(Capability.Enumerant.Shader)]
            StencilExportEXT = 5013,
            [Capability(Capability.Enumerant.Shader)]
            ImageReadWriteLodAMD = 5015,
            [Capability(Capability.Enumerant.Shader)]
            ShaderClockKHR = 5055,
            [Capability(Capability.Enumerant.SampleRateShading)]
            SampleMaskOverrideCoverageNV = 5249,
            [Capability(Capability.Enumerant.Geometry)]
            GeometryShaderPassthroughNV = 5251,
            [Capability(Capability.Enumerant.MultiViewport)]
            ShaderViewportIndexLayerEXT = 5254,
            [Capability(Capability.Enumerant.MultiViewport)]
            ShaderViewportIndexLayerNV = 5254,
            [Capability(Capability.Enumerant.ShaderViewportIndexLayerNV)]
            ShaderViewportMaskNV = 5255,
            [Capability(Capability.Enumerant.ShaderViewportMaskNV)]
            ShaderStereoViewNV = 5259,
            [Capability(Capability.Enumerant.MultiView)]
            PerViewAttributesNV = 5260,
            [Capability(Capability.Enumerant.Shader)]
            FragmentFullyCoveredEXT = 5265,
            [Capability(Capability.Enumerant.Shader)]
            MeshShadingNV = 5266,
            ImageFootprintNV = 5282,
            FragmentBarycentricNV = 5284,
            ComputeDerivativeGroupQuadsNV = 5288,
            [Capability(Capability.Enumerant.Shader)]
            FragmentDensityEXT = 5291,
            [Capability(Capability.Enumerant.Shader)]
            ShadingRateNV = 5291,
            GroupNonUniformPartitionedNV = 5297,
            [Capability(Capability.Enumerant.Shader)]
            ShaderNonUniform = 5301,
            [Capability(Capability.Enumerant.Shader)]
            ShaderNonUniformEXT = 5301,
            [Capability(Capability.Enumerant.Shader)]
            RuntimeDescriptorArray = 5302,
            [Capability(Capability.Enumerant.Shader)]
            RuntimeDescriptorArrayEXT = 5302,
            [Capability(Capability.Enumerant.InputAttachment)]
            InputAttachmentArrayDynamicIndexing = 5303,
            [Capability(Capability.Enumerant.InputAttachment)]
            InputAttachmentArrayDynamicIndexingEXT = 5303,
            [Capability(Capability.Enumerant.SampledBuffer)]
            UniformTexelBufferArrayDynamicIndexing = 5304,
            [Capability(Capability.Enumerant.SampledBuffer)]
            UniformTexelBufferArrayDynamicIndexingEXT = 5304,
            [Capability(Capability.Enumerant.ImageBuffer)]
            StorageTexelBufferArrayDynamicIndexing = 5305,
            [Capability(Capability.Enumerant.ImageBuffer)]
            StorageTexelBufferArrayDynamicIndexingEXT = 5305,
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            UniformBufferArrayNonUniformIndexing = 5306,
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            UniformBufferArrayNonUniformIndexingEXT = 5306,
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            SampledImageArrayNonUniformIndexing = 5307,
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            SampledImageArrayNonUniformIndexingEXT = 5307,
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            StorageBufferArrayNonUniformIndexing = 5308,
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            StorageBufferArrayNonUniformIndexingEXT = 5308,
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            StorageImageArrayNonUniformIndexing = 5309,
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            StorageImageArrayNonUniformIndexingEXT = 5309,
            [Capability(Capability.Enumerant.InputAttachment)]
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            InputAttachmentArrayNonUniformIndexing = 5310,
            [Capability(Capability.Enumerant.InputAttachment)]
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            InputAttachmentArrayNonUniformIndexingEXT = 5310,
            [Capability(Capability.Enumerant.SampledBuffer)]
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            UniformTexelBufferArrayNonUniformIndexing = 5311,
            [Capability(Capability.Enumerant.SampledBuffer)]
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            UniformTexelBufferArrayNonUniformIndexingEXT = 5311,
            [Capability(Capability.Enumerant.ImageBuffer)]
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            StorageTexelBufferArrayNonUniformIndexing = 5312,
            [Capability(Capability.Enumerant.ImageBuffer)]
            [Capability(Capability.Enumerant.ShaderNonUniform)]
            StorageTexelBufferArrayNonUniformIndexingEXT = 5312,
            [Capability(Capability.Enumerant.Shader)]
            RayTracingNV = 5340,
            VulkanMemoryModel = 5345,
            VulkanMemoryModelKHR = 5345,
            VulkanMemoryModelDeviceScope = 5346,
            VulkanMemoryModelDeviceScopeKHR = 5346,
            [Capability(Capability.Enumerant.Shader)]
            PhysicalStorageBufferAddresses = 5347,
            [Capability(Capability.Enumerant.Shader)]
            PhysicalStorageBufferAddressesEXT = 5347,
            ComputeDerivativeGroupLinearNV = 5350,
            [Capability(Capability.Enumerant.Shader)]
            RayTracingProvisionalKHR = 5353,
            [Capability(Capability.Enumerant.Shader)]
            CooperativeMatrixNV = 5357,
            [Capability(Capability.Enumerant.Shader)]
            FragmentShaderSampleInterlockEXT = 5363,
            [Capability(Capability.Enumerant.Shader)]
            FragmentShaderShadingRateInterlockEXT = 5372,
            [Capability(Capability.Enumerant.Shader)]
            ShaderSMBuiltinsNV = 5373,
            [Capability(Capability.Enumerant.Shader)]
            FragmentShaderPixelInterlockEXT = 5378,
            [Capability(Capability.Enumerant.Shader)]
            DemoteToHelperInvocationEXT = 5379,
            SubgroupShuffleINTEL = 5568,
            SubgroupBufferBlockIOINTEL = 5569,
            SubgroupImageBlockIOINTEL = 5570,
            SubgroupImageMediaBlockIOINTEL = 5579,
            [Capability(Capability.Enumerant.Shader)]
            IntegerFunctions2INTEL = 5584,
            SubgroupAvcMotionEstimationINTEL = 5696,
            SubgroupAvcMotionEstimationIntraINTEL = 5697,
            SubgroupAvcMotionEstimationChromaINTEL = 5698,
        }

        #region Matrix
        public static MatrixImpl Matrix()
        {
            return MatrixImpl.Instance;
            
        }

        internal class MatrixImpl: Capability
        {
            public static readonly MatrixImpl Instance = new MatrixImpl();
        
            private  MatrixImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Matrix;
            public new static MatrixImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the MatrixImpl object.</summary>
            /// <returns>A string that represents the MatrixImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Matrix()";
            }
        }
        #endregion //Matrix

        #region Shader
        public static ShaderImpl Shader()
        {
            return ShaderImpl.Instance;
            
        }

        internal class ShaderImpl: Capability
        {
            public static readonly ShaderImpl Instance = new ShaderImpl();
        
            private  ShaderImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Shader;
            public new static ShaderImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShaderImpl object.</summary>
            /// <returns>A string that represents the ShaderImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Shader()";
            }
        }
        #endregion //Shader

        #region Geometry
        public static GeometryImpl Geometry()
        {
            return GeometryImpl.Instance;
            
        }

        internal class GeometryImpl: Capability
        {
            public static readonly GeometryImpl Instance = new GeometryImpl();
        
            private  GeometryImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Geometry;
            public new static GeometryImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GeometryImpl object.</summary>
            /// <returns>A string that represents the GeometryImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Geometry()";
            }
        }
        #endregion //Geometry

        #region Tessellation
        public static TessellationImpl Tessellation()
        {
            return TessellationImpl.Instance;
            
        }

        internal class TessellationImpl: Capability
        {
            public static readonly TessellationImpl Instance = new TessellationImpl();
        
            private  TessellationImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Tessellation;
            public new static TessellationImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the TessellationImpl object.</summary>
            /// <returns>A string that represents the TessellationImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Tessellation()";
            }
        }
        #endregion //Tessellation

        #region Addresses
        public static AddressesImpl Addresses()
        {
            return AddressesImpl.Instance;
            
        }

        internal class AddressesImpl: Capability
        {
            public static readonly AddressesImpl Instance = new AddressesImpl();
        
            private  AddressesImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Addresses;
            public new static AddressesImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the AddressesImpl object.</summary>
            /// <returns>A string that represents the AddressesImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Addresses()";
            }
        }
        #endregion //Addresses

        #region Linkage
        public static LinkageImpl Linkage()
        {
            return LinkageImpl.Instance;
            
        }

        internal class LinkageImpl: Capability
        {
            public static readonly LinkageImpl Instance = new LinkageImpl();
        
            private  LinkageImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Linkage;
            public new static LinkageImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the LinkageImpl object.</summary>
            /// <returns>A string that represents the LinkageImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Linkage()";
            }
        }
        #endregion //Linkage

        #region Kernel
        public static KernelImpl Kernel()
        {
            return KernelImpl.Instance;
            
        }

        internal class KernelImpl: Capability
        {
            public static readonly KernelImpl Instance = new KernelImpl();
        
            private  KernelImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Kernel;
            public new static KernelImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the KernelImpl object.</summary>
            /// <returns>A string that represents the KernelImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Kernel()";
            }
        }
        #endregion //Kernel

        #region Vector16
        public static Vector16Impl Vector16()
        {
            return Vector16Impl.Instance;
            
        }

        internal class Vector16Impl: Capability
        {
            public static readonly Vector16Impl Instance = new Vector16Impl();
        
            private  Vector16Impl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Vector16;
            public new static Vector16Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Vector16Impl object.</summary>
            /// <returns>A string that represents the Vector16Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Vector16()";
            }
        }
        #endregion //Vector16

        #region Float16Buffer
        public static Float16BufferImpl Float16Buffer()
        {
            return Float16BufferImpl.Instance;
            
        }

        internal class Float16BufferImpl: Capability
        {
            public static readonly Float16BufferImpl Instance = new Float16BufferImpl();
        
            private  Float16BufferImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Float16Buffer;
            public new static Float16BufferImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Float16BufferImpl object.</summary>
            /// <returns>A string that represents the Float16BufferImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Float16Buffer()";
            }
        }
        #endregion //Float16Buffer

        #region Float16
        public static Float16Impl Float16()
        {
            return Float16Impl.Instance;
            
        }

        internal class Float16Impl: Capability
        {
            public static readonly Float16Impl Instance = new Float16Impl();
        
            private  Float16Impl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Float16;
            public new static Float16Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Float16Impl object.</summary>
            /// <returns>A string that represents the Float16Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Float16()";
            }
        }
        #endregion //Float16

        #region Float64
        public static Float64Impl Float64()
        {
            return Float64Impl.Instance;
            
        }

        internal class Float64Impl: Capability
        {
            public static readonly Float64Impl Instance = new Float64Impl();
        
            private  Float64Impl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Float64;
            public new static Float64Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Float64Impl object.</summary>
            /// <returns>A string that represents the Float64Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Float64()";
            }
        }
        #endregion //Float64

        #region Int64
        public static Int64Impl Int64()
        {
            return Int64Impl.Instance;
            
        }

        internal class Int64Impl: Capability
        {
            public static readonly Int64Impl Instance = new Int64Impl();
        
            private  Int64Impl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Int64;
            public new static Int64Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Int64Impl object.</summary>
            /// <returns>A string that represents the Int64Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Int64()";
            }
        }
        #endregion //Int64

        #region Int64Atomics
        public static Int64AtomicsImpl Int64Atomics()
        {
            return Int64AtomicsImpl.Instance;
            
        }

        internal class Int64AtomicsImpl: Capability
        {
            public static readonly Int64AtomicsImpl Instance = new Int64AtomicsImpl();
        
            private  Int64AtomicsImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Int64Atomics;
            public new static Int64AtomicsImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Int64AtomicsImpl object.</summary>
            /// <returns>A string that represents the Int64AtomicsImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Int64Atomics()";
            }
        }
        #endregion //Int64Atomics

        #region ImageBasic
        public static ImageBasicImpl ImageBasic()
        {
            return ImageBasicImpl.Instance;
            
        }

        internal class ImageBasicImpl: Capability
        {
            public static readonly ImageBasicImpl Instance = new ImageBasicImpl();
        
            private  ImageBasicImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ImageBasic;
            public new static ImageBasicImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ImageBasicImpl object.</summary>
            /// <returns>A string that represents the ImageBasicImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ImageBasic()";
            }
        }
        #endregion //ImageBasic

        #region ImageReadWrite
        public static ImageReadWriteImpl ImageReadWrite()
        {
            return ImageReadWriteImpl.Instance;
            
        }

        internal class ImageReadWriteImpl: Capability
        {
            public static readonly ImageReadWriteImpl Instance = new ImageReadWriteImpl();
        
            private  ImageReadWriteImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ImageReadWrite;
            public new static ImageReadWriteImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ImageReadWriteImpl object.</summary>
            /// <returns>A string that represents the ImageReadWriteImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ImageReadWrite()";
            }
        }
        #endregion //ImageReadWrite

        #region ImageMipmap
        public static ImageMipmapImpl ImageMipmap()
        {
            return ImageMipmapImpl.Instance;
            
        }

        internal class ImageMipmapImpl: Capability
        {
            public static readonly ImageMipmapImpl Instance = new ImageMipmapImpl();
        
            private  ImageMipmapImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ImageMipmap;
            public new static ImageMipmapImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ImageMipmapImpl object.</summary>
            /// <returns>A string that represents the ImageMipmapImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ImageMipmap()";
            }
        }
        #endregion //ImageMipmap

        #region Pipes
        public static PipesImpl Pipes()
        {
            return PipesImpl.Instance;
            
        }

        internal class PipesImpl: Capability
        {
            public static readonly PipesImpl Instance = new PipesImpl();
        
            private  PipesImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Pipes;
            public new static PipesImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PipesImpl object.</summary>
            /// <returns>A string that represents the PipesImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Pipes()";
            }
        }
        #endregion //Pipes

        #region Groups
        public static GroupsImpl Groups()
        {
            return GroupsImpl.Instance;
            
        }

        internal class GroupsImpl: Capability
        {
            public static readonly GroupsImpl Instance = new GroupsImpl();
        
            private  GroupsImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Groups;
            public new static GroupsImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GroupsImpl object.</summary>
            /// <returns>A string that represents the GroupsImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Groups()";
            }
        }
        #endregion //Groups

        #region DeviceEnqueue
        public static DeviceEnqueueImpl DeviceEnqueue()
        {
            return DeviceEnqueueImpl.Instance;
            
        }

        internal class DeviceEnqueueImpl: Capability
        {
            public static readonly DeviceEnqueueImpl Instance = new DeviceEnqueueImpl();
        
            private  DeviceEnqueueImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.DeviceEnqueue;
            public new static DeviceEnqueueImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DeviceEnqueueImpl object.</summary>
            /// <returns>A string that represents the DeviceEnqueueImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.DeviceEnqueue()";
            }
        }
        #endregion //DeviceEnqueue

        #region LiteralSampler
        public static LiteralSamplerImpl LiteralSampler()
        {
            return LiteralSamplerImpl.Instance;
            
        }

        internal class LiteralSamplerImpl: Capability
        {
            public static readonly LiteralSamplerImpl Instance = new LiteralSamplerImpl();
        
            private  LiteralSamplerImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.LiteralSampler;
            public new static LiteralSamplerImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the LiteralSamplerImpl object.</summary>
            /// <returns>A string that represents the LiteralSamplerImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.LiteralSampler()";
            }
        }
        #endregion //LiteralSampler

        #region AtomicStorage
        public static AtomicStorageImpl AtomicStorage()
        {
            return AtomicStorageImpl.Instance;
            
        }

        internal class AtomicStorageImpl: Capability
        {
            public static readonly AtomicStorageImpl Instance = new AtomicStorageImpl();
        
            private  AtomicStorageImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.AtomicStorage;
            public new static AtomicStorageImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the AtomicStorageImpl object.</summary>
            /// <returns>A string that represents the AtomicStorageImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.AtomicStorage()";
            }
        }
        #endregion //AtomicStorage

        #region Int16
        public static Int16Impl Int16()
        {
            return Int16Impl.Instance;
            
        }

        internal class Int16Impl: Capability
        {
            public static readonly Int16Impl Instance = new Int16Impl();
        
            private  Int16Impl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Int16;
            public new static Int16Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Int16Impl object.</summary>
            /// <returns>A string that represents the Int16Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Int16()";
            }
        }
        #endregion //Int16

        #region TessellationPointSize
        public static TessellationPointSizeImpl TessellationPointSize()
        {
            return TessellationPointSizeImpl.Instance;
            
        }

        internal class TessellationPointSizeImpl: Capability
        {
            public static readonly TessellationPointSizeImpl Instance = new TessellationPointSizeImpl();
        
            private  TessellationPointSizeImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.TessellationPointSize;
            public new static TessellationPointSizeImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the TessellationPointSizeImpl object.</summary>
            /// <returns>A string that represents the TessellationPointSizeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.TessellationPointSize()";
            }
        }
        #endregion //TessellationPointSize

        #region GeometryPointSize
        public static GeometryPointSizeImpl GeometryPointSize()
        {
            return GeometryPointSizeImpl.Instance;
            
        }

        internal class GeometryPointSizeImpl: Capability
        {
            public static readonly GeometryPointSizeImpl Instance = new GeometryPointSizeImpl();
        
            private  GeometryPointSizeImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.GeometryPointSize;
            public new static GeometryPointSizeImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GeometryPointSizeImpl object.</summary>
            /// <returns>A string that represents the GeometryPointSizeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.GeometryPointSize()";
            }
        }
        #endregion //GeometryPointSize

        #region ImageGatherExtended
        public static ImageGatherExtendedImpl ImageGatherExtended()
        {
            return ImageGatherExtendedImpl.Instance;
            
        }

        internal class ImageGatherExtendedImpl: Capability
        {
            public static readonly ImageGatherExtendedImpl Instance = new ImageGatherExtendedImpl();
        
            private  ImageGatherExtendedImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ImageGatherExtended;
            public new static ImageGatherExtendedImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ImageGatherExtendedImpl object.</summary>
            /// <returns>A string that represents the ImageGatherExtendedImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ImageGatherExtended()";
            }
        }
        #endregion //ImageGatherExtended

        #region StorageImageMultisample
        public static StorageImageMultisampleImpl StorageImageMultisample()
        {
            return StorageImageMultisampleImpl.Instance;
            
        }

        internal class StorageImageMultisampleImpl: Capability
        {
            public static readonly StorageImageMultisampleImpl Instance = new StorageImageMultisampleImpl();
        
            private  StorageImageMultisampleImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageImageMultisample;
            public new static StorageImageMultisampleImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageImageMultisampleImpl object.</summary>
            /// <returns>A string that represents the StorageImageMultisampleImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageImageMultisample()";
            }
        }
        #endregion //StorageImageMultisample

        #region UniformBufferArrayDynamicIndexing
        public static UniformBufferArrayDynamicIndexingImpl UniformBufferArrayDynamicIndexing()
        {
            return UniformBufferArrayDynamicIndexingImpl.Instance;
            
        }

        internal class UniformBufferArrayDynamicIndexingImpl: Capability
        {
            public static readonly UniformBufferArrayDynamicIndexingImpl Instance = new UniformBufferArrayDynamicIndexingImpl();
        
            private  UniformBufferArrayDynamicIndexingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.UniformBufferArrayDynamicIndexing;
            public new static UniformBufferArrayDynamicIndexingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UniformBufferArrayDynamicIndexingImpl object.</summary>
            /// <returns>A string that represents the UniformBufferArrayDynamicIndexingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.UniformBufferArrayDynamicIndexing()";
            }
        }
        #endregion //UniformBufferArrayDynamicIndexing

        #region SampledImageArrayDynamicIndexing
        public static SampledImageArrayDynamicIndexingImpl SampledImageArrayDynamicIndexing()
        {
            return SampledImageArrayDynamicIndexingImpl.Instance;
            
        }

        internal class SampledImageArrayDynamicIndexingImpl: Capability
        {
            public static readonly SampledImageArrayDynamicIndexingImpl Instance = new SampledImageArrayDynamicIndexingImpl();
        
            private  SampledImageArrayDynamicIndexingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SampledImageArrayDynamicIndexing;
            public new static SampledImageArrayDynamicIndexingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SampledImageArrayDynamicIndexingImpl object.</summary>
            /// <returns>A string that represents the SampledImageArrayDynamicIndexingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SampledImageArrayDynamicIndexing()";
            }
        }
        #endregion //SampledImageArrayDynamicIndexing

        #region StorageBufferArrayDynamicIndexing
        public static StorageBufferArrayDynamicIndexingImpl StorageBufferArrayDynamicIndexing()
        {
            return StorageBufferArrayDynamicIndexingImpl.Instance;
            
        }

        internal class StorageBufferArrayDynamicIndexingImpl: Capability
        {
            public static readonly StorageBufferArrayDynamicIndexingImpl Instance = new StorageBufferArrayDynamicIndexingImpl();
        
            private  StorageBufferArrayDynamicIndexingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageBufferArrayDynamicIndexing;
            public new static StorageBufferArrayDynamicIndexingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageBufferArrayDynamicIndexingImpl object.</summary>
            /// <returns>A string that represents the StorageBufferArrayDynamicIndexingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageBufferArrayDynamicIndexing()";
            }
        }
        #endregion //StorageBufferArrayDynamicIndexing

        #region StorageImageArrayDynamicIndexing
        public static StorageImageArrayDynamicIndexingImpl StorageImageArrayDynamicIndexing()
        {
            return StorageImageArrayDynamicIndexingImpl.Instance;
            
        }

        internal class StorageImageArrayDynamicIndexingImpl: Capability
        {
            public static readonly StorageImageArrayDynamicIndexingImpl Instance = new StorageImageArrayDynamicIndexingImpl();
        
            private  StorageImageArrayDynamicIndexingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageImageArrayDynamicIndexing;
            public new static StorageImageArrayDynamicIndexingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageImageArrayDynamicIndexingImpl object.</summary>
            /// <returns>A string that represents the StorageImageArrayDynamicIndexingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageImageArrayDynamicIndexing()";
            }
        }
        #endregion //StorageImageArrayDynamicIndexing

        #region ClipDistance
        public static ClipDistanceImpl ClipDistance()
        {
            return ClipDistanceImpl.Instance;
            
        }

        internal class ClipDistanceImpl: Capability
        {
            public static readonly ClipDistanceImpl Instance = new ClipDistanceImpl();
        
            private  ClipDistanceImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ClipDistance;
            public new static ClipDistanceImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ClipDistanceImpl object.</summary>
            /// <returns>A string that represents the ClipDistanceImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ClipDistance()";
            }
        }
        #endregion //ClipDistance

        #region CullDistance
        public static CullDistanceImpl CullDistance()
        {
            return CullDistanceImpl.Instance;
            
        }

        internal class CullDistanceImpl: Capability
        {
            public static readonly CullDistanceImpl Instance = new CullDistanceImpl();
        
            private  CullDistanceImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.CullDistance;
            public new static CullDistanceImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the CullDistanceImpl object.</summary>
            /// <returns>A string that represents the CullDistanceImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.CullDistance()";
            }
        }
        #endregion //CullDistance

        #region ImageCubeArray
        public static ImageCubeArrayImpl ImageCubeArray()
        {
            return ImageCubeArrayImpl.Instance;
            
        }

        internal class ImageCubeArrayImpl: Capability
        {
            public static readonly ImageCubeArrayImpl Instance = new ImageCubeArrayImpl();
        
            private  ImageCubeArrayImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ImageCubeArray;
            public new static ImageCubeArrayImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ImageCubeArrayImpl object.</summary>
            /// <returns>A string that represents the ImageCubeArrayImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ImageCubeArray()";
            }
        }
        #endregion //ImageCubeArray

        #region SampleRateShading
        public static SampleRateShadingImpl SampleRateShading()
        {
            return SampleRateShadingImpl.Instance;
            
        }

        internal class SampleRateShadingImpl: Capability
        {
            public static readonly SampleRateShadingImpl Instance = new SampleRateShadingImpl();
        
            private  SampleRateShadingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SampleRateShading;
            public new static SampleRateShadingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SampleRateShadingImpl object.</summary>
            /// <returns>A string that represents the SampleRateShadingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SampleRateShading()";
            }
        }
        #endregion //SampleRateShading

        #region ImageRect
        public static ImageRectImpl ImageRect()
        {
            return ImageRectImpl.Instance;
            
        }

        internal class ImageRectImpl: Capability
        {
            public static readonly ImageRectImpl Instance = new ImageRectImpl();
        
            private  ImageRectImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ImageRect;
            public new static ImageRectImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ImageRectImpl object.</summary>
            /// <returns>A string that represents the ImageRectImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ImageRect()";
            }
        }
        #endregion //ImageRect

        #region SampledRect
        public static SampledRectImpl SampledRect()
        {
            return SampledRectImpl.Instance;
            
        }

        internal class SampledRectImpl: Capability
        {
            public static readonly SampledRectImpl Instance = new SampledRectImpl();
        
            private  SampledRectImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SampledRect;
            public new static SampledRectImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SampledRectImpl object.</summary>
            /// <returns>A string that represents the SampledRectImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SampledRect()";
            }
        }
        #endregion //SampledRect

        #region GenericPointer
        public static GenericPointerImpl GenericPointer()
        {
            return GenericPointerImpl.Instance;
            
        }

        internal class GenericPointerImpl: Capability
        {
            public static readonly GenericPointerImpl Instance = new GenericPointerImpl();
        
            private  GenericPointerImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.GenericPointer;
            public new static GenericPointerImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GenericPointerImpl object.</summary>
            /// <returns>A string that represents the GenericPointerImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.GenericPointer()";
            }
        }
        #endregion //GenericPointer

        #region Int8
        public static Int8Impl Int8()
        {
            return Int8Impl.Instance;
            
        }

        internal class Int8Impl: Capability
        {
            public static readonly Int8Impl Instance = new Int8Impl();
        
            private  Int8Impl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Int8;
            public new static Int8Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Int8Impl object.</summary>
            /// <returns>A string that represents the Int8Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Int8()";
            }
        }
        #endregion //Int8

        #region InputAttachment
        public static InputAttachmentImpl InputAttachment()
        {
            return InputAttachmentImpl.Instance;
            
        }

        internal class InputAttachmentImpl: Capability
        {
            public static readonly InputAttachmentImpl Instance = new InputAttachmentImpl();
        
            private  InputAttachmentImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.InputAttachment;
            public new static InputAttachmentImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InputAttachmentImpl object.</summary>
            /// <returns>A string that represents the InputAttachmentImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.InputAttachment()";
            }
        }
        #endregion //InputAttachment

        #region SparseResidency
        public static SparseResidencyImpl SparseResidency()
        {
            return SparseResidencyImpl.Instance;
            
        }

        internal class SparseResidencyImpl: Capability
        {
            public static readonly SparseResidencyImpl Instance = new SparseResidencyImpl();
        
            private  SparseResidencyImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SparseResidency;
            public new static SparseResidencyImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SparseResidencyImpl object.</summary>
            /// <returns>A string that represents the SparseResidencyImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SparseResidency()";
            }
        }
        #endregion //SparseResidency

        #region MinLod
        public static MinLodImpl MinLod()
        {
            return MinLodImpl.Instance;
            
        }

        internal class MinLodImpl: Capability
        {
            public static readonly MinLodImpl Instance = new MinLodImpl();
        
            private  MinLodImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.MinLod;
            public new static MinLodImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the MinLodImpl object.</summary>
            /// <returns>A string that represents the MinLodImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.MinLod()";
            }
        }
        #endregion //MinLod

        #region Sampled1D
        public static Sampled1DImpl Sampled1D()
        {
            return Sampled1DImpl.Instance;
            
        }

        internal class Sampled1DImpl: Capability
        {
            public static readonly Sampled1DImpl Instance = new Sampled1DImpl();
        
            private  Sampled1DImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Sampled1D;
            public new static Sampled1DImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Sampled1DImpl object.</summary>
            /// <returns>A string that represents the Sampled1DImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Sampled1D()";
            }
        }
        #endregion //Sampled1D

        #region Image1D
        public static Image1DImpl Image1D()
        {
            return Image1DImpl.Instance;
            
        }

        internal class Image1DImpl: Capability
        {
            public static readonly Image1DImpl Instance = new Image1DImpl();
        
            private  Image1DImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Image1D;
            public new static Image1DImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Image1DImpl object.</summary>
            /// <returns>A string that represents the Image1DImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Image1D()";
            }
        }
        #endregion //Image1D

        #region SampledCubeArray
        public static SampledCubeArrayImpl SampledCubeArray()
        {
            return SampledCubeArrayImpl.Instance;
            
        }

        internal class SampledCubeArrayImpl: Capability
        {
            public static readonly SampledCubeArrayImpl Instance = new SampledCubeArrayImpl();
        
            private  SampledCubeArrayImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SampledCubeArray;
            public new static SampledCubeArrayImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SampledCubeArrayImpl object.</summary>
            /// <returns>A string that represents the SampledCubeArrayImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SampledCubeArray()";
            }
        }
        #endregion //SampledCubeArray

        #region SampledBuffer
        public static SampledBufferImpl SampledBuffer()
        {
            return SampledBufferImpl.Instance;
            
        }

        internal class SampledBufferImpl: Capability
        {
            public static readonly SampledBufferImpl Instance = new SampledBufferImpl();
        
            private  SampledBufferImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SampledBuffer;
            public new static SampledBufferImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SampledBufferImpl object.</summary>
            /// <returns>A string that represents the SampledBufferImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SampledBuffer()";
            }
        }
        #endregion //SampledBuffer

        #region ImageBuffer
        public static ImageBufferImpl ImageBuffer()
        {
            return ImageBufferImpl.Instance;
            
        }

        internal class ImageBufferImpl: Capability
        {
            public static readonly ImageBufferImpl Instance = new ImageBufferImpl();
        
            private  ImageBufferImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ImageBuffer;
            public new static ImageBufferImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ImageBufferImpl object.</summary>
            /// <returns>A string that represents the ImageBufferImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ImageBuffer()";
            }
        }
        #endregion //ImageBuffer

        #region ImageMSArray
        public static ImageMSArrayImpl ImageMSArray()
        {
            return ImageMSArrayImpl.Instance;
            
        }

        internal class ImageMSArrayImpl: Capability
        {
            public static readonly ImageMSArrayImpl Instance = new ImageMSArrayImpl();
        
            private  ImageMSArrayImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ImageMSArray;
            public new static ImageMSArrayImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ImageMSArrayImpl object.</summary>
            /// <returns>A string that represents the ImageMSArrayImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ImageMSArray()";
            }
        }
        #endregion //ImageMSArray

        #region StorageImageExtendedFormats
        public static StorageImageExtendedFormatsImpl StorageImageExtendedFormats()
        {
            return StorageImageExtendedFormatsImpl.Instance;
            
        }

        internal class StorageImageExtendedFormatsImpl: Capability
        {
            public static readonly StorageImageExtendedFormatsImpl Instance = new StorageImageExtendedFormatsImpl();
        
            private  StorageImageExtendedFormatsImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageImageExtendedFormats;
            public new static StorageImageExtendedFormatsImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageImageExtendedFormatsImpl object.</summary>
            /// <returns>A string that represents the StorageImageExtendedFormatsImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageImageExtendedFormats()";
            }
        }
        #endregion //StorageImageExtendedFormats

        #region ImageQuery
        public static ImageQueryImpl ImageQuery()
        {
            return ImageQueryImpl.Instance;
            
        }

        internal class ImageQueryImpl: Capability
        {
            public static readonly ImageQueryImpl Instance = new ImageQueryImpl();
        
            private  ImageQueryImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ImageQuery;
            public new static ImageQueryImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ImageQueryImpl object.</summary>
            /// <returns>A string that represents the ImageQueryImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ImageQuery()";
            }
        }
        #endregion //ImageQuery

        #region DerivativeControl
        public static DerivativeControlImpl DerivativeControl()
        {
            return DerivativeControlImpl.Instance;
            
        }

        internal class DerivativeControlImpl: Capability
        {
            public static readonly DerivativeControlImpl Instance = new DerivativeControlImpl();
        
            private  DerivativeControlImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.DerivativeControl;
            public new static DerivativeControlImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DerivativeControlImpl object.</summary>
            /// <returns>A string that represents the DerivativeControlImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.DerivativeControl()";
            }
        }
        #endregion //DerivativeControl

        #region InterpolationFunction
        public static InterpolationFunctionImpl InterpolationFunction()
        {
            return InterpolationFunctionImpl.Instance;
            
        }

        internal class InterpolationFunctionImpl: Capability
        {
            public static readonly InterpolationFunctionImpl Instance = new InterpolationFunctionImpl();
        
            private  InterpolationFunctionImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.InterpolationFunction;
            public new static InterpolationFunctionImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InterpolationFunctionImpl object.</summary>
            /// <returns>A string that represents the InterpolationFunctionImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.InterpolationFunction()";
            }
        }
        #endregion //InterpolationFunction

        #region TransformFeedback
        public static TransformFeedbackImpl TransformFeedback()
        {
            return TransformFeedbackImpl.Instance;
            
        }

        internal class TransformFeedbackImpl: Capability
        {
            public static readonly TransformFeedbackImpl Instance = new TransformFeedbackImpl();
        
            private  TransformFeedbackImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.TransformFeedback;
            public new static TransformFeedbackImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the TransformFeedbackImpl object.</summary>
            /// <returns>A string that represents the TransformFeedbackImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.TransformFeedback()";
            }
        }
        #endregion //TransformFeedback

        #region GeometryStreams
        public static GeometryStreamsImpl GeometryStreams()
        {
            return GeometryStreamsImpl.Instance;
            
        }

        internal class GeometryStreamsImpl: Capability
        {
            public static readonly GeometryStreamsImpl Instance = new GeometryStreamsImpl();
        
            private  GeometryStreamsImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.GeometryStreams;
            public new static GeometryStreamsImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GeometryStreamsImpl object.</summary>
            /// <returns>A string that represents the GeometryStreamsImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.GeometryStreams()";
            }
        }
        #endregion //GeometryStreams

        #region StorageImageReadWithoutFormat
        public static StorageImageReadWithoutFormatImpl StorageImageReadWithoutFormat()
        {
            return StorageImageReadWithoutFormatImpl.Instance;
            
        }

        internal class StorageImageReadWithoutFormatImpl: Capability
        {
            public static readonly StorageImageReadWithoutFormatImpl Instance = new StorageImageReadWithoutFormatImpl();
        
            private  StorageImageReadWithoutFormatImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageImageReadWithoutFormat;
            public new static StorageImageReadWithoutFormatImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageImageReadWithoutFormatImpl object.</summary>
            /// <returns>A string that represents the StorageImageReadWithoutFormatImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageImageReadWithoutFormat()";
            }
        }
        #endregion //StorageImageReadWithoutFormat

        #region StorageImageWriteWithoutFormat
        public static StorageImageWriteWithoutFormatImpl StorageImageWriteWithoutFormat()
        {
            return StorageImageWriteWithoutFormatImpl.Instance;
            
        }

        internal class StorageImageWriteWithoutFormatImpl: Capability
        {
            public static readonly StorageImageWriteWithoutFormatImpl Instance = new StorageImageWriteWithoutFormatImpl();
        
            private  StorageImageWriteWithoutFormatImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageImageWriteWithoutFormat;
            public new static StorageImageWriteWithoutFormatImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageImageWriteWithoutFormatImpl object.</summary>
            /// <returns>A string that represents the StorageImageWriteWithoutFormatImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageImageWriteWithoutFormat()";
            }
        }
        #endregion //StorageImageWriteWithoutFormat

        #region MultiViewport
        public static MultiViewportImpl MultiViewport()
        {
            return MultiViewportImpl.Instance;
            
        }

        internal class MultiViewportImpl: Capability
        {
            public static readonly MultiViewportImpl Instance = new MultiViewportImpl();
        
            private  MultiViewportImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.MultiViewport;
            public new static MultiViewportImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the MultiViewportImpl object.</summary>
            /// <returns>A string that represents the MultiViewportImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.MultiViewport()";
            }
        }
        #endregion //MultiViewport

        #region SubgroupDispatch
        public static SubgroupDispatchImpl SubgroupDispatch()
        {
            return SubgroupDispatchImpl.Instance;
            
        }

        internal class SubgroupDispatchImpl: Capability
        {
            public static readonly SubgroupDispatchImpl Instance = new SubgroupDispatchImpl();
        
            private  SubgroupDispatchImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SubgroupDispatch;
            public new static SubgroupDispatchImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupDispatchImpl object.</summary>
            /// <returns>A string that represents the SubgroupDispatchImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SubgroupDispatch()";
            }
        }
        #endregion //SubgroupDispatch

        #region NamedBarrier
        public static NamedBarrierImpl NamedBarrier()
        {
            return NamedBarrierImpl.Instance;
            
        }

        internal class NamedBarrierImpl: Capability
        {
            public static readonly NamedBarrierImpl Instance = new NamedBarrierImpl();
        
            private  NamedBarrierImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.NamedBarrier;
            public new static NamedBarrierImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the NamedBarrierImpl object.</summary>
            /// <returns>A string that represents the NamedBarrierImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.NamedBarrier()";
            }
        }
        #endregion //NamedBarrier

        #region PipeStorage
        public static PipeStorageImpl PipeStorage()
        {
            return PipeStorageImpl.Instance;
            
        }

        internal class PipeStorageImpl: Capability
        {
            public static readonly PipeStorageImpl Instance = new PipeStorageImpl();
        
            private  PipeStorageImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.PipeStorage;
            public new static PipeStorageImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PipeStorageImpl object.</summary>
            /// <returns>A string that represents the PipeStorageImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.PipeStorage()";
            }
        }
        #endregion //PipeStorage

        #region GroupNonUniform
        public static GroupNonUniformImpl GroupNonUniform()
        {
            return GroupNonUniformImpl.Instance;
            
        }

        internal class GroupNonUniformImpl: Capability
        {
            public static readonly GroupNonUniformImpl Instance = new GroupNonUniformImpl();
        
            private  GroupNonUniformImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.GroupNonUniform;
            public new static GroupNonUniformImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GroupNonUniformImpl object.</summary>
            /// <returns>A string that represents the GroupNonUniformImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.GroupNonUniform()";
            }
        }
        #endregion //GroupNonUniform

        #region GroupNonUniformVote
        public static GroupNonUniformVoteImpl GroupNonUniformVote()
        {
            return GroupNonUniformVoteImpl.Instance;
            
        }

        internal class GroupNonUniformVoteImpl: Capability
        {
            public static readonly GroupNonUniformVoteImpl Instance = new GroupNonUniformVoteImpl();
        
            private  GroupNonUniformVoteImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.GroupNonUniformVote;
            public new static GroupNonUniformVoteImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GroupNonUniformVoteImpl object.</summary>
            /// <returns>A string that represents the GroupNonUniformVoteImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.GroupNonUniformVote()";
            }
        }
        #endregion //GroupNonUniformVote

        #region GroupNonUniformArithmetic
        public static GroupNonUniformArithmeticImpl GroupNonUniformArithmetic()
        {
            return GroupNonUniformArithmeticImpl.Instance;
            
        }

        internal class GroupNonUniformArithmeticImpl: Capability
        {
            public static readonly GroupNonUniformArithmeticImpl Instance = new GroupNonUniformArithmeticImpl();
        
            private  GroupNonUniformArithmeticImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.GroupNonUniformArithmetic;
            public new static GroupNonUniformArithmeticImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GroupNonUniformArithmeticImpl object.</summary>
            /// <returns>A string that represents the GroupNonUniformArithmeticImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.GroupNonUniformArithmetic()";
            }
        }
        #endregion //GroupNonUniformArithmetic

        #region GroupNonUniformBallot
        public static GroupNonUniformBallotImpl GroupNonUniformBallot()
        {
            return GroupNonUniformBallotImpl.Instance;
            
        }

        internal class GroupNonUniformBallotImpl: Capability
        {
            public static readonly GroupNonUniformBallotImpl Instance = new GroupNonUniformBallotImpl();
        
            private  GroupNonUniformBallotImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.GroupNonUniformBallot;
            public new static GroupNonUniformBallotImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GroupNonUniformBallotImpl object.</summary>
            /// <returns>A string that represents the GroupNonUniformBallotImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.GroupNonUniformBallot()";
            }
        }
        #endregion //GroupNonUniformBallot

        #region GroupNonUniformShuffle
        public static GroupNonUniformShuffleImpl GroupNonUniformShuffle()
        {
            return GroupNonUniformShuffleImpl.Instance;
            
        }

        internal class GroupNonUniformShuffleImpl: Capability
        {
            public static readonly GroupNonUniformShuffleImpl Instance = new GroupNonUniformShuffleImpl();
        
            private  GroupNonUniformShuffleImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.GroupNonUniformShuffle;
            public new static GroupNonUniformShuffleImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GroupNonUniformShuffleImpl object.</summary>
            /// <returns>A string that represents the GroupNonUniformShuffleImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.GroupNonUniformShuffle()";
            }
        }
        #endregion //GroupNonUniformShuffle

        #region GroupNonUniformShuffleRelative
        public static GroupNonUniformShuffleRelativeImpl GroupNonUniformShuffleRelative()
        {
            return GroupNonUniformShuffleRelativeImpl.Instance;
            
        }

        internal class GroupNonUniformShuffleRelativeImpl: Capability
        {
            public static readonly GroupNonUniformShuffleRelativeImpl Instance = new GroupNonUniformShuffleRelativeImpl();
        
            private  GroupNonUniformShuffleRelativeImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.GroupNonUniformShuffleRelative;
            public new static GroupNonUniformShuffleRelativeImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GroupNonUniformShuffleRelativeImpl object.</summary>
            /// <returns>A string that represents the GroupNonUniformShuffleRelativeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.GroupNonUniformShuffleRelative()";
            }
        }
        #endregion //GroupNonUniformShuffleRelative

        #region GroupNonUniformClustered
        public static GroupNonUniformClusteredImpl GroupNonUniformClustered()
        {
            return GroupNonUniformClusteredImpl.Instance;
            
        }

        internal class GroupNonUniformClusteredImpl: Capability
        {
            public static readonly GroupNonUniformClusteredImpl Instance = new GroupNonUniformClusteredImpl();
        
            private  GroupNonUniformClusteredImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.GroupNonUniformClustered;
            public new static GroupNonUniformClusteredImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GroupNonUniformClusteredImpl object.</summary>
            /// <returns>A string that represents the GroupNonUniformClusteredImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.GroupNonUniformClustered()";
            }
        }
        #endregion //GroupNonUniformClustered

        #region GroupNonUniformQuad
        public static GroupNonUniformQuadImpl GroupNonUniformQuad()
        {
            return GroupNonUniformQuadImpl.Instance;
            
        }

        internal class GroupNonUniformQuadImpl: Capability
        {
            public static readonly GroupNonUniformQuadImpl Instance = new GroupNonUniformQuadImpl();
        
            private  GroupNonUniformQuadImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.GroupNonUniformQuad;
            public new static GroupNonUniformQuadImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GroupNonUniformQuadImpl object.</summary>
            /// <returns>A string that represents the GroupNonUniformQuadImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.GroupNonUniformQuad()";
            }
        }
        #endregion //GroupNonUniformQuad

        #region ShaderLayer
        public static ShaderLayerImpl ShaderLayer()
        {
            return ShaderLayerImpl.Instance;
            
        }

        internal class ShaderLayerImpl: Capability
        {
            public static readonly ShaderLayerImpl Instance = new ShaderLayerImpl();
        
            private  ShaderLayerImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ShaderLayer;
            public new static ShaderLayerImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShaderLayerImpl object.</summary>
            /// <returns>A string that represents the ShaderLayerImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ShaderLayer()";
            }
        }
        #endregion //ShaderLayer

        #region ShaderViewportIndex
        public static ShaderViewportIndexImpl ShaderViewportIndex()
        {
            return ShaderViewportIndexImpl.Instance;
            
        }

        internal class ShaderViewportIndexImpl: Capability
        {
            public static readonly ShaderViewportIndexImpl Instance = new ShaderViewportIndexImpl();
        
            private  ShaderViewportIndexImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ShaderViewportIndex;
            public new static ShaderViewportIndexImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShaderViewportIndexImpl object.</summary>
            /// <returns>A string that represents the ShaderViewportIndexImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ShaderViewportIndex()";
            }
        }
        #endregion //ShaderViewportIndex

        #region SubgroupBallotKHR
        public static SubgroupBallotKHRImpl SubgroupBallotKHR()
        {
            return SubgroupBallotKHRImpl.Instance;
            
        }

        internal class SubgroupBallotKHRImpl: Capability
        {
            public static readonly SubgroupBallotKHRImpl Instance = new SubgroupBallotKHRImpl();
        
            private  SubgroupBallotKHRImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SubgroupBallotKHR;
            public new static SubgroupBallotKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupBallotKHRImpl object.</summary>
            /// <returns>A string that represents the SubgroupBallotKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SubgroupBallotKHR()";
            }
        }
        #endregion //SubgroupBallotKHR

        #region DrawParameters
        public static DrawParametersImpl DrawParameters()
        {
            return DrawParametersImpl.Instance;
            
        }

        internal class DrawParametersImpl: Capability
        {
            public static readonly DrawParametersImpl Instance = new DrawParametersImpl();
        
            private  DrawParametersImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.DrawParameters;
            public new static DrawParametersImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DrawParametersImpl object.</summary>
            /// <returns>A string that represents the DrawParametersImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.DrawParameters()";
            }
        }
        #endregion //DrawParameters

        #region SubgroupVoteKHR
        public static SubgroupVoteKHRImpl SubgroupVoteKHR()
        {
            return SubgroupVoteKHRImpl.Instance;
            
        }

        internal class SubgroupVoteKHRImpl: Capability
        {
            public static readonly SubgroupVoteKHRImpl Instance = new SubgroupVoteKHRImpl();
        
            private  SubgroupVoteKHRImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SubgroupVoteKHR;
            public new static SubgroupVoteKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupVoteKHRImpl object.</summary>
            /// <returns>A string that represents the SubgroupVoteKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SubgroupVoteKHR()";
            }
        }
        #endregion //SubgroupVoteKHR

        #region StorageBuffer16BitAccess
        public static StorageBuffer16BitAccessImpl StorageBuffer16BitAccess()
        {
            return StorageBuffer16BitAccessImpl.Instance;
            
        }

        internal class StorageBuffer16BitAccessImpl: Capability
        {
            public static readonly StorageBuffer16BitAccessImpl Instance = new StorageBuffer16BitAccessImpl();
        
            private  StorageBuffer16BitAccessImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageBuffer16BitAccess;
            public new static StorageBuffer16BitAccessImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageBuffer16BitAccessImpl object.</summary>
            /// <returns>A string that represents the StorageBuffer16BitAccessImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageBuffer16BitAccess()";
            }
        }
        #endregion //StorageBuffer16BitAccess

        #region StorageUniformBufferBlock16
        public static StorageUniformBufferBlock16Impl StorageUniformBufferBlock16()
        {
            return StorageUniformBufferBlock16Impl.Instance;
            
        }

        internal class StorageUniformBufferBlock16Impl: Capability
        {
            public static readonly StorageUniformBufferBlock16Impl Instance = new StorageUniformBufferBlock16Impl();
        
            private  StorageUniformBufferBlock16Impl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageUniformBufferBlock16;
            public new static StorageUniformBufferBlock16Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageUniformBufferBlock16Impl object.</summary>
            /// <returns>A string that represents the StorageUniformBufferBlock16Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageUniformBufferBlock16()";
            }
        }
        #endregion //StorageUniformBufferBlock16

        #region UniformAndStorageBuffer16BitAccess
        public static UniformAndStorageBuffer16BitAccessImpl UniformAndStorageBuffer16BitAccess()
        {
            return UniformAndStorageBuffer16BitAccessImpl.Instance;
            
        }

        internal class UniformAndStorageBuffer16BitAccessImpl: Capability
        {
            public static readonly UniformAndStorageBuffer16BitAccessImpl Instance = new UniformAndStorageBuffer16BitAccessImpl();
        
            private  UniformAndStorageBuffer16BitAccessImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.UniformAndStorageBuffer16BitAccess;
            public new static UniformAndStorageBuffer16BitAccessImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UniformAndStorageBuffer16BitAccessImpl object.</summary>
            /// <returns>A string that represents the UniformAndStorageBuffer16BitAccessImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.UniformAndStorageBuffer16BitAccess()";
            }
        }
        #endregion //UniformAndStorageBuffer16BitAccess

        #region StorageUniform16
        public static StorageUniform16Impl StorageUniform16()
        {
            return StorageUniform16Impl.Instance;
            
        }

        internal class StorageUniform16Impl: Capability
        {
            public static readonly StorageUniform16Impl Instance = new StorageUniform16Impl();
        
            private  StorageUniform16Impl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageUniform16;
            public new static StorageUniform16Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageUniform16Impl object.</summary>
            /// <returns>A string that represents the StorageUniform16Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageUniform16()";
            }
        }
        #endregion //StorageUniform16

        #region StoragePushConstant16
        public static StoragePushConstant16Impl StoragePushConstant16()
        {
            return StoragePushConstant16Impl.Instance;
            
        }

        internal class StoragePushConstant16Impl: Capability
        {
            public static readonly StoragePushConstant16Impl Instance = new StoragePushConstant16Impl();
        
            private  StoragePushConstant16Impl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StoragePushConstant16;
            public new static StoragePushConstant16Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StoragePushConstant16Impl object.</summary>
            /// <returns>A string that represents the StoragePushConstant16Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StoragePushConstant16()";
            }
        }
        #endregion //StoragePushConstant16

        #region StorageInputOutput16
        public static StorageInputOutput16Impl StorageInputOutput16()
        {
            return StorageInputOutput16Impl.Instance;
            
        }

        internal class StorageInputOutput16Impl: Capability
        {
            public static readonly StorageInputOutput16Impl Instance = new StorageInputOutput16Impl();
        
            private  StorageInputOutput16Impl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageInputOutput16;
            public new static StorageInputOutput16Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageInputOutput16Impl object.</summary>
            /// <returns>A string that represents the StorageInputOutput16Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageInputOutput16()";
            }
        }
        #endregion //StorageInputOutput16

        #region DeviceGroup
        public static DeviceGroupImpl DeviceGroup()
        {
            return DeviceGroupImpl.Instance;
            
        }

        internal class DeviceGroupImpl: Capability
        {
            public static readonly DeviceGroupImpl Instance = new DeviceGroupImpl();
        
            private  DeviceGroupImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.DeviceGroup;
            public new static DeviceGroupImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DeviceGroupImpl object.</summary>
            /// <returns>A string that represents the DeviceGroupImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.DeviceGroup()";
            }
        }
        #endregion //DeviceGroup

        #region MultiView
        public static MultiViewImpl MultiView()
        {
            return MultiViewImpl.Instance;
            
        }

        internal class MultiViewImpl: Capability
        {
            public static readonly MultiViewImpl Instance = new MultiViewImpl();
        
            private  MultiViewImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.MultiView;
            public new static MultiViewImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the MultiViewImpl object.</summary>
            /// <returns>A string that represents the MultiViewImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.MultiView()";
            }
        }
        #endregion //MultiView

        #region VariablePointersStorageBuffer
        public static VariablePointersStorageBufferImpl VariablePointersStorageBuffer()
        {
            return VariablePointersStorageBufferImpl.Instance;
            
        }

        internal class VariablePointersStorageBufferImpl: Capability
        {
            public static readonly VariablePointersStorageBufferImpl Instance = new VariablePointersStorageBufferImpl();
        
            private  VariablePointersStorageBufferImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.VariablePointersStorageBuffer;
            public new static VariablePointersStorageBufferImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the VariablePointersStorageBufferImpl object.</summary>
            /// <returns>A string that represents the VariablePointersStorageBufferImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.VariablePointersStorageBuffer()";
            }
        }
        #endregion //VariablePointersStorageBuffer

        #region VariablePointers
        public static VariablePointersImpl VariablePointers()
        {
            return VariablePointersImpl.Instance;
            
        }

        internal class VariablePointersImpl: Capability
        {
            public static readonly VariablePointersImpl Instance = new VariablePointersImpl();
        
            private  VariablePointersImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.VariablePointers;
            public new static VariablePointersImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the VariablePointersImpl object.</summary>
            /// <returns>A string that represents the VariablePointersImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.VariablePointers()";
            }
        }
        #endregion //VariablePointers

        #region AtomicStorageOps
        public static AtomicStorageOpsImpl AtomicStorageOps()
        {
            return AtomicStorageOpsImpl.Instance;
            
        }

        internal class AtomicStorageOpsImpl: Capability
        {
            public static readonly AtomicStorageOpsImpl Instance = new AtomicStorageOpsImpl();
        
            private  AtomicStorageOpsImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.AtomicStorageOps;
            public new static AtomicStorageOpsImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the AtomicStorageOpsImpl object.</summary>
            /// <returns>A string that represents the AtomicStorageOpsImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.AtomicStorageOps()";
            }
        }
        #endregion //AtomicStorageOps

        #region SampleMaskPostDepthCoverage
        public static SampleMaskPostDepthCoverageImpl SampleMaskPostDepthCoverage()
        {
            return SampleMaskPostDepthCoverageImpl.Instance;
            
        }

        internal class SampleMaskPostDepthCoverageImpl: Capability
        {
            public static readonly SampleMaskPostDepthCoverageImpl Instance = new SampleMaskPostDepthCoverageImpl();
        
            private  SampleMaskPostDepthCoverageImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SampleMaskPostDepthCoverage;
            public new static SampleMaskPostDepthCoverageImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SampleMaskPostDepthCoverageImpl object.</summary>
            /// <returns>A string that represents the SampleMaskPostDepthCoverageImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SampleMaskPostDepthCoverage()";
            }
        }
        #endregion //SampleMaskPostDepthCoverage

        #region StorageBuffer8BitAccess
        public static StorageBuffer8BitAccessImpl StorageBuffer8BitAccess()
        {
            return StorageBuffer8BitAccessImpl.Instance;
            
        }

        internal class StorageBuffer8BitAccessImpl: Capability
        {
            public static readonly StorageBuffer8BitAccessImpl Instance = new StorageBuffer8BitAccessImpl();
        
            private  StorageBuffer8BitAccessImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageBuffer8BitAccess;
            public new static StorageBuffer8BitAccessImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageBuffer8BitAccessImpl object.</summary>
            /// <returns>A string that represents the StorageBuffer8BitAccessImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageBuffer8BitAccess()";
            }
        }
        #endregion //StorageBuffer8BitAccess

        #region UniformAndStorageBuffer8BitAccess
        public static UniformAndStorageBuffer8BitAccessImpl UniformAndStorageBuffer8BitAccess()
        {
            return UniformAndStorageBuffer8BitAccessImpl.Instance;
            
        }

        internal class UniformAndStorageBuffer8BitAccessImpl: Capability
        {
            public static readonly UniformAndStorageBuffer8BitAccessImpl Instance = new UniformAndStorageBuffer8BitAccessImpl();
        
            private  UniformAndStorageBuffer8BitAccessImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.UniformAndStorageBuffer8BitAccess;
            public new static UniformAndStorageBuffer8BitAccessImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UniformAndStorageBuffer8BitAccessImpl object.</summary>
            /// <returns>A string that represents the UniformAndStorageBuffer8BitAccessImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.UniformAndStorageBuffer8BitAccess()";
            }
        }
        #endregion //UniformAndStorageBuffer8BitAccess

        #region StoragePushConstant8
        public static StoragePushConstant8Impl StoragePushConstant8()
        {
            return StoragePushConstant8Impl.Instance;
            
        }

        internal class StoragePushConstant8Impl: Capability
        {
            public static readonly StoragePushConstant8Impl Instance = new StoragePushConstant8Impl();
        
            private  StoragePushConstant8Impl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StoragePushConstant8;
            public new static StoragePushConstant8Impl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StoragePushConstant8Impl object.</summary>
            /// <returns>A string that represents the StoragePushConstant8Impl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StoragePushConstant8()";
            }
        }
        #endregion //StoragePushConstant8

        #region DenormPreserve
        public static DenormPreserveImpl DenormPreserve()
        {
            return DenormPreserveImpl.Instance;
            
        }

        internal class DenormPreserveImpl: Capability
        {
            public static readonly DenormPreserveImpl Instance = new DenormPreserveImpl();
        
            private  DenormPreserveImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.DenormPreserve;
            public new static DenormPreserveImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DenormPreserveImpl object.</summary>
            /// <returns>A string that represents the DenormPreserveImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.DenormPreserve()";
            }
        }
        #endregion //DenormPreserve

        #region DenormFlushToZero
        public static DenormFlushToZeroImpl DenormFlushToZero()
        {
            return DenormFlushToZeroImpl.Instance;
            
        }

        internal class DenormFlushToZeroImpl: Capability
        {
            public static readonly DenormFlushToZeroImpl Instance = new DenormFlushToZeroImpl();
        
            private  DenormFlushToZeroImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.DenormFlushToZero;
            public new static DenormFlushToZeroImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DenormFlushToZeroImpl object.</summary>
            /// <returns>A string that represents the DenormFlushToZeroImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.DenormFlushToZero()";
            }
        }
        #endregion //DenormFlushToZero

        #region SignedZeroInfNanPreserve
        public static SignedZeroInfNanPreserveImpl SignedZeroInfNanPreserve()
        {
            return SignedZeroInfNanPreserveImpl.Instance;
            
        }

        internal class SignedZeroInfNanPreserveImpl: Capability
        {
            public static readonly SignedZeroInfNanPreserveImpl Instance = new SignedZeroInfNanPreserveImpl();
        
            private  SignedZeroInfNanPreserveImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SignedZeroInfNanPreserve;
            public new static SignedZeroInfNanPreserveImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SignedZeroInfNanPreserveImpl object.</summary>
            /// <returns>A string that represents the SignedZeroInfNanPreserveImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SignedZeroInfNanPreserve()";
            }
        }
        #endregion //SignedZeroInfNanPreserve

        #region RoundingModeRTE
        public static RoundingModeRTEImpl RoundingModeRTE()
        {
            return RoundingModeRTEImpl.Instance;
            
        }

        internal class RoundingModeRTEImpl: Capability
        {
            public static readonly RoundingModeRTEImpl Instance = new RoundingModeRTEImpl();
        
            private  RoundingModeRTEImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.RoundingModeRTE;
            public new static RoundingModeRTEImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RoundingModeRTEImpl object.</summary>
            /// <returns>A string that represents the RoundingModeRTEImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.RoundingModeRTE()";
            }
        }
        #endregion //RoundingModeRTE

        #region RoundingModeRTZ
        public static RoundingModeRTZImpl RoundingModeRTZ()
        {
            return RoundingModeRTZImpl.Instance;
            
        }

        internal class RoundingModeRTZImpl: Capability
        {
            public static readonly RoundingModeRTZImpl Instance = new RoundingModeRTZImpl();
        
            private  RoundingModeRTZImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.RoundingModeRTZ;
            public new static RoundingModeRTZImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RoundingModeRTZImpl object.</summary>
            /// <returns>A string that represents the RoundingModeRTZImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.RoundingModeRTZ()";
            }
        }
        #endregion //RoundingModeRTZ

        #region RayQueryProvisionalKHR
        public static RayQueryProvisionalKHRImpl RayQueryProvisionalKHR()
        {
            return RayQueryProvisionalKHRImpl.Instance;
            
        }

        internal class RayQueryProvisionalKHRImpl: Capability
        {
            public static readonly RayQueryProvisionalKHRImpl Instance = new RayQueryProvisionalKHRImpl();
        
            private  RayQueryProvisionalKHRImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.RayQueryProvisionalKHR;
            public new static RayQueryProvisionalKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayQueryProvisionalKHRImpl object.</summary>
            /// <returns>A string that represents the RayQueryProvisionalKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.RayQueryProvisionalKHR()";
            }
        }
        #endregion //RayQueryProvisionalKHR

        #region RayTraversalPrimitiveCullingProvisionalKHR
        public static RayTraversalPrimitiveCullingProvisionalKHRImpl RayTraversalPrimitiveCullingProvisionalKHR()
        {
            return RayTraversalPrimitiveCullingProvisionalKHRImpl.Instance;
            
        }

        internal class RayTraversalPrimitiveCullingProvisionalKHRImpl: Capability
        {
            public static readonly RayTraversalPrimitiveCullingProvisionalKHRImpl Instance = new RayTraversalPrimitiveCullingProvisionalKHRImpl();
        
            private  RayTraversalPrimitiveCullingProvisionalKHRImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.RayTraversalPrimitiveCullingProvisionalKHR;
            public new static RayTraversalPrimitiveCullingProvisionalKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayTraversalPrimitiveCullingProvisionalKHRImpl object.</summary>
            /// <returns>A string that represents the RayTraversalPrimitiveCullingProvisionalKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.RayTraversalPrimitiveCullingProvisionalKHR()";
            }
        }
        #endregion //RayTraversalPrimitiveCullingProvisionalKHR

        #region Float16ImageAMD
        public static Float16ImageAMDImpl Float16ImageAMD()
        {
            return Float16ImageAMDImpl.Instance;
            
        }

        internal class Float16ImageAMDImpl: Capability
        {
            public static readonly Float16ImageAMDImpl Instance = new Float16ImageAMDImpl();
        
            private  Float16ImageAMDImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.Float16ImageAMD;
            public new static Float16ImageAMDImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the Float16ImageAMDImpl object.</summary>
            /// <returns>A string that represents the Float16ImageAMDImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.Float16ImageAMD()";
            }
        }
        #endregion //Float16ImageAMD

        #region ImageGatherBiasLodAMD
        public static ImageGatherBiasLodAMDImpl ImageGatherBiasLodAMD()
        {
            return ImageGatherBiasLodAMDImpl.Instance;
            
        }

        internal class ImageGatherBiasLodAMDImpl: Capability
        {
            public static readonly ImageGatherBiasLodAMDImpl Instance = new ImageGatherBiasLodAMDImpl();
        
            private  ImageGatherBiasLodAMDImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ImageGatherBiasLodAMD;
            public new static ImageGatherBiasLodAMDImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ImageGatherBiasLodAMDImpl object.</summary>
            /// <returns>A string that represents the ImageGatherBiasLodAMDImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ImageGatherBiasLodAMD()";
            }
        }
        #endregion //ImageGatherBiasLodAMD

        #region FragmentMaskAMD
        public static FragmentMaskAMDImpl FragmentMaskAMD()
        {
            return FragmentMaskAMDImpl.Instance;
            
        }

        internal class FragmentMaskAMDImpl: Capability
        {
            public static readonly FragmentMaskAMDImpl Instance = new FragmentMaskAMDImpl();
        
            private  FragmentMaskAMDImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.FragmentMaskAMD;
            public new static FragmentMaskAMDImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FragmentMaskAMDImpl object.</summary>
            /// <returns>A string that represents the FragmentMaskAMDImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.FragmentMaskAMD()";
            }
        }
        #endregion //FragmentMaskAMD

        #region StencilExportEXT
        public static StencilExportEXTImpl StencilExportEXT()
        {
            return StencilExportEXTImpl.Instance;
            
        }

        internal class StencilExportEXTImpl: Capability
        {
            public static readonly StencilExportEXTImpl Instance = new StencilExportEXTImpl();
        
            private  StencilExportEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StencilExportEXT;
            public new static StencilExportEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StencilExportEXTImpl object.</summary>
            /// <returns>A string that represents the StencilExportEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StencilExportEXT()";
            }
        }
        #endregion //StencilExportEXT

        #region ImageReadWriteLodAMD
        public static ImageReadWriteLodAMDImpl ImageReadWriteLodAMD()
        {
            return ImageReadWriteLodAMDImpl.Instance;
            
        }

        internal class ImageReadWriteLodAMDImpl: Capability
        {
            public static readonly ImageReadWriteLodAMDImpl Instance = new ImageReadWriteLodAMDImpl();
        
            private  ImageReadWriteLodAMDImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ImageReadWriteLodAMD;
            public new static ImageReadWriteLodAMDImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ImageReadWriteLodAMDImpl object.</summary>
            /// <returns>A string that represents the ImageReadWriteLodAMDImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ImageReadWriteLodAMD()";
            }
        }
        #endregion //ImageReadWriteLodAMD

        #region ShaderClockKHR
        public static ShaderClockKHRImpl ShaderClockKHR()
        {
            return ShaderClockKHRImpl.Instance;
            
        }

        internal class ShaderClockKHRImpl: Capability
        {
            public static readonly ShaderClockKHRImpl Instance = new ShaderClockKHRImpl();
        
            private  ShaderClockKHRImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ShaderClockKHR;
            public new static ShaderClockKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShaderClockKHRImpl object.</summary>
            /// <returns>A string that represents the ShaderClockKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ShaderClockKHR()";
            }
        }
        #endregion //ShaderClockKHR

        #region SampleMaskOverrideCoverageNV
        public static SampleMaskOverrideCoverageNVImpl SampleMaskOverrideCoverageNV()
        {
            return SampleMaskOverrideCoverageNVImpl.Instance;
            
        }

        internal class SampleMaskOverrideCoverageNVImpl: Capability
        {
            public static readonly SampleMaskOverrideCoverageNVImpl Instance = new SampleMaskOverrideCoverageNVImpl();
        
            private  SampleMaskOverrideCoverageNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SampleMaskOverrideCoverageNV;
            public new static SampleMaskOverrideCoverageNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SampleMaskOverrideCoverageNVImpl object.</summary>
            /// <returns>A string that represents the SampleMaskOverrideCoverageNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SampleMaskOverrideCoverageNV()";
            }
        }
        #endregion //SampleMaskOverrideCoverageNV

        #region GeometryShaderPassthroughNV
        public static GeometryShaderPassthroughNVImpl GeometryShaderPassthroughNV()
        {
            return GeometryShaderPassthroughNVImpl.Instance;
            
        }

        internal class GeometryShaderPassthroughNVImpl: Capability
        {
            public static readonly GeometryShaderPassthroughNVImpl Instance = new GeometryShaderPassthroughNVImpl();
        
            private  GeometryShaderPassthroughNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.GeometryShaderPassthroughNV;
            public new static GeometryShaderPassthroughNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GeometryShaderPassthroughNVImpl object.</summary>
            /// <returns>A string that represents the GeometryShaderPassthroughNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.GeometryShaderPassthroughNV()";
            }
        }
        #endregion //GeometryShaderPassthroughNV

        #region ShaderViewportIndexLayerEXT
        public static ShaderViewportIndexLayerEXTImpl ShaderViewportIndexLayerEXT()
        {
            return ShaderViewportIndexLayerEXTImpl.Instance;
            
        }

        internal class ShaderViewportIndexLayerEXTImpl: Capability
        {
            public static readonly ShaderViewportIndexLayerEXTImpl Instance = new ShaderViewportIndexLayerEXTImpl();
        
            private  ShaderViewportIndexLayerEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ShaderViewportIndexLayerEXT;
            public new static ShaderViewportIndexLayerEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShaderViewportIndexLayerEXTImpl object.</summary>
            /// <returns>A string that represents the ShaderViewportIndexLayerEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ShaderViewportIndexLayerEXT()";
            }
        }
        #endregion //ShaderViewportIndexLayerEXT

        #region ShaderViewportIndexLayerNV
        public static ShaderViewportIndexLayerNVImpl ShaderViewportIndexLayerNV()
        {
            return ShaderViewportIndexLayerNVImpl.Instance;
            
        }

        internal class ShaderViewportIndexLayerNVImpl: Capability
        {
            public static readonly ShaderViewportIndexLayerNVImpl Instance = new ShaderViewportIndexLayerNVImpl();
        
            private  ShaderViewportIndexLayerNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ShaderViewportIndexLayerNV;
            public new static ShaderViewportIndexLayerNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShaderViewportIndexLayerNVImpl object.</summary>
            /// <returns>A string that represents the ShaderViewportIndexLayerNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ShaderViewportIndexLayerNV()";
            }
        }
        #endregion //ShaderViewportIndexLayerNV

        #region ShaderViewportMaskNV
        public static ShaderViewportMaskNVImpl ShaderViewportMaskNV()
        {
            return ShaderViewportMaskNVImpl.Instance;
            
        }

        internal class ShaderViewportMaskNVImpl: Capability
        {
            public static readonly ShaderViewportMaskNVImpl Instance = new ShaderViewportMaskNVImpl();
        
            private  ShaderViewportMaskNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ShaderViewportMaskNV;
            public new static ShaderViewportMaskNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShaderViewportMaskNVImpl object.</summary>
            /// <returns>A string that represents the ShaderViewportMaskNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ShaderViewportMaskNV()";
            }
        }
        #endregion //ShaderViewportMaskNV

        #region ShaderStereoViewNV
        public static ShaderStereoViewNVImpl ShaderStereoViewNV()
        {
            return ShaderStereoViewNVImpl.Instance;
            
        }

        internal class ShaderStereoViewNVImpl: Capability
        {
            public static readonly ShaderStereoViewNVImpl Instance = new ShaderStereoViewNVImpl();
        
            private  ShaderStereoViewNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ShaderStereoViewNV;
            public new static ShaderStereoViewNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShaderStereoViewNVImpl object.</summary>
            /// <returns>A string that represents the ShaderStereoViewNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ShaderStereoViewNV()";
            }
        }
        #endregion //ShaderStereoViewNV

        #region PerViewAttributesNV
        public static PerViewAttributesNVImpl PerViewAttributesNV()
        {
            return PerViewAttributesNVImpl.Instance;
            
        }

        internal class PerViewAttributesNVImpl: Capability
        {
            public static readonly PerViewAttributesNVImpl Instance = new PerViewAttributesNVImpl();
        
            private  PerViewAttributesNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.PerViewAttributesNV;
            public new static PerViewAttributesNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PerViewAttributesNVImpl object.</summary>
            /// <returns>A string that represents the PerViewAttributesNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.PerViewAttributesNV()";
            }
        }
        #endregion //PerViewAttributesNV

        #region FragmentFullyCoveredEXT
        public static FragmentFullyCoveredEXTImpl FragmentFullyCoveredEXT()
        {
            return FragmentFullyCoveredEXTImpl.Instance;
            
        }

        internal class FragmentFullyCoveredEXTImpl: Capability
        {
            public static readonly FragmentFullyCoveredEXTImpl Instance = new FragmentFullyCoveredEXTImpl();
        
            private  FragmentFullyCoveredEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.FragmentFullyCoveredEXT;
            public new static FragmentFullyCoveredEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FragmentFullyCoveredEXTImpl object.</summary>
            /// <returns>A string that represents the FragmentFullyCoveredEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.FragmentFullyCoveredEXT()";
            }
        }
        #endregion //FragmentFullyCoveredEXT

        #region MeshShadingNV
        public static MeshShadingNVImpl MeshShadingNV()
        {
            return MeshShadingNVImpl.Instance;
            
        }

        internal class MeshShadingNVImpl: Capability
        {
            public static readonly MeshShadingNVImpl Instance = new MeshShadingNVImpl();
        
            private  MeshShadingNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.MeshShadingNV;
            public new static MeshShadingNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the MeshShadingNVImpl object.</summary>
            /// <returns>A string that represents the MeshShadingNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.MeshShadingNV()";
            }
        }
        #endregion //MeshShadingNV

        #region ImageFootprintNV
        public static ImageFootprintNVImpl ImageFootprintNV()
        {
            return ImageFootprintNVImpl.Instance;
            
        }

        internal class ImageFootprintNVImpl: Capability
        {
            public static readonly ImageFootprintNVImpl Instance = new ImageFootprintNVImpl();
        
            private  ImageFootprintNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ImageFootprintNV;
            public new static ImageFootprintNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ImageFootprintNVImpl object.</summary>
            /// <returns>A string that represents the ImageFootprintNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ImageFootprintNV()";
            }
        }
        #endregion //ImageFootprintNV

        #region FragmentBarycentricNV
        public static FragmentBarycentricNVImpl FragmentBarycentricNV()
        {
            return FragmentBarycentricNVImpl.Instance;
            
        }

        internal class FragmentBarycentricNVImpl: Capability
        {
            public static readonly FragmentBarycentricNVImpl Instance = new FragmentBarycentricNVImpl();
        
            private  FragmentBarycentricNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.FragmentBarycentricNV;
            public new static FragmentBarycentricNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FragmentBarycentricNVImpl object.</summary>
            /// <returns>A string that represents the FragmentBarycentricNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.FragmentBarycentricNV()";
            }
        }
        #endregion //FragmentBarycentricNV

        #region ComputeDerivativeGroupQuadsNV
        public static ComputeDerivativeGroupQuadsNVImpl ComputeDerivativeGroupQuadsNV()
        {
            return ComputeDerivativeGroupQuadsNVImpl.Instance;
            
        }

        internal class ComputeDerivativeGroupQuadsNVImpl: Capability
        {
            public static readonly ComputeDerivativeGroupQuadsNVImpl Instance = new ComputeDerivativeGroupQuadsNVImpl();
        
            private  ComputeDerivativeGroupQuadsNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ComputeDerivativeGroupQuadsNV;
            public new static ComputeDerivativeGroupQuadsNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ComputeDerivativeGroupQuadsNVImpl object.</summary>
            /// <returns>A string that represents the ComputeDerivativeGroupQuadsNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ComputeDerivativeGroupQuadsNV()";
            }
        }
        #endregion //ComputeDerivativeGroupQuadsNV

        #region FragmentDensityEXT
        public static FragmentDensityEXTImpl FragmentDensityEXT()
        {
            return FragmentDensityEXTImpl.Instance;
            
        }

        internal class FragmentDensityEXTImpl: Capability
        {
            public static readonly FragmentDensityEXTImpl Instance = new FragmentDensityEXTImpl();
        
            private  FragmentDensityEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.FragmentDensityEXT;
            public new static FragmentDensityEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FragmentDensityEXTImpl object.</summary>
            /// <returns>A string that represents the FragmentDensityEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.FragmentDensityEXT()";
            }
        }
        #endregion //FragmentDensityEXT

        #region ShadingRateNV
        public static ShadingRateNVImpl ShadingRateNV()
        {
            return ShadingRateNVImpl.Instance;
            
        }

        internal class ShadingRateNVImpl: Capability
        {
            public static readonly ShadingRateNVImpl Instance = new ShadingRateNVImpl();
        
            private  ShadingRateNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ShadingRateNV;
            public new static ShadingRateNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShadingRateNVImpl object.</summary>
            /// <returns>A string that represents the ShadingRateNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ShadingRateNV()";
            }
        }
        #endregion //ShadingRateNV

        #region GroupNonUniformPartitionedNV
        public static GroupNonUniformPartitionedNVImpl GroupNonUniformPartitionedNV()
        {
            return GroupNonUniformPartitionedNVImpl.Instance;
            
        }

        internal class GroupNonUniformPartitionedNVImpl: Capability
        {
            public static readonly GroupNonUniformPartitionedNVImpl Instance = new GroupNonUniformPartitionedNVImpl();
        
            private  GroupNonUniformPartitionedNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.GroupNonUniformPartitionedNV;
            public new static GroupNonUniformPartitionedNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the GroupNonUniformPartitionedNVImpl object.</summary>
            /// <returns>A string that represents the GroupNonUniformPartitionedNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.GroupNonUniformPartitionedNV()";
            }
        }
        #endregion //GroupNonUniformPartitionedNV

        #region ShaderNonUniform
        public static ShaderNonUniformImpl ShaderNonUniform()
        {
            return ShaderNonUniformImpl.Instance;
            
        }

        internal class ShaderNonUniformImpl: Capability
        {
            public static readonly ShaderNonUniformImpl Instance = new ShaderNonUniformImpl();
        
            private  ShaderNonUniformImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ShaderNonUniform;
            public new static ShaderNonUniformImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShaderNonUniformImpl object.</summary>
            /// <returns>A string that represents the ShaderNonUniformImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ShaderNonUniform()";
            }
        }
        #endregion //ShaderNonUniform

        #region ShaderNonUniformEXT
        public static ShaderNonUniformEXTImpl ShaderNonUniformEXT()
        {
            return ShaderNonUniformEXTImpl.Instance;
            
        }

        internal class ShaderNonUniformEXTImpl: Capability
        {
            public static readonly ShaderNonUniformEXTImpl Instance = new ShaderNonUniformEXTImpl();
        
            private  ShaderNonUniformEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ShaderNonUniformEXT;
            public new static ShaderNonUniformEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShaderNonUniformEXTImpl object.</summary>
            /// <returns>A string that represents the ShaderNonUniformEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ShaderNonUniformEXT()";
            }
        }
        #endregion //ShaderNonUniformEXT

        #region RuntimeDescriptorArray
        public static RuntimeDescriptorArrayImpl RuntimeDescriptorArray()
        {
            return RuntimeDescriptorArrayImpl.Instance;
            
        }

        internal class RuntimeDescriptorArrayImpl: Capability
        {
            public static readonly RuntimeDescriptorArrayImpl Instance = new RuntimeDescriptorArrayImpl();
        
            private  RuntimeDescriptorArrayImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.RuntimeDescriptorArray;
            public new static RuntimeDescriptorArrayImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RuntimeDescriptorArrayImpl object.</summary>
            /// <returns>A string that represents the RuntimeDescriptorArrayImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.RuntimeDescriptorArray()";
            }
        }
        #endregion //RuntimeDescriptorArray

        #region RuntimeDescriptorArrayEXT
        public static RuntimeDescriptorArrayEXTImpl RuntimeDescriptorArrayEXT()
        {
            return RuntimeDescriptorArrayEXTImpl.Instance;
            
        }

        internal class RuntimeDescriptorArrayEXTImpl: Capability
        {
            public static readonly RuntimeDescriptorArrayEXTImpl Instance = new RuntimeDescriptorArrayEXTImpl();
        
            private  RuntimeDescriptorArrayEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.RuntimeDescriptorArrayEXT;
            public new static RuntimeDescriptorArrayEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RuntimeDescriptorArrayEXTImpl object.</summary>
            /// <returns>A string that represents the RuntimeDescriptorArrayEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.RuntimeDescriptorArrayEXT()";
            }
        }
        #endregion //RuntimeDescriptorArrayEXT

        #region InputAttachmentArrayDynamicIndexing
        public static InputAttachmentArrayDynamicIndexingImpl InputAttachmentArrayDynamicIndexing()
        {
            return InputAttachmentArrayDynamicIndexingImpl.Instance;
            
        }

        internal class InputAttachmentArrayDynamicIndexingImpl: Capability
        {
            public static readonly InputAttachmentArrayDynamicIndexingImpl Instance = new InputAttachmentArrayDynamicIndexingImpl();
        
            private  InputAttachmentArrayDynamicIndexingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.InputAttachmentArrayDynamicIndexing;
            public new static InputAttachmentArrayDynamicIndexingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InputAttachmentArrayDynamicIndexingImpl object.</summary>
            /// <returns>A string that represents the InputAttachmentArrayDynamicIndexingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.InputAttachmentArrayDynamicIndexing()";
            }
        }
        #endregion //InputAttachmentArrayDynamicIndexing

        #region InputAttachmentArrayDynamicIndexingEXT
        public static InputAttachmentArrayDynamicIndexingEXTImpl InputAttachmentArrayDynamicIndexingEXT()
        {
            return InputAttachmentArrayDynamicIndexingEXTImpl.Instance;
            
        }

        internal class InputAttachmentArrayDynamicIndexingEXTImpl: Capability
        {
            public static readonly InputAttachmentArrayDynamicIndexingEXTImpl Instance = new InputAttachmentArrayDynamicIndexingEXTImpl();
        
            private  InputAttachmentArrayDynamicIndexingEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.InputAttachmentArrayDynamicIndexingEXT;
            public new static InputAttachmentArrayDynamicIndexingEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InputAttachmentArrayDynamicIndexingEXTImpl object.</summary>
            /// <returns>A string that represents the InputAttachmentArrayDynamicIndexingEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.InputAttachmentArrayDynamicIndexingEXT()";
            }
        }
        #endregion //InputAttachmentArrayDynamicIndexingEXT

        #region UniformTexelBufferArrayDynamicIndexing
        public static UniformTexelBufferArrayDynamicIndexingImpl UniformTexelBufferArrayDynamicIndexing()
        {
            return UniformTexelBufferArrayDynamicIndexingImpl.Instance;
            
        }

        internal class UniformTexelBufferArrayDynamicIndexingImpl: Capability
        {
            public static readonly UniformTexelBufferArrayDynamicIndexingImpl Instance = new UniformTexelBufferArrayDynamicIndexingImpl();
        
            private  UniformTexelBufferArrayDynamicIndexingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.UniformTexelBufferArrayDynamicIndexing;
            public new static UniformTexelBufferArrayDynamicIndexingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UniformTexelBufferArrayDynamicIndexingImpl object.</summary>
            /// <returns>A string that represents the UniformTexelBufferArrayDynamicIndexingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.UniformTexelBufferArrayDynamicIndexing()";
            }
        }
        #endregion //UniformTexelBufferArrayDynamicIndexing

        #region UniformTexelBufferArrayDynamicIndexingEXT
        public static UniformTexelBufferArrayDynamicIndexingEXTImpl UniformTexelBufferArrayDynamicIndexingEXT()
        {
            return UniformTexelBufferArrayDynamicIndexingEXTImpl.Instance;
            
        }

        internal class UniformTexelBufferArrayDynamicIndexingEXTImpl: Capability
        {
            public static readonly UniformTexelBufferArrayDynamicIndexingEXTImpl Instance = new UniformTexelBufferArrayDynamicIndexingEXTImpl();
        
            private  UniformTexelBufferArrayDynamicIndexingEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.UniformTexelBufferArrayDynamicIndexingEXT;
            public new static UniformTexelBufferArrayDynamicIndexingEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UniformTexelBufferArrayDynamicIndexingEXTImpl object.</summary>
            /// <returns>A string that represents the UniformTexelBufferArrayDynamicIndexingEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.UniformTexelBufferArrayDynamicIndexingEXT()";
            }
        }
        #endregion //UniformTexelBufferArrayDynamicIndexingEXT

        #region StorageTexelBufferArrayDynamicIndexing
        public static StorageTexelBufferArrayDynamicIndexingImpl StorageTexelBufferArrayDynamicIndexing()
        {
            return StorageTexelBufferArrayDynamicIndexingImpl.Instance;
            
        }

        internal class StorageTexelBufferArrayDynamicIndexingImpl: Capability
        {
            public static readonly StorageTexelBufferArrayDynamicIndexingImpl Instance = new StorageTexelBufferArrayDynamicIndexingImpl();
        
            private  StorageTexelBufferArrayDynamicIndexingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageTexelBufferArrayDynamicIndexing;
            public new static StorageTexelBufferArrayDynamicIndexingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageTexelBufferArrayDynamicIndexingImpl object.</summary>
            /// <returns>A string that represents the StorageTexelBufferArrayDynamicIndexingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageTexelBufferArrayDynamicIndexing()";
            }
        }
        #endregion //StorageTexelBufferArrayDynamicIndexing

        #region StorageTexelBufferArrayDynamicIndexingEXT
        public static StorageTexelBufferArrayDynamicIndexingEXTImpl StorageTexelBufferArrayDynamicIndexingEXT()
        {
            return StorageTexelBufferArrayDynamicIndexingEXTImpl.Instance;
            
        }

        internal class StorageTexelBufferArrayDynamicIndexingEXTImpl: Capability
        {
            public static readonly StorageTexelBufferArrayDynamicIndexingEXTImpl Instance = new StorageTexelBufferArrayDynamicIndexingEXTImpl();
        
            private  StorageTexelBufferArrayDynamicIndexingEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageTexelBufferArrayDynamicIndexingEXT;
            public new static StorageTexelBufferArrayDynamicIndexingEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageTexelBufferArrayDynamicIndexingEXTImpl object.</summary>
            /// <returns>A string that represents the StorageTexelBufferArrayDynamicIndexingEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageTexelBufferArrayDynamicIndexingEXT()";
            }
        }
        #endregion //StorageTexelBufferArrayDynamicIndexingEXT

        #region UniformBufferArrayNonUniformIndexing
        public static UniformBufferArrayNonUniformIndexingImpl UniformBufferArrayNonUniformIndexing()
        {
            return UniformBufferArrayNonUniformIndexingImpl.Instance;
            
        }

        internal class UniformBufferArrayNonUniformIndexingImpl: Capability
        {
            public static readonly UniformBufferArrayNonUniformIndexingImpl Instance = new UniformBufferArrayNonUniformIndexingImpl();
        
            private  UniformBufferArrayNonUniformIndexingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.UniformBufferArrayNonUniformIndexing;
            public new static UniformBufferArrayNonUniformIndexingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UniformBufferArrayNonUniformIndexingImpl object.</summary>
            /// <returns>A string that represents the UniformBufferArrayNonUniformIndexingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.UniformBufferArrayNonUniformIndexing()";
            }
        }
        #endregion //UniformBufferArrayNonUniformIndexing

        #region UniformBufferArrayNonUniformIndexingEXT
        public static UniformBufferArrayNonUniformIndexingEXTImpl UniformBufferArrayNonUniformIndexingEXT()
        {
            return UniformBufferArrayNonUniformIndexingEXTImpl.Instance;
            
        }

        internal class UniformBufferArrayNonUniformIndexingEXTImpl: Capability
        {
            public static readonly UniformBufferArrayNonUniformIndexingEXTImpl Instance = new UniformBufferArrayNonUniformIndexingEXTImpl();
        
            private  UniformBufferArrayNonUniformIndexingEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.UniformBufferArrayNonUniformIndexingEXT;
            public new static UniformBufferArrayNonUniformIndexingEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UniformBufferArrayNonUniformIndexingEXTImpl object.</summary>
            /// <returns>A string that represents the UniformBufferArrayNonUniformIndexingEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.UniformBufferArrayNonUniformIndexingEXT()";
            }
        }
        #endregion //UniformBufferArrayNonUniformIndexingEXT

        #region SampledImageArrayNonUniformIndexing
        public static SampledImageArrayNonUniformIndexingImpl SampledImageArrayNonUniformIndexing()
        {
            return SampledImageArrayNonUniformIndexingImpl.Instance;
            
        }

        internal class SampledImageArrayNonUniformIndexingImpl: Capability
        {
            public static readonly SampledImageArrayNonUniformIndexingImpl Instance = new SampledImageArrayNonUniformIndexingImpl();
        
            private  SampledImageArrayNonUniformIndexingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SampledImageArrayNonUniformIndexing;
            public new static SampledImageArrayNonUniformIndexingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SampledImageArrayNonUniformIndexingImpl object.</summary>
            /// <returns>A string that represents the SampledImageArrayNonUniformIndexingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SampledImageArrayNonUniformIndexing()";
            }
        }
        #endregion //SampledImageArrayNonUniformIndexing

        #region SampledImageArrayNonUniformIndexingEXT
        public static SampledImageArrayNonUniformIndexingEXTImpl SampledImageArrayNonUniformIndexingEXT()
        {
            return SampledImageArrayNonUniformIndexingEXTImpl.Instance;
            
        }

        internal class SampledImageArrayNonUniformIndexingEXTImpl: Capability
        {
            public static readonly SampledImageArrayNonUniformIndexingEXTImpl Instance = new SampledImageArrayNonUniformIndexingEXTImpl();
        
            private  SampledImageArrayNonUniformIndexingEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SampledImageArrayNonUniformIndexingEXT;
            public new static SampledImageArrayNonUniformIndexingEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SampledImageArrayNonUniformIndexingEXTImpl object.</summary>
            /// <returns>A string that represents the SampledImageArrayNonUniformIndexingEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SampledImageArrayNonUniformIndexingEXT()";
            }
        }
        #endregion //SampledImageArrayNonUniformIndexingEXT

        #region StorageBufferArrayNonUniformIndexing
        public static StorageBufferArrayNonUniformIndexingImpl StorageBufferArrayNonUniformIndexing()
        {
            return StorageBufferArrayNonUniformIndexingImpl.Instance;
            
        }

        internal class StorageBufferArrayNonUniformIndexingImpl: Capability
        {
            public static readonly StorageBufferArrayNonUniformIndexingImpl Instance = new StorageBufferArrayNonUniformIndexingImpl();
        
            private  StorageBufferArrayNonUniformIndexingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageBufferArrayNonUniformIndexing;
            public new static StorageBufferArrayNonUniformIndexingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageBufferArrayNonUniformIndexingImpl object.</summary>
            /// <returns>A string that represents the StorageBufferArrayNonUniformIndexingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageBufferArrayNonUniformIndexing()";
            }
        }
        #endregion //StorageBufferArrayNonUniformIndexing

        #region StorageBufferArrayNonUniformIndexingEXT
        public static StorageBufferArrayNonUniformIndexingEXTImpl StorageBufferArrayNonUniformIndexingEXT()
        {
            return StorageBufferArrayNonUniformIndexingEXTImpl.Instance;
            
        }

        internal class StorageBufferArrayNonUniformIndexingEXTImpl: Capability
        {
            public static readonly StorageBufferArrayNonUniformIndexingEXTImpl Instance = new StorageBufferArrayNonUniformIndexingEXTImpl();
        
            private  StorageBufferArrayNonUniformIndexingEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageBufferArrayNonUniformIndexingEXT;
            public new static StorageBufferArrayNonUniformIndexingEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageBufferArrayNonUniformIndexingEXTImpl object.</summary>
            /// <returns>A string that represents the StorageBufferArrayNonUniformIndexingEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageBufferArrayNonUniformIndexingEXT()";
            }
        }
        #endregion //StorageBufferArrayNonUniformIndexingEXT

        #region StorageImageArrayNonUniformIndexing
        public static StorageImageArrayNonUniformIndexingImpl StorageImageArrayNonUniformIndexing()
        {
            return StorageImageArrayNonUniformIndexingImpl.Instance;
            
        }

        internal class StorageImageArrayNonUniformIndexingImpl: Capability
        {
            public static readonly StorageImageArrayNonUniformIndexingImpl Instance = new StorageImageArrayNonUniformIndexingImpl();
        
            private  StorageImageArrayNonUniformIndexingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageImageArrayNonUniformIndexing;
            public new static StorageImageArrayNonUniformIndexingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageImageArrayNonUniformIndexingImpl object.</summary>
            /// <returns>A string that represents the StorageImageArrayNonUniformIndexingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageImageArrayNonUniformIndexing()";
            }
        }
        #endregion //StorageImageArrayNonUniformIndexing

        #region StorageImageArrayNonUniformIndexingEXT
        public static StorageImageArrayNonUniformIndexingEXTImpl StorageImageArrayNonUniformIndexingEXT()
        {
            return StorageImageArrayNonUniformIndexingEXTImpl.Instance;
            
        }

        internal class StorageImageArrayNonUniformIndexingEXTImpl: Capability
        {
            public static readonly StorageImageArrayNonUniformIndexingEXTImpl Instance = new StorageImageArrayNonUniformIndexingEXTImpl();
        
            private  StorageImageArrayNonUniformIndexingEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageImageArrayNonUniformIndexingEXT;
            public new static StorageImageArrayNonUniformIndexingEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageImageArrayNonUniformIndexingEXTImpl object.</summary>
            /// <returns>A string that represents the StorageImageArrayNonUniformIndexingEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageImageArrayNonUniformIndexingEXT()";
            }
        }
        #endregion //StorageImageArrayNonUniformIndexingEXT

        #region InputAttachmentArrayNonUniformIndexing
        public static InputAttachmentArrayNonUniformIndexingImpl InputAttachmentArrayNonUniformIndexing()
        {
            return InputAttachmentArrayNonUniformIndexingImpl.Instance;
            
        }

        internal class InputAttachmentArrayNonUniformIndexingImpl: Capability
        {
            public static readonly InputAttachmentArrayNonUniformIndexingImpl Instance = new InputAttachmentArrayNonUniformIndexingImpl();
        
            private  InputAttachmentArrayNonUniformIndexingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.InputAttachmentArrayNonUniformIndexing;
            public new static InputAttachmentArrayNonUniformIndexingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InputAttachmentArrayNonUniformIndexingImpl object.</summary>
            /// <returns>A string that represents the InputAttachmentArrayNonUniformIndexingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.InputAttachmentArrayNonUniformIndexing()";
            }
        }
        #endregion //InputAttachmentArrayNonUniformIndexing

        #region InputAttachmentArrayNonUniformIndexingEXT
        public static InputAttachmentArrayNonUniformIndexingEXTImpl InputAttachmentArrayNonUniformIndexingEXT()
        {
            return InputAttachmentArrayNonUniformIndexingEXTImpl.Instance;
            
        }

        internal class InputAttachmentArrayNonUniformIndexingEXTImpl: Capability
        {
            public static readonly InputAttachmentArrayNonUniformIndexingEXTImpl Instance = new InputAttachmentArrayNonUniformIndexingEXTImpl();
        
            private  InputAttachmentArrayNonUniformIndexingEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.InputAttachmentArrayNonUniformIndexingEXT;
            public new static InputAttachmentArrayNonUniformIndexingEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the InputAttachmentArrayNonUniformIndexingEXTImpl object.</summary>
            /// <returns>A string that represents the InputAttachmentArrayNonUniformIndexingEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.InputAttachmentArrayNonUniformIndexingEXT()";
            }
        }
        #endregion //InputAttachmentArrayNonUniformIndexingEXT

        #region UniformTexelBufferArrayNonUniformIndexing
        public static UniformTexelBufferArrayNonUniformIndexingImpl UniformTexelBufferArrayNonUniformIndexing()
        {
            return UniformTexelBufferArrayNonUniformIndexingImpl.Instance;
            
        }

        internal class UniformTexelBufferArrayNonUniformIndexingImpl: Capability
        {
            public static readonly UniformTexelBufferArrayNonUniformIndexingImpl Instance = new UniformTexelBufferArrayNonUniformIndexingImpl();
        
            private  UniformTexelBufferArrayNonUniformIndexingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.UniformTexelBufferArrayNonUniformIndexing;
            public new static UniformTexelBufferArrayNonUniformIndexingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UniformTexelBufferArrayNonUniformIndexingImpl object.</summary>
            /// <returns>A string that represents the UniformTexelBufferArrayNonUniformIndexingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.UniformTexelBufferArrayNonUniformIndexing()";
            }
        }
        #endregion //UniformTexelBufferArrayNonUniformIndexing

        #region UniformTexelBufferArrayNonUniformIndexingEXT
        public static UniformTexelBufferArrayNonUniformIndexingEXTImpl UniformTexelBufferArrayNonUniformIndexingEXT()
        {
            return UniformTexelBufferArrayNonUniformIndexingEXTImpl.Instance;
            
        }

        internal class UniformTexelBufferArrayNonUniformIndexingEXTImpl: Capability
        {
            public static readonly UniformTexelBufferArrayNonUniformIndexingEXTImpl Instance = new UniformTexelBufferArrayNonUniformIndexingEXTImpl();
        
            private  UniformTexelBufferArrayNonUniformIndexingEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.UniformTexelBufferArrayNonUniformIndexingEXT;
            public new static UniformTexelBufferArrayNonUniformIndexingEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the UniformTexelBufferArrayNonUniformIndexingEXTImpl object.</summary>
            /// <returns>A string that represents the UniformTexelBufferArrayNonUniformIndexingEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.UniformTexelBufferArrayNonUniformIndexingEXT()";
            }
        }
        #endregion //UniformTexelBufferArrayNonUniformIndexingEXT

        #region StorageTexelBufferArrayNonUniformIndexing
        public static StorageTexelBufferArrayNonUniformIndexingImpl StorageTexelBufferArrayNonUniformIndexing()
        {
            return StorageTexelBufferArrayNonUniformIndexingImpl.Instance;
            
        }

        internal class StorageTexelBufferArrayNonUniformIndexingImpl: Capability
        {
            public static readonly StorageTexelBufferArrayNonUniformIndexingImpl Instance = new StorageTexelBufferArrayNonUniformIndexingImpl();
        
            private  StorageTexelBufferArrayNonUniformIndexingImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageTexelBufferArrayNonUniformIndexing;
            public new static StorageTexelBufferArrayNonUniformIndexingImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageTexelBufferArrayNonUniformIndexingImpl object.</summary>
            /// <returns>A string that represents the StorageTexelBufferArrayNonUniformIndexingImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageTexelBufferArrayNonUniformIndexing()";
            }
        }
        #endregion //StorageTexelBufferArrayNonUniformIndexing

        #region StorageTexelBufferArrayNonUniformIndexingEXT
        public static StorageTexelBufferArrayNonUniformIndexingEXTImpl StorageTexelBufferArrayNonUniformIndexingEXT()
        {
            return StorageTexelBufferArrayNonUniformIndexingEXTImpl.Instance;
            
        }

        internal class StorageTexelBufferArrayNonUniformIndexingEXTImpl: Capability
        {
            public static readonly StorageTexelBufferArrayNonUniformIndexingEXTImpl Instance = new StorageTexelBufferArrayNonUniformIndexingEXTImpl();
        
            private  StorageTexelBufferArrayNonUniformIndexingEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.StorageTexelBufferArrayNonUniformIndexingEXT;
            public new static StorageTexelBufferArrayNonUniformIndexingEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the StorageTexelBufferArrayNonUniformIndexingEXTImpl object.</summary>
            /// <returns>A string that represents the StorageTexelBufferArrayNonUniformIndexingEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.StorageTexelBufferArrayNonUniformIndexingEXT()";
            }
        }
        #endregion //StorageTexelBufferArrayNonUniformIndexingEXT

        #region RayTracingNV
        public static RayTracingNVImpl RayTracingNV()
        {
            return RayTracingNVImpl.Instance;
            
        }

        internal class RayTracingNVImpl: Capability
        {
            public static readonly RayTracingNVImpl Instance = new RayTracingNVImpl();
        
            private  RayTracingNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.RayTracingNV;
            public new static RayTracingNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayTracingNVImpl object.</summary>
            /// <returns>A string that represents the RayTracingNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.RayTracingNV()";
            }
        }
        #endregion //RayTracingNV

        #region VulkanMemoryModel
        public static VulkanMemoryModelImpl VulkanMemoryModel()
        {
            return VulkanMemoryModelImpl.Instance;
            
        }

        internal class VulkanMemoryModelImpl: Capability
        {
            public static readonly VulkanMemoryModelImpl Instance = new VulkanMemoryModelImpl();
        
            private  VulkanMemoryModelImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.VulkanMemoryModel;
            public new static VulkanMemoryModelImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the VulkanMemoryModelImpl object.</summary>
            /// <returns>A string that represents the VulkanMemoryModelImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.VulkanMemoryModel()";
            }
        }
        #endregion //VulkanMemoryModel

        #region VulkanMemoryModelKHR
        public static VulkanMemoryModelKHRImpl VulkanMemoryModelKHR()
        {
            return VulkanMemoryModelKHRImpl.Instance;
            
        }

        internal class VulkanMemoryModelKHRImpl: Capability
        {
            public static readonly VulkanMemoryModelKHRImpl Instance = new VulkanMemoryModelKHRImpl();
        
            private  VulkanMemoryModelKHRImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.VulkanMemoryModelKHR;
            public new static VulkanMemoryModelKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the VulkanMemoryModelKHRImpl object.</summary>
            /// <returns>A string that represents the VulkanMemoryModelKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.VulkanMemoryModelKHR()";
            }
        }
        #endregion //VulkanMemoryModelKHR

        #region VulkanMemoryModelDeviceScope
        public static VulkanMemoryModelDeviceScopeImpl VulkanMemoryModelDeviceScope()
        {
            return VulkanMemoryModelDeviceScopeImpl.Instance;
            
        }

        internal class VulkanMemoryModelDeviceScopeImpl: Capability
        {
            public static readonly VulkanMemoryModelDeviceScopeImpl Instance = new VulkanMemoryModelDeviceScopeImpl();
        
            private  VulkanMemoryModelDeviceScopeImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.VulkanMemoryModelDeviceScope;
            public new static VulkanMemoryModelDeviceScopeImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the VulkanMemoryModelDeviceScopeImpl object.</summary>
            /// <returns>A string that represents the VulkanMemoryModelDeviceScopeImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.VulkanMemoryModelDeviceScope()";
            }
        }
        #endregion //VulkanMemoryModelDeviceScope

        #region VulkanMemoryModelDeviceScopeKHR
        public static VulkanMemoryModelDeviceScopeKHRImpl VulkanMemoryModelDeviceScopeKHR()
        {
            return VulkanMemoryModelDeviceScopeKHRImpl.Instance;
            
        }

        internal class VulkanMemoryModelDeviceScopeKHRImpl: Capability
        {
            public static readonly VulkanMemoryModelDeviceScopeKHRImpl Instance = new VulkanMemoryModelDeviceScopeKHRImpl();
        
            private  VulkanMemoryModelDeviceScopeKHRImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.VulkanMemoryModelDeviceScopeKHR;
            public new static VulkanMemoryModelDeviceScopeKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the VulkanMemoryModelDeviceScopeKHRImpl object.</summary>
            /// <returns>A string that represents the VulkanMemoryModelDeviceScopeKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.VulkanMemoryModelDeviceScopeKHR()";
            }
        }
        #endregion //VulkanMemoryModelDeviceScopeKHR

        #region PhysicalStorageBufferAddresses
        public static PhysicalStorageBufferAddressesImpl PhysicalStorageBufferAddresses()
        {
            return PhysicalStorageBufferAddressesImpl.Instance;
            
        }

        internal class PhysicalStorageBufferAddressesImpl: Capability
        {
            public static readonly PhysicalStorageBufferAddressesImpl Instance = new PhysicalStorageBufferAddressesImpl();
        
            private  PhysicalStorageBufferAddressesImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.PhysicalStorageBufferAddresses;
            public new static PhysicalStorageBufferAddressesImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PhysicalStorageBufferAddressesImpl object.</summary>
            /// <returns>A string that represents the PhysicalStorageBufferAddressesImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.PhysicalStorageBufferAddresses()";
            }
        }
        #endregion //PhysicalStorageBufferAddresses

        #region PhysicalStorageBufferAddressesEXT
        public static PhysicalStorageBufferAddressesEXTImpl PhysicalStorageBufferAddressesEXT()
        {
            return PhysicalStorageBufferAddressesEXTImpl.Instance;
            
        }

        internal class PhysicalStorageBufferAddressesEXTImpl: Capability
        {
            public static readonly PhysicalStorageBufferAddressesEXTImpl Instance = new PhysicalStorageBufferAddressesEXTImpl();
        
            private  PhysicalStorageBufferAddressesEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.PhysicalStorageBufferAddressesEXT;
            public new static PhysicalStorageBufferAddressesEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the PhysicalStorageBufferAddressesEXTImpl object.</summary>
            /// <returns>A string that represents the PhysicalStorageBufferAddressesEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.PhysicalStorageBufferAddressesEXT()";
            }
        }
        #endregion //PhysicalStorageBufferAddressesEXT

        #region ComputeDerivativeGroupLinearNV
        public static ComputeDerivativeGroupLinearNVImpl ComputeDerivativeGroupLinearNV()
        {
            return ComputeDerivativeGroupLinearNVImpl.Instance;
            
        }

        internal class ComputeDerivativeGroupLinearNVImpl: Capability
        {
            public static readonly ComputeDerivativeGroupLinearNVImpl Instance = new ComputeDerivativeGroupLinearNVImpl();
        
            private  ComputeDerivativeGroupLinearNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ComputeDerivativeGroupLinearNV;
            public new static ComputeDerivativeGroupLinearNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ComputeDerivativeGroupLinearNVImpl object.</summary>
            /// <returns>A string that represents the ComputeDerivativeGroupLinearNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ComputeDerivativeGroupLinearNV()";
            }
        }
        #endregion //ComputeDerivativeGroupLinearNV

        #region RayTracingProvisionalKHR
        public static RayTracingProvisionalKHRImpl RayTracingProvisionalKHR()
        {
            return RayTracingProvisionalKHRImpl.Instance;
            
        }

        internal class RayTracingProvisionalKHRImpl: Capability
        {
            public static readonly RayTracingProvisionalKHRImpl Instance = new RayTracingProvisionalKHRImpl();
        
            private  RayTracingProvisionalKHRImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.RayTracingProvisionalKHR;
            public new static RayTracingProvisionalKHRImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the RayTracingProvisionalKHRImpl object.</summary>
            /// <returns>A string that represents the RayTracingProvisionalKHRImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.RayTracingProvisionalKHR()";
            }
        }
        #endregion //RayTracingProvisionalKHR

        #region CooperativeMatrixNV
        public static CooperativeMatrixNVImpl CooperativeMatrixNV()
        {
            return CooperativeMatrixNVImpl.Instance;
            
        }

        internal class CooperativeMatrixNVImpl: Capability
        {
            public static readonly CooperativeMatrixNVImpl Instance = new CooperativeMatrixNVImpl();
        
            private  CooperativeMatrixNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.CooperativeMatrixNV;
            public new static CooperativeMatrixNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the CooperativeMatrixNVImpl object.</summary>
            /// <returns>A string that represents the CooperativeMatrixNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.CooperativeMatrixNV()";
            }
        }
        #endregion //CooperativeMatrixNV

        #region FragmentShaderSampleInterlockEXT
        public static FragmentShaderSampleInterlockEXTImpl FragmentShaderSampleInterlockEXT()
        {
            return FragmentShaderSampleInterlockEXTImpl.Instance;
            
        }

        internal class FragmentShaderSampleInterlockEXTImpl: Capability
        {
            public static readonly FragmentShaderSampleInterlockEXTImpl Instance = new FragmentShaderSampleInterlockEXTImpl();
        
            private  FragmentShaderSampleInterlockEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.FragmentShaderSampleInterlockEXT;
            public new static FragmentShaderSampleInterlockEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FragmentShaderSampleInterlockEXTImpl object.</summary>
            /// <returns>A string that represents the FragmentShaderSampleInterlockEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.FragmentShaderSampleInterlockEXT()";
            }
        }
        #endregion //FragmentShaderSampleInterlockEXT

        #region FragmentShaderShadingRateInterlockEXT
        public static FragmentShaderShadingRateInterlockEXTImpl FragmentShaderShadingRateInterlockEXT()
        {
            return FragmentShaderShadingRateInterlockEXTImpl.Instance;
            
        }

        internal class FragmentShaderShadingRateInterlockEXTImpl: Capability
        {
            public static readonly FragmentShaderShadingRateInterlockEXTImpl Instance = new FragmentShaderShadingRateInterlockEXTImpl();
        
            private  FragmentShaderShadingRateInterlockEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.FragmentShaderShadingRateInterlockEXT;
            public new static FragmentShaderShadingRateInterlockEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FragmentShaderShadingRateInterlockEXTImpl object.</summary>
            /// <returns>A string that represents the FragmentShaderShadingRateInterlockEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.FragmentShaderShadingRateInterlockEXT()";
            }
        }
        #endregion //FragmentShaderShadingRateInterlockEXT

        #region ShaderSMBuiltinsNV
        public static ShaderSMBuiltinsNVImpl ShaderSMBuiltinsNV()
        {
            return ShaderSMBuiltinsNVImpl.Instance;
            
        }

        internal class ShaderSMBuiltinsNVImpl: Capability
        {
            public static readonly ShaderSMBuiltinsNVImpl Instance = new ShaderSMBuiltinsNVImpl();
        
            private  ShaderSMBuiltinsNVImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.ShaderSMBuiltinsNV;
            public new static ShaderSMBuiltinsNVImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the ShaderSMBuiltinsNVImpl object.</summary>
            /// <returns>A string that represents the ShaderSMBuiltinsNVImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.ShaderSMBuiltinsNV()";
            }
        }
        #endregion //ShaderSMBuiltinsNV

        #region FragmentShaderPixelInterlockEXT
        public static FragmentShaderPixelInterlockEXTImpl FragmentShaderPixelInterlockEXT()
        {
            return FragmentShaderPixelInterlockEXTImpl.Instance;
            
        }

        internal class FragmentShaderPixelInterlockEXTImpl: Capability
        {
            public static readonly FragmentShaderPixelInterlockEXTImpl Instance = new FragmentShaderPixelInterlockEXTImpl();
        
            private  FragmentShaderPixelInterlockEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.FragmentShaderPixelInterlockEXT;
            public new static FragmentShaderPixelInterlockEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the FragmentShaderPixelInterlockEXTImpl object.</summary>
            /// <returns>A string that represents the FragmentShaderPixelInterlockEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.FragmentShaderPixelInterlockEXT()";
            }
        }
        #endregion //FragmentShaderPixelInterlockEXT

        #region DemoteToHelperInvocationEXT
        public static DemoteToHelperInvocationEXTImpl DemoteToHelperInvocationEXT()
        {
            return DemoteToHelperInvocationEXTImpl.Instance;
            
        }

        internal class DemoteToHelperInvocationEXTImpl: Capability
        {
            public static readonly DemoteToHelperInvocationEXTImpl Instance = new DemoteToHelperInvocationEXTImpl();
        
            private  DemoteToHelperInvocationEXTImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.DemoteToHelperInvocationEXT;
            public new static DemoteToHelperInvocationEXTImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the DemoteToHelperInvocationEXTImpl object.</summary>
            /// <returns>A string that represents the DemoteToHelperInvocationEXTImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.DemoteToHelperInvocationEXT()";
            }
        }
        #endregion //DemoteToHelperInvocationEXT

        #region SubgroupShuffleINTEL
        public static SubgroupShuffleINTELImpl SubgroupShuffleINTEL()
        {
            return SubgroupShuffleINTELImpl.Instance;
            
        }

        internal class SubgroupShuffleINTELImpl: Capability
        {
            public static readonly SubgroupShuffleINTELImpl Instance = new SubgroupShuffleINTELImpl();
        
            private  SubgroupShuffleINTELImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SubgroupShuffleINTEL;
            public new static SubgroupShuffleINTELImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupShuffleINTELImpl object.</summary>
            /// <returns>A string that represents the SubgroupShuffleINTELImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SubgroupShuffleINTEL()";
            }
        }
        #endregion //SubgroupShuffleINTEL

        #region SubgroupBufferBlockIOINTEL
        public static SubgroupBufferBlockIOINTELImpl SubgroupBufferBlockIOINTEL()
        {
            return SubgroupBufferBlockIOINTELImpl.Instance;
            
        }

        internal class SubgroupBufferBlockIOINTELImpl: Capability
        {
            public static readonly SubgroupBufferBlockIOINTELImpl Instance = new SubgroupBufferBlockIOINTELImpl();
        
            private  SubgroupBufferBlockIOINTELImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SubgroupBufferBlockIOINTEL;
            public new static SubgroupBufferBlockIOINTELImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupBufferBlockIOINTELImpl object.</summary>
            /// <returns>A string that represents the SubgroupBufferBlockIOINTELImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SubgroupBufferBlockIOINTEL()";
            }
        }
        #endregion //SubgroupBufferBlockIOINTEL

        #region SubgroupImageBlockIOINTEL
        public static SubgroupImageBlockIOINTELImpl SubgroupImageBlockIOINTEL()
        {
            return SubgroupImageBlockIOINTELImpl.Instance;
            
        }

        internal class SubgroupImageBlockIOINTELImpl: Capability
        {
            public static readonly SubgroupImageBlockIOINTELImpl Instance = new SubgroupImageBlockIOINTELImpl();
        
            private  SubgroupImageBlockIOINTELImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SubgroupImageBlockIOINTEL;
            public new static SubgroupImageBlockIOINTELImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupImageBlockIOINTELImpl object.</summary>
            /// <returns>A string that represents the SubgroupImageBlockIOINTELImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SubgroupImageBlockIOINTEL()";
            }
        }
        #endregion //SubgroupImageBlockIOINTEL

        #region SubgroupImageMediaBlockIOINTEL
        public static SubgroupImageMediaBlockIOINTELImpl SubgroupImageMediaBlockIOINTEL()
        {
            return SubgroupImageMediaBlockIOINTELImpl.Instance;
            
        }

        internal class SubgroupImageMediaBlockIOINTELImpl: Capability
        {
            public static readonly SubgroupImageMediaBlockIOINTELImpl Instance = new SubgroupImageMediaBlockIOINTELImpl();
        
            private  SubgroupImageMediaBlockIOINTELImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SubgroupImageMediaBlockIOINTEL;
            public new static SubgroupImageMediaBlockIOINTELImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupImageMediaBlockIOINTELImpl object.</summary>
            /// <returns>A string that represents the SubgroupImageMediaBlockIOINTELImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SubgroupImageMediaBlockIOINTEL()";
            }
        }
        #endregion //SubgroupImageMediaBlockIOINTEL

        #region IntegerFunctions2INTEL
        public static IntegerFunctions2INTELImpl IntegerFunctions2INTEL()
        {
            return IntegerFunctions2INTELImpl.Instance;
            
        }

        internal class IntegerFunctions2INTELImpl: Capability
        {
            public static readonly IntegerFunctions2INTELImpl Instance = new IntegerFunctions2INTELImpl();
        
            private  IntegerFunctions2INTELImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.IntegerFunctions2INTEL;
            public new static IntegerFunctions2INTELImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the IntegerFunctions2INTELImpl object.</summary>
            /// <returns>A string that represents the IntegerFunctions2INTELImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.IntegerFunctions2INTEL()";
            }
        }
        #endregion //IntegerFunctions2INTEL

        #region SubgroupAvcMotionEstimationINTEL
        public static SubgroupAvcMotionEstimationINTELImpl SubgroupAvcMotionEstimationINTEL()
        {
            return SubgroupAvcMotionEstimationINTELImpl.Instance;
            
        }

        internal class SubgroupAvcMotionEstimationINTELImpl: Capability
        {
            public static readonly SubgroupAvcMotionEstimationINTELImpl Instance = new SubgroupAvcMotionEstimationINTELImpl();
        
            private  SubgroupAvcMotionEstimationINTELImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SubgroupAvcMotionEstimationINTEL;
            public new static SubgroupAvcMotionEstimationINTELImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupAvcMotionEstimationINTELImpl object.</summary>
            /// <returns>A string that represents the SubgroupAvcMotionEstimationINTELImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SubgroupAvcMotionEstimationINTEL()";
            }
        }
        #endregion //SubgroupAvcMotionEstimationINTEL

        #region SubgroupAvcMotionEstimationIntraINTEL
        public static SubgroupAvcMotionEstimationIntraINTELImpl SubgroupAvcMotionEstimationIntraINTEL()
        {
            return SubgroupAvcMotionEstimationIntraINTELImpl.Instance;
            
        }

        internal class SubgroupAvcMotionEstimationIntraINTELImpl: Capability
        {
            public static readonly SubgroupAvcMotionEstimationIntraINTELImpl Instance = new SubgroupAvcMotionEstimationIntraINTELImpl();
        
            private  SubgroupAvcMotionEstimationIntraINTELImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SubgroupAvcMotionEstimationIntraINTEL;
            public new static SubgroupAvcMotionEstimationIntraINTELImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupAvcMotionEstimationIntraINTELImpl object.</summary>
            /// <returns>A string that represents the SubgroupAvcMotionEstimationIntraINTELImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SubgroupAvcMotionEstimationIntraINTEL()";
            }
        }
        #endregion //SubgroupAvcMotionEstimationIntraINTEL

        #region SubgroupAvcMotionEstimationChromaINTEL
        public static SubgroupAvcMotionEstimationChromaINTELImpl SubgroupAvcMotionEstimationChromaINTEL()
        {
            return SubgroupAvcMotionEstimationChromaINTELImpl.Instance;
            
        }

        internal class SubgroupAvcMotionEstimationChromaINTELImpl: Capability
        {
            public static readonly SubgroupAvcMotionEstimationChromaINTELImpl Instance = new SubgroupAvcMotionEstimationChromaINTELImpl();
        
            private  SubgroupAvcMotionEstimationChromaINTELImpl()
            {
            }
            public override Enumerant Value => Capability.Enumerant.SubgroupAvcMotionEstimationChromaINTEL;
            public new static SubgroupAvcMotionEstimationChromaINTELImpl Parse(WordReader reader, uint wordCount)
            {
                return Instance;
            }

            /// <summary>Returns a string that represents the SubgroupAvcMotionEstimationChromaINTELImpl object.</summary>
            /// <returns>A string that represents the SubgroupAvcMotionEstimationChromaINTELImpl object.</returns>
            /// <filterpriority>2</filterpriority>
            public override string ToString()
            {
                return $" Capability.SubgroupAvcMotionEstimationChromaINTEL()";
            }
        }
        #endregion //SubgroupAvcMotionEstimationChromaINTEL

        public abstract Enumerant Value { get; }

        public static Capability Parse(WordReader reader, uint wordCount)
        {
            var id = (Enumerant) reader.ReadWord();
            switch (id)
            {
                case Enumerant.Matrix :
                    return MatrixImpl.Parse(reader, wordCount - 1);
                case Enumerant.Shader :
                    return ShaderImpl.Parse(reader, wordCount - 1);
                case Enumerant.Geometry :
                    return GeometryImpl.Parse(reader, wordCount - 1);
                case Enumerant.Tessellation :
                    return TessellationImpl.Parse(reader, wordCount - 1);
                case Enumerant.Addresses :
                    return AddressesImpl.Parse(reader, wordCount - 1);
                case Enumerant.Linkage :
                    return LinkageImpl.Parse(reader, wordCount - 1);
                case Enumerant.Kernel :
                    return KernelImpl.Parse(reader, wordCount - 1);
                case Enumerant.Vector16 :
                    return Vector16Impl.Parse(reader, wordCount - 1);
                case Enumerant.Float16Buffer :
                    return Float16BufferImpl.Parse(reader, wordCount - 1);
                case Enumerant.Float16 :
                    return Float16Impl.Parse(reader, wordCount - 1);
                case Enumerant.Float64 :
                    return Float64Impl.Parse(reader, wordCount - 1);
                case Enumerant.Int64 :
                    return Int64Impl.Parse(reader, wordCount - 1);
                case Enumerant.Int64Atomics :
                    return Int64AtomicsImpl.Parse(reader, wordCount - 1);
                case Enumerant.ImageBasic :
                    return ImageBasicImpl.Parse(reader, wordCount - 1);
                case Enumerant.ImageReadWrite :
                    return ImageReadWriteImpl.Parse(reader, wordCount - 1);
                case Enumerant.ImageMipmap :
                    return ImageMipmapImpl.Parse(reader, wordCount - 1);
                case Enumerant.Pipes :
                    return PipesImpl.Parse(reader, wordCount - 1);
                case Enumerant.Groups :
                    return GroupsImpl.Parse(reader, wordCount - 1);
                case Enumerant.DeviceEnqueue :
                    return DeviceEnqueueImpl.Parse(reader, wordCount - 1);
                case Enumerant.LiteralSampler :
                    return LiteralSamplerImpl.Parse(reader, wordCount - 1);
                case Enumerant.AtomicStorage :
                    return AtomicStorageImpl.Parse(reader, wordCount - 1);
                case Enumerant.Int16 :
                    return Int16Impl.Parse(reader, wordCount - 1);
                case Enumerant.TessellationPointSize :
                    return TessellationPointSizeImpl.Parse(reader, wordCount - 1);
                case Enumerant.GeometryPointSize :
                    return GeometryPointSizeImpl.Parse(reader, wordCount - 1);
                case Enumerant.ImageGatherExtended :
                    return ImageGatherExtendedImpl.Parse(reader, wordCount - 1);
                case Enumerant.StorageImageMultisample :
                    return StorageImageMultisampleImpl.Parse(reader, wordCount - 1);
                case Enumerant.UniformBufferArrayDynamicIndexing :
                    return UniformBufferArrayDynamicIndexingImpl.Parse(reader, wordCount - 1);
                case Enumerant.SampledImageArrayDynamicIndexing :
                    return SampledImageArrayDynamicIndexingImpl.Parse(reader, wordCount - 1);
                case Enumerant.StorageBufferArrayDynamicIndexing :
                    return StorageBufferArrayDynamicIndexingImpl.Parse(reader, wordCount - 1);
                case Enumerant.StorageImageArrayDynamicIndexing :
                    return StorageImageArrayDynamicIndexingImpl.Parse(reader, wordCount - 1);
                case Enumerant.ClipDistance :
                    return ClipDistanceImpl.Parse(reader, wordCount - 1);
                case Enumerant.CullDistance :
                    return CullDistanceImpl.Parse(reader, wordCount - 1);
                case Enumerant.ImageCubeArray :
                    return ImageCubeArrayImpl.Parse(reader, wordCount - 1);
                case Enumerant.SampleRateShading :
                    return SampleRateShadingImpl.Parse(reader, wordCount - 1);
                case Enumerant.ImageRect :
                    return ImageRectImpl.Parse(reader, wordCount - 1);
                case Enumerant.SampledRect :
                    return SampledRectImpl.Parse(reader, wordCount - 1);
                case Enumerant.GenericPointer :
                    return GenericPointerImpl.Parse(reader, wordCount - 1);
                case Enumerant.Int8 :
                    return Int8Impl.Parse(reader, wordCount - 1);
                case Enumerant.InputAttachment :
                    return InputAttachmentImpl.Parse(reader, wordCount - 1);
                case Enumerant.SparseResidency :
                    return SparseResidencyImpl.Parse(reader, wordCount - 1);
                case Enumerant.MinLod :
                    return MinLodImpl.Parse(reader, wordCount - 1);
                case Enumerant.Sampled1D :
                    return Sampled1DImpl.Parse(reader, wordCount - 1);
                case Enumerant.Image1D :
                    return Image1DImpl.Parse(reader, wordCount - 1);
                case Enumerant.SampledCubeArray :
                    return SampledCubeArrayImpl.Parse(reader, wordCount - 1);
                case Enumerant.SampledBuffer :
                    return SampledBufferImpl.Parse(reader, wordCount - 1);
                case Enumerant.ImageBuffer :
                    return ImageBufferImpl.Parse(reader, wordCount - 1);
                case Enumerant.ImageMSArray :
                    return ImageMSArrayImpl.Parse(reader, wordCount - 1);
                case Enumerant.StorageImageExtendedFormats :
                    return StorageImageExtendedFormatsImpl.Parse(reader, wordCount - 1);
                case Enumerant.ImageQuery :
                    return ImageQueryImpl.Parse(reader, wordCount - 1);
                case Enumerant.DerivativeControl :
                    return DerivativeControlImpl.Parse(reader, wordCount - 1);
                case Enumerant.InterpolationFunction :
                    return InterpolationFunctionImpl.Parse(reader, wordCount - 1);
                case Enumerant.TransformFeedback :
                    return TransformFeedbackImpl.Parse(reader, wordCount - 1);
                case Enumerant.GeometryStreams :
                    return GeometryStreamsImpl.Parse(reader, wordCount - 1);
                case Enumerant.StorageImageReadWithoutFormat :
                    return StorageImageReadWithoutFormatImpl.Parse(reader, wordCount - 1);
                case Enumerant.StorageImageWriteWithoutFormat :
                    return StorageImageWriteWithoutFormatImpl.Parse(reader, wordCount - 1);
                case Enumerant.MultiViewport :
                    return MultiViewportImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupDispatch :
                    return SubgroupDispatchImpl.Parse(reader, wordCount - 1);
                case Enumerant.NamedBarrier :
                    return NamedBarrierImpl.Parse(reader, wordCount - 1);
                case Enumerant.PipeStorage :
                    return PipeStorageImpl.Parse(reader, wordCount - 1);
                case Enumerant.GroupNonUniform :
                    return GroupNonUniformImpl.Parse(reader, wordCount - 1);
                case Enumerant.GroupNonUniformVote :
                    return GroupNonUniformVoteImpl.Parse(reader, wordCount - 1);
                case Enumerant.GroupNonUniformArithmetic :
                    return GroupNonUniformArithmeticImpl.Parse(reader, wordCount - 1);
                case Enumerant.GroupNonUniformBallot :
                    return GroupNonUniformBallotImpl.Parse(reader, wordCount - 1);
                case Enumerant.GroupNonUniformShuffle :
                    return GroupNonUniformShuffleImpl.Parse(reader, wordCount - 1);
                case Enumerant.GroupNonUniformShuffleRelative :
                    return GroupNonUniformShuffleRelativeImpl.Parse(reader, wordCount - 1);
                case Enumerant.GroupNonUniformClustered :
                    return GroupNonUniformClusteredImpl.Parse(reader, wordCount - 1);
                case Enumerant.GroupNonUniformQuad :
                    return GroupNonUniformQuadImpl.Parse(reader, wordCount - 1);
                case Enumerant.ShaderLayer :
                    return ShaderLayerImpl.Parse(reader, wordCount - 1);
                case Enumerant.ShaderViewportIndex :
                    return ShaderViewportIndexImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupBallotKHR :
                    return SubgroupBallotKHRImpl.Parse(reader, wordCount - 1);
                case Enumerant.DrawParameters :
                    return DrawParametersImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupVoteKHR :
                    return SubgroupVoteKHRImpl.Parse(reader, wordCount - 1);
                case Enumerant.StorageBuffer16BitAccess :
                    return StorageBuffer16BitAccessImpl.Parse(reader, wordCount - 1);
                //StorageUniformBufferBlock16 has the same id as another value in enum.
                //case Enumerant.StorageUniformBufferBlock16 :
                //    return StorageUniformBufferBlock16.Parse(reader, wordCount - 1);
                case Enumerant.UniformAndStorageBuffer16BitAccess :
                    return UniformAndStorageBuffer16BitAccessImpl.Parse(reader, wordCount - 1);
                //StorageUniform16 has the same id as another value in enum.
                //case Enumerant.StorageUniform16 :
                //    return StorageUniform16.Parse(reader, wordCount - 1);
                case Enumerant.StoragePushConstant16 :
                    return StoragePushConstant16Impl.Parse(reader, wordCount - 1);
                case Enumerant.StorageInputOutput16 :
                    return StorageInputOutput16Impl.Parse(reader, wordCount - 1);
                case Enumerant.DeviceGroup :
                    return DeviceGroupImpl.Parse(reader, wordCount - 1);
                case Enumerant.MultiView :
                    return MultiViewImpl.Parse(reader, wordCount - 1);
                case Enumerant.VariablePointersStorageBuffer :
                    return VariablePointersStorageBufferImpl.Parse(reader, wordCount - 1);
                case Enumerant.VariablePointers :
                    return VariablePointersImpl.Parse(reader, wordCount - 1);
                case Enumerant.AtomicStorageOps :
                    return AtomicStorageOpsImpl.Parse(reader, wordCount - 1);
                case Enumerant.SampleMaskPostDepthCoverage :
                    return SampleMaskPostDepthCoverageImpl.Parse(reader, wordCount - 1);
                case Enumerant.StorageBuffer8BitAccess :
                    return StorageBuffer8BitAccessImpl.Parse(reader, wordCount - 1);
                case Enumerant.UniformAndStorageBuffer8BitAccess :
                    return UniformAndStorageBuffer8BitAccessImpl.Parse(reader, wordCount - 1);
                case Enumerant.StoragePushConstant8 :
                    return StoragePushConstant8Impl.Parse(reader, wordCount - 1);
                case Enumerant.DenormPreserve :
                    return DenormPreserveImpl.Parse(reader, wordCount - 1);
                case Enumerant.DenormFlushToZero :
                    return DenormFlushToZeroImpl.Parse(reader, wordCount - 1);
                case Enumerant.SignedZeroInfNanPreserve :
                    return SignedZeroInfNanPreserveImpl.Parse(reader, wordCount - 1);
                case Enumerant.RoundingModeRTE :
                    return RoundingModeRTEImpl.Parse(reader, wordCount - 1);
                case Enumerant.RoundingModeRTZ :
                    return RoundingModeRTZImpl.Parse(reader, wordCount - 1);
                case Enumerant.RayQueryProvisionalKHR :
                    return RayQueryProvisionalKHRImpl.Parse(reader, wordCount - 1);
                case Enumerant.RayTraversalPrimitiveCullingProvisionalKHR :
                    return RayTraversalPrimitiveCullingProvisionalKHRImpl.Parse(reader, wordCount - 1);
                case Enumerant.Float16ImageAMD :
                    return Float16ImageAMDImpl.Parse(reader, wordCount - 1);
                case Enumerant.ImageGatherBiasLodAMD :
                    return ImageGatherBiasLodAMDImpl.Parse(reader, wordCount - 1);
                case Enumerant.FragmentMaskAMD :
                    return FragmentMaskAMDImpl.Parse(reader, wordCount - 1);
                case Enumerant.StencilExportEXT :
                    return StencilExportEXTImpl.Parse(reader, wordCount - 1);
                case Enumerant.ImageReadWriteLodAMD :
                    return ImageReadWriteLodAMDImpl.Parse(reader, wordCount - 1);
                case Enumerant.ShaderClockKHR :
                    return ShaderClockKHRImpl.Parse(reader, wordCount - 1);
                case Enumerant.SampleMaskOverrideCoverageNV :
                    return SampleMaskOverrideCoverageNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.GeometryShaderPassthroughNV :
                    return GeometryShaderPassthroughNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.ShaderViewportIndexLayerEXT :
                    return ShaderViewportIndexLayerEXTImpl.Parse(reader, wordCount - 1);
                //ShaderViewportIndexLayerNV has the same id as another value in enum.
                //case Enumerant.ShaderViewportIndexLayerNV :
                //    return ShaderViewportIndexLayerNV.Parse(reader, wordCount - 1);
                case Enumerant.ShaderViewportMaskNV :
                    return ShaderViewportMaskNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.ShaderStereoViewNV :
                    return ShaderStereoViewNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.PerViewAttributesNV :
                    return PerViewAttributesNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.FragmentFullyCoveredEXT :
                    return FragmentFullyCoveredEXTImpl.Parse(reader, wordCount - 1);
                case Enumerant.MeshShadingNV :
                    return MeshShadingNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.ImageFootprintNV :
                    return ImageFootprintNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.FragmentBarycentricNV :
                    return FragmentBarycentricNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.ComputeDerivativeGroupQuadsNV :
                    return ComputeDerivativeGroupQuadsNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.FragmentDensityEXT :
                    return FragmentDensityEXTImpl.Parse(reader, wordCount - 1);
                //ShadingRateNV has the same id as another value in enum.
                //case Enumerant.ShadingRateNV :
                //    return ShadingRateNV.Parse(reader, wordCount - 1);
                case Enumerant.GroupNonUniformPartitionedNV :
                    return GroupNonUniformPartitionedNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.ShaderNonUniform :
                    return ShaderNonUniformImpl.Parse(reader, wordCount - 1);
                //ShaderNonUniformEXT has the same id as another value in enum.
                //case Enumerant.ShaderNonUniformEXT :
                //    return ShaderNonUniformEXT.Parse(reader, wordCount - 1);
                case Enumerant.RuntimeDescriptorArray :
                    return RuntimeDescriptorArrayImpl.Parse(reader, wordCount - 1);
                //RuntimeDescriptorArrayEXT has the same id as another value in enum.
                //case Enumerant.RuntimeDescriptorArrayEXT :
                //    return RuntimeDescriptorArrayEXT.Parse(reader, wordCount - 1);
                case Enumerant.InputAttachmentArrayDynamicIndexing :
                    return InputAttachmentArrayDynamicIndexingImpl.Parse(reader, wordCount - 1);
                //InputAttachmentArrayDynamicIndexingEXT has the same id as another value in enum.
                //case Enumerant.InputAttachmentArrayDynamicIndexingEXT :
                //    return InputAttachmentArrayDynamicIndexingEXT.Parse(reader, wordCount - 1);
                case Enumerant.UniformTexelBufferArrayDynamicIndexing :
                    return UniformTexelBufferArrayDynamicIndexingImpl.Parse(reader, wordCount - 1);
                //UniformTexelBufferArrayDynamicIndexingEXT has the same id as another value in enum.
                //case Enumerant.UniformTexelBufferArrayDynamicIndexingEXT :
                //    return UniformTexelBufferArrayDynamicIndexingEXT.Parse(reader, wordCount - 1);
                case Enumerant.StorageTexelBufferArrayDynamicIndexing :
                    return StorageTexelBufferArrayDynamicIndexingImpl.Parse(reader, wordCount - 1);
                //StorageTexelBufferArrayDynamicIndexingEXT has the same id as another value in enum.
                //case Enumerant.StorageTexelBufferArrayDynamicIndexingEXT :
                //    return StorageTexelBufferArrayDynamicIndexingEXT.Parse(reader, wordCount - 1);
                case Enumerant.UniformBufferArrayNonUniformIndexing :
                    return UniformBufferArrayNonUniformIndexingImpl.Parse(reader, wordCount - 1);
                //UniformBufferArrayNonUniformIndexingEXT has the same id as another value in enum.
                //case Enumerant.UniformBufferArrayNonUniformIndexingEXT :
                //    return UniformBufferArrayNonUniformIndexingEXT.Parse(reader, wordCount - 1);
                case Enumerant.SampledImageArrayNonUniformIndexing :
                    return SampledImageArrayNonUniformIndexingImpl.Parse(reader, wordCount - 1);
                //SampledImageArrayNonUniformIndexingEXT has the same id as another value in enum.
                //case Enumerant.SampledImageArrayNonUniformIndexingEXT :
                //    return SampledImageArrayNonUniformIndexingEXT.Parse(reader, wordCount - 1);
                case Enumerant.StorageBufferArrayNonUniformIndexing :
                    return StorageBufferArrayNonUniformIndexingImpl.Parse(reader, wordCount - 1);
                //StorageBufferArrayNonUniformIndexingEXT has the same id as another value in enum.
                //case Enumerant.StorageBufferArrayNonUniformIndexingEXT :
                //    return StorageBufferArrayNonUniformIndexingEXT.Parse(reader, wordCount - 1);
                case Enumerant.StorageImageArrayNonUniformIndexing :
                    return StorageImageArrayNonUniformIndexingImpl.Parse(reader, wordCount - 1);
                //StorageImageArrayNonUniformIndexingEXT has the same id as another value in enum.
                //case Enumerant.StorageImageArrayNonUniformIndexingEXT :
                //    return StorageImageArrayNonUniformIndexingEXT.Parse(reader, wordCount - 1);
                case Enumerant.InputAttachmentArrayNonUniformIndexing :
                    return InputAttachmentArrayNonUniformIndexingImpl.Parse(reader, wordCount - 1);
                //InputAttachmentArrayNonUniformIndexingEXT has the same id as another value in enum.
                //case Enumerant.InputAttachmentArrayNonUniformIndexingEXT :
                //    return InputAttachmentArrayNonUniformIndexingEXT.Parse(reader, wordCount - 1);
                case Enumerant.UniformTexelBufferArrayNonUniformIndexing :
                    return UniformTexelBufferArrayNonUniformIndexingImpl.Parse(reader, wordCount - 1);
                //UniformTexelBufferArrayNonUniformIndexingEXT has the same id as another value in enum.
                //case Enumerant.UniformTexelBufferArrayNonUniformIndexingEXT :
                //    return UniformTexelBufferArrayNonUniformIndexingEXT.Parse(reader, wordCount - 1);
                case Enumerant.StorageTexelBufferArrayNonUniformIndexing :
                    return StorageTexelBufferArrayNonUniformIndexingImpl.Parse(reader, wordCount - 1);
                //StorageTexelBufferArrayNonUniformIndexingEXT has the same id as another value in enum.
                //case Enumerant.StorageTexelBufferArrayNonUniformIndexingEXT :
                //    return StorageTexelBufferArrayNonUniformIndexingEXT.Parse(reader, wordCount - 1);
                case Enumerant.RayTracingNV :
                    return RayTracingNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.VulkanMemoryModel :
                    return VulkanMemoryModelImpl.Parse(reader, wordCount - 1);
                //VulkanMemoryModelKHR has the same id as another value in enum.
                //case Enumerant.VulkanMemoryModelKHR :
                //    return VulkanMemoryModelKHR.Parse(reader, wordCount - 1);
                case Enumerant.VulkanMemoryModelDeviceScope :
                    return VulkanMemoryModelDeviceScopeImpl.Parse(reader, wordCount - 1);
                //VulkanMemoryModelDeviceScopeKHR has the same id as another value in enum.
                //case Enumerant.VulkanMemoryModelDeviceScopeKHR :
                //    return VulkanMemoryModelDeviceScopeKHR.Parse(reader, wordCount - 1);
                case Enumerant.PhysicalStorageBufferAddresses :
                    return PhysicalStorageBufferAddressesImpl.Parse(reader, wordCount - 1);
                //PhysicalStorageBufferAddressesEXT has the same id as another value in enum.
                //case Enumerant.PhysicalStorageBufferAddressesEXT :
                //    return PhysicalStorageBufferAddressesEXT.Parse(reader, wordCount - 1);
                case Enumerant.ComputeDerivativeGroupLinearNV :
                    return ComputeDerivativeGroupLinearNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.RayTracingProvisionalKHR :
                    return RayTracingProvisionalKHRImpl.Parse(reader, wordCount - 1);
                case Enumerant.CooperativeMatrixNV :
                    return CooperativeMatrixNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.FragmentShaderSampleInterlockEXT :
                    return FragmentShaderSampleInterlockEXTImpl.Parse(reader, wordCount - 1);
                case Enumerant.FragmentShaderShadingRateInterlockEXT :
                    return FragmentShaderShadingRateInterlockEXTImpl.Parse(reader, wordCount - 1);
                case Enumerant.ShaderSMBuiltinsNV :
                    return ShaderSMBuiltinsNVImpl.Parse(reader, wordCount - 1);
                case Enumerant.FragmentShaderPixelInterlockEXT :
                    return FragmentShaderPixelInterlockEXTImpl.Parse(reader, wordCount - 1);
                case Enumerant.DemoteToHelperInvocationEXT :
                    return DemoteToHelperInvocationEXTImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupShuffleINTEL :
                    return SubgroupShuffleINTELImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupBufferBlockIOINTEL :
                    return SubgroupBufferBlockIOINTELImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupImageBlockIOINTEL :
                    return SubgroupImageBlockIOINTELImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupImageMediaBlockIOINTEL :
                    return SubgroupImageMediaBlockIOINTELImpl.Parse(reader, wordCount - 1);
                case Enumerant.IntegerFunctions2INTEL :
                    return IntegerFunctions2INTELImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupAvcMotionEstimationINTEL :
                    return SubgroupAvcMotionEstimationINTELImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupAvcMotionEstimationIntraINTEL :
                    return SubgroupAvcMotionEstimationIntraINTELImpl.Parse(reader, wordCount - 1);
                case Enumerant.SubgroupAvcMotionEstimationChromaINTEL :
                    return SubgroupAvcMotionEstimationChromaINTELImpl.Parse(reader, wordCount - 1);
                default:
                    throw new IndexOutOfRangeException("Unknown Capability "+id);
            }
        }
        
        public static Capability ParseOptional(WordReader reader, uint wordCount)
        {
            if (wordCount == 0) return null;
            return Parse(reader, wordCount);
        }

        public static IList<Capability> ParseCollection(WordReader reader, uint wordCount)
        {
            var end = reader.Position + wordCount;
            var res = new PrintableList<Capability>();
            while (reader.Position < end)
            {
                res.Add(Parse(reader, end-reader.Position));
            }
            return res;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public virtual uint GetWordCount()
        {
            return 1;
        }

        public virtual void Write(WordWriter writer)
        {
            writer.WriteWord((uint)Value);
        }
    }
}