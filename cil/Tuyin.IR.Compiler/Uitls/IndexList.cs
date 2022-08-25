namespace System.Collections.Generic
{
    class IndexList<T> : List<T>
    {
        public new T this[int index] 
        {
            get 
            {
                if (index >= Count)
                    return default;

                return base[index];
            }
            set 
            {
                if (index < 0) return;
                
                if (index >= Count)
                {
                    for (int i = Count; i < index; i++)
                        Add(default);

                    Add(value);
                }
                else
                {
                    base[index] = value;
                }
            }
        }

        public IndexList() 
        {
        }

        public IndexList(int size)
            : base(size)
        {
        }

        public new void Insert(int index, T item) 
        {
            for (var i = this.Count; i >= index; i--)
                this[i + 1] = this[i];

            this[index] = item;
        }
    }
}
