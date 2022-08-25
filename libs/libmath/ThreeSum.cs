using System;

namespace libmath
{
    internal class ThreeSum : Algorithm<IEnumerable<int>, int[]>
    {
        public override IEnumerable<int> Compute(int[] nums)
        {
            Array.Sort(nums);//从小到大排序
            List<int> ilist = new List<int>(nums.Length);
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > 0) return ilist;
                if (i != 0 && nums[i] == nums[i - 1]) continue;//去掉一层，去重关键
                                                               //剩下后两层-双指针法:nums[i]+nums[left]+nums[right]==0
                int left = i + 1;
                int right = nums.Length - 1;
                while (left < right)
                {
                    if (nums[i] + nums[left] + nums[right] < 0) left++;
                    else if (nums[i] + nums[left] + nums[right] > 0) right--;
                    else //(nums[i]+nums[left]+nums[right]==0) 
                    {
                        ilist.Add(nums[i]);
                        ilist.Add(nums[left]);
                        ilist.Add(nums[right]);

                        while (right > left && nums[left] == nums[left + 1]) left++;//去重关键
                        while (right > left && nums[right] == nums[right - 1]) right--;//去重关键

                        left++; right--;//正常双指针往中间缩进                   
                    }
                }
            }

            //好快，10倍多一点
            return ilist;
        }
    }
}
