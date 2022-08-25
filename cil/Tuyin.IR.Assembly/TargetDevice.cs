namespace Tuyin.IR.Assembly
{
    /// <summary>
    /// 目标设备
    /// 包含目标架构寄存器，加法、除法等计算器(ALU)信息
    /// </summary>
    class TargetDevice
    {
        /// <summary>
        /// 目标架构
        /// </summary>
        public TargetArch TargetArch { get; }

        /// <summary>
        /// 是否支持递归
        /// </summary>
        public bool SupportRecursive { get; }
    }
}
