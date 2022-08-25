using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class BColorIntensity : BUIElement
    {
        private Color mColor = Color.FromArgb(128, 128, 128);
        private WinFormInputHelper<Color> mColorInput;

        public int MinValue
        {
            get;
            set;
        } = -2;

        public int MaxValue
        {
            get;
            set;
        } = 2;

        public int Interval
        {
            get;
            set;
        } = 20;

        public Color Color 
        {
            get { return mColor; }
            set 
            {
                if (mColor != value) 
                {
                    mColor = value;
                    ColorChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public BColorIntensity(IBControl host) 
            : base(host)
        {
            mColorInput = new WinFormInputHelper<Color>(host);
            mColorInput.MouseDown += MColorInput_MouseDown;
        }

        private void MColorInput_MouseDown(WinFormMouseInputEventArgs<Color> args)
        {
            Color = args.Area.Tag;
            Host.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            var brush1 = new SolidBrush(Color.White);
            var brush2 = new SolidBrush(Color.Black);
         

            var num = MaxValue - MinValue + 1;
            var width = (int)((float)Size.Width / num);
            var height = Size.Height;
            var diff = (Size.Width - width * num) / 2;
            var x = Location.X + diff;
            var y = Location.Y;
            var bounds = new Rectangle(x + 1, y + 1, Size.Width - diff * 2 - 1, Size.Height - 1);

            if (Color.A != 255)
                e.Graphics.FillColor(Color.Transparent, bounds, width / 3);

            var brush3 = new LinearGradientBrush(bounds, Color.Empty, Color.Empty, 0, false);
            var blend = new ColorBlend();

            var count = MaxValue - MinValue + 1;
            count = count * 2;
            blend.Colors = new Color[count];
            blend.Positions = new float[blend.Colors.Length];
            for (var i = 0; i < count; i++) 
            {
                var k = i / 2 - (count / 2 - 1) / 2;
                var n = k * Interval;
                int r = Math.Min(255, Math.Max(0, Color.R + n));
                int g = Math.Min(255, Math.Max(0, Color.G + n));
                int b = Math.Min(255, Math.Max(0, Color.B + n));
                var c = Color.FromArgb(Color.A, r, g, b);
                var p = ((i + 1) / 2) * (1f / count * 2);

                blend.Colors[i] = c;
                blend.Positions[i] = p;
            }
            brush3.InterpolationColors = blend;
            e.Graphics.FillRectangle(brush3, bounds);
            brush3.Dispose();

            count = MaxValue - MinValue + 1;
            for (var i = 0; i < count; i++) 
            {        
                if (i == Math.Abs(MinValue))
                {
                }
                else
                {
                    var n = i * 2;
                    var r = blend.Positions[n];

                    mColorInput.AddRegion(
                        blend.Colors[n],
                        new Rectangle(
                            bounds.X + (int)(r * bounds.Width),
                            bounds.Y,
                            (int)((float)bounds.Width / count),
                            bounds.Height));
                }
            }

            
            for (var i = MinValue; i < 0; i++) 
            {
                var k = i - 1;
                var n = k * Interval;
                int r = Math.Min(255, Math.Max(0, Color.R + n));
                int g = Math.Min(255, Math.Max(0, Color.G + n));
                int b = Math.Min(255, Math.Max(0, Color.B + n));

                var c = Color.FromArgb(Color.A, r, g, b);
                var rect = new Rectangle(x, y, width, height);
                var text = i.ToString();
                var rect2 = new Rectangle(x + 1, y + 1, width, height);
                e.Graphics.DrawString(text, Host.Font, brush2, rect2, sf);
                e.Graphics.DrawString(text, Host.Font, brush1, rect, sf);

                x = x + width;
            }

            x = x + width;

            for (var i = 0; i < MaxValue; i++) 
            {
                var k = i + 1;
                var n = k * Interval;
                int r = Math.Min(255, Math.Max(0, Color.R + n));
                int g = Math.Min(255, Math.Max(0, Color.G + n));
                int b = Math.Min(255, Math.Max(0, Color.B + n));

                var c = Color.FromArgb(Color.A, r, g, b);
                var rect = new Rectangle(x, y, width, height);
                var text = k.ToString();
                var rect2 = new Rectangle(x + 1, y + 1, width, height);
                e.Graphics.DrawString(text, Host.Font, brush2, rect2, sf);
                e.Graphics.DrawString(text, Host.Font, brush1, rect, sf);

                x = x + width;
            }
            

            x = Location.X + diff;
            e.Graphics.DrawRectangle(new Pen(Color.White, 2), new Rectangle(x, y, Size.Width - diff * 2, Size.Height));
            e.Graphics.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(x + 1, y + 1, Size.Width - diff * 2 - 2, Size.Height - 2));

            brush1.Dispose();
            brush2.Dispose();
        }

        public event EventHandler ColorChanged;
    }
}
