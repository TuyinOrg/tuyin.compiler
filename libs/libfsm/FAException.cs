using System;
using System.Collections.Generic;

namespace libfsm
{
    [Serializable]
    public class FAException : Exception
    {
        public FAException() { }
        public FAException(string message) : base(message) { }
        public FAException(string message, Exception inner) : base(message, inner) { }
        protected FAException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class LeftRecursionOverflowException<T> : FAException 
    {
        public FATransition<T> Transition { get; }

        public LeftRecursionOverflowException(FATransition<T> transition) 
        {
            Transition = transition;
        }
    }

    public class InvalidExpressionException<T> : FAException 
    {
        public FATransition<T> Transition { get; }

        public InvalidExpressionException(FATransition<T> transition)
        {
            Transition = transition;
        }
    }

    public class LoopOverflowException<T> : FAException 
    {
        public LoopGroup<T>[] Groups { get; }

        public LoopOverflowException(LoopGroup<T>[] groups)
        {
            Groups = groups;
        }
    }

    public class ConflictException<T> : FAException 
    {
        public FATransition<T> Left { get; }

        public FATransition<T> Right { get; }

        public IList<FATransition<T>> Transitions { get; }

        public FAActionTable Actions { get; }

        public ConflictException(FATransition<T> left, FATransition<T> right, IList<FATransition<T>> transitions, FAActionTable actions)
        {
            Left = left;
            Right = right;
            Transitions = transitions;
            Actions = actions;
        }
    }

    public class SymbolConflictException<T> : FAException
    {
        public SymbolConflictException(IList<SymbolGroup<T>> groups)
        {
            Groups = groups;
        }

        public IList<SymbolGroup<T>> Groups { get; }
    }

    public class MetadataConflictException<T> : FAException 
    {
        public MetadataConflictException(IList<MetadataGroup<T>> groups)
        {
            Groups = groups;
        }

        public IList<MetadataGroup<T>> Groups { get; }
    }
}
