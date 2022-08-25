using Tuyin.IR.Compiler.Parser.Expressions;

namespace Tuyin.IR.Compiler.Parser
{
    class Token : IComparable<Token>, IComparable
    {
        public ushort Index 
        {
            get; 
        }

        public RegularExpression Expression 
        {
            get;
        }

        public bool IsClearly 
        {
            get { return ClearString != null; }
        }

        public bool Completable 
        {
            get;
            private set;
        }

        public string ClearString 
        {
            get;
        }

        public string SnippetString 
        {
            get;
            private set;
        }

        public string Description 
        {
            get;
        }

        public int MinCount 
        {
            get;
        }

        public int MaxCount 
        {
            get;
        }

        public bool IsComment 
        {
            get;
            private set;
        }

        public TokenColor? Color 
        {
            get;
        }

        internal bool IsSkip
        {
            get;
            set;
        }

        public Token(ushort index, RegularExpression regex, string descrption, TokenColor? color)
        {
            Index = index;
            Expression = regex;
            Description = descrption ?? Expression.GetDescrption();
            Color = color;
            ClearString = regex.GetClearString();
            MinCount = regex.GetMinLength();
            MaxCount = regex.GetMaxLength();
        }

        public ProductionBase Priority(byte priority)
        {
            return this.AsTerminal().Priority(priority);
        }

        public static ProductionBase operator |(Token left, Token right)
        {
            return left.AsTerminal() | right.AsTerminal();
        }

        public static ProductionBase operator |(ProductionBase left, Token right)
        {
            return left | right.AsTerminal();
        }

        public static ProductionBase operator |(Token left, ProductionBase right)
        {
            return left.AsTerminal() | right;
        }

        public static ProductionBase operator &(Token left, Token right)
        {
            return left.AsTerminal() & right.AsTerminal();
        }

        public static ProductionBase operator &(ProductionBase left, Token right)
        {
            return left & right.AsTerminal();
        }

        public static ProductionBase operator &(Token left, ProductionBase right)
        {
            return left.AsTerminal() & right;
        }

        public int CompareTo(Token other)
        {
            return Index.CompareTo(other.Index);
        }

        public int CompareTo(object obj)
        {
            if (!(obj is Token))
                return 0;

            return CompareTo(obj as Token);
        }

        public override string ToString()
        {
            return Description;
        }

        public Token Complete()
        {
            Completable = true;
            return this;
        }

        public Token Snippet(string snippet) 
        {
            Completable = true;
            SnippetString = snippet;
            return this;
        }

        public Token MarkComment() 
        {
            IsComment = true;
            return this;
        }
    }
}
