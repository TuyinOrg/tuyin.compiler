using System;
using Tuyin.IR.Reflection.Instructions;
using Tuyin.IR.Reflection.Symbols;
using String = Tuyin.IR.Reflection.Instructions.String;

namespace Tuyin.IR.Reflection
{
    public interface IExpressionVisitor<T>
    {
        T VisitCall(Call ast);
        T VisitExpression(Expression node);
        T VisitInteger(Integer ast);
        T VisitFloat(Float ast);
        T VisitString(String ast);
        T VisitLiteral(Literal ast);
        T VisitMember(Member ast);
        T VisitIdentifier(Identifier ast);
        T VisitAdd(Add ast);
        T VisitAnd(And ast);
        T VisitDiv(Div ast);
        T VisitEqual(Equal ast);
        T VisitGreaterThen(GreaterThen ast);
        T VisitLeftShift(LeftShift ast);
        T VisitLessThen(LessThen ast);
        T VisitXor(Xor ast);
        T VisitSub(Sub ast);
        T VisitRightShift(RightShift ast);
        T VisitRem(Rem ast);
        T VisitOr(Or ast);
        T VisitNeg(Neg ast);
        T VisitMul(Mul ast);
        T VisitElement(Element ast);
    }

    public interface IStatementVisitor<T>
    {
        T VisitTest(Test ast);
        T VisitGoto(Goto ast);
        T VisitImport(Import ast);
        T VisitFunction(Function ast);
        T VisitReturn(Return ast);
        T VisitStore(Store ast);
    }

    public abstract class AstVisitor<T> : IExpressionVisitor<T>, IStatementVisitor<T>
    {
        public virtual T Visit(AstNode node)
        {
            switch (node.NodeType)
            {
                case AstNodeType.Metadata:
                case AstNodeType.Import:
                case AstNodeType.Function:
                case AstNodeType.Goto:
                case AstNodeType.Test:
                case AstNodeType.Return:
                case AstNodeType.Store:
                    return VisitStatement(node as Statment);
                case AstNodeType.Call:
                case AstNodeType.Integer:
                case AstNodeType.Float:
                case AstNodeType.String:
                case AstNodeType.Literal:
                case AstNodeType.Member:
                case AstNodeType.Identifier:
                case AstNodeType.Add:
                case AstNodeType.And:
                case AstNodeType.Div:
                case AstNodeType.Equal:
                case AstNodeType.GreaterThen:
                case AstNodeType.LeftShift:
                case AstNodeType.LessThen:
                case AstNodeType.Mul:
                case AstNodeType.Neg:
                case AstNodeType.Or:
                case AstNodeType.Rem:
                case AstNodeType.RightShift:
                case AstNodeType.Sub:
                case AstNodeType.Xor:
                case AstNodeType.Element:
                    return VisitExpression(node as Expression);
            }

            throw new NotImplementedException();
        }
        public virtual T VisitExpression(Expression node)
        {
            switch (node.NodeType)
            {
                case AstNodeType.Call: return VisitCall(node as Call);
                case AstNodeType.Integer: return VisitInteger(node as Integer);
                case AstNodeType.Float: return VisitFloat(node as Float);
                case AstNodeType.String: return VisitString(node as String);
                case AstNodeType.Literal: return VisitLiteral(node as Literal);
                case AstNodeType.Member: return VisitMember(node as Member);
                case AstNodeType.Identifier: return VisitIdentifier(node as Identifier);
                case AstNodeType.Add: return VisitAdd(node as Add);
                case AstNodeType.And: return VisitAnd(node as And);
                case AstNodeType.Div: return VisitDiv(node as Div);
                case AstNodeType.Equal: return VisitEqual(node as Equal);
                case AstNodeType.GreaterThen: return VisitGreaterThen(node as GreaterThen);
                case AstNodeType.LeftShift: return VisitLeftShift(node as LeftShift);
                case AstNodeType.LessThen: return VisitLessThen(node as LessThen);
                case AstNodeType.Mul: return VisitMul(node as Mul);
                case AstNodeType.Neg: return VisitNeg(node as Neg);
                case AstNodeType.Or: return VisitOr(node as Or);
                case AstNodeType.Rem: return VisitRem(node as Rem);
                case AstNodeType.RightShift: return VisitRightShift(node as RightShift);
                case AstNodeType.Sub: return VisitSub(node as Sub);
                case AstNodeType.Xor: return VisitXor(node as Xor);
                case AstNodeType.Element: return VisitElement(node as Element);
            }

            throw new NotImplementedException();
        }

        public virtual T VisitStatement(Statment node)
        {
            switch (node.NodeType)
            {
                case AstNodeType.Store: return VisitStore(node as Store);
                case AstNodeType.Metadata: return VisitMetadata(node as DIMetadata);
                case AstNodeType.Import: return VisitImport(node as Import);
                case AstNodeType.Function: return VisitFunction(node as Function);
                case AstNodeType.Goto: return VisitGoto(node as Goto);
                case AstNodeType.Test: return VisitTest(node as Test);
                case AstNodeType.Return: return VisitReturn(node as Return);
            }

            throw new NotImplementedException();
        }

        public virtual T VisitElement(Element ast) { return default; }
        public virtual T VisitXor(Xor ast) { return default; }
        public virtual T VisitSub(Sub ast) { return default; }
        public virtual T VisitRightShift(RightShift ast) { return default; }
        public virtual T VisitRem(Rem ast) { return default; }
        public virtual T VisitOr(Or ast) { return default; }
        public virtual T VisitNeg(Neg ast) { return default; }
        public virtual T VisitMul(Mul ast) { return default; }
        public virtual T VisitLessThen(LessThen ast) { return default; }
        public virtual T VisitLeftShift(LeftShift ast) { return default; }
        public virtual T VisitGreaterThen(GreaterThen ast) { return default; }
        public virtual T VisitEqual(Equal ast) { return default; }
        public virtual T VisitDiv(Div ast) { return default; }
        public virtual T VisitAnd(And ast) { return default; }
        public virtual T VisitAdd(Add ast) { return default; }
        public virtual T VisitStore(Store ast) { return default; }
        public virtual T VisitIdentifier(Identifier ast) { return default; }
        public virtual T VisitMember(Member ast) { return default; }
        public virtual T VisitLiteral(Literal ast) { return default; }
        public virtual T VisitString(String ast) { return default; }
        public virtual T VisitInteger(Integer ast) { return default; }
        public virtual T VisitFloat(Float ast) { return default; }
        public virtual T VisitReturn(Return ast) { return default; }
        public virtual T VisitCall(Call ast) { return default; }
        public virtual T VisitTest(Test ast) { return default; }
        public virtual T VisitGoto(Goto ast) { return default; }
        public virtual T VisitImport(Import ast) { return default; }
        public virtual T VisitFunction(Function ast) { return default; }
        public virtual T VisitMetadata(DIMetadata ast) { return default; }
    }
}
