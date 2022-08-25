using addin.common;
using addin.controls.colorpicker;
using addin.data;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class BColorPicker : BUIElement
    {
        interface ColorRecent 
        {
        }

        interface ColorCustom 
        {
        }

        enum ColorField
        {
            R,
            G,
            B,
            A
        }

        class TopMostWindow
        {
            struct WindowRect
            {
                public int Left;
                public int Top;
                public int Right;
                public int Bottom;
            }

            public const int HWND_TOP = 0;
            public const int HWND_BOTTOM = 1;
            public const int HWND_TOPMOST = -1;
            public const int HWND_NOTOPMOST = -2;

            [DllImport("user32.dll")]
            static extern IntPtr SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint wFlags);

            [DllImport("user32.dll")]
            static extern bool GetWindowRect(IntPtr hWnd, out WindowRect lpRect);

            /// <summary>
            　　/// 设置窗体为TopMost
            　　/// </summary>
            　　/// <param name="hWnd"></param>
            public static void SetTopomost(IntPtr hWnd)
            {
                WindowRect rect = new WindowRect();
                GetWindowRect(hWnd, out rect);
                SetWindowPos(hWnd, (IntPtr)HWND_TOPMOST, rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top, 0);
            }
        }

        class PickerScreen : FloatingWindow 
        {
            private Bitmap mBackImage;

            public Color Selected { get; private set; }

            public PickerScreen(Screen screen)
            {
                Location = screen.Bounds.Location;
                Size = screen.Bounds.Size;

                //mWidth = screen.Bounds.Size.Width;
                mBackImage = new Bitmap(Width, Height);
                Graphics g = Graphics.FromImage(mBackImage);
                g.CopyFromScreen(Location.X, Location.Y, 0, 0, Size);

                var bmp = new LockBitmap(mBackImage);
                bmp.LockBits();
                //mData = bmp.GetPixels();

                mBackImage = bmp.GetRect(0, 0, Width, Height).Bitmap;

                bmp.UnlockBits();
                //Clipboard.SetImage(mBackImage);

                MouseWheel += PickerScreen_MouseWheel;
                MouseDown += PickerScreen_MouseDown;
                MouseMove += PickerScreen_MouseMove;
                KeyDown += PickerScreen_KeyDown;
            }

            private void PickerScreen_MouseMove(object sender, MouseEventArgs e)
            {
                Selected = mBackImage.GetPixel(e.X, e.Y);
                Invalidate();
            }

            private void PickerScreen_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Escape) 
                {
                    Hide();
                }
            }

            private void PickerScreen_MouseDown(object sender, MouseEventArgs e)
            {
                Selected = mBackImage.GetPixel(e.X, e.Y);
                Hide();
            }

            private void PickerScreen_MouseWheel(object sender, MouseEventArgs e)
            {
                // 放大缩小

            }

            protected override void OnPaint(PaintEventArgs e)
            {
                //e.Graphics.DrawImage(mBackImage, 0, 0);

                // 计算信息框是否超出屏幕

            }
        }

        static BColorPicker() 
        {
            FileHistory<ColorRecent>.MaxCount = 14;
            FileHistory<ColorCustom>.MaxCount = 14;
        }

        const int BOX_WIDTH = 20;
        const int RIGHT_TOP_HEIGHT = 26;
        const int OFFSET_X = 6;
        const int OFFSET_Y = 6;
        const int RIGHT_WIDTH = 180;
        const int INV = 6;

        private HtmlToolTips mTips;
        private BButton mBtnCopy;
        private BButton mBtnPick;
        private BButton mBtnStar;
        private BButton mBtnStar_Outline;

        private BColorGrid mGridCommon;
        private BColorGrid mGridRecent;
        private BColorGrid mGridCustom;

        private BColorWheel mWheel;
        private BColorIntensity mIntensity;
        private BTab mTabs;

        private BRgbColorSlider mSliderR;
        private BRgbColorSlider mSliderG;
        private BRgbColorSlider mSliderB;

        private WBNumericBox mInputR;
        private WBNumericBox mInputG;
        private WBNumericBox mInputB;

        private Color mForeColor;
        private Color mSelected;

        private Color valueBeforePicking = default;
        private GlobalColorPickOperation pickingOperation = null;

        public bool ShowSelected
        {
            get;
            set;
        } = true;

        public Color Selected 
        {
            get { return mSelected; }
            set 
            {
                if (mSelected != value) 
                {
                    mSelected = value;
                    SelectedChanged?.Invoke(mSelected);
                }
            }
        }

        public Color ForeColor 
        {
            get { return mForeColor; }
            set 
            {
                if (mForeColor != value) 
                {
                    mForeColor = value;
                    mBtnCopy.SetIconColor(value);
                    mBtnPick.SetIconColor(value);
                    mBtnStar_Outline.SetIconColor(value);
                }
            }
        }

        public BColorPicker(IBControl host) : base(host)
        {
            mTips = new HtmlToolTips();
            mTips.ShowAlways = false;
            mTips.IsBalloon = true;

            mWheel = new BColorWheel(host);
            mWheel.ValueChanged += MWheel_ValueChanged;

            mIntensity = new BColorIntensity(host);
            mIntensity.ColorChanged += MIntensity_ColorChanged;

            mBtnCopy = new BButton(host);
            mBtnPick = new BButton(host);
            mBtnStar = new BButton(host);
            mBtnStar_Outline = new BButton(host);

            mBtnCopy.Click += MBtnCopy_Click;
            mBtnPick.Click += MBtnPick_Click;
            mBtnStar.Click += MBtnStar_Click;
            mBtnStar_Outline.Click += MBtnStar_Click;
            //mBtnCopy.SetIconColor(IDE.AppConfig.Skin.BackColor);
            //mBtnPick.SetIconColor(IDE.AppConfig.Skin.ForeColor);

            mBtnStar.Icon = Resources.GetManifestBitmap(typeof(AppConfig).Assembly, "resources.star_24px.png");
            mBtnStar_Outline.Icon = Resources.GetManifestBitmap(typeof(AppConfig).Assembly, "resources.star_outline.png");
            mBtnCopy.Icon = Resources.GetManifestBitmap(typeof(AppConfig).Assembly, "resources.copy_white_32px.png");
            mBtnPick.Icon = Resources.GetManifestBitmap(typeof(AppConfig).Assembly, "resources.picker_white_32px.png");

            mBtnCopy.AutoSize = false;
            mBtnPick.AutoSize = false;
            mBtnStar.AutoSize = false;
            mBtnStar_Outline.AutoSize = false;

            mBtnCopy.BorderColor = Color.Transparent;
            mBtnCopy.NormalColor = Color.Transparent;
            mBtnCopy.HoverColor = Color.Transparent;
            mBtnCopy.PressedColor = Color.Transparent;

            mBtnPick.BorderColor = Color.Transparent;
            mBtnPick.NormalColor = Color.Transparent;
            mBtnPick.HoverColor = Color.Transparent;
            mBtnPick.PressedColor = Color.Transparent;

            mBtnStar_Outline.BorderColor = Color.Transparent;
            mBtnStar_Outline.NormalColor = Color.Transparent;
            mBtnStar_Outline.HoverColor = Color.Transparent;
            mBtnStar_Outline.PressedColor = Color.Transparent;

            mBtnStar.BorderColor = Color.Transparent;
            mBtnStar.NormalColor = Color.Transparent;
            mBtnStar.HoverColor = Color.Transparent;
            mBtnStar.PressedColor = Color.Transparent;
            mBtnStar.IconNormalColor = Color.White;
            mBtnStar.IconHoverColor = Color.White;
            mBtnStar.IconPressedColor = Color.White;

            mGridCommon = new BColorGrid(host, IDE.AppConfig.Skin.OtherAccentColor.GetRange(0, FileHistory<ColorRecent>.MaxCount - 2).Union(new Color[] { Color.Black, Color.White }), 6);
            mGridRecent = new BColorGrid(host, FileHistory<ColorRecent>.Values.Select(x => x.ToColor()), FileHistory<ColorRecent>.MaxCount);
            mGridCustom = new BColorGrid(host, FileHistory<ColorCustom>.Values.Select(x => x.ToColor()), FileHistory<ColorCustom>.MaxCount);

            mGridCommon.ScrollBarVisual = false;
            mGridRecent.ScrollBarVisual = false;
            mGridCustom.ScrollBarVisual = false;

            mGridCommon.ColumnCount = 7;
            mGridRecent.ColumnCount = 7;
            mGridCustom.ColumnCount = 7;

            mGridCommon.Click += MGridCommon_Click;
            mGridRecent.Click += MGridRecent_Click;
            mGridCustom.Click += MGridCustom_Click;

            IDE.Workbench.Deactivate += Workbench_Deactivate;
            IDE.GlobalHandler.MouseLeftButtonDown += GlobalHandler_MouseLeftButtonDown;
            IDE.GlobalHandler.MouseLeftButtonUp += GlobalHandler_MouseLeftButtonUp;

            ForeColor = IDE.AppConfig.Skin.ForeColor;

            mTabs = new BTab(host);
            mTabs[Lang.Tran("常用")] = mGridCommon;
            mTabs[Lang.Tran("自定义")] = mGridCustom;
            mTabs[Lang.Tran("最近")] = mGridRecent;

            mTabs.SelectedTab = Lang.Tran("自定义");

            mSliderR = new BRgbColorSlider(host);
            mSliderG = new BRgbColorSlider(host);
            mSliderB = new BRgbColorSlider(host);

            mSliderR.Maximum = 255;
            mSliderG.Maximum = 255;
            mSliderB.Maximum = 255;

            mSliderR.Channel = RgbaChannel.Red;
            mSliderG.Channel = RgbaChannel.Green;
            mSliderB.Channel = RgbaChannel.Blue;

            mSliderR.NubColor = IDE.AppConfig.Skin.ForeColor;
            mSliderG.NubColor = IDE.AppConfig.Skin.ForeColor;
            mSliderB.NubColor = IDE.AppConfig.Skin.ForeColor;

            mSliderR.ValueChanged += MSliderR_ValueChanged;
            mSliderG.ValueChanged += MSliderG_ValueChanged;
            mSliderB.ValueChanged += MSliderB_ValueChanged;

            mInputR = new WBNumericBox();
            mInputG = new WBNumericBox();
            mInputB = new WBNumericBox();

            mInputR.Interval = 1;
            mInputG.Interval = 1;
            mInputB.Interval = 1;

            mInputR.Font = new Font(mInputG.Font.FontFamily, 9f, FontStyle.Regular, GraphicsUnit.Pixel);
            mInputG.Font = new Font(mInputG.Font.FontFamily, 10f, FontStyle.Regular, GraphicsUnit.Pixel);
            mInputB.Font = new Font(mInputG.Font.FontFamily, 11f, FontStyle.Regular, GraphicsUnit.Pixel);

            mInputR.Value = 0;
            mInputG.Value = 0;
            mInputB.Value = 0;

            mInputR.MinValue = 0;
            mInputG.MinValue = 0;
            mInputB.MinValue = 0;

            mInputR.MaxValue = 255;
            mInputG.MaxValue = 255;
            mInputB.MaxValue = 255;

            mInputR.ValueChanged += MInput_ValueChanged;
            mInputG.ValueChanged += MInput_ValueChanged;
            mInputB.ValueChanged += MInput_ValueChanged;

            if (host is Control ctrl) 
            {
                ctrl.Controls.Add(mInputR);
                ctrl.Controls.Add(mInputG);
                ctrl.Controls.Add(mInputB);
            }
        }

        private void MInput_ValueChanged(object sender, EventArgs e)
        {
            SetColor(Color.FromArgb(Selected.A, (int)mInputR.Value, (int)mInputR.Value, (int)mInputR.Value));
        }

        private void MIntensity_ColorChanged(object sender, EventArgs e)
        {
            SetColor(mIntensity.Color);
        }

        private void MWheel_ValueChanged(object sender, EventArgs e)
        {
            SetColor(mWheel.Value, false);
        }

        private void MSliderB_ValueChanged(object sender, EventArgs e)
        {
            SetColor(Color.FromArgb(Selected.A, Selected.R, Selected.G, (int)mSliderB.Value));
        }

        private void MSliderR_ValueChanged(object sender, EventArgs e)
        {
            SetColor(Color.FromArgb(Selected.A, (int)mSliderR.Value, Selected.G, Selected.B));
        }

        private void MSliderG_ValueChanged(object sender, EventArgs e)
        {
            SetColor(Color.FromArgb(Selected.A, Selected.R, (int)mSliderG.Value, Selected.B));
        }

        public void Save()
        {
            FileHistory<ColorRecent>.Values.Clear();
            FileHistory<ColorRecent>.Values.AddRange(mGridRecent.Colors.Select(x => x.ToHex()));

            FileHistory<ColorCustom>.Values.Clear();
            FileHistory<ColorCustom>.Values.AddRange(mGridRecent.Colors.Select(x => x.ToHex()));
        }

        private void MBtnStar_Click(object sender, MouseEventArgs e)
        {
            var hex = Selected.ToHex();
            var index = FileHistory<ColorCustom>.Values.IndexOf(hex);
            if (index != -1)
                FileHistory<ColorCustom>.Values.RemoveAt(index);
            else
                FileHistory<ColorCustom>.SetItem(hex);

            FileHistory<ColorCustom>.Save();

            mGridCustom.ChangeColors(FileHistory<ColorCustom>.Values.Select(x => x.ToColor()), FileHistory<ColorCustom>.MaxCount);
        }

        private void MGridCustom_Click(Color color)
        {
            SetColor(color);
            SetRecent(Selected);
        }

        private void MGridRecent_Click(Color color)
        {
            SetColor(color);
        }

        private void MGridCommon_Click(Color color)
        {
            SetColor(color);
            SetRecent(Selected);
        }

        private void SetColor(Color color, bool changWheel = true) 
        {
            mIntensity.ColorChanged -= MIntensity_ColorChanged;
            mSliderR.ValueChanged -= MSliderR_ValueChanged;
            mSliderG.ValueChanged -= MSliderG_ValueChanged;
            mSliderB.ValueChanged -= MSliderB_ValueChanged;
            mInputR.ValueChanged -= MInput_ValueChanged;
            mInputG.ValueChanged -= MInput_ValueChanged;
            mInputB.ValueChanged -= MInput_ValueChanged;

            Selected = color;
            mIntensity.Color = color;

            mSliderR.Value = color.R;
            mSliderR.Color = color;
            mSliderG.Value = color.G;
            mSliderG.Color = color;
            mSliderB.Value = color.B;
            mSliderB.Color = color;

            mInputR.Value = color.R;
            mInputG.Value = color.G;
            mInputB.Value = color.B;

            mIntensity.ColorChanged += MIntensity_ColorChanged;
            mSliderR.ValueChanged += MSliderR_ValueChanged;
            mSliderG.ValueChanged += MSliderG_ValueChanged;
            mSliderB.ValueChanged += MSliderB_ValueChanged;
            mInputR.ValueChanged += MInput_ValueChanged;
            mInputG.ValueChanged += MInput_ValueChanged;
            mInputB.ValueChanged += MInput_ValueChanged;

            if (changWheel)
            {
                mWheel.ValueChanged -= MWheel_ValueChanged;
                mWheel.Value = color;
                mWheel.ValueChanged += MWheel_ValueChanged;
            }
        }

        private void Workbench_Deactivate(object sender, EventArgs e)
        {
            mTips.Hide();
            ClearGlobalEvents();
        }

        private void GlobalHandler_MouseLeftButtonUp(object sender, GlobalEventArgs<MouseEventArgs> e)
        {
            mTips.Hide();
            ClearGlobalEvents();
        }

        private void GlobalHandler_MouseLeftButtonDown(object sender, GlobalEventArgs<MouseEventArgs> e)
        {
            mTips.Hide();
            ClearGlobalEvents();
        }

        private void MBtnPick_Click(object sender, MouseEventArgs e)
        {
            valueBeforePicking = Selected;

            if (this.pickingOperation == null)
                this.pickingOperation = new GlobalColorPickOperation();

            this.pickingOperation.PickedColorChanged += this.pickingOperation_PickedColorChanged;
            this.pickingOperation.OperationEnded += this.pickingOperation_OperationEnded;
            this.pickingOperation.Start();
        }

        private void pickingOperation_PickedColorChanged(object sender, EventArgs e)
        {
            if (pickingOperation.PickedColor != Selected)
            {
                SetColor(pickingOperation.PickedColor);
                SetRecent(pickingOperation.PickedColor);
            }
        }
        private void pickingOperation_OperationEnded(object sender, EventArgs e)
        {
            if (this.pickingOperation.IsCanceled)
                SetColor(valueBeforePicking);

            this.pickingOperation.PickedColorChanged -= this.pickingOperation_PickedColorChanged;
            this.pickingOperation.OperationEnded -= this.pickingOperation_OperationEnded;
        }

        private void MBtnCopy_Click(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(Selected.ToHex());

            if (!mTips.IsVisual)
            {
                mTips.ShowTips(
                    Lang.Tran("已复制颜色Hex代码到剪切板"),
                    Host,
                    new Point(
                        mBtnCopy.Location.X + mBtnCopy.Size.Width - 18,
                        mBtnCopy.Location.Y - 2));
            }
        }

        private void SetRecent(Color color)
        {
            var hex = color.ToHex();
            var index = FileHistory<ColorRecent>.Values.IndexOf(hex);

            if (index != -1)
            {
                FileHistory<ColorRecent>.Values.RemoveAt(index);
            }

            FileHistory<ColorRecent>.SetItem(hex);
            FileHistory<ColorRecent>.Save();

            mGridRecent.ChangeColors(FileHistory<ColorRecent>.Values.Select(x => x.ToColor()), FileHistory<ColorRecent>.MaxCount);
        }

        private void ClearGlobalEvents()
        {
            IDE.Workbench.Deactivate -= Workbench_Deactivate;
            IDE.GlobalHandler.MouseLeftButtonDown -= GlobalHandler_MouseLeftButtonDown;
            IDE.GlobalHandler.MouseLeftButtonUp -= GlobalHandler_MouseLeftButtonUp;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var foreBrush = new SolidBrush(ForeColor);

            var leftOffset = Location.X + INV;
            var leftY = INV;
            var leftWidth = Size.Width - RIGHT_WIDTH - INV - OFFSET_X;
            var rightOffset = Location.X + OFFSET_X + leftWidth + INV;

            var hex = Selected.ToHex();
            var hexSize = e.Graphics.MeasureString(hex, IDE.AppConfig.Skin.DefaultFont);
            var bounds = new Rectangle(rightOffset, OFFSET_Y, (int)hexSize.Height, (int)hexSize.Height);

            mWheel.Location = new Point(leftOffset, OFFSET_Y + bounds.Height + 6);
            mWheel.Size = new Size(leftWidth, leftWidth);
            mWheel.Paint(e);

            mTabs.Location = new Point(leftOffset, mWheel.Location.Y + mWheel.Size.Height + 14);
            mTabs.Size = new Size(leftWidth, 200);
            mTabs.Paint(e);

            mIntensity.Size = new Size(Size.Width - leftOffset - leftWidth - 12, 66);
            mIntensity.Location = new Point(leftOffset + leftWidth + 6, mTabs.Location.Y);
            mIntensity.Paint(e);

            mInputR.Size = new Size(48, 18);
            mInputG.Size = new Size(48, 18);
            mInputB.Size = new Size(48, 18);

            mSliderR.Size = new Size(mIntensity.Size.Width - mInputR.Size.Width - 2 - 8, 24);
            mSliderG.Size = new Size(mIntensity.Size.Width - mInputG.Size.Width - 2 - 8, 24);
            mSliderB.Size = new Size(mIntensity.Size.Width - mInputB.Size.Width - 2 - 8, 24);

            var strInv = Math.Max(
                StringHelper.GetLength(Host.Font, "R"),
                Math.Max(StringHelper.GetLength(Host.Font, "G"),
                StringHelper.GetLength(Host.Font, "B")));

            leftY = leftY + OFFSET_Y + bounds.Height + 4;
            e.Graphics.DrawString("R", Host.Font, IDE.AppConfig.Skin.ForeBrush, mIntensity.Location.X, leftY - 1);
            mSliderR.Location = new Point(mIntensity.Location.X + strInv + 2, leftY);
            mInputR.Location = new Point(mSliderR.Location.X + mSliderR.Size.Width + 2, leftY - 1);
            leftY = leftY + mSliderR.Size.Height + 3;
            e.Graphics.DrawString("G", Host.Font, IDE.AppConfig.Skin.ForeBrush, mIntensity.Location.X - 1, leftY - 1);
            mSliderG.Location = new Point(mIntensity.Location.X + strInv + 2, leftY);
            mInputG.Location = new Point(mSliderG.Location.X + mSliderG.Size.Width + 2, leftY - 1);
            leftY = leftY + mSliderG.Size.Height + 3;
            e.Graphics.DrawString("B", Host.Font, IDE.AppConfig.Skin.ForeBrush, mIntensity.Location.X, leftY - 1);
            mSliderB.Location = new Point(mIntensity.Location.X + strInv + 2, leftY);
            mInputB.Location = new Point(mSliderB.Location.X + mSliderB.Size.Width + 2, leftY - 1);

            mSliderR.Paint(e);
            mSliderG.Paint(e);
            mSliderB.Paint(e);

            if (ShowSelected)
            {
                e.Graphics.FillColor(
                    Selected,
                    bounds, 
                    bounds.Width / 2);

                e.Graphics.DrawString(hex,
                    IDE.AppConfig.Skin.DefaultFont,
                    foreBrush,
                    rightOffset + hexSize.Height + 2,
                    OFFSET_Y);
            }

            var buttonOffset = Location.X + (Size.Width - (bounds.Size.Width + 2) * 3 - 3); // rightOffset + (int)Math.Ceiling(hexSize.Height + 2 + 4 + hexSize.Width);
            var starButton =  mGridCustom.Colors.Contains(Selected) ? mBtnStar : mBtnStar_Outline;
            starButton.Location = new Point(buttonOffset, OFFSET_Y);
            starButton.Size = bounds.Size;
            starButton.IconSize = bounds.Size;

            mBtnCopy.Location = new Point(starButton.Location.X + starButton.Size.Width + 3, OFFSET_Y);
            mBtnCopy.Size = bounds.Size;
            mBtnCopy.IconSize = bounds.Size;

            mBtnPick.Location = new Point(mBtnCopy.Location.X + mBtnCopy.Size.Width + 2, OFFSET_Y);
            mBtnPick.Size = bounds.Size;
            mBtnPick.IconSize = bounds.Size;

            starButton.Paint(e);
            mBtnCopy.Paint(e);
            mBtnPick.Paint(e);
        }

        public event ColorDelegate SelectedChanged;
    }
}
