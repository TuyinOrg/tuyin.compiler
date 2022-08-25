namespace compute.drawing
{
    /// <summary>
    /// 通用计算资源
    /// 通常被用于在运行时编译成spirv进行使用
    /// </summary>
    /// <typeparam name="TInput">输入的静态资源接口</typeparam>
    /// <typeparam name="TOuput">输出的静态资源接口</typeparam>
    public interface IComputeResource<TInput, TOuput> : IResource where TInput : struct where TOuput : struct
    {
        /// <summary>
        /// 编译管道使用的计算资源
        /// </summary>
        byte[] Compile();
    }
}
