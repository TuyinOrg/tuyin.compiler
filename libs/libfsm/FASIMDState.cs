namespace libfsm
{
    public struct FASIMDState 
    {
        public FASIMDState(int count, ushort shift)
        {
            Count = count;
            Shift = shift;
        }

        public int Count { get; }

        public ushort Shift { get; }
    }
}
