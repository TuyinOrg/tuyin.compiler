using System.Collections.Generic;

namespace Tuyin.IR.Reflection
{
    public class Scope
    {
        private int mStart;
        private int mLength;
        private List<Scope> mChildrens;

        public int StartIndex => mStart;

        public int Length => mLength;

        public IReadOnlyList<Scope> Childrens => mChildrens;

        internal Scope(Scope parent, int start, Label enter, Label exit)
        {
            mStart = start;
            Parent = parent;
            Enter = enter;
            Exit = exit;
            mChildrens = new List<Scope>();
        }

        public Scope Parent { get; }

        public Label Enter { get; }

        public Label Exit { get; }

        internal Scope New(int irIndex)
        {
            var scope = new Scope(this, irIndex, new Label(), new Label());
            mChildrens.Add(scope);
            return scope;
        }

        internal Scope Return(int irIndex)
        {
            mLength = irIndex - mStart;
            return Parent;
        }

        public IEnumerable<Statment> GetStatments(IReadOnlyList<Statment> statments) 
        {
            var length = mLength == 0 ? statments.Count - mStart : mLength;
            var end = mStart + length;
            for (var i = mStart; i < end; i++)
            {
                yield return statments[i];
            }
        }
    }
}
