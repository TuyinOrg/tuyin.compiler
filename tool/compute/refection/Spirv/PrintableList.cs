using System.Collections.Generic;

namespace Toe.SPIRV
{
    internal class PrintableList<T> : List<T>
    {
        public PrintableList()
        {

        }

        public PrintableList(IEnumerable<T> values)
        {
            AddRange(values);
        }

        public override string ToString()
        {
            return string.Format("[{0}]", string.Join(", ", this));
        }
    }
}