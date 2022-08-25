using System.Drawing;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public interface IUIPoint
    {
        bool Enabled { get; set; }

        Point Location { get; set; }

        void Paint(PaintEventArgs e);
    }

    public interface IUIElment : IUIPoint
    {
        Size        Size        { get; set; }
        Padding     Padding     { get; set; }
    }
}
