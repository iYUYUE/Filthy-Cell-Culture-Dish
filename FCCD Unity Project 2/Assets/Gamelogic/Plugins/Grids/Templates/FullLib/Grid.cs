#if !IgnoreRectLib
//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013-15 Gamelogic (Pty) Ltd    //
//----------------------------------------------//

// Auto-generated File

using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

namespace Gamelogic.Grids
{
	public partial class RectGrid<TCell>
	{
		#region Creation
		/**
			Construct a new grid in the default shape with the given width and height.
			No transformations are applied to the grid.

			Normally, the static factory methods or shape building methods should be used to create grids.
			These constructors are provided for advanced usage.

			@link_constructing_grids  
		*/
		public RectGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		/**
			Construct a new grid whose cells are determined by the given test function.

			The test function should only return true for points within the bounds of the default shape.

			No transformations are applied to the grid.

			Normally, the static factory methods or shape building methods should be used to create grids.
			These constructors are provided for advanced usage.

			@link_constructing_grids 
		*/
		public RectGrid(int width, int height, Func<RectPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		/**
			Construct a new grid whose cells are determined by the given test function.

			The function should only return true for points within the bounds of the rectangle when 
			the given transforms are applied to them.

			Normally, the static factory methods or shape building methods should be used to create grids.
			These constructors are provided for advanced usage.

			@link_constructing_grids 
		*/
		public RectGrid(int width, int height, Func<RectPoint, bool> isInside, RectPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset), RectPoint.MainDirections)
		{}	
		#endregion

		#region Properties
		override protected RectPoint GridOrigin
		{
			get
			{
				return PointTransform(RectPoint.Zero);
			}
		}
		#endregion

		#region Clone methods
		/**
			Returns a grid in the same shape, but with contents in the new type.
		*/
		override public IGrid<TNewCellType, RectPoint> CloneStructure<TNewCellType>()
		{
			return new RectGrid<TNewCellType>(width, height, contains, PointTransform, InversePointTransform, NeighborDirections);
		}
		#endregion

		#region Shape Fluents
		/**
			Use this method to begin a shape building sequence. 

			@link_constructing_grids
		*/
		public static RectOp<TCell> BeginShape()
		{
			return new RectOp<TCell>();
		}
		#endregion

		#region ToString
		override public string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<RectPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}

	public partial class DiamondGrid<TCell>
	{
		#region Creation
		/**
			Construct a new grid in the default shape with the given width and height.
			No transformations are applied to the grid.

			Normally, the static factory methods or shape building methods should be used to create grids.
			These constructors are provided for advanced usage.

			@link_constructing_grids  
		*/
		public DiamondGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		/**
			Construct a new grid whose cells are determined by the given test function.

			The test function should only return true for points within the bounds of the default shape.

			No transformations are applied to the grid.

			Normally, the static factory methods or shape building methods should be used to create grids.
			These constructors are provided for advanced usage.

			@link_constructing_grids 
		*/
		public DiamondGrid(int width, int height, Func<DiamondPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		/**
			Construct a new grid whose cells are determined by the given test function.

			The function should only return true for points within the bounds of the rectangle when 
			the given transforms are applied to them.

			Normally, the static factory methods or shape building methods should be used to create grids.
			These constructors are provided for advanced usage.

			@link_constructing_grids 
		*/
		public DiamondGrid(int width, int height, Func<DiamondPoint, bool> isInside, DiamondPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset), DiamondPoint.MainDirections)
		{}	
		#endregion

		#region Properties
		override protected DiamondPoint GridOrigin
		{
			get
			{
				return PointTransform(DiamondPoint.Zero);
			}
		}
		#endregion

		#region Clone methods
		/**
			Returns a grid in the same shape, but with contents in the new type.
		*/
		override public IGrid<TNewCellType, DiamondPoint> CloneStructure<TNewCellType>()
		{
			return new DiamondGrid<TNewCellType>(width, height, contains, PointTransform, InversePointTransform, NeighborDirections);
		}
		#endregion

		#region Shape Fluents
		/**
			Use this method to begin a shape building sequence. 

			@link_constructing_grids
		*/
		public static DiamondOp<TCell> BeginShape()
		{
			return new DiamondOp<TCell>();
		}
		#endregion

		#region ToString
		override public string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<DiamondPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}

}
#endif
