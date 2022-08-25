using LLParserLexerLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tuyin.IR.Reflection.Symbols;

namespace Tuyin.IR.Reflection
{
    public class Metadatas : IEnumerable<DIMetadata>, IAST
    {
        private Dictionary<DIMetadata, int> mMetadataIndex;
        private Dictionary<int, DIMetadata> mIndexMetadata;

        public string FileName { get; internal set; }

        public Metadatas()
        {
            mMetadataIndex = new Dictionary<DIMetadata, int>();
            mIndexMetadata = new Dictionary<int, DIMetadata>();
        }

        internal void AddMetadata(DIMetadata meta) 
        {
            if (!mMetadataIndex.ContainsKey(meta))
            {
                mIndexMetadata[mMetadataIndex.Count] = meta;
                mMetadataIndex[meta] = mMetadataIndex.Count;
            }
        }

        public DIMetadata GetMetadata(int index)
        {
            return mIndexMetadata[index];
        }

        public int GetMetadataIndex(DIMetadata meta) 
        {
            return mMetadataIndex[meta];
        }

        public int GetMetadataIndex(string meta)
        {
            var item = this.FirstOrDefault(x => x.Properties.FirstOrDefault(x => x.Value.Token.String == meta) != null);
            if (item != null)
                return mMetadataIndex[item];

            return -1;
        }

        public IAST Add(DIMetadata nt1_s)
        {
            AddMetadata(nt1_s);
            return this;
        }

        public IEnumerator<DIMetadata> GetEnumerator()
        {
            return mMetadataIndex.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
