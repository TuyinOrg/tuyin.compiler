using System;
using System.Collections.Generic;
using System.Text;

namespace compute
{
    public interface IPipeline
    {
        public IEnumerable<ShaderEntryPoint> GetEntryPoints();
    }
}
