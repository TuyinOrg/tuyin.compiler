using System;
using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Instructions
{
    /// <summary>
    /// 涉及寄存器使用
    /// </summary>
    public class Identifier : Reference, IEquatable<Identifier>
    {
        public Identifier()
        {
        }

        public Identifier(string value)
        {
            Value = value;
        }

        public override AstNodeType NodeType => AstNodeType.Identifier;

        public string Value { get; }

        public override IEnumerable<AstNode> GetNodes()
        {
            yield return this;
        }

        public override bool Equals(object obj)
        {
            return obj is Identifier identifier && Equals(identifier);
        }

        public bool Equals(Identifier other)
        {
            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public override string ToString()
        {
            return Value;
        }

        public static bool operator ==(Identifier left, Identifier right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Identifier left, Identifier right)
        {
            return !Equals(left, right);
        }
    }
}