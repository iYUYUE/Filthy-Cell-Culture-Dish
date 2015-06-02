//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using UnityEngine;

[AddComponentMenu("Gamelogic/Cells/MazeCell")]
public class MazeCell : SpriteCell
{
	public void SetOrientation(int index, bool open)
	{
		FrameIndex = (index + (open ? 3 : 0));
	}
}