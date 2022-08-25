namespace Tuyin.IR.Reflection.Symbols.old
{
    public class DILexicalBlock : DIBlock
    {
        public DIBlock Scope { get; }

        public DIFile File { get; }

        public int Line { get; }

        public int Column { get; }

        public DILexicalBlock(DIBlock scope, DIFile file, int line, int column)
        {
            Scope = scope;
            File = file;
            Line = line;
            Column = column;
        }

        public override string Parse(DIMetadataManager manager)
        {
            return $"distinct !DILexicalBlock(scope: {manager.GetReference(Scope)}, file: {manager.GetReference(File)}, line: {Line}, column: {Column})";
        }
    }
}
