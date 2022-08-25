using OpenCL.Bindings;

namespace OpenCL
{
    /// <summary>
    /// Represents an event created by an external library
    /// </summary>
    class ComputeExternalEvent : ComputeEvent
    {
        /// <summary>
        /// Creates an see<see cref="ComputeExternalEvent"/> from an external handle
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="queue"></param>
        public ComputeExternalEvent(CLEventHandle handle, ComputeCommandQueue queue)
            : base(handle, queue)
        {
        }
    }
}