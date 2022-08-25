namespace Tuyin.IR.Reflection.Symbols.old
{
    internal class DIMetadataScope : DIMetadata
    {
        public int Start { get; }

        public int Length { get; }

        public DIMetadataScope Parent { get; }

        public DIMetadataScope(DIMetadataScope parent, int start, int length)
        {
            Parent = parent;
            Start = start;
            Length = length;
        }

        public override string Parse(DIMetadataManager manager)
        {
            return $"!DIScope({(Parent == null ? "nil" : manager.GetIdent(this))},{Start},{Length})";
        }
    }
}
