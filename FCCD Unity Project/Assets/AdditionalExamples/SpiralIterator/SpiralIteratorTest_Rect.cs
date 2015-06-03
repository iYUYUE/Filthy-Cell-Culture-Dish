using System.Linq;
using Gamelogic.Grids;
using UnityEngine;
using System.Collections;

public class SpiralIteratorTest_Rect : GridBehaviour<RectPoint>
{
	public InspectableVectorPoint inspectableOrigin = new InspectableVectorPoint(RectPoint.Zero);
	public int ringCount = 5;
	public Color defaultColor = Color.white;

	public Gradient colors;
	public Color textColor;

	private RectGrid<UIImageTextCell> gridCopy;
	private RectPoint origin;
	private int celldCount;

	public override void InitGrid()
	{
		origin = inspectableOrigin.GetRectPoint();

		gridCopy = (RectGrid<UIImageTextCell>)Grid.CastValues<UIImageTextCell, RectPoint>();
		celldCount = gridCopy.Count();

		UpdateGrid();
	}

	public void OnLeftClick(RectPoint point)
	{
		origin = point;
		UpdateGrid();
	}

	private void UpdateGrid()
	{
		gridCopy.Apply(ResetCells);

		int i = 0;

		if (gridCopy.Contains(origin))
		{
			foreach (var point in gridCopy.GetSpiralIterator(origin, ringCount))
			{
				gridCopy[point].Text = i.ToString();
				gridCopy[point].Color = colors.Evaluate(i / (float)celldCount);
				i++;
			}
		}
	}

	private void ResetCells(UIImageTextCell cell)
	{
		cell.Color = defaultColor;
		cell.Text = "";
		cell.TextColor = textColor;
	}
}
