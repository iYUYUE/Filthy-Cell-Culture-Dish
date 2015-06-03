using System.Linq;
using Gamelogic.Grids;
using UnityEngine;
using System.Collections;

public class SpiralIteratorTest_Diamond : GridBehaviour<DiamondPoint>
{
	public InspectableVectorPoint inspectableOrigin = new InspectableVectorPoint(DiamondPoint.Zero);
	public int ringCount = 5;
	public Color defaultColor = Color.white;
	public Gradient colors;
	public Color textColor;

	private DiamondGrid<UIImageTextCell> gridCopy;
	private DiamondPoint origin;
	private int celldCount;

	public override void InitGrid()
	{
		origin = inspectableOrigin.GetDiamondPoint();

		gridCopy = (DiamondGrid<UIImageTextCell>) Grid.CastValues<UIImageTextCell, DiamondPoint>();
		celldCount = gridCopy.Count();

		UpdateGrid();
	}

	public void OnLeftClick(DiamondPoint point)
	{
		origin = point;
		UpdateGrid();
	}

	private void UpdateGrid()
	{
		gridCopy.Apply(ResetCells);

		int i = 0;

		foreach (var point in gridCopy.GetSpiralIterator(origin, ringCount))
		{
			gridCopy[point].Text = i.ToString();
			gridCopy[point].Color = colors.Evaluate(i/(float) celldCount);
			i++;
		}
	}

	private void ResetCells(UIImageTextCell cell)
	{
		cell.Color = defaultColor;
		cell.Text = "";
		cell.TextColor = textColor;
	}
}
