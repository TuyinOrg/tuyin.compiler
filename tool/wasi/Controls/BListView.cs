using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace addin.controls.renderer
{
    public enum BListViewItemState 
    {
        Normal,
        Hold,
        Pressed,
        Selected,
    }

    public class BListViewEventArgs : EventArgs
    {
        public IBListViewItem Item { get; private set; }

        public BListViewEventArgs(IBListViewItem item)
        {
            Item = item;
        }
    }

    public class BListViewDragDropEventArgs : MouseEventArgs
    {
        public IBListViewItem Node { get; private set; }
        public IDataObject Data { get; private set; }

        public BListViewDragDropEventArgs(IBListViewItem node, IDataObject data, MouseButtons button, int clicks, int x, int y)
            : base(button, clicks, x, y, 0)
        {
            Node = node;
            Data = data;
        }
    }

    public class BListViewDoDragEventArgs : MouseEventArgs
    {
        public IBListViewItem[] Nodes { get; private set; }

        public BListViewDoDragEventArgs(IBListViewItem[] nodes, MouseButtons button, int clicks, int x, int y) 
            : base(button, clicks, x, y, 0)                                             
        {
            Nodes = nodes;
        }
    }

    public class BListViewEventArgs<T> : BListViewEventArgs
    {
        public T Arg { get; set; }

        public BListViewEventArgs(T arg, IBListViewItem item)
            : base(item)
        {
            Arg = arg;
        }
    }

    public interface IBListViewItem : IUIElment
    {
        BListViewItemState State { get; set; }
    }

    public class BListViewItem : IBListViewItem
    {
        public virtual bool Enabled
        {
            get;
            set;
        }

        public virtual Point Location
        {
            get;
            set;
        }

        public virtual Padding Padding
        {
            get;
            set;
        }

        public virtual Size Size
        {
            get;
            set;
        }

        public virtual BListViewItemState State
        {
            get;
            set;
        }

        public virtual void Paint(PaintEventArgs e)
        {

        }
    }

    /// <summary>
    /// 数据列表显示
    /// </summary>
    public class BListView : BUIElement
    {
        private bool mIsShiftPressed;
        private bool mIsLeftButtonDown;
        private BScrollBar mScrollBar;
        private Point mDownPoint;
        private Timer mTimer;
        private IBListViewItem mDropTargetNode;
        private IBListViewItem[] mDropTreeNodes;
        private DragEventArgs mDragEventArgs;
        private WinFormInputHelper<IBListViewItem> mInputHelper;

        public List<IBListViewItem> Items                                               
        {
            get;
            set;
        }

        public Size ItemSize                                                            
        {
            get;
            set;
        }

        public Padding ItemPadding                                                      
        {
            get;
            set;
        }

        public bool IsHorizontal                                                        
        {
            get;
            set;
        }

        public int LineMaxCount                                                         
        {
            get;
            set;
        }

        public bool MultipleSelect                                                      
        {
            get;
            set;
        }

        public bool AllowItemDrop                                                       
        {
            get;
            set;
        }

        public bool AllowItemDrag                                                       
        {
            get;
            set;
        }

        public BScrollBar ScrollBar                                                     
        {
            get { return mScrollBar; }
        }

        public IEnumerable<IBListViewItem> GetSelected()                                
        {
            return Items.Where(X => X.State == BListViewItemState.Selected);
        }

        public BListView(IBControl host)                                                 
            : base(host)                                                                
        {
            mScrollBar = new BScrollBar(host);
            Items = new List<IBListViewItem>();
            mInputHelper = new WinFormInputHelper<IBListViewItem>(host);

            ItemSize = new Size(199, 120);
            ItemPadding = new Padding(2);
            //Padding = new Padding(3, 3, 3, 3);

            Host.KeyDown += Host_KeyDown;
            Host.KeyUp += Host_KeyUp;
            Host.MouseDown += Host_MouseDown;
            Host.MouseMove += Host_MouseMove;
            Host.DragOver += Host_DragOver;
            Host.DragDrop += Host_DragDrop;
            Host.DragEnter += Host_DragEnter;
            Host.Disposed += Host_Disposed;
            mInputHelper.MouseEnter += mInputHelper_MouseEnter;
            mInputHelper.MouseLeave += mInputHelper_MouseLeave;
            mInputHelper.MouseClick += mInputHelper_MouseClick;
            mInputHelper.MouseDown += mInputHelper_MouseDown;
            mInputHelper.MouseUp += mInputHelper_MouseUp;

            mTimer = new Timer();
            mTimer.Interval = 100;
            mTimer.Tick += mTimer_Tick;
        }

        private void Host_Disposed(object sender, EventArgs e)
        {
            mDragEventArgs = null;
            mDropTreeNodes = null;
            mDropTargetNode = null;
            mTimer.Stop();
        }

        private void Host_MouseDown(object sender, MouseEventArgs e)
        {
            mDownPoint = e.Location;

            mDragEventArgs = null;
            mDropTreeNodes = null;
            mDropTargetNode = null;
            mTimer.Stop();
        }

        private void Host_DragEnter(object sender, DragEventArgs e)
        {
            mDragEventArgs = e;
        }

        public IBListViewItem GetItemAt(Point point)                                    
        {
            var drawPoint = new Point(
                Host.Padding.Left + Location.X + Padding.Left,
                Host.Padding.Top + Location.Y + Padding.Top);

            // 得到工作大小
            var size = new Size(
                Size.Width - Padding.Left - Padding.Right,
                Size.Height - Padding.Top - Padding.Bottom);

            var itemSize = new Size(
                Math.Min(ItemSize.Width, size.Width),
                Math.Min(ItemSize.Height, size.Height));


            // 首先将物体分成若干行列
            var maxRowNumber = IsHorizontal ? (size.Height - (mScrollBar.Visible ? mScrollBar.Width : 0)) / itemSize.Height : (int)Math.Ceiling((float)Items.Count / (size.Width / itemSize.Width));
            var maxColumnNumber = IsHorizontal ? (int)Math.Ceiling((float)Items.Count / (size.Height / itemSize.Height)) : size.Width / itemSize.Width;

            // 绘制物体集
            var startShowRowNumber = (IsHorizontal ? 0 : mScrollBar.Depth / itemSize.Height);
            var startShowColumnNumber = (IsHorizontal ? mScrollBar.Depth / itemSize.Width : 0);
            var showRowNumber = IsHorizontal ? startShowRowNumber + maxRowNumber : Math.Min(maxRowNumber, startShowRowNumber + (int)Math.Ceiling((float)size.Height / itemSize.Height) + 1);
            var showColumnNumber = IsHorizontal ? Math.Min(maxColumnNumber, startShowColumnNumber + (int)Math.Ceiling((float)size.Width / itemSize.Width) + 1) : maxColumnNumber;

            for (var y = startShowRowNumber; y < showRowNumber; y++)
            {
                for (var x = startShowColumnNumber; x < showColumnNumber; x++)
                {
                    var index = IsHorizontal ? y + x * maxRowNumber : x + y * maxColumnNumber;
                    if (index < Items.Count)
                    {
                        var item = Items[index];

                        var location = new Point(
                            drawPoint.X + x * itemSize.Width + ItemPadding.Left + (IsHorizontal ? -mScrollBar.Depth : 0),
                            drawPoint.Y + y * itemSize.Height + ItemPadding.Top + (IsHorizontal ? 0 : -mScrollBar.Depth));

                        var size2 = new Size(
                            itemSize.Width - ItemPadding.Left - ItemPadding.Right - (IsHorizontal && mScrollBar.Visible ? 0 : mScrollBar.Width),
                            itemSize.Height - ItemPadding.Top - ItemPadding.Bottom);

                        if (new Rectangle(location, size2).Contains(point))
                        {
                            return item;
                        }
                    }
                }
            }

            return null;
        }

        private void Host_DragDrop(object sender, DragEventArgs e)                      
        {
            if (Host.AllowSelectRegion && AllowItemDrag && mDragEventArgs != null)
            {
                if (NodeDragDrop != null && mDropTargetNode != null)
                {
                    NodeDragDrop(this,
                        new BListViewDragDropEventArgs(
                            mDropTargetNode,
                            mDragEventArgs.Data,
                            MouseButtons.Left,
                            0,
                            e.X,
                            e.Y));
                }
            }

            mDragEventArgs = null;
            mDropTreeNodes = null;
            mDropTargetNode = null;
            mTimer.Stop();
        }

        private void Host_DragOver(object sender, DragEventArgs e)                      
        {
            Point pt = Host.PointToClient(Control.MousePosition);
            pt = new Point(pt.X - Location.X, pt.Y - Location.Y);

            IBListViewItem node = GetItemAt(pt);

            if (node != null)
            {
                if (mDropTargetNode != node)
                {
                    mDropTargetNode = node;
                }

                Host.Invalidate();
            }
        }

        private void mTimer_Tick(object sender, EventArgs e)                            
        {
            const int SCROLL_LENGTH = 30;

            Point pt = Host.PointToClient(Control.MousePosition);
            pt = new Point(
                    pt.X - Location.X - Padding.Left,
                    pt.Y - Location.Y - Padding.Top);

            var height = Host.Height - Location.Y - Padding.Top;
            var width = Host.Width - Location.X - Padding.Left;
            // if mouse is near to the top, scroll up
            if (pt.Y < SCROLL_LENGTH && ScrollBar.Depth > 0)
            {
                ScrollBar.Depth -= (int)Math.Ceiling((SCROLL_LENGTH - pt.Y) / 1.5f);
                if (ScrollBar.Depth < 0)
                    ScrollBar.Depth = 0;
            }
            // if mouse is near to the bottom, scroll down
            else if ((!IsHorizontal ? pt.Y > height - SCROLL_LENGTH : pt.X > width - SCROLL_LENGTH) && ScrollBar.Depth < ScrollBar.DropDepth)
            {
                ScrollBar.Depth += (int)Math.Ceiling((SCROLL_LENGTH - (!IsHorizontal ? height - pt.Y : width - pt.X)) / 1.5f);
                if (ScrollBar.Depth > ScrollBar.DropDepth)
                    ScrollBar.Depth = ScrollBar.DropDepth;
            }
        }

        void Host_MouseMove(object sender, MouseEventArgs e)                            
        {
            if (e.Button == MouseButtons.Left &&
                         mDropTreeNodes == null &&
                         Host.AllowSelectRegion &&
                         AllowItemDrag &&
                         GetSelected().Count() > 0 &&
                         Math.Abs(mDownPoint.Y - e.Location.Y) > 5)
            {
                mDropTreeNodes = GetSelected().ToArray();
                mTimer.Start();

                NodeDoDrag?.Invoke(this, new BListViewDoDragEventArgs(
                    mDropTreeNodes,
                    MouseButtons.Left,
                    0,
                    e.X,
                    e.Y));

                Host.DoDragDrop(mDropTreeNodes, DragDropEffects.Move);
            }
        }

        void Host_KeyUp(object sender, KeyEventArgs e)                                  
        {
            mIsShiftPressed = e.Shift;
        }

        void Host_KeyDown(object sender, KeyEventArgs e)                                
        {
            mIsShiftPressed = e.Shift;
        }

        void mInputHelper_MouseUp(WinFormMouseInputEventArgs<IBListViewItem> args)      
        {
            mIsLeftButtonDown = false;

            if (args.Args.Button == MouseButtons.Left)
            {
                if (MultipleSelect && mIsShiftPressed)
                {
                    if (args.Area.Tag.State == BListViewItemState.Selected)
                    {
                        args.Area.Tag.State = BListViewItemState.Hold;
                    }
                    else
                    {
                        args.Area.Tag.State = BListViewItemState.Selected;
                    }
                }
                else
                {
                    GetSelected().Do(X => X.State = BListViewItemState.Normal);
                    args.Area.Tag.State = BListViewItemState.Selected;
                }
            }
            else if (args.Args.Button == MouseButtons.Right)
            {
                if (!GetSelected().Contains(args.Area.Tag))
                {
                    GetSelected().Do(X => X.State = BListViewItemState.Normal);
                    args.Area.Tag.State = BListViewItemState.Selected;
                }
            }


            if (ItemMouseUp != null)
                ItemMouseUp(this, new BListViewEventArgs<MouseEventArgs>(args.Args, args.Area.Tag));
        }

        void mInputHelper_MouseDown(WinFormMouseInputEventArgs<IBListViewItem> args)    
        {
            mIsLeftButtonDown = args.Args.Button == MouseButtons.Left;

            if (ItemMouseDown != null)
                ItemMouseDown(this, new BListViewEventArgs<MouseEventArgs>(args.Args, args.Area.Tag));
        }

        void mInputHelper_MouseLeave(WinFormMouseInputEventArgs<IBListViewItem> args)   
        {
            if (args.Area.Tag.State != BListViewItemState.Selected)
                args.Area.Tag.State = BListViewItemState.Normal;

            ItemMouseLeave?.Invoke(this, new BListViewEventArgs<MouseEventArgs>(args.Args, args.Area.Tag));

            Host.Invalidate();
        }

        void mInputHelper_MouseEnter(WinFormMouseInputEventArgs<IBListViewItem> args)   
        {
            if (args.Area.Tag.State != BListViewItemState.Selected)
                args.Area.Tag.State = BListViewItemState.Hold;

            ItemMouseEnter?.Invoke(this, new BListViewEventArgs<MouseEventArgs>(args.Args, args.Area.Tag));

            Host.Invalidate();
        }

        void mInputHelper_MouseClick(WinFormMouseInputEventArgs<IBListViewItem> args)   
        {
            if (ItemMouseClick != null)
                ItemMouseClick(this, new BListViewEventArgs(args.Area.Tag));

            Host.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)                               
        {
            mInputHelper.ClearRegions();

            var drawPoint = new Point(
                Host.Padding.Left + Location.X + Padding.Left,
                Host.Padding.Top + Location.Y + Padding.Top);

            // 得到工作大小
            var size = new Size(
                Size.Width - Padding.Left - Padding.Right,
                Size.Height - Padding.Top - Padding.Bottom);

            var itemSize = new Size(
                Math.Min(ItemSize.Width, size.Width),
                Math.Min(ItemSize.Height, size.Height));

            e.Graphics.Clip = new Region(new Rectangle(drawPoint, size));

            // 首先将物体分成若干行列
            var maxRowNumber = IsHorizontal ? (size.Height - (mScrollBar.Visible ? mScrollBar.Width : 0)) / itemSize.Height : (int)Math.Ceiling((float)Items.Count / (size.Width / itemSize.Width));
            var maxColumnNumber = IsHorizontal ? (int)Math.Ceiling((float)Items.Count / (size.Height / itemSize.Height)) : size.Width / itemSize.Width;

            // 绘制物体集
            var startShowRowNumber = (IsHorizontal ? 0 : mScrollBar.Depth / itemSize.Height);
            var startShowColumnNumber = (IsHorizontal ? mScrollBar.Depth / itemSize.Width : 0);
            var showRowNumber = IsHorizontal ? startShowRowNumber + maxRowNumber : Math.Min(maxRowNumber, startShowRowNumber + (int)Math.Ceiling((float)size.Height / itemSize.Height) + 1);
            var showColumnNumber = IsHorizontal ? Math.Min(maxColumnNumber, startShowColumnNumber + (int)Math.Ceiling((float)size.Width / itemSize.Width) + 1) : maxColumnNumber;

            for (var y = startShowRowNumber; y < showRowNumber; y++)
            {
                for (var x = startShowColumnNumber; x < showColumnNumber; x++)
                {
                    var index = IsHorizontal ? y + x * maxRowNumber : x + y * maxColumnNumber;
                    if (index < Items.Count)
                    {
                        var item = Items[index];
                        var isdroptarget = mDropTargetNode == item && Host.AllowSelectRegion && AllowItemDrop && mDragEventArgs != null;

                        item.Location = new Point(
                            drawPoint.X + x * itemSize.Width + ItemPadding.Left + (IsHorizontal ? -mScrollBar.Depth : 0),
                            drawPoint.Y + y * itemSize.Height + ItemPadding.Top + (IsHorizontal ? 0 : -mScrollBar.Depth));

                        item.Size = new Size(
                            itemSize.Width - ItemPadding.Left - ItemPadding.Right, // - (IsHorizontal && mScrollBar.Visible ? 0 : mScrollBar.Width),
                            itemSize.Height - ItemPadding.Top - ItemPadding.Bottom);

                        mInputHelper.AddRegion(item, new Rectangle(item.Location, item.Size));


                        if (isdroptarget)
                        {
                            var whiteBrush = new SolidBrush(Color.FromArgb(222, 222, 222));
                            e.Graphics.FillRectangle(whiteBrush, new Rectangle(item.Location, item.Size));
                        }

                        item.Paint(e);
                    }
                }
            }
            

            e.Graphics.ResetClip();

            // 绘制滚动条
            mScrollBar.IsHorizontal = IsHorizontal;
            mScrollBar.Location = new Point(
                IsHorizontal ? drawPoint.X : drawPoint.X + size.Width - mScrollBar.Width,
                IsHorizontal ? drawPoint.Y + size.Height - mScrollBar.Width : drawPoint.Y);

            mScrollBar.Length = IsHorizontal ?
                size.Width - 2 : size.Height - 2;

            mScrollBar.MaxDepth = IsHorizontal ? maxColumnNumber * itemSize.Width : maxRowNumber * itemSize.Height;

            mScrollBar.Paint(e); 
        }

        public event EventHandler<BListViewEventArgs> ItemMouseClick;
        public event EventHandler<BListViewEventArgs> ItemMouseEnter;
        public event EventHandler<BListViewEventArgs> ItemMouseLeave;
        public event EventHandler<BListViewEventArgs<MouseEventArgs>> ItemMouseDown;
        public event EventHandler<BListViewEventArgs<MouseEventArgs>> ItemMouseUp;

        public event EventHandler<BListViewDoDragEventArgs> NodeDoDrag;
        public event EventHandler<BListViewDragDropEventArgs> NodeDragDrop;
    }
}
