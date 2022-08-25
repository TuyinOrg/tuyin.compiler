using addin.common;
using BigBuns.Compute.Drawing;
using System;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class BLabel : BUIElement
    {
        class StringLine 
        {
            public string Text { get; }

            public Size Size { get; }

            public StringLine(string text, Size size) 
            {
                Text = text;
                Size = size;
            }
        }


        private StringLine[] mLines;
        private string mText;
        private Font mFont;

        public bool Multiline
        {
            get;
            set;
        } = true;

        public string Text
        {
            get { return mText; }
            set 
            {
                if (mText != value) 
                {
                    mText = value;
                    ComputeSize();
                }
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
                    ComputeSize();
                }
            }
        }

        public Color ForeColor 
        {
            get;
            set;
        }

        public override Size Size 
        { 
            get => base.Size;
            set
            {
                if (base.Size != value)
                {
                    base.Size = value;
                    ComputeSize();
                }
            }
        }

        public BLabel(IBControl host) 
            : base(host)
        {
            Font = IDE.AppConfig.Skin.DefaultFont;
            ForeColor = IDE.AppConfig.Skin.ForeColor;
        }

        private bool CanPaint() 
        {
            return !(string.IsNullOrEmpty(Text) || Font == null || Size.Width * Size.Height == 0 || mLines == null || mLines.Length == 0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!CanPaint())
            {
                ComputeSize();
                if (!CanPaint())
                {
                    return;
                }
            }

            var y = 0;
            for (var i = 0; i < mLines.Length; i++)
            {
                var line = mLines[i];
                e.Graphics.DrawString(line.Text, Font, Enabled ? new SolidBrush(ForeColor) : IDE.AppConfig.Skin.TipsBrush, Location.X, Location.Y + y);

                y = y + line.Size.Height + 2;
            }
        }

        private void ComputeSize() 
        {
            if (string.IsNullOrEmpty(Text) || Font == null || Host.Width * Host.Height == 0)
            {
                Size = Size.Empty;
                return;
            }

            if (Multiline)
            {
                var lines = StringHelper.CheckLength(Font, Text, Host.Width - Location.X);
                mLines = new StringLine[lines.Length];
                for(var i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];
                    var size = TextRenderer.MeasureText(line, Font);
                    mLines[i] = new StringLine(line, size);
                }
            }
            else 
            {
                mLines = new StringLine[1];
                mLines[1] = new StringLine(Text, TextRenderer.MeasureText(Text, Font));
            }

            var width = 0;
            var height = 0;

            for (var i = 0; i < mLines.Length; i++) 
            {
                var line = mLines[i];
                width = Math.Max(width, line.Size.Width);
                height = height + line.Size.Height + 2;
            }

            Size = new Size(width, height);
        }
    }
}
