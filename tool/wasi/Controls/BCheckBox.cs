using addin.common;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class BCheckBox : BUIElement
    {
        /// <summary>
        /// state of button
        /// </summary>
        enum ButtonState
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal,
            /// <summary>
            /// 鼠标悬浮
            /// </summary>
            Hover,
            /// <summary>
            /// 按下
            /// </summary>
            Pressed,
            /// <summary>
            /// 禁用
            /// </summary>
            Disabled
        }

        private bool mIsChecked;
        private bool mCheckShowName;
        private string mText;
        private string mShowName;
        private Font mFont;
        private ButtonState mState;
        private WinFormInputHelper<int> mInputHelper;

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

        private Color mIconNormalColor;
        private Color mIconHoverColor;
        private Color mIconPressedColor;
        private Color mIconDisenableColor;

        private SolidBrush mIconNormalBrush;
        private SolidBrush mIconHoverBrush;
        private SolidBrush mIconPressedBrush;
        private SolidBrush mIconDisenableBrush;

        public void ClearEvents()
        {
            CheckedChanged = null;
        }

        public bool IsChecked                                                       
        {
            get { return mIsChecked; }
            set
            {
                if (mIsChecked != value)
                {
                    mIsChecked = value;
                }
            }
        }

        public bool IsCheckWithName                                                 
        {
            get;
            set; 
        }

        public bool ColourName                                                      
        { 
            get; 
            set; 
        }

        public Size CheckBoxSize                                                    
        { 
            get;
            set;
        }

        public string Text                                                          
        {
            get { return mText; }
            set
            {
                if (mText != value)
                {
                    mText = value;
                    mCheckShowName = true;
                    Host.Invalidate();
                }
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

        public Color IconNormalColor                                                
        {
            get { return mIconNormalColor; }
            set
            {
                if (mIconNormalColor != value)
                {
                    mIconNormalColor = value;
                    mIconNormalBrush = new SolidBrush(mIconNormalColor);
                    Host.Invalidate();
                }
            }
        }

        public Color IconHoverColor                                                 
        {
            get { return mIconHoverColor; }
            set
            {
                if (mIconHoverColor != value)
                {
                    mIconHoverColor = value;
                    mIconHoverBrush = new SolidBrush(mIconHoverColor);
                    Host.Invalidate();
                }
            }
        }

        public Color IconPressedColor                                               
        {
            get { return mIconPressedColor; }
            set
            {
                if (mIconPressedColor != value)
                {
                    mIconPressedColor = value;
                    mIconPressedBrush = new SolidBrush(mIconPressedColor);
                    Host.Invalidate();
                }
            }
        }

        public Color IconDisenableColor                                             
        {
            get { return mIconDisenableColor; }
            set
            {
                if (mIconDisenableColor != value)
                {
                    mIconDisenableColor = value;
                    mIconDisenableBrush = new SolidBrush(mIconDisenableColor);
                    Host.Invalidate();
                }
            }
        }

        public virtual Color BorderColor                                            
        {
            get { return mBorderColor; }
            set 
            {
                if (mBorderColor != value)
                {
                    mBorderColor = value;
                    Host.Invalidate();
                }
            }
        }

        public Font Font                                                            
        {
            get { return mFont; }
            set 
            {
                if (mFont != value) 
                {
                    mFont = value;
                    Host.Invalidate();
                }
            }
        }

        private void mInputHelper_MouseMove(WinFormMouseInputEventArgs<int> args)   
        {
            if (mState != ButtonState.Disabled)
            {
                if (args.Args.Button != MouseButtons.Left)
                {
                    if (mState != ButtonState.Hover)
                    {
                        mState = ButtonState.Hover;
                        Host.Invalidate();
                    }
                }
            }
        }

        private void mInputHelper_MouseUp(WinFormMouseInputEventArgs<int> args)     
        {
            if (mState != ButtonState.Disabled)
            {
                mState = ButtonState.Normal;
                Host.Invalidate();
            }
        }

        private void mInputHelper_MouseDown(WinFormMouseInputEventArgs<int> args)   
        {
            if (mState != ButtonState.Disabled)
            {
                IsChecked = !IsChecked;
                mState = ButtonState.Pressed;

                if (CheckedChanged != null)
                    CheckedChanged(null, EventArgs.Empty);

                Host.Invalidate();
            }
        }

        private void mInputHelper_MouseLeave(WinFormMouseInputEventArgs<int> args)  
        {
            if (mState != ButtonState.Disabled)
            {
                Host.Cursor = Cursors.Default;
                mState = ButtonState.Normal;
                Host.Invalidate();
            }
        }

        private void mInputHelper_MouseEnter(WinFormMouseInputEventArgs<int> args)  
        {
            if (mState != ButtonState.Disabled)
            {
                Host.Cursor = Cursors.Hand;
                mState = ButtonState.Hover;
                Host.Invalidate();
            }
        }

        public BCheckBox(IBControl host)                                            
            : base(host)                                                            
        {
            Padding = new Padding(2);

            mInputHelper = new WinFormInputHelper<int>(host);
            mInputHelper.MouseEnter += mInputHelper_MouseEnter;
            mInputHelper.MouseLeave += mInputHelper_MouseLeave;
            mInputHelper.MouseDown += mInputHelper_MouseDown;
            mInputHelper.MouseUp += mInputHelper_MouseUp;
            mInputHelper.MouseMove += mInputHelper_MouseMove;

            SetIconColor(IDE.AppConfig.Skin.AccentColor);
            SetBackColor(IDE.AppConfig.Skin.BackColor);
            SetForeColor(IDE.AppConfig.Skin.ForeColor);

            BorderColor = Color.FromArgb(42, 42, 42);

            Font = IDE.AppConfig.Skin.DefaultFont;
        }

        protected override void OnPaint(PaintEventArgs e)                           
        {
            base.OnPaint(e);

            CheckBoxSize = new Size(Size.Height - 8, Size.Height - 8);

            mInputHelper.ClearRegions();

            SolidBrush backBrush = mNormalBrush;
            switch (mState)
            {
                case ButtonState.Disabled: backBrush = mDisenableBrush; break;
                case ButtonState.Hover: backBrush = mHoverBrush; break;
                case ButtonState.Pressed: backBrush = mPressedBrush; break;
            }

            if (backBrush != null && backBrush.Color.A != 0)
                e.Graphics.FillRectangle(backBrush, new Rectangle(Location, Size));
            /*
            e.Graphics.DrawRectangle(
                new Pen(BorderColor),
                new Rectangle(
                    Location.X,
                    Location.Y,
                    Size.Width - 1,
                    Size.Height - 1));
     
            Brush iconBrush = mIconNormalBrush;
            switch (mState)
            {
                case ButtonState.Disabled: iconBrush = mIconDisenableBrush; break;
                case ButtonState.Hover: iconBrush = mIconHoverBrush; break;
                case ButtonState.Pressed: iconBrush = mIconPressedBrush; break;
            }
            */

            var checkBoxBounds = new Rectangle(new Point(Location.X + 4, Location.Y + 4), CheckBoxSize);
            if (BorderColor.A != 0)
            {
                var borderColor = new Pen(BorderColor);
                e.Graphics.DrawRectangle(
                    borderColor,
                    checkBoxBounds);
            }

            // 绘制是否打勾勾
            if (IsChecked) 
            {
                Pen checkMark = new Pen(Color.White, 1.8f);
                checkMark.StartCap = LineCap.Round;
                checkMark.EndCap = LineCap.Round;

                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                e.Graphics.DrawLine(checkMark, Location.X + 4, checkBoxBounds.Bottom - checkBoxBounds.Width / 2, checkBoxBounds.Left + checkBoxBounds.Width / 2.5f, checkBoxBounds.Bottom - 2);
                e.Graphics.DrawLine(checkMark, checkBoxBounds.Left + checkBoxBounds.Width / 2.6f, checkBoxBounds.Bottom - 2, checkBoxBounds.Right, checkBoxBounds.Top);

                checkMark.Dispose();
            }

            // 绘制文字
            if (mCheckShowName)
            {
                mShowName = StringHelper.CheckLength(Font, Text, Size.Width - CheckBoxSize.Width - 1, "..");
                mCheckShowName = false;
            }

            Brush foreBrush = mForeNormalBrush;
            if (ColourName)
            {
                switch (mState)
                {
                    case ButtonState.Disabled: foreBrush = mForeDisenableBrush; break;
                    case ButtonState.Hover: foreBrush = mForeHoverBrush; break;
                    case ButtonState.Pressed: foreBrush = mForePressedBrush; break;
                }
            }

            e.Graphics.DrawString(mShowName, Font, foreBrush, Location.X + CheckBoxSize.Width + 9, Location.Y);

            if (IsCheckWithName)
                checkBoxBounds = new Rectangle(Point.Empty, Size);

            mInputHelper.AddRegion(0, checkBoxBounds);
        }

        public void SetIconColor(Color color)                                       
        {
            if (color.A == 0)
            {
                IconNormalColor = Color.Transparent;
                IconHoverColor = Color.Transparent;
                IconPressedColor = Color.Transparent;
                IconDisenableColor = Color.Transparent;
            }
            else
            {
                IconNormalColor = color;
                IconHoverColor = ControlPaint.Light(IconNormalColor);
                IconPressedColor = ControlPaint.LightLight(IconNormalColor);
                IconDisenableColor = ControlPaint.Dark(IconNormalColor);
            }
        }

        public void SetIconColor(byte a, byte r, byte g, byte b)                    
        {
            if (a == 0)
            {
                IconNormalColor = Color.Transparent;
                IconHoverColor = Color.Transparent;
                IconPressedColor = Color.Transparent;
                IconDisenableColor = Color.Transparent;
            }
            else
            {
                IconNormalColor = Color.FromArgb(a, r, g, b);
                IconHoverColor = ControlPaint.Light(IconNormalColor);
                IconPressedColor = ControlPaint.LightLight(IconNormalColor);
                IconDisenableColor = ControlPaint.Dark(IconNormalColor);
            }
        }

        public void SetForeColor(Color color)                                       
        {
            if (color.A == 0)
            {
                ForeNormalColor = Color.Transparent;
                ForeHoverColor = Color.Transparent;
                ForePressedColor = Color.Transparent;
                ForeDisenableColor = Color.Transparent;
            }
            else
            {
                ForeNormalColor = color;
                ForeHoverColor = ControlPaint.Light(ForeNormalColor);
                ForePressedColor = Color.White;
                ForeDisenableColor = Color.FromArgb(162, 164, 165);
            }
        }

        public void SetForeColor(byte a, byte r, byte g, byte b)                    
        {
            if (a == 0)
            {
                ForeNormalColor = Color.Transparent;
                ForeHoverColor = Color.Transparent;
                ForePressedColor = Color.Transparent;
                ForeDisenableColor = Color.Transparent;
            }
            else
            {
                ForeNormalColor = Color.FromArgb(a, r, g, b);
                ForeHoverColor = ControlPaint.Light(ForeNormalColor);
                ForePressedColor = Color.White;
                ForeDisenableColor = Color.FromArgb(162, 164, 165);
            }
        }

        public void SetBackColor(Color color)                                       
        {
            if (color.A == 0)
            {
                NormalColor = Color.Transparent;
                HoverColor = Color.Transparent;
                PressedColor = Color.Transparent;
                DisenableColor = Color.Transparent;
            }
            else
            {
                NormalColor = color;
                HoverColor = ControlPaint.Light(NormalColor);
                PressedColor = IDE.AppConfig.Skin.AccentColor;
                DisenableColor = NormalColor;
            }
        }

        public void SetBackColor(byte a, byte r, byte g, byte b)                    
        {
            if (a == 0)
            {
                NormalColor = Color.Transparent;
                HoverColor = Color.Transparent;
                PressedColor = Color.Transparent;
                DisenableColor = Color.Transparent;
            }
            else
            {
                NormalColor = Color.FromArgb(a, r, g, b);
                HoverColor = ControlPaint.Light(NormalColor);
                PressedColor = IDE.AppConfig.Skin.AccentColor;
                DisenableColor = NormalColor;
            }
        }

        public event EventHandler CheckedChanged;
    }
}
