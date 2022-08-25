using System.Collections;

namespace Tuyin.IR.Compiler.Uitls
{
    class CharSource : IEnumerable<char>
    {
        private static readonly char[] EMPTY = new char[0];

        private int mLength;
        private char[] mChars;

        public int Length => mLength;

        public unsafe char* Handle
        {
            get
            {
                fixed (char* handle = &mChars[0])
                {
                    return handle;
                }
            }
        }

        public char[] Chars => mChars;

        public char this[int index]
        {
            get
            {
                return mChars[index];
            }
        }

        public CharSource(int capacity)
        {
            mChars = new char[capacity];
            mLength = 0;
        }

        public ReadOnlySpan<char> AsSpan(int start, int length)
        {
            return new ReadOnlySpan<char>(mChars, start, length);
        }

        public unsafe void Insert(int index, char c, int length)
        {
            if (length == 0) return;

            var checkLength = mLength + length + 1;
            if (mChars.Length < checkLength)
                Resize(checkLength);

            fixed (char* src1 = &mChars[index])
            {
                var p1 = (byte*)src1;
                var p3 = (byte*)(src1 + length);

                FastBuffer.UnsafeBlockCopyRL(p1, p3, (mLength - index) * 2);
                for (var i = 0; i < length; i++)
                {
                    *(src1 + i) = c;
                }
            }

            mLength = mLength + length;
            mChars[mLength] = '\0';
        }

        public unsafe void Insert(int index, char[] chars, int length)
        {
            fixed (char* src2 = &chars[0])
            {
                Insert(index, src2, length);
            }
        }

        public unsafe void Insert(int index, char* intptr, int length)
        {
            if (length <= 0) return;

            var checkLength = mLength + length + 1;
            if (mChars.Length < checkLength)
                Resize(checkLength);

            fixed (char* src1 = &mChars[index])
            {
                var p1 = (byte*)src1;
                var p2 = (byte*)intptr;
                var p3 = (byte*)(src1 + length);

                FastBuffer.UnsafeBlockCopyRL(p1, p3, (mLength - index) * 2);
                FastBuffer.UnsafeBlockCopyRL(p2, p1, length * 2);
            }

            mLength = mLength + length;
            mChars[mLength] = '\0';
        }

        public unsafe void Remove(int index, int length)
        {
            if (length <= 0) return;

            var copyLength = (mLength - index - length) * 2;
            if (copyLength > 0)
            {
                fixed (char* src1 = &mChars[index])
                {
                    var dst = (byte*)src1;
                    var src = dst + length * 2;

                    FastBuffer.ParallelBlockCopyLR(src, dst, copyLength);
                }
            }

            mLength = mLength - length;
            mChars[mLength] = '\0';
        }

        public char GetChar(int index)
        {
            if (index < 0 || index > mLength - 1)
                return default;

            return mChars[index];
        }

        public string GetString(int start, int end)
        {
            return new string(Range(start, end - start));
        }

        public unsafe char[] Range(Range range)
        {
            return Range(range.Index, range.Length);
        }

        public unsafe char[] Range(int index, int length)
        {
            if (length <= 0) return EMPTY;

            if (index < 0) index = 0;

            length = Math.Min(length, mLength - index);

            var chars = new char[length];
            if (length > 0)
            {
                fixed (char* csrc = &mChars[index], cdst = &chars[0])
                {
                    var src = (byte*)csrc;
                    var dst = (byte*)cdst;

                    FastBuffer.ParallelBlockCopyRL(src, dst, length * 2);
                }
            }

            return chars;
        }

        public unsafe void Resize(int size)
        {
            var targetSize = size + Math.Max(1024, size / 2);
            targetSize = targetSize + targetSize % 32;

            var arr = new char[targetSize];
            fixed (char* src1 = &mChars[0], src2 = &arr[0])
            {
                var p1 = (byte*)src1;
                var p2 = (byte*)src2;

                FastBuffer.ParallelBlockCopyRL(p1, p2, mLength * 2);
            }

            mChars = arr;
        }

        public void Clear()
        {
            mLength = 0;
        }

        public IEnumerator<char> GetEnumerator()
        {
            for (var i = 0; i < mLength; i++)
                yield return mChars[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return new string(AsSpan(0, Length));
        }
    }
}
