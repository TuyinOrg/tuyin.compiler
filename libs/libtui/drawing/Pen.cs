using System;

namespace libtui.drawing
{
    public sealed class Pen : IDisposable
    {
        public Pen(Brush brush)
        {
            Brush = brush;
        }

        public Pen(Color color)
            : this(color, 1)
        {
        }

        public Pen(Color color, float thickenss) 
        {
            Brush = new SolidBrush(color);
            Width = thickenss;
        }

        public Brush Brush { get; }

        public float Width { get; }

        public void Dispose()
        {
        }
    }
}
