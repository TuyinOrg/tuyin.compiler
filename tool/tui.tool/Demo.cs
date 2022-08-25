using libtui.controls;

namespace tui.tool
{
    internal class Demo : ControlBase
    {
        private Grid grid;

        public Demo() 
        {
            grid = new Grid();
        }

        public override void Paint(PaintEventArgs e)
        {
            grid.Paint(e);
        }
    }
}
