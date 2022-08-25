using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace addin.controls.renderer
{
    public delegate void BCurveEditorDelegate(VectorPath path); 

    public class BCurveEditor : BUIElement
    {
        const int LEFT_WIDTH = 28;
        const int BOTTOM_HEIGHT = 28;
        const float POINT_SIZE = 5;
        const float HALF_POINT_ZIE = POINT_SIZE / 2;

        private VectorPath mPath;
        private int mDownIndex;
        private Vector2 mDownPoint;
        private Point mDownLocation;
        private SizeF mMaxSize;
        private List<Vector2> mCreatePoints;
        private List<Vector2> mPaintPoints;

        public float MaxError
        {
            get;
            set;
        } = 2;

        public Color LineColor
        {
            get;
            set;
        } = IDE.AppConfig.Skin.ForeColor;

        public Color ForeColor
        {
            get;
            set;
        } = IDE.AppConfig.Skin.ForeColor;

        public VectorPath Path 
        {
            get { return mPath; }
            set
            {
                if (mPath != value) 
                {
                    mPath = value;
                    InitPath(mPath);
                    mCreatePoints = new List<Vector2>(mPaintPoints);
                }
            }
        }

        public BCurveEditor(BControl host)
            : base(host)
        {
            mPaintPoints = new List<Vector2>();
            Host.MouseDown += Host_MouseDown;
            Host.MouseMove += Host_MouseMove;
            Host.MouseUp += Host_MouseUp;
        }

        private void Host_MouseUp(object sender, MouseEventArgs e)
        {
            if (mCreatePoints[mDownIndex] != mPaintPoints[mDownIndex])
            {
                mCreatePoints = new List<Vector2>(mPaintPoints);
                Changed?.Invoke(Path);
            }

            mDownIndex = -1; 
        }

        private void Host_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (mDownIndex == -1)
                    return;

                var cp = e.Location;
                var ox = cp.X - mDownLocation.X;
                var oy = cp.Y - mDownLocation.Y;

                var scale = GetScale();
                var offset = new Vector2(ox, -oy) / scale;
                if (offset.X * offset.Y == 0)
                    return;

                mCreatePoints[mDownIndex] = mDownPoint + offset;

                ResetPath();
                Host.Invalidate();
            }
            else 
            {
                var scale = GetScale();
                for (var i = mPaintPoints.Count - 1; i >= 0; i--)
                {
                    var point = mPaintPoints[i];
                    var vp = GetViewPoint(mPaintPoints[i], scale);
                    var bounds = new RectangleF(vp.X - HALF_POINT_ZIE, vp.Y - HALF_POINT_ZIE, POINT_SIZE, POINT_SIZE);
                    if (bounds.Contains(e.X, e.Y))
                    {
                        mDownIndex = i;
                        mDownPoint = point;
                        Host.Invalidate();
                        break;
                    }
                }
            }
        }

        private void Host_MouseDown(object sender, MouseEventArgs e)
        {
            if (mDownIndex != -1) 
            {
                if (e.Button == MouseButtons.Left)
                {
                    mDownLocation = e.Location;
                }
                else
                {
                    mPaintPoints.RemoveAt(mDownIndex);
                    ResetPath();
                    Changed?.Invoke(Path);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var linePen = new Pen(LineColor);
            var foreBrush = new SolidBrush(ForeColor);

            var curveBounds = new Rectangle(Location.X + LEFT_WIDTH, Location.Y, Size.Width - LEFT_WIDTH, Size.Height - BOTTOM_HEIGHT);

            e.Graphics.DrawLine(linePen, curveBounds.X, Location.Y, curveBounds.X, Location.Y + curveBounds.Height);
            e.Graphics.DrawLine(linePen, curveBounds.X, curveBounds.Bottom, curveBounds.Right, curveBounds.Bottom);

            var temp = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            if (Path != null)
            {
                var scale = GetScale();

                for (var i = 0; i < mPaintPoints.Count - 1; i++) 
                {
                    var vc = GetViewPoint(mPaintPoints[i], scale);
                    var vn = GetViewPoint(mPaintPoints[i + 1], scale);

                    e.Graphics.DrawLine(i == mDownIndex ? IDE.AppConfig.Skin.AccentPen : linePen, vc, vn);
                }
            }

            e.Graphics.SmoothingMode = temp;

            var oneStr = "1";
            var zeroStr = "0";
            var oneSize = e.Graphics.MeasureString(oneStr, IDE.AppConfig.Skin.SmallFont);
            var zeroSzie = e.Graphics.MeasureString(zeroStr, IDE.AppConfig.Skin.SmallFont);

            e.Graphics.DrawString(oneStr, IDE.AppConfig.Skin.SmallFont, foreBrush, curveBounds.Left - oneSize.Width - 2, curveBounds.Top);
            e.Graphics.DrawString(oneStr, IDE.AppConfig.Skin.SmallFont, foreBrush, curveBounds.Right - oneSize.Width, curveBounds.Bottom + 2);
            e.Graphics.DrawString(zeroStr, IDE.AppConfig.Skin.SmallFont, foreBrush, curveBounds.X, curveBounds.Bottom + 2);

            linePen.Dispose();
            foreBrush.Dispose();
        }

        private void ResetPath() 
        {
            Path.Reset(CurveFit.Fit(mCreatePoints, MaxError));
            InitPath(Path);
        }

        private Vector2 GetScale() 
        {
            var curveBounds = new Rectangle(Location.X + LEFT_WIDTH, Location.Y, Size.Width - LEFT_WIDTH, Size.Height - BOTTOM_HEIGHT);
            var scaleX = curveBounds.Width / mMaxSize.Width;
            var scaleY = curveBounds.Height / mMaxSize.Height;
            return new Vector2(scaleX, scaleY);
        }

        private void InitPath(VectorPath mPath)
        {
            mPaintPoints = mPath.GetVertices().ToList();
            if (mPaintPoints.Count == 0)
                return;

            var maxX = mPaintPoints.Max(x => x.X);
            var maxY = mPaintPoints.Max(x => x.Y);
            mMaxSize = new SizeF(maxX, maxY);
        }

        public void Update() 
        {
            InitPath(Path);
            mCreatePoints = new List<Vector2>(mPaintPoints);
        }

        private PointF GetViewPoint(Vector2 vec2, Vector2 scale) 
        {
            vec2 = vec2 * scale;
            return new PointF(Location.X + LEFT_WIDTH + vec2.X, Location.Y + Size.Height - BOTTOM_HEIGHT - vec2.Y);
        }

        public event BCurveEditorDelegate Changed;
    }
}
