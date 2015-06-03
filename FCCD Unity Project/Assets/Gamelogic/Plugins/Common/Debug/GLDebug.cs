using UnityEngine;

public static class GLDebug
{
	/**
		Check whether the condition is true, and print an error message if it is not.

		@version1_2
	*/
	public static void Assert(bool condition, string message)
	{
		if (!condition)
		{
			Debug.LogError(message);
		}
	}
}
