public static partial class U
{
	public class Tuple<T1, T2>
	{
		public T1 Item1;
		public T2 Item2;

		public Tuple(T1 a, T2 b)
		{
			Item1 = a;
			Item2 = b;
		}

		public static Tuple<T1, T2> Create(T1 a, T2 b) => new Tuple<T1, T2>(a, b);
	}
}
