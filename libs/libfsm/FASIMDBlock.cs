namespace libfsm
{
    struct FASIMDBlock
    {
        public int Count { get; }

        public FASIMDBlock(ushort[] data, int targetLength) 
        {
            Count = data.Length;
        }
    }
}
