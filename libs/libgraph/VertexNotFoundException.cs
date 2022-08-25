using System;

namespace libgraph
{
    public class VertexNotFoundException : GraphException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="VertexNotFoundException"/> class.
        /// </summary>
        public VertexNotFoundException()
            : base("Vertex is not present in the libgraph.")
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="VertexNotFoundException"/> class.
        /// </summary>
        public VertexNotFoundException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }

    }
}