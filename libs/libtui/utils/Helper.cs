using libtui.drawing;

namespace libtui.utils
{
    static class Helper
    {
        public static PointF[] ToFloat(this Point[] pts)
        {
            PointF[] r = new PointF[pts.Length];
            for (var i = 0; i < r.Length; i++)
                r[i] = new PointF(pts[i].X, pts[i].Y);

            return r;
        }
    }
}
