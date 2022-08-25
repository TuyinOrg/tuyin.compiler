namespace Tuyin.IR.Reflection.Symbols.old
{
    public class DILocation : DIMetadata
    {
        public DIBlock Scope { get; set; }

        public int CharIndex { get; }

        public int Line { get; }

        public int Column { get; }

        public DILocation(DIBlock scope, int charIndex, int line, int column)
        {
            CharIndex = charIndex;
            Scope = scope;
            Line = line;
            Column = column;
        }

        public override string Parse(DIMetadataManager manager)
        {
            return $"!DILocation(line: {Line}, column: {Column}, scope: {manager.GetReference(Scope)})";
        }
    }
}
