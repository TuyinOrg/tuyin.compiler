namespace libfsm
{
    public struct FAValue<T>
    {
        public ushort   Shift   
        {
            get;
        }

        public T        Token   
        {
            get;
        }

        public FAValue(ushort shift, T token)
        {
            Shift = shift;
            Token = token;
        }

        public override int GetHashCode()
        {
            return Shift ^ Token.GetHashCode();
        }
    }
}
