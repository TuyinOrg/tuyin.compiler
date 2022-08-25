namespace Toe.SPIRV.Reflection.Nodes
{
    internal static partial class INodeWithNextExtensionMethods
    {
        internal static T Then<T>(this INodeWithNext prevNode, T node) where T: ExecutableNode
        {
            prevNode.Next = node;
            return node;
        }
    }
}