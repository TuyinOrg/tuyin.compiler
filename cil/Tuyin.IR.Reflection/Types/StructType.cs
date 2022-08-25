using System;
using System.Collections.Generic;
using System.Linq;

namespace Tuyin.IR.Reflection.Types
{
    public class StructType : Type
    {
        public override string Name => String.Join(",", Members.Select(x => $"{x.Key}:{x.Value.Type.Name}"));

        public override uint BitsSize => (uint)Members.Sum(x => x.Value.Type.BitsSize);

        public class StructMember
        {
            public StructMember(string name, Type type)
            {
                Name = name;
                Type = type;
            }

            public string Name { get; }

            public Type Type { get; }
        }

        public IReadOnlyDictionary<string, StructMember> Members { get; }

        public StructType(IEnumerable<StructMember> members)
        {
            Members = new Dictionary<string, StructMember>(members.ToDictionary(x => x.Name));
        }
    }

}
