﻿#if !IgnoreRectLib
//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

// Auto-generated File
using System;
using System.Collections.Generic;

namespace Gamelogic.Grids
{
	public partial class RectGrid<TCell>
	{
		#region Construction
		public RectGrid(
			int width, 
			int height, 
			Func<RectPoint, bool> isInside, 
			Func<RectPoint, RectPoint> gridPointTransform, 
			Func<RectPoint, RectPoint> inverseGridPointTransform
		):			
			this(width, height, isInside, gridPointTransform, inverseGridPointTransform, RectPoint.MainDirections)
		{}

		public RectGrid(
			int width, 
			int height, 
			Func<RectPoint, bool> isInside, 
			Func<RectPoint, RectPoint> gridPointTransform, 
			Func<RectPoint, RectPoint> inverseGridPointTransform,
			IEnumerable<RectPoint> neighborDirections
		):			
			base(width, height, isInside, gridPointTransform, inverseGridPointTransform, neighborDirections)
		{}
		#endregion

		#region Shape Functions		
		public static bool DefaultContains(RectPoint point, int width, int height)
		{
			ArrayPoint storagePoint = ArrayPointFromGridPoint(point);

			return
				storagePoint.X >= 0 &&
				storagePoint.X < width &&
				storagePoint.Y >= 0 &&
				storagePoint.Y < height;
		}
		#endregion

		#region Wrapped Grids
		/**
			Returns a grid wrapped along a parallelogram.

			@since 1.7
		*/
		public static WrappedGrid<TCell, RectPoint> WrappedParallelogram(int width, int height)
		{
			var grid = Parallelogram(width, height);
			var wrapper = new RectParallelogramWrapper(width, height);
			var wrappedGrid = new WrappedGrid<TCell, RectPoint>(grid, wrapper);

			return wrappedGrid;
		}

		/**
			Returns a grid wrapped horizontally along a parallelogram.

			@since 1.7
		*/
		public static WrappedGrid<TCell, RectPoint> HorizontallyWrappedParallelogram(int width, int height)
		{
			var grid = Parallelogram(width, height);
			var wrapper = new RectHorizontalWrapper(width);
			var wrappedGrid = new WrappedGrid<TCell, RectPoint>(grid, wrapper);

			return wrappedGrid;
		}

		/**
			Returns a grid wrapped vertically along a parallelogram.

			@since 1.7
		*/
		public static WrappedGrid<TCell, RectPoint> VerticallyWrappedParallelogram(int width, int height)
		{
			var grid = Parallelogram(width, height);
			var wrapper = new RectVerticalWrapper(height);
			var wrappedGrid = new WrappedGrid<TCell, RectPoint>(grid, wrapper);

			return wrappedGrid;
		}
		#endregion
	}
	public partial class DiamondGrid<TCell>
	{
		#region Construction
		public DiamondGrid(
			int width, 
			int height, 
			Func<DiamondPoint, bool> isInside, 
			Func<DiamondPoint, DiamondPoint> gridPointTransform, 
			Func<DiamondPoint, DiamondPoint> inverseGridPointTransform
		):			
			this(width, height, isInside, gridPointTransform, inverseGridPointTransform, DiamondPoint.MainDirections)
		{}

		public DiamondGrid(
			int width, 
			int height, 
			Func<DiamondPoint, bool> isInside, 
			Func<DiamondPoint, DiamondPoint> gridPointTransform, 
			Func<DiamondPoint, DiamondPoint> inverseGridPointTransform,
			IEnumerable<DiamondPoint> neighborDirections
		):			
			base(width, height, isInside, gridPointTransform, inverseGridPointTransform, neighborDirections)
		{}
		#endregion

		#region Shape Functions		
		public static bool DefaultContains(DiamondPoint point, int width, int height)
		{
			ArrayPoint storagePoint = ArrayPointFromGridPoint(point);

			return
				storagePoint.X >= 0 &&
				storagePoint.X < width &&
				storagePoint.Y >= 0 &&
				storagePoint.Y < height;
		}
		#endregion

		#region Wrapped Grids
		/**
			Returns a grid wrapped along a parallelogram.

			@since 1.7
		*/
		public static WrappedGrid<TCell, DiamondPoint> WrappedParallelogram(int width, int height)
		{
			var grid = Parallelogram(width, height);
			var wrapper = new DiamondParallelogramWrapper(width, height);
			var wrappedGrid = new WrappedGrid<TCell, DiamondPoint>(grid, wrapper);

			return wrappedGrid;
		}

		/**
			Returns a grid wrapped horizontally along a parallelogram.

			@since 1.7
		*/
		public static WrappedGrid<TCell, DiamondPoint> HorizontallyWrappedParallelogram(int width, int height)
		{
			var grid = Parallelogram(width, height);
			var wrapper = new DiamondHorizontalWrapper(width);
			var wrappedGrid = new WrappedGrid<TCell, DiamondPoint>(grid, wrapper);

			return wrappedGrid;
		}

		/**
			Returns a grid wrapped vertically along a parallelogram.

			@since 1.7
		*/
		public static WrappedGrid<TCell, DiamondPoint> VerticallyWrappedParallelogram(int width, int height)
		{
			var grid = Parallelogram(width, height);
			var wrapper = new DiamondVerticalWrapper(height);
			var wrappedGrid = new WrappedGrid<TCell, DiamondPoint>(grid, wrapper);

			return wrappedGrid;
		}
		#endregion
	}
}
#endif
