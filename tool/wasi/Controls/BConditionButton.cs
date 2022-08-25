using addin.common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    class BConditionButton<T> : BUIElement
    {
        private static Color sContextMenuItemSelectedBackColor = Color.FromArgb(255, 248, 249, 250);
        private static Color sContextMenuItemForeColor = Color.FromArgb(255, 30, 30, 30);

        private HashSet<T> mSelecteds;
        private WBContextMenu mPopupMenu;
        private Dictionary<T, WBMenuItem> mItemDict;
        private WinFormInputHelper<int> mInputHelper;

        private bool mHoverButton;
        private bool mPressedButton;

        private string mText;

        private Color mBorderColor;
        private Color mBackColor;
        private Color mForeColor;
        private Color mDisenableBackColor;
        private Color mDisenableForeColor;
        private Color mButtonHoverColor;
        private Color mButtonPressedColor;
        private Color mButtonNormalColor;
        private Color mButtonDisenbaleColor;

        private Pen mBorderPen;
        private Brush mButtonHoverBrush;
        private Brush mButtonPressedBrush;
        private Brush mButtonNormalBrush;
        private Brush mButtonDisenableBrush;

        public T Selected
        {
            get
            {
                return mSelecteds.FirstOrDefault();
            }
            set
            {
                mSelecteds.Clear();
                mSelecteds.Add(value);
                Host.Invalidate();
            }
        }

        public IEnumerable<T> Selecteds
        {
            get { return mSelecteds; }
            set
            {
                mSelecteds = new HashSet<T>(value);
                Host.Invalidate();
            }
        }

        public bool ShowButton
        {
            get;
            set;
        }

        public bool Multiple
        {
            get { return mPopupMenu.ShowCheckMargin; }
            set
            {
                if (mPopupMenu.ShowCheckMargin != value)
                {
                    mPopupMenu.ShowCheckMargin = value;
                    if (mPopupMenu.ShowCheckMargin)
                    {
                        foreach (WBMenuItem item in mItemDict.Values)
                        {
                            item.CheckOnClick = true;
                            item.CheckedChanged += MenuItem_CheckedChanged;
                        }
                    }
                    else
                    {
                        foreach (WBMenuItem item in mItemDict.Values)
                        {
                            item.CheckOnClick = false;
                            item.CheckedChanged -= MenuItem_CheckedChanged;
                        }
                    }
                }
            }
        }

        public Padding BorderPadding
        {
            get;
            set;
        }

        public Color BorderColor
        {
            get { return mBorderColor; }
            set
            {
                if (mBorderColor != value)
                {
                    mBorderColor = value;
                    mPopupMenu.BorderColor = value;
                    mBorderPen = new Pen(new SolidBrush(mBorderColor));
                    Host.Invalidate();
                }
            }
        }

        public Color BackColor
        {
            get { return Enabled ? mBackColor : mDisenableBackColor; }
            set
            {
                if (mBackColor != value)
                {
                    mBackColor = value;
                    mPopupMenu.BackColor = mBackColor;

                    foreach (WBMenuItem item in mPopupMenu.Items)
                        item.BackColor = value;
                }
            }
        }

        public Color ForeColor
        {
            get { return Enabled ? mForeColor : mDisenableForeColor; }
            set
            {
                if (mForeColor != value)
                {
                    mForeColor = value;
                    mPopupMenu.ForeColor = mForeColor;

                    foreach (WBMenuItem item in mPopupMenu.Items)
                        item.ForeColor = value;
                }
            }
        }

        public Color DisenableBackColor
        {
            get { return mDisenableBackColor; }
            set
            {
                if (mDisenableBackColor != value)
                {
                    mDisenableBackColor = value;
                }
            }
        }

        public Color DisenableForeColor
        {
            get { return mDisenableForeColor; }
            set
            {
                if (mDisenableForeColor != value)
                {
                    mDisenableForeColor = value;
                }
            }
        }

        public Color PressedBackColor
        {
            get;
            set;
        }

        public Color PressedForeColor
        {
            get;
            set;
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
                    Host.Invalidate();
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
                    Host.Invalidate();
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
                    Host.Invalidate();
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
                    Host.Invalidate();
                }
            }
        }

        public bool AutoSize
        {
            get { return mPopupMenu.AutoSize; }
            set { mPopupMenu.AutoSize = value; }
        }

        public bool DropDownFirst
        {
            get;
            set;
        }

        public bool CheckStringLength
        {
            get;
            set;
        }

        public string Text
        {
            get
            {
                return mText;
            }
            set
            {
                mText = value;
                Host.Invalidate();
            }
        }

        public BConditionButton(BControl host)
            : base(host)
        {
            DropDownFirst = true;
            ShowButton = true;
            CheckStringLength = true;

            mSelecteds = new HashSet<T>();

            mBackColor = Color.FromArgb(255, 51, 51, 55);
            mForeColor = Color.FromArgb(255, 220, 220, 220);
            mDisenableBackColor = Color.FromArgb(255, 232, 232, 236);
            mDisenableForeColor = Color.Gray;
            var color = IDE.AppConfig.Skin.AccentColor;

            mPopupMenu = new WBContextMenu();
            mPopupMenu.BorderColor = Color.Transparent;
            mPopupMenu.BackColor = mBackColor;
            mPopupMenu.ItemAdded += mPopupMenu_ItemAdded;
            mPopupMenu.ItemRemoved += mPopupMenu_ItemRemoved;
            mPopupMenu.ItemClicked += mPopupMenu_ItemClicked;
            mPopupMenu.Closed += MPopupMenu_Closed;

            mItemDict = new Dictionary<T, WBMenuItem>();
            mInputHelper = new WinFormInputHelper<int>(Host);
            mInputHelper.MouseEnter += mInputHelper_MouseEnter;
            mInputHelper.MouseLeave += mInputHelper_MouseLeave;
            mInputHelper.MouseDown += mInputHelper_MouseDown;
            mInputHelper.MouseUp += mInputHelper_MouseUp;

            BorderColor = Color.FromArgb(255, 67, 67, 70);
            ButtonDisenableColor = Color.LightGray;
            ButtonNormalColor = Color.FromArgb(255, 220, 220, 220);
            ButtonHoverColor = Color.FromArgb((byte)((float)200 / 255 * color.A), color.R, color.G, color.B);
            ButtonPressedColor = color;

            PressedBackColor = Color.FromArgb(27, 27, 28);
            PressedForeColor = mForeColor;

            //Font = new Font("Consolas", 9f);
            BorderPadding = new Padding(0);

            Host.MouseUp += WBComboBox_MouseUp;
        }

        void WBComboBox_MouseUp(object sender, MouseEventArgs e)
        {
            mPressedButton = false;

            Host.Invalidate();
        }

        void mInputHelper_MouseUp(WinFormMouseInputEventArgs<int> args)
        {
            mPressedButton = false;

            Host.Invalidate();
        }

        void mInputHelper_MouseDown(WinFormMouseInputEventArgs<int> args)
        {
            mPressedButton = true;
            Host.Invalidate();

            if (args.Area.Token == 1)
            {
                if (!mPopupMenu.IsVisual)
                {
                    DropItems();
                }
                else
                {
                    mPopupMenu.Close();
                }
            }
            else
            {
                mPopupMenu.Close();
                OnClick?.Invoke(this, EventArgs.Empty);
            }
        }

        void mInputHelper_MouseLeave(WinFormMouseInputEventArgs<int> args)
        {
            mHoverButton = false;
            Host.Invalidate();
        }

        void mInputHelper_MouseEnter(WinFormMouseInputEventArgs<int> args)
        {
            mHoverButton = true;
            Host.Invalidate();
        }

        void mPopupMenu_ItemAdded(WBMenuItem e)
        {
            if (ItemAdded != null)
                ItemAdded(e);
        }

        void mPopupMenu_ItemRemoved(WBMenuItem e)
        {
            if (ItemRemoved != null)
                ItemRemoved(e);
        }

        void mPopupMenu_ItemClicked(WBMenuItem wbitem)
        {
            var item = mItemDict.FirstOrDefault(X => X.Value == wbitem).Key;
            var args = new WBComboBoxItemSelectingEventArgs<T>() { Item = item };
            if (ItemSelecting != null)
                ItemSelecting(null, args);

            if (!args.Canecl)
            {
                if (Multiple)
                {
                    wbitem.Checked = !wbitem.Checked;
                    if (wbitem.Checked)
                    {
                        mSelecteds.Add(item);
                    }
                    else
                    {
                        mSelecteds.Remove(item);
                    }
                }
                else
                {
                    mSelecteds.Clear();
                    mSelecteds.Add(item);
                }

                Host.Invalidate();

                if (ItemSelected != null)
                    ItemSelected(null, EventArgs.Empty);
            }
        }

        void MPopupMenu_Closed(object sender, EventArgs e)
        {
            mPressedButton = false;
            Host.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            mInputHelper.ClearRegions();

            base.OnPaint(e);

            //e.Graphics.Clear(mPopupMenu.Visible ? PressedBackColor : (Enabled ? BackColor : DisenableBackColor));

            e.Graphics.DrawRectangle(
                mBorderPen,
                Location.X + BorderPadding.Left,
                Location.Y + BorderPadding.Top + (!DropDownFirst && mPopupMenu.IsVisual ? -1 : 0),
                Size.Width - 1 + BorderPadding.Right - BorderPadding.Left,
                Size.Height - 1 + BorderPadding.Bottom - BorderPadding.Top + (DropDownFirst && mPopupMenu.IsVisual ? 1 : 0));

            int buttonWidth = ShowButton ? 14 : 0;

            // 绘制icon
            var icon = Selected == null ? null : Selected.GetAtrributeIcon();
            var iconWidth = icon == null ? 2 : icon.Width + 4;
            if (icon != null)
                e.Graphics.DrawImageUnscaled(icon, new Point(2, (Size.Height - icon.Height) / 2));

            // 绘制文字
            var maxTextWidth = Size.Width - buttonWidth - 4 - iconWidth;
            if (maxTextWidth > 0)
            {
                using (Brush textBrush = new SolidBrush(mPopupMenu.IsVisual ? PressedForeColor : (Enabled ? ForeColor : DisenableForeColor)))
                {
                    var text = Text;
                    if (CheckStringLength)
                        text = StringHelper.CheckLength(Host.Font, text, maxTextWidth, "..");

                    var textSize = e.Graphics.MeasureString(text, Host.Font);
                    e.Graphics.DrawString(
                        text,
                        Host.Font,
                        textBrush,
                        new PointF(Location.X + 2, Location.Y + iconWidth + (Size.Height - textSize.Height) / 2));
                }
            }

            // 添加按钮区域
            mInputHelper.AddRegion(1, Location.X, Location.Y, Size.Width - buttonWidth - (ShowButton ? 2 : 0), Size.Height);

            if (ShowButton)
            {
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
                var buttonBrush = !Enabled ? mButtonDisenableBrush : (mPressedButton ? mButtonPressedBrush : (mHoverButton ? mButtonHoverBrush : mButtonNormalBrush));
                e.Graphics.FillTriangle(
                    buttonBrush,
                    buttonTriangleRectangle,
                    180);

                e.Graphics.SmoothingMode = temp1;
                e.Graphics.InterpolationMode = temp2;

                // 添加按钮区域
                mInputHelper.AddRegion(2, buttonRectangle);
            }
        }

        public void DropItems()
        {
            if (OnDropDown != null)
                OnDropDown(null, EventArgs.Empty);

            mPopupMenu.Width = Math.Max(65, Size.Width);
            mPopupMenu.Height = (int)(Math.Ceiling(mPopupMenu.Items.Count * 0.5 + mPopupMenu.Items.Cast<object>().Where(X => X is WBMenuItem).Cast<WBMenuItem>().Sum(X => X.Height)));

            for (var i = 0; i < mPopupMenu.Items.Count; i++)
                mPopupMenu.Items[i].Width = mPopupMenu.Width;

            var showPoint = DropDownFirst ? new Point(BorderPadding.Left, Size.Height - 1 + BorderPadding.Top) : new Point(BorderPadding.Left, -mPopupMenu.Height + 1 + BorderPadding.Top);
            showPoint = new Point(Location.X + showPoint.X, Location.Y + showPoint.Y);
            var actualPoint = Host.PointToScreen(showPoint);

            mPopupMenu.BackColor = PressedBackColor;
            foreach (var item in mItemDict)
            {
                item.Value.BackColor = PressedBackColor;
                var wbitem = item.Value as WBMenuItem;
                wbitem.Checked = Multiple && mSelecteds.Contains(item.Key);
            }

            mPopupMenu.FirstMask = DropDownFirst;
            mPopupMenu.MaskBorderWidth = Size.Width;
            mPopupMenu.Show(actualPoint);

            if (OnDropDownEnd != null)
                OnDropDownEnd(null, EventArgs.Empty);
        }

        public void ClearItems()
        {
            mItemDict.Clear();
            mPopupMenu.Items.Clear();
        }

        public void AddItem(T item)
        {
            if (!mItemDict.ContainsKey(item))
            {
                var text = item.GetDescription();
                if (text != "-")
                {
                    var menuItem = new WBMenuItem(text);
                    var icon = item.GetAtrributeIcon();
                    //menuItem.Font = Host.Font;
                    menuItem.ShowIcon = icon != null;
                    menuItem.Image = icon;
                    menuItem.ForeColor = ForeColor;
                    menuItem.SelectedForeColor = ForeColor;
                    //menuItem.AutoSize = AutoSize;
                    menuItem.Height = 14;
                    if (Multiple)
                    {
                        menuItem.CheckOnClick = true;
                        menuItem.CheckedChanged += MenuItem_CheckedChanged;
                    }

                    mPopupMenu.Items.Add(menuItem);
                    mItemDict[item] = menuItem;

                    if (mPopupMenu.Items.Count == 1)
                    {
                        mSelecteds.Add(item);
                        //UpdateText();
                        //ForceInvalidate();
                    }
                }
                else
                {
                    mPopupMenu.Items.Add(new WBMenuItemSeparator());
                }

                Host.Invalidate();
            }
        }

        public void AddItems(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        private void MenuItem_CheckedChanged(object sender, EventArgs e)
        {
            return;

            if (!Multiple)
                throw new NotImplementedException();

            var item = sender as WBMenuItem;
            var value = mItemDict.FirstOrDefault(X => X.Value == item).Key;
            if (value == null)
                throw new NotImplementedException();

            var args = new WBComboBoxItemSelectingEventArgs<T>() { Item = value };
            if (ItemSelecting != null)
                ItemSelecting(null, args);

            if (!args.Canecl)
            {
                var target = item.Checked;
                //item.Checked = target;
                if (target)
                {
                    mSelecteds.Add(value);
                }
                else
                {
                    mSelecteds.Remove(value);
                }

                Host.Invalidate();

                if (ItemSelected != null)
                    ItemSelected(null, EventArgs.Empty);
            }
        }

        public void RemoveItem(T item)
        {
            if (item == null)
                throw new NotImplementedException();

            if (mItemDict.ContainsKey(item))
            {
                mPopupMenu.Items.Remove(mItemDict[item]);
                Host.Invalidate();
            }
        }

        public WBMenuItem GetItem(T item)
        {
            return mItemDict[item] as WBMenuItem;
        }

        public event EventHandler OnDropDown;
        public event EventHandler OnDropDownEnd;
        public event EventHandler<WBComboBoxItemSelectingEventArgs<T>> ItemSelecting;
        public event EventHandler ItemSelected;
        public event WBContextMenuItemDelegate ItemAdded;
        public event WBContextMenuItemDelegate ItemRemoved;
        public event EventHandler OnClick;
    }
}
