using System.Linq;
using Gamelogic;
using Gamelogic.Grids;
using UnityEngine;

public class WrappedHexagonTest : GLMonoBehaviour
{
	public GameObject cellPrefab;

	private WrappedGrid<GameObject, PointyHexPoint> grid;
	private IMap3D<PointyHexPoint> map; 

	public void Start()
	{
		grid = PointyHexGrid<GameObject>.HorizontallyWrappedRectangle(5, 5);
		
		map = new PointyHexMap(cellPrefab.GetComponent<SpriteCell>().Dimensions)
			.WithWindow(new Rect(0, 0, 0, 0))
			.AlignMiddleCenter(grid)
			.To3DXY();

		grid.Fill(MakeCell);
	}

	private GameObject MakeCell(PointyHexPoint point)
	{
		var cell = Instantiate(cellPrefab);

		cell.transform.parent = transform;
		cell.transform.localPosition = map[point];
		ResetCellColor(cell);
		cell.name = point.ToString();

		return cell;
	}

	private static void ResetCellColor(GameObject cell)
	{
		if (cell != null)
		{
			cell.GetComponent<SpriteCell>().Color = ExampleUtils.Colors[4];
		}
	}



	public bool __CompilerHint__Diamond__TileCell()
	{
		var grid = new DiamondGrid<TileCell>(1, 1);

		foreach (var point in grid) { grid[point] = cellPrefab.GetComponent<TileCell>(); }

		var shapeStorageInfo = new ShapeStorageInfo<DiamondPoint>(new IntRect(), p => true);
		var shapeInfo = new DiamondShapeInfo<TileCell>(shapeStorageInfo);

		return grid[grid.First()] != null || shapeInfo.Translate(DiamondPoint.Zero) != null;
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			//grid.Apply(ResetCellColor);

			/*foreach (var point in grid)
			{
				ResetCellColor(grid[point]);
			}*/


			foreach (var cell1 in grid.Values)
			{
				ResetCellColor(cell1);
			}


			var mousePosition = Input.mousePosition;

			var worldPoint = GridBuilderUtils.ScreenToWorld(gameObject, mousePosition);
			var gridPoint = map[worldPoint];
			var wrappedPoint = grid.Wrap(gridPoint);
			var cell = grid[wrappedPoint];

			cell.GetComponent<SpriteCell>().Color = ExampleUtils.Colors[6];

			//if (grid.Contains(gridPoint))
			{
				var neighbors = grid.GetNeighbors(gridPoint);

				foreach (var neighbor in neighbors)
				{
					var neighborCell = grid[neighbor];

					if (neighborCell == null)
					{
						Debug.LogError("cell null");
					}
					else
					{
						neighborCell.GetComponent<SpriteCell>().Color = ExampleUtils.Colors[7];
					}
				}
			}
		}
	}
}
