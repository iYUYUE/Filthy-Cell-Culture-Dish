using Gamelogic.Grids;

/**
	Extensions for PointyTriOp that defines three new shapes.
*/
using UnityEngine;

public static class PointyTriOpExtensions
{
	public static PointyTriShapeInfo<TCell> Ring<TCell>(this PointyTriOp<TCell> op)
	{
		return op
			.BeginGroup()	//If you do not use begin group and EndGroup
			// the shapes will behave unexpectedly when combined
			// with other shapes.
				.Hexagon(4)
				.Translate(-2, -2)
				.Difference()
				.Hexagon(2)
				.Translate(-5, 4) //4 -1 > 9 5
				.Intersection()
				.Star(3)
				.Translate(0, 3)
			.EndGroup(op);
	}

	public static PointyTriShapeInfo<TCell> Chain<TCell>(this PointyTriOp<TCell> op)
	{
		return op
			.BeginGroup()
				.Ring()
				.Translate(8, -4)
				.Union()
				.Ring()
				.Translate(8, -4)
				.Union()
				.Ring()
				.Translate(8, -4)
				.Union()
				.Ring()
				.Translate(8, -4)
				.Union()
				.Ring()
				.Translate(8, -4)
				.Union()
				.Ring()
			.EndGroup(op);
	}

	public static PointyTriShapeInfo<TCell> ChainMail<TCell>(this PointyTriOp<TCell> op)
	{
		return op
			.BeginGroup()
				.Chain()
				.Translate(0, 4)
				.Union()
				.Chain()
				.Translate(0, 4)
				.Union()
				.Chain()
				.Translate(0, 4)
				.Union()
				.Chain()
				.Translate(0, 4)
				.Union()
				.Chain()
			.EndGroup(op);
	}
}

public class ChainMailGrid : CustomGridBuilder
{
	public override IGrid<TCell, TPoint> MakeGrid<TCell, TPoint>()
	{
		if (typeof (TPoint) == typeof (PointyTriPoint))
		{
			var grid = PointyTriGrid<TCell>
				.BeginShape()
					//.ChainMail() //You can now chain the newly defined method to BeginShape
					.ChainMail()
				.EndShape();

			return (IGrid<TCell, TPoint>) grid;
		}

		Debug.LogError("<color=blue><b>" + GetType().ToString() + "</b></color> does not support grids for points of type " +
		               typeof (TPoint));


		return null;
	}
}
