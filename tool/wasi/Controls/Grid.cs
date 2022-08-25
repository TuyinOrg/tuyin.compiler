using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    public class Grid<T> where T : IUIElment
    {
        private BScrollBar mScrollBar;

        private Dictionary<T, Rectangle> mItemCells;

        public List<GridLength> Rows { get; }

        public List<GridLength> Columns { get; }

        public List<GridItem<T>> Items { get; }

        public Grid()
        {
            Rows = new List<GridLength>();
            Columns = new List<GridLength>();
            Items = new List<GridItem<T>>();
            mItemCells = new Dictionary<T, Rectangle>();
        }

        public Grid(IBControl host) 
            : this()
        {
            mScrollBar = new BScrollBar(host);
            mScrollBar.AutoHide = true;
        }

        public Rectangle GetItemCell(T item)
        {
            if (mItemCells.ContainsKey(item))
                return mItemCells[item];

            return default;
        }

        public Size ComputeLayout(Size maxSize)
        {
            mItemCells.Clear();

            var autoItems = Items.Where(x => IsAutoRectangle(x)).ToArray();

            foreach (var item in autoItems)
            {
                if (Rows[item.Row].Type == GridType.Auto)
                    Rows[item.Row] = new GridLength(GridType.Auto, 0);

                if (Columns[item.Column].Type == GridType.Auto)
                    Columns[item.Column] = new GridLength(GridType.Auto, 0);
            }

            foreach (var item in autoItems)
            {
                var size = ComputeSize(item.Item, maxSize);

                if (Rows[item.Row].Type == GridType.Auto)
                    Rows[item.Row] = new GridLength(GridType.Auto, size.Height);

                if (Columns[item.Column].Type == GridType.Auto)
                    Columns[item.Column] = new GridLength(GridType.Auto, size.Width);
            }

            var rects = GetRectangles(maxSize);
            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                var rect = rects[item.Column + item.Row * Columns.Count];
                mItemCells[item.Item] = rect;
            }

            var right = rects.Max(x => x.Right);
            var bottom = rects.Max(x => x.Bottom);
            return new Size(right, bottom);
        }

        private Rectangle[] GetRectangles(Size clientSize)
        {
            var list = new List<Rectangle>();
            var cr = Columns.Where(x => x.Type == GridType.Rate).Sum(x => x.Value);
            var rr = Rows.Where(x => x.Type == GridType.Rate).Sum(x => x.Value);

            var sw = clientSize.Width - Columns.Where(x => x.Type != GridType.Rate).Sum(x => x.Value);
            var sh = clientSize.Height - Rows.Where(x => x.Type != GridType.Rate).Sum(x => x.Value);

            var offsetY = 0.0;
            for (var y = 0; y < Rows.Count; y++)
            {
                var row = Rows[y];
                var height = row.Type == GridType.Rate ? (row.Value / rr) * sh : row.Value;
                double offsetX = 0.0;
                for (var x = 0; x < Columns.Count; x++)
                {
                    var column = Columns[x];
                    var width = column.Type == GridType.Rate ? (column.Value / cr) * sw : column.Value;

                    list.Add(new Rectangle((int)offsetX, (int)offsetY, (int)width, (int)height));
                    offsetX = offsetX + width;
                }

                offsetY = offsetY + height;
            }

            return list.ToArray();
        }

        public void Paint(PaintEventArgs e, Size maxSize)
        {
            var size = ComputeLayout(maxSize);

            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                var rect = GetItemCell(item.Item);

                var x = rect.Location.X;
                var y = rect.Location.Y - (mScrollBar?.Depth ?? 0);

                item.Item.Location = new Point(x, y);
                item.Item.Size = rect.Size;
                item.Item.Paint(e);
            }

            if (mScrollBar != null) 
            {
                mScrollBar.Location = new Point(mScrollBar.Host.Width - 1 - mScrollBar.Width, 0);
                mScrollBar.MaxDepth = size.Height;
                mScrollBar.Paint(e);
            }
        }

        private bool IsAutoRectangle(GridItem<T> item)
        {
            return Columns[item.Column].Type == GridType.Auto || Rows[item.Row].Type == GridType.Auto;
        }

        private Size ComputeSize(T item, Size maxSize)
        {
            return new Size(Math.Min(item.Size.Width, maxSize.Width), Math.Min(item.Size.Height, maxSize.Height));
        }
    }
}
