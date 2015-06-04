using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace AssemblyCSharp {

	public class Demo : MonoBehaviour {
		public Text selfPopAmount;
		public Text globalLargestPop;
		public Text selfCellAmount;
		public Text globalLargestCells;
		public Text growthValue;
		public Text expoValue;
		public Text attackValue;
		public Text defenseValue;
		public Text ultimateValue;
		public Text turnCounter;
		public Text growthLevel;
		public Text expoLevel;
		public Text attackLevel;
		public Text defenseLevel;
		public Text ultimateLevel;
		// Use this for initialization
		void Start () {

		}
		
		// Update is called once per frame
		void Update () {
			globalLargestPop.text = Global.largestPop.ToString ();
			globalLargestCells.text = Global.largestTerritory.ToString ();
			turnCounter.text = "Turn " + Global.numTurns.ToString ();
			selfPopAmount.text = Global.players[Global.currentPlayer].getPop ().ToString ();
			selfCellAmount.text = Global.players [Global.currentPlayer].getCells ().ToString ();
			growthValue.text = Global.players [Global.currentPlayer].getGrowthValue ().ToString ();
			expoValue.text = Global.players [Global.currentPlayer].getExplorationValue ().ToString ();
			attackValue.text = Global.players [Global.currentPlayer].getAttackValue ().ToString ();
			defenseValue.text = Global.players [Global.currentPlayer].getDefenseValue ().ToString ();
			ultimateValue.text = Global.players [Global.currentPlayer].getUltimateValue ().ToString ();
			growthLevel.text = Global.players [Global.currentPlayer].getGrowthLevel ();
			expoLevel.text = Global.players [Global.currentPlayer].getExplorationLevel ();
			attackLevel.text = Global.players [Global.currentPlayer].getAttackLevel ();
			defenseLevel.text = Global.players [Global.currentPlayer].getDefenseLevel ();
			ultimateLevel.text = Global.players [Global.currentPlayer].getUltimateLevel ();


		}
	}
}