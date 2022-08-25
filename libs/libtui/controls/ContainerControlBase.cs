using libtui.drawing;
using libtui.libraries.kdtree;
using System.Collections.Generic;
using System.Linq;

namespace libtui.controls
{
    public abstract class ContainerControlBase : ControlBase, IContainerControl
    {
        private List<IControl> mChildrens;

        public ContainerControlBase() 
        {
            mChildrens = new List<IControl>();
        }

        public IEnumerable<IControl> Childrens => mChildrens;

        public override void OnMouseClick(MouseEventArgs e)
        {
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            var c = FindChildren((int)e.X, (int)e.Y);
            if (c != null)
                c.OnMouseDown(e);


        }

        public override void OnMouseUp(MouseEventArgs e)
        {
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
        }

        public override void OnMouseWheel(MouseEventArgs e)
        {
        }

        public override void OnMosueEnter(MouseEventArgs e)
        {
        }

        public override void OnMouseLeave(MouseEventArgs e)
        {
        }

        public override void OnGetFocus()
        {
        }

        public override void OnLostFocus()
        {
        }

        public override void OnKeyDown(KeyEventArgs e)
        {
        }

        public override void OnKeyUp(KeyEventArgs e)
        {
        }

        public override void OnSizeChanged(SizeChangeEventArgs e)
        {
        }

        public override void Invalidate()
        {
            base.Invalidate();
            foreach (var child in Childrens)
                child.Invalidate();
        }

        public override void ApplySkin(Skin skin)
        {
            base.Invalidate();
            foreach (var child in Childrens)
                child.ApplySkin(skin);
        }

        public IControl FindChildren(int x, int y) 
        {
            for (var i = 0; i < mChildrens.Count; i++) 
            {
                var child = mChildrens[i];
                if (child.Bounds.Contains(x, y)) 
                {
                    return child;
                }
            }

            return null;
        }

        class PointTypeMatch : ITypeMath<Point>
        {
            public Point MinValue => new Point(int.MaxValue);

            public Point MaxValue => new Point(0);

            public Point Zero => new Point(0);

            public Point NegativeInfinity => new Point(-1);

            public Point PositiveInfinity => new Point(-2);

            public Point Add(Point a, Point b)
            {
                return a + b;
            }

            public bool AreEqual(Point a, Point b)
            {
                return a.Equals(b);
            }

            public bool AreEqual(Point[] a, Point[] b)
            {
                return a.SequenceEqual(b);
            }

            public int Compare(Point a, Point b)
            {
                return a.GetHashCode().CompareTo(b.GetHashCode());
            }

            public Point DistanceSquaredBetweenPoints(Point[] a, Point[] b)
            {
                throw new System.NotImplementedException();
            }

            public Point Max(Point a, Point b)
            {
                return Point.Max(a, b);
            }

            public Point Min(Point a, Point b)
            {
                return Point.Min(a, b);
            }

            public Point Multiply(Point a, Point b)
            {
                return Point.Multiply(a, b);
            }

            public Point Subtract(Point a, Point b)
            {
                return Point.Subtract(a, b);
            }
        }
    }
}
