using System.Collections.Generic;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeFunction:SpirvTypeBase
    {
        public TypeFunction()
        {
        }

        public TypeFunction(SpirvTypeBase returnType, IEnumerable<TypeFunctionArgument> arguments)
        {
            ReturnType = returnType;
            if (arguments != null) { foreach (var argument in arguments) { Arguments.Add(argument); } }
        }

        public TypeFunction(SpirvTypeBase returnType, params TypeFunctionArgument[] arguments)
        {
            ReturnType = returnType;
            foreach (var argument in arguments) { Arguments.Add(argument); }
        }

        public SpirvTypeBase ReturnType { get; set; }

        public IList<TypeFunctionArgument> Arguments { get; } = new List<TypeFunctionArgument>();

        public IEnumerable<SpirvTypeBase> ParameterTypes
        {
            get
            {
                foreach (var argument in Arguments)
                {
                    yield return argument.Type;
                }
            }
        }
        public override string ToString()
        {
            return DebugName ?? base.ToString();
        }

        partial void SetUp(OpTypeFunction instruction, SpirvInstructionTreeBuilder treeBuilder)
        {
            ReturnType = treeBuilder.ResolveType(instruction.ReturnType);
            foreach (var parameterType in instruction.ParameterTypes)
                Arguments.Add(new TypeFunctionArgument(treeBuilder.ResolveType(parameterType.Id)));
        }
    }
}