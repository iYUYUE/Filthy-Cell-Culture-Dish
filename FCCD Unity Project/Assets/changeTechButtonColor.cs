using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AssemblyCSharp {
	public class changeTechButtonColor : MonoBehaviour {
		public Button growth;
		public Button exp;
		public Button attack;
		public Button def;
		public Button ulti;
		public Button trait;
		public Button hidden;
		Button current;
		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			trait.onClick.AddListener(() => { 
				growth.image.color = Color.white;
				exp.image.color = Color.white;
				attack.image.color = Color.white;
				def.image.color = Color.white;
				ulti.image.color = Color.white;
				trait.image.color = Color.white;
				switch(Global.players[Global.currentPlayer].TechOnResearch) {
				case 0: current = growth; break;
				case 1: current = exp; break;
				case 2: current = attack; break;
				case 3: current = def; break;
				case 4: current = ulti; break;
				default: current = hidden; break;
				}
				current.image.color = Global.players[Global.currentPlayer].getColor();
			});
		}
	}
}
