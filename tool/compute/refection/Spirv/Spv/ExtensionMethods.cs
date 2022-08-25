using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Toe.SPIRV.Spv
{
    internal static class ExtensionMethods
    {
        public static uint GetWordCount(this uint value)
        {
            return 1;
        }

        public static uint GetWordCount(this Op value)
        {
            return 1;
        }

        public static uint GetWordCount(this IList<uint> value)
        {
            return (uint)value.Count;
        }

        public static uint GetWordCount(this IList<IdRef> value)
        {
            if (value == null)
                return 0;
            return (uint)value.Count;
        }

        public static uint GetWordCount(this IList<PairLiteralIntegerIdRef> value)
        {
            return (uint)value.Select(_ => (long)_.GetWordCount()).Sum();
        }

        public static uint GetWordCount(this IList<PairIdRefLiteralInteger> value)
        {
            return (uint)value.Select(_ => (long)_.GetWordCount()).Sum();
        }

        public static uint GetWordCount(this IList<PairIdRefIdRef> value)
        {
            return (uint)value.Select(_ => (long)_.GetWordCount()).Sum();
        }

        public static uint GetWordCount(this string value)
        {
            return WordWriter.GetWordCount(value);
        }
        
        public static void Write(this uint value, WordWriter writer)
        {
            writer.WriteWord(value);
        }

        public static void Write(this Op value, WordWriter writer)
        {
            writer.WriteWord((uint)value);
        }

        public static void Write(this IList<uint> value, WordWriter writer)
        {
            foreach (var item in value)
            {
                item.Write(writer);
            }
        }

        public static void Write(this IList<IdRef> value, WordWriter writer)
        {
            foreach (var item in value)
            {
                item.Write(writer);
            }
        }

        public static void Write(this IList<PairLiteralIntegerIdRef> value, WordWriter writer)
        {
            foreach (var item in value)
            {
                item.Write(writer);
            }
        }

        public static void Write(this IList<PairIdRefIdRef> value, WordWriter writer)
        {
            foreach (var item in value)
            {
                item.Write(writer);
            }
        }

        public static void Write(this IList<PairIdRefLiteralInteger> value, WordWriter writer)
        {
            foreach (var item in value)
            {
                item.Write(writer);
            }
        }
        

        public static void Write(this string value, WordWriter writer)
        {
            writer.Write(value);
        }
    }
}