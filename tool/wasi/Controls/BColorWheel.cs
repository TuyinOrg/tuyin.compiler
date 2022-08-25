using addin.data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    class BColorWheel : BUIElement
    {
        private const int PSize = 360;
        private const float PCenter = PSize * 0.5f;
        private const float PCenterSquared = PSize * PSize * 0.25f;
        private const float PLength = 20;
        private const float PRadius = PCenter - 2 * PLength;
        private const float PRadiusSquared = PRadius * PRadius;
        private const float PMedium = PCenter - PLength;
        private const float PMediumSquared = PMedium * PMedium;
        private static readonly PointF Center = new PointF(PCenter, PCenter);
        private const double _rad = Math.PI / 180;
        private const double _invrad = 180d / Math.PI;

        private Bitmap MainPalette = new Bitmap(PSize, PSize);
        private Bitmap BlackPalette = new Bitmap(PSize, PSize);
        private Bitmap AlphaBackground = new Bitmap(PSize, PSize);
        private Bitmap AlphaPalette = new Bitmap(PSize, PSize);

        private int frameWidth;
        private Point startpos;
        private PointD mVec;

        private bool clicked = false;
        private int area = -1;
        private bool internalValueChanging = false;

        public override Size Size
        {
            get => base.Size;
            set
            {
                if (base.Size != value)
                {
                    base.Size = value;
                    AdjustFrame();
                    //Host.Invalidate();
                }
            }
        }

        public Color BackColor
        {
            get;
            set;
        }

        public Image BackgroundImage
        {
            get;
            set;
        }

        public BColorWheel(IBControl host)
            : base(host)
        {
            float[] r = { 0, -4.25f, 0, 0, 4.25f, 0 };
            float[] g = { 4.25f, 0, 0, -4.25f, 0, 0 };
            float[] b = { 0, 0, 4.25f, 0, 0, -4.25f };

            float R = 255, G = 0, B = 0;
            for (int k = 0; k < 6; k++)
            {
                for (int i = 0, p = k * 60; i < 60; i++)
                {
                    int q = p + i;
                    R += r[k];
                    G += g[k];
                    B += b[k];
                    aR[q] = (byte)R; aG[q] = (byte)G; aB[q] = (byte)B;
                }
            }

            AdjustFrame();
            CreateMainPalette();
            ColorFromMainPalette = ChooseMain(0, 1);
            CreateAlphaBackground();

            Host.MouseDown += Host_MouseDown;
            Host.MouseMove += Host_MouseMove;
            Host.MouseUp += Host_MouseUp;
        }

        private void Host_MouseUp(object sender, MouseEventArgs e)
        {
            clicked = false;
            area = -1;
        }

        private void Host_MouseMove(object sender, MouseEventArgs e)
        {
            if (!clicked) return;

            Point pos = new Point(e.X - Location.X - startpos.X, e.Y - Location.Y - startpos.Y);
            double f = 360d / frameWidth;
            PointD
                wpos = new PointD(pos.X * f, pos.Y * f),
                wcen = new PointD(Center.X, Center.Y);
            var vec = wpos - wcen;
            double dist = vec.LengthSquared();

            mVec = vec;

            if (area == -1 || area == 0)
            {
                area = 0;
                var d = InvSqrt((float)dist);
                double x = vec.X, y = vec.Y;
                double a = Math.Acos(Math.Abs(x * d)) * _invrad;
                if (x <= 0 && y <= 0) a = 180 - a;
                if (x < 0 && y > 0) a = 180 + a;
                if (x >= 0 && y > 0) a = 360 - a;
                a = 360 - a;
                d = (float)Math.Sqrt(dist) / PRadius;
                ColorFromMainPalette = ChooseMain(a, d);
            }
            else if (area == -1 || area == 1)
            {
                area = 1;
                var d = InvSqrt((float)dist);
                double x = vec.X, y = vec.Y;
                double a = Math.Acos(Math.Abs(x * d)) * _invrad;
                if (x <= 0 && y <= 0) a = 180 - a;
                if (x < 0 && y > 0) a = 180 + a;
                if (x >= 0 && y > 0) a = 360 - a;
                a = 360 - a;
                ColorFromBlackPalette = ChooseBlack(a);
            }
            else if (area == -1 || area == 2)
            {
                area = 2;
                var d = InvSqrt((float)dist);
                double x = vec.X, y = vec.Y;
                double a = Math.Acos(Math.Abs(x * d)) * _invrad;
                if (x <= 0 && y <= 0) a = 180 - a;
                if (x < 0 && y > 0) a = 180 + a;
                if (x >= 0 && y > 0) a = 360 - a;
                a = 360 - a;
                internalValueChanging = true;
                Value = ChooseAlpha(a);
            }
        }

        private void Host_MouseDown(object sender, MouseEventArgs e)
        {
            area = -1;

            var bounds = new Rectangle(Location, Size);
            if (bounds.Contains(e.Location))
            {
                clicked = true;

                Point pos = new Point(e.X - Location.X - startpos.X, e.Y - Location.Y - startpos.Y);
                double f = 360d / frameWidth;
                PointD
                    wpos = new PointD(pos.X * f, pos.Y * f),
                    wcen = new PointD(Center.X, Center.Y);
                var vec = wpos - wcen;
                double dist = vec.LengthSquared();

                if (dist < PRadiusSquared && (area == -1 || area == 0))
                {
                    area = 0;
                }
                else if (dist < PMediumSquared && (area == -1 || area == 1))
                {
                    area = 1;
                }
                else if (dist < PCenterSquared && (area == -1 || area == 2))
                {
                    area = 2;
                }

                Host_MouseMove(sender, e);
            }
        }

        private Color _ColorFromMainPalette;
        private Color ColorFromMainPalette
        {
            get => _ColorFromMainPalette;
            set
            {
                _ColorFromMainPalette = value;
                CreateBlackPalette();
                ColorFromBlackPalette = ChooseBlack(359);
            }
        }

        private Color _ColorFromBlackPalette;
        private Color ColorFromBlackPalette
        {
            get => _ColorFromBlackPalette;
            set
            {
                _ColorFromBlackPalette = value;
                CreateAlphaPalette();
                internalValueChanging = true;
                Value = value;
                Host.Invalidate();
            }
        }

        private Color _Value;
        public Color Value
        {
            get => _Value;
            set
            {
                if (internalValueChanging)
                {
                    _Value = value;
                    internalValueChanging = false;
                    var changingEvent = new HandledEventArgs(false);
                    ValueChanging?.Invoke(this, changingEvent);
                    if (changingEvent.Handled) return;
                    ValueChanged?.Invoke(this, new EventArgs());
                    return;
                }
                ColorFromMainPalette = Color.FromArgb(value.R, value.G, value.B);
            }
        }

        private void AdjustFrame()
        {
            frameWidth = Math.Min(Size.Width, Size.Height);
            startpos = new Point((Size.Width - frameWidth) / 2, (Size.Height - frameWidth) / 2);
        }

        byte[] aR = new byte[360];
        byte[] aG = new byte[360];
        byte[] aB = new byte[360];

        private void CreateBlackPalette()
        {
            var Color = ColorFromMainPalette;
            var gr = Graphics.FromImage(BlackPalette);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.Clear(Color.Transparent);
            float
                _360 = 1f / 360,
                _r = _360 * Color.R,
                _g = _360 * Color.G,
                _b = _360 * Color.B;
            for (int i = 0; i < 360; i++)
            {
                var r = _r * i;
                var g = _g * i;
                var b = _b * i;
                var P1 = new PointF(
                        PCenter + PRadius * (float)Math.Cos((i) * _rad),
                        PCenter + PRadius * (float)Math.Sin((i) * _rad)
                    );
                var P2 = new PointF(
                       PCenter + (PRadius + PLength) * (float)Math.Cos((i) * _rad),
                       PCenter + (PRadius + PLength) * (float)Math.Sin((i) * _rad)
                   );
                gr.DrawLine(new Pen(Color.FromArgb((byte)r, (byte)g, (byte)b), 3.5f), P1, P2);
            }
        }

        private void CreateMainPalette()
        {
            PointF[] pts = new PointF[6];
            const double a = Math.PI / 3;
            for (int i = 0; i < 6; i++)
            {
                pts[i].X = 2 * PLength + (float)(PRadius * (1 + Math.Cos(i * a)));
                pts[i].Y = 2 * PLength + (float)(PRadius * (1 + Math.Sin(i * a)));
            }
            using (var g = Graphics.FromImage(MainPalette))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = new GraphicsPath())
                {
                    path.AddLines(pts);
                    path.Flatten();
                    using (var path_brush = new PathGradientBrush(path))
                    {
                        path_brush.CenterColor = Color.White;
                        path_brush.SurroundColors = new Color[]
                        {
                        Color.Red,Color.Yellow,Color.Lime,
                        Color.Cyan,Color.Blue,Color.Magenta
                        };
                        g.FillPath(path_brush, path);
                    }
                }
            }
        }

        private void CreateAlphaBackground()
        {
            var Color = ColorFromBlackPalette;
            var gr = Graphics.FromImage(AlphaBackground);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.Clear(Color.Transparent);
            var pattern = new Bitmap(20, 20);
            using (var gp = Graphics.FromImage(pattern))
            {
                gp.FillRectangle(Brushes.White, new Rectangle(0, 0, 10, 10));
                gp.FillRectangle(Brushes.LightGray, new Rectangle(10, 0, 10, 10));
                gp.FillRectangle(Brushes.LightGray, new Rectangle(0, 10, 10, 10));
                gp.FillRectangle(Brushes.White, new Rectangle(10, 10, 10, 10));
            }
            var tBrush = new TextureBrush(pattern);
            var path = new GraphicsPath();
            path.AddEllipse(new RectangleF(PLength, PLength, 2 * PMedium, 2 * PMedium));
            gr.FillEllipse(tBrush, new Rectangle(0, 0, PSize, PSize));
            gr.Clip = new Region(path);
            gr.Clear(Color.Transparent);
        }

        private void CreateAlphaPalette()
        {
            var Color = ColorFromBlackPalette;
            var gr = Graphics.FromImage(AlphaPalette);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.Clear(Color.Transparent);
            float _360 = 255f / 360;
            for (int i = 0; i < 360; i++)
            {
                float a = _360 * i;
                var P1 = new PointF(
                        PCenter - PSize / 2 * (float)Math.Cos((i) * _rad),
                        PCenter - PSize / 2 * (float)Math.Sin((i) * _rad)
                    );
                var P2 = new PointF(
                       PCenter - PMedium * (float)Math.Cos((i) * _rad),
                       PCenter - PMedium * (float)Math.Sin((i) * _rad)
                   );
                gr.DrawLine(new Pen(Color.FromArgb((byte)a, Color), 4f), P1, P2);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            //g.DrawImageUnscaled(background,Point.Empty);
            e.Graphics.DrawImage(MainPalette, Location.X, Location.Y, Size.Width, Size.Height);
            e.Graphics.DrawImage(BlackPalette, Location.X, Location.Y, Size.Width, Size.Height);
            e.Graphics.DrawImage(AlphaBackground, Location.X, Location.Y, Size.Width, Size.Height);
            e.Graphics.DrawImage(AlphaPalette, Location.X, Location.Y, Size.Width, Size.Height);

            if (clicked)
            {
                double dist = mVec.LengthSquared();
                var d = InvSqrt((float)dist);
                double x = mVec.X, y = mVec.Y;
                double a = Math.Acos(Math.Abs(x * d)) * _invrad;

                if (area == 0)
                {
                }
                else if (area == 1)
                {
                    var Color = ColorFromMainPalette;
                    float
                      _360 = 1f / 360,
                      _r = _360 * Color.R,
                      _g = _360 * Color.G,
                      _b = _360 * Color.B;

                    if (x <= 0 && y <= 0) a = 180 - a;
                    if (x < 0 && y > 0) a = 180 + a;
                    if (x >= 0 && y > 0) a = 360 - a;
                    a = 360 - a;

                    var r = 255 - _r * a;
                    var g = 255 - _g * a;
                    var b = 255 - _b * a;

                    var p1 = new PointF(PCenter + PRadius * (float)Math.Cos((a) * _rad),
                                        PCenter + PRadius * (float)Math.Sin((a) * _rad));

                    var p2 = new PointF(PCenter + (PRadius + PLength) * (float)Math.Cos((a) * _rad),
                                        PCenter + (PRadius + PLength) * (float)Math.Sin((a) * _rad));

                    var x1 = Location.X + p1.X * Size.Width / BlackPalette.Size.Width;
                    var y1 = Location.Y + p1.Y * Size.Height / BlackPalette.Size.Height;
                    var x2 = Location.X + p2.X * Size.Width / BlackPalette.Size.Width;
                    var y2 = Location.Y + p2.Y * Size.Height / BlackPalette.Size.Height;

                    e.Graphics.DrawLine(new Pen(Color.FromArgb((byte)r, (byte)g, (byte)b), 1f), x1, y1, x2, y2);
                }
                else if (area == 2)
                {
                    if (x <= 0 && y <= 0) a = 180 - a;
                    if (x < 0 && y > 0) a = 180 + a;
                    if (x >= 0 && y > 0) a = 360 - a;
                    a = 360 - a;

                    var alpha = ChooseAlpha(a).A; // _360 * a;

                    var Color = ColorFromBlackPalette;
                    var r = 255 - Color.R; // * a;
                    var g = 255 - Color.G;
                    var b = 255 - Color.B;

                    var a2 = a + 180;
                    var p1 = new PointF(PCenter - PSize / 2 * (float)Math.Cos(a2 * _rad),
                                        PCenter - PSize / 2 * (float)Math.Sin(a2 * _rad));

                    var p2 = new PointF(PCenter - PMedium * (float)Math.Cos(a2 * _rad),
                                        PCenter - PMedium * (float)Math.Sin(a2 * _rad));

                    var x1 = Location.X + p1.X * Size.Width / AlphaPalette.Size.Width;
                    var y1 = Location.Y + p1.Y * Size.Height / AlphaPalette.Size.Height;
                    var x2 = Location.X + p2.X * Size.Width / AlphaPalette.Size.Width;
                    var y2 = Location.Y + p2.Y * Size.Height / AlphaPalette.Size.Height;

                    var hsl = new HslColor(Color.FromArgb(r, g, b));
                    var maxL = hsl.L;
                    hsl.L = alpha / 255d * maxL;
                    e.Graphics.DrawLine(new Pen(hsl.ToRgbColor(), 1f), x1, y1, x2, y2);
                }
            }
        }

        private Color ChooseMain(double a, double d)
        {
            var r = aR[(int)a] * d + 255 * (1 - d);
            var g = aG[(int)a] * d + 255 * (1 - d);
            var b = aB[(int)a] * d + 255 * (1 - d);
            return Color.FromArgb((byte)r, (byte)g, (byte)b);
        }

        private Color ChooseBlack(double a)
        {
            a /= 360;
            var r = ColorFromMainPalette.R * a;
            var g = ColorFromMainPalette.G * a;
            var b = ColorFromMainPalette.B * a;
            return Color.FromArgb((byte)r, (byte)g, (byte)b);
        }

        private Color ChooseAlpha(double a)
        {
            a -= 180;
            if (a < 0) a += 360;
            a /= 360;
            return Color.FromArgb((byte)(a * 255), ColorFromBlackPalette);
        }

        private float InvSqrt(float x)
        {
            float xhalf = 0.5f * x;
            int i = BitConverter.ToInt32(BitConverter.GetBytes(x), 0);
            i = 0x5f3759df - (i >> 1);
            x = BitConverter.ToSingle(BitConverter.GetBytes(i), 0);
            x = x * (1.5f - xhalf * x * x);
            return x;
        }

        public delegate void OnValueChanging(object sender, HandledEventArgs e);
        public event OnValueChanging ValueChanging;

        public delegate void OnValueChanged(object sender, EventArgs e);
        public event OnValueChanged ValueChanged;
    }
}
