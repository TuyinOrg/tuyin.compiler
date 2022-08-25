using System.Collections.Generic;
using System.Linq;

namespace compute.drawing
{
    public class Path : IGeometry
    {
        public IList<IGeometry> Geometries { get; }

        public IList<IGeometry> Holes { get; }

        public IEnumerable<GeometryData> GetGeometryDatas()
        {
            var first = Geometries.SelectMany(x => x.GetGeometryDatas());
            var second = Holes.SelectMany(x => x.GetGeometryDatas());
            foreach (var item in second)
                item.Type = GeometryDataType.Hole;

            return first.Union(second);
        }
    }
}
