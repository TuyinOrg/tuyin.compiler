namespace libtui.drawing
{
    public class SolidBrush : Brush
    {
        public SolidBrush(Color color)
        {
            Color = color;
        }

        public Color Color { get; }
    }
}
