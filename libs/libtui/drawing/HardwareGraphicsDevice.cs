using libtui.utils;
using System;
using System.Linq;

namespace libtui.drawing
{
    internal class HardwareGraphicsDevice : IGraphicDevice
    {
        private Surface mSurface;

        public HardwareGraphicsDevice(Surface surface) 
        {
            mSurface = surface;
        }

        public void DrawGeometry(Pen pen, IGeometry geo)
        {
            throw new NotImplementedException();
        }

        public void FillGeometry(Brush brush, IGeometry geo)
        {
            var rgb = (brush as SolidBrush).Color.ToVector3();
            mSurface.UpdateBuffers(geo.GetGeometryDatas().SelectMany(x => x.Points.Select(y => new Vertex(new System.Numerics.Vector3(y.X, y.Y, 0), rgb, new System.Numerics.Vector2(0, 0)))).ToArray());
        }

        public void DrawString(string ctx, Font font, Brush brush, int x, int y)
        {
            throw new NotImplementedException();
        }

        public SizeF MeasureString(string text, Font font)
        {
            throw new NotImplementedException();
        }

        public void ResetClip()
        {
            throw new NotImplementedException();
        }

        public void SetClip(IGeometry geo)
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            mSurface.Flush();
        }
    }
}
