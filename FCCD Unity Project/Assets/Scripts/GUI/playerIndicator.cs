using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace AssemblyCSharp
{

	public class playerIndicator : MonoBehaviour {
		public Button nextTurn;
		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
			nextTurn.image.color = Global.players [Global.currentPlayer].getColor ();
		}
	}

}