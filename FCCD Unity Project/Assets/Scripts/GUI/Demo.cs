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
			growthLevel.text = growthInt < Global.numTech? growthInt.ToString () + "/" + Global.numTech : "MAX";

			int expoInt = Global.players [Global.currentPlayer].TechAndProgress [1, 0];
			expoLevel.text = expoInt < Global.numTech? expoInt.ToString () + "/" + Global.numTech : "MAX";

			int attInt = Global.players [Global.currentPlayer].TechAndProgress [2, 0];
			attackLevel.text = attInt < Global.numTech? attInt.ToString () + "/" + Global.numTech : "MAX";

			int defInt = Global.players [Global.currentPlayer].TechAndProgress [3, 0];
			defenseLevel.text = defInt < Global.numTech? defInt.ToString () + "/" + Global.numTech : "MAX";

			int ultInt = Global.players [Global.currentPlayer].TechAndProgress [4, 0];
			ultimateLevel.text = ultInt < Global.numTech? ultInt.ToString () + "/" + Global.numTech : "MAX";

			if (!growthLevel.text.Equals ("MAX"))
				growthProc.text = Global.players [Global.currentPlayer].getGrowthValueForDisplay ();
			else
				growthProc.text = "";
			if (!expoLevel.text.Equals ("MAX"))
				expoProc.text = Global.players [Global.currentPlayer].getExplorationValueForDisplay ();
			else
				expoProc.text = "";
			if (!attackLevel.text.Equals ("MAX"))
				attackProc.text = Global.players [Global.currentPlayer].getAttackValueForDisplay ();
			else
				attackProc.text = "";
			if (!defenseLevel.text.Equals ("MAX"))
				defenseProc.text = Global.players [Global.currentPlayer].getDefenseValueForDisplay ();
			else
				defenseProc.text = "";
			if(!ultimateLevel.text.Equals("MAX"))
				ultimateProc.text = Global.players [Global.currentPlayer].getUltimateValueForDisplay ();
		}
	}
}