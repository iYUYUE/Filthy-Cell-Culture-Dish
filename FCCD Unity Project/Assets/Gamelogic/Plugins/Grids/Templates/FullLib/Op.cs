#if !IgnoreRectLib
//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

// Auto-generated File

using System;

namespace Gamelogic.Grids
{
	/**
		Class for making RectGrids in different shapes.
		
		@link_constructing_grids
			
		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0
		@see @ref AbstractOp
		@ingroup BuilderInterface
	*/
	public partial class RectOp<TCell> : AbstractOp<ShapeStorageInfo<RectPoint>>
	{
		public RectOp(){}

		public RectOp(
			ShapeStorageInfo<RectPoint> leftShapeInfo,
			Func<ShapeStorageInfo<RectPoint>, ShapeStorageInfo<RectPoint>, ShapeStorageInfo<RectPoint>> combineShapeInfo) :
			base(leftShapeInfo, combineShapeInfo)
		{}

		/**
			Use this function to create shapes to ensure they fit into memory.
		
			The test function can test shapes anywhere in space. If you specify the bottom corner 
			(in terms of the storage rectangle), the shape is automatically translated in memory 
			to fit, assuming memory width and height is big enough.

			Strategy for implementing new shapes:
				- First, determine the test function.
				- Next, draw a storage rectangle that contains the shape.
				- Determine the storgae rectangle width and height.
				- Finally, determine the grid-space coordinate of the left bottom corner of the storage rectangle.
		
			Then define your function as follows:

			\code{cs}
			public RectShapeInfo<TCell> MyShape()
			{
				Shape(stargeRectangleWidth, storageRectangleHeight, isInsideMyShape, storageRectangleBottomleft);
			}
			\endcode

			\param width The widh of the storage rectangle
			\param height The height of the storage rectangle
			\param isInside A function that returns true if a passed point lies inside the shape being defined
			\param bottomLeftCorner The grid-space coordinate of the bottom left corner of the storage rect.

		*/
		public RectShapeInfo<TCell> Shape(int width, int height, Func<RectPoint, bool> isInside, RectPoint bottomLeftCorner)
		{
			var shapeInfo = MakeShapeStorageInfo<RectPoint>(width, height, x=>isInside(x + bottomLeftCorner));
			return new RectShapeInfo<TCell>(shapeInfo).Translate(bottomLeftCorner);
		}

		/**
			The same as Shape with all parameters, but with bottomLeft Point set to  RectPoint.Zero.
		*/
		public RectShapeInfo<TCell> Shape(int width, int height, Func<RectPoint, bool> isInside)
		{
			return Shape(width, height, isInside, RectPoint.Zero);
		}

		/**
			Creates the grid in a shape that spans 
			the entire storage rectangle of the given width and height.
		*/
		[ShapeMethod]
		public RectShapeInfo<TCell> Default(int width, int height)
		{
			var rawInfow = MakeShapeStorageInfo<RectPoint>(
				width, 
				height,
				x => RectGrid<TCell>.DefaultContains(x, width, height));

			return new RectShapeInfo<TCell>(rawInfow);
		}

		/**
			Makes a grid with a single cell that corresponds to the origin.
		*/
		[ShapeMethod]
		public RectShapeInfo<TCell> Single()
		{
			var rawInfow = MakeShapeStorageInfo<RectPoint>(
				1, 
				1,
				x => x == RectPoint.Zero);

			return new RectShapeInfo<TCell>(rawInfow);
		}

		/**
			Starts a compound shape operation.

			Any shape that is defined in terms of other shape operations must use this method, and use Endgroup() to end the definition.

				public static RectShapeInfo<TCell> MyCustomShape(this RectOp<TCell> op)
				{
					return 
						BeginGroup()
							.Shape1()
							.Union()
							.Shape2()
						.EndGroup(op);
				}

			@since 1.1
		*/
		public RectOp<TCell> BeginGroup()
		{
			return RectGrid<TCell>.BeginShape();
		}
	}
	/**
		Class for making DiamondGrids in different shapes.
		
		@link_constructing_grids
			
		@copyright Gamelogic.
		@author Herman Tulleken
		@since 1.0
		@see @ref AbstractOp
		@ingroup BuilderInterface
	*/
	public partial class DiamondOp<TCell> : AbstractOp<ShapeStorageInfo<DiamondPoint>>
	{
		public DiamondOp(){}

		public DiamondOp(
			ShapeStorageInfo<DiamondPoint> leftShapeInfo,
			Func<ShapeStorageInfo<DiamondPoint>, ShapeStorageInfo<DiamondPoint>, ShapeStorageInfo<DiamondPoint>> combineShapeInfo) :
			base(leftShapeInfo, combineShapeInfo)
		{}

		/**
			Use this function to create shapes to ensure they fit into memory.
		
			The test function can test shapes anywhere in space. If you specify the bottom corner 
			(in terms of the storage rectangle), the shape is automatically translated in memory 
			to fit, assuming memory width and height is big enough.

			Strategy for implementing new shapes:
				- First, determine the test function.
				- Next, draw a storage rectangle that contains the shape.
				- Determine the storgae rectangle width and height.
				- Finally, determine the grid-space coordinate of the left bottom corner of the storage rectangle.
		
			Then define your function as follows:

			\code{cs}
			public DiamondShapeInfo<TCell> MyShape()
			{
				Shape(stargeRectangleWidth, storageRectangleHeight, isInsideMyShape, storageRectangleBottomleft);
			}
			\endcode

			\param width The widh of the storage rectangle
			\param height The height of the storage rectangle
			\param isInside A function that returns true if a passed point lies inside the shape being defined
			\param bottomLeftCorner The grid-space coordinate of the bottom left corner of the storage rect.

		*/
		public DiamondShapeInfo<TCell> Shape(int width, int height, Func<DiamondPoint, bool> isInside, DiamondPoint bottomLeftCorner)
		{
			var shapeInfo = MakeShapeStorageInfo<DiamondPoint>(width, height, x=>isInside(x + bottomLeftCorner));
			return new DiamondShapeInfo<TCell>(shapeInfo).Translate(bottomLeftCorner);
		}

		/**
			The same as Shape with all parameters, but with bottomLeft Point set to  DiamondPoint.Zero.
		*/
		public DiamondShapeInfo<TCell> Shape(int width, int height, Func<DiamondPoint, bool> isInside)
		{
			return Shape(width, height, isInside, DiamondPoint.Zero);
		}

		/**
			Creates the grid in a shape that spans 
			the entire storage rectangle of the given width and height.
		*/
		[ShapeMethod]
		public DiamondShapeInfo<TCell> Default(int width, int height)
		{
			var rawInfow = MakeShapeStorageInfo<DiamondPoint>(
				width, 
				height,
				x => DiamondGrid<TCell>.DefaultContains(x, width, height));

			return new DiamondShapeInfo<TCell>(rawInfow);
		}

		/**
			Makes a grid with a single cell that corresponds to the origin.
		*/
		[ShapeMethod]
		public DiamondShapeInfo<TCell> Single()
		{
			var rawInfow = MakeShapeStorageInfo<DiamondPoint>(
				1, 
				1,
				x => x == DiamondPoint.Zero);

			return new DiamondShapeInfo<TCell>(rawInfow);
		}

		/**
			Starts a compound shape operation.

			Any shape that is defined in terms of other shape operations must use this method, and use Endgroup() to end the definition.

				public static DiamondShapeInfo<TCell> MyCustomShape(this DiamondOp<TCell> op)
				{
					return 
						BeginGroup()
							.Shape1()
							.Union()
							.Shape2()
						.EndGroup(op);
				}

			@since 1.1
		*/
		public DiamondOp<TCell> BeginGroup()
		{
			return DiamondGrid<TCell>.BeginShape();
		}
	}
}

#endif
