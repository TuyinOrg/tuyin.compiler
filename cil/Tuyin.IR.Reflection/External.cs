using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Reflection
{
    public abstract class External : IExternal
    {
        public Identifier Identifier { get; }

        public External(Identifier identifier) 
        {
            Identifier = identifier;
        }
    }
}
