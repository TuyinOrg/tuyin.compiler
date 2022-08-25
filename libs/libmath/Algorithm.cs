namespace libmath
{
    abstract class Algorithm<TResult, TArg>
    {
        public abstract TResult Compute(TArg arg);
    }
}