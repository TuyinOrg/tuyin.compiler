using System;
using System.Drawing;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class BColorField : BUIElement
    {
        class ColorFloatingForm : AdvancedForm
        {
            private BControl mCtrl;
            private BColorField mField;
            private BColorPicker mPicker;

            protected internal override bool CloseBox => false;

            protected internal override bool MaximizeBox => false;

            protected internal override bool MinimizeBox => false;

            public Color Selectd
            {
                get { return mPicker?.Selected ?? default; }
                set 
                { 
                    if (mPicker != null && mPicker.Selected != value)
                        mPicker.Selected = value;
                }
            }
            public override Color ForeColor 
            {
                get { return mPicker?.ForeColor ?? default; }
                set
                {
                    if (mPicker != null && mPicker.ForeColor != value)
                        mPicker.ForeColor = value;
                }
            }

            public Color BorderColor 
            {
                get;
                set;
            }

            public int MaskLength { get; set; }

            public ColorFloatingForm(BColorField field)
            {
                Font = IDE.AppConfig.Skin.DefaultFont;
                mField = field;
                ShowInTaskbar = false;
                Size = new Size(352, 280);
                CanResize = false;
            }

            public void Init() 
            {
                mCtrl = new BControl(false);
                mCtrl.Location = new Point(1, 1);
                mCtrl.Size = new Size(Size.Width - 2, Size.Height - 2);
                mCtrl.Paint += MCtrl_Paint;

                mPicker = new BColorPicker(mCtrl);
                mPicker.ShowSelected = false;
                mPicker.Size = mCtrl.Size;
                mPicker.ForeColor = ForeColor;
                mPicker.SelectedChanged += MCtrl_SelectedChanged;

                this.Controls.Add(mCtrl);
            }

            private void MCtrl_Paint(object sender, PaintEventArgs e)
            {
                mPicker?.Paint(e);
            }

            private void MCtrl_SelectedChanged(Color color)
            {
                mField.Color = color;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(Point.Empty, ClientSize));


                e.Graphics.DrawRectangle(new Pen(BorderColor), 0, 0, Width - 1, Height - 1);
                e.Graphics.DrawLine(new Pen(BackColor), 1, 0, MaskLength - 1, 0);
            }

            internal void Save()
            {
                mPicker.Save();
            }
        }

        private Color mColor;
        private Color mPopupBackColor;
        private bool mIsPoping;
        private bool mHoverButton;
        private ColorFloatingForm mForm;
        private WinFormInputHelper<object> mInputHelper;

        public bool ShowText 
        {
            get;
            set;
        }

        public Color Color 
        {
            get { return mColor; }
            set 
            {
                if (mColor != value) 
                {
                    mColor = value;
                    ColorChanged?.Invoke(mColor);

                    Host.Invalidate();
                }
            }
        }

        public Color ForeColor 
        {
            get;
            set;
        }

        public Color BorderColor 
        {
            get;
            set;
        }

        public Color PopupBackColor 
        {
            get { return mPopupBackColor; }
            set 
            {
                if (mPopupBackColor != value)
                {
                    mPopupBackColor = value;
                }
            }
        }

        public BColorField(IBControl host) 
            : base(host)
        {
            ForeColor = IDE.AppConfig.Skin.ForeColor;
            BorderColor = IDE.AppConfig.Skin.AccentColor;
            PopupBackColor = IDE.AppConfig.Skin.BackColor;

            mInputHelper = new WinFormInputHelper<object>(host);
            mInputHelper.MouseEnter += MInputHelper_MouseEnter;
            mInputHelper.MouseLeave += MInputHelper_MouseLeave;
            mInputHelper.MouseDown += MInputHelper_MouseDown;

            mForm = new ColorFloatingForm(this);
            //mForm.BackColor = PopupBackColor;
            mForm.Init();
            mForm.ForeColor = ForeColor;
            mForm.BorderColor = BorderColor;
            mForm.BackColor = PopupBackColor;
            //mForm.Poping += MForm_Poping;
            //mForm.Closed += MForm_Closed;

            mForm.VisibleChanged += MForm_VisibleChanged;
        }

        private void MForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!mForm.Visible)
            {
                mIsPoping = false;
                IDE.GlobalHandler.MouseLeftButtonDown -= GlobalHandler_MouseLeftButtonDown;
                IDE.GlobalHandler.MouseRightButtonDown -= GlobalHandler_MouseLeftButtonDown;
                IDE.Workbench.Deactivate -= Workbench_Deactivate;

                PopupBackColor = IDE.AppConfig.Skin.BackColor;
                Host.Invalidate();
            }
        }

        private void Popup()
        {
            mIsPoping = true;
            mForm.Selectd = Color;
            mForm.MaskLength = Size.Width;
            mForm.Show(IDE.Workbench);
            mForm.Location = Host.PointToScreen(new Point(Location.X, Location.Y + Size.Height - 1)); //, false);

            Host.Refresh();

            IDE.GlobalHandler.MouseLeftButtonDown += GlobalHandler_MouseLeftButtonDown;
            IDE.GlobalHandler.MouseRightButtonDown += GlobalHandler_MouseLeftButtonDown;
            IDE.Workbench.Deactivate += Workbench_Deactivate;
        }

        private void GlobalHandler_MouseLeftButtonDown(object sender, common.GlobalEventArgs<MouseEventArgs> e)
        {
            if (mForm != null && mIsPoping) 
            {
                if (!new Rectangle(mForm.Location, mForm.Size).Contains(e.EventArgs.Location) &&
                    !new Rectangle(Host.PointToScreen(this.Location), Size).Contains(e.EventArgs.Location)) 
                {
                    mForm.Hide();
                    mForm.Save();
                    Host.Invalidate();
                }
            }
        }

        private void MInputHelper_MouseDown(WinFormMouseInputEventArgs<object> args)
        {
            if (!mIsPoping && !mForm.Visible)
                Popup();
        }

        private void MInputHelper_MouseLeave(WinFormMouseInputEventArgs<object> args)
        {
            if (mIsPoping)
                return;

            mHoverButton = false;
            Host.Invalidate();
        }

        private void MInputHelper_MouseEnter(WinFormMouseInputEventArgs<object> args)
        {
            if (mIsPoping)
                return;

            mHoverButton = true;
            Host.Invalidate();
        }

        private void Workbench_Deactivate(object sender, EventArgs e)
        {
            mForm.Hide();
            mForm.Save();
            Host.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            mInputHelper.AddRegion(null, new Rectangle(Location, Size));

            if (mIsPoping) 
            {
                e.Graphics.FillRectangle(new SolidBrush(PopupBackColor), new Rectangle(Location, Size));
            }

            var rrr = mIsPoping ? 2 : 1;
            var foreBrush = new SolidBrush(ForeColor);
            var bounds = new Rectangle(Location.X + rrr, Location.Y + rrr, Size.Height - 2 * rrr, Size.Height - 2 * rrr);

            e.Graphics.FillColor(Color, bounds, bounds.Width / 2);

            if (ShowText)
            {
                var hex = Color.ToHex();

                e.Graphics.DrawString(hex,
                    IDE.AppConfig.Skin.DefaultFont,
                    foreBrush,
                    Location.X + Size.Height + 2,
                    Location.Y);
            }

            if (mIsPoping)
            {
                e.Graphics.DrawRectangle(new Pen(BorderColor), new Rectangle(Location, Size));
            }

            const int buttonWidth = 14;

            // 绘制三角形按钮
            var buttonHeight = Size.Height % 2 != 0 ? Size.Height + 1 : Size.Height;
            var buttonRectangle = new Rectangle(Location.X + Size.Width - buttonWidth - 2, Location.Y, buttonWidth, buttonHeight);

            // 得到绘制的按钮矩形
            var buttonTriangleWidth = buttonWidth / 2;
            var buttonTriangleHeight = buttonWidth / 4;
            var buttonTriangleRectangle = new Rectangle(
                buttonRectangle.X + (buttonWidth - buttonTriangleWidth) / 2,
                buttonRectangle.Y + (Size.Height - buttonTriangleHeight) / 2,
                buttonTriangleWidth,
                buttonTriangleHeight);

            var temp1 = e.Graphics.SmoothingMode;
            var temp2 = e.Graphics.InterpolationMode;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            // 绘制按钮三角形
            var buttonBrush = !mIsPoping ? (mHoverButton ? new SolidBrush(Color.White) : foreBrush) : IDE.AppConfig.Skin.TipsBrush;
            e.Graphics.FillTriangle(
                buttonBrush,
                buttonTriangleRectangle,
                180);

            e.Graphics.SmoothingMode = temp1;
            e.Graphics.InterpolationMode = temp2;
        }

        public event ColorDelegate ColorChanged;
    }
}
