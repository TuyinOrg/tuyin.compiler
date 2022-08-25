using addin;
using addin.common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace addin.controls.renderer
{
    public class BTreeNodeDragDropEventArgs : MouseEventArgs
    {
        public IBTreeNode Node { get; private set; }
        public IDataObject Data { get; private set; }

        public BTreeNodeDragDropEventArgs(IBTreeNode node, IDataObject data, MouseButtons button, int clicks, int x, int y)
            : base(button, clicks, x, y, 0)
        {
            Node = node;
            Data = data;
        }
    }

    public class BTreeNodeDoDragEventArgs : MouseEventArgs
    {
        public IBTreeNode[] Nodes { get; private set; }

        public BTreeNodeDoDragEventArgs(IBTreeNode[] nodes, MouseButtons button, int clicks, int x, int y)
            : base(button, clicks, x, y, 0)
        {
            Nodes = nodes;
        }
    }

    public class BTreeNodeDragEnterEventArgs : MouseEventArgs
    {
        public IBTreeNode Node { get; private set; }
        public IDataObject Data { get; private set; }
        public DragDropEffects Effect { get; set; }

        public BTreeNodeDragEnterEventArgs(IBTreeNode node, IDataObject data, MouseButtons button, int clicks, int x, int y)
            : base(button, clicks, x, y, 0)
        {
            Effect = DragDropEffects.None;
            Node = node;
            Data = data;
        }
    }

    public class BTreeNodeDragLeaveEventArgs : MouseEventArgs
    {
        public BTreeNodeDragLeaveEventArgs(MouseButtons button, int clicks, int x, int y)
            : base(button, clicks, x, y, 0)
        {
        }
    }

    public class BTreeNodeMouseEventArgs : MouseEventArgs
    {
        public IBTreeNode Node { get; private set; }

        public BTreeNodeMouseEventArgs(IBTreeNode node, MouseButtons button, int clicks, int x, int y)
            : base(button, clicks, x, y, 0)
        {
            Node = node;
        }
    }

    public interface IBTreeNode : IUIElment
    {
        Color HoverBackColor { get; set; }
        Color NormalBackColor { get; set; }
        Color SelectedBackColor { get; set; }

        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        bool IsVisual { get; set; }
        bool IsVisualButton { get; }

        bool AllowSelect { get; }
        IEnumerable<IBTreeNode> Nodes { get; }
        object Tag { get; set; }

        void OnClosed();
        void OnExpanded();
        void OnClick();
        void OnDobuleClick();
    }

    public class BTreeNode : IBTreeNode
    {
        private bool mIsExpanded;
        private List<IBTreeNode> mNodes;

        public virtual List<IBTreeNode> Items
        {
            get { return mNodes; }
        }

        public Color HoverBackColor
        {
            get;
            set;
        }

        public Color SelectedBackColor
        {
            get;
            set;
        }

        public Color SelectedButtonColor
        {
            get;
            set;
        }

        public virtual bool IsExpanded
        {
            get { return mIsExpanded; }
            set
            {
                if (mIsExpanded != value)
                {
                    mIsExpanded = value;
                    if (mIsExpanded)
                    {
                        OnExpanded();
                    }
                    else
                    {
                        OnClosed();
                    }
                }
            }
        }

        public virtual bool IsSelected
        {
            get;
            set;
        }

        public virtual IEnumerable<IBTreeNode> Nodes
        {
            get { return mNodes; }
        }

        public List<IBTreeNode> Childrens
        {
            get { return mNodes; }
        }

        public Image Icon
        {
            get;
            set;
        }

        public string Text
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

        public Object Tag
        {
            get;
            set;
        }

        public Font Font
        {
            get;
            set;
        }

        public Color ForeColor
        {
            get;
            set;
        }

        public BTreeNode()
        {
            mNodes = new List<IBTreeNode>();
            IsVisual = true;
            IsVisualButton = true;
            Size = new Size(0, 20);
            NormalBackColor = Color.Transparent;
            Font = IDE.AppConfig.Skin.DefaultFont;
            SelectedBackColor = Color.FromArgb(63, 63, 70);
            ForeColor = IDE.AppConfig.Skin.ForeColor;
        }

        public virtual void Paint(PaintEventArgs e)
        {
            var startX = Location.X;
            var startY = Location.Y;
            startX = startX + 2;

            if (Icon != null)
            {
                e.Graphics.DrawImage(Icon, startX, startY + (Size.Height - Icon.Height) / 2);

                startX += 16 + 2;
            }

            // 绘制文字
            var textFont = IsSelected ? new Font(Font, FontStyle.Bold) : Font;
            var text = Text;
            var textSize = e.Graphics.MeasureString(
                text,
                textFont);

            e.Graphics.DrawString(
                text,
                textFont,
                new SolidBrush(ForeColor),
                new PointF(startX, startY + (Size.Height - textSize.Height) / 2 + 1));

            Size = new Size(26 + (int)Math.Ceiling(textSize.Width), Size.Height);
        }

        public bool IsVisualButton
        {
            get;
            set;
        }

        public Color NormalBackColor
        {
            get;
            set;
        }

        public virtual bool AllowSelect
        {
            get;
            set;
        }

        public bool IsVisual
        {
            get;
            set;
        }

        public virtual void OnDobuleClick()
        {
        }

        public virtual void OnClick()
        {
        }

        public virtual void OnClosed()
        {
            foreach (var item in Items) item.OnClosed();
        }

        public virtual void OnExpanded()
        {
            foreach (var item in Items) item.OnExpanded();
        }
    }


    public abstract class BTreeNodeBase<T> : IBTreeNode
    {
        private bool mIsExpanded;

        public abstract IList<T> Items { get; }

        public Color HoverBackColor
        {
            get;
            set;
        }

        public Color SelectedBackColor
        {
            get;
            set;
        }

        public Color SelectedButtonColor
        {
            get;
            set;
        }

        public virtual bool IsExpanded
        {
            get { return mIsExpanded; }
            set
            {
                if (mIsExpanded != value)
                {
                    mIsExpanded = value;
                    if (mIsExpanded)
                    {
                        OnExpanded();
                    }
                    else
                    {
                        OnClosed();
                    }
                }
            }
        }

        public virtual bool IsSelected
        {
            get;
            set;
        }

        public virtual IEnumerable<IBTreeNode> Nodes => Items.Cast<IBTreeNode>();

        public Image Icon
        {
            get;
            set;
        }

        public string Text
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

        public Object Tag
        {
            get;
            set;
        }

        public Font Font
        {
            get;
            set;
        }

        public Color ForeColor
        {
            get;
            set;
        }

        public BTreeNodeBase()
        {
            IsVisual = true;
            IsVisualButton = true;
            Size = new Size(0, 20);
            NormalBackColor = Color.Transparent;
            Font = IDE.AppConfig.Skin.DefaultFont;
            SelectedBackColor = Color.FromArgb(63, 63, 70);
            ForeColor = IDE.AppConfig.Skin.ForeColor;
        }

        public virtual void Paint(PaintEventArgs e)
        {
            var startX = Location.X;
            var startY = Location.Y;
            startX = startX + 2;

            if (Icon != null)
            {
                e.Graphics.DrawImage(Icon, startX, startY + (Size.Height - 16) / 2, 16, 16);

                startX += 16 + 2;
            }

            // 绘制文字
            var textFont = IsSelected ? new Font(Font, FontStyle.Bold) : Font;
            var text = Text;
            var textSize = e.Graphics.MeasureString(
                text,
                textFont);

            e.Graphics.DrawString(
                text,
                textFont,
                new SolidBrush(ForeColor),
                new PointF(startX, startY + (Size.Height - textSize.Height) / 2));

            Size = new Size(26 + (int)Math.Ceiling(textSize.Width), Size.Height);
        }

        public virtual bool IsVisualButton
        {
            get;
            set;
        }

        public Color NormalBackColor
        {
            get;
            set;
        }

        public virtual bool AllowSelect
        {
            get;
            set;
        }

        public bool IsVisual
        {
            get;
            set;
        }

        public virtual void OnDobuleClick()
        {
        }

        public virtual void OnClick()
        {
        }

        public virtual void OnClosed()
        {
            foreach (var item in Nodes) item.OnClosed();
        }

        public virtual void OnExpanded()
        {
            foreach (var item in Nodes) item.OnExpanded();
        }
    }

    public class BTreeView : BUIElement
    {
        private bool mInit;
        private IBTreeNode mHoverNode;
        private IBTreeNode mEnterNode;
        private IBTreeNode mSelectedNode;
        private IBTreeNode mDropTargetNode;

        private BScrollBar mVScrollBar;
        private BScrollBar mHScrollBar;

        private IBTreeNode[] mDropTreeNodes;

        private Size mSize;
        private Point mLocation;
        private Point mDownPoint;
        private Timer mTimer;
        private DragEventArgs mDragEventArgs;

        private WinFormInputHelper<IBTreeNode> mButtonInputHelper;
        private WinFormInputHelper<IBTreeNode> mNodeInputHelper;

        public IBTreeNode TopNode { get; set; }
        public bool EnableNodeEvents { get; set; }
        public bool MultiSelect { get; set; }
        public bool AllowDrop { get; set; }
        public bool AllowItemDrop { get; set; }
        public bool AllowItemDrag { get; set; }
        public bool ShowTop { get; set; }
        public Padding ItemPadding { get; set; }
        public Color ButtonColor { get; set; }
        public Color SelectedButtonColor { get; set; }
        public Color? DisenableItemBackground { get; set; }
        public BScrollBar VScrollBar { get { return mVScrollBar; } }
        public BScrollBar HScrollBar { get { return mHScrollBar; } }

        public IEnumerable<IBTreeNode> SelectedNodes
        {
            get { return GetNodes(TopNode).Where(X => X != null && X.IsSelected); }
        }

        public IBTreeNode HoverNode
        {
            get { return mHoverNode; }
        }

        public override Point Location
        {
            get { return mLocation; }
            set 
            {
                if (mLocation != value) 
                {
                    mLocation = value;
                    HScrollBar.Location = new Point(value.X, value.Y + Size.Height - HScrollBar.Width);
                    VScrollBar.Location = new Point(value.X + Size.Width - VScrollBar.Width, value.Y);
                }
            }
        }

        public override Size Size 
        {
            get => mSize;
            set
            {
                if (mSize != value)
                {
                    mSize = value;
                    HScrollBar.Location = new Point(Location.X, Location.Y + mSize.Height - HScrollBar.Width);
                    VScrollBar.Location = new Point(Location.X + mSize.Width - VScrollBar.Width, Location.Y);
                    HScrollBar.Length = mSize.Width - VScrollBar.Width;
                    VScrollBar.Length = mSize.Height - HScrollBar.Width;
                }
            }
        }

        public int DrawIndex 
        { 
            get; 
            private set;
        }

        public BTreeView(IBControl host)
            : base(host)
        {
            ShowTop = true;
            EnableNodeEvents = true;

            mVScrollBar = new BScrollBar(host);
            mHScrollBar = new BScrollBar(host);
            mHScrollBar.IsHorizontal = true;
            mHScrollBar.InputControl = false;

            mButtonInputHelper = new WinFormInputHelper<IBTreeNode>(host);
            mNodeInputHelper = new WinFormInputHelper<IBTreeNode>(host);

            mNodeInputHelper.MouseDown += mNodeInputHelper_MouseDown;
            mNodeInputHelper.MouseUp += mNodeInputHelper_MouseUp;
            mNodeInputHelper.MouseEnter += mNodeInputHelper_MouseEnter;
            mNodeInputHelper.MouseLeave += mNodeInputHelper_MouseLeave;
            mNodeInputHelper.MouseClick += mNodeInputHelper_MouseClick;
            mNodeInputHelper.MouseDoubleClick += mNodeInputHelper_MouseDoubleClick;
            mButtonInputHelper.MouseDown += mButtonInputHelper_MouseDown;
            mButtonInputHelper.MouseEnter += mButtonInputHelper_MouseEnter;
            mButtonInputHelper.MouseLeave += mButtonInputHelper_MouseLeave;

            Host.MouseDown += Host_MouseDown;
            Host.MouseMove += Host_MouseMove;
            Host.DragDrop += Host_DragDrop;
            Host.DragOver += Host_DragOver;
            Host.DragEnter += Host_DragEnter;
            Host.LostFocus += Host_LostFocus;

            ItemPadding = new Padding(16, 0, 0, 0);

            ButtonColor = IDE.AppConfig.Skin.BackColor;
            //SelectedButtonColor = Color.Transparent;
            DisenableItemBackground = IDE.AppConfig.Skin.InvertColor;

            mTimer = new Timer();
            mTimer.Interval = 100;
            mTimer.Tick += mTimer_Tick;

            HScrollBar.Width = 12;
            HScrollBar.EnableScrollButton = false;
            HScrollBar.RoundScrollButton = false;

            VScrollBar.Width = 12;
            VScrollBar.EnableScrollButton = false;
            VScrollBar.RoundScrollButton = false;

            SelectedButtonColor = Color.White;
            ButtonColor = Color.FromArgb(
                IDE.AppConfig.Skin.ForeColor.A,
                Math.Max(158, IDE.AppConfig.Skin.ForeColor.R - 38),
                Math.Max(158, IDE.AppConfig.Skin.ForeColor.G - 38),
                Math.Max(158, IDE.AppConfig.Skin.ForeColor.B - 38));
        }

        public void ClearSelected()
        {
            GetNodes(TopNode).Do(X => X.IsSelected = false);
        }

        private void mNodeInputHelper_MouseClick(WinFormMouseInputEventArgs<IBTreeNode> args)
        {
            var clickNode = args.Area.Tag;
            if (clickNode != null)
            {
                clickNode.OnClick();

                if (NodeClick != null)
                    NodeClick(this, new BTreeNodeMouseEventArgs(
                        clickNode, args.Args.Button, args.Args.Clicks, args.Args.X, args.Args.Y));

                Host.Invalidate();
            }
        }

        void mTimer_Tick(object sender, EventArgs e)
        {
            var scrollLength = Math.Min(30, mVScrollBar.ButtonOrWheelScrollLength);

            Point pt = Host.PointToClient(Control.MousePosition);
            pt = new Point(
                    pt.X - Location.X - Padding.Left,
                    pt.Y - Location.Y - Padding.Top);

            var height = Host.Height - Location.Y - Padding.Top;
            // if mouse is near to the top, scroll up
            if (pt.Y < scrollLength && mVScrollBar.Depth > 0)
            {
                mVScrollBar.Depth -= (int)Math.Ceiling((scrollLength - pt.Y) / 1.5f);
                if (mVScrollBar.Depth < 0)
                    mVScrollBar.Depth = 0;
            }
            // if mouse is near to the bottom, scroll down
            else if (pt.Y > height - scrollLength && mVScrollBar.Depth < mVScrollBar.DropDepth)
            {
                mVScrollBar.Depth += (int)Math.Ceiling((scrollLength - (height - pt.Y)) / 1.5f);
                if (mVScrollBar.Depth > mVScrollBar.DropDepth)
                    mVScrollBar.Depth = mVScrollBar.DropDepth;
            }
        }

        void Host_LostFocus(object sender, EventArgs e)
        {
            Host.Invalidate();
        }

        void Host_DragOver(object sender, DragEventArgs e)
        {
            Point pt = Host.PointToClient(Control.MousePosition);
            pt = new Point(pt.X - Location.X, pt.Y - Location.Y);

            IBTreeNode node = GetNodeAt(pt);

            if (node != null)
            {
                if (node.AllowSelect && mDropTargetNode != node)
                {
                    mDropTargetNode = node;
                }

                Host.Invalidate();
            }
        }

        void Host_DragDrop(object sender, DragEventArgs e)
        {
            if (Host.AllowSelectRegion && AllowItemDrop && mDragEventArgs != null)
            {
                if (NodeDragDrop != null && mDropTargetNode != null)
                {
                    NodeDragDrop(this,
                        new BTreeNodeDragDropEventArgs(
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

        void Host_DragEnter(object sender, DragEventArgs e)
        {
            mDragEventArgs = e;
        }

        void Host_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mInit)
            {
                Host.Invalidate();
                mInit = true;
            }

            if (e.Button == MouseButtons.Left &&
                mDropTreeNodes == null &&
                Host.AllowSelectRegion &&
                AllowItemDrag &&
                mSelectedNode != null &&
                Math.Abs(mDownPoint.Y - e.Location.Y) > 5)
            {
                mDropTreeNodes = SelectedNodes.ToArray();
                mTimer.Start();

                NodeDoDrag?.Invoke(this, new BTreeNodeDoDragEventArgs(
                    mDropTreeNodes,
                    MouseButtons.Left,
                    0,
                    e.X,
                    e.Y));

                Host.DoDragDrop(mDropTreeNodes, DragDropEffects.Move);
            }
        }

        void Host_MouseDown(object sender, MouseEventArgs e)
        {
            mDownPoint = e.Location;

            mDragEventArgs = null;
            mDropTreeNodes = null;
            mDropTargetNode = null;
            mTimer.Stop();
        }

        void mNodeInputHelper_MouseDown(WinFormMouseInputEventArgs<IBTreeNode> args)
        {
            if (!args.Area.Tag.AllowSelect) return;

            List<IBTreeNode> selectNodes = new List<IBTreeNode>();

            var isShift = Keyboard.IsKeyDown(Keys.LShiftKey) | Keyboard.IsKeyDown(Keys.RShiftKey);
            var isCtrl = Keyboard.IsKeyDown(Keys.LControlKey) | Keyboard.IsKeyDown(Keys.RControlKey);
            if (MultiSelect && (isCtrl || isShift))
            {
                if (isShift)
                {
                    // 添加最后选择到当前点击位置
                    if (mSelectedNode != null && mSelectedNode != args.Area.Tag)
                    {
                        IBTreeNode start = null;
                        foreach (var node in GetVisualNodes(TopNode))
                        {
                            if (node == mSelectedNode || node == args.Area.Tag)
                            {
                                selectNodes.Add(node);
                                if (start != null)
                                {
                                    break;
                                }
                                start = node;
                            }
                            else if (start != null)
                            {
                                selectNodes.Add(node);
                            }
                        }
                    }
                    else
                    {
                        selectNodes.Add(args.Area.Tag);
                    }
                }
                else
                {
                    selectNodes.Add(args.Area.Tag);
                }
            }
            else
            {
                foreach (var selected in SelectedNodes)
                    selected.IsSelected = false;
            }

            foreach (var selected in selectNodes)
            {
                if (selected.AllowSelect)
                {
                    selected.IsSelected = true;

                    NodeSelected?.Invoke(this, new BTreeNodeMouseEventArgs(
                        selected, args.Args.Button, args.Args.Clicks, args.Args.X, args.Args.Y));
                }
            }

            if (args.Area.Tag.AllowSelect)
            {
                args.Area.Tag.IsSelected = true;
                mSelectedNode = args.Area.Tag;

                NodeSelected?.Invoke(this, new BTreeNodeMouseEventArgs(
                    mSelectedNode, args.Args.Button, args.Args.Clicks, args.Args.X, args.Args.Y));
            }

            if (NodeMouseDown != null)
                NodeMouseDown(this, new BTreeNodeMouseEventArgs(
                    mSelectedNode, args.Args.Button, args.Args.Clicks, args.Args.X, args.Args.Y));

            Host.Invalidate();
        }

        void mNodeInputHelper_MouseUp(WinFormMouseInputEventArgs<IBTreeNode> args)
        {
            if (NodeMouseUp != null)
                NodeMouseUp(this, new BTreeNodeMouseEventArgs(
                    mSelectedNode, args.Args.Button, args.Args.Clicks, args.Args.X, args.Args.Y));

            Host.Invalidate();
        }

        void mButtonInputHelper_MouseLeave(WinFormMouseInputEventArgs<IBTreeNode> args)
        {
            mEnterNode = null;

            Host.Invalidate();
        }

        void mButtonInputHelper_MouseEnter(WinFormMouseInputEventArgs<IBTreeNode> args)
        {
            mEnterNode = args.Area.Tag;

            Host.Invalidate();
        }

        void mNodeInputHelper_MouseDoubleClick(WinFormMouseInputEventArgs<IBTreeNode> args)
        {
            var clickNode = args.Area.Tag;
            if (clickNode != null)
            {
                clickNode.OnDobuleClick();

                if (NodeDoubleClick != null)
                    NodeDoubleClick(this, new BTreeNodeMouseEventArgs(
                        clickNode, args.Args.Button, args.Args.Clicks, args.Args.X, args.Args.Y));

                Host.Invalidate();
            }
        }

        void mNodeInputHelper_MouseEnter(WinFormMouseInputEventArgs<IBTreeNode> args)
        {
            mHoverNode = args.Area.Tag;

            if (Host.AllowSelectRegion &&
                AllowItemDrop &&
                mDragEventArgs != null)
            {
                if (NodeDragEnter != null)
                {
                    NodeDragEnter(this,
                        new BTreeNodeDragEnterEventArgs(
                            mHoverNode,
                            mDragEventArgs.Data,
                            args.Args.Button,
                            args.Args.Clicks,
                            args.Args.X,
                            args.Args.Y));
                }
            }

            Host.Invalidate();
        }

        void mNodeInputHelper_MouseLeave(WinFormMouseInputEventArgs<IBTreeNode> args)
        {
            mHoverNode = null;

            if (AllowItemDrop && mDragEventArgs != null)
            {
                if (NodeDragLeave != null)
                {
                    NodeDragLeave(this,
                        new BTreeNodeDragLeaveEventArgs(
                            args.Args.Button,
                            args.Args.Clicks,
                            args.Args.X,
                            args.Args.Y));
                }
            }

            Host.Invalidate();
        }

        void mButtonInputHelper_MouseDown(WinFormMouseInputEventArgs<IBTreeNode> args)
        {
            args.Area.Tag.IsExpanded = !args.Area.Tag.IsExpanded;
            if (args.Area.Tag.IsExpanded)
                args.Area.Tag.OnExpanded();
            else
                args.Area.Tag.OnClosed();

            Host.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawIndex = 0;
            // 清空区域
            mButtonInputHelper.ClearRegions();
            mNodeInputHelper.ClearRegions();

            var width = Size.Width == 0 ? Host.Width : Size.Width;
            var bounds = new Rectangle(
                Location.X + Padding.Left,
                Location.Y + Padding.Top,
                width - Padding.Right,
                Host.Height - Padding.Bottom);

            e.Graphics.Clip = new Region(bounds);

            // 首先绘制节点
            var itemSize = new Size(0, 0);
            if (TopNode != null)
            {
                itemSize = PaintNode(e, TopNode, ShowTop ? 0 : -1, new Size(0, -mVScrollBar.Depth + Padding.Top + Location.Y));
            }

            // 绘制滚动条
            mVScrollBar.Length = Host.ClientSize.Height;
            mVScrollBar.MaxDepth = itemSize.Height + mVScrollBar.Depth;
            if (mVScrollBar.Visible)
                mVScrollBar.Paint(e);

            mHScrollBar.Length = Host.ClientSize.Width;
            mHScrollBar.MaxDepth = itemSize.Width;
            if (mHScrollBar.Visible)
                mHScrollBar.Paint(e);

            e.Graphics.ResetClip();
        }

        private Size PaintNode(PaintEventArgs e, IBTreeNode node, int level, Size size)
        {
            if (!node.IsVisual || node == null) return size;

            DrawIndex++;

            const int button_width = 8;

            var g = e.Graphics;

            // 取得当前X偏移
            var startX = level * ItemPadding.Left - mHScrollBar.Depth + Padding.Left + Location.X;
            var startY = size.Height;
            var over0Count = node.Nodes == null ? false : node.Nodes.FirstOrDefault() != null;
            int maxX = 0;

            if (level >= 0)
            {
                // 获取背景bounds
                var isdroptarget = mDropTargetNode == node && Host.AllowSelectRegion && AllowItemDrop && mDragEventArgs != null;
                var backColor =
                    isdroptarget || node.IsSelected ?
                        (Host.Focused || !DisenableItemBackground.HasValue ?
                            node.SelectedBackColor :
                            DisenableItemBackground.Value)
                        : (mHoverNode != null && mHoverNode == node && mHoverNode.HoverBackColor.A != 0 ? mHoverNode.HoverBackColor : node.NormalBackColor);

                if (backColor != Color.Transparent)
                {
                    //var backBounds = new Rectangle(ItemPadding.Left, startY, Host.Width - ItemPadding.Right, node.Size.Height + ItemPadding.Bottom + ItemPadding.Top);
                    var backBounds = new Rectangle(
                        Location.X, 
                        startY, 
                        (Size.Width == 0 ? Host.Width : Size.Width) - (VScrollBar.Visible ? 1 + VScrollBar.Width : 0), 
                        node.Size.Height + ItemPadding.Bottom + ItemPadding.Top);

                    // 绘制背景
                    using (SolidBrush brush = new SolidBrush(backColor))
                    {
                        e.Graphics.FillRectangle(brush, backBounds);
                    }
                }

                startY += ItemPadding.Top;

                // 绘制节点按钮
                Rectangle buttonBounds = default(Rectangle);
                if (node.IsVisualButton && over0Count)
                {
                    Color buttonColor = node.IsSelected ?
                        (SelectedButtonColor == Color.Transparent ? ButtonColor : SelectedButtonColor) :
                        (mEnterNode == node ? SelectedButtonColor : ButtonColor);

                    if (node.IsExpanded)
                    {
                        buttonBounds = new Rectangle(startX, startY + 8, button_width, button_width / 2);

                        if (node.IsSelected && mEnterNode == node)
                        {
                            using (Pen pen = new Pen(new SolidBrush(buttonColor)))
                            {
                                var rect = new Rectangle(startX + 1, startY + 8, button_width, button_width / 2);
                                GraphicsExpand.DrawTriangle(
                                    g,
                                    pen,
                                    rect,
                                    135);
                            }

                        }
                        else
                        {
                            using (Brush brush = new SolidBrush(buttonColor))
                            {
                                GraphicsExpand.FillTriangle(
                                    g,
                                    brush,
                                    new RectangleF(startX, startY + 8, button_width + 1, button_width / 2 + 0.5f),
                                    135);
                            }
                        }
                    }
                    else
                    {
                        buttonBounds = new Rectangle(startX, startY + 7, button_width, button_width / 2);

                        if (node.IsSelected && mEnterNode == node)
                        {
                            using (Brush brush = new SolidBrush(buttonColor))
                            {
                                var rect = new Rectangle(startX, startY + 7, button_width + 2, button_width / 2 + 1);
                                //rect.Inflate(1, 1);
                                GraphicsExpand.FillTriangle(
                                    g,
                                    brush,
                                    rect,
                                    90);
                            }
                        }
                        else
                        {
                            using (Pen pen = new Pen(new SolidBrush(buttonColor)))
                            {
                                GraphicsExpand.DrawTriangle(
                                    g,
                                    pen,
                                    buttonBounds,
                                    90);
                            }


                        }
                    }
                }

                startX = startX + button_width;

                // 设置节点位置
                node.Location = new Point(startX, startY);

                if (node.Location.Y > Host.Height || node.Location.Y + node.Size.Height < 0)
                {
                }
                else
                {
                    if (EnableNodeEvents)
                    {
                        // 添加node选取
                        var nodeBounds = new Rectangle(
                                Location.X + Padding.Left,
                                startY - ItemPadding.Top,
                                Host.Width - Location.X - Padding.Left - Padding.Right + mHScrollBar.Depth,
                                node.Size.Height + ItemPadding.Bottom);

                        mNodeInputHelper.AddRegion(node, nodeBounds);
                    }

                    // 绘制节点
                    node.Paint(e);
                }

                // 添加按钮事件
                if (!buttonBounds.IsEmpty)
                {
                    mButtonInputHelper.AddRegion(node,
                        new Rectangle(
                            buttonBounds.X - 5,
                            buttonBounds.Y - 5,
                            buttonBounds.Width + 5,
                            buttonBounds.Height + 5));
                }

                // 查询最大x
                maxX = Math.Max(startX + node.Size.Width, size.Width);

                // 添加当前节点高度
                startY = startY + node.Size.Height + ItemPadding.Bottom;
            }

            // 绘制子节点
            if (node.IsExpanded && over0Count)
            {
                var ns = node.Nodes.ToArray();
                for (var i = 0; i < ns.Length; i++)
                {
                    var snode = ns[i];
                    var snodeSize = PaintNode(e, snode, level + 1, new Size(maxX, startY));
                    startY = snodeSize.Height;
                    maxX = Math.Max(snodeSize.Width, maxX);
                }
            }

            return new Size(maxX, startY);
        }

        public void Expand()
        {
            Expand(true);
        }

        public void Expand(bool ignoreChildren)
        {
            Expand(TopNode, ignoreChildren);
        }

        public void Expand(IBTreeNode node)
        {
            Expand(node, true);
        }

        public void Expand(IBTreeNode node, bool ignoreChildren)
        {
            node.IsExpanded = true;

            if (!ignoreChildren)
                foreach (var item in GetCollectionNodes(node))
                    item.IsExpanded = true;
        }

        public void Collapse()
        {
            Collapse(true);
        }

        public void Collapse(bool ignoreChildren)
        {
            Collapse(TopNode, ignoreChildren);
        }

        public void Collapse(IBTreeNode node)
        {
            Collapse(node, false);
        }

        public void Collapse(IBTreeNode node, bool ignoreChildren)
        {
            node.IsExpanded = false;

            if (!ignoreChildren)
                foreach (var item in GetCollectionNodes(node))
                    item.IsExpanded = false;
        }

        public IBTreeNode GetNodeAt(Point point)
        {
            return GetNodeAt(point.X, point.Y);
        }

        public IBTreeNode GetNodeAt(int x, int y)
        {
            //x = x + mHScrollBar.Depth - Padding.Left;
            y = y + mVScrollBar.Depth - Padding.Top; // -Padding.Bottom;

            foreach (var node in GetVisualNodes(TopNode))
            {
                y = y - node.Size.Height - ItemPadding.Top - ItemPadding.Bottom;

                if (y <= 0)
                {
                    return node;
                }
            }

            return null;
        }

        public int GetRealPointY(IBTreeNode node)
        {
            var result = 0;
            var visualNodes = GetVisualNodes(TopNode);
            foreach (var vnode in visualNodes)
            {
                if (vnode == node)
                {
                    return result;
                }
                else
                {
                    result = result + vnode.Size.Height;
                }
            }

            return result;
        }

        public IEnumerable<IBTreeNode> GetNodeParents(IBTreeNode node)
        {
            var parent = GetNodes(TopNode).FirstOrDefault(X => X.Nodes.Contains(node));
            if (parent != null)
            {
                yield return parent;

                foreach (var sparent in GetNodeParents(parent))
                {
                    yield return sparent;
                }
            }
        }

        public IEnumerable<IBTreeNode> GetNodes()
        {
            return GetNodes(TopNode);
        }

        public IEnumerable<IBTreeNode> GetNodes(IBTreeNode node)
        {
            if (node != null)
            {
                yield return node;

                if (node?.Nodes != null)
                {
                    foreach (IBTreeNode snode in node.Nodes)
                    {
                        foreach (var ssnode in GetNodes(snode))
                        {
                            yield return ssnode;
                        }
                    }
                }
            }
        }

        public IEnumerable<IBTreeNode> GetVisualNodes(IBTreeNode node)
        {
            if (node != null && node.IsVisual)
            {
                if (node != TopNode || ShowTop)
                    yield return node;

                if (node.IsExpanded)
                {
                    foreach (IBTreeNode snode in node.Nodes)
                    {
                        foreach (var ssnode in GetVisualNodes(snode))
                        {
                            yield return ssnode;
                        }
                    }
                }
            }
        }

        public IEnumerable<IBTreeNode> GetCollectionNodes(IBTreeNode node)
        {
            if (node?.Nodes?.FirstOrDefault() != null)
            {
                if (node != null)
                {
                    yield return node;

                    foreach (IBTreeNode snode in node?.Nodes)
                    {
                        foreach (var ssnode in GetNodes(snode))
                        {
                            yield return ssnode;
                        }
                    }
                }
            }
        }

        public event EventHandler<BTreeNodeDoDragEventArgs> NodeDoDrag;
        public event EventHandler<BTreeNodeDragDropEventArgs> NodeDragDrop;
        public event EventHandler<BTreeNodeDragEnterEventArgs> NodeDragEnter;
        public event EventHandler<BTreeNodeDragLeaveEventArgs> NodeDragLeave;
        public event EventHandler<BTreeNodeMouseEventArgs> NodeMouseDown;
        public event EventHandler<BTreeNodeMouseEventArgs> NodeMouseUp;
        public event EventHandler<BTreeNodeMouseEventArgs> NodeSelected;
        public event EventHandler<BTreeNodeMouseEventArgs> NodeClick;
        public event EventHandler<BTreeNodeMouseEventArgs> NodeDoubleClick;
    }
}
