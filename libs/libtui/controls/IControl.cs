using libtui.drawing;

namespace libtui.controls
{
    public interface IControl : IVisualElment, IInputElement
    {
        Rectangle Bounds { get; }

        bool IsEnabled { get; }

        Image Cursor { get; }

        Point PointToClient(Point point);
    }
}
