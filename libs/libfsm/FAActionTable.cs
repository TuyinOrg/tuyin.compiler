using System;
using System.Collections.Generic;

namespace libfsm
{
    public class FAActionTable
    {
        private readonly List<FAAction> mList = new List<FAAction>();
        private readonly Dictionary<int, FAAction> mDict = new Dictionary<int, FAAction>();

        public int Count => mDict.Count;

        public FAAction this[int index] 
        {
            get { return mList[index]; }
            set 
            { 
                mDict[index] = value;
                mList.Add(value);
            }
        }

        public bool Contains(int id) 
        {
            return mDict.ContainsKey(id);
        }

        public FAAction FromId(int id)
        {
            return mDict[id];
        }
    }
}
