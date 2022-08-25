using System.Collections.Generic;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Reflection.Types
{
    public abstract class Type : Reference
    {
        public override AstNodeType NodeType => AstNodeType.Type;

        public virtual uint BitsSize { get; }

        public abstract string Name { get; }

        public virtual bool IsPointer => this is PointerType;

        public virtual bool IsArray => this is ArrayType;

        public virtual bool IsStruct => this is StructType;

        public virtual bool IsAuto => this is AutoType;

        public virtual bool IsMutable => this is MutableType;

        public virtual bool IsPrimitive => this is PrimitiveType;

        public virtual bool IsFloatingPoint => false;

        public virtual bool IsIntegral => false;

        public virtual bool IsSigned => false;

        public virtual bool IsUnsigned => false;

        public virtual int GenericTypeCount { get; }

        public virtual Type GetGenericType(int index)
        {
            return this;
        }

        public override IEnumerable<AstNode> GetNodes()
        {
            for (var i = 0; i < GenericTypeCount; i++)
                yield return GetGenericType(i);

            yield return this;
        }
    }

}
