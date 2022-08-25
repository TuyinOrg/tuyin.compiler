using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class BTab : BUIElement
    {
        public const int TAB_MIN_WIDTH_PADDING = 8;
        public const int TAB_MIN_HEIGHT_PADDING = 20;

        private string mSelectedTab;
        private WinFormInputHelper<string> mInput;
        private Dictionary<string, BUIElement> mCtrls;

        public BUIElement this[string tabName]
        {
            get { return mCtrls[tabName]; }
            set { mCtrls[tabName] = value; }
        }

        public string SelectedTab
        {
            get { return mSelectedTab; }
            set
            {
                if (mSelectedTab != value && mCtrls.ContainsKey(value) && mCtrls[value] != null)
                {
                    mSelectedTab = value;
                    SelectedChanged?.Invoke(this, EventArgs.Empty);

                    Host.Invalidate();
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

        public BTab(IBControl host)
            : base(host)
        {
            mCtrls = new Dictionary<string, BUIElement>();
            mInput = new WinFormInputHelper<string>(host);
            mInput.MouseClick += MInput_MouseClick;
            mInput.MouseEnter += MInput_MouseEnter;
            mInput.MouseLeave += MInput_MouseLeave;
            NormalTabForeColor = Color.FromArgb(165, 165, 165);
            SelectedTabForeColor = IDE.AppConfig.Skin.ForeColor;
            NormalTabBackground = IDE.AppConfig.Skin.BackColor;
            SelectedTabBackground = IDE.AppConfig.Skin.AccentColor;
        }

        private void MInput_MouseLeave(WinFormMouseInputEventArgs<string> args)
        {
            Host.Cursor = Cursors.Default;
        }

        private void MInput_MouseEnter(WinFormMouseInputEventArgs<string> args)
        {
            Host.Cursor = Cursors.Hand;
        }

        private void MInput_MouseClick(WinFormMouseInputEventArgs<string> args)
        {
            SelectedTab = args.Area.Tag;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var brush1 = new SolidBrush(SelectedTabForeColor);
            var brush2 = new SolidBrush(NormalTabForeColor);

            Rectangle rect = new Rectangle(Location.X + 2, Location.Y + 2, Size.Width - 4, TAB_MIN_HEIGHT_PADDING - 2);
            g.DrawLine(IDE.AppConfig.Skin.AccentPen, 0, rect.Bottom - 1, Location.X + Size.Width, rect.Bottom - 1);
            g.Clip = new Region(new Rectangle(Location.X, Location.Y, Size.Width, TAB_MIN_HEIGHT_PADDING - 1));

            using (var tabFont = new Font(FontFamily.GenericSansSerif, 8))
            {
                foreach (var tab in mCtrls)
                {
                    var size = g.MeasureString(tab.Key, tabFont);
                    rect.Width = (int)Math.Ceiling(size.Width) + TAB_MIN_WIDTH_PADDING;

                    if (SelectedTab != tab.Key)
                    {
                        g.DrawString(tab.Key, tabFont, brush2, rect.Left + 5, rect.Top + 3);
                        mInput.AddRegion(tab.Key, rect);
                    }

                    rect.Offset(rect.Width, 0);
                }

                rect = new Rectangle(Location.X + 2, Location.Y + 2, Size.Width - 4, Size.Height - 2);

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

                        g.DrawString(tab.Key, tabFont, brush1, rect.Left + TAB_MIN_WIDTH_PADDING / 2, rect.Top + 2);

                        break;
                    }

                    rect.Offset(rect.Width, 0);
                }

                if (mCtrls.ContainsKey(SelectedTab))
                {
                    var ctrl = mCtrls[SelectedTab];
                    if (ctrl != null)
                    {
                        ctrl.Location = new Point(Location.X, Location.Y + 2 + TAB_MIN_HEIGHT_PADDING + 1);
                        ctrl.Size = new Size(Size.Width, Size.Height - ctrl.Location.Y);
                        ctrl.Paint(e);
                    }
                }

                brush1.Dispose();
                brush2.Dispose();
            }

            g.Clip = new Region();
        }

        public event EventHandler SelectedChanged;
    }
}
