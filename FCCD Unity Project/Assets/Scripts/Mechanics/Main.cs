//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using UnityEngine;
using UnityEngine.UI;
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
	public Camera cam;
	private DiamondGrid<SpriteCell> grid;
	private IMap3D<DiamondPoint> map;
	private DiamondPoint historyPoint;

	public Text cellDetail;
	
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
		for (int i =0; i<4; i++) {
			int j = 0;
			Global.techCost[i,j] = 2*Global.baseCapacity;
			for (j++;j<Global.numTech;j++){
				Global.techCost[i,j] = (int)((double)Global.techCost[i,j-1]*10);
			}
		}
		int jj = 0;
		Global.techCost[4,jj] = 10*Global.baseCapacity;
		for (jj++;jj<Global.numTech;jj++){
			Global.techCost[4,jj] = (int)((double)Global.techCost[4,jj-1]*20);
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

			grid[point] = cell;

			/////////////////////////////////////
			
			Cell OurCell = new Cell(point);

			Global.binder.Add (point, OurCell);
//			Debug.Log(point);
		}

//		foreach (var item in Global.binder)
//			Debug.Log(item.Key);

	}
	
	Thread RunUpdate;
	public void Update()
	{
		if (!EventSystem.current.IsPointerOverGameObject()) {

			if (Global.drawAll) {
				Global.UpdateAllColor ();
				Global.drawAll = false;
				return;
			}

			if (Global.block)
				return;

			Vector2 worldPosition = GridBuilderUtils.ScreenToWorld (root, Input.mousePosition, cam);
			DiamondPoint point = map [worldPosition];

			if (Global.players [Global.currentPlayer].TechSelected && Input.GetMouseButtonDown (0) && grid.Contains (point)) {
				Cell tempCell;
				Global.binder.TryGetValue (point, out tempCell);

				tempCell.explore (Global.players [Global.currentPlayer]);
				Global.update ();
			}

			if (historyPoint != point) {
				if (grid.Contains (historyPoint))
					grid [historyPoint].GetComponent<SpriteCell> ().HighlightOn = false;
			
				if (grid.Contains (point)) {
					historyPoint = point;
					//Toggle the highlight
					grid [point].GetComponent<SpriteCell> ().HighlightOn = true;

					// show the cell detail
					Cell tempCell;
//					string cellInfo = "This Cell:";
					string cellInfo = "";
					Global.binder.TryGetValue (point, out tempCell);

					foreach(Player pl in tempCell.getPlayerList()) {
						if(cellInfo.Length > 10)
							cellInfo += "/";
						cellInfo += " <color=\"" + RGBtoHex(pl.getColor()) + "\">" + tempCell.getPop(pl) + ((tempCell.getPop(pl) >= Formula.GrowthCap(pl)) ? "(FULL)" : "") + "</color> ";
					}
					cellDetail.text = cellInfo;

					//			Debug.Log(Global.currentPlayer);
				} else {
					historyPoint = new DiamondPoint (-1, -1);
				}

			}
		}
	}

	public string RGBtoHex(Color input) {
		Color32 myColor = input;
		return "#" + myColor.r.ToString ("X2") + myColor.g.ToString ("X2") + myColor.b.ToString ("X2");
	}
	
}