using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static addin.controls.FloatingWindow;
using BigBuns.Compute.Drawing;

namespace addin.controls.renderer
{
    public interface IBControl : IUIElment
    {
        List<WinFormInputRegion> InputRegins { get; }

        void Invalidate();

        void Refresh();

        int Width { get; set; }

        int Height { get; set; }

        Size ClientSize { get; }

        bool AllowSelectRegion { get; set; }

        Cursor Cursor { get; set; }

        Color BackColor { get; set; }

        Font Font { get; set; }

        bool Focused { get; }

        event EventHandler SizeChanged;
        event EventHandler ClientSizeChanged;
        event MouseEventHandler MouseDown;
        event MouseEventHandler MouseMove;
        event MouseEventHandler MouseUp;
        event EventHandler GotFocus;
        event EventHandler LostFocus;
        event EventHandler MouseLeave;
        event MouseEventHandler MouseWheel;
        event KeyEventHandler KeyDown;
        event KeyEventHandler KeyUp;
        event DragEventHandler DragDrop;
        event DragEventHandler DragEnter;
        event DragEventHandler DragOver;
        event EventHandler DragLeave;
        event EventHandler Disposed;

        Point PointToClient(Point mousePosition);

        DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects);

        void AddRegion(WinFormInputRegion item);

        bool RemoveRegion(WinFormInputRegion item);

        void ClearRegions(IEnumerable<WinFormInputRegion> items);

        Point PointToScreen(Point showPoint);

        bool Focus();
    }

    public class BControl : Control, IBControl
    {
        private Color mBackColor;
        private Size mRazorSize;
        private IntPtr mHandle;
        private bool mBackground;
        private volatile bool mFocused;
        private volatile bool mInPaintQueue;
        private readonly HandleRef hDCRef;
        private readonly Graphics hDCGraphics;
        private readonly BPainter mPainter;
        private readonly object mLock = new object();
        private DynamicArray<OverlapPaintTask> mOverlapTasks;

        public Image Icon
        {
            get;
            set;
        }

        public bool InPainting => mInPaintQueue;

        public bool Background => mBackground;

        public Bitmap RazorBMP { get; private set; }

        public Graphics RazorGFX { get; private set; }

        public List<WinFormInputRegion> InputRegins                         
        {
            get { return mRegions; }
        }

        public bool AllowSelectRegion                                       
        {
            get;
            set;
        }

        public new IntPtr Handle                                            
        {
            get { return mHandle; }
        }

        public override bool Focused => mBackground ? mFocused : base.Focused;

        public BControl(bool background = false)                            
        {
            mBackground = background;
            mRegions = new List<WinFormInputRegion>();
            mOverlapTasks = new DynamicArray<OverlapPaintTask>(0);

            AllowSelectRegion = true;
            BackColor = IDE.AppConfig.Skin.BackColor;
            Font = IDE.AppConfig.Skin.DefaultFont;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            if (mBackground)
            {
                SetStyle(ControlStyles.Opaque, true);
                SetStyle(ControlStyles.ResizeRedraw, false);
                SetStyle(ControlStyles.DoubleBuffer, false);
                SetStyle(ControlStyles.OptimizedDoubleBuffer, false);

                hDCGraphics = CreateGraphics();
                hDCRef = new HandleRef(hDCGraphics, hDCGraphics.GetHdc());
                mPainter = new BPainter();

                this.Resize += (sender, args) =>
                {
                    mInPaintQueue = true;
                    IDE.ResizeControl(this);
                };

                base.Paint += (sender, args) => 
                {
                    Invalidate();
                };

                this.GotFocus += (s, e) => mFocused = true;
                this.LostFocus += (s, e) => mFocused = false;
            }
            else 
            {
                SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                SetStyle(ControlStyles.ResizeRedraw, true);

                base.Paint += (s, e) =>
                {
                    mInPaintQueue = true;
                    InputRegins.Clear();
                    Paint?.Invoke(this, e);
                    OnPaint(e);
                    PaintOverlap(e.Graphics);
                    mInPaintQueue = false;
                };
            }
        }

        struct OverlapPaintTask                                             
        {
            public Bitmap Bitmap { get; }

            public Rectangle Bounds { get; }

            public OverlapPaintTask(Bitmap bitmap, Rectangle bounds)        
            {
                Bitmap = bitmap;
                Bounds = bounds;
            }
        }

        public void OverlapPaint(Bitmap bitmap, Rectangle dst)              
        {
            if (InPainting)
            {
                mOverlapTasks[mOverlapTasks.Length] = new OverlapPaintTask(bitmap, dst);
            }
            else 
            {
                if (mBackground)
                {
                    lock (mLock)
                    {
                        if (bitmap == RazorBMP)
                        {
                            mPainter.Paint(hDCRef, bitmap, dst);
                        }
                        else
                        {
                            var painter = new BPainter();
                            painter.Paint(hDCRef, bitmap, dst);
                            painter.Dispose();
                        }
                    }
                }
                else 
                {
                    
                }
            }
        }

        private void PaintOverlap(Graphics g)                               
        {
            if (mOverlapTasks.Length > 0)
            {
                if (mBackground)
                {
                    var painter = new BPainter();
                    for (var i = 0; i < mOverlapTasks.Length; i++)
                    {
                        var task = mOverlapTasks[i];
                        painter.Paint(hDCRef, task.Bitmap, task.Bounds);
                    }

                    painter.Dispose();
                }
                else 
                {
                    for (var i = 0; i < mOverlapTasks.Length; i++)
                    {
                        var task = mOverlapTasks[i];
                        if (task.Bitmap != null)
                        {
                            g.DrawImage(task.Bitmap, task.Bounds);
                        }
                    }
                }
                mOverlapTasks.Clear();
            }
        }

        protected override void OnHandleCreated(EventArgs e)                
        {
            base.OnHandleCreated(e);
            mHandle = base.Handle;
        }

        protected override void Dispose(bool disposing)                     
        {
            if (mBackground)
            {
                mRazorSize = default;
                if (RazorGFX != null) RazorGFX.Dispose();
                if (RazorBMP != null) RazorBMP.Dispose();
                if (hDCGraphics != null) hDCGraphics.Dispose();

                mPainter.Dispose();
            }

            base.Dispose(disposing);
        }

        public virtual new void Invalidate()                                
        {
            if (mBackground)
            {
                if (mRazorSize.Width == 0 || mRazorSize.Width == 0 || RazorGFX == null || RazorBMP == null || IsDisposed) return;

                if (!mInPaintQueue)
                {
                    mInPaintQueue = true;
                    IDE.PaintControl(this, new Rectangle(0, 0, mRazorSize.Width, mRazorSize.Height));
                }
            }
            else 
            {
                base.Invalidate();
            }
        }

        public void ForceInvalidate()                                       
        {
            if (mBackground)
            {
                if (mRazorSize.Width == 0 || mRazorSize.Width == 0 || RazorGFX == null || RazorBMP == null || IsDisposed)
                    return;

                mInPaintQueue = true;

                IDE.PaintControl(this, new Rectangle(0, 0, mRazorSize.Width, mRazorSize.Height));
            }
            else
            {
                base.Refresh();
            }
        }

        public virtual new void Invalidate(Rectangle rect)                  
        {
            if (mBackground)
            {
                if (!mInPaintQueue)
                {
                    IDE.PaintControl(this, rect);
                }
            }
            else 
            {
                base.Invalidate(rect);
            }
        }

        public override void Refresh()                                      
        {
            if (mBackground)
            {
                Invalidate();
            }
            else 
            {
                base.Refresh();
            }
        }

        internal void ReleaseQueue()                                        
        {
            mInPaintQueue = false;
        }

        internal void ThreadResize()                                        
        {
            lock (mLock)
            {
                if (RazorGFX != null) RazorGFX.Dispose();
                if (RazorBMP != null) RazorBMP.Dispose();
                if (Width > 0 && Height > 0 && !IsDisposed)
                {
                    mRazorSize = new Size(Width, Height);
                    RazorBMP = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
                    RazorBMPChanged?.Invoke(this, EventArgs.Empty);
                    RazorGFX = Graphics.FromImage(RazorBMP);
                    ThreadPaint(new Rectangle(0, 0, RazorBMP.Width, RazorBMP.Height));
                }
                else
                {
                    mRazorSize = default;
                }

                mInPaintQueue = false;
            }
        }

        internal void ThreadPaint(Rectangle bounds)                         
        {
            if (Width == 0 || Height == 0 || RazorGFX == null || IsDisposed)
            {
                mInPaintQueue = false;
                return;
            }

            lock (mLock)
            {
                InputRegins.Clear();
                RazorGFX.Clear(BackColor);
                //var bounds = new Rectangle(0, 0, RazorBMP.Width, RazorBMP.Height);
                var args = new PaintEventArgs(RazorGFX, bounds);
                Paint?.Invoke(this, args);
                OnPaint(args);
                mPainter.Paint(hDCRef, RazorBMP, bounds);
                PaintOverlap(RazorGFX);
                mInPaintQueue = false;
                Painted?.Invoke(this, args);
            }
        }

        protected virtual new void OnPaint(PaintEventArgs e)                
        {
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)        
        {
            CatchItem(e);
            if (mCatchRegion != null)
            {
                mCatchRegion.State = WinFormInputAreaState.Click;
                mCatchRegion.InputHelper.FireMouseDoubleClick(mCatchRegion, e);
            }

            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)               
        {
            Focus();
            CatchItem(e);
            mDownRegion = mCatchRegion;
            if (mCatchRegion != null)
            {
                mCatchRegion.State = WinFormInputAreaState.Pressed;
                mCatchRegion.InputHelper.FireMouseDown(mCatchRegion, e);
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)                 
        {
            CatchItem(e);
            if (mCatchRegion != null)
            {
                mCatchRegion.State = WinFormInputAreaState.Hover;
                mCatchRegion.InputHelper.FireMouseUp(mCatchRegion, e);

                if (mDownRegion == mCatchRegion)
                {
                    mCatchRegion.State = WinFormInputAreaState.Click;
                    mCatchRegion.InputHelper.FireMouseClick(mCatchRegion, e);
                }

                mCatchRegion.State = WinFormInputAreaState.Hover;
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)                   
        {
            var e2 = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);
            CatchItem(e2);
            if (mCatchRegion != null)
            {
                mCatchRegion.State = WinFormInputAreaState.None;
                mCatchRegion.InputHelper.FireMouseLeave(
                    mCatchRegion,
                    e2);
            }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)               
        {
            CatchItem(e);
            base.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)              
        {
            if (mCatchRegion != null)
                mCatchRegion.InputHelper.FireMouseWheel(mCatchRegion, e);

            base.OnMouseWheel(e);
        }

        public void CatchItem(MouseEventArgs e)                             
        {
            if (AllowSelectRegion)
            {
                WinFormInputRegion focusItem = null;

                lock (mRegions)
                {
                    for (var i = mRegions.Count - 1; i > -1; i--)
                    {
                        var item = mRegions[i];

                        if (item.Rectangle.Contains(e.Location))
                        {
                            focusItem = item;

                            break;
                        }
                    }
                }

                // 检查是否离开某个区域
                if (mCatchRegion != focusItem)
                {
                    if (mCatchRegion != null)
                    {
                        mCatchRegion.State = WinFormInputAreaState.None;
                        mCatchRegion.InputHelper.FireMouseLeave(mCatchRegion, e);
                    }

                    if (focusItem != null && focusItem.State != WinFormInputAreaState.Hover)
                    {
                        focusItem.State = WinFormInputAreaState.Hover;
                        focusItem.InputHelper.FireMouseEnter(focusItem, e);
                    }

                    mCatchRegion = focusItem;
                }
                else
                {
                    if (mCatchRegion != null)
                    {
                        if (mCatchRegion.State != WinFormInputAreaState.Pressed)
                            mCatchRegion.State = WinFormInputAreaState.Hover;

                        mCatchRegion.InputHelper.FireMouseMove(mCatchRegion, e);
                    }
                }
            }
        }

        #region InputHelper

        private WinFormInputRegion          mCatchRegion;
        private WinFormInputRegion          mDownRegion;
        private List<WinFormInputRegion>    mRegions;

        public void AddRegion(WinFormInputRegion item)                      
        {
            lock (mRegions)
            {
                mRegions.Add(item);
            }
        }

        public bool RemoveRegion(WinFormInputRegion item)                   
        {
            lock (mRegions)
            {
                return mRegions.Remove(item);
            }
        }

        public void ClearRegions(IEnumerable<WinFormInputRegion> items)     
        {
            lock (mRegions)
            {
                foreach (var item in items) mRegions.Remove(item);
            }
        }

        void IUIPoint.Paint(PaintEventArgs e)
        {
        }

        #endregion

        public event PaintEventHandler Paint;
        public event PaintEventHandler Painted;
    }
}
  