namespace Tuyin.IR.Reflection.Symbols.old
{
    public sealed class DICompileUnit : DIMetadata
    {
        /// <summary>
        /// 获得编译版本信息
        /// </summary>
        public string Producer
        {
            get;
        }

        /// <summary>
        /// 获得编译语言
        /// </summary>
        public string Language
        {
            get;
        }

        /// <summary>
        /// 是否进行过优化
        /// </summary>
        public bool IsOptimized
        {
            get;
        }

        /// <summary>
        /// 获得运行器版本
        /// </summary>
        public uint RuntimeVersion
        {
            get;
        }

        /// <summary>
        /// 隶属文件
        /// </summary>
        public DIFile File
        {
            get;
        }

        public DICompileUnit(string lang, DIFile file, string producer, bool isOptimized, uint runtimeVersion)
        {
            File = file;
            Producer = producer;
            Language = lang;
            IsOptimized = isOptimized;
            RuntimeVersion = runtimeVersion;
        }

        public override string Parse(DIMetadataManager manager)
        {
            return $"distinct !DICompileUnit(" +
                $"language: {Language}, " +
                $"file: {manager.GetReference(File)}, " +
                $"producer: \"{Producer}\", " +
                $"isOptimized: {(IsOptimized ? "true" : "false")}, " +
                $"runtimeVersion: {RuntimeVersion}, " +
                $"emissionKind: FullDebug, " +
                $"enums: !{{}}, retainedTypes: !{{}}, globals: !{{}}, imports: !{{}})";
        }
    }
}
