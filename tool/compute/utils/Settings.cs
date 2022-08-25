namespace compute.utils
{
    static class Settings
    {
        public const float MaxFloat = 3.402823466e+38f;
        public const float Epsilon = 1.192092896e-07f;
        public const float Pi = 3.14159265359f;
        public static int MaxPolygonVertices = int.MaxValue;

        /// <summary>
        /// Set this to true to skip sanity checks in the engine. This will speed up the
        /// tools by removing the overhead of the checks, but you will need to handle checks
        /// yourself where it is needed.
        /// </summary>
        public const bool SkipSanityChecks = false;
    }
}
