using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace AssemblyCSharp {

	public class Demo : MonoBehaviour {
		public Text selfPopAmount;
		public Text globalLargestPop;
		public Text selfCellAmount;
		public Text globalLargestCells;

		public Text turnCounter;
		public Text popIndicator;
		public Text cellIndicator;

		public Text growthLevel;
		public Text expoLevel;
		public Text attackLevel;
		public Text defenseLevel;
		public Text ultimateLevel;

		public Text growthProc;
		public Text expoProc;
		public Text attackProc;
		public Text defenseProc;
		public Text ultimateProc;
		// Use this for initialization
		void Start () {

		}
		
		// Update is called once per frame
		void Update () {
			//In Game GUI
			popIndicator.text = "Population: " + Global.players[Global.currentPlayer].getPop ().ToString ();
			cellIndicator.text = "Cells: " + Global.players [Global.currentPlayer].getCells ().ToString ();

			//Demographics

			if(!(Global.largestPopPlayer == null)){
				globalLargestPop.color = Global.largestPopPlayer.getColor ();}
			if(!(Global.largestCellPlayer == null)){
				globalLargestCells.color = Global.largestCellPlayer.getColor ();}

			globalLargestPop.text = Global.largestPop.ToString ();
			globalLargestCells.text = Global.largestTerritory.ToString () + "/" + Global.binder.Count.ToString ();
			turnCounter.text = "Turn " + Global.numTurns.ToString () + "/" + Global.maxTurn;
			selfPopAmount.text = Global.players[Global.currentPlayer].getPop ().ToString ();
			selfCellAmount.text = Global.players [Global.currentPlayer].getCells ().ToString () + "/" + Global.binder.Count.ToString ();

			//Traits
			int growthInt = Global.players [Global.currentPlayer].TechAndProgress [0, 0];
			growthLevel.text = growthInt < Global.numTech? growthInt.ToString () : "MAX";

			int expoInt = Global.players [Global.currentPlayer].TechAndProgress [1, 0];
			expoLevel.text = expoInt < Global.numTech? expoInt.ToString () : "MAX";

			int attInt = Global.players [Global.currentPlayer].TechAndProgress [2, 0];
			attackLevel.text = attInt < Global.numTech? attInt.ToString () : "MAX";

			int defInt = Global.players [Global.currentPlayer].TechAndProgress [3, 0];
			defenseLevel.text = defInt < Global.numTech? defInt.ToString () : "MAX";

			int ultInt = Global.players [Global.currentPlayer].TechAndProgress [4, 0];
			ultimateLevel.text = ultInt < Global.numTech? ultInt.ToString () : "MAX";

			growthProc.text = Global.players [Global.currentPlayer].getGrowthValueForDisplay ();
			expoProc.text = Global.players [Global.currentPlayer].getExplorationValueForDisplay ();
			attackProc.text = Global.players [Global.currentPlayer].getAttackValueForDisplay ();
			defenseProc.text = Global.players [Global.currentPlayer].getDefenseValueForDisplay ();
			ultimateProc.text = Global.players [Global.currentPlayer].getUltimateValueForDisplay ();
		}
	}
}