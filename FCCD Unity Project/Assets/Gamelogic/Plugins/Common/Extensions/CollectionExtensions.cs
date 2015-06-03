using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Gamelogic
{
	/**
		This class provides useful extension methods for collections, mostly IEnumerable<T>.

		@version1_0
	*/
	public static class CollectionExtensions
	{
		/**
			Returns all elements of the source which are of FilterType.
		*/
		public static IEnumerable<TFilter> FilterByType<T, TFilter>(this IEnumerable<T> source)
			where T : class
			where TFilter : class, T
		{
			return source.Where(item => item as TFilter != null).Cast<TFilter>();
		}

		/**
			Removes all the elements in the list that does not satisfy the predicate.
		*/
		public static void RemoveAllBut<T>(this List<T> source, Predicate<T> match)
		{
			Predicate<T> nomatch = item => !match(item);

			source.RemoveAll(nomatch);
		}

		/**
			Returns whether this source is empty.
		*/
		public static bool IsEmpty<T>(this ICollection<T> collection)
		{
			return collection.Count == 0;
		}
		
		/**
			Add all elements of other to the given source.
		*/
		public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> other)
		{
			if (other == null)//nothing to add
			{
				return;
			}

			foreach (var obj in other)
			{
				collection.Add(obj);
			}
		}

		/**
			Returns a pretty string representation of the given list. The resulting string looks something like
			<code>{a, b, c}</code>.
		*/
		public static string ListToString<T>(this IEnumerable<T> source)
		{
			if (source == null)
			{
				return "null";
			}

			var s = "[";

			s += source.Aggregate(s, (res, x) => res + ", " + x.ToString());
			s += "]";

			return s;
		}

		/**
			Returns an enumerable of all elements of the given list	but the first,
			keeping them in order.
		*/
		public static IEnumerable<T> ButFirst<T>(this IEnumerable<T> source)
		{
			return source.Skip(1);
		}

		/**
			Returns an enumarable of all elements in the given 
			list but the last, keeping them in order.
		*/
		public static IEnumerable<T> ButLast<T>(this IEnumerable<T> source)
		{
			var lastX = default(T);
			var first = true;

			foreach (var x in source)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					yield return lastX;
				}

				lastX = x;
			}
		}

		/**
			Finds the maximum element in the source as scored by the given function.
		*/
		public static T MaxBy<T>(this IEnumerable<T> source, Func<T, IComparable> score)
		{
			return source.Aggregate((x, y) => score(x).CompareTo(score(y)) > 0 ? x : y);
		}

		/**
			Finds the maximum element in the source as scored by the given function.
		*/
		//public static T MinBy<T>(this IEnumerable<T> source, Func<T, IComparable> score)
		//{
		//	return source.Aggregate((x, y) => score(x).CompareTo(score(y)) < 0 ? x : y);
		//}

		public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
	Func<TSource, TKey> selector)
		{
			return source.MinBy(selector, Comparer<TKey>.Default);
		}

		public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
			Func<TSource, TKey> selector, IComparer<TKey> comparer)
		{
			source.ThrowIfNull("source");
			selector.ThrowIfNull("selector");
			comparer.ThrowIfNull("comparer");
			
			using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
			{
				if (!sourceIterator.MoveNext())
				{
					throw new InvalidOperationException("Sequence was empty");
				}

				TSource min = sourceIterator.Current;
				TKey minKey = selector(min);
				
				while (sourceIterator.MoveNext())
				{
					TSource candidate = sourceIterator.Current;
					TKey candidateProjected = selector(candidate);
					if (comparer.Compare(candidateProjected, minKey) < 0)
					{
						min = candidate;
						minKey = candidateProjected;
					}
				}
				return min;
			}
		}

		public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
	Func<TSource, TKey> selector)
		{
			return source.MaxBy(selector, Comparer<TKey>.Default);
		}

		public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
			Func<TSource, TKey> selector, IComparer<TKey> comparer)
		{
			source.ThrowIfNull("source");
			selector.ThrowIfNull("selector");
			comparer.ThrowIfNull("comparer");

			using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
			{
				if (!sourceIterator.MoveNext())
				{
					throw new InvalidOperationException("Sequence was empty");
				}

				TSource max = sourceIterator.Current;
				TKey maxKey = selector(max);

				while (sourceIterator.MoveNext())
				{
					TSource candidate = sourceIterator.Current;
					TKey candidateProjected = selector(candidate);
					
					if (comparer.Compare(candidateProjected, maxKey) > 0)
					{
						max = candidate;
						maxKey = candidateProjected;
					}
				}

				return max;
			}
		}

		private static void ThrowIfNull(this object o, string message)
		{
			if(o == null) throw new NullReferenceException(message);
		}


		/**
			Returns a enumerable with elements in order, but the first element is moved to the end.
		*/
		//TODO consider changing left to something more universal
		public static IEnumerable<T> RotateLeft<T>(this IEnumerable<T> source)
		{
			var enumeratedList = source as IList<T> ?? source.ToList();
			return enumeratedList.ButFirst().Concat(enumeratedList.Take(1));
		}

		/**
			Returns a enumerable with elements in order, but the last element is moved to the front.
		*/
		public static IEnumerable<T> RotateRight<T>(this IEnumerable<T> source)
		{
			var enumeratedList = source as IList<T> ?? source.ToList();
			yield return enumeratedList.Last();

			foreach (var item in enumeratedList.ButLast())
			{
				yield return item;
			}
		}

		/**
			Returns a random element from the list.
		*/
		public static T RandomItem<T>(this IEnumerable<T> source)
		{
			return source.SampleRandom(1).First();
		}

		/**
			Returns a random sample from the list.
		*/
		public static IEnumerable<T> SampleRandom<T>(this IEnumerable<T> source, int sampleCount)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			if (sampleCount < 0)
			{
				throw new ArgumentOutOfRangeException("sampleCount");
			}

			/* Reservoir sampling. */
			var samples = new List<T>();

			//Must be 1, otherwise we have to use Range(0, i + 1)
			var i = 1;

			foreach (var item in source)
			{
				if (i <= sampleCount)
				{
					samples.Add(item);
				}
				else
				{
					// Randomly replace elements in the reservoir with a decreasing probability.
					var r = Random.Range(0, i);

					if (r < sampleCount)
					{
						samples[r] = item;
					}
				}

				i++;
			}

			return samples;
		}
		
		/**	
			Shuffles a list.
		*/
		public static void Shuffle<T>(this IList<T> source)  
		{  
		    var n = source.Count;  
		    
			while (n > 1) 
			{  
		        n--;  
		        var k = Random.Range(0, n + 1);  
		        var value = source[k];  
		        source[k] = source[n];  
		        source[n] = value;  
		    }  
		}

		public static IEnumerable<T> TakeHalf<T>(this IEnumerable<T> source)
		{
			int count = source.Count();

			return source.Take(count/2);
		}
	}
}