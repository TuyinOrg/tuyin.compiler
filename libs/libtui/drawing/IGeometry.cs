using System.Collections.Generic;

namespace libtui.drawing
{
    public interface IGeometry
    {
        IEnumerable<GeometryData> GetGeometryDatas();
    }
}
