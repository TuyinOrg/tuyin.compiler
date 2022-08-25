using System;

namespace Toe.SPIRV.Spv
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    internal class CapabilityAttribute : Attribute
    {
        public CapabilityAttribute(Capability.Enumerant capability)
        {
            Capability = capability;
        }

        public Capability.Enumerant Capability { get; }
    }
}