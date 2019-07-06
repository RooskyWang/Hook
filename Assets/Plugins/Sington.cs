public class Sington<T> where T : class, new()
{
	private static T inst;

	public static T Instance
	{
		get
		{
			if (inst == null)
				inst = new T();
			return inst;
		}
	}
}
