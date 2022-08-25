using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Tuyin.IR.Compiler.Parser;
using Tuyin.IR.Compiler.Target;
using Tuyin.IR.Reflection;
using Attribute = System.Attribute;
using Path = System.IO.Path;
using String = System.String;
using Type = System.Type;

namespace Tuyin.IR.Compiler
{
    static class Helper
    {
        public unsafe static void FredouImplementation(string input, int inputLength, string replace, string[] replaceBy)
        {
            var indexes = new List<int>();

            //input = "ABCDABCABCDABCABCDABCABCDABCD";
            //inputLength = input.Length;
            //replaceBy = new string[] { "AA", "BB", "CC", "DD", "EE" };

            //my own string.indexof to save a few ms
            int len = inputLength;

            fixed (char* i = input, r = replace)
            {
                int replaceValAsInt = *((int*)r);

                while (--len > -1)
                {
                    if (replaceValAsInt == *((int*)&i[len]))
                    {
                        indexes.Add(len--);
                    }
                }
            }

            var idx = indexes.ToArray();
            len = indexes.Count;

            Parallel.For(0, replaceBy.Length, l =>
                Process(input, inputLength, replaceBy[l], idx, len)
            );
        }

        private unsafe static void Process(string input, int len, string replaceBy, int[] idx, int idxLen)
        {
            var output = new char[len];

            fixed (char* o = output, i = input, r = replaceBy)
            {
                int replaceByValAsInt = *((int*)r);

                //direct copy, simulate string.copy
                while (--len > -1)
                {
                    o[len] = i[len];
                }

                while (--idxLen > -1)
                {
                    ((int*)&o[idx[idxLen]])[0] = replaceByValAsInt;
                }
            }

            //Console.WriteLine(output);
        }

        public static IEnumerable<ProductionBase> Scan(this ProductionBase production, Action<ProductionBase> action = null, Func<ProductionBase, bool> filter = null)
        {
            var visited = new HashSet<ProductionBase>();
            var reutrns = new HashSet<ProductionBase>();
            var stack = new Stack<ProductionBase>();
            stack.Push(production);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (filter == null || filter(current))
                {
                    action?.Invoke(current);
                    reutrns.Add(current);
                }

                visited.Add(current);

                foreach (var item in current.GetChildrens())
                {
                    if (item != null && !visited.Contains(item))
                    {
                        stack.Push(item);
                    }
                }
            }
            return reutrns;
        }

        public static void ReportError(this StatmentBuilder cache, Errors error, ISourceSpan sourceSpan)
        {
            cache.ReportError((int)error, error.GetHelperDescrption(), sourceSpan);
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

        public static bool Same<T, T2>(this IEnumerable<T> items, Func<T, T2> func)
        {
            bool result = true;
            var v = items.FirstOrDefault();
            T2 t2 = func(v);
            if (v != null)
            {
                var e = items.GetEnumerator();
                e.MoveNext();
                while (e.MoveNext())
                {
                    if (e.Current != null)
                    {
                        var t3 = func(e.Current);
                        if (!object.Equals(t2, t3))
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        private static readonly string[] charToString = new string[256];

        static readonly IDictionary<string, string> replaceDict = new Dictionary<string, string>();

        const string ms_regexEscapes = @"[\a\b\f\n\r\t\v\\""]";

        readonly static char[] separators = { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, Path.VolumeSeparatorChar };

        public static string StringLiteral(string str)
        {
            return System.Text.RegularExpressions.Regex.Replace(str, ms_regexEscapes, Match);
        }

        public static string CharLiteral(char c)
        {
            return c == '\'' ? @"'\''" : string.Format("'{0}'", c);
        }

        private static string Match(System.Text.RegularExpressions.Match m)
        {
            string match = m.ToString();
            if (replaceDict.ContainsKey(match))
            {
                return replaceDict[match];
            }

            throw new NotSupportedException();
        }

        static Helper()
        {
            replaceDict.Add("\a", @"\a");
            replaceDict.Add("\b", @"\b");
            replaceDict.Add("\f", @"\f");
            replaceDict.Add("\n", @"\n");
            replaceDict.Add("\r", @"\r");
            replaceDict.Add("\t", @"\t");
            replaceDict.Add("\v", @"\v");

            replaceDict.Add("\\", @"\\");
            replaceDict.Add("\0", @"\0");

            //The SO parser gets fooled by the verbatim version 
            //of the string to replace - @"\"""
            //so use the 'regular' version
            replaceDict.Add("\"", "\\\"");

            for (var i = 0; i < charToString.Length; ++i)
            {
                var c = (char)i;
                charToString[i] = c.ToString();
            }
        }

        internal static int IndexOf<T>(this IEnumerable<T> items, Func<T, bool> func)
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

        internal static int IndexOf<T>(this T[] items, T item)
        {
            for (var i = 0; i < items.Length; i++)
                if (Object.Equals(items[i], item))
                    return i;

            return -1;
        }

        internal static bool One<T>(this IEnumerable<T> items, Func<T, bool> func)
        {
            foreach (var item in items)
            {
                if (func(item))
                    return true;
            }

            return false;
        }

        internal static bool UnionCheck<T>(this ISet<T> set, IEnumerable<T> toUnion)
        {
            bool changed = false;

            foreach (var item in toUnion)
            {
                changed = set.Add(item) || changed;
            }

            return changed;
        }

        public static T MaxItem<T>(this IEnumerable<T> items, Func<T, int> func)
        {
            var value = int.MinValue;
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

        public static float Clamp(float value, float min, float max)
        {
            // First we check to see if we're greater than the max
            value = (value > max) ? max : value;

            // Then we check to see if we're less than the min.
            value = (value < min) ? min : value;

            // There's no check to see if min > max.
            return value;
        }

        public static int Clamp(int value, int min, int max)
        {
            value = (value > max) ? max : value;
            value = (value < min) ? min : value;
            return value;
        }

        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }

        public static float LerpPrecise(float value1, float value2, float amount)
        {
            return ((1 - amount) * value1) + (value2 * amount);
        }

        /// <summary> 
        /// 获取枚举变量值的 Description 属性 
        /// </summary> 
        /// <param name="obj">枚举变量</param> 
        /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns> 
        public static string GetHelperDescrption(this object obj)
        {
            return GetHelperDescrption(obj, !obj.GetType().IsEnum);
        }

        /// <summary> 
        /// 获取枚举变量值的 Description 属性 
        /// </summary> 
        /// <param name="obj">枚举变量</param> 
        /// <param name="isTop">是否改变为返回该类、枚举类型的头 Description 属性，而不是当前的属性或枚举变量值的 Description 属性</param> 
        /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns> 
        public static string GetHelperDescrption(this object obj, bool isTop)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            try
            {
                Type _enumType = obj.GetType();
                DescriptionAttribute dna = null;
                if (isTop)
                {
                    dna = (DescriptionAttribute)Attribute.GetCustomAttribute(_enumType, typeof(DescriptionAttribute));
                }
                else
                {
                    FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, obj));
                    dna = (DescriptionAttribute)Attribute.GetCustomAttribute(
                       fi, typeof(DescriptionAttribute));
                }
                if (dna != null && string.IsNullOrEmpty(dna.Description) == false)
                    return dna.Description;
            }
            catch
            {
            }
            return obj.ToString();
        }

        public static TAttribute GetAttribute<TAttribute>(Enum enumValue) where TAttribute : Attribute
        {
            string value = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(value);
            var attribute = field.GetCustomAttribute(typeof(TAttribute), false) as TAttribute;
            return attribute;
        }

        public static TAttribute GetAttribute<TAttribute>(Type type) where TAttribute : Attribute
        {
            object[] dscArray = type.GetCustomAttributes(typeof(TAttribute), false);
            if (dscArray.Length == 0)
                return null;
            else
                return dscArray[0] as TAttribute;
        }

        public static TAttribute GetAttribute<TAttribute>(Type type, bool inherit) where TAttribute : Attribute
        {
            object[] dscArray = type.GetCustomAttributes(typeof(TAttribute), inherit);
            if (dscArray.Length == 0)
                return null;
            else
                return dscArray[0] as TAttribute;
        }

        public static T RemoveAndReturn<T>(this List<T> list, int index)
        {
            var item = list[index];
            list.RemoveAt(index);
            return item;
        }

        public static void Foreach<T1, T2>(IEnumerable<T1> items1, IEnumerable<T2> items2, Action<T1, T2> action)
        {
            foreach (var item in items1)
                foreach (var item2 in items2)
                    action(item, item2);
        }

        public static string[] ReadLines(this string source)
        {
            var list = new List<string>();
            var sr = new StringReader(source);
            var line = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                list.Add(line);
            }
            return list.ToArray();
        }

        public static T AddAndReturn<T>(this IList<T> list, T item)
        {
            list.Add(item);
            return item;
        }

        public static T First<T>(this IEnumerable<T> items, T defaultValue = default)
        {
            var enumer = items.GetEnumerator();
            if (enumer.MoveNext())
                return enumer.Current;

            return defaultValue;
        }

        public static T Second<T>(this IEnumerable<T> items, T defaultValue = default)
        {
            var enumer = items.GetEnumerator();
            if (enumer.MoveNext() && enumer.MoveNext())
                return enumer.Current;

            return defaultValue;
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

        public static T MinItem<T>(this IEnumerable<T> items, Func<T, int> func)
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

            return result;
        }

        public static IEnumerable<T> MinItems<T>(this IEnumerable<T> items, Func<T, int> func)
        {
            var value = int.MaxValue;

            foreach (var item in items)
            {
                var itemValue = func(item);
                if (itemValue < value)
                {
                    value = itemValue;
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

        public static bool IsUrl(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");
            return path.IndexOf("://", StringComparison.Ordinal) > 0;
        }

        public static string NormalizePath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return fileName;

            int i;

            bool isWeb = false;
            for (i = 0; i < fileName.Length; i++)
            {
                if (fileName[i] == '/' || fileName[i] == '\\')
                    break;
                if (fileName[i] == ':')
                {
                    if (i > 1)
                        isWeb = true;
                    break;
                }
            }

            char outputSeparator = isWeb ? '/' : System.IO.Path.DirectorySeparatorChar;

            StringBuilder result = new StringBuilder();
            if (isWeb == false && fileName.StartsWith(@"\\") || fileName.StartsWith("//"))
            {
                i = 2;
                result.Append(outputSeparator);
            }
            else
            {
                i = 0;
            }
            int segmentStartPos = i;
            for (; i <= fileName.Length; i++)
            {
                if (i == fileName.Length || fileName[i] == '/' || fileName[i] == '\\')
                {
                    int segmentLength = i - segmentStartPos;
                    switch (segmentLength)
                    {
                        case 0:
                            // ignore empty segment (if not in web mode)
                            // On unix, don't ignore empty segment if i==0
                            if (isWeb || (i == 0 && Environment.OSVersion.Platform == PlatformID.Unix))
                            {
                                result.Append(outputSeparator);
                            }
                            break;
                        case 1:
                            // ignore /./ segment, but append other one-letter segments
                            if (fileName[segmentStartPos] != '.')
                            {
                                if (result.Length > 0) result.Append(outputSeparator);
                                result.Append(fileName[segmentStartPos]);
                            }
                            break;
                        case 2:
                            if (fileName[segmentStartPos] == '.' && fileName[segmentStartPos + 1] == '.')
                            {
                                // remove previous segment
                                int j;
                                for (j = result.Length - 1; j >= 0 && result[j] != outputSeparator; j--) ;
                                if (j > 0)
                                {
                                    result.Length = j;
                                }
                                break;
                            }
                            else
                            {
                                // append normal segment
                                goto default;
                            }
                        default:
                            if (result.Length > 0) result.Append(outputSeparator);
                            result.Append(fileName, segmentStartPos, segmentLength);
                            break;
                    }
                    segmentStartPos = i + 1; // remember start position for next segment
                }
            }
            if (isWeb == false)
            {
                if (result.Length > 0 && result[result.Length - 1] == outputSeparator)
                {
                    result.Length -= 1;
                }
                if (result.Length == 2 && result[1] == ':')
                {
                    result.Append(outputSeparator);
                }
            }
            return result.ToString();
        }

        public static string GetRelativePath(string baseDirectoryPath, string absPath)
        {
            if (string.IsNullOrEmpty(baseDirectoryPath))
            {
                return absPath;
            }
            if (IsUrl(absPath) || IsUrl(baseDirectoryPath))
            {
                return absPath;
            }

            baseDirectoryPath = NormalizePath(baseDirectoryPath);
            absPath = NormalizePath(absPath);

            string[] bPath = baseDirectoryPath.Split(separators);
            string[] aPath = absPath.Split(separators);
            int indx = 0;
            for (; indx < Math.Min(bPath.Length, aPath.Length); ++indx)
            {
                if (!bPath[indx].Equals(aPath[indx], StringComparison.OrdinalIgnoreCase))
                    break;
            }

            if (indx == 0)
            {
                return absPath;
            }

            StringBuilder erg = new StringBuilder();

            if (indx == bPath.Length)
            {
                //				erg.Append('.');
                //				erg.Append(Path.DirectorySeparatorChar);
            }
            else
            {
                for (int i = indx; i < bPath.Length; ++i)
                {
                    erg.Append("..");
                    erg.Append(Path.DirectorySeparatorChar);
                }
            }
            erg.Append(String.Join(Path.DirectorySeparatorChar.ToString(), aPath, indx, aPath.Length - indx));
            return erg.ToString();
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

        public static IEnumerable<T> MaxItems<T>(this IEnumerable<T> items, Func<T, int> func)
        {
            var value = int.MinValue;

            foreach (var item in items)
            {
                var itemValue = func(item);
                if (itemValue > value)
                {
                    value = itemValue;
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

        public static IEnumerable<T> Range<T>(this IEnumerable<T> items, int start, int count)
        {
            if (items != null)
            {
                var index = 0;
                foreach (var item in items)
                {
                    if (index >= start + count)
                        break;

                    if (index >= start)
                        yield return item;

                    index++;
                }
            }
        }

        public static void For(int start, int length, Action<int> action)
        {
            for (var i = start; i < length; i++)
            {
                action(i);
            }
        }

        public static T As<T>(this object node) where T : class
        {
            //if (node == null || !(node is T))
            //    return default;

            return node as T;
        }

        public static Dictionary<TKey, TValue> ToDictionary2<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
        {
            var dict = new Dictionary<TKey, TValue>();
            foreach (var item in source)
                dict[keySelector(item)] = valueSelector(item);

            return dict;
        }

        public static TwoKeyDictionary<TKey, TKey2, TValue> ToTwoKeyDictionary<TSource, TKey, TKey2, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TKey2> keySelector2, Func<TSource, TValue> valueSelector)
        {
            var dict = new TwoKeyDictionary<TKey, TKey2, TValue>();
            foreach (var item in source)
                dict[keySelector(item), keySelector2(item)] = valueSelector(item);

            return dict;
        }

        public static IEnumerable<TResult> OfType<TSource, TResult>(this IEnumerable<TSource> items)
        {
            return items.Where(x => x is TResult).Cast<TResult>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string CharToString(char c)
        {
            if (c >= 0 && c < charToString.Length)
            {
                return charToString[c];
            }
            return c.ToString();
        }

        public static string PadRightBracket(this string l)
        {
            return l.PadRight(l.Length + (l.Count(X => X == '(') - l.Count(X => X == ')')), ')');
        }

        public static string PadLeftBracket(this string r)
        {
            return r.PadLeft(r.Length + (r.Count(X => X == ')') - r.Count(X => X == '(')), '(');
        }

    }
}
