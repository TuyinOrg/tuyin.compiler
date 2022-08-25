namespace Tuyin.IR.Reflection
{
    public enum AstNodeType
    {
        // 声明
        Metadata,
        Import,
        Type,
        Struct,
        Function,

        // 控制流
        Goto,
        Test,
        Call,
        Return,

        // 引用
        Integer,
        Float,
        String,
        Literal,
        Pointer,
        Member,
        Element,
        Identifier,

        // 内存
        Store,

        // 表达式
        Add,
        And,
        Div,
        Equal,
        GreaterThen,
        LeftShift,
        LessThen,
        Mul,
        Neg,
        Or,
        Rem,
        RightShift,
        Sub,
        Xor,

        // 其他
        Custom
    }
}
