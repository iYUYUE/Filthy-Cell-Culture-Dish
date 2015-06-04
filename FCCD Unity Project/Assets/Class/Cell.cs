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
using System.Linq;
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
//			point.cell = this;
//			Debug.Log(point.cell);
			pops = new Dictionary<Player ,int>();
		}

		public void update(){
			// Search Players Nearby
			List<Player> PlayerNearBy = new List<Player> ();
			foreach (Cell neighbor in this.getNeighbors()) {
				foreach (Player player in neighbor.getPlayerList())
					if(((double)neighbor.getPop(player) / (double)Formula.GrowthCap(player.getGrowthValue())) > 
					   Formula.spreadThreshold(player.getExplorationValue()))
						PlayerNearBy.Add(player);
			}

			var GroupedNewPlayer = PlayerNearBy.GroupBy(s => s).Select(group => new { Player = group.Key, Count = group.Count() });
			// Add New Players
			foreach (var player in GroupedNewPlayer) {
				if(!this.getPlayerList().Contains(player.Player))
					pops.Add (player.Player, this.growthChecker(player.Count * Global.baseCapacity / 10, player.Player));
				else
					pops[player.Player] += this.growthChecker(player.Count * Global.baseCapacity / 10, player.Player);

			
			}

			// Update Population
			foreach (var pop in pops)
			{
				this.explore(pop.Key);
				foreach (var popX in pops){
					if(pop.Key.isPeaceWith(popX.Key))
						pops[pop.Key] += this.growthChecker(PopDance(pop.Key, popX.Key, pops[pop.Key], pops[popX.Key]), pop.Key);
				}
				// race distinction
				if(pops[pop.Key] <= 0)
					pops.Remove(pop.Key);
			}

			UpdateColor ();
		}

		public void UpdateColor() {
			List<Color> colorList = new List<Color> ();
			// Update Color
			foreach (Player player in this.getPlayerList())
				colorList.Add (Formula.ColorLighter(player.getColor(), ((float)this.getPop (player)/(float)Formula.GrowthCap (player.getGrowthValue ()))));
			foreach (Color item in colorList)
				Debug.Log (item);
			//			Debug.Log ("ha: "+Formula.ColorMixer(colorList));
			Global.grid [point].GetComponent<SpriteCell> ().Color = Formula.ColorMixer(colorList);
		}

		public int PopDance(Player Player1, Player PlayerX, int Pop1, int PopX) {
			return Formula.LosePop(Player1, PlayerX, Pop1, PopX) + 
				Formula.GainPop(Player1, PlayerX, Pop1, PopX);
		}

		public void explore(Player pl){
			if (Global.explored)
				return;
			Global.explored = false;
			if (!this.getPlayerList ().Contains (pl))
				pops.Add(pl, Global.baseCapacity / 10);
			else
				pops[pl] += this.growthChecker((int) (Formula.GrowthRate(pl.getGrowthValue()) * (double) ((Formula.GrowthCap(pl.getGrowthValue()) - pops[pl]) * pops[pl])), pl);
			UpdateColor ();
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
					Cell tempCell;
					Global.binder.TryGetValue (neighbor, out tempCell);
					ret.Add(tempCell);
				}
			}

			return ret;
		}

		private int growthChecker(int growth, Player pl) {
			if ((this.getPop (pl) + growth) <= Formula.GrowthCap (pl.getGrowthValue ()))
				return growth;
			else
				return Formula.GrowthCap (pl.getGrowthValue ()) - this.getPop (pl);
		}

		public List<Player> getPlayerList() {
			List<Player> ret = new List<Player> ();
			foreach (var pop in pops)
				ret.Add (pop.Key);
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

