using addin.common;
using addin.extends;
using isc;
using notelib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class BInputField : BUIElement, ITextBox
    {
        private readonly static Image sIcon = Resources.GetManifestBitmap(typeof(BInputField).Assembly, "resources.书写.png");

        private bool mFocused;
        private int mIndex;
        private Font mFont;
        private CharSource mSource;

        public int Index 
        {
            get { return mIndex; }
            set 
            {
                var val = value;
                if (val < 0)
                    val = 0;

                if (val > mSource.Length - 1)
                    val = mSource.Length - 1;

                if (mIndex != val)
                { 
                    mIndex = val;
                    Host.Invalidate();
                }
            }
        }

        public int CharWidth 
        {
            get;
            private set;
        }

        public int ChineseCharWidth 
        {
            get;
            private set;
        }

        public int CharHeight 
        {
            get;
            private set;
        }

        public int LineInterval 
        {
            get;
            set;
        }

        public int LineHeight 
        {
            get;
            private set;
        }

        public string Text 
        {
            get { return mSource.ToString(); }
            set 
            {
                mSource.Clear();
                mSource.Insert(0, value.ToCharArray(), value.Length);
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
                    ApplyFont(mFont);
                }
            }
        }

        public Color ForeColor 
        {
            get;
            set;
        }

        public Color BackColor 
        {
            get;
            set;
        }

        public StringRef Source => new StringRef(mSource.AsSpan(0, mSource.Length));

        public BInputField(IBControl host) 
            : base(host)
        {
            Font = host.Font;
            mSource = new CharSource(64);

            ForeColor = IDE.AppConfig.Skin.ForeColor;
            BackColor = IDE.AppConfig.Skin.BackColor;

            Host.MouseDown += Host_MouseDown;
            Host.LostFocus += Host_LostFocus;
            Host.KeyDown += Host_KeyDown;
        }

        private void Host_LostFocus(object sender, EventArgs e)
        {
            mFocused = false;
        }

        private void Host_KeyDown(object sender, KeyEventArgs e)
        {
            if (mFocused) 
            {
                var args = new KeyPressEventArgs(Native.KeyCodeToChar(e.KeyCode));
                KeyPressing?.Invoke(this, args);
                if (args.Handled)
                    return;

                switch (e.KeyCode) 
                {
                    case Keys.Left:
                        Index--;
                        break;
                    case Keys.Right:
                        Index++;
                        break;
                    case Keys.Back:
                        if (Index > 0)
                            mSource.Remove(Index--, 1);
                        break;
                    case Keys.D1:
                        mSource.Insert(Index++, '1', 1);
                        break;
                    case Keys.D2:
                        mSource.Insert(Index++, '2', 1);
                        break;
                    case Keys.D3:
                        mSource.Insert(Index++, '3', 1);
                        break;
                    case Keys.D4:
                        mSource.Insert(Index++, '4', 1);
                        break;
                    case Keys.D5:
                        mSource.Insert(Index++, '5', 1);
                        break;
                    case Keys.D6:
                        mSource.Insert(Index++, '6', 1);
                        break;
                    case Keys.D7:
                        mSource.Insert(Index++, '7', 1);
                        break;
                    case Keys.D8:
                        mSource.Insert(Index++, '8', 1);
                        break;
                    case Keys.D9:
                        mSource.Insert(Index++, '9', 1);
                        break;
                }

                KeyPressed?.Invoke(this, args);
            }
        }

        private void Host_MouseDown(object sender, MouseEventArgs e)
        {
            var bounds = new Rectangle(Location, Size);
            if (bounds.Contains(e.Location))
            {
                mFocused = true;

                // 计算位置并设置index
                if (!string.IsNullOrWhiteSpace(Text))
                {
                    var x = Location.X;
                    var index = -1;
                    for (var i = 0; i < mSource.Length; i++)
                    {
                        var c = mSource[i];

                        var w = c < 127 ? CharWidth : ChineseCharWidth;

                        var b = new Rectangle(x, Location.Y, w, LineHeight);
                        if(b.Contains(e.Location))
                        {
                            index = i;
                            break;
                        }

                        x = x + w + 1;
                    }

                    if (x < 0) index = 0;
                    if (index == -1)
                        index = mSource.Length - 1;

                    Index = index;
                }
            }
            else 
            {
                mFocused = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (BackColor.A != 0)
                e.Graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(Location, Size));

            var foreBrush = new SolidBrush(ForeColor);
            var backBrush = new SolidBrush(BackColor);

            var x = Location.X;
            var y = Location.Y;

            if (!string.IsNullOrWhiteSpace(Text))
            {
                for (var i = 0; i < mSource.Length; i++) 
                {
                    var c = mSource[i];
                    e.Graphics.DrawString(
                        new string(c, 1), 
                        Font,
                        foreBrush,
                        x, 
                        y);

                    var w = c < 127 ? CharWidth : ChineseCharWidth;

                    x = x + w + 1;
                }
            }

            x = x + 2;

            // 绘制指针
            if (mFocused)
                e.Graphics.DrawImage(sIcon, x, y + (Size.Height - sIcon.Height) / 2);

            foreBrush.Dispose();
            backBrush.Dispose();
        }

        public bool Focus()
        {
            if (Host.Focus())
            {
                mFocused = true;
                return true;
            }

            return false;
        }

        public void Goto(int charIndex)
        {
        }

        public void Goto(int line, int column)
        {
        }

        public void Select(int start, int end)
        {
        }

        public void SelectAll()
        {
        }

        public void Invalidate()
        {
            Host.Invalidate();
        }

        private void ApplyFont(Font font)
        {
            var size = GetCharSize(font, '2');

            CharWidth = (int)Math.Round(size.Width * 1f) - 1;
            ChineseCharWidth = CharWidth * 2;
            CharHeight = (int)Math.Round(size.Height * 1f) - 1;
            LineHeight = LineInterval + CharHeight - 1;
        }

        private static SizeF GetCharSize(Font font, char c)
        {
            Size sz2 = TextRenderer.MeasureText("<" + c.ToString() + ">", font);
            Size sz3 = TextRenderer.MeasureText("<>", font);

            var size = new SizeF(sz2.Width - sz3.Width + 1, font.Height);
            return size;
        }

        public event KeyPressEventHandler KeyPressing;
        public event KeyPressEventHandler KeyPressed;
    }
}
