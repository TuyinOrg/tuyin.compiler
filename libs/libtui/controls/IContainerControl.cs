using System.Collections.Generic;

namespace libtui.controls
{
    public interface IContainerControl : IControl
    {
        IEnumerable<IControl> Childrens { get; }

        IControl FindChildren(int x, int y);
    }
}
