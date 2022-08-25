﻿using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Instructions
{
    public class GreaterThen : Expression
    {
        public GreaterThen(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override AstNodeType NodeType => AstNodeType.GreaterThen;

        public Expression Left { get; }

        public Expression Right { get; }

        public override IEnumerable<AstNode> GetNodes()
        {
            foreach (var item in Left.GetNodes())
                yield return item;

            foreach (var item in Right.GetNodes())
                yield return item;

            yield return this;
        }

        public override string ToString()
        {
            return $"gt {Left},{Right}";
        }
    }
}