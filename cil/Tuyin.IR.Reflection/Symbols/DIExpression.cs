namespace Tuyin.IR.Reflection.Symbols
{
    public abstract class DIExpression
    {
        public object Const { get; }

        public DIToken Token { get; }

        public DIExpression(object @const, DIToken token)
        {
            Const = @const;
            Token = token;
        }

        public override string ToString()
        {
            return Const.ToString();
        }
    }

    public class DIExpression<T> : DIExpression 
    {
        public DIExpression(T val, DIToken token)
            : base(val, token)
        {
        }
    }
}
