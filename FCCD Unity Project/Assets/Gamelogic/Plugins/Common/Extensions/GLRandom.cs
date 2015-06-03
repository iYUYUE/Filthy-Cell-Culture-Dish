using UnityEngine;

namespace Gamelogic
{
	/**
		Some convenience functions for random bools and integers.

		@version1_2
	*/
	public static class GLRandom
	{
		/**
			Generates a random bool, true with the given probability.
		*/
		public static bool Bool(float probability)
		{
			return Random.value < probability;
		}

		/**
			Generates a Random integer between 0 inclusive and the given max, exclusive.
		*/
		public static int Range(int max)
		{
			return Random.Range(0, max);
		}
	}
}