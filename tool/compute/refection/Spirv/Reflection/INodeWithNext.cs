using Toe.SPIRV.Reflection.Nodes;

namespace Toe.SPIRV.Reflection
{
    internal interface INodeWithNext
    {
        ExecutableNode Next { get; set; }
    }
}