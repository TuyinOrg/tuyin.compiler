using System;
using System.Collections;
using System.Collections.Generic;
using Tuyin.IR.Reflection.Symbols;

namespace Tuyin.IR.Reflection
{
    public class StatmentBuilder : IReadOnlyList<Statment>
    {
        private List<Statment> mStatments;
        private Stack<int> mCacheScopeStarts;
        private List<DIMetadata> mCacheMetadatas;

        public Scope Scope { get; private set; }

        public int Count => mStatments.Count;

        public Statment this[int index] => mStatments[index];

        public StatmentBuilder()
        {
            mStatments = new List<Statment>();
            mCacheMetadatas = new List<DIMetadata>();
            mCacheScopeStarts = new Stack<int>();
        }

        public void ReportError(int id, string template, ISourceSpan sourceSpan, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void DefineLabel(Label label)
        {
            label.Index = checked((ushort)mStatments.Count);
        }

        public void RemoveAt(int index)
        {
            mStatments.RemoveAt(index);
        }

        public void Add(Statment statment)
        {
            mStatments.Add(statment);
        }

        public void Insert(int index, Statment statment)
        {
            mStatments.Insert(index, statment);
        }

        public void StartScope(int startIndex)
        {
            mCacheScopeStarts.Push(startIndex);

            if (Scope == null)
                Scope = new Scope(null, mStatments.Count, new Label(), new Label());
            else
                Scope = Scope.New(mStatments.Count);
        }

        public Scope EndScope(int endIndex)
        {
            var startIndex = mCacheScopeStarts.Pop();
            var sourceSpan = new SourceSpan(startIndex, endIndex);

            var temp = Scope;
            Scope = Scope.Return(mStatments.Count);
            return temp;
        }

        public IEnumerator<Statment> GetEnumerator()
        {
            return mStatments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
