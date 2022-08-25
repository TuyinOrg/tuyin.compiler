using libtui.drawing;
using System;


namespace libtui.controls
{
    public class SizeChangeEventArgs : EventArgs
    {
        public SizeChangeEventArgs(int width, int height) : this(new Size(width, height)) { }

        public SizeChangeEventArgs(Size size) { Size = size; }

        public Size Size { get; }
    }
}