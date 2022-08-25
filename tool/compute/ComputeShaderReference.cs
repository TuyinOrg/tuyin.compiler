using System.Collections.Generic;

namespace compute
{
    public struct ComputeShaderReference : IPipeline
    {
        public ComputeShaderReference(ShaderEntry entry, int count)
        {
            Entry = entry;
            DispatchCount = count;
        }

        public ShaderEntry Entry { get; }

        public int DispatchCount { get; }

        public IEnumerable<ShaderEntryPoint> GetEntryPoints()
        {
            yield return Entry.EntryPoint;
        }
    }
}
