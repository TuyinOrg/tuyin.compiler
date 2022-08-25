using System.Collections.Generic;
using System.Linq;

namespace Tuyin.IR.Reflection.Types
{
    public class FunctionType : Type
    {
        public override string Name => $"{ReturnType.Name}({string.Join(",", ParameterTypes.Select(x => x.Name))})";

        public override uint BitsSize => 32;

        public Type ReturnType { get; }

        public IReadOnlyList<Type> ParameterTypes { get; }

        public FunctionType(Type returnType, IReadOnlyList<Type> parameterTypes)
        {
            ReturnType = returnType;
            ParameterTypes = parameterTypes;
        }
    }

}
