using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class BPanel : BUIElement
    {
        public bool IsHorizontal
        {
            get;
            set;
        } = true;

        public int Interval
        {
            get;
            set;
        } = 3;

        public List<BUIElement> Elements 
        {
            get;
        }

        public BPanel(IBControl host, params BUIElement[] elements) 
            : base(host)
        {
            Elements = new List<BUIElement>(elements);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var width = 0;
            var height = 0;
            if (IsHorizontal)
            {
                foreach (var item in Elements)
                {
                    var x = Location.X + width;

                    item.Location = new Point(x, Location.Y);
                    item.Paint(e);

                    width = width + item.Size.Width + Interval;
                    height = Math.Max(height, item.Size.Height);
                }
            }
            else
            {
                foreach (var item in Elements)
                {
                    var y = Location.Y + height;

                    item.Location = new Point(Location.X, y);
                    item.Paint(e);

                    width = Math.Max(width, item.Size.Width);
                    height = height + item.Size.Height + Interval;
                }
            }

            Size = new Size(width, height);
        }
    }
}
