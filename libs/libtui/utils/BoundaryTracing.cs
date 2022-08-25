using libtui.drawing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace libtui.utils
{
    interface IPixels
    {
        bool IsTransparent(Point p);

        int Tolerate(Point p1, Point p2);

        Rectangle GetBounds();
    }

    struct Boundary 
    {
        public IList<Point> Points { get; }

        public int Parent { get; }

        public Boundary(int parent, IList<Point> pts) 
        {
            Parent = parent;
            Points = pts;
        }
    }

    static class BoundaryTracing
    {
        interface IPath
        {
            bool Contains(Point point);

            Rectangle GetBounds();
        }

        class RectPath : IPath
        {
            private Rectangle rect;

            public RectPath(Rectangle rect) 
            {
                this.rect = rect;
            }

            public bool Contains(Point point)
            {
                return ((point.X < 0) || (point.X >= rect.Width) || (point.Y < 0) || (point.Y >= rect.Height)) ? false : true;
            }

            public Rectangle GetBounds()
            {
                return rect;
            }
        }

        class PolyPath : IPath
        {
            private IList<Point> points;

            public PolyPath(IList<Point> points) 
            {
                this.points = points;
            }

            public bool Contains(Point point)
            {
                //return PointIsInPolygon(point.X, point.Y, points);
                throw new NotImplementedException();
            }
       
            public Rectangle GetBounds()
            {
                return MathTools.GetBoundingBox(points); // BoundaryTracing.GetBounds(points);
            }
        }

        class Soilds : IPixels 
        {
            private IPath mPath;
            private bool[,] pixels;

            public Soilds(IList<Point> points, IPath path)
            {
                mPath = path;

                var bounds = mPath.GetBounds();
                pixels = new bool[bounds.Width, bounds.Height];

                for (var i = 0; i < points.Count; i++)
                {
                    var p = points[i];
                    pixels[p.X - bounds.X, p.Y - bounds.Y] = true;
                }
            }

            public Rectangle GetBounds()
            {
                return mPath.GetBounds();
            }

            public bool IsTransparent(Point p)
            {
                if (!mPath.Contains(p))
                    return true;

                return !pixels[p.X, p.Y];
            }

            public int Tolerate(Point p1, Point p2)
            {
                return 0;
            }
        }

        class Pixels : IPixels
        {
            private Rectangle rect;
            private Color[] pixels;
            private byte[] grays;

            public Color this[Point point]
            {
                get
                {
                    int n = (point.Y * rect.Width) + point.X;
                    return pixels[n];
                }
                protected set
                {
                    int n = (point.Y * rect.Width) + point.X;
                    pixels[n] = value;
                }
            }

            public Pixels(Color[] pixels, IPath path)
            {
                rect = path.GetBounds();
                this.pixels = pixels;
                grays = new byte[rect.Width * rect.Height];

                for (var i = 0; i < grays.Length; i++)
                {
                    var pixel = pixels[i];
                    grays[i] = (byte)(pixel.R * 0.2989 + pixel.G * 0.5870 + pixel.B * 0.1140);
                }
            }

            public bool Contains(Point point)
            {
                return ((point.X < 0) || (point.X >= rect.Width) || (point.Y < 0) || (point.Y >= rect.Height)) ? false : true;
            }

            public bool IsTransparent(Point point)
            {
                if (!Contains(point))
                    return true;

                return this[point].A == 0;
            }

            public int Tolerate(Point p1, Point p2)
            {
                var n = grays[(p1.Y * rect.Width) + p1.X];
                var n2 = grays[(p2.Y * rect.Width) + p2.X];

                if (n > n2)
                    return n - n2;
                else
                    return n2 - n;
            }

            public Rectangle GetBounds()
            {
                return rect;
            }
        }

        private static readonly Tuple<Func<Point, Point>, int>[] sNeighborhood8 = new Tuple<Func<Point, Point>, int>[] {
            new Tuple<Func<Point, Point>, int>(point => new Point(point.X-1,point.Y), 7),
            new Tuple<Func<Point, Point>, int>(point => new Point(point.X-1,point.Y-1), 7),
            new Tuple<Func<Point, Point>, int>(point => new Point(point.X,point.Y-1), 1),
            new Tuple<Func<Point, Point>, int>(point => new Point(point.X+1,point.Y-1), 1),
            new Tuple<Func<Point, Point>, int>(point => new Point(point.X+1,point.Y), 3),
            new Tuple<Func<Point, Point>, int>(point => new Point(point.X+1,point.Y+1), 3),
            new Tuple<Func<Point, Point>, int>(point => new Point(point.X,point.Y+1), 5),
            new Tuple<Func<Point, Point>, int>(point => new Point(point.X-1,point.Y+1), 5)
        };

        private static readonly Tuple<Func<Point, Point>, int>[] sNeighborhood4 = new Tuple<Func<Point, Point>, int>[] {
            new Tuple<Func<Point, Point>, int>(point => new Point(point.X-1,point.Y), 4),
            new Tuple<Func<Point, Point>, int>(point => new Point(point.X,point.Y-1), 1),
            new Tuple<Func<Point, Point>, int>(point => new Point(point.X+1,point.Y), 2),
            new Tuple<Func<Point, Point>, int>(point => new Point(point.X,point.Y+1), 3),
        };

        public static List<Boundary> Create(Color[] pixels, int width, int height) 
        {
            return Create(pixels, 0, false, new Rectangle(0, 0, width, height));
        }

        public static List<Boundary> Create(Color[] pixels, int width, int height, int tolerance)
        {
            return Create(pixels, tolerance, false, new Rectangle(0, 0, width, height));
        }

        public static List<Boundary> Create(Color[] pixels, int width, int height, int tolerance, bool isLaplacian) 
        {
            return Create(pixels, tolerance, isLaplacian, new Rectangle(0, 0, width, height));
        }

        public static List<Boundary> Create(Color[] pixels, int tolerance, bool isLaplacian, Rectangle rect)
        {
            var path = new RectPath(rect);
            return Create(new Pixels(pixels, path), tolerance, isLaplacian);
        }

        public static List<Boundary> Create(IPixels pixels, int tolerance, bool isLaplacian, int count = int.MaxValue) 
        {
            var bounds = pixels.GetBounds();

            int[,] found = new int[bounds.Width, bounds.Height];
            List<Point> list = null;
            List<Boundary> lists = new List<Boundary>();
            bool inside = false;
            int parentIndex = 0;
            Point insidePoint = default;

            // Defines the neighborhood offset position from current position and the neighborhood
            // position we want to check next if we find a new border at checkLocationNr.
            var neighborhood = isLaplacian ? sNeighborhood8 : sNeighborhood4;
            var maxCounter = isLaplacian ? 3 : 2;
            var maxCounter2 = isLaplacian ? 8 : 4;

            for (int y = 0; y < bounds.Height; ++y)
            {
                for (int x = 0; x < bounds.Width; ++x)
                {
                    Point point = new Point(x, y);
                    // Scan for non-transparent pixel
                    if (found[x, y] != 0)
                    {
                        parentIndex = found[x, y];
                        // Entering an already discovered border
                        if (!inside)
                        {
                            inside = true;
                            insidePoint = point;
                            continue;
                        }
                    }

                    bool isTransparent = pixels.IsTransparent(point);
                    if (!isTransparent && inside)
                    {
                        // Already discovered border point
                        if (pixels.Tolerate(point, insidePoint) > tolerance)
                        {
                            inside = false;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (isTransparent && inside)
                    {
                        // Leaving a border
                        inside = false;
                        continue;
                    }

                    if (!isTransparent && !inside)
                    {
                        lists.Add(new Boundary(parentIndex, list = new List<Point>()));

                        // Undiscovered border point
                        found[point.X, point.Y] = lists.Count;
                        list.Add(new Point(point.X + bounds.X, point.Y + bounds.Y));   // Mark the start pixel
                        int checkLocationNr = 1;  // The neighbor number of the location we want to check for a new border point
                        Point startPos = point;      // Set start position
                        Point lastPos = point;
                        int counter = 0;       // Counter is used for the jacobi stop criterion
                        int counter2 = 0;       // Counter2 is used to determine if the point we have discovered is one single point

                        // Trace around the neighborhood
                        while (true)
                        {
                            // The corresponding absolute array address of checkLocationNr
                            Point checkPosition = neighborhood[checkLocationNr - 1].Item1(point);
                            // Variable that holds the neighborhood position we want to check if we find a new border at checkLocationNr
                            int newCheckLocationNr = neighborhood[checkLocationNr - 1].Item2;

                            // Beware that the point might be outside the bitmap.
                            // The isTransparent method contains the safety check.
                            if (!pixels.IsTransparent(checkPosition) && pixels.Tolerate(point, checkPosition) <= tolerance)
                            {
                                // Next border point found
                                if (checkPosition == startPos)
                                {
                                    counter++;

                                    // Stopping criterion (jacob)
                                    if (true) // newCheckLocationNr == 1 || counter >= maxCounter)
                                    {
                                        // Close loop
                                        if (lastPos != startPos)
                                            list.Add(lastPos);

                                        inside = true; // Since we are starting the search at were we first started we must set inside to true
                                        insidePoint = checkPosition;
                                        //parentIndex = lists.Count - 1;

                                        if (lists.Count >= count)
                                            return lists;

                                        break;
                                    }
                                }
                               
                                checkLocationNr = newCheckLocationNr; // Update which neighborhood position we should check next
                                point = checkPosition;
                                counter2 = 0;             // Reset the counter that keeps track of how many neighbors we have visited
                                found[point.X, point.Y] = lists.Count;
                                list.Add(new Point(point.X + bounds.X, point.Y + bounds.Y)); // Set the border pixel                              
                            }
                            else
                            {
                                // Rotate clockwise in the neighborhood
                                checkLocationNr = 1 + (checkLocationNr % maxCounter2);
                                if (counter2 > maxCounter2)
                                {
                                    // If counter2 is above 8 we have traced around the neighborhood and
                                    // therefor the border is a single black pixel and we can exit
                                    // counter2 = 0;
                                    // list = null;
                                    break;
                                }
                                else
                                {
                                    counter2++;
                                }
                            }
                        }

                    }
                }
            }

            return lists;
        }

        public static bool PointIsInPolygon(float x, float y, IReadOnlyList<GeometryPoint> polygon_points)
        {
            // Get the angle between the point and the
            // first and last vertices.
            int max_point = polygon_points.Count - 1;
            float total_angle = GetAngle(
                polygon_points[max_point].X, polygon_points[max_point].Y,
                x, y,
                polygon_points[0].X, polygon_points[0].Y);

            // Add the angles from the point
            // to each other pair of vertices.
            for (int i = 0; i < max_point; i++)
            {
                if (polygon_points[i].X == x && polygon_points[i].Y == y)
                    return true;

                total_angle += GetAngle(
                    polygon_points[i].X, polygon_points[i].Y,
                    x, y,
                    polygon_points[i + 1].X, polygon_points[i + 1].Y);
            }

            // The total angle should be 2 * PI or -2 * PI if
            // the point is in the polygon and close to zero
            // if the point is outside the polygon.
            return (Math.Abs(total_angle) > 0.00001);
        }

        private static float GetAngle(float Ax, float Ay, float Bx, float By, float Cx, float Cy)
        {
            // Get the dot product.
            float dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy);

            // Get the cross product.
            float cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);

            // Calculate the angle.
            return (float)Math.Atan2(cross_product, dot_product);
        }

        private static float DotProduct(float Ax, float Ay, float Bx, float By, float Cx, float Cy)
        {
            // Get the vectors' coordinates.
            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;

            // Calculate the dot product.
            return (BAx * BCx + BAy * BCy);
        }

        private static float CrossProductLength(float Ax, float Ay, float Bx, float By, float Cx, float Cy)
        {
            // Get the vectors' coordinates.
            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;

            // Calculate the Z coordinate of the cross product.
            return (BAx * BCy - BAy * BCx);
        }


        // Given three colinear points p, q, r,
        // the function checks if point q lies
        // on line segment 'pr'
        static bool onSegment(PointF p, PointF q, PointF r)
        {
            if (q.X <= Math.Max(p.X, r.X) &&
                q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) &&
                q.Y >= Math.Min(p.Y, r.Y))
            {
                return true;
            }
            return false;
        }

        // To find orientation of ordered triplet (p, q, r).
        // The function returns following values
        // 0 --> p, q and r are colinear
        // 1 --> Clockwise
        // 2 --> Counterclockwise
        static int orientation(PointF p, PointF q, PointF r)
        {
            var val = (q.Y - p.Y) * (r.X - q.X) -
                    (q.X - p.X) * (r.Y - q.Y);

            if (val == 0)
            {
                return 0; // colinear
            }
            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }

        // The function that returns true if
        // line segment 'p1q1' and 'p2q2' intersect.
        static bool doIntersect(PointF p1, PointF q1,
                                PointF p2, PointF q2)
        {
            // Find the four orientations needed for
            // general and special cases
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
            {
                return true;
            }

            // Special Cases
            // p1, q1 and p2 are colinear and
            // p2 lies on segment p1q1
            if (o1 == 0 && onSegment(p1, p2, q1))
            {
                return true;
            }

            // p1, q1 and p2 are colinear and
            // q2 lies on segment p1q1
            if (o2 == 0 && onSegment(p1, q2, q1))
            {
                return true;
            }

            // p2, q2 and p1 are colinear and
            // p1 lies on segment p2q2
            if (o3 == 0 && onSegment(p2, p1, q2))
            {
                return true;
            }

            // p2, q2 and q1 are colinear and
            // q1 lies on segment p2q2
            if (o4 == 0 && onSegment(p2, q1, q2))
            {
                return true;
            }

            // Doesn't fall in any of the above cases
            return false;
        }

        // Returns true if the point p lies
        // inside the polygon[] with n vertices
        public static bool IsInside(float x, float y, IList<PointF> polygon)
        {
            int n = polygon.Count;
            // There must be at least 3 vertices in polygon[]
            if (n < 3)
            {
                return false;
            }

            PointF p = new PointF(x, y);

            // Create a point for line segment from p to infinite
            PointF extreme = new PointF(10000000, p.Y);

            // Count intersections of the above line
            // with sides of polygon
            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;

                // Check if the line segment from 'p' to
                // 'extreme' intersects with the line
                // segment from 'polygon[i]' to 'polygon[next]'
                if (doIntersect(polygon[i],
                                polygon[next], p, extreme))
                {
                    // If the point 'p' is colinear with line
                    // segment 'i-next', then check if it lies
                    // on segment. If it lies, return true, otherwise false
                    if (orientation(polygon[i], p, polygon[next]) == 0)
                    {
                        return onSegment(polygon[i], p,
                                        polygon[next]);
                    }
                    count++;
                }
                i = next;
            } while (i != 0);

            // Return true if count is odd, false otherwise
            return (count % 2 == 1); // Same as (count%2 == 1)
        }


        public static IList<GeometryPoint> Optimize(IList<GeometryPoint> points, Double tolerance = 0.00000000001) 
        {
            return ShapeOptimizationHelper.DouglasPeuckerReduction(points, tolerance);
        }

        public static IList<PointF> Optimize(IList<PointF> points, Double tolerance = 0.00000000001) 
        {
            return ShapeOptimizationHelper.DouglasPeuckerReduction(points, tolerance);
        }

        public static float GetArea(IList<Point> points)
        {
            // Get the areas.
            float area = 0;
            for (int i = 0; i < points.Count; i++)
            {
                var curr = points[i];
                var next = points[i == points.Count - 1 ? 0 : i + 1];

                area +=
                    (next.X - curr.X) *
                    (next.Y + curr.Y) / 2;
            }

            // Return the result.
            return Math.Abs(area);
        }

        static List<Point> getPoints(List<List<Point>> lists)
        {
            lists.Sort((x, y) => x.Count.CompareTo(y.Count));
            return lists.Last();
        }

        static class ShapeOptimizationHelper
        {
            static class Constants
            {
                /// <summary>
                /// The default plane distance tolerance
                /// </summary>
                internal const double DefaultPlaneDistanceTolerance = 1e-10;
                /// <summary>
                /// The starting delta dot product in simplex
                /// </summary>
                internal const double StartingDeltaDotProductInSimplex = 0.5;
                /// <summary>
                /// The connector table size
                /// </summary>
                internal const int ConnectorTableSize = 2017;
            }

            public static IList<PointF> DouglasPeuckerReduction(IList<PointF> points, Double tolerance)
            {
                if (points == null || points.Count < 3)
                    return points;

                Int32 firstPoint = 0;
                Int32 lastPoint = points.Count - 1;
                List<Int32> pointIndexsToKeep = new List<Int32>();

                //Add the first and last index to the keepers
                pointIndexsToKeep.Add(firstPoint);
                pointIndexsToKeep.Add(lastPoint);

                //The first and the last point cannot be the same
                while (points[firstPoint].Equals(points[lastPoint]))
                {
                    lastPoint--;
                }

                DouglasPeuckerReductionRecursive(points, firstPoint, lastPoint,
                    tolerance, ref pointIndexsToKeep);

                List<PointF> returnPoints = new List<PointF>();
                pointIndexsToKeep.Sort();
                foreach (Int32 index in pointIndexsToKeep)
                {
                    returnPoints.Add(points[index]);
                }

                return returnPoints;
            }

            public static IList<GeometryPoint> DouglasPeuckerReduction(IList<GeometryPoint> points, Double tolerance)
            {
                if (points == null || points.Count < 3)
                    return points;

                Int32 firstPoint = 0;
                Int32 lastPoint = points.Count - 1;
                List<Int32> pointIndexsToKeep = new List<Int32>();

                //Add the first and last index to the keepers
                pointIndexsToKeep.Add(firstPoint);
                pointIndexsToKeep.Add(lastPoint);

                //The first and the last point cannot be the same
                while (points[firstPoint].Equals(points[lastPoint]))
                {
                    lastPoint--;
                }

                DouglasPeuckerReductionRecursive(points, firstPoint, lastPoint,
                    tolerance, ref pointIndexsToKeep);

                List<GeometryPoint> returnPoints = new List<GeometryPoint>();
                pointIndexsToKeep.Sort();
                foreach (Int32 index in pointIndexsToKeep)
                {
                    returnPoints.Add(points[index]);
                }

                return returnPoints;
            }

            private static void DouglasPeuckerReductionRecursive(IList<GeometryPoint>
                points, Int32 firstPoint, Int32 lastPoint, Double tolerance,
                ref List<Int32> pointIndexsToKeep)
            {
                Double maxDistance = 0;
                Int32 indexFarthest = 0;

                for (Int32 index = firstPoint; index < lastPoint; index++)
                {
                    Double distance = (Double)PerpendicularDistance
                        (points[firstPoint].Location, points[lastPoint].Location, points[index].Location);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        indexFarthest = index;
                    }
                }

                if (maxDistance > tolerance && indexFarthest != 0)
                {
                    //Add the largest point that exceeds the tolerance
                    pointIndexsToKeep.Add(indexFarthest);

                    DouglasPeuckerReductionRecursive(points, firstPoint,
                        indexFarthest, tolerance, ref pointIndexsToKeep);
                    DouglasPeuckerReductionRecursive(points, indexFarthest,
                        lastPoint, tolerance, ref pointIndexsToKeep);
                }
            }

            private static void DouglasPeuckerReductionRecursive(IList<PointF>
                points, Int32 firstPoint, Int32 lastPoint, Double tolerance,
                ref List<Int32> pointIndexsToKeep)
            {
                Double maxDistance = 0;
                Int32 indexFarthest = 0;

                for (Int32 index = firstPoint; index < lastPoint; index++)
                {
                    Double distance = (Double)PerpendicularDistance
                        (points[firstPoint], points[lastPoint], points[index]);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        indexFarthest = index;
                    }
                }

                if (maxDistance > tolerance && indexFarthest != 0)
                {
                    //Add the largest point that exceeds the tolerance
                    pointIndexsToKeep.Add(indexFarthest);

                    DouglasPeuckerReductionRecursive(points, firstPoint,
                        indexFarthest, tolerance, ref pointIndexsToKeep);
                    DouglasPeuckerReductionRecursive(points, indexFarthest,
                        lastPoint, tolerance, ref pointIndexsToKeep);
                }
            }

            private static double PerpendicularDistance(PointF Point1, PointF Point2, PointF Point)
            {
                double area = Math.Abs(.5f * (Point1.X * Point2.Y + Point2.X *
                    Point.Y + Point.X * Point1.Y - Point2.X * Point1.Y - Point.X *
                    Point2.Y - Point1.X * Point.Y));
                double bottom = Math.Sqrt(Math.Pow(Point1.X - Point2.X, 2f) +
                    Math.Pow(Point1.Y - Point2.Y, 2f));
                double height = area / bottom * 2f;

                return height;
            }
        }
    }
}