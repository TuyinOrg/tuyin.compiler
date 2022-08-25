namespace compute.utils
{
    class MeshOpations
    {
        public const double MIN_ANGLE = 1;

        public bool QualityMesh { get; set; } = true;

        public bool ConformingDelaunay { get; set; }

        public bool ConvexMesh { get; set; }

        public bool UseSweeplineAlgorithm { get; set; }

        public double MinimumAngle { get; set; } = 10;

        public double MaximumAngle { get; set; } = 180;

        public double MaximumArea { get; set; }

        public double Precision { get; set; } = 0.98f;
    }
}
