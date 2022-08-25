using System.Drawing;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class BProgressBar : BUIElement
    {
        private bool    mShowText;
        private Color   mForeColor;
        private Brush   mForeBrush;
        private Color   mBackColor;
        private Brush   mBackBrush;
        private Color   mBorderColor;
        private Pen     mBorderPen;

        public Color    ForeColor   
        {
            get { return mForeColor; }
            set
            {
                if (mForeColor != value)
                {
                    mForeColor = value;
                    mForeBrush = new SolidBrush(mForeColor);
                    if(Host != null) Host.Invalidate();
                }
            }
        }

        public Color    BackColor   
        {
            get { return mBackColor; }
            set
            {
                if (mBackColor != value)
                {
                    mBackColor = value;
                    mBackBrush = new SolidBrush(mBackColor);
                    if (Host != null) Host.Invalidate();
                }
            }
        }

        public Color    BorderColor 
        {
            get { return mBorderColor; }
            set
            {
                if (mBorderColor != value)
                {
                    mBorderColor = value;
                    mBorderPen = new Pen(mBorderColor);
                    if (Host != null) Host.Invalidate();
                }
            }
        }

        public bool     ShowText    { get; set; }
        public long     Value       { get; set; }
        public long     MaxValue    { get; set; }

        public BProgressBar(IBControl host)
            : base(host)
        {
            ForeColor = IDE.AppConfig.Skin.AccentColor;
            BackColor = IDE.AppConfig.Skin.AccentColor;
            BorderColor = ControlPaint.Dark(BackColor);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(mBackBrush, new Rectangle(Location.X, Location.Y, (int)(((double)Value / MaxValue) * Size.Width), Size.Height));
        }


    }
}
