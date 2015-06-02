//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd         //
//----------------------------------------------//

using Gamelogic.Grids;
using UnityEngine;

[AddComponentMenu("Gamelogic/Examples/PrimsAlgorithmHex")]
public class PrimsAlgorithmRect : GridBehaviour<RectPoint>
{
	override public void InitGrid()
	{
		if (((RectTileGridBuilder)GridBuilder).GridShape != RectTileGridBuilder.Shape.Rectangle)
		{
			Debug.LogError("Shape must be Rectangle");

			return;
		}

		if ((GridBuilder.Dimensions.X % 2 != 1) && (GridBuilder.Dimensions.Y % 2 != 1))
		{
			Debug.LogError("Both dimensions must be even!");

			return;
		}

		foreach (var point in Grid)
		{
			int color = point.GetColor3() == 0 ? 0 : 1;
			Grid[point].Color = ExampleUtils.Colors[color];
		}
		
		foreach (var point in MazeAlgorithms.GenerateMazeWalls((RectGrid<TileCell>) Grid))
		{
			Grid[point].Color = ExampleUtils.Colors[0];
		}
	}
}
