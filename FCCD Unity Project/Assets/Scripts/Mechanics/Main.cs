//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using AssemblyCSharp;
using Gamelogic;
using Gamelogic.Grids;
using Gamelogic.Grids.Examples;
using System.Collections.Generic;

using System.Threading;
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
		Global.winPop =(int)((double)(Global.WIDTH*Global.HEIGHT*Global.baseCapacity)*0.5);
	}
	
	public void Start()
	{
		Global.binder = new Dictionary<DiamondPoint, Cell>();
		
		Global.oldbinder = new Dictionary<DiamondPoint, Cell> ();
		historyPoint = new DiamondPoint (-1, -1);
		InitializeGlobal ();
		BuildGrid();
	}
	
	private void BuildGrid()
	{
		Global.grid =DiamondGrid<SpriteCell>.ThinRectangle(Global.WIDTH, Global.HEIGHT);
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

//			cell.HighlightOn = true;

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
	
	Thread RunUpdate;
	public void Update()
	{
		if (!EventSystem.current.IsPointerOverGameObject ()) {
			if (Global.drawAll) {
				Global.UpdateAllColor ();
				Global.drawAll = false;
				return;
			}
			if (Global.block)
				return;
			Vector2 worldPosition = GridBuilderUtils.ScreenToWorld (root, Input.mousePosition);
			DiamondPoint point = map [worldPosition];
			if (Input.GetMouseButtonDown (0) && grid.Contains (point)) {
				Cell tempCell;
				Global.binder.TryGetValue (point, out tempCell);

				tempCell.explore (Global.players [Global.currentPlayer]);
				SceneView.RepaintAll ();
				Global.update ();
			}
			if (historyPoint != point) {
				Debug.Log (Global.players [Global.numberOfPlayers - 1].getPop ());
				if (grid.Contains (historyPoint))
					grid [historyPoint].GetComponent<SpriteCell> ().HighlightOn = false;
				if (grid.Contains (point)) {
					historyPoint = point;
					//Toggle the highlight
					grid [point].GetComponent<SpriteCell> ().HighlightOn = true;
					//Debug.Log(Global.currentPlayer);
				} else {
					historyPoint = new DiamondPoint (-1, -1);
				}
			}
		}
	}
	
}