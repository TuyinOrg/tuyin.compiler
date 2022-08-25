using System;
using System.Drawing;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public delegate void ButtonCustomDrawDelegate(PaintEventArgs e, BCustomDrawButton button);

    public class BCustomDrawButton : BUIElement
    {
        private BButtonState mState = BButtonState.Normal;
        private WinFormInputHelper<object> mInput;

        private Color mBackNormalColor;
        private Color mBackHoverColor;
        private Color mBackPressedColor;
        private Color mBackDisenableColor;

        private Color mBorderNormalColor;
        private Color mBorderHoverColor;
        private Color mBorderPressedColor;
        private Color mBorderDisenableColor;

        public BButtonState State
        {
            get { return Enabled ? mState : BButtonState.Disabled; }
            set { mState = value; }
        }

        public Color BorderNormalColor                                      
        {
            get { return mBorderNormalColor; }
            set
            {
                if (mBorderNormalColor != value)
                {
                    mBorderNormalColor = value;
                    Host.Invalidate();
                }
            }
        }

        public Color BorderHoverColor                                       
        {
            get { return mBorderHoverColor; }
            set
            {
                if (mBorderHoverColor != value)
                {
                    mBorderHoverColor = value;
                    Host.Invalidate();
                }
            }
        }

        public Color BorderPressedColor                                     
        {
            get { return mBorderPressedColor; }
            set
            {
                if (mBorderPressedColor != value)
                {
                    mBorderPressedColor = value;
                    Host.Invalidate();
                }
            }
        }

        public Color BorderDisenableColor                                   
        {
            get { return mBorderDisenableColor; }
            set
            {
                if (mBorderDisenableColor != value)
                {
                    mBorderDisenableColor = value;
                    Host.Invalidate();
                }
            }
        }

        public Color BackNormalColor                                        
        {
            get { return mBackNormalColor; }
            set
            {
                if (mBackNormalColor != value)
                {
                    mBackNormalColor = value;
                    Host.Invalidate();
                }
            }
        }

        public Color BackHoverColor                                         
        {
            get { return mBackHoverColor; }
            set
            {
                if (mBackHoverColor != value)
                {
                    mBackHoverColor = value;
                    Host.Invalidate();
                }
            }
        }

        public Color BackPressedColor                                       
        {
            get { return mBackPressedColor; }
            set
            {
                if (mBackPressedColor != value)
                {
                    mBackPressedColor = value;
                    Host.Invalidate();
                }
            }
        }

        public Color BackDisenableColor                                     
        {
            get { return mBackDisenableColor; }
            set
            {
                if (mBackDisenableColor != value)
                {
                    mBackDisenableColor = value;
                    Host.Invalidate();
                }
            }
        }

        public override Point Location                                      
        {
            get;
            set;
        }

        public override Size Size                                           
        {
            get;
            set;
        }

        public bool Checked
        {
            get;
            set;
        }

        public bool ShowBorder 
        {
            get;
            set;
        }

        public object Tag 
        {
            get;
            set;
        }

        public BCustomDrawButton(BControl host)
            : base(host)                                                    
        {
            SetBackColor(IDE.AppConfig.Skin.ForeColor);
            SetBorderColor(IDE.AppConfig.Skin.AccentColor);

            ShowBorder = true;
            mInput = new WinFormInputHelper<object>(host);
            mInput.MouseEnter += mInput_MouseEnter;
            mInput.MouseLeave += mInput_MouseLeave;
            mInput.MouseMove += mInput_MouseMove;
            mInput.MouseDown += mInput_MouseDown;
            mInput.MouseUp += mInput_MouseUp;
        }

        void mInput_MouseEnter(WinFormMouseInputEventArgs<object> args)     
        {
            if (State != BButtonState.Disabled)
            {
                Host.Cursor = Cursors.Hand;
            }
        }

        void mInput_MouseUp(WinFormMouseInputEventArgs<object> args)        
        {
            if (State != BButtonState.Disabled)
            {
                if (Up != null)
                    Up(this, EventArgs.Empty);

                if (State == BButtonState.Pressed)
                    if (Click != null)
                        Click(this, EventArgs.Empty);

                if (State != BButtonState.Disabled)
                    State = BButtonState.Normal;

                Host.Invalidate();
            }
        }

        void mInput_MouseDown(WinFormMouseInputEventArgs<object> args)      
        {
            if (State != BButtonState.Disabled)
            {
                State = BButtonState.Pressed;

                if (Down != null)
                    Down(this, EventArgs.Empty);

                Host.Invalidate();
            }
        }

        void mInput_MouseMove(WinFormMouseInputEventArgs<object> args)      
        {
            if (State != BButtonState.Disabled && State != BButtonState.Pressed)
            {
                if (State != BButtonState.Hover)
                {
                    State = BButtonState.Hover;
                    Host.Invalidate();
                }
            }
        }

        void mInput_MouseLeave(WinFormMouseInputEventArgs<object> args)     
        {
            Host.Cursor = System.Windows.Forms.Cursors.Default;
            if (State != BButtonState.Disabled)
            {
                State = BButtonState.Normal;
                Host.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)                   
        {
            mInput.ClearRegions();

            var bounds = new Rectangle(Location, Size);
            mInput.AddRegion(this, bounds);

            Color backColor = Color.Transparent;
            Color borderColor = Color.Transparent;
  
            switch (State)
            {
                case BButtonState.Normal:
                    backColor = Checked ? BackDisenableColor : BackNormalColor;
                    borderColor = Checked ? BorderDisenableColor : BorderNormalColor;
                    break;
                case BButtonState.Hover:
                    backColor = BackHoverColor;
                    borderColor = BorderHoverColor;
                    break;
                case BButtonState.Pressed:
                    backColor = BackPressedColor;
                    borderColor = BorderPressedColor;
                    break;
                case BButtonState.Disabled:
                    backColor = BackDisenableColor;
                    borderColor = BorderDisenableColor;
                    break;
            }

            if (backColor != Color.Transparent && backColor.A > 0)
            {
                using (var brush = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(brush, bounds);
                }
            }

            if (ShowBorder)
            {
                if (borderColor != Color.Transparent && borderColor.A > 0)
                {
                    using (var pen = new Pen(borderColor))
                    {
                        e.Graphics.DrawRectangle(pen, bounds);
                    }
                }
            }


            CustomDraw?.Invoke(e, this);
        }

        public void SetBorderColor(Color color)                             
        {
            BorderNormalColor = color;
            BorderHoverColor = ControlPaint.Light(BackNormalColor);
            BorderPressedColor = ControlPaint.Dark(BackNormalColor);
            BorderDisenableColor = Color.Gray;
        }

        public void SetBorderColor(byte a, byte r, byte g, byte b)          
        {
            BorderNormalColor = Color.FromArgb(a, r, g, b);
            BorderHoverColor = ControlPaint.Light(BackNormalColor);
            BorderPressedColor = ControlPaint.Dark(BackNormalColor);
            BorderDisenableColor = Color.Gray;
        }

        public void SetBackColor(Color color)                               
        {
            BackNormalColor = Color.FromArgb(color.A, color.R, color.G, color.B);
            BackHoverColor = ControlPaint.Light(BackNormalColor);
            BackPressedColor = ControlPaint.Dark(BackNormalColor);
            BackDisenableColor = Color.Gray;
        }

        public void SetBackColor(byte a, byte r, byte g, byte b)            
        {
            BackNormalColor = Color.FromArgb(a, r, g, b);
            BackHoverColor = ControlPaint.Light(BackNormalColor);
            BackPressedColor = ControlPaint.Dark(BackNormalColor);
            BackDisenableColor = Color.Gray;
        }

        public event ButtonCustomDrawDelegate CustomDraw;
        public event EventHandler Click;
        public event EventHandler Down;
        public event EventHandler Up;
    }
}
