using System;

namespace libfsm
{
    [Flags]
    public enum FATableFlags
    {
        None                = 0,
        /// <summary>
        /// 连接到子集
        /// </summary>
        ConnectSubset       = 1,
        /// <summary>
        /// 不进行合并检测
        /// 注意:启用该项时会导致冲突解决策略无法进行
        /// </summary>
        NotMergeEquivalent  = 2,
        /// <summary>
        /// 启用连接拟合
        /// 可优化内存空间
        /// </summary>
        FitFragmentMerge    = 4,
        /// <summary>
        /// 启用连接冲突解决
        /// </summary>
        EdgeConflicts       = 8,
        /// <summary>
        /// 启用元数据冲突解决
        /// </summary>
        MetadataConflicts   = 16,
        /// <summary>
        /// 启用符号冲突解决
        /// </summary>
        SymbolConflicts     = 32,
        /// <summary>
        /// 允许无右侧连接作为结束点
        /// </summary>
        Allow0Rights        = 64,
        /// <summary>
        /// 是否开启优化
        /// </summary>
        Optimize            = 128,
        /// <summary>
        /// 在启用Optimize时才会进行内存布局优化
        /// </summary>
        MemoryLayout        = 256,

        /// <summary>
        /// 启用所有冲突解决
        /// </summary>
        ConflictResolution  = EdgeConflicts | SymbolConflicts | MetadataConflicts
    }
}
