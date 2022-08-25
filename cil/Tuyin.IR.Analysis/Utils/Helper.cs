using System;
using System.Collections.Generic;
using System.Linq;
using Tuyin.IR.Reflection;
using Path = System.IO.Path;

namespace Tuyin.IR.Analysis.Utils
{
    static class Helper
    {
        public static IEnumerable<T> GetRange<T>(this IEnumerable<T> items, int index, int length) 
        {
            var curr = index == 0 ? 
                items.GetEnumerator() : 
                items.Skip(index).GetEnumerator();

            for (var i = 0; i < length; i++) 
            {
                if (!curr.MoveNext())
                    break;

                yield return curr.Current;
            }
        }

        public static IEnumerable<T> GetRange<T>(this IReadOnlyList<T> items, int index, int length)
        {
            var end = index + length;
            for (var i = index; i < end; i++)
                yield return items[i];
        }

        public static void ReportError(this StatmentBuilder cache, Errors error, ISourceSpan sourceSpan)
        {
            cache.ReportError((int)error, error.GetHelperDescrption(), sourceSpan);
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

    }
}
