using libtui.drawing;

namespace libtui.controls
{
    public interface IVisualElment
    {
        Point Location { get; set; }

        Size Size { get; set; }

        Padding Padding { get; set; }

        void Invalidate();

        void Tick(Timer timer);

        void Paint(PaintEventArgs e);

        void ApplySkin(Skin skin);
    }
}
