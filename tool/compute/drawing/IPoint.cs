using System;
using System.Collections.Generic;
using System.Text;

namespace compute.drawing
{
    public interface IPoint<T> where T : struct
    {
        T X { get; }

        T Y { get; }
    }
}
