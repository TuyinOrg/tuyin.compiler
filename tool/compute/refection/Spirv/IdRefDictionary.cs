using System;
using System.Collections;
using System.Collections.Generic;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV
{
    internal class IdRefDictionary<TValue> : IDictionary<uint, TValue>
    {
        private readonly EqualityComparer<TValue> _valueComparer = EqualityComparer<TValue>.Default;
        private readonly TValue[] _values;

        public IdRefDictionary(uint bound)
        {
            _values = new TValue[bound];
        }

        public int Count => _values.Length;
        public bool IsReadOnly => false;

        public ICollection<uint> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => _values;

        public TValue this[uint key]
        {
            get => _values[key];
            set => _values[key] = value;
        }

        public void Add(KeyValuePair<uint, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            for (var index = 0; index < _values.Length; index++) _values[index] = default;
        }

        public bool Contains(KeyValuePair<uint, TValue> item)
        {
            return ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<uint, TValue>[] array, int arrayIndex)
        {
            for (var index = 0; index < _values.Length; index++)
                array[index + arrayIndex] = new KeyValuePair<uint, TValue>((uint) index, _values[index]);
        }

        public bool Remove(KeyValuePair<uint, TValue> item)
        {
            if (Contains(item))
            {
                _values[item.Key] = default;
                return true;
            }

            return false;
        }

        public void Add(uint key, TValue value)
        {
            //if (_valueComparer.Equals(_values[key], default))
            //    throw new ArgumentException(
            //        $"An element {_values[key]} with the same key {key} already exists in the dictionary");
            _values[key] = value;
        }

        public bool ContainsKey(uint key)
        {
            return !_valueComparer.Equals(_values[key], default);
        }

        public bool Remove(uint key)
        {
            if (ContainsKey(key))
            {
                _values[key] = default;
                return true;
            }

            return false;
        }

        public bool TryGetValue(uint key, out TValue value)
        {
            value = _values[key];
            return true;
        }

        public IEnumerator<KeyValuePair<uint, TValue>> GetEnumerator()
        {
            for (var index = 0; index < _values.Length; index++)
                yield return new KeyValuePair<uint, TValue>((uint) index, _values[index]);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ref TValue GetRef(uint target)
        {
            return ref _values[target];
        }
    }
}