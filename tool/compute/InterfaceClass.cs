namespace compute
{
    public enum InterfaceClass 
    {
        UniformConstant = 0,
        Input = 1,
        [Capability(Capability.Shader)]
        Uniform = 2,
        [Capability(Capability.Shader)]
        Output = 3,
        Workgroup = 4,
        CrossWorkgroup = 5,
        [Capability(Capability.Shader)]
        Private = 6,
        Function = 7,
        [Capability(Capability.GenericPointer)]
        Generic = 8,
        [Capability(Capability.Shader)]
        PushConstant = 9,
        [Capability(Capability.AtomicStorage)]
        AtomicCounter = 10,
        Image = 11,
        [Capability(Capability.Shader)]
        StorageBuffer = 12,
        [Capability(Capability.RayTracingNV)]
        [Capability(Capability.RayTracingProvisionalKHR)]
        CallableDataNV = 5328,
        [Capability(Capability.RayTracingNV)]
        [Capability(Capability.RayTracingProvisionalKHR)]
        CallableDataKHR = 5328,
        [Capability(Capability.RayTracingNV)]
        [Capability(Capability.RayTracingProvisionalKHR)]
        IncomingCallableDataNV = 5329,
        [Capability(Capability.RayTracingNV)]
        [Capability(Capability.RayTracingProvisionalKHR)]
        IncomingCallableDataKHR = 5329,
        [Capability(Capability.RayTracingNV)]
        [Capability(Capability.RayTracingProvisionalKHR)]
        RayPayloadNV = 5338,
        [Capability(Capability.RayTracingNV)]
        [Capability(Capability.RayTracingProvisionalKHR)]
        RayPayloadKHR = 5338,
        [Capability(Capability.RayTracingNV)]
        [Capability(Capability.RayTracingProvisionalKHR)]
        HitAttributeNV = 5339,
        [Capability(Capability.RayTracingNV)]
        [Capability(Capability.RayTracingProvisionalKHR)]
        HitAttributeKHR = 5339,
        [Capability(Capability.RayTracingNV)]
        [Capability(Capability.RayTracingProvisionalKHR)]
        IncomingRayPayloadNV = 5342,
        [Capability(Capability.RayTracingNV)]
        [Capability(Capability.RayTracingProvisionalKHR)]
        IncomingRayPayloadKHR = 5342,
        [Capability(Capability.RayTracingNV)]
        [Capability(Capability.RayTracingProvisionalKHR)]
        ShaderRecordBufferNV = 5343,
        [Capability(Capability.RayTracingNV)]
        [Capability(Capability.RayTracingProvisionalKHR)]
        ShaderRecordBufferKHR = 5343,
        [Capability(Capability.PhysicalStorageBufferAddresses)]
        PhysicalStorageBuffer = 5349,
        [Capability(Capability.PhysicalStorageBufferAddresses)]
        PhysicalStorageBufferEXT = 5349,
    }
}
