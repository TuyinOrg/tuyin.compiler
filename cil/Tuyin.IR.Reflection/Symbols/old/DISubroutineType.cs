using System.Text;

namespace Tuyin.IR.Reflection.Symbols.old
{
    public sealed class DISubroutineType : DIMetadata
    {
        public DIBasicType[] ParameterTypes { get; }

        public DIBasicType ReturnType { get; }

        public DISubroutineType(DIBasicType returnType, params DIBasicType[] argTypes)
        {
            ReturnType = returnType;
            ParameterTypes = argTypes;
        }

        public override string Parse(DIMetadataManager manager)
        {
            var sb = new StringBuilder();
            sb.Append("{");
            if (ReturnType != null)
                sb.Append(manager.GetReference(ReturnType));
            else
                sb.Append("null");

            for (var i = 0; i < ParameterTypes.Length; i++)
            {
                var type = ParameterTypes[i];

                sb.Append($", {manager.GetReference(type)}");
            }

            sb.Append("}");

            return $"!DISubroutineType(types: !{sb})";
        }
    }
}
