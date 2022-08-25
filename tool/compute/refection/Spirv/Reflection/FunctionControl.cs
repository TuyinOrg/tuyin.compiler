namespace Toe.SPIRV.Reflection
{
    internal enum FunctionControl
    {
        None = 0x0000,
        Inline = 0x0001,
        DontInline = 0x0002,
        Pure = 0x0004,
        Const = 0x0008,
    }
}