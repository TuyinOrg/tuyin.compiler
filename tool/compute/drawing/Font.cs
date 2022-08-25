using compute.environment;
using System;
using System.Collections.Generic;
using System.Text;

namespace compute.drawing
{
    public sealed class Font
    {
        private VulkanFont mVFont;

        internal Font(VulkanFont font) 
        {
            mVFont = font;
        }


    }
}
