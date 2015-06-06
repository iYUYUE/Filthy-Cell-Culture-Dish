using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace AssemblyCSharp
{
	public class techButtonPressed : MonoBehaviour {
		Button currentBtn;
		public Button techBtn1;
		public Button techBtn2;
		public Button techBtn3;
		public Button techBtn4;
		private int techToResearch = -1;
		// Use this for initialization
		void Start () {
			currentBtn = GetComponent<Button> ();
			switch (currentBtn.GetComponentInChildren<Text>().text) {
			case "Growth": techToResearch = 0; break;
			case "Exploration": techToResearch = 1; break;
			case "Attack": techToResearch = 2; break;
			case "Defense": techToResearch = 3; break;
			case "Ultimate": techToResearch = 4; break;
			}
			currentBtn.onClick.AddListener(() => { 
				currentBtn.image.color = Global.players[Global.currentPlayer].getColor();
				Global.players[Global.currentPlayer].TechOnResearch = techToResearch;
				techBtn1.image.color = Color.white;
				techBtn2.image.color = Color.white;
				techBtn3.image.color = Color.white;
				techBtn4.image.color = Color.white;
				Global.players[Global.currentPlayer].TechSelected = true;
			});
		}
		
		// Update is called once per frame
		void Update () {

		}
	}
}
