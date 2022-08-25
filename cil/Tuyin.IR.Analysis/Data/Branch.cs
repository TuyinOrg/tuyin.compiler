namespace Tuyin.IR.Analysis.Data
{
    public class Branch
    {
        private int[] mBranches;
        private int[] mParents;
        private int[] mPreviews;

        internal Branch(int[] branches, int[] parents, int[] previews)
        {
            this.mBranches = branches;
            this.mParents = parents;
            this.mPreviews = previews;
        }

        public int[] StatmentBranches => mBranches;

        public int[] StatmentParentBranches => mParents;

        public int[] BranchParentBranches => mPreviews;
    }
}
