using compute.drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using TriangleNet;
using TriangleNet.Geometry;
using TriangleNet.Meshing;
using TriangleNet.Meshing.Algorithm;
using TriangleNet.Smoothing;
using TriangleNet.Tools;
using Polygon = TriangleNet.Geometry.Polygon;

namespace compute.utils
{
    static class GeometryTools
    {
        public static Mesh Delaunay(this IEnumerable<GeometryData> data, MeshOpations opations) 
        {
            var mesh = Triangulate(data, opations);
            //Refine(layout);
            //Smooth(layout);
            //Smooth(layout);
            return mesh;
        }

        public static Mesh Triangulate(IEnumerable<GeometryData> data, MeshOpations opations) 
        {
            if (data == null)
                throw new ArgumentException("region empty.");

            var options = new ConstraintOptions();
            var quality = new QualityOptions();
          
            if (opations.ConformingDelaunay)
                options.ConformingDelaunay = true;

            if (opations.QualityMesh)
            {
                quality.MinimumAngle = Math.Max(MeshOpations.MIN_ANGLE, opations.MinimumAngle);
                quality.MaximumAngle = opations.MaximumAngle;
                // Ignore area constraints on initial triangulation.

                //double area = slMaxArea.Value * 0.01;
                //if (area > 0 && area < 1)
                //{
                //    var size = input.Bounds;
                //    double min = Math.Min(size.Width, size.Height);
                //    mesh.SetOption(Options.MaxArea, area * min);
                //}
            }

            if (opations.ConvexMesh)
                options.Convex = true;

            Mesh mesh = null;
            var polygon = ToPolygon(data, opations);
            if (polygon.Points.Count > 2)
            {
                if (opations.UseSweeplineAlgorithm)
                    mesh = (Mesh)polygon.Triangulate(options, quality, new SweepLine());
                else
                    mesh = (Mesh)polygon.Triangulate(options, quality);
            }
            else throw new ArgumentException("region points less 2.");

            return mesh;
        }

        public static bool Refine(Mesh mesh, MeshOpations opations) 
        {
            if (mesh == null)
                throw new ArgumentException("mesh empty.");

            double area = opations.MaximumArea;

            var quality = new QualityOptions();

            if (area > 0 && area < 1)
            {
                var statistic = new Statistic();
                statistic.Update(mesh, 10);
                quality.MaximumArea = area * statistic.LargestArea;
            }

            quality.MinimumAngle = Math.Max(MeshOpations.MIN_ANGLE, opations.MinimumAngle);
            quality.MaximumAngle = opations.MaximumAngle;

            try
            {
                mesh.Refine(quality, opations.ConformingDelaunay);

                return true;
            }
            catch
            {
                opations.MinimumAngle++;
                if (opations.MinimumAngle >= opations.MaximumAngle - 20) 
                {
                    opations.MinimumAngle = opations.MaximumAngle - 20;
                    return false;
                }

                return Refine(mesh, opations);
            }
        }

        public static bool Smooth(Mesh mesh, MeshOpations opations) 
        {
            if (mesh == null)
                throw new ArgumentException("mesh empty.");


            if (!mesh.IsPolygon)
                return true;

            try
            {
                var smoother = new SimpleSmoother();
                smoother.Smooth(mesh, 20);

                return true;
            }
            catch 
            {
                if (Refine(mesh, opations))
                    return Smooth(mesh, opations);

                return false;
            }
        }

        public static Polygon ToPolygon(this IEnumerable<GeometryData> datas, MeshOpations opations) 
        {
            var polygon = new Polygon();

            var vertexs = datas.Where(x => x.Type == GeometryDataType.Vertex).Select(x => BoundaryTracing.Optimize(x.Points, opations.Precision)).Where(x => x.Count > 2);
            foreach (var vertex in vertexs)
            {
                var list = new Vertex[vertex.Count];
                for (int i = 0; i < vertex.Count; i++)
                {
                    GeometryPoint p = vertex[i];
                    list[i] = new Vertex(p.X, p.Y, 0, p.Attribute);
                }

                polygon.Add(new Contour(list));
            }

            var holes = datas.Where(x => x.Type == GeometryDataType.Hole).Select(x => BoundaryTracing.Optimize(x.Points, opations.Precision)).Where(x => x.Count > 2);
            foreach (var hole in holes) 
            {
                var list = new Vertex[hole.Count];
                for (int i = 0; i < hole.Count; i++)
                {
                    GeometryPoint p = hole[i];
                    list[i] = new Vertex(p.X, p.Y, 0, p.Attribute);
                }

                polygon.Add(new Contour(list), true);
            }
            
            return polygon;
        }


    }
}
