using System;
using System.IO;

namespace Toe.SPIRV
{
    public class WordWriter:IDisposable
    {
        private readonly BinaryWriter _writer;

        public WordWriter(BinaryWriter writer)
        {
            _writer = writer;
        }

        public void WriteWord(uint word)
        {
            _writer.Write(word);
            ++Position;
        }

        public void Dispose()
        {
            _writer.Dispose();
        }

        public uint Position { get; private set; }

        public static uint GetWordCount(string name)
        {
            var bytes = WordReader.Encoding.GetByteCount(name);
            return ((uint)bytes + 3 + 1) / 4;
        }

        public void Write(string value)
        {
            var bytes = WordReader.Encoding.GetBytes(value);
            var wordCount = (((uint)bytes.Length + 3 + 1)/4);
            var length = 4*wordCount;
            _writer.Write(bytes);
            for (var index = bytes.Length; index < length; index++)
            {
                _writer.Write((byte)0);
            }
            Position += wordCount;
        }
        public void Write(byte[] bytes)
        {
            var wordCount = (((uint)bytes.Length + 3) / 4);
            var length = 4 * wordCount;
            _writer.Write(bytes);
            for (var index = bytes.Length; index < length; index++)
            {
                _writer.Write((byte)0);
            }
            Position += wordCount;
        }
    }
}