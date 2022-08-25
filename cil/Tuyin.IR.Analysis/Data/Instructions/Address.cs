using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Analysis.Data.Instructions
{
    internal class Address : Identifier
    {
        internal Address(Address parent, string identifier, int branchIndex, int useIndex)
              : base(parent != null ? $"{parent}[{identifier}.{branchIndex}.{useIndex}]" : $"{identifier}.{branchIndex}.{useIndex}")
        {
            Parent = parent;
            Identifier = identifier;
            BranchIndex = branchIndex;
            UseIndex = useIndex;
        }

        internal Address(string value)
            : base(value)
        {
            Parent = null;
            Identifier = value;
            BranchIndex = 0;
            UseIndex = 0;
        }

        public Address Parent { get; }

        public string Identifier { get; }

        public int BranchIndex { get; }

        public int UseIndex { get; }
    }
}
