using System;

namespace libgraph
{
    public abstract class GraphException : Exception
    {
        protected GraphException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
