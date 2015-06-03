//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using UnityEngine;
using System;
namespace AssemblyCSharp {
	public class Player {
		private int netPop;
		private int numCell;
		private bool[] isPeace;
		//whether tech get Upgrade last turn
		private Color color;
		private bool techUp;
		//techAndProgree[Global.Techs.Exploration][0] the level techAndProgree[Global.Techs.Exploration][1] progress
		private int[,] techAndProgress;
		private int techOnResearch;
		public Player (Color color)
		{
			this.color = color;
			isPeace = new bool[Global.players.Count];
			techAndProgress = new int[5,2];
			techUp = false;
			netPop = 0;
			numCell = 0;
		}

		public void update(){
			//update number of pops and cells for the current turn
			netPop = 0;
			numCell = 0;
			foreach (Gamelogic.Grids.DiamondPoint point in Global.grid) {
				Cell cell = point.cell;
				int tmpPop = cell.getPop(this);
				if(tmpPop != 0) numCell++;
				netPop += tmpPop;
			}
			UnityEngine.Debug.Log ("Player " + Global.players.IndexOf(this) + "'s pop = " 
			                       + netPop + "\tnumCells = " + numCell);

			//update teches
			techAndProgress [techOnResearch, 1] += netPop;
			//tech done
			if (techAndProgress [techOnResearch, 1] >= 
				Global.techCost [techOnResearch, techAndProgress [techOnResearch, 0]]) {
				techAndProgress[techOnResearch,0]++;
				techAndProgress[techOnResearch,1] = 0;
				techUp = true;
			}
		}
		public Color getColor(){
			return color;
		}
		public bool researhDone() {
			return techUp;
		}

		public void setTechUp(bool tech){
			techUp = tech;
		}

		public bool isPeaceWith(Player pl) {
			return isPeace [Global.players.IndexOf (pl)];
		}

		public int getGrowthValue() {
			return techAndProgress [Global.GROWTH, 0];
		}

		public int getExplorationValue() {
			return techAndProgress [Global.EXPLORATION, 0];
		}

		public int getAttackValue() {
			return techAndProgress [Global.ATTACKING, 0];
		}

		public int getDefenseValue() {
			return techAndProgress [Global.DEFENSING, 0];
		}
		
		public int getUltimateValue() {
			return techAndProgress [Global.ULTIMATE, 0];
		}

		public int getPop() {
			return netPop;
		}

		public int getCells() {
			return numCell;
		}
	}
}

