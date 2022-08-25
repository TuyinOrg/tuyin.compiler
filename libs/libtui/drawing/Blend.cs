namespace libtui.drawing
{
    public sealed class Blend
    {
        float[] factors;
        float[] positions;

        public Blend()
        {
            factors = new float[1];
            positions = new float[1];
        }

        public Blend(int count)
        {
            factors = new float[count];
            positions = new float[count];
        }

        public float[] Factors
        {
            get
            {
                return factors;
            }
            set
            {
                factors = value;
            }
        }

        public float[] Positions
        {
            get
            {
                return positions;
            }
            set
            {
                positions = value;
            }
        }
    }

}
