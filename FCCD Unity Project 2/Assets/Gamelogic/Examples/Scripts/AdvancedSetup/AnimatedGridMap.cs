using Gamelogic.Grids;
using UnityEngine;

public class AnimatedGridMap : CustomMapBuilder
{
	override public WindowedMap<TPoint> CreateWindowedMap<TPoint>()
	{

		if (typeof(TPoint) == typeof(PointyHexPoint))
		{
			var map = new PointyHexMap(new Vector2(69, 80))
				.Animate((x, t) => x.Rotate(45 + 0*t), (x, t) => x.Rotate(-45 + 0*t))
				.Animate((x, t) => x + new Vector2(75*Mathf.Sin(t/5.0f), 0),
					(x, t) => x - new Vector2(75*Mathf.Sin(t/5.0f), 0))
				.Animate((x, t) => x*(1.5f + 0.5f*Mathf.Sin(t*7)),
					(x, t) => x/(1.5f + 0.5f*Mathf.Sin(t*7)))
				.WithWindow(ExampleUtils.ScreenRect);

			return (WindowedMap<TPoint>) (object) map;
		}

		return null;
	}
}
