using System.Collections.Generic;
using Tuyin.IR.Analysis.Data;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;
using Scope = Tuyin.IR.Analysis.Data.Scope;

namespace Tuyin.IR.Analysis.Passes
{
    class ScopeAnalysis : IAnalysis<ScopeAnalysisOpation, IReadOnlyList<Scope>>
    {
        public IReadOnlyList<Scope> Run(ScopeAnalysisOpation input)
        {         
            var branch = input.Branch.StatmentBranches;
            var scopes = DynamicArray<Scope>.Create(branch.Length);

            var parent = 0;
            var curr = branch[0];
            for (var i = 1; i < branch.Length; i++) 
            {
                var b = branch[i];
                if (curr != b) 
                {
                    var stmt = input.Statments[i - 1] as Goto;
                    var scope = stmt?.NodeType == AstNodeType.Test ?
                        new Scope(parent, i, stmt.Label.Index, i) :
                        new Scope(parent, i, -1, stmt?.Label.Index ?? (i == branch.Length - 1 ? -1 : i));

                    scopes.Add(scope);
                    parent = i;
                    curr = b;
                }
            }

            if (parent != branch.Length - 1)
                scopes.Add(new Scope(parent, branch.Length));

            return scopes;
        }
    }

    class ScopeAnalysisOpation
    {
        public ScopeAnalysisOpation(Branch branch, IReadOnlyList<Statment> input)
        {
            Branch = branch;
            Statments = input;
        }

        public Branch Branch { get; }

        public IReadOnlyList<Statment> Statments { get; }
    }
}
