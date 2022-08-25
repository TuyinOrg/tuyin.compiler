using libtui.utils;
using System;

namespace libtui.drawing
{
    public static class GraphicsExpand
    {
        public static void DrawRoundRectangle(this IGraphicDevice g, Pen pen, Rectangle rect, int cornerRadius)          
        {
            if (cornerRadius == 0)
            {
                g.DrawGeometry(pen, rect);
            }
            else
            {
                g.DrawGeometry(pen, CreateRoundedRectanglePath(rect, cornerRadius));
            }
        }

        public static void FillColor(this IGraphicDevice g, Color color, RectangleF rect, int cellWidth = 0) 
        {
            g.SetClip(rect);
            if (cellWidth > 0) 
            {
                var width = Math.Ceiling(rect.Width / cellWidth);
                var height = Math.Ceiling(rect.Height / cellWidth);
                var light = new SolidBrush(Color.White);
                var dark = new SolidBrush(Color.FromArgb(205, 205, 205));

                for (var c = 0; c < width; c++) 
                {
                    for (var r = 0; r < height; r++) 
                    {
                        var x = c * cellWidth + rect.Left;
                        var y = r * cellWidth + rect.Top;

                        var isDark = (c + r) % 2 == 0;

                        g.FillGeometry(isDark ? dark : light, new RectangleF(x, y, cellWidth + 1, cellWidth + 1));
                    }
                }
            }

            g.ResetClip();
            if(color.A != 0) g.FillGeometry(new SolidBrush(color), rect);
        }

        public static void FillRoundRectangle(this IGraphicDevice g, Brush brush, Rectangle rect, int cornerRadius)      
        {
            if (brush == null)
                throw new NotSupportedException();

            g.FillGeometry(brush, CreateRoundedRectanglePath(rect, cornerRadius));
        }

        public static void DrawTriangle(this IGraphicDevice g, Pen pen, Rectangle rect, float angle)                      
        {
            var center = new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
            var point1 = MathTools.Rotate(center, new Point(rect.Left + rect.Width / 2, rect.Top), angle);
            var point2 = MathTools.Rotate(center, new Point(rect.Right, rect.Bottom), angle);
            var point3 = MathTools.Rotate(center, new Point(rect.Left, rect.Bottom), angle);

            g.DrawGeometry(pen, new Polygon(new Point[] { point1, point2, point3 }));
        }

        public static void FillTriangle(this IGraphicDevice g, Brush brush, RectangleF rect, float angle)                 
        {
            var center = new PointF(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
            var point1 = MathTools.Rotate(center, new PointF(rect.Left + rect.Width / 2, rect.Top), angle);
            var point2 = MathTools.Rotate(center, new PointF(rect.Right, rect.Bottom), angle);
            var point3 = MathTools.Rotate(center, new PointF(rect.Left, rect.Bottom), angle);

            g.FillGeometry(brush, new PolygonF(new PointF[] { point1, point2, point3 }));
        }

        public static void FillTriangle(this IGraphicDevice g, Brush brush, PointF point1, PointF point2, PointF point3)  
        {
            g.FillGeometry(brush, new PolygonF(new PointF[] { point1, point2, point3 }));
        }

        public static void DrawArc(this IGraphicDevice g, Pen pen, PointF p1, PointF p2, PointF p3)
        {
            float x1, y1, x2, y2, y3, x3;
            x1 = p1.X;
            y1 = p1.Y;
            x2 = p2.X;
            y2 = p2.Y;
            x3 = p3.X;
            y3 = p3.Y;
            float x0, y0, r0;//圆心和半径
            float m1, m2, mx1, mx2, my1, my2;
            if (Math.Abs(y2 - y1) < 0.0001)
            {
                m2 = -(x3 - x2) / (y3 - y2);
                mx2 = (x2 + x3) / 2.0f;
                my2 = (y2 + y3) / 2.0f;
                x0 = (x1 + x2) / 2.0f;
                y0 = m2 * (x0 - mx2) + my2;
            }
            else if (Math.Abs(y3 - y2) < 0.0001)
            {
                m1 = -(x2 - x1) / (y2 - y1);
                mx1 = (x1 + x2) / 2.0f;
                my1 = (y1 + y2) / 2.0f;
                x0 = (x2 + x3) / 2.0f;
                y0 = m1 * (x0 - mx1) + my1;
            }
            else
            {
                m1 = -(x2 - x1) / (y2 - y1);
                m2 = -(x3 - x2) / (y3 - y2);
                mx1 = (x1 + x2) / 2.0f;
                mx2 = (x2 + x3) / 2.0f;
                my1 = (y1 + y2) / 2.0f;
                my2 = (y2 + y3) / 2.0f;
                x0 = (m1 * mx1 - m2 * mx2 + my2 - my1) / (m1 - m2);
                y0 = m1 * (x0 - mx1) + my1;
            }

            r0 = Convert.ToSingle(Math.Sqrt((x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1)));


            //画弧
            float k1, k2, k3;//斜率
            k1 = (y1 - y0) / (x1 - x0);
            k2 = (y2 - y0) / (x2 - x0);
            k3 = (y3 - y0) / (x3 - x0);

            double[] a = new double[3];//弧度
            double[] b = new double[3];//角度
            double[] x = new double[3];
            double min, max;
            double v = 180.0 / 3.14159;//弧度与角度转换
            a[0] = Math.Atan(k1);
            a[1] = Math.Atan(k2);
            a[2] = Math.Atan(k3);

            x[0] = x1;
            x[1] = x2;
            x[2] = x3;
            for (int i = 0; i < 3; i++)
            {
                if (a[i] >= 0)
                {
                    if (x[i] > x0)
                    {
                        b[i] = a[i] * v;
                    }
                    else if (x[i] < x0)
                    {
                        b[i] = 180.0 + a[i] * v;
                    }
                }
                else
                {
                    a[i] = Math.Abs(a[i]);
                    if (x[i] > x0)
                    {
                        b[i] = 360.0 - a[i] * v;
                    }
                    else if (x[i] < x0)
                    {
                        b[i] = 180.0 - a[i] * v;
                    }
                }
            }
            //寻找最小角度和最大角度
            min = b[0];
            max = b[0];
            for (int i = 1; i < 3; i++)
            {
                if (b[i] < min)
                    min = b[i];
                if (b[i] > max)
                    max = b[i];
            }
            //画弧
            //g.DrawGeometry(pen, new RectangleF(x0 - r0, y0 - r0, 2 * r0, 2 * r0), (float)min, (float)(max - min));
        }

        private static IGeometry CreateTabPath(Rectangle rect, int cornerRadius, Padding padding)                
        {
            /*
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
            */
            return null;
        }

        private static IGeometry CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)                   
        {
            if (cornerRadius == 0)
            {
                return rect;
            }
            else
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
                path.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
                path.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
                path.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
                path.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
                path.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
                path.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
                path.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
                return path;
            }
        }
    }
}
