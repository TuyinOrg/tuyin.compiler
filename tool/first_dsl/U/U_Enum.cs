using System;

public static partial class U
{
	public static class EnumUtils
	{
		public static bool IsDefined<T>(string enumString) where T : struct
		{
			if (string.IsNullOrEmpty(enumString))
				throw new NullReferenceException(enumString);
			if (typeof(T).IsEnum == false)
				throw new ArgumentException("Type given must be an Enum", "T");
			return Enum.IsDefined(typeof(T), enumString);
		}
		public static T ParseEnum<T>(string enumString) where T : struct
		{
			if (string.IsNullOrEmpty(enumString))
				throw new NullReferenceException(enumString);
			if (typeof(T).IsEnum == false)
				throw new ArgumentException("Type given must be an Enum", "T");
			return (T)Enum.Parse(typeof(T), enumString, true);
		}
		public static string[] GetNames<T>() where T : struct
		{
			if (typeof(T).IsEnum == false)
				throw new ArgumentException("Type given must be an Enum", "T");
			return Enum.GetNames(typeof(T));
		}
	}
}
