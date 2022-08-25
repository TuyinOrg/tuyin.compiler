using System.Collections.Generic;

namespace System.Linq
{
    static class LinqExtension 
    {
        public static IList<IList<TElement>> GetCombination<TElement>(this IList<TElement[]> containers) 
        {
            return GetCombination(0, containers);
        }

        private static IList<IList<TElement>> GetCombination<TElement>(int currentIndex, IList<TElement[]> containers)
        {
            if (currentIndex == containers.Count)
            {
                // Skip the items for the last container
                IList<IList<TElement>> combinations2 = new List<IList<TElement>>();
                combinations2.Add(new List<TElement>());
                return combinations2;
            }

            var combinations = new List<IList<TElement>>();

            var containerItemList = containers[currentIndex];
            while (containerItemList == null)
            {
                containerItemList = containers[++currentIndex];
                if (currentIndex >= containers.Count)
                {
                    break;
                }
            }
            // Get combination from next index
            var suffixList = GetCombination(currentIndex + 1, containers);
            int size = containerItemList == null ? 0 : containerItemList.Length;
            for (int ii = 0; ii < size; ii++)
            {
                TElement containerItem = containerItemList[ii];
                if (suffixList != null)
                {
                    foreach (var suffix in suffixList)
                    {
                        var nextCombination = new List<TElement>();
                        nextCombination.Add(containerItem);
                        nextCombination.AddRange(suffix);
                        combinations.Add(nextCombination);
                    }
                }
            }
            return combinations;
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> values) 
        {
            return new Queue<T>(values);
        }

        public static bool OverCount<T>(this IEnumerable<T> items, int count)
        {
            var index = 0;
            foreach (var item in items)
            {
                index++;
                if (index >= count)
                    return true;
            }

            return false;
        }

        public static bool Same<T>(this IEnumerable<T> items)
        {
            bool result = true;
            var v = items.FirstOrDefault();
            if (v != null)
            {
                var e = items.GetEnumerator();
                e.MoveNext();
                while (e.MoveNext())
                {
                    if (e.Current != null && !e.Current.Equals(v))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public static bool Same<T>(this IEnumerable<T> items, Func<T, object> selector)
        {
            bool result = true;
            var v = items.FirstOrDefault();
            if (v != null)
            {
                var item = selector(v);

                var e = items.GetEnumerator();
                e.MoveNext();
                while (e.MoveNext())
                {
                    if (e.Current == null || !Object.Equals(item, selector(e.Current)))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public static T FromIndex<T>(this IEnumerable<T> items, int index) 
        {
            var c = 0;
            foreach (var item in items) 
            {
                if (c == index)
                    return item;

                c++;
            }

            return default;
        }

        public static int IndexOf<T>(this IEnumerable<T> items, Func<T, bool> func)
        {
            var index = 0;
            foreach (var i in items)
            {
                if (func(i))
                    return index;

                index++;
            }

            return -1;
        }


        public static int IndexOf<T>(this IEnumerable<T> items, T item) 
        {
            var index = 0;
            foreach (var i in items)
            {
                if (i == null) 
                {
                    if (item == null) 
                    {
                        return index;
                    }

                    continue;
                }

                if (i.Equals(item)) 
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        public static T MinItem<T>(this IEnumerable<T> items, Func<T, double> func) 
        {
            var value = double.MaxValue;
            var result = default(T);

            foreach (var item in items) 
            {
                var itemValue = func(item);
                if (itemValue < value)
                {
                    value = itemValue;
                    result = item;
                }
            }

            return result;
        }

        public static IEnumerable<T> MinItems<T>(this IEnumerable<T> items, Func<T, int> func)
        {
            var value = int.MaxValue;
            var result = default(T);

            foreach (var item in items)
            {
                var itemValue = func(item);
                if (itemValue < value)
                {
                    value = itemValue;
                    result = item;
                }
            }

            foreach (var item in items)
            {
                var itemValue = func(item);
                if (itemValue == value)
                {
                    yield return item;
                }
            }
        }

        public static T MaxItem<T>(this IEnumerable<T> items, Comparison<T> comparison) 
        {
            var list = items.ToList();
            list.Sort(comparison);
            return list.FirstOrDefault();
        }

        public static T MaxItem<T>(this IEnumerable<T> items, Func<T, float> func)
        {
            var value = float.MinValue;
            var result = default(T);

            foreach (var item in items)
            {
                var itemValue = func(item);
                if (itemValue > value)
                {
                    value = itemValue;
                    result = item;
                }
            }

            return result;
        }

        public static IEnumerable<T> RemoveCount<T>(this IEnumerable<T> items, int removeCount, Func<T, bool> func) 
        {
            var areadyRemoveCount = 0;

            foreach (var item in items)
            {
                if (func(item) && areadyRemoveCount < removeCount)
                {
                    areadyRemoveCount++;
                }
                else 
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> RemoveFrist<T>(this IEnumerable<T> items, Func<T, bool> func) 
        {
            var hasRemoveFirst = false;

            foreach (var item in items)
            {
                if (!func(item) || hasRemoveFirst)
                {
                    yield return item;
                }
                else 
                {
                    hasRemoveFirst = true;
                }
            }
        }

        public static IEnumerable<T> SelectRange<T>(this IEnumerable<T> items, int start, int end)
        {
            var e = items.GetEnumerator();
            e.MoveNext();

            for (var i = 0; i <= end; i++)
            {
                if (i >= start)
                    yield return e.Current;

                e.MoveNext();
            }
        }


        public static IEnumerable<T> SelectIndexs<T>(this IEnumerable<T> items, IEnumerable<int> indexs) 
        {
            var index = 0;
            foreach (var item in items)
            {
                if (indexs.Contains(index))
                    yield return item;

                index++;
            }
        }

        public static IEnumerable<T> RemoveIndexs<T>(this IEnumerable<T> items, IEnumerable<int> indexs) 
        {
            var index = 0;
            foreach (var item in items)
            {
                if (!indexs.Contains(index))
                    yield return item;

                index++;
            }
        }

        public static void Do<T>(this IEnumerable<T> items, Action<T> action) 
        {
            foreach (var item in items) action(item);
        }
    }
}
