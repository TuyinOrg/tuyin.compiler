using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public delegate void ColorDelegate(Color color);

    public class BColorGrid : BUIElement
    {
        class ColorItem : IBListViewItem
        {
            public Color Color { get; }

            public ColorItem(Color color) 
            {
                Color = color;
            }

            public BListViewItemState State 
            {
                get; 
                set;
            }

            public bool Enabled 
            { 
                get; 
                set;
            }

            public Point Location 
            { 
                get; 
                set; 
            }

            public Size Size 
            {
                get;
                set;
            }

            public Padding Padding
            { 
                get; 
                set; 
            }

            public void Paint(PaintEventArgs e)
            {
                var padding = State == BListViewItemState.Selected ? 0 : 2;
                var bounds = new Rectangle(
                    Location.X + padding, 
                    Location.Y + padding, 
                    Size.Width - padding * 2, 
                    Size.Height - padding * 2);

                e.Graphics.FillColor(Color, bounds, bounds.Width / 2);

                if (Color.A != 0)
                    e.Graphics.DrawRectangle(new Pen(Color.Black), bounds);
            }
        }

        private bool? mScrollBarVisual;
        private ColorItem mSelected;
        private BListView mListView;

        public Color Selected => mSelected?.Color ?? default;

        public IEnumerable<Color> Colors => mListView.Items.Cast<ColorItem>().Select(x => x.Color);

        public bool ScrollBarVisual
        {
            get { return mScrollBarVisual ?? mListView.ScrollBar.Visible; }
            set 
            {
                if (mScrollBarVisual != value)
                {
                    mScrollBarVisual = value;
                    mListView.ScrollBar.AutoHide = false;
                    mListView.ScrollBar.Visible = value;
                }
            }
        }

        public int ColumnCount
        {
            get;
            set;
        } = 5;

        public override Point Location 
        { 
            get => mListView.Location; 
            set => mListView.Location = value;
        }

        public override Size Size 
        {
            get => mListView.Size; 
            set => mListView.Size = value;
        }

        public bool AllowSelect 
        {
            get;
            set;
        }

        public bool AutoSize
        {
            get;
            set;
        } = true;

        public BColorGrid(IBControl host, IEnumerable<Color> colors, int minCount) 
            : base(host)
        {
            mListView = new BListView(host);
            mListView.IsHorizontal = false;
            mListView.ItemPadding = new Padding(1);
            mListView.MultipleSelect = false;
            mListView.ItemMouseClick += MListView_ItemMouseClick;

            ChangeColors(colors, minCount);
        }

        private void MListView_ItemMouseClick(object sender, BListViewEventArgs e)
        {
            var item = e.Item as ColorItem;
            if (AllowSelect)
            {
                if (item != mSelected)
                {
                    mSelected = item;
                    SelectedChanged?.Invoke(item.Color);
                }
            }
            else 
            {
                e.Item.State = BListViewItemState.Normal;
            }

            Click?.Invoke(item.Color);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var itemWidth = (Size.Width - mListView.ScrollBar.Width) / (ColumnCount == 0 ? 5 : ColumnCount);

            if (AutoSize)
            {
                mListView.Size = new Size(
                    itemWidth * Math.Min(ColumnCount == 0 ? 6 : ColumnCount, mListView.Items.Count),
                    itemWidth * (int)(Math.Ceiling((double)mListView.Items.Count / (ColumnCount == 0 ? 5 : ColumnCount))));
            }

            mListView.ItemSize = new Size(itemWidth, itemWidth);
            mListView.Paint(e);
        }

        public event ColorDelegate SelectedChanged;
        public event ColorDelegate Click;

        public void ChangeColors(IEnumerable<Color> colors, int minCount)
        {
            mListView.Items.Clear();

            var list = colors.ToList();
            for (var i = list.Count; i < minCount; i++)
                list.Add(default);

            list.Do(x => mListView.Items.Add(new ColorItem(x)));

            mSelected = mListView.Items.FirstOrDefault() as ColorItem;
            mSelected.State = BListViewItemState.Selected;
        }
    }
}
