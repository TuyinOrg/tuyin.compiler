namespace Tuyin.IR.Reflection.Symbols
{
    public struct DIToken
    {
        public string String;
        public int Index;
        public int Length;

        public DIToken(string @string, int index, int length)
        {
            String = @string;
            Index = index;
            Length = length;
        }
    }
}
