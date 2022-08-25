using System;
using System.Collections.Generic;
using System.Text;

namespace libfsm
{
    /// <summary>
    /// 操作日志
    /// 用于记录如何一步一步从原始图转变成当前图的操作
    /// </summary>
    public struct FABuildStep<T>
    {
        public FABuildStep(FABuildStage stage, FABuildType type, FATransition<T> transition)
        {
            Stage = stage;
            Type = type;
            Transition = transition;
        }

        public FABuildStage Stage { get; }

        public FABuildType Type { get; }

        public FATransition<T> Transition { get; }
    }
}
