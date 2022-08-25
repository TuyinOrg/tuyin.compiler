using System;
using System.Collections.Generic;
using System.Linq;

namespace compute
{
    /// <summary>
    /// 着色器入口
    /// </summary>
    public struct ShaderEntry : IPipeline
    {
        public Shader Shader { get; }

        public ShaderEntryPoint EntryPoint { get; }

        public ShaderEntry(Shader shader, string entryPoint)
        {
            Shader = shader;
            EntryPoint = shader.EntryPoints.FirstOrDefault(x => x.EntryPointName == entryPoint);

            if (EntryPoint == null)
                throw new ArgumentException($"The shader does not contain an entry point named '{entryPoint}'.");
        }

        public IEnumerable<ShaderEntryPoint> GetEntryPoints()
        {
            yield return EntryPoint;
        }

        public override bool Equals(object obj)
        {
            return obj is ShaderEntry entry &&
                   EqualityComparer<Shader>.Default.Equals(Shader, entry.Shader) &&
                   EntryPoint.EntryPointName == entry.EntryPoint.EntryPointName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Shader, EntryPoint.EntryPointName);
        }
    }
}
