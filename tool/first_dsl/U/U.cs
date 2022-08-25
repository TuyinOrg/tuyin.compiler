using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static partial class U
{
	public static T DeepCopy<T>(this T src)
	{
		if (!typeof(T).IsSerializable)
			throw new ArgumentException("The type must be serializable.", "src");

		// Don't serialize a null object, simply return the default for that object
		if (Object.ReferenceEquals(src, null))
			return default(T);

		BinaryFormatter formatter = new BinaryFormatter();
		using (Stream ms = new MemoryStream())
		{
			formatter.Serialize(ms, src);
			ms.Seek(0, SeekOrigin.Begin);
			return (T)formatter.Deserialize(ms);
		}
	}

	public static void Swap<T>(ref T a, ref T b)
	{
		var t = a;
		a = b;
		b = t;
	}

	public static string F(string fmt, params object[] args)
	{
		return string.Format(CultureInfo.InvariantCulture, fmt, args);
	}


	public static IEnumerable<int> Count(int a, int b)
	{
		for (var i = a; i < b; ++i)
			yield return i;
	}

	/// <summary>
	/// struttura utilizzata per ritornare le info 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public struct FirstLastItem<T>
	{
		public T First;
		public T Previous;
		public T Current;
		public T Next;
		public bool IsFirst;
		public bool IsLast;
	}



	/// <summary>
	/// dato un enumerabile, ritorna un enumerabile che dice se siamo all'inizio della collezione, alla fine, e il prossimo/precedente elemento.
	/// </summary>
	/// <typeparam bname="T">un qualunque tipo</typeparam>
	/// <param name="e">l'enumerabile</param>
	/// <returns>l'enumerabile con proprieta` estese.</returns>
	public static IEnumerable<FirstLastItem<T>> FirstLast<T>(IEnumerable<T> e)
	{
		var en = e.GetEnumerator();

		if (en.MoveNext() == false)
		{
			yield break;  // fine file
		}

		FirstLastItem<T> ret;
		ret.IsFirst = true;
		ret.Current = en.Current;
		ret.First = en.Current;
		ret.Previous = default(T);

		if (en.MoveNext())
		{
			ret.IsLast = false;
			ret.Next = en.Current;
			yield return ret;
		}
		else
		{
			ret.IsLast = true;
			ret.Next = default(T);
			yield return ret;
			yield break;  // fine file
		}

		ret.IsFirst = false;

		while (ret.IsLast == false)
		{
			ret.Previous = ret.Current;
			ret.Current = ret.Next;

			if (en.MoveNext())
			{
				ret.Next = en.Current;
			}
			else
			{
				ret.IsLast = true;
				ret.Next = default(T);
			}
			yield return ret;
		}
	}


	public delegate bool SameGroup<T>(T a, T b);
	public static IEnumerable<List<T>> Group<T>(IEnumerable<T> l, SameGroup<T> sameGroup)
	{
		List<T> ret = new List<T>();
		T last = default(T);
		foreach (T t in l)
		{
			if (ret.Count > 0 && sameGroup(last, t) == false)
			{
				yield return ret;
				ret = new List<T>();
			}
			ret.Add(t);
			last = t;
		}
		if (ret.Count > 0)
			yield return ret;
	}
	public static IEnumerable<List<T>> Group<T>(IEnumerable l, SameGroup<T> sameGroup)
	{
		List<T> ret = new List<T>();
		T last = default(T);
		foreach (T t in l)
		{
			if (ret.Count > 0 && sameGroup(last, t) == false)
			{
				yield return ret;
				ret = new List<T>();
			}
			ret.Add(t);
			last = t;
		}
		if (ret.Count > 0)
			yield return ret;
	}
}
