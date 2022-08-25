using LLParserLexerLib;
using System;
using System.Text;
using static LLParserLexerLib.NFA;

namespace Tuyin.IR.Reflection.Symbols
{
    partial class DIMetadataParser
    {
        internal Metadatas Parse(LexReader rd)
        {
            this.init(rd);
            var v = this.metadatas(null);
            v.FileName = rd.FileName; 
            return v;
        }

        private void ReadString(LexReader rd, StringBuilder sb)
        {
            int last = 0;
            for (; ; )
            {
                int ch = rd.Read().ch;
                if (ch == -1) throw new Exception("EOF in string");

                if (last != '\\' && ch == '\'')
                    break;

                if (ch != '\\')
                    sb.Append((char)ch);

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

        private SourceSpan GetSpan(params ISourceSpan[] tracks)
        {
            return new SourceSpan(tracks[0].StartIndex, tracks[^1].EndIndex);
        }
    }
}
