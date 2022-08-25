using compute.drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace compute.utils
{
    /// <summary>
    /// Provides a set of tools to simplify polygons in various ways.
    /// </summary>
    static class SimplifyTools
    {
        /// <summary>
        /// Removes all collinear points on the polygon.
        /// </summary>
        /// <param name="vertices">The polygon that needs simplification.</param>
        /// <param name="collinearityTolerance">The collinearity tolerance.</param>
        /// <returns>A simplified polygon.</returns>
        public static IList<PointF> CollinearSimplify(IList<PointF> vertices, float collinearityTolerance = 0)
        {
            var verticesSize = vertices.Count;
            if (verticesSize <= 3)
                return vertices;

           var simplified = new List<PointF>(verticesSize);

            for (int i = 0; i < vertices.Count; i++)
            {
                PointF prev = vertices[i - 1 < 0 ? verticesSize - 1 : i - 1];
                PointF current = vertices[i];
                PointF next = vertices[(i + 1 > vertices.Count - 1) ? 0 : i + 1];

                //If they collinear, continue
                if (MathTools.IsCollinear(ref prev, ref current, ref next, collinearityTolerance))
                    continue;

                simplified.Add(current);
            }

            return simplified;
        }

        /// <summary>
        /// Ramer-Douglas-Peucker polygon simplification algorithm. This is the general recursive version that does not use the
        /// speed-up technique by using the Melkman convex hull.
        /// 
        /// If you pass in 0, it will remove all collinear points.
        /// </summary>
        /// <returns>The simplified polygon</returns>
        public static IEnumerable<PointF> DouglasPeuckerSimplify(IList<PointF> vertices, float distanceTolerance)
        {
            if (vertices.Count <= 3)
                return vertices;

            bool[] usePoint = new bool[vertices.Count];

            for (int i = 0; i < vertices.Count; i++)
                usePoint[i] = true;

            SimplifySection(vertices, 0, vertices.Count - 1, usePoint, distanceTolerance);

            List<PointF> simplified = new List<PointF>(vertices.Count);

            for (int i = 0; i < vertices.Count; i++)
            {
                if (usePoint[i])
                    simplified.Add(vertices[i]);
            }

            return simplified;
        }

        private static void SimplifySection(IList<PointF> vertices, int i, int j, bool[] usePoint, float distanceTolerance)
        {
            if ((i + 1) == j)
                return;

            PointF a = vertices[i];
            PointF b = vertices[j];

            double maxDistance = -1.0;
            int maxIndex = i;
            for (int k = i + 1; k < j; k++)
            {
                PointF point = vertices[k];

                double distance = LineTools.DistanceBetweenPointAndLineSegment(point, a, b);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    maxIndex = k;
                }
            }

            if (maxDistance <= distanceTolerance)
            {
                for (int k = i + 1; k < j; k++)
                {
                    usePoint[k] = false;
                }
            }
            else
            {
                SimplifySection(vertices, i, maxIndex, usePoint, distanceTolerance);
                SimplifySection(vertices, maxIndex, j, usePoint, distanceTolerance);
            }
        }

        /// <summary>
        /// Merges all parallel edges in the list of vertices
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        /// <param name="tolerance">The tolerance.</param>
        public static IList<PointF> MergeParallelEdges(IList<PointF> vertices, float tolerance)
        {
            //From Eric Jordan's convex decomposition library

            if (vertices.Count <= 3)
                return vertices; //Can't do anything useful here to a triangle

            bool[] mergeMe = new bool[vertices.Count];
            int newNVertices = vertices.Count;

            //Gather points to process
            for (int i = 0; i < vertices.Count; ++i)
            {
                int lower = (i == 0) ? (vertices.Count - 1) : (i - 1);
                int middle = i;
                int upper = (i == vertices.Count - 1) ? (0) : (i + 1);

                float dx0 = vertices[middle].X - vertices[lower].X;
                float dy0 = vertices[middle].Y - vertices[lower].Y;
                float dx1 = vertices[upper].Y - vertices[middle].X;
                float dy1 = vertices[upper].Y - vertices[middle].Y;
                float norm0 = (float)Math.Sqrt(dx0 * dx0 + dy0 * dy0);
                float norm1 = (float)Math.Sqrt(dx1 * dx1 + dy1 * dy1);

                if (!(norm0 > 0.0f && norm1 > 0.0f) && newNVertices > 3)
                {
                    //Merge identical points
                    mergeMe[i] = true;
                    --newNVertices;
                }

                dx0 /= norm0;
                dy0 /= norm0;
                dx1 /= norm1;
                dy1 /= norm1;
                float cross = dx0 * dy1 - dx1 * dy0;
                float dot = dx0 * dx1 + dy0 * dy1;

                if (Math.Abs(cross) < tolerance && dot > 0 && newNVertices > 3)
                {
                    mergeMe[i] = true;
                    --newNVertices;
                }
                else
                    mergeMe[i] = false;
            }

            if (newNVertices == vertices.Count || newNVertices == 0)
                return vertices;

            int currIndex = 0;

            //Copy the vertices to a new list and clear the old
            IList<PointF> newVertices = new List<PointF>(newNVertices);

            for (int i = 0; i < vertices.Count; ++i)
            {
                if (mergeMe[i] || newNVertices == 0 || currIndex == newNVertices)
                    continue;

                Debug.Assert(currIndex < newNVertices);

                newVertices.Add(vertices[i]);
                ++currIndex;
            }

            return newVertices;
        }

        /// <summary>
        /// Merges the identical points in the polygon.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        public static IList<PointF> MergeIdenticalPoints(IList<PointF> vertices)
        {
            HashSet<PointF> unique = new HashSet<PointF>();

            foreach (PointF vertex in vertices)
            {
                unique.Add(vertex);
            }

            return new List<PointF>(unique);
        }

        /// <summary>
        /// Reduces the polygon by distance.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        /// <param name="distance">The distance between points. Points closer than this will be removed.</param>
        public static IList<PointF> ReduceByDistance(IList<PointF> vertices, float distance)
        {
            if (vertices.Count <= 3)
                return vertices;

            float distance2 = distance * distance;

            IList<PointF> simplified = new List<PointF>(vertices.Count);

            for (int i = 0; i < vertices.Count; i++)
            {
                PointF current = vertices[i];
                PointF next = vertices[(i + 1 > vertices.Count - 1) ? 0 : i + 1];

                //If they are closer than the distance, continue
                if ((next - current).LengthSquared() <= distance2)
                    continue;

                simplified.Add(current);
            }

            return simplified;
        }

        /// <summary>
        /// Reduces the polygon by removing the Nth vertex in the vertices list.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        /// <param name="nth">The Nth point to remove. Example: 5.</param>
        /// <returns></returns>
        public static IList<PointF> ReduceByNth(IList<PointF> vertices, int nth)
        {
            if (vertices.Count <= 3)
                return vertices;

            if (nth == 0)
                return vertices;

            IList<PointF> simplified = new List<PointF>(vertices.Count);

            for (int i = 0; i < vertices.Count; i++)
            {
                if (i % nth == 0)
                    continue;

                simplified.Add(vertices[i]);
            }

            return simplified;
        }

        /// <summary>
        /// Simplify the polygon by removing all points that in pairs of 3 have an area less than the tolerance.
        /// 
        /// Pass in 0 as tolerance, and it will only remove collinear points.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="areaTolerance"></param>
        /// <returns></returns>
        public static IList<PointF> ReduceByArea(IList<PointF> vertices, float areaTolerance)
        {
            //From physics2d.net

            if (vertices.Count <= 3)
                return vertices;

            if (areaTolerance < 0)
                throw new ArgumentOutOfRangeException("areaTolerance", "must be equal to or greater than zero.");

            IList<PointF> simplified = new List<PointF>(vertices.Count);
            PointF v3;
            PointF v1 = vertices[vertices.Count - 2];
            PointF v2 = vertices[vertices.Count - 1];
            areaTolerance *= 2;

            for (int i = 0; i < vertices.Count; ++i, v2 = v3)
            {
                v3 = i == vertices.Count - 1 ? simplified[0] : vertices[i];

                float old1;
                MathTools.Cross(v1, v2, out old1);

                float old2;
                MathTools.Cross(v2, v3, out old2);

                float new1;
                MathTools.Cross(v1, v3, out new1);

                if (Math.Abs(new1 - (old1 + old2)) > areaTolerance)
                {
                    simplified.Add(v2);
                    v1 = v2;
                }
            }

            return simplified;
        }

        public static List<List<PointF>> FastSimplify(List<PointF> vertices)
        {
            var polygons = new List<List<PointF>>();
            var vertexCount = vertices.Count;
            for (var edgeOneIndex = 0; edgeOneIndex < vertexCount - 2; edgeOneIndex++)
            {
                var lastEdgeIndex = vertexCount - 1;
                var edgeOne = new Segment(vertices[edgeOneIndex], vertices[(edgeOneIndex + 1) % vertexCount]);
                //First index is neighbor with last index
                if (edgeOneIndex == 0) lastEdgeIndex--;
                for (var edgeTwoIndex = edgeOneIndex + 2; edgeTwoIndex <= lastEdgeIndex; edgeTwoIndex++)
                {
                    var edgeTwo = new Segment(vertices[edgeTwoIndex], vertices[(edgeTwoIndex + 1) % vertexCount]);
                    if (Segment.Intersect(edgeOne, edgeTwo, out var intersectionPoint))
                    {
                        var subPolygonOne = new List<PointF>();
                        for (var i = 0; i <= edgeOneIndex; i++)
                        {
                            subPolygonOne.Add(vertices[i]);
                        }
                        subPolygonOne.Add(intersectionPoint);
                        for (var i = edgeTwoIndex + 1; i < vertexCount; i++)
                        {
                            subPolygonOne.Add(vertices[i]);
                        }

                        var subPolygonTwo = new List<PointF>();
                        subPolygonTwo.Add(intersectionPoint);
                        for (var i = edgeOneIndex + 1; i <= edgeTwoIndex; i++)
                        {
                            subPolygonTwo.Add(vertices[i]);
                        }

                        var simplifiedSubPolygonOne = FastSimplify(subPolygonOne);
                        var simplifiedSubPolygonTwo = FastSimplify(subPolygonTwo);

                        for (var i = 0; i < simplifiedSubPolygonOne.Count; i++)
                        {
                            polygons.Add(simplifiedSubPolygonOne[i]);
                        }

                        for (var i = 0; i < simplifiedSubPolygonTwo.Count; i++)
                        {
                            polygons.Add(simplifiedSubPolygonTwo[i]);
                        }

                        return polygons;
                    }
                }
            }

            polygons.Add(vertices);
            return polygons;
        }

        public static List<List<PointF>> FastSimplify(List<PointF> vertices, List<List<PointF>> holes)
        {
            var vertexIndexSet = new HashSet<int>();

            for (var holeIndex = holes.Count - 1; holeIndex >= 0; holeIndex--)
            {

                var holeVertexIndex = 0;
                if (!TryFindASurgerySegment(vertices, holes, holeIndex, ref holeVertexIndex, new Segment(), vertexIndexSet, out var surgerySegment1)) continue;

                var removedHole = holes[holeIndex];
                var surgerySegment = new Segment(removedHole[surgerySegment1.holeVertIndex], vertices[surgerySegment1.vertIndex]);
                holeVertexIndex++;
                if (!TryFindASurgerySegment(vertices, holes, holeIndex, ref holeVertexIndex, surgerySegment, vertexIndexSet, out var surgerySegment2)) continue;

                //Create subpolygons
                var vertexCount = vertices.Count;
                var holeVertexCount = removedHole.Count;

                var polygon1 = new List<PointF>();
                var startIndex = surgerySegment1.vertIndex;
                var endIndex = surgerySegment2.vertIndex;
                var index = startIndex;
                while (index != endIndex)
                {
                    polygon1.Add(vertices[index]);
                    index = (index + 1) % vertexCount;
                }
                polygon1.Add(vertices[endIndex]);

                startIndex = surgerySegment2.holeVertIndex;
                endIndex = surgerySegment1.holeVertIndex;
                index = startIndex;
                while (index != endIndex)
                {
                    polygon1.Add(removedHole[index]);
                    if (--index < 0) index += holeVertexCount;
                }
                polygon1.Add(removedHole[endIndex]);

                var polygon2 = new List<PointF>();
                startIndex = surgerySegment2.vertIndex;
                endIndex = surgerySegment1.vertIndex;
                index = startIndex;
                while (index != endIndex)
                {
                    polygon2.Add(vertices[index]);
                    index = (index + 1) % vertexCount;
                }
                polygon2.Add(vertices[endIndex]);

                startIndex = surgerySegment1.holeVertIndex;
                endIndex = surgerySegment2.holeVertIndex;
                index = startIndex;
                while (index != endIndex)
                {
                    polygon2.Add(removedHole[index]);
                    if (--index < 0) index += holeVertexCount;
                }
                polygon2.Add(removedHole[endIndex]);

                holes.RemoveAt(holeIndex);

                var polygon1Holes = new List<List<PointF>>();
                var polygon2Holes = new List<List<PointF>>();

                for (var i = 0; i < holes.Count; i++)
                {
                    var hole = holes[i];
                    if (PolygonInclude(polygon1, hole[0])) polygon1Holes.Add(hole);
                    else polygon2Holes.Add(hole);
                }

                var subPolygons = new List<List<PointF>>(2);

                if (polygon1Holes.Count > 0)
                {
                    var innerSubPolygons = FastSimplify(polygon1, polygon1Holes);
                    for (var i = 0; i < innerSubPolygons.Count; i++)
                    {
                        subPolygons.Add(innerSubPolygons[i]);
                    }
                }
                else subPolygons.Add(polygon1);

                if (polygon2Holes.Count > 0)
                {
                    var innerSubPolygons = FastSimplify(polygon2, polygon2Holes);
                    for (var i = 0; i < innerSubPolygons.Count; i++)
                    {
                        subPolygons.Add(innerSubPolygons[i]);
                    }
                }
                else subPolygons.Add(polygon2);

                return subPolygons;
            }

            throw new Exception("Could not find the " + nameof(SurgerySegment) + " couple!");
        }

        private static bool PolygonInclude(List<PointF> vertices, PointF q)
        {
            var intersectionCount = 0;
            var vertexCount = vertices.Count;
            for (var i = 0; i < vertexCount; i++)
            {
                var a = vertices[i];
                var b = vertices[(i + 1) % vertexCount];
                if (MathTools.Approximately(a.Y, b.Y)) continue;
                if (a.Y > b.Y)
                {
                    var temp = a;
                    a = b;
                    b = temp;
                }

                if (q.Y < a.Y || q.Y >= b.Y) continue;
                if (Triangle.CalculateOrientation(new Triangle(a, b, q)) == Triangle.TriangleOrientation.Clockwise) intersectionCount++;
            }

            return intersectionCount % 2 > 0;
        }

        private static bool TryFindASurgerySegment(List<PointF> vertices, List<List<PointF>> holes, int holeIndex, ref int holeVertexIndex,
            Segment otherSurgerySegment, HashSet<int> vertexIndexSet, out SurgerySegment surgerySegment)
        {
            var holeCount = holes.Count;
            var hole = holes[holeIndex];
            var holeVertexCount = hole.Count;
            for (; holeVertexIndex < holeVertexCount; holeVertexIndex++)
            {
                var holeVertex = hole[holeVertexIndex];
                var vertexCount = vertices.Count;
                for (var vertIndex = 0; vertIndex < vertexCount; vertIndex++)
                {
                    if (vertexIndexSet.Contains(vertIndex)) continue;

                    var vertex = vertices[vertIndex];
                    var segment = new Segment(holeVertex, vertex);

                    var intersection = Segment.Intersect(segment, otherSurgerySegment);
                    if (intersection) continue;

                    var nextHoleVertexIndex = (holeVertexIndex + 1) % holeVertexCount;
                    for (var j = 0; j < holeVertexCount - 2; j++)
                    {
                        var index = (nextHoleVertexIndex + j) % holeVertexCount;
                        var comparedSegment = new Segment(hole[index], hole[(index + 1) % holeVertexCount]);
                        if (Segment.Intersect(segment, comparedSegment))
                        {
                            intersection = true;
                            break;
                        }
                    }

                    if (intersection) continue;

                    for (var j = 0; j < holeCount; j++)
                    {
                        if (j == holeIndex) continue;
                        var comparedHole = holes[j];
                        if (Segment.Intersect(segment, comparedHole))
                        {
                            intersection = true;
                            break;
                        }
                    }

                    if (intersection) continue;

                    var nextVertexIndex = (vertIndex + 1) % vertexCount;
                    for (var j = 0; j < vertexCount - 2; j++)
                    {
                        var index = (nextVertexIndex + j) % vertexCount;
                        var comparedSegment = new Segment(vertices[index], vertices[(index + 1) % vertexCount]);
                        if (Segment.Intersect(segment, comparedSegment))
                        {
                            intersection = true;
                            break;
                        }
                    }

                    if (!intersection)
                    {
                        // Debug.DrawLine(segment.p, segment.q, Color.red, 10);
                        vertexIndexSet.Add(vertIndex);
                        surgerySegment = new SurgerySegment(holeVertexIndex, vertIndex);
                        return true;
                    }
                }
            }

            surgerySegment = new SurgerySegment();
            return false;
        }

        private struct Triangle
        {
            public PointF a;
            public PointF b;
            public PointF c;

            public Triangle(PointF a, PointF b, PointF c)
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }

            public static TriangleOrientation CalculateOrientation(Triangle tri)
            {
                var a = tri.a;
                var b = tri.b;
                var c = tri.c;
                var value = (b.X - a.X) * (c.Y - a.Y) - (c.X - a.X) * (b.Y - a.Y);
                if (value < 0) return TriangleOrientation.Clockwise;
                if (value > 0) return TriangleOrientation.CounterClockwise;
                return TriangleOrientation.CoLinear;
            }

            public enum TriangleOrientation
            {
                Clockwise,
                CounterClockwise,
                CoLinear
            }
        }

        private struct Line
        {
            private PointF a;
            private PointF b;

            public Line(PointF a, PointF b)
            {
                this.a = a;
                this.b = b;
            }

            public static bool TryGetIntersectionPointOfTwoLines(Line l1, Line l2, out PointF intersectionPoint)
            {
                var a = l1.a;
                var b = l1.b;
                var c = l2.a;
                var d = l2.b;

                float a1 = b.Y - a.Y;
                float b1 = a.X - b.X;
                float c1 = a1 * (a.X) + b1 * (a.Y);

                float a2 = d.Y - c.Y;
                float b2 = c.X - d.X;
                float c2 = a2 * (c.X) + b2 * (c.Y);

                float determinant = a1 * b2 - a2 * b1;

                if (determinant == 0)
                {
                    intersectionPoint = PointF.Zero;
                    return false;
                }

                float x = (b2 * c1 - b1 * c2) / determinant;
                float y = (a1 * c2 - a2 * c1) / determinant;
                intersectionPoint = new PointF(x, y);
                return true;
            }
        }

        private struct Segment
        {
            public PointF p;
            public PointF q;

            public Segment(PointF p, PointF q)
            {
                this.p = p;
                this.q = q;
            }

            public static bool Intersect(Segment s, List<PointF> vertices)
            {
                var vertexCount = vertices.Count;
                for (int i = 0; i < vertexCount; i++)
                {
                    var edge = new Segment(vertices[i], vertices[(i + 1) % vertexCount]);
                    if (CalculateIntersectionType(s, edge) != SegmentIntersectionType.NoIntersection) return true;
                }
                return false;
            }

            public static bool Intersect(Segment s1, Segment s2)
            {
                return CalculateIntersectionType(s1, s2) != SegmentIntersectionType.NoIntersection;
            }

            public static bool Intersect(Segment s1, Segment s2, out PointF intersectionPoint)
            {
                var segmentIntersectionType = CalculateIntersectionType(s1, s2);
                if (segmentIntersectionType != SegmentIntersectionType.NoIntersection && segmentIntersectionType != SegmentIntersectionType.Overlapping)
                {
                    return Line.TryGetIntersectionPointOfTwoLines(new Line(s1.p, s1.q), new Line(s2.p, s2.q), out intersectionPoint);
                }

                intersectionPoint = PointF.Zero;
                return false;
            }

            public static SegmentIntersectionType CalculateIntersectionType(Segment s1, Segment s2)
            {
                var abc = Triangle.CalculateOrientation(new Triangle(s1.p, s1.q, s2.p));
                var abd = Triangle.CalculateOrientation(new Triangle(s1.p, s1.q, s2.q));
                var cda = Triangle.CalculateOrientation(new Triangle(s2.p, s2.q, s1.p));
                var cdb = Triangle.CalculateOrientation(new Triangle(s2.p, s2.q, s1.q));
                if (abc != abd && cda != cdb)
                {
                    var coLinerCount = 0;
                    if (abc == Triangle.TriangleOrientation.CoLinear) coLinerCount++;
                    if (abd == Triangle.TriangleOrientation.CoLinear) coLinerCount++;
                    if (cda == Triangle.TriangleOrientation.CoLinear) coLinerCount++;
                    if (cdb == Triangle.TriangleOrientation.CoLinear) coLinerCount++;
                    switch (coLinerCount)
                    {
                        case 0:
                            return SegmentIntersectionType.IntersectionOnTheMiddle;

                        case 1:
                            return SegmentIntersectionType.IntersectionFromOneEnd;

                        case 2:
                            return SegmentIntersectionType.IntersectionFromTwoEnd;

                        case 4:
                            return SegmentIntersectionType.Overlapping;
                    }
                }

                return SegmentIntersectionType.NoIntersection;
            }



            public enum SegmentIntersectionType
            {
                NoIntersection,
                IntersectionOnTheMiddle,
                IntersectionFromOneEnd,
                IntersectionFromTwoEnd,
                Overlapping
            }
        }

        private struct SurgerySegment
        {
            public readonly int holeVertIndex;
            public readonly int vertIndex;

            public SurgerySegment(int holeVertIndex, int vertIndex)
            {
                this.holeVertIndex = holeVertIndex;
                this.vertIndex = vertIndex;
            }
        }
    }
}