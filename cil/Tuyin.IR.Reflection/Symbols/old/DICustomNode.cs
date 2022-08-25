namespace Tuyin.IR.Reflection.Symbols.old
{
    public class DICustomNode : DIMetadata
    {
        public string[] Tags { get; }

        public DICustomNode(params string[] tags)
        {
            Tags = tags;
        }

        public override string Parse(DIMetadataManager manager)
        {
            var val = string.Join(",", Tags);

            return $"!{{{val}}}";
        }
    }
}
