using System.Collections.Generic;

namespace libfsm
{
    public sealed class SymbolGroup<T>
    {
        public SymbolGroup(FASymbol symbol, IList<FATransition<T>> transitions)
        {
            FASymbol = symbol;
            Transitions = transitions;
        }

        public FASymbol FASymbol { get; }

        public IList<FATransition<T>> Transitions { get; }
    }
}
