using System.Collections.Generic;
using System.Linq;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Reflection
{
    public class Import : Statment, IExternal
    {
        public override AstNodeType NodeType => AstNodeType.Import;

        public String[] Path { get; }

        public Identifier Identifier { get; }

        public Import(String[] path, Identifier identifier)
        {
            Path = path;
            Identifier = identifier;
        }

        public override IEnumerable<AstNode> GetNodes()
        {
            return new AstNode[0];
        }

        public string GetFullPath() 
        {
            return string.Join(".", Path.Select(x => x.Value));
        }
    }
}