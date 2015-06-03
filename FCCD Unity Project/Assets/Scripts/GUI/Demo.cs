using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace AssemblyCSharp {

	public class Demo : MonoBehaviour {
		Text selfPopAmount;
		Text globalLargestPop;
		Text selfCellAmount;
		Text globalLargestCells;
		Text growthValue;
		Text expoValue;
		Text attackValue;
		Text defenseValue;
		Text ultimateValue;
		// Use this for initialization
		void Start () {
			selfPopAmount = GetComponent<Text> ();
			globalLargestPop = GetComponent<Text> ();
			selfCellAmount = GetComponent<Text> ();
			globalLargestCells = GetComponent<Text> ();
			globalLargestCells = GetComponent<Text> ();
			growthValue = GetComponent<Text> ();
			expoValue = GetComponent<Text> ();
			attackValue = GetComponent<Text> ();
			defenseValue = GetComponent<Text> ();
			ultimateValue = GetComponent<Text> ();

		}
		
		// Update is called once per frame
		void Update () {
			selfPopAmount.text = Global.players[Global.currentPlayer].getPop ().ToString ();
			globalLargestPop.text = Global.largestPop.ToString ();
			selfCellAmount.text = Global.players [Global.currentPlayer].getCells ().ToString ();
			globalLargestCells.text = Global.largestTerritory.ToString ();
			growthValue.text = Global.players [Global.currentPlayer].getGrowthValue ().ToString ();
			expoValue.text = Global.players [Global.currentPlayer].getExplorationValue ().ToString ();
			attackValue.text = Global.players [Global.currentPlayer].getAttackValue ().ToString ();
			defenseValue.text = Global.players [Global.currentPlayer].getDefenseValue ().ToString ();
			ultimateValue.text = Global.players [Global.currentPlayer].getUltimateValue ().ToString ();
		}
	}
}