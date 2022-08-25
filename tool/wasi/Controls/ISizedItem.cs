using System.Drawing;

namespace addin.controls.renderer
{
    public interface ISizedItem 
    {
        Size ComputeSize(Size maxSize);
    }
}
