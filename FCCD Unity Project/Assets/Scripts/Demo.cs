using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace AssemblyCSharp {

	public class Demo : MonoBehaviour {
		Text selfPopAmount;
		Text globalLargestPop;
		// Use this for initialization
		void Start () {
			selfPopAmount = GetComponent<Text> ();
			globalLargestPop = GetComponent<Text> ();
		}
		
		// Update is called once per frame
		void Update () {
			//selfPopAmount.text = Global.players[Global.currentPlayer].getPop().ToString();
			globalLargestPop.text = Global.largestPop.ToString();
		}
	}
}