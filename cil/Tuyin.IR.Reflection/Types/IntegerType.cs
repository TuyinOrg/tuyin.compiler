namespace Tuyin.IR.Reflection.Types
{
    public class IntegerType : Type
    {
        public override string Name => $"i{BitsSize}";

        public int Numerator { get; }

        public bool Sign { get; }

        public override uint BitsSize => (uint)Numerator;

        public IntegerType(int numerator, bool sign)
        {
            Numerator = numerator;
            Sign = sign;
        }

        public IntegerType(int numerator)
        {
            Numerator = numerator;
        }
    }

}
