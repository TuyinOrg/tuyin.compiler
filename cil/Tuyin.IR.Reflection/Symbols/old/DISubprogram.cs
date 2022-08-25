namespace Tuyin.IR.Reflection.Symbols.old
{
    public sealed class DISubprogram : DIBlock
    {
        public bool IsFixed { get; set; }

        public string Name { get; }

        public string LinkageName { get; }

        public DIBlock Scope { get; }

        public bool IsLocal { get; }

        public bool IsDefinition { get; }

        public DIFile File { get; }

        public int Line { get; }

        public int ScopeLine { get; set; }

        public DISubroutineType Type { get; }

        public DICompileUnit Unit { get; }

        public DISubprogram(DICompileUnit unit, string name, string linkageName, DIBlock scope, DIFile file, int line, int scopeLine, DISubroutineType type, bool isLocal, bool isDefinition)
        {
            Name = name;
            LinkageName = linkageName;
            Scope = scope;
            File = file;
            Line = line;
            ScopeLine = scopeLine;
            Type = type;
            IsLocal = isLocal;
            IsDefinition = isDefinition;
            Unit = unit;
        }

        public override string Parse(DIMetadataManager manager)
        {
            return
                $"distinct !DISubprogram(" +
                $"name: \"{Name}\"," +
                $"linkageName: \"{LinkageName}\"," +
                $"scope: {manager.GetReference(Scope)}," +
                $"file: {manager.GetReference(File)}," +
                $"line: {Line}," +
                $"scopeLine: {ScopeLine}," +
                $"type: {manager.GetReference(Type)}," +
                $"isLocal: {(IsLocal ? "true" : "false")}," +
                $"isDefinition: {(IsDefinition ? "true" : "false")}," +
                //$"containingType: !4," +
                //$"virtuality: DW_VIRTUALITY_pure_virtual," +
                //$"virtualIndex: 10," +
                //$"flags: DIFlagPrototyped," +
                //$"isOptimized: {(IsOptimized ? "true" : "false")}," +
                $"unit: {manager.GetReference(Unit)})";
            //$"templateParams: !6," +
            //$"declaration: !7," +
            //$"retainedNodes: !8," +
            //$"thrownTypes: !9)";
        }
    }
}
