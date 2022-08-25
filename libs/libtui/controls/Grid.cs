using libtui.drawing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace libtui.controls
{
    public class Grid : ContainerControlBase
    {
        private ScrollBar mScrollBar;
        private Dictionary<IControl, Rectangle> mItemCells;
        private Dictionary<IControl, RowColumn> mItemRowColumns;

        public List<GridLength> Rows { get; }

        public List<GridLength> Columns { get; }

        public List<IControl> Items { get; }

        public Grid()
        {
            Items = new List<IControl>();
            Rows = new List<GridLength>();
            Columns = new List<GridLength>();
            mItemCells = new Dictionary<IControl, Rectangle>();
            mItemRowColumns = new Dictionary<IControl, RowColumn>();

            mScrollBar = new ScrollBar();
            mScrollBar.AutoHide = true;
        }

        public Rectangle GetItemCell(IControl item)
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
                var rc = mItemRowColumns[item];
                if (Rows[rc.Row].Type == GridType.Auto)
                    Rows[rc.Row] = new GridLength(GridType.Auto, 0);

                if (Columns[rc.Column].Type == GridType.Auto)
                    Columns[rc.Column] = new GridLength(GridType.Auto, 0);
            }

            foreach (var item in autoItems)
            {
                var rc = mItemRowColumns[item];
                var size = ComputeSize(item, maxSize);

                if (Rows[rc.Row].Type == GridType.Auto)
                    Rows[rc.Row] = new GridLength(GridType.Auto, size.Height);

                if (Columns[rc.Column].Type == GridType.Auto)
                    Columns[rc.Column] = new GridLength(GridType.Auto, size.Width);
            }

            var rects = GetRectangles(maxSize);
            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                var rc = mItemRowColumns[item];
                var rect = rects[rc.Column + rc.Row * Columns.Count];
                mItemCells[item] = rect;
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
                var rect = GetItemCell(item);

                var x = rect.Location.X;
                var y = rect.Location.Y - (mScrollBar?.Depth ?? 0);

                item.Location = new Point(x, y);
                item.Size = rect.Size;
                item.Paint(e);
            }

            if (mScrollBar != null)
            {
                mScrollBar.Location = new Point(Size.Width - 1 - mScrollBar.Width, 0);
                mScrollBar.MaxDepth = size.Height;
                mScrollBar.Paint(e);
            }
        }

        private bool IsAutoRectangle(IControl item)
        {
            var rc = mItemRowColumns[item];
            return Columns[rc.Column].Type == GridType.Auto || Rows[rc.Row].Type == GridType.Auto;
        }

        private Size ComputeSize(IControl item, Size maxSize)
        {
            return new Size(Math.Min(item.Size.Width, maxSize.Width), Math.Min(item.Size.Height, maxSize.Height));
        }

        struct RowColumn
        {
            public int Column;
            public int Row;
        }
    }
}
