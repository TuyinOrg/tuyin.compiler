namespace addin.controls.renderer
{
    public struct GridItem<T>
    {
        public T Item { get; }

        public int Row { get; }

        public int Column { get; }

        public GridItem(T item, int column, int row) 
        {
            Item = item;
            Row = row;
            Column = column;
        }
    }
}
