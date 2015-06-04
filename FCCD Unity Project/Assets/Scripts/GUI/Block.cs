using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace AssemblyCSharp {
	public class Block : MonoBehaviour {
		Button button;
		// Use this for initialization
		void Start () {
			button = GetComponent<Button>();
		}
		
		// Update is called once per frame
		void Update () {
			button.onClick.AddListener (() => { 
				Global.block = !Global.block;
			});
		}
	}
}
