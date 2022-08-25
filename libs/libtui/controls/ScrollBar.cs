using libtui.drawing;
using System;
using System.Threading;

namespace libtui.controls
{
    public enum ScrollBarButtonState
    {
        Normal,
        Hover,
        HalfHover,
        Pressed,
    }

    public class ScrollBar : ControlBase
    {
        private const int SCROLL_BUTTON_TAG = 101;
        private const int SCROLL_DROP_AREA_TAG = 102;
        private const int SCROLL_BUTTON_LEFT_TAG = 103;
        private const int SCROLL_BUTTON_RIGHT_TAG = 104;

        private bool mInputControl = true;
        private bool mVisual = false;
        private bool mAutoHide = false;
        private bool mEnableScrollButton = false;
        private bool mRoundScrollButton = true;
        private int mEnterButtonTag;
        private int mDownButtonTag;

        private CancellationTokenSource mTimerCancel;
        private DateTime mTimerBeginDateTime;

        private int mDepth;
        private int mMaxDepth;
        private int mButtonOrWheelScrollLength;
        private Size mClientSize;
        private Color mHoverColor;
        private Color mPressedColor;
        private Color mNormalColor;
        private Color mDisenbaleColor;

        private Color mButtonHoverColor;
        private Color mButtonPressedColor;
        private Color mButtonNormalColor;
        private Color mButtonDisenbaleColor;
        private Color mButtonBackColor;
        private Color mButtonBorderColor;

        private Brush mHoverBrush;
        private Brush mPressedBrush;
        private Brush mNormalBrush;
        private Brush mDisenableBrush;

        private Brush mButtonHoverBrush;
        private Brush mButtonPressedBrush;
        private Brush mButtonNormalBrush;
        private Brush mButtonDisenableBrush;

        private int mScrollButtonPressedDepth;
        private ScrollBarButtonState mScrollButtonState;
        private Point mScrollButtonPressedLocation;

        /// <summary>
        /// 拖动按钮最小高度
        /// </summary>
        public int MinButtonHeight
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置是否开启滚动按钮
        /// </summary>
        public bool EnableScrollButton
        {
            get { return mEnableScrollButton; }
            set { mEnableScrollButton = value; }
        }

        /// <summary>
        /// 获取或设置是否显示圆角
        /// </summary>
        public bool RoundScrollButton
        {
            get { return mRoundScrollButton; }
            set { mRoundScrollButton = value; }
        }

        /// <summary>
        /// 获取或设置最小滚动距离
        /// </summary>
        public int SmallChange
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置滚动按钮或滚轮滚动距离
        /// </summary>
        public int ButtonOrWheelScrollLength
        {
            get { return mButtonOrWheelScrollLength; }
            set { mButtonOrWheelScrollLength = value; }
        }

        /// <summary>
        /// 获取滚动条状态
        /// </summary>
        public ScrollBarButtonState State
        {
            get { return mScrollButtonState; }
            set
            {
                if (mScrollButtonState != value)
                {
                    var oldState = mScrollButtonState;
                    mScrollButtonState = value;

                    if (StateChanged != null)
                        StateChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 获取或设置悬浮颜色
        /// </summary>
        public virtual Color HoverColor
        {
            get { return mHoverColor; }
            set
            {
                if (mHoverColor != value)
                {
                    mHoverColor = value;
                    mHoverBrush = new SolidBrush(mHoverColor);
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置按下的颜色
        /// </summary>
        public virtual Color PressedColor
        {
            get { return mPressedColor; }
            set
            {
                if (mPressedColor != value)
                {
                    mPressedColor = value;
                    mPressedBrush = new SolidBrush(mPressedColor);
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置平时的颜色
        /// </summary>
        public virtual Color NormalColor
        {
            get { return mNormalColor; }
            set
            {
                if (mNormalColor != value)
                {
                    mNormalColor = value;
                    mNormalBrush = new SolidBrush(mNormalColor);
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置无效化时的颜色
        /// </summary>
        public virtual Color DisenableColor
        {
            get { return mDisenbaleColor; }
            set
            {
                if (mDisenbaleColor != value)
                {
                    mDisenbaleColor = value;
                    mDisenableBrush = new SolidBrush(mDisenbaleColor);
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置悬浮颜色
        /// </summary>
        public virtual Color ButtonHoverColor
        {
            get { return mButtonHoverColor; }
            set
            {
                if (mButtonHoverColor != value)
                {
                    mButtonHoverColor = value;
                    mButtonHoverBrush = new SolidBrush(mButtonHoverColor);
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置按下的颜色
        /// </summary>
        public virtual Color ButtonPressedColor
        {
            get { return mButtonPressedColor; }
            set
            {
                if (mButtonPressedColor != value)
                {
                    mButtonPressedColor = value;
                    mButtonPressedBrush = new SolidBrush(mButtonPressedColor);
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置平时的颜色
        /// </summary>
        public virtual Color ButtonNormalColor
        {
            get { return mButtonNormalColor; }
            set
            {
                if (mButtonNormalColor != value)
                {
                    mButtonNormalColor = value;
                    mButtonNormalBrush = new SolidBrush(mButtonNormalColor);
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置无效化时的颜色
        /// </summary>
        public virtual Color ButtonDisenableColor
        {
            get { return mButtonDisenbaleColor; }
            set
            {
                if (mButtonDisenbaleColor != value)
                {
                    mButtonDisenbaleColor = value;
                    mButtonDisenableBrush = new SolidBrush(mButtonDisenbaleColor);
                    Invalidate();
                }
            }
        }

        public int ButtonRadius
        {
            get;
            set;
        } = 2;

        public int Width
        {
            get;
            set;
        }

        public int Length
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是横向
        /// </summary>
        public bool IsHorizontal
        {
            get;
            set;
        }

        /// <summary>
        /// 获取可滚动的距离
        /// </summary>
        public int DropDepth
        {
            get
            {
                return Math.Max(0, MaxDepth - (ActualLength ?? Length));
            }
        }

        /// <summary>
        /// 最大深度
        /// </summary>
        public int MaxDepth
        {
            get { return mMaxDepth; }
            set
            {
                if (mMaxDepth != value)
                {
                    mMaxDepth = value;
                    if (Depth > mMaxDepth)
                        Depth = mMaxDepth;

                    ComputeHideState();
                }
            }
        }

        /// <summary>
        /// 当前深度
        /// </summary>
        public int Depth
        {
            get { return mDepth; }
            set
            {
                var tempDepth = value;
                var maxValue = Math.Max(0, DropDepth);

                if (tempDepth < 0) tempDepth = 0;
                if (tempDepth > maxValue) tempDepth = maxValue;

                if (mDepth != tempDepth)
                {
                    mDepth = tempDepth;
                    if (mDepth > mMaxDepth)
                        mDepth = mMaxDepth;

                    Invalidate();
                    Scroll?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 获取显示状态
        /// </summary>
        public bool Visible
        {
            get { return mVisual; }
            set
            {
                if (mVisual != value)
                {
                    mVisual = value;
                    if (VisualChanged != null)
                        VisualChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 获取或设置是否自动隐藏
        /// </summary>
        public bool AutoHide
        {
            get { return mAutoHide; }
            set { mAutoHide = value; }
        }

        /// <summary>
        /// 获取或设置是否接收输入控制
        /// </summary>
        public bool InputControl
        {
            get { return mInputControl; }
            set { mInputControl = value; }
        }

        public bool InputControlWheel
        {
            get;
            set;
        }

        public int? ActualLength
        {
            get;
            set;
        }

        public ScrollBar()
        {
            AutoHide = true;

            Width = 12;
            MinButtonHeight = 18;
            ButtonOrWheelScrollLength = 70;
            mVisual = true;
            EnableScrollButton = false;
            RoundScrollButton = false;
        }

        void ComputeHideState()
        {
            var maxValue = Math.Max(0, MaxDepth - Length);

            var temp = maxValue > 0;
            if (AutoHide)
            {
                Visible = temp;
            }

            IsEnabled = temp;
        }

        void TimerTick(object sender)
        {
            if (DateTime.Now - mTimerBeginDateTime > TimeSpan.FromMilliseconds(500))
            {
                if (mDownButtonTag == SCROLL_BUTTON_LEFT_TAG)
                {
                    Depth = Math.Max(Depth - ButtonOrWheelScrollLength, 0);
                }
                else if (mDownButtonTag == SCROLL_BUTTON_RIGHT_TAG)
                {
                    Depth = Math.Min(Depth + ButtonOrWheelScrollLength, DropDepth);
                }
            }
        }

        public override void ApplySkin(Skin skin)
        {
            DisenableColor = Color.LightGray;
            NormalColor = Color.FromArgb(65, 104, 104, 104);
            HoverColor = Color.FromArgb(255, 158, 158, 158);
            PressedColor = Color.FromArgb(255, 239, 235, 239);

            ButtonDisenableColor = Color.FromArgb(85, 85, 88);
            ButtonNormalColor = Color.FromArgb(255, 153, 153, 153);
            ButtonHoverColor = Color.FromArgb(188, skin.AccentColor.R, skin.AccentColor.G, skin.AccentColor.B);
            ButtonPressedColor = Color.FromArgb(255, skin.AccentColor.R, skin.AccentColor.G, skin.AccentColor.B);
        }

        public override void OnKeyDown(KeyEventArgs e)
        {
            if (!InputControl) return;

            var doInvalidate = false;
            if (e.KeyCode == Keys.Up)
            {
                Depth = Math.Max(Depth - ButtonOrWheelScrollLength, 0);
                doInvalidate = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                Depth = Math.Min(Depth + ButtonOrWheelScrollLength, DropDepth);
                doInvalidate = true;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                //Depth = Math.Min(Depth + Host.Height, DropDepth);
                doInvalidate = true;
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //Depth = Math.Max(Depth - Host.Height, 0);
                doInvalidate = true;
            }
            else if (e.KeyCode == Keys.End)
            {
                Depth = DropDepth;
                doInvalidate = true;
            }
            else if (e.KeyCode == Keys.Home)
            {
                Depth = 0;
                doInvalidate = true;
            }

            if (doInvalidate)
                Invalidate();
        }

        public override void OnMouseWheel(MouseEventArgs e)
        {
            var oldDepth = Depth;

            var count = e.Delta / 120;
            if (count > 0)
            {
                if (SmallChange != 0)
                    Depth = Depth / SmallChange * SmallChange - SmallChange;
                else
                    Depth = Math.Max(Depth - ButtonOrWheelScrollLength * count, 0);
            }
            else
            {
                if (SmallChange != 0)
                    Depth = Depth / SmallChange * SmallChange + SmallChange;
                else
                    Depth = Math.Min(Depth + ButtonOrWheelScrollLength * -count, DropDepth);
            }

            if (oldDepth != Depth)
                Invalidate();
        }

        public override void OnSizeChanged(SizeChangeEventArgs e)
        {
            Depth = Depth;
            ComputeHideState();
            Invalidate();
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            switch ((int)e.Tag)
            {
                case SCROLL_BUTTON_TAG:
                    if (State != ScrollBarButtonState.Pressed)
                        State = ScrollBarButtonState.Hover;
                    break;
                case SCROLL_DROP_AREA_TAG:
                    if (State != ScrollBarButtonState.Pressed)
                        State = ScrollBarButtonState.HalfHover;
                    break;
            }

            Invalidate();
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            mDownButtonTag = (int)e.Tag;
            if (e.Button == MouseButtons.Left)
            {
                switch (mDownButtonTag)
                {
                    case SCROLL_BUTTON_TAG:
                        State = ScrollBarButtonState.Pressed;
                        mScrollButtonPressedLocation = e.Location;
                        mScrollButtonPressedDepth = Depth;

                        App.MouseMove += GlobalHandler_MouseMove;
                        App.MouseButtonUp += GlobalHandler_MouseLeftButtonUp;
                        break;
                    case SCROLL_BUTTON_LEFT_TAG:
                    case SCROLL_BUTTON_RIGHT_TAG:
                        mTimerCancel = App.SetTimeout(50, TimerTick);

                        // 点击时先做一次事件
                        mTimerBeginDateTime = DateTime.Now - TimeSpan.FromMilliseconds(2000);
                        TimerTick(null);
                        mTimerBeginDateTime = DateTime.Now;
                        break;
                }

                AllowFocus = false;
                Invalidate();
            }
        }

        public override void OnMouseUp(MouseEventArgs e)
        {
            switch ((int)e.Tag)
            {
                case SCROLL_BUTTON_TAG:
                    State = ScrollBarButtonState.Hover;
                    break;
                case SCROLL_DROP_AREA_TAG:
                    break;
            }

            AllowFocus = true;
        }

        public override void OnMouseClick(MouseEventArgs e)
        {
            switch ((int)e.Tag)
            {
                case SCROLL_BUTTON_TAG:
                    break;
                case SCROLL_DROP_AREA_TAG:

                    var viewLength = Length;
                    if (MaxDepth > viewLength)
                    {
                        var computeValue = 0d;
                        if (IsHorizontal)
                            computeValue = (e.X - e.Rectangle.Left) / e.Rectangle.Width;
                        else
                            computeValue = (e.Y - e.Rectangle.Top) / e.Rectangle.Height;

                        if (SmallChange != 0)
                        {
                            Depth = (int)(computeValue * MaxDepth) / SmallChange * SmallChange;
                        }
                        else
                        {
                            Depth = (int)(computeValue * MaxDepth);
                        }

                        if (Depth < 0)
                            Depth = 0;

                        var maxValue = Math.Max(0, MaxDepth - viewLength);
                        if (Depth > maxValue)
                            Depth = maxValue;
                    }

                    break;
            }
        }

        public override void OnMosueEnter(MouseEventArgs e)
        {
            mEnterButtonTag = (int)e.Tag;

            switch (mEnterButtonTag)
            {
                case SCROLL_BUTTON_TAG:
                    if (State != ScrollBarButtonState.Pressed)
                        State = ScrollBarButtonState.Hover;
                    break;
                case SCROLL_DROP_AREA_TAG:
                    if (State != ScrollBarButtonState.Pressed)
                        State = ScrollBarButtonState.HalfHover;
                    break;
            }

            Invalidate();
        }

        public override void OnMouseLeave(MouseEventArgs e)
        {
            mEnterButtonTag = 0;
            mDownButtonTag = 0;

            if (!mTimerCancel.Token.IsCancellationRequested)
                mTimerCancel.Cancel();

            switch ((int)e.Tag)
            {
                case SCROLL_BUTTON_TAG:
                    if (State != ScrollBarButtonState.Pressed)
                        State = ScrollBarButtonState.Normal;
                    break;
                case SCROLL_DROP_AREA_TAG:
                    if (State != ScrollBarButtonState.Pressed)
                        State = ScrollBarButtonState.Normal;
                    break;
            }

            Invalidate();
        }

        public override void Paint(PaintEventArgs e)
        {
            Rectangle originBounds = new Rectangle(
                     Location.X,
                     Location.Y,
                     IsHorizontal ? Length : Width,
                     IsHorizontal ? Width : Length);

            Rectangle bounds = originBounds;

            if (EnableScrollButton)
            {
                bounds = new Rectangle(
                    IsHorizontal ? bounds.X + Width * 2 : bounds.X,
                    IsHorizontal ? bounds.Y : bounds.Y + Width * 2,
                    IsHorizontal ? bounds.Width - Width * 4 : bounds.Width,
                    IsHorizontal ? bounds.Height : bounds.Height - Width * 4);
            }

            var viewLength = Length;
            var buttonViewLength = viewLength - (EnableScrollButton ? Width * 4 : 0);
            var buttonLength = DropDepth <= 0 ? buttonViewLength : (int)(buttonViewLength * ((float)viewLength / MaxDepth));

            var originLength = buttonLength;
            if (buttonLength < MinButtonHeight)
                buttonLength = MinButtonHeight;

            if (buttonLength > buttonViewLength)
                buttonLength = buttonViewLength;

            if (!IsEnabled)
                buttonLength = 0;

            var buttonWidth = (IsHorizontal ? buttonLength : Width);// - (IsHorizontal ? Padding.Right + Padding.Left : 0);
            var buttonHeight = (IsHorizontal ? Width : buttonLength);// - (IsHorizontal ? 0 : Padding.Bottom + Padding.Top);

            var diffLength = buttonLength - originLength;
            var buttonRate = (float)Depth / DropDepth;
            var buttonLocaction = (viewLength - diffLength - (IsHorizontal ? buttonWidth : buttonHeight)) * buttonRate;
            var buttonBounds = new Rectangle(
                (int)((IsHorizontal ? buttonLocaction : 0) + bounds.X),
                (int)((IsHorizontal ? 0 : buttonLocaction) + bounds.Y),
                buttonWidth,
                buttonHeight);

            if (mVisual)
            {
                e.Input.AddRegion(SCROLL_DROP_AREA_TAG, bounds);
                if (MaxDepth > 0)
                {
                    if (buttonLength > 0)
                    {
                        e.Input.AddRegion(SCROLL_BUTTON_TAG, buttonBounds);

                        Brush brush = null;
                        switch (State)
                        {
                            case ScrollBarButtonState.HalfHover:
                            case ScrollBarButtonState.Hover:
                                brush = mHoverBrush;
                                break;
                            case ScrollBarButtonState.Normal:
                                brush = mNormalBrush;
                                break;
                            case ScrollBarButtonState.Pressed:
                                brush = mPressedBrush;
                                break;
                        }

                        // 绘制中间滚动按钮
                        if (brush != null && buttonBounds.X >= 0 && buttonBounds.Y >= 0)
                            if (RoundScrollButton)
                                GraphicsExpand.FillRoundRectangle(e.Graphics, brush, buttonBounds, ButtonRadius);
                            else
                                e.Graphics.FillGeometry(brush, buttonBounds);
                    }
                }

                // 绘制左右滚动按钮
                if (EnableScrollButton)
                {
                    var leftButtonRectangle = new Rectangle(
                            IsHorizontal ? originBounds.X : originBounds.X,
                            IsHorizontal ? originBounds.Y : originBounds.Y,
                            IsHorizontal ? Width * 2 : Width,
                            IsHorizontal ? Width : Width * 2);

                    var rightButtonRectangle = new Rectangle(
                            IsHorizontal ? originBounds.X + originBounds.Width - Width * 2 : originBounds.X,
                            IsHorizontal ? originBounds.Y : originBounds.Y + originBounds.Height - Width * 2,
                            IsHorizontal ? Width * 2 : Width,
                            IsHorizontal ? Width : Width * 2);

                    var leftButtonTriangleRectangle = new RectangleF(
                           IsHorizontal ? originBounds.X + Width / 2 : originBounds.X - 1,
                           IsHorizontal ? originBounds.Y + Width / 4 : originBounds.Y + Width / 2 + Width / 4,
                           IsHorizontal ? Width : Width + 1,
                           IsHorizontal ? Width / 2 : Width / 2 + 0.5f);

                    var rightButtonTriangleRectangle = new RectangleF(
                            IsHorizontal ? originBounds.X + originBounds.Width - Width * 2 + Width / 2 : originBounds.X,
                            IsHorizontal ? originBounds.Y + Width / 4 : originBounds.Y + originBounds.Height - Width * 2 + Width / 2,
                            IsHorizontal ? Width + 1 : Width,
                            IsHorizontal ? Width / 2 + 0.5f : Width / 2);

                    e.Input.AddRegion(SCROLL_BUTTON_LEFT_TAG, leftButtonRectangle);
                    e.Input.AddRegion(SCROLL_BUTTON_RIGHT_TAG, rightButtonRectangle);

                    Brush leftButtonBrush = DropDepth == 0 || !IsEnabled ? mButtonDisenableBrush : (mDownButtonTag == SCROLL_BUTTON_LEFT_TAG ? mButtonPressedBrush : (mEnterButtonTag == SCROLL_BUTTON_LEFT_TAG ? mButtonHoverBrush : mButtonNormalBrush));
                    Brush rightButtonBrush = DropDepth == 0 || !IsEnabled ? mButtonDisenableBrush : (mDownButtonTag == SCROLL_BUTTON_RIGHT_TAG ? mButtonPressedBrush : (mEnterButtonTag == SCROLL_BUTTON_RIGHT_TAG ? mButtonHoverBrush : mButtonNormalBrush));

                    e.Graphics.FillTriangle(
                            leftButtonBrush,
                            leftButtonTriangleRectangle,
                            IsHorizontal ? -90 : 0);

                    e.Graphics.FillTriangle(
                            rightButtonBrush,
                            rightButtonTriangleRectangle,
                            IsHorizontal ? 90 : 180);
                }
            }
        }

        public void SetMainColor(Color color)
        {
            NormalColor = Color.FromArgb((byte)(88 / 255f * color.A), color.R, color.G, color.B);
            HoverColor = Color.FromArgb((byte)(188 / 255f * color.A), color.R, color.G, color.B);
            PressedColor = Color.FromArgb((byte)(225 / 255f * color.A), color.R, color.G, color.B);
        }

        public void SetMainColor(byte a, byte r, byte g, byte b)
        {
            NormalColor = Color.FromArgb((byte)(88 / 255f * a), r, g, b);
            HoverColor = Color.FromArgb((byte)(188 / 255f * a), r, g, b);
            PressedColor = Color.FromArgb((byte)(225 / 255f * a), r, g, b);
        }

        void GlobalHandler_MouseLeftButtonUp(MouseEventArgs e)
        {
            App.MouseMove -= GlobalHandler_MouseMove;
            App.MouseButtonUp -= GlobalHandler_MouseLeftButtonUp;

            mDownButtonTag = 0;
            if (!mTimerCancel.Token.IsCancellationRequested)
                mTimerCancel.Cancel();

            if (State == ScrollBarButtonState.Pressed)
            {
                State = ScrollBarButtonState.Normal;
                AllowFocus = true;
                Invalidate();
            }
        }

        void GlobalHandler_MouseMove(MouseEventArgs e)
        {
            if (State == ScrollBarButtonState.Pressed)
            {
                var point = PointToClient(e.Location);
                var viewLength = (ActualLength ?? Length);
                var oldDepth = Depth;

                float computeValue = 0;
                if (IsHorizontal)
                    computeValue = (float)(point.X - mScrollButtonPressedLocation.X) / viewLength;
                else
                    computeValue = (float)(point.Y - mScrollButtonPressedLocation.Y) / viewLength;

                if (SmallChange != 0)
                {
                    Depth = mScrollButtonPressedDepth + (int)(computeValue * MaxDepth) / SmallChange * SmallChange;
                }
                else
                {
                    Depth = mScrollButtonPressedDepth + (int)(computeValue * MaxDepth);
                }

                if (Depth < 0)
                    Depth = 0;

                var maxValue = Math.Max(0, MaxDepth - viewLength);
                if (Depth > maxValue)
                    Depth = maxValue;

                if (oldDepth != Depth)
                    Invalidate();
            }
        }

        public event EventHandler Scroll;
        public event EventHandler VisualChanged;
        public event EventHandler StateChanged;
    }
}
