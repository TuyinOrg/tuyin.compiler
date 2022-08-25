namespace Tuyin.IR.Compiler.Parser.Generater
{
    partial class GraphEdgeValue
    {
        internal static readonly GraphEdgeValue Empty = new EmptyClass();

        class EmptyClass : GraphEdgeValue
        {
            public override bool Xor => false;

            public EmptyClass()
            {
                Chars = new char[0];
            }

            public override bool IsVaild()
            {
                return true;
            }
        }
    }
}
