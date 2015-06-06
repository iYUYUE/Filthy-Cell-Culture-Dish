using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace AssemblyCSharp
{
	public class changeBtnColor : MonoBehaviour {
		Button button;
		public Button growth;
		public Button explore;
		public Button attack;
		public Button defense;
		public Button ultimate;
		// Use this for initialization
		void Start () {
			button = GetComponent<Button> ();
			button.onClick.AddListener(() => { 
				//clear existing color
				growth.image.color = Color.white;
				explore.image.color = Color.white;
				attack.image.color = Color.white;
				defense.image.color = Color.white;
				ultimate.image.color = Color.white;

				//disable buttons that player reaches maximum level
//				Debug.Log(Global.players [Global.currentPlayer].TechAndProgress [0, 0] + "\t" + Global.numTech);
				growth.interactable = 
					(Global.players [Global.currentPlayer].TechAndProgress [0, 0] < Global.numTech) ?
						true : false;
				explore.interactable = 
					(Global.players [Global.currentPlayer].TechAndProgress [1, 0] < Global.numTech) ?
						true : false;
				attack.interactable = 
					(Global.players [Global.currentPlayer].TechAndProgress [2, 0] < Global.numTech) ?
						true : false;
				defense.interactable = 
					(Global.players [Global.currentPlayer].TechAndProgress [3, 0] < Global.numTech) ?
						true : false;
				ultimate.interactable = 
					(Global.players [Global.currentPlayer].TechAndProgress [4, 0] < Global.numTech) ?
						true : false;

				
				//get current tech and set its color
				switch (Global.players[Global.currentPlayer].TechOnResearch) {
				case 0:growth.image.color = Global.players[Global.currentPlayer].getColor(); break;
				case 1:explore.image.color = Global.players[Global.currentPlayer].getColor(); break;
				case 2:attack.image.color = Global.players[Global.currentPlayer].getColor(); break;
				case 3:defense.image.color = Global.players[Global.currentPlayer].getColor(); break;
				case 4:ultimate.image.color = Global.players[Global.currentPlayer].getColor(); break;
				default:break;
				}
			});
		}
		
		// Update is called once per frame
		void Update () {

		}
	}
}
