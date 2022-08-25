using libtui.drawing;

namespace libtui.controls
{
    public class MouseEventArgs
    {
        public MouseEventArgs(double x, double y, int delta, InputState action, MouseButtons button, ModifierKeys modifiers, object tag, Rectangle rect)
        {
            X = x;
            Y = y;
            Delta = delta;
            Action = action;
            Button = button;
            Modifiers = modifiers;
            Tag = tag;
            Rectangle = rect;
        }

        public Point Location => new Point((int)X, (int)Y);

        public double X { get; internal set; }

        public double Y { get; internal set; }

        public int Delta { get; }

        public object Tag { get; internal set; }

        public Rectangle Rectangle { get; internal set; }

        public InputState Action { get; }

        public MouseButtons Button { get; }

        public ModifierKeys Modifiers { get; }
    }
}
