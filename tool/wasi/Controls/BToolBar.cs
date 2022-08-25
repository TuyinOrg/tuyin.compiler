using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class BToolBar : BUIElement
    {
        [Flags]
        enum LineFixed 
        {
            None = 0,
            LeftTop = 1,
            RightTop = 2,
            RightBottom = 4,
            LeftBottom = 8,
            Left = LeftTop | LeftBottom,
            Right = RightTop | RightBottom,
            Top = LeftTop | RightTop,
            Bottom = RightBottom | LeftBottom,
            All = LeftBottom | RightTop | RightBottom | LeftBottom
        }

        private List<BButton> mVerticalButtons;
        private List<BButton> mHorizontalButtons;
        private Color mIconColor;

        public IEnumerable<BButton> Buttons 
        {
            get 
            {
                foreach (var b in mVerticalButtons)
                    yield return b;

                foreach (var b in mHorizontalButtons)
                    yield return b;
            }
        }

        public Color BackColor 
        {
            get;
            set;
        }

        public Color IconColor 
        {
            get { return mIconColor; }
            set 
            {
                if (mIconColor != value) 
                {
                    mIconColor = value;
                    for (var i = 0; i < mVerticalButtons.Count; i++)
                        mVerticalButtons[i].SetIconColor(mIconColor);
                }
            }
        }

        public int Radius
        {
            get;
            set;
        } = 8;

        public int OutlineWidth
        {
            get;
            set;
        } = 4;

        public int BarWidth
        {
            get;
            set;
        } = 32;

        public int HorizontalIconOffsetY
        {
            get;
            set;
        } = 0;

        public int HorizontalIconOffetX
        {
            get;
            set;
        } = 6;

        public BToolBar(IBControl host) 
            : base(host)
        {
            mVerticalButtons = new List<BButton>();
            mHorizontalButtons = new List<BButton>();

            BackColor = IDE.AppConfig.Skin.AccentColor;
            IconColor = Color.White;
        }

        public BButton SetButton(int index, Image icon, Action<BButton> callback, string descrption, bool isHorizontal, object tag = null, bool useWhiteColor = false) 
        {
            var button = new BButton(Host);
            button.ToolTipText = descrption;
            button.ToolTipOffset = new Point(-2, -1);
            button.Icon = icon;
            button.Radius = 0;
            button.Tag = tag;
            button.BorderStyle = BorderStyle.None;
            button.BorderColor = Color.Transparent;
            button.NormalColor = Color.Transparent;
            button.HoverColor = Color.Transparent;
            button.PressedColor = Color.Transparent;
            button.DisenableColor = Color.Transparent;

            var buttonColor = useWhiteColor ? Color.White : IconColor;

            button.IconNormalColor = buttonColor;
            button.IconHoverColor = buttonColor;
            button.IconPressedColor = buttonColor;
            button.IconDisenableColor = Color.Gray;

            button.IconDisenableColor = IDE.AppConfig.Skin.TipsColor;
            button.Click += (s, e) => callback(s as BButton);
            button.Size = new Size(icon.Width, icon.Height);
            button.AutoSize = false;

            if (isHorizontal)
                mHorizontalButtons.Insert(index, button);
            else
                mVerticalButtons.Insert(index, button);

            return button;
        }

        public BButton AddButton(Image icon, Action<BButton> callback, string descrption, bool isHorizontal, object tag = null, bool useWhiteColor = false)
        {
            return SetButton(isHorizontal ? mHorizontalButtons.Count : mVerticalButtons.Count, icon, callback, descrption, isHorizontal, tag, useWhiteColor);
        }

        public void RemoveButton(BButton button) 
        {
            mVerticalButtons.Remove(button);
            mHorizontalButtons.Remove(button);
            Host.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) 
        {
            PaintGroup(e, false);
            PaintGroup(e, true);
        }

        private void PaintGroup(PaintEventArgs e, bool isHorizontal) 
        {
            if (isHorizontal)
            {
                if (mHorizontalButtons.Count == 0)
                {
                    return;
                }
            }
            else
            {
                if (mVerticalButtons.Count == 0)
                {
                    return;
                }
            }



            var left = isHorizontal && mVerticalButtons.Count > 0;
            var fix = left ? LineFixed.Left : (!isHorizontal && Location.Y == 0 ? LineFixed.RightTop : LineFixed.None);
            if (isHorizontal && Location.Y == 0)
                fix = fix | LineFixed.RightTop;

            var offsetX = left ? BarWidth - OutlineWidth : 0;
            var length = left ? (mHorizontalButtons.Count + 2) * HorizontalIconOffetX + mHorizontalButtons.Sum(x => x.Size.Width) : Size.Height - 1;
            var bounds = new Rectangle(Location.X + offsetX, Location.Y, isHorizontal ? length : BarWidth, isHorizontal ? BarWidth : length);
            var path = CreateRoundedRectanglePath(bounds, fix);

            FillRoundRectangle(e.Graphics, new SolidBrush(Host.BackColor), path);

            if (isHorizontal)
            {
                var scaleX = (float)(bounds.Width -  OutlineWidth * (mVerticalButtons.Count > 0 ? 1 : 2) + 1) / bounds.Width;
                var scaleY = (float)(bounds.Height - OutlineWidth * (Location.Y == 0 ? 1 : 2) + 1) / bounds.Height;
                var matrix = new Matrix();
                matrix.Scale(scaleX, scaleY);
                matrix.Translate(mVerticalButtons.Count > 0 ? 0 : OutlineWidth, Location.Y == 0 ? 0 : OutlineWidth);
                path.Transform(matrix);
            }
            else
            {
                var scaleX = (float)(bounds.Width - OutlineWidth * (Location.X == 0 ? 1 : 2)) / bounds.Width;
                var scaleY = (float)(bounds.Height - OutlineWidth * (Location.Y == 0 ? 1 : 2) + 1) / bounds.Height;
                var matrix = new Matrix();
                matrix.Scale(scaleX, scaleY);
                matrix.Translate(Location.X == 0 ? 0 : OutlineWidth, Location.Y == 0 ? 0 : OutlineWidth);
                path.Transform(matrix);
            }

            FillRoundRectangle(e.Graphics, new SolidBrush(BackColor), path);

            if (isHorizontal)
            {
                var width = bounds.Width; // - OutlineWidth * 2;
                var height = bounds.Height - OutlineWidth * (Location.Y == 0 ? 1 : 2);

                var size = 0;
                for (var i = 0; i < mHorizontalButtons.Count; i++)
                    size = size + mHorizontalButtons[i].Size.Width;

                //var single = HorizontalIconOffetX; ////(width - size) / (1 + mHorizontalButtons.Count);
                var offset = HorizontalIconOffetX + (mVerticalButtons.Count > 0 ? 0 : OutlineWidth);
                for (var i = 0; i < mHorizontalButtons.Count; i++)
                {
                    var button = mHorizontalButtons[i];
                    button.Location = new Point(bounds.X + offset, bounds.Y + (height - button.Size.Height) / 2 + HorizontalIconOffsetY);
                    button.Paint(e);
                    offset = offset + button.Size.Width + HorizontalIconOffetX;
                }
            }
            else
            {
                var width = bounds.Width - OutlineWidth * (Location.X == 0 ? 1 : 2);
                var height = bounds.Height - OutlineWidth * 2;

                var size = 0;
                for (var i = 0; i < mVerticalButtons.Count; i++)
                    size = size + mVerticalButtons[i].Size.Height;

                var single = (height - size) / (1 + mVerticalButtons.Count);
                var offset = single + OutlineWidth;
                for (var i = 0; i < mVerticalButtons.Count; i++)
                {
                    var button = mVerticalButtons[i];
                    button.Location = new Point(bounds.X + (width - button.Size.Width) / 2, bounds.Y + offset);
                    button.Paint(e);
                    offset = offset + button.Size.Height + single;
                }
            }
        }

        private void FillRoundRectangle(Graphics g, Brush brush, GraphicsPath path)
        {
            if (brush == null)
                throw new NotSupportedException();

            var temp = g.SmoothingMode;
            var temp2 = g.CompositingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingMode = CompositingMode.SourceOver;

            g.FillPath(brush, path);

            g.SmoothingMode = temp;
            g.CompositingMode = temp2;
        }

        private GraphicsPath CreateRoundedRectanglePath(RectangleF rect, LineFixed fix)
        {
            if (Radius == 0)
            {
                GraphicsPath roundedRect = new GraphicsPath();
                roundedRect.AddRectangle(rect);
                return roundedRect;
            }
            else
            {

                GraphicsPath roundedRect = new GraphicsPath();

                if (rect.X == 0 || fix.HasFlag(LineFixed.LeftTop))
                    roundedRect.AddLine(rect.X, rect.Y, Radius, rect.Y);
                else
                    roundedRect.AddArc(rect.X, rect.Y, Radius * 2, Radius * 2, 180, 90);

                roundedRect.AddLine(rect.X + Radius, rect.Y, rect.Right - Radius * 2, rect.Y);

                if (rect.Right >= Host.Width || fix.HasFlag(LineFixed.RightTop))
                    roundedRect.AddLine(rect.X + rect.Width - Radius * 2, rect.Y, rect.Right, rect.Y);
                else
                    roundedRect.AddArc(rect.X + rect.Width - Radius * 2, rect.Y, Radius * 2, Radius * 2, 270, 90);

                roundedRect.AddLine(rect.Right, rect.Y + Radius * 2, rect.Right, rect.Y + rect.Height - Radius * 2);

                if (rect.Bottom >= Host.Height || fix.HasFlag(LineFixed.RightBottom))
                    roundedRect.AddLine(rect.Right, rect.Y + rect.Height - Radius * 2, rect.Right, rect.Height);
                else
                    roundedRect.AddArc(rect.X + rect.Width - Radius * 2, rect.Y + rect.Height - Radius * 2, Radius * 2, Radius * 2, 0, 90);

                roundedRect.AddLine(rect.Right - Radius * 2, rect.Bottom, rect.X + Radius * 2, rect.Bottom);

                if (rect.X == 0 || fix.HasFlag(LineFixed.LeftBottom))
                    roundedRect.AddLine(rect.X + Radius * 2, rect.Bottom, rect.X, rect.Bottom);
                else
                    roundedRect.AddArc(rect.X, rect.Bottom - Radius * 2, Radius * 2, Radius * 2, 90, 90);

                roundedRect.AddLine(rect.X, rect.Bottom - Radius * 2, rect.X, rect.Y + Radius * 2);

                roundedRect.CloseFigure();
                return roundedRect;
            }
        }
    }
}
