using System.Collections.Generic;
using System.Linq;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Reflection
{
    public partial class Function : Statment, IExternal
    {
        public override AstNodeType NodeType => AstNodeType.Function;

        public Linkage Linkage { get; }

        public Visibility Visibility { get; }
        
        public Identifier Identifier { get; }

        public IReadOnlyList<Parameter> Parameters { get; }

        public IReadOnlyList<Statment> Statments { get; }

        public Function(Linkage linkage, Visibility visibility, Identifier identifier, IEnumerable<Parameter> args, IEnumerable<Statment> stmts)
        {
            Linkage = linkage;
            Visibility = visibility;
            Identifier = identifier;
            Parameters = args.ToArray();
            Statments = stmts.ToArray();
        }

        public override string ToString()
        {
            var header = $"{Linkage.GetHelperDescrption()} {Visibility.GetHelperDescrption()} {Identifier.Value}({string.Join(",", Parameters.Select(x => $"{x.Identifier.Value}:{x.Type.Name}"))})\n";
            return $"{header}" +
                string.Join("   \n", Statments);
        }

        public override IEnumerable<AstNode> GetNodes()
        {
            return Statments;
        }
    }
}