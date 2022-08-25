using LLParserLexerLib;
using System.Globalization;
using System.Text;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprUnicode : ExprNum
    {
        private TokenAST nt1_s;

        public override AstNodeType AstType => AstNodeType.ExprUnicode;

        internal ExprUnicode(SourceSpan sourceSpan, TokenAST nt1_s)
            : base(sourceSpan)
        {
            this.nt1_s = nt1_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Integer(int.Parse(EscapeChars(cache, nt1_s.strRead, 0, nt1_s.strRead.Length, new SourceSpan(nt1_s.StartIndex, nt1_s.EndIndex))));
        }

        internal static string EscapeChars(StatmentBuilder cache, string source, int index, int length, SourceSpan span)
        {
            StringBuilder sb = new StringBuilder();
            for (var pos = index; pos < length; pos++)
            {
                char c = source[pos];
                if (c != '\\')
                    continue;

                pos++;
                if (pos >= length)
                {
                    cache.ReportError(
                        Errors.MissingEscape,
                        new SourceSpan(span.StartIndex + pos, span.StartIndex + pos + 1));

                    break;
                }

                switch (source[pos])
                {
                    case 'n':
                        c = '\n';
                        break;
                    case 'r':
                        c = '\r';
                        break;
                    case '\'':
                        c = '\'';
                        break;
                    case '\"':
                        c = '\"';
                        break;
                    case '\\':
                        c = '\\';
                        break;
                    case '0':
                        c = '\0';
                        break;
                    case 'a':
                        c = '\a';
                        break;
                    case 'b':
                        c = '\b';
                        break;
                    case 'f':
                        c = '\f';
                        break;
                    case 't':
                        c = '\t';
                        break;
                    case 'v':
                        c = '\v';
                        break;
                    case 'x':
                        var hex = new StringBuilder(4);
                        pos++;
                        if (pos >= length)
                        {
                            cache.ReportError(
                                Errors.MissingEscape,
                                new SourceSpan(span.StartIndex + pos, span.StartIndex + pos + 1));

                            break;
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            c = source[pos];
                            if (!(char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F')))
                                break;

                            hex.Append(c);
                            pos++;
                            if (pos > length)
                                break;
                        }

                        if (hex.Length == 0)
                        {
                            cache.ReportError(
                                Errors.UnrecognizedEscape,
                                new SourceSpan(span.StartIndex + pos, span.StartIndex + pos + 1));

                            break;
                        }

                        c = (char)Int32.Parse(hex.ToString(), NumberStyles.HexNumber);
                        pos--;
                        break;
                    case 'u':

                        if (pos + 4 >= length)
                        {
                            cache.ReportError(
                                Errors.UnrecognizedEscape,
                                new SourceSpan(span.StartIndex + pos, span.StartIndex + pos + 4));

                            break;
                        }

                        try
                        {
                            uint charValue = UInt32.Parse(source.Substring(pos + 1, 4), NumberStyles.HexNumber);
                            c = (char)charValue;
                            pos += 4;
                        }
                        catch (Exception)
                        {
                            cache.ReportError(
                                Errors.UnrecognizedEscape,
                                new SourceSpan(span.StartIndex + pos, span.StartIndex + pos + 4));

                            break;
                        }

                        break;
                    case 'U':

                        if (pos + 8 >= length)
                        {
                            cache.ReportError(
                                Errors.UnrecognizedEscape,
                                new SourceSpan(span.StartIndex + pos, span.StartIndex + pos + 8));

                            break;
                        }

                        try
                        {
                            uint charValue = UInt32.Parse(source.Substring(pos + 1, 8), NumberStyles.HexNumber);
                            if (charValue > 0xffff)
                            {
                                cache.ReportError(
                                    Errors.UnrecognizedEscape,
                                    new SourceSpan(span.StartIndex + pos, span.StartIndex + pos + 8));
                            }

                            c = (char)charValue;
                            pos += 8;
                        }
                        catch (Exception)
                        {
                            cache.ReportError(
                                Errors.UnrecognizedEscape,
                                new SourceSpan(span.StartIndex + pos, span.StartIndex + pos + 8));

                            break;
                        }

                        break;
                    default:
                        cache.ReportError(
                            Errors.UnrecognizedEscape,
                            new SourceSpan(span.StartIndex + pos, span.StartIndex + pos + 1));

                        break;
                }

                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}