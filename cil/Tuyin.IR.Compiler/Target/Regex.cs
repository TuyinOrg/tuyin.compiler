using LLParserLexerLib;
using System.Text;
using Tuyin.IR.Compiler.Parser.Expressions;
using Tuyin.IR.Reflection;

namespace Tuyin.IR.Compiler.Target
{
    internal class Regex
    {
        public Regex(SourceExpression exp)
        {
            Expression = exp;
        }

        public SourceExpression Expression { get; }
    }

    partial class RegexParser 
    {
        public int StartIndex { get; }

        public RegexParser(int startIndex)
            : base(0)
        {
            StartIndex = startIndex;
        }

        internal Regex Parse(LexReader rd)
        {
            this.init(rd);
            var v = this.regex(null);
            return v;
        }

        private RegularExpression Literal(TokenAST token) 
        {
            return RegularExpression.Literal(token.tokenStr);
        }

        private RegularExpression CharSet(TokenAST token) 
        {
            return RegularExpression.CharSet(token.tokenStr);
        }

        private RegularExpression Range(TokenAST start, TokenAST end) 
        {
            var sctx = start.tokenStr;
            var ectx = end.tokenStr;

            var first = sctx[^1];
            var follow = ectx[0];

            var exp = RegularExpression.Range(first, follow);
            var left = sctx.Substring(0, sctx.Length - 1);
            if (left.Length > 0)
                exp = RegularExpression.Literal(left) > exp;

            var right = ectx.Substring(1, ectx.Length - 1);
            if (right.Length > 0)
                exp = exp > RegularExpression.Literal(right);

            return exp;
        }

        private RegularExpression Until(TokenAST token) 
        {
            return RegularExpression.Until(token.tokenStr);
        }

        private RegularExpression Except(TokenAST token) 
        {
            return RegularExpression.Except(token.tokenStr.ToArray());
        }

        private RegularExpression Repeat(SourceExpression exp, TokenAST count)
        {
            RegularExpression result = exp;

            if (!int.TryParse(count.tokenStr, out int c))
            {
                throw new NotImplementedException();
            }
            else
            {
                for (var i = 0; i < c; i++)
                    result = result > exp;
            }

            return result;
        }

        private void ReadLiteral(LexReader rd, StringBuilder sb)
        {
            const string flags = "|?*+~-.()[]{}\r\n\t\b";

            // 遇到标记符停止
            int last = 0;
            for (; ; )
            {
                char ch = (char)rd.Read().ch;
                if (ch == -1) throw new Exception("EOF in string");

                if (last != '\\' && flags.Contains(ch))
                    break;

                if (ch != '\\')
                    sb.Append(ch);

                last = ch;
            }
        }

        private void ReadComment(LexReader rd)
        {
            for (; ; )
            {
                if (rd.Peek().ch == -1) throw new Exception("EOF in comment");
                if (rd.Read().ch == '*' && rd.Peek().ch == '/')
                {
                    rd.Read();
                    break;
                }
            }
        }
    }

    static class RegexFileHelper
    {
        public static SourceExpression SetSpan(this RegularExpression regex, int startIndex, params ISourceSpan[] tracks)
        {
            var start = tracks[0].StartIndex + startIndex;
            var length = tracks[^1].EndIndex - tracks[0].StartIndex;
            return new SourceExpression(new SourceSpan(start, start + length), regex);
        }
    }
}