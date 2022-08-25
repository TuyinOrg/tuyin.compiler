using Tuyin.IR.Compiler.Parser.Generater;
using System.Globalization;

namespace Tuyin.IR.Compiler.Parser.Expressions
{
    public abstract class RegularExpression
    {
        private static readonly HashSet<UnicodeCategory> sLettersCategories = new HashSet<UnicodeCategory>()
        {
            UnicodeCategory.LetterNumber,
            UnicodeCategory.LowercaseLetter,
            UnicodeCategory.ModifierLetter,
            UnicodeCategory.OtherLetter,
            UnicodeCategory.TitlecaseLetter,
            UnicodeCategory.UppercaseLetter
        };

        private static readonly string sExceptString = string.Empty;

        static RegularExpression() 
        {
            var chars = new List<char>();
            for (var i = char.MinValue; i < char.MaxValue; i++)
            {
                if (!sLettersCategories.Contains(Char.GetUnicodeCategory(i))) 
                {
                    chars.Add(i);
                }
            }
            sExceptString = new String(chars.ToArray());
        }

        protected internal RegularExpression() 
        {
        }

        internal bool IsOptional { get; set; }

        internal abstract RegularExpressionType ExpressionType { get; }

        internal abstract int GetMinLength();

        internal abstract int GetMaxLength();

        internal abstract string GetClearString();

        internal abstract GraphEdgeStep<TMetadata> CreateGraphState<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> step, TMetadata metadata) where TMetadata : struct;

        public abstract string GetDescrption();

        public RegularExpression Many() 
        {
            if (ExpressionType == RegularExpressionType.Repeat)
                return this;

            return new RepeatExpression(this).Optional();
        }

        public RegularExpression Many1() 
        {
            return this > this.Many();
        }

        public RegularExpression Optional() 
        {
            this.IsOptional = true;

            return new EmptyExpression() | this;
        }

        internal abstract int RepeatLevel();

        public static RegularExpression Any() 
        {
            return new CharSetExpression(new GraphEdgeValue(true, '\0'));
        }

        public static RegularExpression Symbol(char c) 
        {
            return new SymbolExpression(c);
        }

        public static RegularExpression CharSet(string literal) 
        {
            return new CharSetExpression(literal.ToCharArray());
        }

        public static RegularExpression CharSet(IEnumerable<char> set) 
        {
            return new CharSetExpression(set.ToArray());
        }

        public static RegularExpression Literal(string literal) 
        {
            if (string.IsNullOrEmpty(literal))
                throw new NotImplementedException();

            if(literal.Length == 1)
                return new SymbolExpression(literal[0]);

            SymbolExpression[] symbols = new SymbolExpression[literal.Length];
            for (var i = 0; i < literal.Length; i++)
                symbols[i] = new SymbolExpression(literal[i]);

            return new ConcatenationExpression(symbols);
        }

        public static RegularExpression Range(char start, char end) 
        {
            var chars = new char[end - start + 1];
            for (var i = start; i <= end; i++)
                chars[i - start] = i;

            return new CharSetExpression(chars);
        }

        public static RegularExpression Except(params char[] symbol) 
        {
            return new CharSetExpression(new GraphEdgeValue(true, symbol.Concat(new char[] { '\0' })));
        }

        public static RegularExpression Until(string literal) 
        {
            return new ExceptExpression(literal, "\0");
        }

        public static RegularExpression operator |(RegularExpression left, RegularExpression right)
        {
            var combineExp = left.Combine(right);
            if (combineExp != null)
                return combineExp;

            return new OrExpression(left, right);
        }

        protected virtual  RegularExpression Combine(RegularExpression right)
        {
            return null;
        }

        public static RegularExpression operator >(RegularExpression left, RegularExpression right)
        {
            return new ConcatenationExpression(left, right);
        }

        public static RegularExpression operator <(RegularExpression left, RegularExpression right)
        {
            return new ConcatenationExpression(right, left);
        }

        public static RegularExpression operator |(string left, RegularExpression right)
        {
            return new OrExpression(Literal(left), right);
        }

        public static RegularExpression operator >(string left, RegularExpression right)
        {
            return new ConcatenationExpression(Literal(left), right);
        }

        public static RegularExpression operator <(string left, RegularExpression right)
        {
            return new ConcatenationExpression(right, Literal(left));
        }

        public static RegularExpression operator |(char left, RegularExpression right)
        {
            return new OrExpression(Symbol(left), right);
        }

        public static RegularExpression operator >(char left, RegularExpression right)
        {
            return new ConcatenationExpression(Symbol(left), right);
        }

        public static RegularExpression operator <(char left, RegularExpression right)
        {
            return new ConcatenationExpression(right, Symbol(left));
        }

        public static RegularExpression operator |(RegularExpression left, string right)
        {
            return new OrExpression(left, Literal(right));
        }

        public static RegularExpression operator >(RegularExpression left, string right)
        {
            return new ConcatenationExpression(left, Literal(right));
        }

        public static RegularExpression operator <(RegularExpression left, string right)
        {
            return new ConcatenationExpression(Literal(right), left);
        }

        public static RegularExpression operator |(RegularExpression left, char right)
        {
            return new OrExpression(left, Symbol(right));
        }

        public static RegularExpression operator >(RegularExpression left, char right)
        {
            return new ConcatenationExpression(left, Symbol(right));
        }

        public static RegularExpression operator <(RegularExpression left, char right)
        {
            return new ConcatenationExpression(Symbol(right), left);
        }

    }
}
