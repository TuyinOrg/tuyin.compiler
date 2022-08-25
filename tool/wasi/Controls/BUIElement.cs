using BigBuns.Compute.Drawing;

namespace addin.controls.renderer
{
    public abstract class BUIElement : IUIElment
    {
        public virtual IBControl    Host        { get; private set; }
        public virtual bool         Enabled     { get; set; } = true;
        public virtual Point        Location    { get; set; }
        public virtual Size         Size        { get; set; }
        public virtual Padding      Padding     { get; set; }

        public BUIElement(IBControl host)                    
        {
            Host = host;
        }

        public void Paint(PaintEventArgs e)                 
        {
            OnPaint(e);
        }

        protected virtual void OnPaint(PaintEventArgs e)    
        {

        }
    }
}
