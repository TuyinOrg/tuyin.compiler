using System;

namespace compute
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class CapabilityAttribute : Attribute 
    {
        public Capability Capability { get; }

        public CapabilityAttribute(Capability capability)
        {
            Capability = capability;
        }
    }
}
