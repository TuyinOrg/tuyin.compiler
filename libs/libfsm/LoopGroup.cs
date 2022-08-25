namespace libfsm
{
    public sealed class LoopGroup<T>
    {
        public FATransition<T> Entry { get; }

        public FATransition<T>[] Failds { get; }

        public LoopGroup(FATransition<T> entry, FATransition<T>[] failds)
        {
            Entry = entry;
            Failds = failds;
        }
    }
}
