using System.Collections.Generic;
using System.Globalization;

namespace ULib
{
	public static class Num
	{
		public static byte ToByte(this string s) { return byte.Parse(s, CultureInfo.InvariantCulture); }
		public static sbyte ToSByte(this string s) { return sbyte.Parse(s, CultureInfo.InvariantCulture); }
		public static short ToShort(this string s) { return short.Parse(s, CultureInfo.InvariantCulture); }
		public static ushort ToUShort(this string s) { return ushort.Parse(s, CultureInfo.InvariantCulture); }
		public static int ToInt(this string s) { return int.Parse(s, CultureInfo.InvariantCulture); }
		public static uint ToUInt(this string s) { return uint.Parse(s, CultureInfo.InvariantCulture); }
		public static long ToLong(this string s) { return long.Parse(s, CultureInfo.InvariantCulture); }
		public static ulong ToULong(this string s) { return ulong.Parse(s, CultureInfo.InvariantCulture); }
		public static decimal ToDecimal(this string s) { return decimal.Parse(s, CultureInfo.InvariantCulture); }
	}
}

public static partial class U
{
	/// <summary>
	/// genera tutte le combinazione di "n" cifre con ogni cifra che puo' assumere da 0 a v-1 interi.
	/// Combine(3, 2) ritorna 000,100,010,110 ecc
	/// </summary>
	/// <param name="n"></param>
	/// <param name="v"></param>
	/// <returns></returns>
	public static IEnumerable<int[]> Combine(int n, int v)
	{
		var r = new int[n];
		for (int p = 0; p < n; ++p)
			r[p] = 0;

		for (; ; )
		{
			yield return r;

			for (int p = 0; p < n; ++p)
			{
				r[p] += 1;
				if (r[p] < v) break;
				r[p] = 0;
			}

			int c = 0;
			for (int p = 0; p < n; ++p)
				c += r[p];
			if (c == 0)
				yield break;
		}
	}

}

