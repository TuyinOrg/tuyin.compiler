using libtui.drawing;
using System;
using System.Collections.Generic;
using System.Text;

namespace libtui.controls
{
    public class PaintEventArgs
    {
        public IGraphicDevice Graphics { get; }
        public IInputDevice Input { get; }

        internal PaintEventArgs(IGraphicDevice graphics, IInputDevice input)
        {
            Graphics = graphics;
            Input = input;
        }
    }
}
