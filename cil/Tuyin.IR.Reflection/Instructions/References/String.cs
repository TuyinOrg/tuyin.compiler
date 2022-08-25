using System;
using System.Collections.Generic;
using System.Text;

namespace Tuyin.IR.Reflection.Instructions
{
    public class String : Reference
    {
        public String(string value)
        {
            Value = value;
        }

        public String(string value, SourceSpan sourceSpan)
            : this(value)
        {
            SourceSpan = sourceSpan;
        }

        public override AstNodeType NodeType => AstNodeType.String;

        public string Value { get; }

        public override IEnumerable<AstNode> GetNodes()
        {
            yield return this;
        }

        public byte[] GetBytes(Encoding encoding)
        {
            var result = new byte[0x64];
            Write(null, result, 0, encoding, Value);
            return result;
        }

        private const int StoreBufferResizeSize = 0x100;
        private const int LargeByteBufferSize = 1024;

        protected virtual void Write(byte[] buffer, int position, byte value)
        {
            CheckBufferLength(buffer, position);

            buffer[position] = value;
        }

        private void Write(byte[] buffer, int index, int count, byte[] store, int position)
        {
            CheckBufferLength(store, position, count);
            Buffer.BlockCopy(buffer, index, store, position, count);
        }

        private unsafe void Write(byte[] large, byte[] buffer, int position, Encoding encoding, string value)
        {
            int maxChars = 0;
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            int byteCount = encoding.GetByteCount(value);
            Write7BitEncodedInt(buffer, position, byteCount);
            if (large == null)
            {
                large = new byte[LargeByteBufferSize];
                maxChars = LargeByteBufferSize / encoding.GetMaxByteCount(1);
            }
            if (byteCount <= LargeByteBufferSize)
            {
                encoding.GetBytes(value, 0, value.Length, large, 0);
                Write(large, 0, byteCount, buffer, position);
            }
            else
            {
                var encoder = encoding.GetEncoder();
                int num4;
                int num2 = 0;
                for (int i = value.Length; i > 0; i -= num4)
                {
                    num4 = i > maxChars ? maxChars : i;
                    fixed (char* str = value.ToCharArray())
                    {
                        int num5;
                        char* chPtr = str;
                        fixed (byte* numRef = large)
                        {
                            num5 = encoder.GetBytes(chPtr + num2, num4, numRef, LargeByteBufferSize, num4 == i);
                            //delete str = null;
                        }
                        Write(large, 0, num5, buffer, position);
                        num2 += num4;
                    }
                }
            }
        }

        private void Write7BitEncodedInt(byte[] buffer, int position, int value)
        {
            uint num = (uint)value;
            while (num >= 0x80)
            {
                Write(buffer, position, (byte)(num | 0x80));
                num = num >> 7;
            }
            Write(buffer, position, (byte)num);
        }

        private void CheckBufferLength(byte[] buffer, int position, int count = 1)
        {
            int farLength = position + count;

            if (buffer.Length <= farLength)
            {
                int multResize = farLength / StoreBufferResizeSize + 1;
                Array.Resize(ref buffer, multResize * StoreBufferResizeSize);
            }
        }

        public override string ToString()
        {
            return $"ldstr {Value}";
        }
    }
}
