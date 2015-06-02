using UnityEngine;

namespace Gamelogic.Grids
{
	/**
		Inherit from this class to implement custom map for an 
		grid builder.

		This class should return a map. To have the grid builder 
		used this map, you should set the Map to Custom in the 
		editor, and attach this script to the same game object
		as the grid builder.

		@version1_8
		@ingroup UnityEditorSupport
	*/
	public abstract class CustomMapBuilder : MonoBehaviour
	{
		/**
			@implementers note:
				Check the type; if it is "your" type, return the map.
				Otherwise return null.
		*/
		public virtual WindowedMap<TPoint> CreateWindowedMap<TPoint>()
			where TPoint : IGridPoint<TPoint>
		{
			return null;
		}
	}
}