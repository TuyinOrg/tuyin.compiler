namespace Tuyin.IR.Analysis
{
    public class EnvironmentSettings
    {
        internal static readonly EnvironmentSettings Default = new EnvironmentSettings();

        /// <summary>
        /// 是否使用WASM
        /// </summary>
        public bool SupportWASM { get; set; } = true;

        /// <summary>
        /// 是否使用WASI
        /// </summary>
        public bool SupportWASI { get; set; } = true;

        /// <summary>
        /// 是否使用Vulkan
        /// </summary>
        public bool SupportVualkan { get; set; } = true;

        /// <summary>
        /// 是否使用解释器
        /// </summary>
        public bool Interpreter { get; set; } = false;

        /// <summary>
        /// 常量优化（传播/折叠）
        /// </summary>
        public bool ConstantOptimize { get; set; } = true;

        /// <summary>
        /// 清理无效文法
        /// </summary>
        public bool ClearInvaildStatment { get; set; } = true;

        /// <summary>
        /// 是否清理无效成员
        /// </summary>
        public bool ClearInvaildMember { get; set; } = true;
    }
}
