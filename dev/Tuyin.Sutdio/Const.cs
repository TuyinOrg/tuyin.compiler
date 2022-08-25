namespace Tuyin.Sutdio
{
    static class Const
    {
        public const string SCHEME = "studio";
        public const string HOST = "app";
        public const string NAME = $"{SCHEME}-{HOST}-resource";
        public const string BASEURL = $"{SCHEME}://{HOST}/";
        public static readonly string BASEROOT = $"{AppDomain.CurrentDomain.BaseDirectory}\\{HOST}\\";
    }
}
