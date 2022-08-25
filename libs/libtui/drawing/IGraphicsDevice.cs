namespace libtui.drawing
{
    public interface IGraphicDevice
    {
        SizeF MeasureString(string text, Font font);

        void FillGeometry(Brush brush, IGeometry geo);

        void DrawGeometry(Pen pen, IGeometry geo);

        void DrawString(string ctx, Font font, Brush brush, int x, int y);

        void SetClip(IGeometry geo);

        void ResetClip();

        void Flush();
    }
}
