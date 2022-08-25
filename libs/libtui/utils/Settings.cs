namespace libtui.utils
{
    static class Settings
    {
        public const string Verstion = "libtui(1.0.0.0)";
        public const float MaxFloat = 3.402823466e+38f;
        public const float Epsilon = 1.192092896e-07f;
        public const float Pi = 3.14159265359f;
        public static int MaxPolygonVertices = int.MaxValue;
        public const bool SkipSanityChecks = false;

        public const float SufaceUpdateTime = 1000f / 60;
        public const int LoopSleepTime = 1;
    }
}
