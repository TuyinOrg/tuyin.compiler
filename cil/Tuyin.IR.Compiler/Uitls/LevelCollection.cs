using System.Collections.Generic;
using System.Collections;
using System;

namespace System.Collections.Generic
{
    class LevelCollection<T> : IEnumerable<T>
    {
        private IndexList<T> mLevels;

        private int mCurrentIndex = -1;

        public T Current
        {
            get { return mCurrentIndex == -1 ? default(T) : mLevels[mCurrentIndex]; }
            set { mLevels[mCurrentIndex] = value; }
        }

        public T this[int index] 
        {
            get { return mLevels[index]; }
        }

        public int CurrentIndex { get { return mLevels.IndexOf(Current); } }

        public LevelCollection()
        {
            mLevels = new IndexList<T>();
        }

        public void Push()
        {
            Push(Activator.CreateInstance<T>());
        }

        public T Push(T item)
        {
            mLevels[++mCurrentIndex] = item;
            return item;
        }

        public T Pop()
        {
            // 销毁当前
            var item = mLevels[mCurrentIndex];
            mLevels.RemoveAt(mCurrentIndex--);
            return item;
        }

        public void Restart()
        {
            mCurrentIndex = 0;
        }

        public T[] ToArray()
        {
            return mLevels.ToArray();
        }

        public int IndexOf(T item)
        {
            return mLevels.IndexOf(item);
        }

        public int Count { get { return mLevels.Count; } }

        public T GetItem(int level)
        {
            return mLevels[level];
        }

        public IEnumerable<T> GetRangeItem(int start, int end)
        {
            for (var i = start; i < end; i++)
            {
                yield return mLevels[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return mLevels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static implicit operator T(LevelCollection<T> list)
        {
            return list.Current;
        }
    }
}
