namespace addin.controls.renderer
{
    public struct GridLength
    {
        public GridType Type { get; }

        public float Value { get; }

        public GridLength(GridType type, float value) 
        {
            Type = type;
            Value = value;
        }
    }
}
