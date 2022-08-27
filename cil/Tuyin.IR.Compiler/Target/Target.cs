using LLParserLexerLib;
using System.Text;
using Tuyin.IR.Compiler.Parser;
using Tuyin.IR.Compiler.Parser.Expressions;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Symbols;
using System.Linq;
using Type = Tuyin.IR.Reflection.Types.Type;

namespace Tuyin.IR.Compiler.Target
{
    internal class Target
    {
        public Target(string fileName, TargetLanguage targetLanguage, Lexicon lexicon, Path @namespace, ImportList imports, DeclareList declares)
        {
            FileName = fileName;
            TargetLanguage = targetLanguage;
            Lexicon = lexicon;
            Namespace = @namespace;
            Imports = imports;
            Declares = declares;
        }

        public TargetLanguage TargetLanguage { get; }

        public string FileName { get; }

        public Lexicon Lexicon { get; }

        public Path Namespace { get; }

        public ImportList Imports { get; }

        public DeclareList Declares { get; }

        internal ProductionBase FindProduction(TokenAST id)
        {
            return Declares.Where(x => x.DeclareType == DeclareType.Match).Cast<MatchDecl>().FirstOrDefault(x => x.Identifier.strRead.Equals(id.strRead))?.Production;
        }

        internal Module ToIR()
        {
            var module = new Module(
                FileName,
                Namespace.ToStrings(),
                Declares.Where(x => x.DeclareType == DeclareType.Function).Cast<FuncDecl>().Select(x => x.ToIR())).Import(Imports.Select(x => new Reflection.Import(x.Path.ToStrings(), new Reflection.Instructions.Identifier(x.Path[0].strRead))));

            return module;
        }

        public static Target Parse(string fileName)
        {
            using (var lexer = new LexReader(fileName))
                return new TargetParser().Parse(lexer);
        }

        public static Target ParseSource(string source)
        {
            using (var lexer = new LexReader(new StringReader(source), String.Empty))
                return new TargetParser().Parse(lexer);
        }
    }

    partial class TargetParser
    {
        private Dictionary<string, Token> mTokens;

        public Lexicon Lexicon { get; }

        public string FileName { get; private set; }

        public TargetLanguage TargetLanguage { get; private set; }

        public TargetParser() 
            : base(0) 
        {
            Lexicon = new Lexicon();
            mTokens = new Dictionary<string, Token>();
        }

        internal Target Parse(LexReader rd)
        {
            FileName = rd.FileName;

            this.init(rd);
            var v = this.target(null);
            var frags = v.Declares.Where(x => x.DeclareType == DeclareType.Match).Cast<MatchDecl>().SelectMany(x => x.Production.Scan(x => 
                (x as ReferenceMatch).FindProduction(v), 
                x => x is ReferenceMatch)).ToArray();

            return v;
        }

        private Token GetLiterallToken(TokenAST token) 
        {
            var ctx = token.tokenStr;
            if (!mTokens.ContainsKey(ctx))
                mTokens[ctx] = Lexicon.DefineToken(RegularExpression.Literal(ctx));

            return mTokens[ctx];
        }

        private Token GetRegexToken(TokenAST token)
        {
            var ctx = token.tokenStr;
            if (!mTokens.ContainsKey(ctx))
            {
                // parse regex 
                using (LexReader rd = new LexReader(new StringReader(ctx), FileName)) 
                {
                    RegexParser rf = new RegexParser(token.StartIndex);
                    var regex = rf.Parse(rd);
                    mTokens[ctx] = Lexicon.DefineToken(regex.Expression);
                }
            }

            return mTokens[ctx];
        }

        private void ReadRegex(LexReader rd, StringBuilder sb)
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

    static class ParserFileHelper 
    {
        public static SourceProduction SetSpan(this ProductionBase production, params ISourceSpan[] tracks) 
        {
            return new SourceProduction(new SourceSpan(tracks[0].StartIndex, tracks[^1].EndIndex), production);
        }

        public static SourceType SetSpan(this Type type, params ISourceSpan[] tracks)
        {
            return new SourceType(tracks.Length == 0 ? default : new SourceSpan(tracks[0].StartIndex, tracks[^1].EndIndex), type);
        }
    }
}