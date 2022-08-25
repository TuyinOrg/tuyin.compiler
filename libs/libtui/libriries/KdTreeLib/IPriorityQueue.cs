namespace libtui.libraries.kdtree
{
    public interface IPriorityQueue<TItem, TPriority>
	{
		void Enqueue(TItem item, TPriority priority);

		TItem Dequeue();

		int Count { get; }
	}
}
