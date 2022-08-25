namespace Tuyin.IR.Reflection.Symbols.old
{
    public class DILocalVariable : DIMetadata
    {
        public string Name { get; }

        public int? Arg { get; }

        public int Line { get; }

        public DIBlock Scope { get; set; }

        public DIFile File { get; }

        public DIBasicType Type { get; }

        public DILocalVariable(string name, DIBlock scope, DIFile file, int line, DIBasicType type, int? arg)
        {
            Name = name;
            Scope = scope;
            Arg = arg;
            File = file;
            Line = line;
            Type = type;
        }

        public override string Parse(DIMetadataManager manager)
        {
            return $"!DILocalVariable(name: \"{Name}\", scope: {manager.GetReference(Scope)}, file: {manager.GetReference(File)}, line: {Line}, type: {manager.GetReference(Type)}" + (Arg.HasValue ? $" , arg: {Arg})" : ")");
        }
    }
}
