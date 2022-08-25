namespace libfsm
{

    public class FASMID
    {
        private FASIMDState[] mStateReadCount;

        internal FASMID(int stateCount) 
        {
            mStateReadCount = new FASIMDState[stateCount];
        }

        public FASIMDState this[ushort state] 
        {
            get { return mStateReadCount[state]; }
            internal set { mStateReadCount[state] = value; }
        }
    }
}
