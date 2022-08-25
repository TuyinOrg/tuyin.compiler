using System.Linq;

namespace libfsm
{
    partial class FATable<T>
    {
        public void Insert(FATransition<T> tran) 
        {
            Transitions.Add(tran);
            var mergeResult = Minimize(Transitions, new ushort[] { tran.Right });
            var confictResult = ConflictResolution(Transitions, ConflictsDetectionFlags.Metadata | ConflictsDetectionFlags.Symbol);
            mBuildSteps.Add(new FABuildStep<T>(FABuildStage.Dynamic, FABuildType.Add, tran));
            mBuildSteps.AddRange(mergeResult);
            mBuildSteps.AddRange(confictResult);
        }

        public void Remove(FATransition<T> tran) 
        {
            if (Transitions.Remove(tran))
            {
                var mergeResult = Minimize(Transitions, new ushort[] { tran.Right });
                var confictResult = ConflictResolution(Transitions, ConflictsDetectionFlags.Metadata | ConflictsDetectionFlags.Symbol);
                mBuildSteps.Add(new FABuildStep<T>(FABuildStage.Dynamic, FABuildType.Delete, tran));
                mBuildSteps.AddRange(mergeResult);
                mBuildSteps.AddRange(confictResult);
            }
        }
    }
}
