using addin.data;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public enum RgbaChannel
    {
        Red,
        Green,
        Blue,
        Alpha
    }

    public class BRgbColorSlider : BColorSlider
    {
        private RgbaChannel _channel;
        private Color _color;

        public BRgbColorSlider(IBControl host) 
            : base(host)
        {
            BarStyle = ColorBarStyle.Custom;
        }

        public virtual RgbaChannel Channel
        {
            get { return _channel; }
            set
            {
                if (this.Channel != value)
                {
                    _channel = value;

                    this.OnChannelChanged(EventArgs.Empty);
                }
            }
        }

        public virtual Color Color
        {
            get { return _color; }
            set
            {
                if (this.Color != value)
                {
                    _color = value;

                    this.OnColorChanged(EventArgs.Empty);
                }
            }
        }


        protected virtual void CreateScale()
        {
            ColorCollection custom;
            Color color;
            RgbaChannel channel;

            custom = new ColorCollection();
            color = this.Color;
            channel = this.Channel;

            for (int i = 0; i < 254; i++)
            {
                int a;
                int r;
                int g;
                int b;

                a = color.A;
                r = color.R;
                g = color.G;
                b = color.B;

                switch (channel)
                {
                    case RgbaChannel.Red:
                        r = i;
                        break;
                    case RgbaChannel.Green:
                        g = i;
                        break;
                    case RgbaChannel.Blue:
                        b = i;
                        break;
                    case RgbaChannel.Alpha:
                        a = i;
                        break;
                }

                custom.Add(Color.FromArgb(a, r, g, b));
            }

            this.CustomColors = custom;
        }

        protected virtual void OnChannelChanged(EventArgs e)
        {
            ChannelChanged?.Invoke(this, e);
            this.CreateScale();
            Host.Invalidate();
        }

        protected virtual void OnColorChanged(EventArgs e)
        {
            ColorChanged?.Invoke(this, e);
            this.CreateScale();
            Host.Invalidate();
        }

        protected override void PaintBar(PaintEventArgs e)
        {
            if (this.Color.A != 255)
                e.Graphics.FillColor(Color.Transparent, BarBounds, (Size.Height - NubSize.Height - 2) / 2);

            base.PaintBar(e);
        }

        public event EventHandler ChannelChanged;
        public event EventHandler ColorChanged;
    }
}
