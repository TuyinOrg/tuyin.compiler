using addin.common;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace addin.controls.renderer
{
    public class BSplitLine : BUIElement
    {
        private const int BAR_HEIGHT = 16;

        private WinFormInputHelper<DockStyle> mInputHelper;

        private DockStyle mAttach;
        private BButtonState mState;

        private int mDownValue;
        private int mDownValue2;
        private int mDownPoint;

        public int? LimitLength 
        {
            get;
            set;
        }

        public int BarWidth
        {
            get;
            set;
        } = 5;

        public IUIElment Element 
        {
            get;
        }

        public DockStyle Dock 
        {
            get;
            set;
        } = DockStyle.Right;

        public Color NormalColor 
        {
            get;
            set;
        }

        public Color HoverColor 
        {
            get;
            set;
        }

        public Color PressedColor 
        {
            get;
            set;
        }

        public override Point Location 
        {
            get 
            {
                var x = Dock.HasFlag(DockStyle.Left) ? Element.Location.X - BarWidth : Element.Location.X;
                var y = Dock.HasFlag(DockStyle.Top) ? Element.Location.Y - BarWidth : Element.Location.Y;

                return new Point(x, y);
            }
            set 
            {
                var x = Dock.HasFlag(DockStyle.Left) ? value.X + BarWidth : value.X;
                var y = Dock.HasFlag(DockStyle.Top) ? value.Y - BarWidth : value.Y;

                Element.Location = new Point(x, y);
            }
        }

        public override Size Size 
        {
            get 
            {
                var w = Element.Size.Width;
                var h = Element.Size.Height;

                if (Dock.HasFlag(DockStyle.Left))
                    w = w + BarWidth;

                if (Dock.HasFlag(DockStyle.Top))
                    h = h + BarWidth;

                if (Dock.HasFlag(DockStyle.Right))
                    w = w + BarWidth;

                if (Dock.HasFlag(DockStyle.Bottom))
                    h = h + BarWidth;

                return new Size(w, h);
            }
            set 
            {
                var w = value.Width;
                var h = value.Height;

                if (Dock.HasFlag(DockStyle.Left))
                    w = w - BarWidth;

                if (Dock.HasFlag(DockStyle.Top))
                    h = h - BarWidth;

                if (Dock.HasFlag(DockStyle.Right))
                    w = w - BarWidth;

                if (Dock.HasFlag(DockStyle.Bottom))
                    h = h - BarWidth;

                Element.Size = new Size(w, h);
            }
        }

        public BSplitLine(BUIElement element) 
            : base(element.Host)
        {
            Element = element;
            NormalColor = Color.FromArgb(IDE.AppConfig.Skin.BackColor.R - 10, IDE.AppConfig.Skin.BackColor.G - 10, IDE.AppConfig.Skin.BackColor.B - 10);
            PressedColor = IDE.AppConfig.Skin.AccentColor;
            HoverColor = ControlPaint.Light(PressedColor);

            mInputHelper = new WinFormInputHelper<DockStyle>(Host);
            mInputHelper.MouseEnter += MInputHelper_MouseEnter;
            mInputHelper.MouseLeave += MInputHelper_MouseLeave;
            mInputHelper.MouseDown += MInputHelper_MouseDown;
        }

        public BSplitLine(IBControl host, IBControl element)
            : base(host)
        {
            Element = element;
            NormalColor = Color.FromArgb(IDE.AppConfig.Skin.BackColor.R - 10, IDE.AppConfig.Skin.BackColor.G - 10, IDE.AppConfig.Skin.BackColor.B - 10);
            PressedColor = IDE.AppConfig.Skin.AccentColor;
            HoverColor = ControlPaint.Light(PressedColor);

            mInputHelper = new WinFormInputHelper<DockStyle>(Host);
            mInputHelper.MouseEnter += MInputHelper_MouseEnter;
            mInputHelper.MouseLeave += MInputHelper_MouseLeave;
            mInputHelper.MouseDown += MInputHelper_MouseDown;
        }

        private void MInputHelper_MouseDown(WinFormMouseInputEventArgs<DockStyle> args) 
        {
            mState = BButtonState.Pressed;
            mAttach = args.Area.Tag;

            var p = Host.PointToScreen(args.Args.Location);

            switch (args.Area.Tag) 
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    mDownPoint = p.X;
                    mDownValue = Element.Location.X;
                    mDownValue2 = Element.Size.Width;
                    break;
                case DockStyle.Top:
                case DockStyle.Bottom:
                    mDownPoint = p.Y;
                    mDownValue = Element.Location.Y;
                    mDownValue2 = Element.Size.Height;
                    break;
            }

            IDE.GlobalHandler.MouseMove += GlobalHandler_MouseMove;
            IDE.GlobalHandler.MouseLeftButtonUp += GlobalHandler_MouseLeftButtonUp;

            Host.Invalidate();
        }

        private void GlobalHandler_MouseLeftButtonUp(object sender, GlobalEventArgs<MouseEventArgs> e) 
        {
            Host.Cursor = Cursors.Default;
            mState = BButtonState.Normal;
            
            IDE.GlobalHandler.MouseMove -= GlobalHandler_MouseMove;
            IDE.GlobalHandler.MouseLeftButtonUp -= GlobalHandler_MouseLeftButtonUp;

            Host.Invalidate();

            Changed?.Invoke(this, e.EventArgs);
        }

        private void GlobalHandler_MouseMove(object sender, GlobalEventArgs<MouseEventArgs> e) 
        {
            mState = BButtonState.Pressed;
            var x = e.EventArgs.X - mDownPoint;
            var y = e.EventArgs.Y - mDownPoint;

            switch (mAttach)
            {
                case DockStyle.Left:
                    Element.Location = new Point(mDownValue + x, Element.Location.Y);
                    Element.Size = new Size(mDownValue2 - x, Element.Size.Height);
                    break;
                case DockStyle.Right:
                    Element.Size = new Size(mDownValue2 + x, Element.Size.Height);
                    break;
                case DockStyle.Top:
                    Element.Location = new Point(Element.Location.X, mDownValue + y);
                    Element.Size = new Size(Element.Size.Width, mDownValue2 - y);
                    break;
                case DockStyle.Bottom:
                    Element.Size = new Size(Element.Size.Width, mDownValue2 + y);
                    break;
            }

            Host.Invalidate();
            /*
            if (Element is IBControl host)
                if (host != Host)
                    host.Refresh();
            */
        }

        private void MInputHelper_MouseLeave(WinFormMouseInputEventArgs<DockStyle> args) 
        {
            if (args.Args.Button != MouseButtons.Left)
            {
                Host.Cursor = Cursors.Default;
                mState = BButtonState.Normal;
                Host.Invalidate();
            }
        }

        private void MInputHelper_MouseEnter(WinFormMouseInputEventArgs<DockStyle> args) 
        {
            if (args.Args.Button != MouseButtons.Left)
            {
                mAttach = args.Area.Tag;
                mState = BButtonState.Hover;

                switch (mAttach)
                {
                    case DockStyle.Left:
                    case DockStyle.Right:
                        Host.Cursor = Cursors.SizeWE;
                        break;
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        Host.Cursor = Cursors.SizeNS;
                        break;
                }

                Host.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e) 
        {
            Element.Paint(e);

            if (Dock.HasFlag(DockStyle.Left))
                PaintLine(e.Graphics, DockStyle.Left, new Point(Element.Location.X - BarWidth, Element.Location.Y), new Point(Element.Location.X - BarWidth, Element.Location.Y + (LimitLength ?? Element.Size.Height)));

            if (Dock.HasFlag(DockStyle.Top))
                PaintLine(e.Graphics, DockStyle.Top, new Point(Element.Location.X, Element.Location.Y - BarWidth), new Point(Element.Location.X + (LimitLength ?? Element.Size.Width), Element.Location.Y - BarWidth));

            if (Dock.HasFlag(DockStyle.Right))
                PaintLine(e.Graphics, DockStyle.Right, new Point(Element.Location.X + Element.Size.Width, Element.Location.Y), new Point(Element.Location.X + Element.Size.Width, Element.Location.Y + (LimitLength ?? Element.Size.Height)));

            if (Dock.HasFlag(DockStyle.Bottom))
                PaintLine(e.Graphics, DockStyle.Bottom, new Point(Element.Location.X, Element.Location.Y + Element.Size.Height), new Point(Element.Location.X + (LimitLength ?? Element.Size.Width), Element.Location.Y + Element.Size.Height));

        }

        private void PaintLine(Graphics g, DockStyle style, Point start, Point end) 
        {
            Pen pen = null;
            switch (mState) 
            {
                case BButtonState.Normal:
                    pen = new Pen(NormalColor);
                    break;
                case BButtonState.Disabled:
                    pen = new Pen(Color.DarkGray);
                    break;
                case BButtonState.Hover:
                    pen = new Pen(HoverColor);
                    break;
                case BButtonState.Pressed:
                    pen = new Pen(PressedColor);
                    break;
            }

            if (style == DockStyle.Left || style == DockStyle.Right)
            {
                g.DrawLine(pen, start.X + (BarWidth - pen.Width) / 2, start.Y, start.X + (BarWidth - pen.Width) / 2, start.Y + (end.Y - start.Y) / 2 - BAR_HEIGHT / 2 - BarWidth);
                g.DrawLine(pen, start.X + (BarWidth - pen.Width) / 2, start.Y + (end.Y - start.Y) / 2 + BAR_HEIGHT / 2 + BarWidth, start.X + (BarWidth - pen.Width) / 2, end.Y);

                var x = start.X + BarWidth / 2;
                var y = start.Y + (end.Y - start.Y) / 2 - BAR_HEIGHT / 2;
                g.DrawLine(pen, x, y, x, y + BAR_HEIGHT);
                g.DrawLine(pen, x - 1, y + 1, x - 1, y + BAR_HEIGHT - 1);
                g.DrawLine(pen, x + 1, y + 1, x + 1, y + BAR_HEIGHT - 1);

                mInputHelper.AddRegion(style, start.X, start.Y, BarWidth, end.Y - start.Y);
            }
            else 
            {
                g.DrawLine(pen, start.X, start.Y + (BarWidth - pen.Width) / 2, start.X + (end.X - start.X) / 2 - BAR_HEIGHT / 2 - BarWidth, start.Y + (BarWidth - pen.Width) / 2);
                g.DrawLine(pen, start.X + (end.X - start.X) / 2 + BAR_HEIGHT / 2 + BarWidth, start.Y + (BarWidth - pen.Width) / 2, end.X, start.Y + (BarWidth - pen.Width) / 2);

                var x = start.X + (end.X - start.X) / 2 - BAR_HEIGHT / 2;
                var y = start.Y + BarWidth / 2;
                g.DrawLine(pen, x, y, x + BAR_HEIGHT, y);
                g.DrawLine(pen, x + 1, y - 1, x + BAR_HEIGHT - 1, y - 1);
                g.DrawLine(pen, x + 1, y + 1, x + BAR_HEIGHT - 1, y + 1);

                mInputHelper.AddRegion(style, start.X, start.Y, end.X - start.X, BarWidth);
            }

            pen.Dispose();
        }

        public event EventHandler Changed;
    }

    public class BSplitLineValue : BUIElement
    {
        private const int BAR_HEIGHT = 16;

        private WinFormInputHelper<bool> mInputHelper;
        private int mValue;
        private BButtonState mState;

        private int mDownValue;
        private int mDownPoint;

        public int Value 
        {
            get { return mValue; }
            set 
            {
                if (mValue != value) 
                {
                    mValue = value;
                    Changed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public int Length 
        {
            get;
            set;
        }

        public bool IsHorizontal 
        {
            get;
            set;
        }

        public int BarWidth
        {
            get;
            set;
        } = 5;

        public Color NormalColor
        {
            get;
            set;
        }

        public Color HoverColor
        {
            get;
            set;
        }

        public Color PressedColor
        {
            get;
            set;
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

        public BSplitLineValue(IBControl host)
            : base(host)
        {
            NormalColor = Color.FromArgb(IDE.AppConfig.Skin.BackColor.R - 10, IDE.AppConfig.Skin.BackColor.G - 10, IDE.AppConfig.Skin.BackColor.B - 10);
            PressedColor = IDE.AppConfig.Skin.AccentColor;
            HoverColor = ControlPaint.Light(PressedColor);

            mInputHelper = new WinFormInputHelper<bool>(Host);
            mInputHelper.MouseEnter += MInputHelper_MouseEnter;
            mInputHelper.MouseLeave += MInputHelper_MouseLeave;
            mInputHelper.MouseDown += MInputHelper_MouseDown;
        }

        private void MInputHelper_MouseDown(WinFormMouseInputEventArgs<bool> args)
        {
            mState = BButtonState.Pressed;

            var p = Host.PointToScreen(args.Args.Location);

            if (!IsHorizontal)
            {
                mDownPoint = p.X;
                mDownValue = Value;
            }
            else 
            {
                mDownPoint = p.Y;
                mDownValue = Value;
            }

            IDE.GlobalHandler.MouseMove += GlobalHandler_MouseMove;
            IDE.GlobalHandler.MouseLeftButtonUp += GlobalHandler_MouseLeftButtonUp;

            Host.Invalidate();
        }

        private void GlobalHandler_MouseLeftButtonUp(object sender, GlobalEventArgs<MouseEventArgs> e)
        {
            Host.Cursor = Cursors.Default;
            mState = BButtonState.Normal;

            IDE.GlobalHandler.MouseMove -= GlobalHandler_MouseMove;
            IDE.GlobalHandler.MouseLeftButtonUp -= GlobalHandler_MouseLeftButtonUp;

            Host.Invalidate();
        }

        private void GlobalHandler_MouseMove(object sender, GlobalEventArgs<MouseEventArgs> e)
        {
            mState = BButtonState.Pressed;
            var x = e.EventArgs.X - mDownPoint;
            var y = e.EventArgs.Y - mDownPoint;

            if (!IsHorizontal)
            {
                Value = mDownValue + x;
            }
            else 
            {
                Value = mDownValue + y;
            }

            Host.Invalidate();
            /*
            if (Element is IBControl host)
                if (host != Host)
                    host.Refresh();
            */
        }

        private void MInputHelper_MouseLeave(WinFormMouseInputEventArgs<bool> args)
        {
            if (args.Args.Button != MouseButtons.Left)
            {
                Host.Cursor = Cursors.Default;
                mState = BButtonState.Normal;
                Host.Invalidate();
            }
        }

        private void MInputHelper_MouseEnter(WinFormMouseInputEventArgs<bool> args)
        {
            if (args.Args.Button != MouseButtons.Left)
            {
                mState = BButtonState.Hover;

                if (!IsHorizontal)
                {
                    Host.Cursor = Cursors.SizeWE;
                }
                else 
                {
                    Host.Cursor = Cursors.SizeNS;
                }

                Host.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (IsHorizontal)
            {
                PaintLine(e.Graphics, new Point(Location.X, Location.Y - BarWidth / 2), new Point(Location.X + Length, Location.Y - BarWidth / 2));
            }
            else 
            {
                PaintLine(e.Graphics, new Point(Location.X - BarWidth / 2, Location.Y), new Point(Location.X - BarWidth / 2, Location.Y + Length));
            }
        }

        private void PaintLine(Graphics g, Point start, Point end)
        {
            Pen pen = null;
            switch (mState)
            {
                case BButtonState.Normal:
                    pen = new Pen(NormalColor);
                    break;
                case BButtonState.Disabled:
                    pen = new Pen(Color.DarkGray);
                    break;
                case BButtonState.Hover:
                    pen = new Pen(HoverColor);
                    break;
                case BButtonState.Pressed:
                    pen = new Pen(PressedColor);
                    break;
            }

            if (!IsHorizontal)
            {
                g.DrawLine(pen, start.X + (BarWidth - pen.Width) / 2, start.Y, start.X + (BarWidth - pen.Width) / 2, start.Y + (end.Y - start.Y) / 2 - BAR_HEIGHT / 2 - BarWidth);
                g.DrawLine(pen, start.X + (BarWidth - pen.Width) / 2, start.Y + (end.Y - start.Y) / 2 + BAR_HEIGHT / 2 + BarWidth, start.X + (BarWidth - pen.Width) / 2, end.Y);

                var x = start.X + BarWidth / 2;
                var y = start.Y + (end.Y - start.Y) / 2 - BAR_HEIGHT / 2;
                g.DrawLine(pen, x, y, x, y + BAR_HEIGHT);
                g.DrawLine(pen, x - 1, y + 1, x - 1, y + BAR_HEIGHT - 1);
                g.DrawLine(pen, x + 1, y + 1, x + 1, y + BAR_HEIGHT - 1);

                mInputHelper.AddRegion(IsHorizontal, start.X, start.Y, BarWidth, end.Y - start.Y);
            }
            else
            {
                g.DrawLine(pen, start.X, start.Y + (BarWidth - pen.Width) / 2, start.X + (end.X - start.X) / 2 - BAR_HEIGHT / 2 - BarWidth, start.Y + (BarWidth - pen.Width) / 2);
                g.DrawLine(pen, start.X + (end.X - start.X) / 2 + BAR_HEIGHT / 2 + BarWidth, start.Y + (BarWidth - pen.Width) / 2, end.X, start.Y + (BarWidth - pen.Width) / 2);

                var x = start.X + (end.X - start.X) / 2 - BAR_HEIGHT / 2;
                var y = start.Y + BarWidth / 2;
                g.DrawLine(pen, x, y, x + BAR_HEIGHT, y);
                g.DrawLine(pen, x + 1, y - 1, x + BAR_HEIGHT - 1, y - 1);
                g.DrawLine(pen, x + 1, y + 1, x + BAR_HEIGHT - 1, y + 1);

                mInputHelper.AddRegion(IsHorizontal, start.X, start.Y, end.X - start.X, BarWidth);
            }

            pen.Dispose();
        }

        public event EventHandler Changed;
    }
}
