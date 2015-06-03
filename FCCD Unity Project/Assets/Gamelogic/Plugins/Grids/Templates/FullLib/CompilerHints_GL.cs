#if !IgnoreRectLib
//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

// Auto-generated File

using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids
{
	/**
		Compiler hints for our examples.

		@since 1.8
	*/
	public static class __CompilerHintsGL
	{
		public static bool __CompilerHint__Rect__TileCell()
		{
			return __CompilerHint1__Rect__TileCell() && __CompilerHint2__Rect__TileCell();
		}

		public static bool __CompilerHint1__Rect__TileCell()
		{
			var grid = new RectGrid<TileCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new TileCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<RectPoint>(new IntRect(), p => true);
			var shapeInfo = new RectShapeInfo<TileCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(RectPoint.Zero) != null;
		}

		public static bool __CompilerHint2__Rect__TileCell()
		{
			//Ensures abstract super classes for base grids gets created
			var grid = new RectGrid<TileCell>(1, 1, p => p == RectPoint.Zero, x => x, x => x, new List<RectPoint>());

			//Ensures shape infpo classes get created
			var shapeStorageInfo = new ShapeStorageInfo<RectPoint>(new IntRect(), p => true);
			var shapeInfo = new RectShapeInfo<TileCell>(shapeStorageInfo);

			return grid[grid.First()] == null || shapeInfo.Translate(RectPoint.Zero) != null;
		}
		public static bool __CompilerHint__Diamond__TileCell()
		{
			return __CompilerHint1__Diamond__TileCell() && __CompilerHint2__Diamond__TileCell();
		}

		public static bool __CompilerHint1__Diamond__TileCell()
		{
			var grid = new DiamondGrid<TileCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new TileCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<DiamondPoint>(new IntRect(), p => true);
			var shapeInfo = new DiamondShapeInfo<TileCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(DiamondPoint.Zero) != null;
		}

		public static bool __CompilerHint2__Diamond__TileCell()
		{
			//Ensures abstract super classes for base grids gets created
			var grid = new DiamondGrid<TileCell>(1, 1, p => p == DiamondPoint.Zero, x => x, x => x, new List<DiamondPoint>());

			//Ensures shape infpo classes get created
			var shapeStorageInfo = new ShapeStorageInfo<DiamondPoint>(new IntRect(), p => true);
			var shapeInfo = new DiamondShapeInfo<TileCell>(shapeStorageInfo);

			return grid[grid.First()] == null || shapeInfo.Translate(DiamondPoint.Zero) != null;
		}
		public static bool __CompilerHint__Rect__SpriteCell()
		{
			return __CompilerHint1__Rect__SpriteCell() && __CompilerHint2__Rect__SpriteCell();
		}

		public static bool __CompilerHint1__Rect__SpriteCell()
		{
			var grid = new RectGrid<SpriteCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new SpriteCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<RectPoint>(new IntRect(), p => true);
			var shapeInfo = new RectShapeInfo<SpriteCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(RectPoint.Zero) != null;
		}

		public static bool __CompilerHint2__Rect__SpriteCell()
		{
			//Ensures abstract super classes for base grids gets created
			var grid = new RectGrid<SpriteCell>(1, 1, p => p == RectPoint.Zero, x => x, x => x, new List<RectPoint>());

			//Ensures shape infpo classes get created
			var shapeStorageInfo = new ShapeStorageInfo<RectPoint>(new IntRect(), p => true);
			var shapeInfo = new RectShapeInfo<SpriteCell>(shapeStorageInfo);

			return grid[grid.First()] == null || shapeInfo.Translate(RectPoint.Zero) != null;
		}
		public static bool __CompilerHint__Diamond__SpriteCell()
		{
			return __CompilerHint1__Diamond__SpriteCell() && __CompilerHint2__Diamond__SpriteCell();
		}

		public static bool __CompilerHint1__Diamond__SpriteCell()
		{
			var grid = new DiamondGrid<SpriteCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new SpriteCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<DiamondPoint>(new IntRect(), p => true);
			var shapeInfo = new DiamondShapeInfo<SpriteCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(DiamondPoint.Zero) != null;
		}

		public static bool __CompilerHint2__Diamond__SpriteCell()
		{
			//Ensures abstract super classes for base grids gets created
			var grid = new DiamondGrid<SpriteCell>(1, 1, p => p == DiamondPoint.Zero, x => x, x => x, new List<DiamondPoint>());

			//Ensures shape infpo classes get created
			var shapeStorageInfo = new ShapeStorageInfo<DiamondPoint>(new IntRect(), p => true);
			var shapeInfo = new DiamondShapeInfo<SpriteCell>(shapeStorageInfo);

			return grid[grid.First()] == null || shapeInfo.Translate(DiamondPoint.Zero) != null;
		}
		public static bool __CompilerHint__Rect__UVCell()
		{
			return __CompilerHint1__Rect__UVCell() && __CompilerHint2__Rect__UVCell();
		}

		public static bool __CompilerHint1__Rect__UVCell()
		{
			var grid = new RectGrid<UVCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new UVCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<RectPoint>(new IntRect(), p => true);
			var shapeInfo = new RectShapeInfo<UVCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(RectPoint.Zero) != null;
		}

		public static bool __CompilerHint2__Rect__UVCell()
		{
			//Ensures abstract super classes for base grids gets created
			var grid = new RectGrid<UVCell>(1, 1, p => p == RectPoint.Zero, x => x, x => x, new List<RectPoint>());

			//Ensures shape infpo classes get created
			var shapeStorageInfo = new ShapeStorageInfo<RectPoint>(new IntRect(), p => true);
			var shapeInfo = new RectShapeInfo<UVCell>(shapeStorageInfo);

			return grid[grid.First()] == null || shapeInfo.Translate(RectPoint.Zero) != null;
		}
		public static bool __CompilerHint__Diamond__UVCell()
		{
			return __CompilerHint1__Diamond__UVCell() && __CompilerHint2__Diamond__UVCell();
		}

		public static bool __CompilerHint1__Diamond__UVCell()
		{
			var grid = new DiamondGrid<UVCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new UVCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<DiamondPoint>(new IntRect(), p => true);
			var shapeInfo = new DiamondShapeInfo<UVCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(DiamondPoint.Zero) != null;
		}

		public static bool __CompilerHint2__Diamond__UVCell()
		{
			//Ensures abstract super classes for base grids gets created
			var grid = new DiamondGrid<UVCell>(1, 1, p => p == DiamondPoint.Zero, x => x, x => x, new List<DiamondPoint>());

			//Ensures shape infpo classes get created
			var shapeStorageInfo = new ShapeStorageInfo<DiamondPoint>(new IntRect(), p => true);
			var shapeInfo = new DiamondShapeInfo<UVCell>(shapeStorageInfo);

			return grid[grid.First()] == null || shapeInfo.Translate(DiamondPoint.Zero) != null;
		}
		public static bool __CompilerHint__Rect__MeshTileCell()
		{
			return __CompilerHint1__Rect__MeshTileCell() && __CompilerHint2__Rect__MeshTileCell();
		}

		public static bool __CompilerHint1__Rect__MeshTileCell()
		{
			var grid = new RectGrid<MeshTileCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new MeshTileCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<RectPoint>(new IntRect(), p => true);
			var shapeInfo = new RectShapeInfo<MeshTileCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(RectPoint.Zero) != null;
		}

		public static bool __CompilerHint2__Rect__MeshTileCell()
		{
			//Ensures abstract super classes for base grids gets created
			var grid = new RectGrid<MeshTileCell>(1, 1, p => p == RectPoint.Zero, x => x, x => x, new List<RectPoint>());

			//Ensures shape infpo classes get created
			var shapeStorageInfo = new ShapeStorageInfo<RectPoint>(new IntRect(), p => true);
			var shapeInfo = new RectShapeInfo<MeshTileCell>(shapeStorageInfo);

			return grid[grid.First()] == null || shapeInfo.Translate(RectPoint.Zero) != null;
		}
		public static bool __CompilerHint__Diamond__MeshTileCell()
		{
			return __CompilerHint1__Diamond__MeshTileCell() && __CompilerHint2__Diamond__MeshTileCell();
		}

		public static bool __CompilerHint1__Diamond__MeshTileCell()
		{
			var grid = new DiamondGrid<MeshTileCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new MeshTileCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<DiamondPoint>(new IntRect(), p => true);
			var shapeInfo = new DiamondShapeInfo<MeshTileCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(DiamondPoint.Zero) != null;
		}

		public static bool __CompilerHint2__Diamond__MeshTileCell()
		{
			//Ensures abstract super classes for base grids gets created
			var grid = new DiamondGrid<MeshTileCell>(1, 1, p => p == DiamondPoint.Zero, x => x, x => x, new List<DiamondPoint>());

			//Ensures shape infpo classes get created
			var shapeStorageInfo = new ShapeStorageInfo<DiamondPoint>(new IntRect(), p => true);
			var shapeInfo = new DiamondShapeInfo<MeshTileCell>(shapeStorageInfo);

			return grid[grid.First()] == null || shapeInfo.Translate(DiamondPoint.Zero) != null;
		}
		public static bool CallAll__()
		{
			if(!__CompilerHint__Rect__TileCell()) return false;
			if(!__CompilerHint__Diamond__TileCell()) return false;
			if(!__CompilerHint__Rect__SpriteCell()) return false;
			if(!__CompilerHint__Diamond__SpriteCell()) return false;
			if(!__CompilerHint__Rect__UVCell()) return false;
			if(!__CompilerHint__Diamond__UVCell()) return false;
			if(!__CompilerHint__Rect__MeshTileCell()) return false;
			if(!__CompilerHint__Diamond__MeshTileCell()) return false;
			return true;

		}
	}
}
#endif
