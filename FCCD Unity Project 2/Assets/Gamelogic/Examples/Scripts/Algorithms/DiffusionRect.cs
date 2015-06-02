//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Grids;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
	This example gives an example of building a new 
	algorithm on top of the grid components. 
*/
[AddComponentMenu("Gamelogic/Examples/Diffusion (Rect, Editor)")]
public class DiffusionRect : GridBehaviour<RectPoint>
{
	private readonly Color offColor = ExampleUtils.Colors[4];
	private readonly Color onColor = ExampleUtils.Colors[7]; 
	
	private IGrid<float, RectPoint> gas;	

	public void Start()
	{
		gas = Grid.CloneStructure<float>();

		foreach (var point in gas)
		{
			gas[point] = 0;
		}		
	}
		
	float CalculateAverage(RectPoint point, IEnumerable<RectPoint> neighbors)
	{
		float sum = neighbors
			.Select(x => gas[x])
			.Aggregate((p, q) => p + q) + gas[point];
		
		return sum / (neighbors.Count() + 1);
	}
	
	public void Update()
	{
		ProcessInput();
		Algorithms.AggregateNeighborhood(gas, CalculateAverage); //This adds the 
		
		foreach(var point in gas)
		{
			UpdateCell(point);
		}
	}

	private void ProcessInput()
	{
		if (Input.GetMouseButton(0))
		{
			var gridPoint = MousePosition;

			if (Grid.Contains(gridPoint))
			{
				gas[gridPoint] = 2;
			}
		}

		if (Input.GetMouseButton(1))
		{
			var gridPoint = MousePosition;

			if (Grid.Contains(gridPoint))
			{
				gas[gridPoint] = 0;
			}
		}
	}

	private void UpdateCell(RectPoint point)
	{
		Color newColor = ExampleUtils.Blend(gas[point], offColor, onColor);
		Grid[point].Color = newColor;
	}
}
