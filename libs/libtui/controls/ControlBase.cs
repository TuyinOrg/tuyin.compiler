using libtui.drawing;

namespace libtui.controls
{
    public abstract class ControlBase : IControl
    {
        public virtual Rectangle Bounds => new Rectangle(Location, Size);

        public bool IsFocused => App.Focused == this;

        public bool AllowFocus { get; set; }

        public bool AllowFileDrop { get; set; }

        public virtual bool IsEnabled { get; set; }

        public virtual Point Location { get; set; }

        public virtual Size Size { get; set; }

        public virtual Padding Padding { get; set; }

        public Image Cursor { get; set; }

        public ControlBase() 
        {
            IsEnabled = true;
        }

        public virtual void Invalidate()
        {
        }

        public virtual void Tick(Timer timer) 
        {
        }

        public virtual void Paint(PaintEventArgs e)
        {
        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {
        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
        }

        public virtual void OnMouseClick(MouseEventArgs e) 
        {
        }

        public virtual void OnMouseWheel(MouseEventArgs e)
        {
        }

        public virtual void OnMosueEnter(MouseEventArgs e)
        {
        }

        public virtual void OnMouseLeave(MouseEventArgs e)
        {
        }

        public virtual void OnKeyDown(KeyEventArgs e) 
        {

        }

        public virtual void OnKeyUp(KeyEventArgs e) 
        {
        }

        public virtual void OnKeyPress(KeyEventArgs e)
        {
        }

        public virtual void FileDrop(string[] fileNames)
        {
        }

        public virtual void OnGetFocus()
        {
        }

        public virtual void OnLostFocus()
        {
        }

        public virtual void OnSizeChanged(SizeChangeEventArgs e) 
        {
        }

        public virtual void OnCharInput(CharEventArgs e)
        {
        }

        public virtual void ApplySkin(Skin skin)
        {
        }

        public virtual Point PointToClient(Point point)
        {
            return point - Location;
        }
    }
}
