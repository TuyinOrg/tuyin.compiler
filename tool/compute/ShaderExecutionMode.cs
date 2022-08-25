namespace compute
{
    public enum ShaderExecutionMode                                     
    {
        [Capability(Capability.Geometry)]
        Invocations = 0,
        [Capability(Capability.Tessellation)]
        SpacingEqual = 1,
        [Capability(Capability.Tessellation)]
        SpacingFractionalEven = 2,
        [Capability(Capability.Tessellation)]
        SpacingFractionalOdd = 3,
        [Capability(Capability.Tessellation)]
        VertexOrderCw = 4,
        [Capability(Capability.Tessellation)]
        VertexOrderCcw = 5,
        [Capability(Capability.Shader)]
        PixelCenterInteger = 6,
        [Capability(Capability.Shader)]
        OriginUpperLeft = 7,
        [Capability(Capability.Shader)]
        OriginLowerLeft = 8,
        [Capability(Capability.Shader)]
        EarlyFragmentTests = 9,
        [Capability(Capability.Tessellation)]
        PointMode = 10,
        [Capability(Capability.TransformFeedback)]
        Xfb = 11,
        [Capability(Capability.Shader)]
        DepthReplacing = 12,
        [Capability(Capability.Shader)]
        DepthGreater = 14,
        [Capability(Capability.Shader)]
        DepthLess = 15,
        [Capability(Capability.Shader)]
        DepthUnchanged = 16,
        LocalSize = 17,
        [Capability(Capability.Kernel)]
        LocalSizeHint = 18,
        [Capability(Capability.Geometry)]
        InputPoints = 19,
        [Capability(Capability.Geometry)]
        InputLines = 20,
        [Capability(Capability.Geometry)]
        InputLinesAdjacency = 21,
        [Capability(Capability.Geometry)]
        [Capability(Capability.Tessellation)]
        Triangles = 22,
        [Capability(Capability.Geometry)]
        InputTrianglesAdjacency = 23,
        [Capability(Capability.Tessellation)]
        Quads = 24,
        [Capability(Capability.Tessellation)]
        Isolines = 25,
        [Capability(Capability.Geometry)]
        [Capability(Capability.Tessellation)]
        [Capability(Capability.MeshShadingNV)]
        OutputVertices = 26,
        [Capability(Capability.Geometry)]
        [Capability(Capability.MeshShadingNV)]
        OutputPoints = 27,
        [Capability(Capability.Geometry)]
        OutputLineStrip = 28,
        [Capability(Capability.Geometry)]
        OutputTriangleStrip = 29,
        [Capability(Capability.Kernel)]
        VecTypeHint = 30,
        [Capability(Capability.Kernel)]
        ContractionOff = 31,
        [Capability(Capability.Kernel)]
        Initializer = 33,
        [Capability(Capability.Kernel)]
        Finalizer = 34,
        [Capability(Capability.SubgroupDispatch)]
        SubgroupSize = 35,
        [Capability(Capability.SubgroupDispatch)]
        SubgroupsPerWorkgroup = 36,
        [Capability(Capability.SubgroupDispatch)]
        SubgroupsPerWorkgroupId = 37,
        LocalSizeId = 38,
        [Capability(Capability.Kernel)]
        LocalSizeHintId = 39,
        [Capability(Capability.SampleMaskPostDepthCoverage)]
        PostDepthCoverage = 4446,
        [Capability(Capability.DenormPreserve)]
        DenormPreserve = 4459,
        [Capability(Capability.DenormFlushToZero)]
        DenormFlushToZero = 4460,
        [Capability(Capability.SignedZeroInfNanPreserve)]
        SignedZeroInfNanPreserve = 4461,
        [Capability(Capability.RoundingModeRTE)]
        RoundingModeRTE = 4462,
        [Capability(Capability.RoundingModeRTZ)]
        RoundingModeRTZ = 4463,
        [Capability(Capability.StencilExportEXT)]
        StencilRefReplacingEXT = 5027,
        [Capability(Capability.MeshShadingNV)]
        OutputLinesNV = 5269,
        [Capability(Capability.MeshShadingNV)]
        OutputPrimitivesNV = 5270,
        [Capability(Capability.ComputeDerivativeGroupQuadsNV)]
        DerivativeGroupQuadsNV = 5289,
        [Capability(Capability.ComputeDerivativeGroupLinearNV)]
        DerivativeGroupLinearNV = 5290,
        [Capability(Capability.MeshShadingNV)]
        OutputTrianglesNV = 5298,
        [Capability(Capability.FragmentShaderPixelInterlockEXT)]
        PixelInterlockOrderedEXT = 5366,
        [Capability(Capability.FragmentShaderPixelInterlockEXT)]
        PixelInterlockUnorderedEXT = 5367,
        [Capability(Capability.FragmentShaderSampleInterlockEXT)]
        SampleInterlockOrderedEXT = 5368,
        [Capability(Capability.FragmentShaderSampleInterlockEXT)]
        SampleInterlockUnorderedEXT = 5369,
        [Capability(Capability.FragmentShaderShadingRateInterlockEXT)]
        ShadingRateInterlockOrderedEXT = 5370,
        [Capability(Capability.FragmentShaderShadingRateInterlockEXT)]
        ShadingRateInterlockUnorderedEXT = 5371,
    }
}
