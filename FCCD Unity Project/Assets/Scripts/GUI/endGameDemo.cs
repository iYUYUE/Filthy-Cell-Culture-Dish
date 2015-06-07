using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace AssemblyCSharp {
	public class endGameDemo : MonoBehaviour {
		[SerializeField] private Text playerList;
		[SerializeField] private Text popList;
		[SerializeField] private Text cellList;
		// Use this for initialization
		void Start () {
			//table title
			playerList.text = "Player\n";
			popList.text = "Population\n";
			cellList.text = "Cells Occupied\n";

			string textToDisplay = "Player Color\tPopulation\tCells Occupied\n";
			
			foreach (Player pl in Global.players) {
				playerList.text += "<color=" + "#" + ColorToHex(pl.getColor()) + ">■■■■■■</color>\n";
				popList.text += pl.getPop().ToString() + "\n";
				cellList.text += pl.getCells().ToString() + "\n";
			}
		}
		
		// Update is called once per frame
		void Update () {
		}

		// Note that Color32 and Color implictly convert to each other. You may pass a Color object to this method without first casting it.
		string ColorToHex(Color32 color)
		{
			string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
			return hex;
		}
	}
}
