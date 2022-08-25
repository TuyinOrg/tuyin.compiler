using System;
using System.Collections.Generic;
using System.Linq;

namespace libfsm
{
    public struct FAAction : IEquatable<FAAction>
    {
        public ushort Index { get; }

        public IList<FASymbol> Symbols { get; }

        public FAAction(ushort index, IList<FASymbol> symbols)
        {
            Index = index;
            Symbols = symbols;
        }

        public override bool Equals(object obj)
        {
            return obj is FAAction action && Equals(action);
        }

        public bool Equals(FAAction other)
        {
            return Symbols.SequenceEqual(other.Symbols);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Symbols);
        }
    }
}
