namespace Tuyin.IR.Reflection.Symbols.old
{
    public sealed class DINamespace : DIMetadata
    {
        public string Name { get; }

        public DIBlock Scope { get; }

        public DIFile File { get; }

        public int Line { get; }

        public DINamespace(string name, DIBlock scope, DIFile file, int line)
        {
            Name = name;
            Scope = scope;
            File = file;
            Line = line;
        }

        public override string Parse(DIMetadataManager manager)
        {
            return $"!DINamespace(name: \"{Name}\", scope: {manager.GetReference(Scope)}, file: {manager.GetReference(File)}, line: {Line})";
        }
    }
}
