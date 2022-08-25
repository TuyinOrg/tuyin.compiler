using System.Drawing;
using System.Drawing.Drawing2D;

namespace addin.controls.renderer
{
    public class BColorGradient : BUIElement
    {
        private Color mColor;

        private PointF[] mPoints;
        private Color[] mBarColors;

        public Color Color
        {
            get { return mColor; }
            set
            {
                if (mColor != value)
                {
                    
                    mColor = value;
                }
            }
        }

        public BColorGradient(IBControl host)
            : base(host)
        {
        }

        private void InitBarColors(Color color) 
        {

        }

        Brush CreateGradientBrush()
        {
            Brush result;

            if (false)
            {
                result = new PathGradientBrush(new PointF[0], WrapMode.Clamp)
                {
                    CenterPoint = new PointF(Size.Width / 2, Size.Height / 2),
                    CenterColor = Color.White,
                    //SurroundColors = _colors
                };
            }
            else
            {
                result = null;
            }

            return result;
        }
    }
}
