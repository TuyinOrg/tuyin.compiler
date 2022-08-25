namespace Tuyin.IR.Reflection.Types
{
    public class MutableType : Type
    {
        public override string Name { get; }

        public MutableType(string name)
        {
            Name = name;
        }
    }

}
