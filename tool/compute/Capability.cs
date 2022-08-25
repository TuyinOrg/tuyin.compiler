namespace compute
{

    public enum Capability
    {
        Matrix = 0,
        [Capability(Capability.Matrix)]
        Shader = 1,
        [Capability(Capability.Shader)]
        Geometry = 2,
        [Capability(Capability.Shader)]
        Tessellation = 3,
        Addresses = 4,
        Linkage = 5,
        Kernel = 6,
        [Capability(Capability.Kernel)]
        Vector16 = 7,
        [Capability(Capability.Kernel)]
        Float16Buffer = 8,
        Float16 = 9,
        Float64 = 10,
        Int64 = 11,
        [Capability(Capability.Int64)]
        Int64Atomics = 12,
        [Capability(Capability.Kernel)]
        ImageBasic = 13,
        [Capability(Capability.ImageBasic)]
        ImageReadWrite = 14,
        [Capability(Capability.ImageBasic)]
        ImageMipmap = 15,
        [Capability(Capability.Kernel)]
        Pipes = 17,
        Groups = 18,
        [Capability(Capability.Kernel)]
        DeviceEnqueue = 19,
        [Capability(Capability.Kernel)]
        LiteralSampler = 20,
        [Capability(Capability.Shader)]
        AtomicStorage = 21,
        Int16 = 22,
        [Capability(Capability.Tessellation)]
        TessellationPointSize = 23,
        [Capability(Capability.Geometry)]
        GeometryPointSize = 24,
        [Capability(Capability.Shader)]
        ImageGatherExtended = 25,
        [Capability(Capability.Shader)]
        StorageImageMultisample = 27,
        [Capability(Capability.Shader)]
        UniformBufferArrayDynamicIndexing = 28,
        [Capability(Capability.Shader)]
        SampledImageArrayDynamicIndexing = 29,
        [Capability(Capability.Shader)]
        StorageBufferArrayDynamicIndexing = 30,
        [Capability(Capability.Shader)]
        StorageImageArrayDynamicIndexing = 31,
        [Capability(Capability.Shader)]
        ClipDistance = 32,
        [Capability(Capability.Shader)]
        CullDistance = 33,
        [Capability(Capability.SampledCubeArray)]
        ImageCubeArray = 34,
        [Capability(Capability.Shader)]
        SampleRateShading = 35,
        [Capability(Capability.SampledRect)]
        ImageRect = 36,
        [Capability(Capability.Shader)]
        SampledRect = 37,
        [Capability(Capability.Addresses)]
        GenericPointer = 38,
        Int8 = 39,
        [Capability(Capability.Shader)]
        InputAttachment = 40,
        [Capability(Capability.Shader)]
        SparseResidency = 41,
        [Capability(Capability.Shader)]
        MinLod = 42,
        Sampled1D = 43,
        [Capability(Capability.Sampled1D)]
        Image1D = 44,
        [Capability(Capability.Shader)]
        SampledCubeArray = 45,
        SampledBuffer = 46,
        [Capability(Capability.SampledBuffer)]
        ImageBuffer = 47,
        [Capability(Capability.Shader)]
        ImageMSArray = 48,
        [Capability(Capability.Shader)]
        StorageImageExtendedFormats = 49,
        [Capability(Capability.Shader)]
        ImageQuery = 50,
        [Capability(Capability.Shader)]
        DerivativeControl = 51,
        [Capability(Capability.Shader)]
        InterpolationFunction = 52,
        [Capability(Capability.Shader)]
        TransformFeedback = 53,
        [Capability(Capability.Geometry)]
        GeometryStreams = 54,
        [Capability(Capability.Shader)]
        StorageImageReadWithoutFormat = 55,
        [Capability(Capability.Shader)]
        StorageImageWriteWithoutFormat = 56,
        [Capability(Capability.Geometry)]
        MultiViewport = 57,
        [Capability(Capability.DeviceEnqueue)]
        SubgroupDispatch = 58,
        [Capability(Capability.Kernel)]
        NamedBarrier = 59,
        [Capability(Capability.Pipes)]
        PipeStorage = 60,
        GroupNonUniform = 61,
        [Capability(Capability.GroupNonUniform)]
        GroupNonUniformVote = 62,
        [Capability(Capability.GroupNonUniform)]
        GroupNonUniformArithmetic = 63,
        [Capability(Capability.GroupNonUniform)]
        GroupNonUniformBallot = 64,
        [Capability(Capability.GroupNonUniform)]
        GroupNonUniformShuffle = 65,
        [Capability(Capability.GroupNonUniform)]
        GroupNonUniformShuffleRelative = 66,
        [Capability(Capability.GroupNonUniform)]
        GroupNonUniformClustered = 67,
        [Capability(Capability.GroupNonUniform)]
        GroupNonUniformQuad = 68,
        ShaderLayer = 69,
        ShaderViewportIndex = 70,
        SubgroupBallotKHR = 4423,
        [Capability(Capability.Shader)]
        DrawParameters = 4427,
        SubgroupVoteKHR = 4431,
        StorageBuffer16BitAccess = 4433,
        StorageUniformBufferBlock16 = 4433,
        [Capability(Capability.StorageBuffer16BitAccess)]
        [Capability(Capability.StorageUniformBufferBlock16)]
        UniformAndStorageBuffer16BitAccess = 4434,
        [Capability(Capability.StorageBuffer16BitAccess)]
        [Capability(Capability.StorageUniformBufferBlock16)]
        StorageUniform16 = 4434,
        StoragePushConstant16 = 4435,
        StorageInputOutput16 = 4436,
        DeviceGroup = 4437,
        [Capability(Capability.Shader)]
        MultiView = 4439,
        [Capability(Capability.Shader)]
        VariablePointersStorageBuffer = 4441,
        [Capability(Capability.VariablePointersStorageBuffer)]
        VariablePointers = 4442,
        AtomicStorageOps = 4445,
        SampleMaskPostDepthCoverage = 4447,
        StorageBuffer8BitAccess = 4448,
        [Capability(Capability.StorageBuffer8BitAccess)]
        UniformAndStorageBuffer8BitAccess = 4449,
        StoragePushConstant8 = 4450,
        DenormPreserve = 4464,
        DenormFlushToZero = 4465,
        SignedZeroInfNanPreserve = 4466,
        RoundingModeRTE = 4467,
        RoundingModeRTZ = 4468,
        [Capability(Capability.Shader)]
        RayQueryProvisionalKHR = 4471,
        [Capability(Capability.RayQueryProvisionalKHR)]
        [Capability(Capability.RayTracingProvisionalKHR)]
        RayTraversalPrimitiveCullingProvisionalKHR = 4478,
        [Capability(Capability.Shader)]
        Float16ImageAMD = 5008,
        [Capability(Capability.Shader)]
        ImageGatherBiasLodAMD = 5009,
        [Capability(Capability.Shader)]
        FragmentMaskAMD = 5010,
        [Capability(Capability.Shader)]
        StencilExportEXT = 5013,
        [Capability(Capability.Shader)]
        ImageReadWriteLodAMD = 5015,
        [Capability(Capability.Shader)]
        ShaderClockKHR = 5055,
        [Capability(Capability.SampleRateShading)]
        SampleMaskOverrideCoverageNV = 5249,
        [Capability(Capability.Geometry)]
        GeometryShaderPassthroughNV = 5251,
        [Capability(Capability.MultiViewport)]
        ShaderViewportIndexLayerEXT = 5254,
        [Capability(Capability.MultiViewport)]
        ShaderViewportIndexLayerNV = 5254,
        [Capability(Capability.ShaderViewportIndexLayerNV)]
        ShaderViewportMaskNV = 5255,
        [Capability(Capability.ShaderViewportMaskNV)]
        ShaderStereoViewNV = 5259,
        [Capability(Capability.MultiView)]
        PerViewAttributesNV = 5260,
        [Capability(Capability.Shader)]
        FragmentFullyCoveredEXT = 5265,
        [Capability(Capability.Shader)]
        MeshShadingNV = 5266,
        ImageFootprintNV = 5282,
        FragmentBarycentricNV = 5284,
        ComputeDerivativeGroupQuadsNV = 5288,
        [Capability(Capability.Shader)]
        FragmentDensityEXT = 5291,
        [Capability(Capability.Shader)]
        ShadingRateNV = 5291,
        GroupNonUniformPartitionedNV = 5297,
        [Capability(Capability.Shader)]
        ShaderNonUniform = 5301,
        [Capability(Capability.Shader)]
        ShaderNonUniformEXT = 5301,
        [Capability(Capability.Shader)]
        RuntimeDescriptorArray = 5302,
        [Capability(Capability.Shader)]
        RuntimeDescriptorArrayEXT = 5302,
        [Capability(Capability.InputAttachment)]
        InputAttachmentArrayDynamicIndexing = 5303,
        [Capability(Capability.InputAttachment)]
        InputAttachmentArrayDynamicIndexingEXT = 5303,
        [Capability(Capability.SampledBuffer)]
        UniformTexelBufferArrayDynamicIndexing = 5304,
        [Capability(Capability.SampledBuffer)]
        UniformTexelBufferArrayDynamicIndexingEXT = 5304,
        [Capability(Capability.ImageBuffer)]
        StorageTexelBufferArrayDynamicIndexing = 5305,
        [Capability(Capability.ImageBuffer)]
        StorageTexelBufferArrayDynamicIndexingEXT = 5305,
        [Capability(Capability.ShaderNonUniform)]
        UniformBufferArrayNonUniformIndexing = 5306,
        [Capability(Capability.ShaderNonUniform)]
        UniformBufferArrayNonUniformIndexingEXT = 5306,
        [Capability(Capability.ShaderNonUniform)]
        SampledImageArrayNonUniformIndexing = 5307,
        [Capability(Capability.ShaderNonUniform)]
        SampledImageArrayNonUniformIndexingEXT = 5307,
        [Capability(Capability.ShaderNonUniform)]
        StorageBufferArrayNonUniformIndexing = 5308,
        [Capability(Capability.ShaderNonUniform)]
        StorageBufferArrayNonUniformIndexingEXT = 5308,
        [Capability(Capability.ShaderNonUniform)]
        StorageImageArrayNonUniformIndexing = 5309,
        [Capability(Capability.ShaderNonUniform)]
        StorageImageArrayNonUniformIndexingEXT = 5309,
        [Capability(Capability.InputAttachment)]
        [Capability(Capability.ShaderNonUniform)]
        InputAttachmentArrayNonUniformIndexing = 5310,
        [Capability(Capability.InputAttachment)]
        [Capability(Capability.ShaderNonUniform)]
        InputAttachmentArrayNonUniformIndexingEXT = 5310,
        [Capability(Capability.SampledBuffer)]
        [Capability(Capability.ShaderNonUniform)]
        UniformTexelBufferArrayNonUniformIndexing = 5311,
        [Capability(Capability.SampledBuffer)]
        [Capability(Capability.ShaderNonUniform)]
        UniformTexelBufferArrayNonUniformIndexingEXT = 5311,
        [Capability(Capability.ImageBuffer)]
        [Capability(Capability.ShaderNonUniform)]
        StorageTexelBufferArrayNonUniformIndexing = 5312,
        [Capability(Capability.ImageBuffer)]
        [Capability(Capability.ShaderNonUniform)]
        StorageTexelBufferArrayNonUniformIndexingEXT = 5312,
        [Capability(Capability.Shader)]
        RayTracingNV = 5340,
        VulkanMemoryModel = 5345,
        VulkanMemoryModelKHR = 5345,
        VulkanMemoryModelDeviceScope = 5346,
        VulkanMemoryModelDeviceScopeKHR = 5346,
        [Capability(Capability.Shader)]
        PhysicalStorageBufferAddresses = 5347,
        [Capability(Capability.Shader)]
        PhysicalStorageBufferAddressesEXT = 5347,
        ComputeDerivativeGroupLinearNV = 5350,
        [Capability(Capability.Shader)]
        RayTracingProvisionalKHR = 5353,
        [Capability(Capability.Shader)]
        CooperativeMatrixNV = 5357,
        [Capability(Capability.Shader)]
        FragmentShaderSampleInterlockEXT = 5363,
        [Capability(Capability.Shader)]
        FragmentShaderShadingRateInterlockEXT = 5372,
        [Capability(Capability.Shader)]
        ShaderSMBuiltinsNV = 5373,
        [Capability(Capability.Shader)]
        FragmentShaderPixelInterlockEXT = 5378,
        [Capability(Capability.Shader)]
        DemoteToHelperInvocationEXT = 5379,
        SubgroupShuffleINTEL = 5568,
        SubgroupBufferBlockIOINTEL = 5569,
        SubgroupImageBlockIOINTEL = 5570,
        SubgroupImageMediaBlockIOINTEL = 5579,
        [Capability(Capability.Shader)]
        IntegerFunctions2INTEL = 5584,
        SubgroupAvcMotionEstimationINTEL = 5696,
        SubgroupAvcMotionEstimationIntraINTEL = 5697,
        SubgroupAvcMotionEstimationChromaINTEL = 5698,
    }
}
