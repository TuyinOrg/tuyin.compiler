using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public enum BButtonBorderEffect 
    {
        Convex,
        Concave
    }

    public class BButton : BUIElement
    {
        private BButtonState mState = BButtonState.Normal;
        private BorderStyle mBorderStyle = BorderStyle.Fixed3D;
        private HtmlToolTips mTips;
        private WinFormInputHelper<object> mInput;
 
        private Color mNormalColor;
        private Color mHoverColor;
        private Color mPressedColor;
        private Color mDisenableColor;
        private Color mBorderColor;

        private SolidBrush mNormalBrush;
        private SolidBrush mHoverBrush;
        private SolidBrush mPressedBrush;
        private SolidBrush mDisenableBrush;


        private Color mForeNormalColor;
        private Color mForeHoverColor;
        private Color mForePressedColor;
        private Color mForeDisenableColor;

        private SolidBrush mForeNormalBrush;
        private SolidBrush mForeHoverBrush;
        private SolidBrush mForePressedBrush;
        private SolidBrush mForeDisenableBrush;


        private Pen mBorderPen;

        private Image mIcon;
        private Image mOriginIcon;
        private Color[] mPixels;
        private Color mIconDefaultColor;
        private Color mIconHoverColor;
        private Color mIconPressedColor;
        private Color mIconDisenableColor;

        private bool mMultiLine;
        private string mText;
        private int mPitch;
        private Size mIconSize;
        private bool mChecked;

        public int Radius
        {
            get;
            set;
        }

        public bool Underline
        {
            get;
            set;
        }

        public Font Font
        {
            get;
            set;
        }

        public bool Checked
        {
            get => mChecked;
            set 
            {
                if (mChecked != value) 
                {
                    mChecked = value;
                    CheckdChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool CheckOnClick
        {
            get;
            set;
        }

        public Color CheckedColor
        {
            get;
            set;
        }

        public bool ShowBorderOnChecked
        {
            get;
            set;
        }

        public BButtonBorderEffect BorderEffect
        {
            get;
            set;
        } = BButtonBorderEffect.Concave;

        public void ClearClickListeners()
        {
            Click = null;
        }

        public override bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                if (base.Enabled != value)
                {
                    base.Enabled = value;
                    if (value)
                        mState = BButtonState.Normal;
                    else
                        mState = BButtonState.Disabled;
                    Host.Invalidate();
                }
            }
        }

        public object Tag { get; set; }

        public BButton(IBControl host)
            : base(host)
        {
            /*
             * support double buffer and transparent
             */
            mPitch = 2;
            Radius = 3;
            Font = host.Font;
            ToolTipOffset = new Point(12, 6);
            SetIconColor(Color.White);
            SetForeColor(IDE.AppConfig.Skin.ForeColor);

            BorderColor = IDE.AppConfig.Skin.InvertColor;
            NormalColor = IDE.AppConfig.Skin.BackColor;
            HoverColor = Color.FromArgb(85, 85, 98);
            PressedColor = IDE.AppConfig.Skin.BufferColor;
            CheckedColor = IDE.AppConfig.Skin.ForeColor;

            mInput = new WinFormInputHelper<object>(host);
            mInput.MouseEnter += mInput_MouseEnter;
            mInput.MouseLeave += mInput_MouseLeave;
            mInput.MouseMove += mInput_MouseMove;
            mInput.MouseDown += mInput_MouseDown;
            mInput.MouseUp += mInput_MouseUp;
        }

        void mInput_MouseEnter(WinFormMouseInputEventArgs<object> args)
        {
            if (mState != BButtonState.Disabled)
            {
                Host.Cursor = Cursors.Hand;

                if (!string.IsNullOrWhiteSpace(ToolTipText))
                {
                    if (mTips == null)
                    {
                        mTips = new HtmlToolTips();
                        mTips.IsBalloon = true;
                    }

                    if (!mTips.IsVisual)
                    {
                        mTips.ShowTips(
                            ToolTipText,
                            Host,
                            new Point(Location.X + ToolTipOffset.X, Location.Y + ToolTipOffset.Y));
                    }
                }
            }  
        }

        void mInput_MouseUp(WinFormMouseInputEventArgs<object> args)
        {
            if (mState != BButtonState.Disabled)
            {
                if (Up != null)
                    Up(this, args.Args);

                var inBounds = new Rectangle(Location, Size).Contains(args.Args.Location);

                if (mState == BButtonState.Pressed)
                {
                    if (inBounds)
                    {
                        if (CheckOnClick)
                            Checked = !Checked;

                        Click?.Invoke(this, args.Args);
                    }
                }

                if (mState != BButtonState.Disabled)
                    mState = BButtonState.Normal;

                Host.Invalidate();
            }
        }

        void mInput_MouseDown(WinFormMouseInputEventArgs<object> args)
        {
            if (mState != BButtonState.Disabled)
            {
                mState = BButtonState.Pressed;

                if (Down != null)
                    Down(this, args.Args);

                Host.Invalidate();
            }

            if (mTips != null && mTips.IsVisual)
            {
                mTips.Hide();
                mTips.Dispose();
                mTips = null;
            }
        }

        void mInput_MouseMove(WinFormMouseInputEventArgs<object> args)
        {
            if (mState != BButtonState.Disabled && mState != BButtonState.Pressed)
            {
                if (mState != BButtonState.Hover)
                {
                    mState = BButtonState.Hover;
                    Host.Invalidate();
                }
            }
        }

        void mInput_MouseLeave(WinFormMouseInputEventArgs<object> args)
        {
            Host.Cursor = System.Windows.Forms.Cursors.Default;
            if (mState != BButtonState.Disabled)
            {
                mState = BButtonState.Normal;
                Host.Invalidate();
            }

            if (mTips != null && mTips.IsVisual)
            {
                mTips.Hide();
                mTips.Dispose();
                mTips = null;
            }
        }

        public BorderStyle BorderStyle
        {
            get { return mBorderStyle; }
            set
            {
                if (mBorderStyle != value)
                {
                    mBorderStyle = value;
                    Host.Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置是否自动设置大小
        /// </summary>
        public bool AutoSize
        {
            get;
            set;
        }

        /// <summary>
        /// 文字与图标的间隔
        /// </summary>
        public int Pitch
        {
            get { return mPitch; }
            set { mPitch = value; }
        }

        public bool IsSoildColorIcon
        {
            get;
            set;
        }

        public Point IconOffset
        {
            get;
            set;
        }

        public Image Icon
        {
            get { return mOriginIcon; }
            set
            {
                if (mOriginIcon != value)
                {
                    mOriginIcon = value;
                    if (mIconSize.IsEmpty)
                    {
                        mIconSize = mOriginIcon.Size;
                    }

                    ResetIcon();
                    Host.Invalidate();
                }
            }
        }

        public Color IconNormalColor
        {
            get { return mIconDefaultColor; }
            set
            {
                mIconDefaultColor = value;
                Host.Invalidate();
            }
        }

        public Color IconHoverColor
        {
            get { return mIconHoverColor; }
            set
            {
                mIconHoverColor = value;
                Host.Invalidate();
            }
        }

        public Color IconPressedColor
        {
            get { return mIconPressedColor; }
            set
            {
                mIconPressedColor = value;
                Host.Invalidate();
            }
        }

        public Color IconDisenableColor
        {
            get { return mIconDisenableColor; }
            set
            {
                mIconDisenableColor = value;
                Host.Invalidate();
            }
        }

        public string Text
        {
            get
            {
                return mText;
            }
            set
            {
                if (mText != value)
                {
                    mText = value;

                    if (AutoSize)
                    {
                        var textSize = TextRenderer.MeasureText(Text, Font);
                        var iconSize = Icon == null ? SizeF.Empty : new SizeF(mIconSize.Width, mIconSize.Height);
                        int pitch = Icon == null || string.IsNullOrEmpty(Text) ? 0 : Pitch;
                        var width = (int)Math.Ceiling(pitch + textSize.Width + iconSize.Width + Padding.Left - Padding.Right);
                        var height = (int)Math.Ceiling(Math.Max(textSize.Height, iconSize.Height) + Padding.Top + Padding.Bottom);

                        Size = new Size(width, height);
                    }

                    Host.Invalidate();
                }
            }
        }

        public string ToolTipText
        {
            get;
            set;
        }

        public Point ToolTipOffset
        {
            get;
            set;
        }

        public bool MultiLine
        {
            get
            {
                return mMultiLine;
            }
            set
            {
                mMultiLine = value;
                Host.Invalidate();
            }
        }

        public Color NormalColor
        {
            get { return mNormalColor; }
            set
            {
                if (mNormalColor != value)
                {
                    mNormalColor = value;
                    mNormalBrush = new SolidBrush(mNormalColor);
                    Host.Invalidate();
                }
            }
        }

        public Color HoverColor
        {
            get { return mHoverColor; }
            set
            {
                if (mHoverColor != value)
                {
                    mHoverColor = value;
                    mHoverBrush = new SolidBrush(mHoverColor);
                    Host.Invalidate();
                }
            }
        }

        public Color PressedColor
        {
            get { return mPressedColor; }
            set
            {
                if (mPressedColor != value)
                {
                    mPressedColor = value;
                    mPressedBrush = new SolidBrush(mPressedColor);
                    Host.Invalidate();
                }
            }
        }

        public Color DisenableColor
        {
            get { return mDisenableColor; }
            set
            {
                if (mDisenableColor != value)
                {
                    mDisenableColor = value;
                    mDisenableBrush = new SolidBrush(mDisenableColor);
                    Host.Invalidate();
                }
            }
        }

        public Color ForeNormalColor
        {
            get { return mForeNormalColor; }
            set
            {
                if (mForeNormalColor != value)
                {
                    mForeNormalColor = value;
                    mForeNormalBrush = new SolidBrush(mForeNormalColor);
                    Host.Invalidate();
                }
            }
        }

        public Color ForeHoverColor
        {
            get { return mForeHoverColor; }
            set
            {
                if (mForeHoverColor != value)
                {
                    mForeHoverColor = value;
                    mForeHoverBrush = new SolidBrush(mForeHoverColor);
                    Host.Invalidate();
                }
            }
        }

        public Color ForePressedColor
        {
            get { return mForePressedColor; }
            set
            {
                if (mForePressedColor != value)
                {
                    mForePressedColor = value;
                    mForePressedBrush = new SolidBrush(mForePressedColor);
                    Host.Invalidate();
                }
            }
        }

        public Color ForeDisenableColor
        {
            get { return mForeDisenableColor; }
            set
            {
                if (mForeDisenableColor != value)
                {
                    mForeDisenableColor = value;
                    mForeDisenableBrush = new SolidBrush(mForeDisenableColor);
                    Host.Invalidate();
                }
            }
        }

        public Color BorderColor
        {
            get { return mBorderColor; }
            set
            {
                if (mBorderColor != value)
                {
                    mBorderColor = value;
                    mBorderPen = new Pen(new SolidBrush(mBorderColor));
                }
            }
        }

        public Size IconSize
        {
            get { return mIconSize; }
            set
            {
                if (mIconSize != value)
                {
                    mIconSize = value;
                    ResetIcon();
                    Host.Invalidate();
                }
            }
        }

        public Bitmap CheckedIcon { get; set; }

        public void SetIconColor(Color color, bool changeColors = false)
        {
            if (changeColors)
            {
                IconNormalColor = Color.FromArgb((byte)(238 / 255f * color.A), color.R, color.G, color.B);
                IconHoverColor = ControlPaint.Light(IconNormalColor);
                IconPressedColor = ControlPaint.Dark(IconNormalColor);
            }
            else
            {
                IconNormalColor = IconHoverColor = IconPressedColor = color;
            }
            IconDisenableColor = Color.Gray;
        }

        public void SetIconColor(byte a, byte r, byte g, byte b)
        {
            IconNormalColor = Color.FromArgb((byte)(238 / 255f * a), r, g, b);
            IconHoverColor = ControlPaint.Light(IconNormalColor);
            IconPressedColor = ControlPaint.Dark(IconNormalColor);
            IconDisenableColor = Color.Gray;
        }

        public void SetForeColor(Color color)
        {
            ForeNormalColor = color;
            ForeHoverColor = color;
            ForePressedColor = color;
            ForeDisenableColor = Color.FromArgb(162, 164, 165);
        }

        public void SetForeColor(byte a, byte r, byte g, byte b)
        {
            ForeNormalColor = Color.FromArgb(a, r, g, b);
            ForeHoverColor = ForeNormalColor;
            ForePressedColor = ForeNormalColor;
            ForeDisenableColor = Color.FromArgb(162, 164, 165);
        }

        public void SetBackColor(Color color)
        {
            NormalColor = color;
            HoverColor = ControlPaint.Light(NormalColor);
            PressedColor = ControlPaint.Dark(NormalColor);
            DisenableColor = Color.Gray;
        }

        public void SetBackColor(byte a, byte r, byte g, byte b)
        {
            NormalColor = Color.FromArgb(a, r, g, b);
            HoverColor = ControlPaint.Light(NormalColor);
            PressedColor = ControlPaint.Dark(NormalColor);
            DisenableColor = Color.Gray;
        }

        private Bitmap ResetIconSize(Image bitmap)
        {
            var newIcon = new Bitmap(IconSize.Width, IconSize.Height);

            var g = Graphics.FromImage(newIcon);
            g.SmoothingMode = SmoothingMode.HighQuality;
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(
                bitmap,
                new Rectangle(0, 0, newIcon.Width, newIcon.Height),
                new Rectangle(0, 0, bitmap.Width + 2, bitmap.Height + 2),
                GraphicsUnit.Pixel);
            g.Flush();

            return newIcon;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            mInput.ClearRegions();

            Graphics g = e.Graphics;

            var textSize = g.MeasureString(Text, Font);
            var iconSize = Icon == null ? SizeF.Empty : new SizeF(mIconSize.Width, mIconSize.Height);
            int pitch = Icon == null || string.IsNullOrEmpty(Text) ? 0 : Pitch;
            var width = (int)Math.Ceiling(pitch + textSize.Width + iconSize.Width + Padding.Left - Padding.Right);
            var height = (int)Math.Ceiling(Math.Max(textSize.Height, iconSize.Height) + Padding.Top + Padding.Bottom);

            var iconStartPoint = new Point(
                (int)(mMultiLine ? 0 : (Size.Width - width) / 2) + Location.X + IconOffset.X,
                (int)((Size.Height - iconSize.Height) / 2) + Location.Y + IconOffset.Y);

            var textStartPoint = new Point(
                (int)iconStartPoint.X + pitch + (int)iconSize.Width,
                (int)((mMultiLine ? 0 : (Size.Height - textSize.Height) / 2 + 2)) + Location.Y);

            if (AutoSize)
            {
                Size = new Size(width, height);

                iconStartPoint = new Point(
                    (int)((Size.Width - width) / 2) + Location.X + Padding.Left,
                    (int)((Size.Height - iconSize.Height) / 2) + Location.Y);

                textStartPoint = new Point(
                    (int)iconStartPoint.X + pitch + (int)iconSize.Width - Padding.Right,
                    (int)((Size.Height - textSize.Height) / 2 + 2) + Location.Y);
            }

            if (mState == BButtonState.Pressed)
            {
                iconStartPoint = new Point(iconStartPoint.X + 1, iconStartPoint.Y + 1);
                textStartPoint = new Point(textStartPoint.X + 1, textStartPoint.Y + 1);
            }

            var bounds = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);
            mInput.AddRegion(this, bounds);

            SolidBrush backBrush = null;
            switch (mState)
            {
                case BButtonState.Normal:
                    backBrush = mNormalBrush;
                    break;
                case BButtonState.Hover:
                    backBrush = mHoverBrush;
                    break;
                case BButtonState.Pressed:
                    backBrush = mPressedBrush;
                    break;
                case BButtonState.Disabled:
                    backBrush = mDisenableBrush;
                    break;
            }

            if ((backBrush?.Color.A ?? 0) != 0)
                g.FillRoundRectangle(backBrush, bounds, Radius);

            if (mPixels != null && mIcon != null)
            {
                var temp1 = e.Graphics.CompositingMode;
                var temp2 = e.Graphics.SmoothingMode;
                e.Graphics.CompositingMode = CompositingMode.SourceOver;
                e.Graphics.SmoothingMode = SmoothingMode.None;

                Color iconFixedColor = Color.White;
                switch (mState)
                {
                    case BButtonState.Normal:
                        iconFixedColor = IconNormalColor;
                        break;
                    case BButtonState.Hover:
                        iconFixedColor = IconHoverColor;
                        break;
                    case BButtonState.Pressed:
                        iconFixedColor = IconPressedColor;
                        break;
                    case BButtonState.Disabled:
                        iconFixedColor = IconDisenableColor;
                        break;
                }

                if (IsSoildColorIcon)
                {
                    if (iconFixedColor.A > 0)
                    {
                        for (var i = 0; i < mPixels.Length; i++)
                        {
                            var pixel = mPixels[i];
                            if (pixel.A > 0)
                            {
                                var color = Color.FromArgb(
                                    (byte)(pixel.A * iconFixedColor.A),
                                    iconFixedColor.R,
                                    iconFixedColor.G,
                                    iconFixedColor.B);

                                if (color.A > 0)
                                {
                                    g.FillRectangle(new SolidBrush(color), new Rectangle(iconStartPoint.X + i % mIcon.Width, iconStartPoint.Y + i / mIcon.Height, 1, 1));
                                }
                            }
                        }
                    }
                }
                else
                {

                    var iconFixedColorA = iconFixedColor.A / 255f;
                    var iconFixedColorR = iconFixedColor.R / 255f;
                    var iconFixedColorG = iconFixedColor.G / 255f;
                    var iconFixedColorB = iconFixedColor.B / 255f;

                    if (iconFixedColorA > 0)
                    {
                        for (var i = 0; i < mPixels.Length; i++)
                        {
                            var pixel = mPixels[i];
                            if (pixel.A > 0)
                            {
                                var color = Color.FromArgb(
                                    (byte)(pixel.A * iconFixedColorA),
                                    (byte)(pixel.R * iconFixedColorR),
                                    (byte)(pixel.G * iconFixedColorG),
                                    (byte)(pixel.B * iconFixedColorB));

                                if (color.A > 0)
                                {
                                    g.FillRectangle(new SolidBrush(color), new Rectangle(iconStartPoint.X + i % mIcon.Width, iconStartPoint.Y + i / mIcon.Height, 1, 1));
                                }
                            }
                        }
                    }
                }

                e.Graphics.CompositingMode = temp1;
                e.Graphics.SmoothingMode = temp2;
            }


            if (textSize.Width > 0 && textSize.Height > 0)
            {
                Brush foreBrush = null;
                switch (mState)
                {
                    case BButtonState.Normal:
                        foreBrush = mForeNormalBrush;
                        break;
                    case BButtonState.Hover:
                        foreBrush = mForeHoverBrush;
                        break;
                    case BButtonState.Pressed:
                        foreBrush = mForePressedBrush;
                        break;
                    case BButtonState.Disabled:
                        foreBrush = mForeDisenableBrush;
                        break;
                }

                if (mMultiLine)
                {
                    var startY = textStartPoint.Y;
                    var lines = StringHelper.CheckLength(Font, Text, Size.Width);
                    for (var i = 0; i < lines.Length; i++)
                    {
                        var lineSize = e.Graphics.MeasureString(lines[i], Font);
                        if (Underline && (mState == BButtonState.Hover || mState == BButtonState.Pressed))
                            e.Graphics.FillRectangle(foreBrush, textStartPoint.X, startY + lineSize.Height - 2, lineSize.Width - 2, 1);

                        g.DrawString(lines[i], Font, foreBrush, textStartPoint.X, startY);
                        startY = startY + Font.Height;
                    }

                    if (lines.Length > 1)
                    {
                        Size = new Size(Size.Width, startY - textStartPoint.Y);
                    }
                }
                else
                {
                    if (Underline && (mState == BButtonState.Hover || mState == BButtonState.Pressed))
                        e.Graphics.FillRectangle(foreBrush, textStartPoint.X, textStartPoint.Y + textSize.Height - 2, textSize.Width - 2, 1);

                    g.DrawString(
                             Text,
                             Font,
                             foreBrush,
                             textStartPoint.X,
                             textStartPoint.Y);
                }
            }

            var drawBorder = BorderStyle != BorderStyle.None;
            if (drawBorder && ShowBorderOnChecked)
                drawBorder = Checked || mState == BButtonState.Pressed;

            if (drawBorder)
            {
                var borderColor = Checked ? CheckedColor : BorderColor;
                if (borderColor.A != 0)
                {


                    if (Checked && CheckedIcon != null)
                    {
                        var imgAttribute2 = new System.Drawing.Imaging.ImageAttributes();
                        imgAttribute2.SetColorMatrix(new System.Drawing.Imaging.ColorMatrix()
                        {
                            Matrix00 = CheckedColor.R / 255f,
                            Matrix11 = CheckedColor.G / 255f,
                            Matrix22 = CheckedColor.B / 255f,
                            Matrix33 = CheckedColor.A / 255f
                        });
                        e.Graphics.DrawImage(CheckedIcon, new Rectangle(iconStartPoint, new Size(CheckedIcon.Width, CheckedIcon.Height)), 0, 0, CheckedIcon.Width, CheckedIcon.Height, GraphicsUnit.Pixel, imgAttribute2);
                    }


                    if (borderColor != Color.Transparent)
                    {

                        if (BorderStyle == BorderStyle.FixedSingle)
                        {
                            var pen = new Pen(borderColor);

                            g.DrawRoundRectangle(pen, bounds, Radius);

                            pen.Dispose();
                        }
                        else
                        {
                            const int b2 = 81;

                            var b1 = (borderColor.R + borderColor.G + borderColor.B) / 3;
                            var c2 = Color.FromArgb(81, 81, 81);

                            var light = b1 > b2 ? borderColor : c2;
                            var dark = b1 > b2 ? c2 : borderColor;

                            var pl = new Pen(light);
                            var dl = new Pen(dark);

                            var pen = BorderEffect == BButtonBorderEffect.Concave ? dl : pl;
                            var pen2 = BorderEffect == BButtonBorderEffect.Concave ? pl : dl;

                            g.DrawRoundRectangle(!Checked && mState != BButtonState.Pressed ? pen2 : pen, bounds, Radius);
                            var half = new GraphicsPath();
                            half.AddLines(new PointF[]
                            {
                                new PointF(bounds.Right + 1, bounds.Top - 1),
                                new PointF(bounds.Right + 1, bounds.Bottom + 1),
                                new PointF(bounds.Left - 1, bounds.Bottom + 1),
                            });
                            half.CloseAllFigures();
                            g.SetClip(half);
                            g.DrawRoundRectangle(!Checked && mState != BButtonState.Pressed ? pen : pen2, bounds, Radius);
                            g.ResetClip();

                            pen.Dispose();
                            pen2.Dispose();
                        }
                    }
                }
            }
        }

        private void ResetIcon()
        {
            if (mOriginIcon == null)
            {
                mPixels = null;
            }
            else
            {
                mIcon = ResetIconSize(mOriginIcon);
                mIconSize = mIcon.Size;
                var bitmap = mIcon as Bitmap;
                mPixels = new Color[mIcon.Width * mIcon.Height];
                for (var x = 0; x < mIcon.Width; x++)
                {
                    for (var y = 0; y < mIcon.Height; y++)
                    {
                        var color = bitmap.GetPixel(x, y);

                        mPixels[x + y * bitmap.Width] = color;
                    }
                }
            }
        }

        public event EventHandler CheckdChanged;
        public event EventHandler<MouseEventArgs> Click;
        public event EventHandler<MouseEventArgs> Down;
        public event EventHandler<MouseEventArgs> Up;
    }
}
