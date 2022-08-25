namespace Tuyin.IR.Compiler.Parser.Generater
{
    partial class GraphEdgeValue
    {
        public static readonly GraphEdgeValue Any = new AnyClass();

        class AnyClass : GraphEdgeValue
        {
            public override bool Xor => true;

            public AnyClass() 
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
