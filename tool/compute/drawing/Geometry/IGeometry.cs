using System.Collections.Generic;

namespace compute.drawing
{
    public interface IGeometry
    {
        IEnumerable<GeometryData> GetGeometryDatas();
    }
}
