using addin.common;
using System.Drawing;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class BArrowButton : BUIElement
    {
        private Bitmap mImg1;
        private Bitmap mImg2;
        private BButton mBtnExpand;
        private BButton mBtnClose;
        private int mWidth;
        private bool isClosed;
        private Point mLocation;
        private Point mLocation2;

        public bool IsClosed
        {
            get { return isClosed; }
            set
            {
                if (isClosed != value)
                {
                    isClosed = value;
                    if (!isClosed)
                    {
                        Host.Width = mWidth;
                        Location = mLocation2;
                    }
                    else
                    {
                        mWidth = Host.Width;
                        mLocation2 = Location;

                        Host.Width = mImg1.Width + 6;
                        Location = new Point(3, Location.Y);
                    }
                }
            }
        }

        public override Point Location
        {
            get => mLocation;
            set
            {
                if (mLocation != value)
                {
                    mLocation = value;
                    mBtnExpand.Location = value;
                    mBtnClose.Location = value;
                    ComputeStates();
                }
            }
        }

        public override Size Size
        {
            get => mBtnExpand.Size;
            set
            {
                mBtnExpand.Size = value;
                mBtnClose.Size = value;
            }
        }

        public Color NormalColor
        {
            get { return mBtnExpand.IconNormalColor; }
            set
            {
                mBtnExpand.IconNormalColor = value;
                mBtnClose.IconNormalColor = value;
            }
        }

        public Color HoverColor
        {
            get { return mBtnExpand.IconHoverColor; }
            set
            {
                mBtnExpand.IconHoverColor = value;
                mBtnClose.IconHoverColor = value;
            }
        }

        public Color PressedColor
        {
            get { return mBtnExpand.IconPressedColor; }
            set
            {
                mBtnExpand.IconPressedColor = value;
                mBtnClose.IconPressedColor = value;
            }
        }

        public Color DisenableColor
        {
            get { return mBtnExpand.IconDisenableColor; }
            set
            {
                mBtnExpand.IconDisenableColor = value;
                mBtnClose.IconDisenableColor = value;
            }
        }

        public BArrowButton(BControl host) : base(host)
        {
            var img = Resources.GetManifestBitmap(typeof(BArrowButton).Assembly, "resources.fold.png");
            var img2 = ImageHelper.RevPicX(img);
            mImg1 = img;
            mImg2 = img2;

            mBtnExpand = new BButton(host);
            mBtnClose = new BButton(host);
            //mBtnExpand.SetBackColor(Color.Transparent);
            //mBtnClose.SetBackColor(Color.Transparent);
            mBtnExpand.AutoSize = false;
            mBtnClose.AutoSize = false;
            mBtnClose.Click += ButtonClick;
            mBtnExpand.Click += ButtonClick;

            NormalColor = Color.FromArgb(198, 198, 198);
            HoverColor = Color.FromArgb(232, 232, 232);
            PressedColor = IDE.AppConfig.Skin.AccentColor;
            DisenableColor = IDE.AppConfig.Skin.InvertColor;

            Host.ClientSizeChanged += Host_ClientSizeChanged;
        }

        private void Host_ClientSizeChanged(object sender, System.EventArgs e)
        {
            ComputeStates();
        }

        private void ComputeStates() 
        {
            if (Host.Width == 0) return;

            if (mLocation.X < Host.Width / 2)
            {
                if (mBtnClose.Icon != mImg1)
                {
                    mBtnClose.Icon = mImg1;
                    mBtnExpand.Icon = mImg2;
                    Host.Refresh();
                }
            }
            else
            {
                if (mBtnClose.Icon != mImg2)
                {
                    mBtnExpand.Icon = mImg1;
                    mBtnClose.Icon = mImg2;
                    Host.Refresh();
                }
            }
        }

        private void ButtonClick(object sender, MouseEventArgs e)
        {
            IsClosed = !IsClosed;
            //Host.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (IsClosed)
            {
                mBtnExpand.Paint(e);
            }
            else 
            {
                mBtnClose.Paint(e);
            }


            base.OnPaint(e);
        }
    }
}
