using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Toe.SPIRV
{
    internal class WordReader
    {
        internal static readonly UTF8Encoding Encoding = new UTF8Encoding(false);
        private readonly BinaryReader _reader;


        public WordReader(BinaryReader reader, InstructionRegistry instructionRegistry)
        {
            _reader = reader;
            Instructions = instructionRegistry;
        }

        public uint Position { get; private set; }

        public InstructionRegistry Instructions { get; }

        public uint ReadWord()
        {
            ++Position;
            return _reader.ReadUInt32();
        }

        public string ReadString(uint words)
        {
            var bytes = _reader.ReadBytes((int) words * 4);
            var len = bytes.Length;
            while (len > 0 && bytes[len - 1] == 0) --len;
            Position += (uint) (bytes.Length / 4);
            if (len == 0)
                return string.Empty;
            return Encoding.GetString(bytes, 0, len);
        }

        public string ReadString()
        {
            var bytes = new List<byte>(4);
            for (;;)
            {
                byte last;
                bytes.Add(_reader.ReadByte());
                bytes.Add(_reader.ReadByte());
                bytes.Add(_reader.ReadByte());
                bytes.Add(last = _reader.ReadByte());
                ++Position;
                if (last == 0)
                    break;
            }

            var len = bytes.Count;
            while (len > 0 && bytes[len - 1] == 0) --len;
            if (len == 0)
                return string.Empty;
            return Encoding.GetString(bytes.ToArray(), 0, len);
        }

        public byte[] ReadBytes(uint numWords)
        {
            var res = _reader.ReadBytes((int) numWords * 4);
            Position += (uint) res.Length / 4;
            return res;
        }
    }
}