using System;
using System.Collections.Generic;
using System.Text;

namespace compute
{
    public struct GraphicsShaderReference : IPipeline
    {
        public GraphicsShaderReference(ShaderEntry fragmentShader, ShaderEntry vertexShader)
        {
            FragmentShader = fragmentShader;
            VertexShader = vertexShader;
        }

        public ShaderEntry FragmentShader { get; }

        public ShaderEntry VertexShader { get; }

        public IEnumerable<ShaderEntryPoint> GetEntryPoints()
        {
            yield return VertexShader.EntryPoint;
            yield return FragmentShader.EntryPoint;
        }
    }
}
