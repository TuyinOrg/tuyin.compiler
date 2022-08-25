using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Toe.SPIRV
{
    internal class ListSegment<T> : IReadOnlyList<T>, IList<T>
    {
        private readonly IList<T> _values;
        private int _offset;
        private int _count;

        public ListSegment(IList<T> values)
        {
            _values = values ?? Array.Empty<T>();
            _offset = 0;
            _count = _values.Count;
        }

        public ListSegment(IEnumerable<T> values):this(values?.ToArray() ?? Array.Empty<T>())
        {
        }

        public ListSegment(IList<T> values, int offset, int count)
        {
            _values = values ?? Array.Empty<T>();
            _offset = offset;
            _count = count;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var index = _offset; index < _offset+_count; ++index)
            {
                yield return _values[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotImplementedException();
        }

        void ICollection<T>.Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            var comparer = EqualityComparer<T>.Default;
            for (var index = _offset; index < _offset + _count; ++index)
            {
                if (comparer.Equals(item, _values[index]))
                    return true;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (var index = 0; index < _count; ++index)
            {
                array[arrayIndex + index] = _values[_offset + index];
            }
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int Count => _count;

        public bool IsReadOnly => true;

        public int IndexOf(T item)
        {
            var comparer = EqualityComparer<T>.Default;
            for (var index = _offset; index < _offset + _count; ++index)
            {
                if (comparer.Equals(item, _values[index]))
                    return index-_offset;
            }

            return -1;
        }

        void IList<T>.Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get
            {
                return _values[index + _offset];
            }
            set { throw new NotImplementedException(); }
        }

        public override string ToString()
        {
            return $"new {typeof(T).Name}[{_count}] {{{string.Join(", ", this)}}}";
        }
    }
}