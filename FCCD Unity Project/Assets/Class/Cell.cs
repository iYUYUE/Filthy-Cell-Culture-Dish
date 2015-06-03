//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using Gamelogic.Grids;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Cell
	{
		private DiamondPoint point;
		private Dictionary<Player ,int> pops;

		public Cell (DiamondPoint DP)
		{
			point = DP;
			point.cell = this;
		}

		public void update(){

		}

		public void explore(Player pl){

		}

		private List<Cell> getNeighbors(){
			List<Cell> ret = new List<Cell> ();

			var neighbors = Global.grid.GetNeighbors(point);
			
			foreach (DiamondPoint neighbor in neighbors)
			{
				if (neighbor == null)
				{
					Debug.LogError("cell null");
				}
				else
				{
					ret.Add(neighbor.cell);
				}
			}

			return ret;
		}

		public int getPop(Player pl) {
			int pop;
			if (this.pops.TryGetValue (pl, out pop))
				return pop;
			else
				return -1;
		}
	}
}
