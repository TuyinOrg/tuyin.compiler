using Tuyin.IR.Reflection.Instructions;
using Tuyin.IR.Reflection.Types;

namespace Tuyin.IR.Reflection
{
    public class Parameter : External
    {
        public Type Type { get; internal set; }

        public Parameter(Identifier identifier)
            : base(identifier)
        {
            Type = new AutoType();
        }
    }
}
