using addin.common;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class BSlideBar : BUIElement
    {
        private bool mIsDraging;
        private HtmlToolTips mTips;
        private Rectangle mBounds;
        private double mValue;
        private double mDownValue;

        public int ThumbSize 
        {
            get;
            set;
        } = 8;

        public double SmallChange 
        {
            get;
            set;
        }

        public double MinValue 
        {
            get;
            set;
        }

        public double MaxValue 
        {
            get;
            set;
        }

        public double Value 
        {
            get { return mValue; }
            set 
            {
                if (mValue != value) 
                {
                    mValue = value;

                    if (mValue > MaxValue) mValue = MaxValue;
                    if (mValue < MinValue) mValue = MinValue;
                    Host.Invalidate();
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public double HarshValue
        {
            get;
            set;
        } = 0;

        public Color MainColor 
        {
            get;
            set;
        }

        public Color MinorColor 
        {
            get;
            set;
        }

        public Color BorderColor
        {
            get;
            set;
        } = Color.Black;

        public BSlideBar(IBControl host) : base(host)
        {
            mTips = new HtmlToolTips();
            mTips.ShowAlways = true;
            mTips.IsBalloon = true;

            //mTips.BackColor = host.BackColor;
            //mTips.ForeColor = IDE.AppConfig.Skin.DarkForeColor;
            //mTips.BorderColor = IDE.AppConfig.Skin.DarkForeColor;

            MaxValue = 100;
            MainColor = IDE.AppConfig.Skin.AccentColor;
            MinorColor = IDE.AppConfig.Skin.TipsColor;

            Host.MouseDown += Host_MouseDown;
            Host.MouseMove += Host_MouseMove;
        }

        ~BSlideBar() 
        {
            IDE.GlobalHandler.MouseLeftButtonUp -= GlobalHandler_MouseLeftButtonUp;
        }

        private void Host_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Enabled) return;

            if (e.Button == MouseButtons.Left && new Rectangle(Location, Size).Contains(e.Location))
            {
                mDownValue = Value;
                if (mBounds.Contains(e.Location))
                {
                    mIsDraging = true;
                    IDE.GlobalHandler.MouseLeftButtonUp += GlobalHandler_MouseLeftButtonUp;
                }
                else 
                {
                    var rate = (float)(e.X - Location.X) / Size.Width;
                    var value = (int)(rate * MaxValue / SmallChange) * SmallChange;
                    if (mDownValue != value)
                    {
                        Value = value;
                        DragDone?.Invoke(this, e);
                    }
                }
            }
        }

        private void Host_MouseMove(object sender, MouseEventArgs e)
        {
            if (mIsDraging)
            {
                // 计算当前值
                var rate = (float)(e.X - Location.X) / Size.Width;
                var value = (int)(rate * MaxValue / SmallChange) * SmallChange;
                var temp = Value;
                Value = value;

                if (temp != Value)
                {
                    var f = value;
                    int i = (int)(f * 100);
                    f = (double)(i * 1.0) / 100;

                    var center = (Value - MinValue) / (MaxValue - MinValue);
                    var x = Location.X + (int)(center * Size.Width - ThumbSize / 2);
                    var y = Location.Y + (Size.Height - ThumbSize) / 2;
                    mTips.ShowTips(f.ToString(), Host, new Point(x, y));
                }
            }
        }

        private void GlobalHandler_MouseLeftButtonUp(object sender, GlobalEventArgs<MouseEventArgs> e)
        {
            IDE.GlobalHandler.MouseLeftButtonUp -= GlobalHandler_MouseLeftButtonUp;

            if (mDownValue != Value)
            {
                DragDone?.Invoke(this, e.EventArgs);
            }

            mIsDraging = false;
            mTips.Hide();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.None;

            if (!Enabled)
            {
                int track = (int)(((Value - MinValue) * (Size.Width - ThumbSize)) / (MaxValue - MinValue));
                var barRect = new Rectangle(Location.X + 1, Location.Y + Size.Height / 2, Size.Width - 2, 5);

                Brush sliderLGBrushH = new LinearGradientBrush(barRect, ColorScheme.ColorGray122,
                    ColorScheme.ColorGray107, LinearGradientMode.Horizontal);


                Brush barFill = Brushes.Gray;
                //draw bar
                {
                    // Background gradient
                    e.Graphics.FillRectangle(sliderLGBrushH, barRect);
                    // Background fill
                    e.Graphics.FillRectangle(ColorScheme.SliderBorderBrush,
                        barRect.Left + 1, barRect.Top, barRect.Width - 2, barRect.Height - 1);
                    // Bar fill
                    e.Graphics.FillRectangle(ColorScheme.SliderFillBrush,
                        barRect.Left + 2, barRect.Top + 1, barRect.Width - 4, barRect.Height - 3);
                    // Elapsed bar fill

                    e.Graphics.FillRectangle(barFill,
                        barRect.Left + 2, barRect.Top + 1, track + ThumbSize / 2 - 2, barRect.Height - 3);

                    //draw bar band                    
                    //g.DrawRectangle(barPen, barRect);
                }

                sliderLGBrushH.Dispose();
            }
            else
            {
                int track = (int)(((Value - MinValue) * (Size.Width - ThumbSize)) / (MaxValue - MinValue));
                var thumbRect = mBounds = new Rectangle(Location.X + track, Location.Y + Size.Height / 2 - 3, ThumbSize - 1, 10);

                //adjust drawing rects
                var barRect = new Rectangle(Location.X + 1, Location.Y + Size.Height / 2, Size.Width - 2, 5);

                //get thumb shape path 
                var thumbPath = new GraphicsPath();
                thumbPath.AddPolygon(new Point[] {
                    new Point(thumbRect.Left, thumbRect.Top),
                    new Point(thumbRect.Right, thumbRect.Top),
                    new Point(thumbRect.Right, thumbRect.Bottom - 4),
                    new Point(thumbRect.Left + thumbRect.Width / 2, thumbRect.Bottom),
                    new Point(thumbRect.Left, thumbRect.Bottom - 4)
                });

                Brush sliderLGBrushH = new LinearGradientBrush(barRect, ColorScheme.ColorGray122,
                    ColorScheme.ColorGray107, LinearGradientMode.Horizontal);

                Brush barFill = Value > HarshValue ? Brushes.Peru : Brushes.Green;

                //draw bar
                {
                    // Background gradient
                    e.Graphics.FillRectangle(sliderLGBrushH, barRect);
                    // Background fill
                    e.Graphics.FillRectangle(ColorScheme.SliderBorderBrush,
                        barRect.Left + 1, barRect.Top, barRect.Width - 2, barRect.Height - 1);
                    // Bar fill
                    e.Graphics.FillRectangle(ColorScheme.SliderFillBrush,
                        barRect.Left + 2, barRect.Top + 1, barRect.Width - 4, barRect.Height - 3);
                    // Elapsed bar fill

                    e.Graphics.FillRectangle(barFill,
                        barRect.Left + 2, barRect.Top + 1, track + ThumbSize / 2 - 2, barRect.Height - 3);

                    //draw bar band                    
                    //g.DrawRectangle(barPen, barRect);
                }

                sliderLGBrushH.Dispose();

                //draw thumb
                Brush brushInner = new LinearGradientBrush(thumbRect,
                    Color.FromArgb(111, 111, 111), Color.FromArgb(80, 80, 80),
                    LinearGradientMode.Vertical);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(brushInner, thumbPath);
                e.Graphics.DrawPath(Pens.Black, thumbPath);

                brushInner.Dispose();
            }
        }

        public event EventHandler ValueChanged;
        public event EventHandler DragDone;
    }
}
