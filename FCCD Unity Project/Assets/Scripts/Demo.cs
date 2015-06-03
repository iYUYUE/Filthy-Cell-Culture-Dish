using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace AssemblyCSharp {

	public class Demo : MonoBehaviour {
		Text selfPopAmount;
		Text globalLargestPop;
		Text selfCellAmount;
		Text globalLargestCells;
		// Use this for initialization
		void Start () {
			selfPopAmount = GetComponent<Text> ();
			globalLargestPop = GetComponent<Text> ();
			selfCellAmount = GetComponent<Text> ();
			globalLargestCells = GetComponent<Text> ();

		}
		
		// Update is called once per frame
		void Update () {
			selfPopAmount.text = Global.players[Global.currentPlayer].getPop ().ToString ();
			globalLargestPop.text = Global.largestPop.ToString ();
			selfCellAmount.text = Global.players [Global.currentPlayer].getCells ().ToString ();
			globalLargestCells.text = Global.largestTerritory.ToString ();
		}
	}
}