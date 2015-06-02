//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic;
using Gamelogic.Grids;
using System.Linq;
using UnityEngine;

public class LightsOutTri: GridBehaviour<FlatTriPoint>
{
	private Color offColor;
	private Color onColor;

	public void OnGUI()
	{
		if (GUILayout.Button("Reset"))
		{
			Reset();
		}
	}

	public override void InitGrid()
	{
		if (GridBuilder.Colors.Length >= 2)
		{
			onColor = GridBuilder.Colors[0];
			offColor = GridBuilder.Colors[1];
		}
		else
		{
			onColor = Color.white;
			offColor = Color.black;
		}

		Reset();
	}

	public void Reset()
	{
		foreach (var point in Grid)
		{
			((SpriteCell)Grid[point]).HighlightOn = false;
			Grid[point].Color = offColor;
		}

		InitGame();
	}

	public void InitGame()
	{
		//Initialize with random pattern
		Grid.SampleRandom(9).ToList().ForEach(ToggleCellAt);		
	}

	public void OnClick(FlatTriPoint point)
	{
		ToggleCellAt(point);
	}

	private void ToggleCellAt(FlatTriPoint centerPoint)
	{
		foreach (var point in Grid.GetNeighbors(centerPoint))
		{
			var cell = (SpriteCell)Grid[point];

			cell.HighlightOn = !cell.HighlightOn;
			cell.Color = cell.HighlightOn ? onColor : offColor;
		}
	}
}