using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addin.controls.renderer
{
    public class BGDIGraphics : BGraphics
    {
        private Graphics mGraphics;

        public BGDIGraphics(Graphics graphics)
        {
            mGraphics = graphics;
        }
    }
}
