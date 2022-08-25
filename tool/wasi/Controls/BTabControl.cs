using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class BTabControl : BControl
    {
        public const int TAB_MIN_WIDTH_PADDING = 8;
        public const int TAB_MIN_HEIGHT_PADDING = 20;

        private string mSelectedTab;
        private WinFormInputHelper<string> mInput;
        private Dictionary<string, Control> mCtrls;

        public Control this[string tabName]
        {
            get { return mCtrls[tabName]; }
            set { mCtrls[tabName] = value;  }
        }

        public string SelectedTab
        {
            get { return mSelectedTab; }
            set
            {
                if (mSelectedTab != value)
                {
                    if (!string.IsNullOrEmpty(mSelectedTab) && mCtrls.ContainsKey(mSelectedTab) && mCtrls[SelectedTab] != null) 
                        this.Controls.Remove(mCtrls[SelectedTab]);
                    mSelectedTab = value;

                    if (mCtrls.ContainsKey(mSelectedTab))
                    {
                        var ctrl = mCtrls[SelectedTab];
                        if (ctrl != null)
                        {
                            ctrl.Dock = DockStyle.None;
                            ctrl.Location = new Point(0, 2 + TAB_MIN_HEIGHT_PADDING + 1);
                            ctrl.Size = new Size(this.Width, this.Height - ctrl.Location.Y);

                            this.Controls.Add(mCtrls[SelectedTab]);
                        }
                    }

                    SelectedChanged?.Invoke(this, EventArgs.Empty);

                    Invalidate();
                }
            }
        }

        public Color SelectedTabBackground
        {
            get;
            set;
        }

        public Color SelectedTabForeColor
        {
            get;
            set;
        }

        public Color NormalTabBackground
        {
            get;
            set;
        }

        public Color NormalTabForeColor
        {
            get;
            set;
        }

        public bool Shadow
        {
            get;
            set;
        }

        public BTabControl()
        {
            mCtrls = new Dictionary<string, Control>();
            mInput = new WinFormInputHelper<string>(this);
            mInput.MouseClick += MInput_MouseClick;
            mInput.MouseEnter += MInput_MouseEnter;
            mInput.MouseLeave += MInput_MouseLeave;
            NormalTabForeColor = IDE.AppConfig.Skin.ForeColor;
            SelectedTabForeColor = IDE.AppConfig.Skin.ForeColor;
            NormalTabBackground = IDE.AppConfig.Skin.BackColor;
            SelectedTabBackground = IDE.AppConfig.Skin.AccentColor;
        }

        private void MInput_MouseLeave(WinFormMouseInputEventArgs<string> args)
        {
            Cursor = Cursors.Default;
        }

        private void MInput_MouseEnter(WinFormMouseInputEventArgs<string> args)
        {
            Cursor = Cursors.Hand;
        }

        private void MInput_MouseClick(WinFormMouseInputEventArgs<string> args)
        {
            SelectedTab = args.Area.Tag;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            /*
            mInput.ClearRegions();

            float x = 0;
            float selectedX = 0;
            foreach (var tab in mCtrls)
            {
                Graphics g = e.Graphics;
                if (SelectedTab == tab.Key)
                {
                    selectedX = x;
                }
                var width = DrawTab(g, x, tab.Key);
                x = x + width;
            }

            DrawTab(e.Graphics, selectedX, SelectedTab);
            */

            mInput.ClearRegions();
            Graphics g = e.Graphics;

            Rectangle rect = new Rectangle(2, 2, ClientRectangle.Width - 4, TAB_MIN_HEIGHT_PADDING - 2);
            g.DrawLine(IDE.AppConfig.Skin.AccentPen, 0, rect.Bottom - 1, ClientRectangle.Right, rect.Bottom - 1);
            g.Clip = new Region(new Rectangle(0, 0, ClientRectangle.Width, TAB_MIN_HEIGHT_PADDING - 1));

            using (var tabFont = new Font(FontFamily.GenericSansSerif, 8))
            {
                foreach (var tab in mCtrls)
                {
                    var size = g.MeasureString(tab.Key, tabFont);
                    rect.Width = (int)Math.Ceiling(size.Width) + TAB_MIN_WIDTH_PADDING;

                    if (SelectedTab != tab.Key)
                    {
                        g.DrawString(tab.Key, tabFont, Brushes.DimGray, rect.Left + 5, rect.Top + 3);
                        mInput.AddRegion(tab.Key, rect);
                    }

                    rect.Offset(rect.Width, 0);
                }

                rect = new Rectangle(2, 2, ClientRectangle.Width - 4, ClientRectangle.Height - 2);

                foreach (var tab in mCtrls)
                {
                    var size = g.MeasureString(tab.Key, tabFont);
                    rect.Width = (int)Math.Ceiling(size.Width) + TAB_MIN_WIDTH_PADDING;

                    if (SelectedTab == tab.Key)
                    {
                        var pen = SelectedTab == tab.Key ? IDE.AppConfig.Skin.AccentPen : new Pen(Color.FromArgb(255, 63, 63, 70));

                        g.DrawLine(pen, rect.Left, rect.Top + 1,
                            rect.Left, rect.Bottom);
                        g.DrawLine(pen, rect.Left + 1, rect.Top,
                            rect.Right - 1, rect.Top);
                        g.DrawLine(pen, rect.Right, rect.Top + 1,
                            rect.Right, rect.Bottom);

                        if (Shadow)
                            g.DrawLine(SystemPens.ControlDark, rect.Right + 1, rect.Top + 2,
                                rect.Right + 1, rect.Bottom - 1);

                        g.FillRectangle(SelectedTab == tab.Key ? IDE.AppConfig.Skin.AccentBrush : new SolidBrush(Color.FromArgb(255, 63, 63, 70)),
                            new Rectangle(rect.Left + 1, rect.Top + 1, rect.Width - 1, rect.Height - 1));

                        g.DrawString(tab.Key, tabFont, new SolidBrush(SelectedTab == tab.Key ? IDE.AppConfig.Skin.ForeColor : Color.FromArgb(255, 241, 241, 241)), rect.Left + TAB_MIN_WIDTH_PADDING / 2, rect.Top + 2);

                        break;
                    }

                    rect.Offset(rect.Width, 0);
                }

            }

            g.Clip = new Region();
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);

            if (!string.IsNullOrEmpty(SelectedTab) && mCtrls.ContainsKey(SelectedTab) && mCtrls[SelectedTab] != null)
            {
                var ctrl = mCtrls[SelectedTab];
                ctrl.Dock = DockStyle.None;
                ctrl.Location = new Point(0, 2 + TAB_MIN_HEIGHT_PADDING + 1);
                ctrl.Size = new Size(this.Width, this.Height - ctrl.Location.Y);
            }
        }

        private float DrawTab(Graphics g, float x, string name)
        {
            var font = IDE.AppConfig.Skin.DefaultFont;
            var size = g.MeasureString(name, font);

            // 圆角半径
            int cRadius = 4;

            // 要实现 圆角化的 矩形
            RectangleF rect = new RectangleF(x, 2, size.Width + TAB_MIN_WIDTH_PADDING - cRadius, TAB_MIN_HEIGHT_PADDING + 1);

            // 指定图形路径， 有一系列 直线/曲线 组成
            GraphicsPath myPath = new GraphicsPath();
            myPath.StartFigure();
            myPath.AddArc(new RectangleF(new PointF(rect.X, rect.Y), new SizeF(2 * cRadius, 2 * cRadius)), 180, 90);
            myPath.AddLine(new PointF(rect.X + cRadius, rect.Y), new PointF(rect.Right - cRadius, rect.Y));
            myPath.AddArc(new RectangleF(new PointF(rect.Right - 2 * cRadius, rect.Y), new Size(2 * cRadius, 2 * cRadius)), 270, 90);

            myPath.AddLine(new PointF(rect.Right, rect.Y + cRadius), new PointF(rect.Right, rect.Bottom - cRadius));
            myPath.AddLine(new PointF(rect.Right, rect.Bottom), new PointF(rect.X, rect.Bottom));

            myPath.AddLine(new PointF(rect.X, rect.Bottom - cRadius), new PointF(rect.X, rect.Y + cRadius));

            myPath.CloseFigure();
            g.FillPath(new SolidBrush(SelectedTab == name ? SelectedTabBackground : NormalTabBackground), myPath);
            g.DrawString(name, font, new SolidBrush(SelectedTab == name ? SelectedTabForeColor : NormalTabForeColor), x + (rect.Width - size.Width) / 2, 2 + (rect.Height - size.Height) / 2 + 2);

            if (SelectedTab != name)
            {
                mInput.AddRegion(name, new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height));
            }
            else
            {
                g.DrawLine(new Pen(SelectedTabBackground), new Point(0, this.Height - 1), new Point(Width, this.Height - 1));
            }

            return size.Width;
        }

        public event EventHandler SelectedChanged;
    }
}
