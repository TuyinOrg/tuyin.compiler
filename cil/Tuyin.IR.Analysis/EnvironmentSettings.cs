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
    }
}
