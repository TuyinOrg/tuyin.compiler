using System;
using System.Drawing;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class BTrackBar : BUIElement
    {
        private float mValue;
        private bool mIsDrag;
        private float mDownValue;
        private Point mDownPoint;
        private WinFormInputHelper<object> mInput;

        public float Value
        {
            get { return mValue; }
            set
            {
                if (value > MaxValue) value = MaxValue;
                if (value < MinValue) value = MinValue;

                if (mValue != value)
                {
                    mValue = value;

                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public float MaxValue { get; set; }
        public float MinValue { get; set; }

        public BTrackBar(BControl host)
            : base(host)
        {
            MinValue = 0;
            MaxValue = 100;

            mInput = new WinFormInputHelper<object>(host);
            mInput.MouseDown += MInput_MouseDown;
            Host.MouseUp += Host_MouseUp;
            Host.MouseMove += Host_MouseMove;
        }

        private void Host_MouseUp(object sender, MouseEventArgs e)
        {
            mIsDrag = false;
            mDownValue = Value;
        }

        private void Host_MouseMove(object sender, MouseEventArgs e)
        {
            if (mIsDrag && Enabled)
            {
                var value = mDownValue + (e.Location.X - mDownPoint.X) / (float)Size.Width * MaxValue;

                if (value > MaxValue) value = MaxValue;
                if (value < MinValue) value = MinValue;

                Value = value;
            }

            Host.Invalidate();
        }


        private void MInput_MouseDown(WinFormMouseInputEventArgs<object> args)
        {
            mDownPoint = args.Args.Location;
            mIsDrag = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            mInput.ClearRegions();

            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(37, 37, 37)), new RectangleF(0, (Size.Height - 2) / 2, Size.Width, 2));
            var tempSmooth = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            var bounds = new Rectangle((int)(((Value - MinValue) / (MaxValue - MinValue)) * Size.Width - ((Size.Height - 1) / 2)), 0, Size.Height - 1, Size.Height - 1);
            e.Graphics.FillEllipse(Enabled ? IDE.AppConfig.Skin.AccentBrush : new SolidBrush(Color.FromArgb(157, 157, 157)), bounds);
            e.Graphics.SmoothingMode = tempSmooth;
            mInput.AddRegion(null, bounds);

            base.OnPaint(e);
        }

        public event EventHandler ValueChanged;
    }
}
