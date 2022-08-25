using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace addin.controls.renderer
{
    class BCustomDataGrid : BUIElement       
    {
        private int mColumnCount;
        private int mRowCount;
        private int mCustomColumnCount = -1;
        private int mMaxColumn;
        private int mDataLength;

        /// <summary>
        /// 最大偏移X
        /// </summary>
        public int MaxDepthX { get; set; }

        /// <summary>
        /// 最大偏移Y
        /// </summary>
        public int MaxDepthY { get; set; }

        /// <summary>
        /// 偏移X
        /// </summary>
        public int DepthX { get; set; }

        /// <summary>
        /// 偏移Y
        /// </summary>
        public int DepthY { get; set; }

        /// <summary>
        /// 获取或设置是否裁剪
        /// </summary>
        public bool DoClip { get; set; }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        public Func<int> GetDataLength { get; set; }

        /// <summary>
        /// 绘制数据函数
        /// DrawData(SpriteBatcher g, int x, int y, int index)
        /// </summary>
        public Func<Graphics, float, float, int, bool> PaintItem { get; set; }

        /// <summary>
        /// 数据绘制大小区域
        /// </summary>
        public Size ItemSize { get; set; }

        /// <summary>
        /// 物体偏移X
        /// </summary>
        public int ItemOffsetX { get; set; }

        /// <summary>
        /// 物体偏移Y
        /// </summary>
        public int ItemOffsetY { get; set; }

        /// <summary>
        /// 每行显示数量
        /// </summary>
        public int ColumnShowCount
        {
            get 
            {
                var dataSize = GetRealDataSize();

                return mCustomColumnCount == -1 ? (mDataLength == 0 ? 0 : (int)(Size.Width / dataSize.X)) : mCustomColumnCount; 
            }
            set 
            {
                mCustomColumnCount = value; 
            }
        }

        /// <summary>
        /// 缩放
        /// </summary>
        public PointF Scale { get; set; }

        public BCustomDataGrid(BControl host) 
            : base(host)
        {
            Scale = new PointF(1, 1);
            DoClip = true;
        }

        protected override void OnPaint(PaintEventArgs e)       
        {
            if (PaintItem != null)
            {
                var g = e.Graphics;
                var dataSize = GetRealDataSize();

                if (dataSize.X == 0 || dataSize.Y == 0) return;

                //var ox = (int)(Host.Location.X + Location.X);
                //var oy = (int)(Host.Location.Y + Location.Y);
                var ox = Location.X;
                var oy = Location.Y;

                // 取得绘制行列
                mDataLength = GetDataLength == null ? 0 : GetDataLength();
                mColumnCount = mDataLength == 0 ? 0 : (int)Math.Ceiling(Size.Width / dataSize.X);
                mRowCount = mDataLength == 0 ? 0 : (int)Math.Ceiling(Size.Height / dataSize.Y);

                var showCount = mColumnCount * mRowCount;
                mMaxColumn = ColumnShowCount;
                var maxRow = (int)(Math.Ceiling((float)mDataLength / mMaxColumn));

                // 取得最大拉伸范围
                MaxDepthX = Math.Max(0, (int)(dataSize.X * mMaxColumn - Size.Width));
                MaxDepthY = Math.Max(0, (int)(dataSize.Y * maxRow - Size.Height));

                var doClip = DoClip;
                if (doClip)
                    g.SetClip(new Rectangle(ox, oy, Size.Width, Size.Height));

                var depthX = DepthX - ItemOffsetX;
                var depthY = DepthY - ItemOffsetY;

                var startLine = (int)((depthY + ItemOffsetY) / dataSize.Y);
                var endLine = startLine + mRowCount + (depthY % dataSize.Y != 0 ? 1 : 0);
                var startRow = (int)((depthX + ItemOffsetX) / dataSize.X);
                var endRow = startRow + mColumnCount + (depthX % dataSize.X != 0 ? 1 : 0);
                var startIndex = startLine * mMaxColumn + startRow;
                for (var y2 = startLine; y2 < endLine; y2++)
                {
                    for (var x2 = startRow; x2 < endRow; x2++)
                    {
                        var index = y2 * mMaxColumn + x2;
                        var x = (int)(ox - depthX + x2 * dataSize.X) + ItemOffsetX;
                        var y = (int)(oy - depthY + y2 * dataSize.Y) + ItemOffsetY;
                        if (index < mDataLength)
                        {
                            if (!PaintItem(g, x, y, index)) 
                            {
                                goto PAINT_END;
                            }
                        }
                    }
                }

            PAINT_END:

                if (doClip)
                    g.ResetClip();
            }
        }

        public PointF GetRealDataSize()                         
        {
            return new PointF(ItemSize.Width * Scale.X, ItemSize.Height * Scale.Y);
        }

        public PointF[] GetDisplayRowLocations()                
        {
            var dataSize = GetRealDataSize();

            if (dataSize.X == 0 || dataSize.Y == 0 || mMaxColumn == 0) return new PointF[0];

            var result = new List<PointF>();

            //var ox = Host.Location.X + Location.X;
            //var oy = Host.Location.Y + Location.Y;
            var ox = Location.X;
            var oy = Location.Y;

            var depthX = DepthX - ItemOffsetX;
            var depthY = DepthY - ItemOffsetY;

            var startLine = (int)(depthY / dataSize.Y);
            var endLine = startLine + mRowCount + (depthY % dataSize.Y != 0 ? 1 : 0);
            for (var y2 = startLine; y2 < endLine; y2++)
            {
                var index = y2 * mMaxColumn + 0;
                var x = (int)(ox - depthX + 0 * dataSize.X);
                var y = (int)(oy - depthY + y2 * dataSize.Y);

                result.Add(new PointF(x, y));
            }

            return result.ToArray();
        }

        public PointF[] GetDisplayColumnLocations()             
        {
            var dataSize = GetRealDataSize();

            if (dataSize.X == 0 || dataSize.Y == 0 || mMaxColumn == 0) return new PointF[0];

            var result = new List<PointF>();

            //var ox = (int)(Host.Location.X + Location.X);
            //var oy = (int)(Host.Location.Y + Location.Y);
            var ox = Location.X;
            var oy = Location.Y;

            var depthX = DepthX - ItemOffsetX;
            var depthY = DepthY - ItemOffsetY;

            var startRow = (int)(depthX / dataSize.X);
            var endRow = startRow + mColumnCount + (depthX % dataSize.X != 0 ? 1 : 0);   
            for (var x2 = startRow; x2 < endRow; x2++)
            {
                var index = 0 * mMaxColumn + x2;
                var x = (int)(ox - depthX + x2 * dataSize.X);
                var y = (int)(oy - depthY + 0 * dataSize.Y);
                result.Add(new PointF(x, y));
            }

            return result.ToArray();
        }
    }
}
