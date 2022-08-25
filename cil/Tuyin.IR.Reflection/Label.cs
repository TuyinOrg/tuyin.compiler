namespace Tuyin.IR.Reflection
{
    public class Label
    {
        public virtual int Index { get; internal set; }

        public Label()
        {
        }

        public override string ToString()
        {
            return "Label";
        }
    }
}
