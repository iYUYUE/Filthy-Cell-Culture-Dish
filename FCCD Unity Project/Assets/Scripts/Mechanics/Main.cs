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

	void AddPlayers ()
	{
		for (int i = 0; i<Global.numberOfPlayers; i++) {
			Global.players.Add(new Player(ExampleUtils.Colors[i]));
		}
	}

		public void Start()
		{
<<<<<<< HEAD
		BuildGrid();
		AddPlayers ();
=======
			BuildGrid();
		Global.players.Add (new Player ());
		//Global.players.Add (new Player ());
>>>>>>> origin/master
		}

		private void BuildGrid()
		{
		Global.grid =DiamondGrid<SpriteCell>.ThinRectangle(20, 10);
		grid = Global.grid;

			map = new DiamondMap(cellPrefab.Dimensions)
				.WithWindow(ExampleUtils.ScreenRect)
				.AlignMiddleCenter(grid)
				.To3DXY();

			foreach (DiamondPoint point in grid)
			{
				var cell = Instantiate(cellPrefab);
				Vector3 worldPoint = map[point];

				cell.transform.parent = root.transform;
				cell.transform.localScale = Vector3.one;
				cell.transform.localPosition = worldPoint;

				cell.Color = ExampleUtils.Colors[4];
				cell.name = point.ToString();

				grid[point] = cell;
				
				var OurCell = new Cell(point);
			}
		}

		public void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector2 worldPosition = GridBuilderUtils.ScreenToWorld(root, Input.mousePosition);

				DiamondPoint point = map[worldPosition];

				if (grid.Contains(point))
				{
					//Toggle the highlight
					grid[point].HighlightOn = !grid[point].HighlightOn;
				Debug.Log(Global.players.Count);
				}
			}
		}
	}