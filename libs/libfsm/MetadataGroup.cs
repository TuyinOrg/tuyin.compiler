using System.Collections.Generic;

namespace libfsm
{
    public sealed class MetadataGroup<T>
    {
        public MetadataGroup(T metadata, IList<FATransition<T>> transitions)
        {
            Metadata = metadata;
            Transitions = transitions;
        }

        public T Metadata { get; }

        public IList<FATransition<T>> Transitions { get; }
    }
}
