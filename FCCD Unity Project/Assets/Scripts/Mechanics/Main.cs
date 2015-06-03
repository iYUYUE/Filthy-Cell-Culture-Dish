//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using UnityEngine;
using AssemblyCSharp;
using Gamelogic;
using Gamelogic.Grids;
using Gamelogic.Grids.Examples;
using System.Collections.Generic;
/**
		This example shows how to use a diamond grid.
	
		It also shows how to use transforms such as rotation 
		and scale on the map.
	*/
public class Main : GLMonoBehaviour
{
	public SpriteCell cellPrefab;
	public GameObject root;
	
	private DiamondGrid<SpriteCell> grid;
	private IMap3D<DiamondPoint> map;
	private DiamondPoint historyPoint;
	private Color historyColor;
	
	void AddPlayers ()
	{
		for (int i = 0; i<Global.numberOfPlayers; i++) {
			Global.players.Add(new Player(ExampleUtils.Colors[i]));
		}
	}

	void InitializeGlobal ()
	{
		AddPlayers ();
		Global.techCost = new int[5,Global.numTech];
		for (int i =0; i<5; i++) {
			int j = 0;
			Global.techCost[i,j] = 10;
			for (j++;j<Global.numTech;j++){
				Global.techCost[i,j] = (int)((double)Global.techCost[i,j-1]*1.5);
			}
		}
		Global.winPop =(int)((double)(Global.WIDTH*Global.HEIGHT)*0.5);
	}
	
	public void Start()
	{
		Global.binder = new Dictionary<DiamondPoint, Cell>();
		historyPoint = new DiamondPoint (-1, -1);
		InitializeGlobal ();
		BuildGrid();
	}
	
	private void BuildGrid()
	{
		Global.grid =DiamondGrid<SpriteCell>.ThinRectangle(10, 5);
		grid = Global.grid;
		
		map = new DiamondMap(cellPrefab.Dimensions)
			.WithWindow(ExampleUtils.ScreenRect)
				.AlignMiddleCenter(grid)
				.To3DXY();
		int count = 0;
		foreach (DiamondPoint point in grid)
		{
			var cell = Instantiate(cellPrefab);
			Vector3 worldPoint = map[point];

			cell.transform.parent = root.transform;
			cell.transform.localScale = Vector3.one;
			cell.transform.localPosition = worldPoint;
			
			cell.Color = Color.white;
			cell.name = point.ToString();

			grid[point] = cell;

			/////////////////////////////////////
			
			Cell OurCell = new Cell(point);

			Global.binder.Add (point, OurCell);
//			Debug.Log("HAHA");
//		Debug.Log(point);
	//		Debug.Log(OurCell.getPoint().cell);
	//		point.cell = OurCell;
		}

//		foreach (var item in Global.binder)
//			Debug.Log(item.Key);

	}
	public void Update()
	{
		Vector2 worldPosition = GridBuilderUtils.ScreenToWorld(root, Input.mousePosition);
		
		DiamondPoint point = map[worldPosition];
		if (!Global.explored &&Input.GetMouseButtonDown(0)&&grid.Contains (point)){
//			Debug.Log("haha: "+point);
			Cell tempCell;
			Global.binder.TryGetValue (point, out tempCell);
			
			tempCell.explore(Global.players[Global.currentPlayer]);
			Global.explored = true;
		}
//		if (historyPoint != point) {
//			if(grid.Contains (historyPoint))
//				grid [historyPoint].GetComponent<SpriteCell> ().Color = historyColor;
//		
//			if (grid.Contains (point)) {
//				historyPoint = point;
//				historyColor = grid [point].GetComponent<SpriteCell> ().Color;
//				//Toggle the highlight
//				grid [point].GetComponent<SpriteCell> ().Color = LighterColor(grid [point].GetComponent<SpriteCell> ().Color);
//				//			Debug.Log(Global.currentPlayer);
//			} else {
//				historyPoint = new DiamondPoint (-1, -1);
//			}
//		}
	}

	private Color LighterColor(Color origin) {
		float rate = 1.1f;
		float newr = (origin.r * rate > 1.0f) ? 1.0f : origin.r * rate;
		float newg = (origin.g * rate > 1.0f) ? 1.0f : origin.r * rate;
		float newb = (origin.b * rate > 1.0f) ? 1.0f : origin.r * rate;
		return new Color(newr, newg, newb);
	}
}