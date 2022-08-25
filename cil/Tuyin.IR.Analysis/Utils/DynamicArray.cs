using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Tuyin.IR.Analysis.Utils;

namespace Tuyin.IR.Analysis
{
    public interface IReadOnlyArray<out T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IDisposable
    {
        int Length { get; }
    }

    public interface IArray<T> : IReadOnlyArray<T>
    {
        new T this[int index] { get; set; }

        void Clear();

        void Resize(int length);
    }

    public class DynamicArray<T> : IArray<T>, IDisposable, IObjectItem<int>
        where T : struct
    {
        private readonly int SIZE = Unsafe.SizeOf<T>();

        private int mLength;
        private T[] mItems;

        public T this[int index]
        {
            get
            {
                return mItems[index];
            }
            set
            {
                mItems[index] = value;
            }
        }

        public int Length
        {
            get { return mLength; }
            set { mLength = value; }
        }

        public int Count => Length;

        public int Capacity => mItems.Length;

        public DynamicArray()
        {
        }

        public DynamicArray(int size) 
        {
            mItems = new T[size];
        }

        public DynamicArray(List<T> items)
            : this(items.Count, items)
        {
        }

        public DynamicArray(int count, List<T> items)
            : this(Math.Max(count, items.Count))
        {
            for (var i = 0; i < items.Count; i++) 
            {
                mItems[i] = items[i];
            }
        }

        ~DynamicArray() 
        {
            mItems = null;
            Dispose();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize(int parameter)
        {
            mItems = new T[parameter];
            mLength = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Resize(int length)
        {
            Array.Resize(ref mItems, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item) 
        {
            CheckLength(mLength + 1);
            this[mLength++] = item;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void AddRange(DynamicArray<T> items)
        {
            var c = items.Length;
            CheckLength(mLength + c);
            byte* dst = (byte*)Unsafe.AsPointer(ref mItems[Count + 1]);
            byte* src = (byte*)Unsafe.AsPointer(ref items.mItems[0]);
            FastBuffer.UnsafeBlockCopyRL(src, dst, c * SIZE);
            mLength = mLength + c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void AddRange(T[] items)
        {
            var c = items.Length;
            CheckLength(mLength + c);
            byte* dst = (byte*)Unsafe.AsPointer(ref mItems[Count + 1]);
            byte* src = (byte*)Unsafe.AsPointer(ref items[0]);
            FastBuffer.UnsafeBlockCopyRL(src, dst, c * SIZE);
            mLength = mLength + c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear() 
        {
            mLength = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckLength(int length) 
        {
            if (mItems.Length <= length)
                Array.Resize(ref mItems, length * 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Insert(int index, T v) 
        {
            var s = SIZE;
            CheckLength(mLength + 1);
            byte* src = (byte*)Unsafe.AsPointer(ref mItems[index]);
            byte* dst = src + s;
            FastBuffer.UnsafeBlockCopyRL(src, dst, (mItems.Length - index) * SIZE);
            mItems[index] = v;
            mLength++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void InsertRange(int index, DynamicArray<T> v) 
        {
            var c = v.Length;
            var s = c * SIZE;
            CheckLength(mLength + c);
            byte* src = (byte*)Unsafe.AsPointer(ref mItems[index]);
            byte* dst = src + s;
            FastBuffer.UnsafeBlockCopyRL(src, dst, (mItems.Length - index) * SIZE);

            dst = src;
            src = (byte*)Unsafe.AsPointer(ref v.mItems[0]);
            FastBuffer.UnsafeBlockCopyRL(src, dst, s);
            mLength = mLength + c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void InsertRange(int index, T[] v)
        {
            var c = v.Length;
            var s = c * SIZE;
            CheckLength(mLength + c);
            byte* src = (byte*)Unsafe.AsPointer(ref mItems[index]);
            byte* dst = src + s;
            FastBuffer.UnsafeBlockCopyRL(src, dst, (mItems.Length - index) * SIZE);

            dst = src;
            src = (byte*)Unsafe.AsPointer(ref v[0]);
            FastBuffer.UnsafeBlockCopyRL(src, dst, s);
            mLength = mLength + c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Remove(T item)
        {
            var index = FindIndex(0, item);
            if (index != -1)
                RemoveAt(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                return;

            byte* src = (byte*)Unsafe.AsPointer(ref mItems[index + 1]);
            byte* dst = src - SIZE;
            FastBuffer.UnsafeBlockCopyLR(src, dst, (mItems.Length - index - 1) * SIZE);
            mLength--;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void RemoveRange(int index, int length) 
        {
            if (index < 0)
            {
                length = length + index;
                index = 0;
            }

            if (index + length > Count)
                length = Count - index;

            if (length <= 0)
                return;

            byte* src = (byte*)Unsafe.AsPointer(ref mItems[index + length]);
            byte* dst = src - (length * SIZE);
            FastBuffer.UnsafeBlockCopyLR(src, dst, (mItems.Length - index - 1) * SIZE);
            mLength = mLength - length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int FindIndex(int index, T target)
        {
            for (var i = index; i < mItems.Length; i++)
                if (Object.Equals(mItems[i], target))
                    return i;

            return -1;
        }

        public DynamicArray<T> Clone()
        {
            var clone = new DynamicArray<T>(Length);
            Array.Copy(mItems, clone.mItems, Length);
            clone.mLength = mLength;
            return clone;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return mItems.AsEnumerable().GetRange(0, mLength).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static ObjectPool<DynamicArray<T>, int> sObjectPool = ObjectPool<DynamicArray<T>, int>.GetInstance();

        public static DynamicArray<T> Create(int capacity) 
        {
            return sObjectPool.GetObjectFromPool(capacity);
        }

        public void Dispose()
        {
            sObjectPool.Free(this);
        }
    }
}
