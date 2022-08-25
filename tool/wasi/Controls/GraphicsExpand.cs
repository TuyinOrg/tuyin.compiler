using addin.common;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Matrix = System.Drawing.Drawing2D.Matrix;

namespace System.Drawing
{
    public static class GraphicsExpand
    {
        public static Image ResetImageSize(Image bitmap, int targetWidth, int targetHeight)                         
        {
            Bitmap b = new Bitmap(targetWidth, targetHeight);
            Graphics g = Graphics.FromImage(b);
            // 插值算法的质量
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(bitmap, new Rectangle(0, 0, targetWidth, targetHeight), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
            g.Dispose();
            return b;
        }

        public static void DrawRoundRectangle(this Graphics g, Pen pen, RectangleF rect, int cornerRadius)          
        {
            if (cornerRadius == 0)
            {
                g.DrawRectangle(pen, new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height));
            }
            else
            {
                using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        public static void FillColor(this Graphics g, Color color, RectangleF rect, int cellWidth = 0) 
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

                        g.FillRectangle(isDark ? dark : light, x, y, cellWidth + 1, cellWidth + 1);
                    }
                }
            }
            g.ResetClip();

            if(color.A != 0) g.FillRectangle(new SolidBrush(color), rect);
        }

        public static void FillRoundRectangle(this Graphics g, Brush brush, RectangleF rect, int cornerRadius)      
        {
            if (brush == null)
                throw new NotSupportedException();

            var temp = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.HighQuality;

            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                g.FillPath(brush, path);
            }

            g.SmoothingMode = temp;
        }

        public static void DrawPlusSign(this Graphics g, Pen pen, Rectangle rect, int width)                        
        {
            using (GraphicsPath path = CreatePlusSignPath(rect, width))
            {
                g.DrawPath(pen, path);
            }
        }

        public static void FillPlusSign(this Graphics g, Brush brush, Rectangle rect, int width)                    
        {
            using (GraphicsPath path = CreatePlusSignPath(rect, width))
            {
                g.FillPath(brush, path);
            }
        }

        public static void DrawError(this Graphics g, Pen pen, Rectangle rect, float width)                         
        {
            using (GraphicsPath path = CreatePlusSignPath(rect, width))
            {
                var matrix = new Matrix();
                matrix.Rotate(45);
                path.Transform(matrix);

                g.DrawPath(pen, path);
            }
        }

        public static void FillError(this Graphics g, Brush brush, Rectangle rect, float width)                     
        {
            using (GraphicsPath path = CreatePlusSignPath(rect, width))
            {
                var matrix = new Matrix();
                matrix.RotateAt(45, new PointF(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2));
                path.Transform(matrix);

                g.FillPath(brush, path);
            }
        }

        public static void DrawTally(this Graphics g, Pen pen, Rectangle rect)                                      
        {
            var tempSmoothingMode = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.HighQuality;
            using (GraphicsPath path = CreateTallyPath(rect)) 
            {
                g.DrawPath(pen, path);
            }

            g.SmoothingMode = tempSmoothingMode;
        }

        public static void FillTally(this Graphics g, Brush brush, Rectangle rect)                                  
        {
            var tempSmoothingMode = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.HighQuality;
            using (GraphicsPath path = CreateTallyPath(rect))
            {
                g.FillPath(brush, path);
            }

            g.SmoothingMode = tempSmoothingMode;
        }

        public static void DrawPencil(this Graphics g, Pen pen, Rectangle rect)                                     
        {

        }

        public static void DrawTriangle(this Graphics g, Pen pen, Rectangle rect, float angle)                      
        {
            var center = new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
            var point1 = new Point(rect.Left + rect.Width / 2, rect.Top);
            var point2 = new Point(rect.Right, rect.Bottom);
            var point3 = new Point(rect.Left, rect.Bottom);

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddLine(point1.X, point1.Y, point2.X, point2.Y);
                path.AddLine(point2.X, point2.Y, point3.X, point3.Y);
                path.AddLine(point3.X, point3.Y, point1.X, point1.Y);
                path.CloseFigure();

                var matrix = new Matrix();
                matrix.RotateAt(angle, center);
                path.Transform(matrix);

                g.DrawPath(pen, path);
            }
        }

        public static void FillTriangle(this Graphics g, Brush brush, RectangleF rect, float angle)                 
        {
            var center = new PointF(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
            var point1 = new PointF(rect.Left + rect.Width / 2, rect.Top);
            var point2 = new PointF(rect.Right, rect.Bottom);
            var point3 = new PointF(rect.Left, rect.Bottom);

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddLine(point1.X, point1.Y, point2.X, point2.Y);
                path.AddLine(point2.X, point2.Y, point3.X, point3.Y);
                path.AddLine(point3.X, point3.Y, point1.X, point1.Y);
                path.CloseFigure();

                var matrix = new Matrix();
                matrix.RotateAt(angle, center);
                path.Transform(matrix);

                g.FillPath(brush, path);
            }
        }

        public static void FillTriangle(this Graphics g, Brush brush, PointF point1, PointF point2, PointF point3)  
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddLine(point1.X, point1.Y, point2.X, point2.Y);
                path.AddLine(point2.X, point2.Y, point3.X, point3.Y);
                path.AddLine(point3.X, point3.Y, point1.X, point1.Y);
                path.CloseFigure();

                g.FillPath(brush, path);
            }
        }

        public static void DrawStop(this Graphics g, Pen pen, RectangleF rect, float thinkness, float space)        
        {
            var startX = rect.X + (rect.Width - thinkness * 2 - space) / 2;
            g.DrawRectangle(pen, startX, rect.Y, thinkness, rect.Height);
            startX = startX + space + thinkness;
            g.DrawRectangle(pen, startX, rect.Y, thinkness, rect.Height);
        }

        public static void FillStop(this Graphics g, Brush brush, RectangleF rect, float thinkness, float space)    
        {
            var startX = rect.X + (rect.Width - thinkness * 2 - space) / 2;
            g.FillRectangle(brush, startX, rect.Y, thinkness, rect.Height);
            startX = startX + space + thinkness;
            g.FillRectangle(brush, startX, rect.Y, thinkness, rect.Height);
        }

        public static void DrawThumb(this Graphics g, Pen pen, Rectangle rect)                                      
        {
            throw new NotSupportedException();

            var width = rect.Width;
            for (var y = 0; y < width; y++) 
            {
                if (y % 2 == 0)
                {
                    for (var x = 0; x < width / 2; x++)
                    {

                    }
                }
            }
        }

        public static void DrawTab(this Graphics g, Pen pen, Rectangle rect, int cornerRadius, Padding padding)     
        {
            g.DrawRectangle(pen, rect);

            return;

            using (GraphicsPath path = CreateTabPath(rect, cornerRadius, padding))
            {
                g.DrawPath(pen, path);
            }
        }

        public static void FillTab(this Graphics g, Brush brush, Rectangle rect, int cornerRadius, Padding padding) 
        {
            g.FillRectangle(brush, rect);

            return;

            using (GraphicsPath path = CreateTabPath(rect, cornerRadius, padding))
            {
                g.FillPath(brush, path);
            }
        }

        public static void DrawWaveLine(this Graphics g, Color color, float width, PointF start, PointF end)          
        {
            using (var pen = new Pen(new SolidBrush(color), width))
            {
                if (end.X - start.X < 2)
                {
                    g.DrawLine(pen, start, end);
                    return;
                }

                var offset = -1;
                var points = new List<PointF>();

                for (float i = start.X; i <= end.X; i += 2)
                {
                    points.Add(new PointF(i, start.Y + offset));
                    offset = -offset;
                }

                g.DrawLines(pen, points.ToArray());
            }
        }

        public static void DrawDotLine(this Graphics g, PointF start, PointF end, Color color, float thickness, float dotLength)
        {
            var length = MathHelper.GetDistance(start, end);
            if (length < dotLength)
            {
                g.DrawLine(new Pen(color, thickness), start, end);
            }
            else
            {
                var dotCount = (int)(length / dotLength);
                for (var i = 0; i < dotCount; i++)
                {
                    if (i % 3 != 0)
                    {
                        var currPoint = MathHelper.GetExtendPoint(start, end, i * dotLength);
                        var nextPoint = MathHelper.GetExtendPoint(currPoint, end, dotLength);
                        g.DrawLine(new Pen(color, thickness), currPoint, nextPoint);
                    }
                }
            }
        }

        public static void DrawArc(this Graphics g, Pen pen, PointF p1, PointF p2, PointF p3)
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
            g.DrawArc(pen, new RectangleF(x0 - r0, y0 - r0, 2 * r0, 2 * r0), (float)min, (float)(max - min));
        }

        private static GraphicsPath CreateTabPath(Rectangle rect, int cornerRadius, Padding padding)                
        {
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
        }

        private static GraphicsPath CreateRoundedRectanglePath(RectangleF rect, int cornerRadius)                   
        {
            if (cornerRadius == 0)
            {
                GraphicsPath roundedRect = new GraphicsPath();
                roundedRect.AddRectangle(rect);
                return roundedRect;
            }
            else
            {
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
            }
        }

        private static GraphicsPath CreatePlusSignPath(Rectangle rect, float width)                                 
        {
            var point1 = new PointF(rect.X + (rect.Width - width) / 2, rect.Y);
            var point2 = new PointF(point1.X + width, rect.Y);
            var point3 = new PointF(point2.X, rect.Y + (rect.Height - width) / 2);
            var point4 = new PointF(rect.Right, point3.Y);
            var point5 = new PointF(point4.X, point4.Y + width);
            var point6 = new PointF(point3.X, point5.Y);
            var point7 = new PointF(point6.X, rect.Bottom);
            var point8 = new PointF(point1.X, rect.Bottom);
            var point9 = new PointF(point1.X, point6.Y);
            var point10 = new PointF(rect.Left, point5.Y);
            var point11= new PointF(rect.Left, point4.Y);
            var point12 = new PointF(point1.X, point3.Y);

            GraphicsPath path = new GraphicsPath();
            path.AddLine(point1, point2);
            path.AddLine(point2, point3);
            path.AddLine(point3, point4);
            path.AddLine(point4, point5);
            path.AddLine(point5, point6);
            path.AddLine(point6, point7);
            path.AddLine(point7, point8);
            path.AddLine(point8, point9);
            path.AddLine(point9, point10);
            path.AddLine(point10, point11);
            path.AddLine(point11, point12);
            path.CloseFigure();
            return path;
        }

        private static GraphicsPath CreateTallyPath(Rectangle rect)                                                 
        {
            var point1 = new PointF(rect.X + 0, rect.Y + 0.5f * rect.Height);
            var point2 = new PointF(rect.X + 5f / 13f * rect.Width, rect.Y + rect.Height);
            var point3 = new PointF(rect.X + rect.Width, rect.Y + 0.2f * rect.Height);
            var point4 = new PointF(rect.X + 11f / 13f * rect.Width, rect.Y + 0);
            var point5 = new PointF(rect.X + 5f / 13f * rect.Width, rect.Y + 6f / 10f * rect.Height);
            var point6 = new PointF(rect.X + 2f / 13f * rect.Width, rect.Y + 3f / 10f * rect.Height);

            GraphicsPath path = new GraphicsPath();
            path.AddLine(point1, point2);
            path.AddLine(point2, point3);
            path.AddLine(point3, point4);
            path.AddLine(point4, point5);
            path.AddLine(point5, point6);
            path.CloseFigure();
            return path;
        }

        //定义图像反色函数
        public static Bitmap PContray(Bitmap a)                                                                     
        {
            int w = a.Width;
            int h = a.Height;
            Bitmap dstBitmap = new Bitmap(a.Width, a.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Imaging.BitmapData srcData = a.LockBits(new Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Imaging.BitmapData dstData = dstBitmap.LockBits(new Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pIn = (byte*)srcData.Scan0.ToPointer();
                byte* pOut = (byte*)dstData.Scan0.ToPointer();
                byte* p;
                int stride = srcData.Stride;
                int r, g, b;
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        p = pIn;
                        r = p[2];
                        g = p[1];
                        b = p[0];
                        pOut[2] = (byte)(255 - r);
                        pOut[1] = (byte)(255 - g);
                        pOut[0] = (byte)(255 - b);
                        pIn += 3;
                        pOut += 3;
                    }
                    pIn += srcData.Stride - w * 3;
                    pOut += srcData.Stride - w * 3;
                }
                a.UnlockBits(srcData);
                dstBitmap.UnlockBits(dstData);
                return dstBitmap;
            }
        }

        public static Point[] GetOutlinePoints(Bitmap image)                                                        
        {
            List<Point> outlinePoints = new List<Point>();

            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] originalBytes = new byte[image.Width * image.Height * 4];
            Marshal.Copy(bitmapData.Scan0, originalBytes, 0, originalBytes.Length);

            for (int x = 0; x < bitmapData.Width; x++)
            {
                for (int y = 0; y < bitmapData.Height; y++)
                {
                    byte alpha = originalBytes[y * bitmapData.Stride + 4 * x + 3];

                    if (alpha != 0)
                    {
                        Point p = new Point(x, y);

                        if (!ContainsPoint(outlinePoints, p))
                            outlinePoints.Add(p);

                        break;
                    }
                }
            }

            for (int y = 0; y < bitmapData.Height; y++)
            {
                for (int x = bitmapData.Width - 1; x >= 0; x--)
                {
                    byte alpha = originalBytes[y * bitmapData.Stride + 4 * x + 3];

                    if (alpha != 0)
                    {
                        Point p = new Point(x, y);

                        if (!ContainsPoint(outlinePoints, p))
                            outlinePoints.Add(p);

                        break;
                    }
                }
            }

            for (int x = bitmapData.Width - 1; x >= 0; x--)
            {
                for (int y = bitmapData.Height - 1; y >= 0; y--)
                {
                    byte alpha = originalBytes[y * bitmapData.Stride + 4 * x + 3];

                    if (alpha != 0)
                    {
                        Point p = new Point(x, y);

                        if (!ContainsPoint(outlinePoints, p))
                            outlinePoints.Add(p);

                        break;
                    }
                }
            }

            for (int y = bitmapData.Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < bitmapData.Width; x++)
                {
                    byte alpha = originalBytes[y * bitmapData.Stride + 4 * x + 3];

                    if (alpha != 0)
                    {
                        Point p = new Point(x, y);

                        if (!ContainsPoint(outlinePoints, p))
                            outlinePoints.Add(p);

                        break;
                    }
                }
            }

            // Added to close the loop
            outlinePoints.Add(outlinePoints[0]);

            image.UnlockBits(bitmapData);

            return outlinePoints.ToArray();
        }

        public static bool ContainsPoint(IEnumerable<Point> points, Point value)                                    
        {
            foreach (Point p in points)
            {
                if (p == value)
                    return true;
            }

            return false;
        }



    }
}
