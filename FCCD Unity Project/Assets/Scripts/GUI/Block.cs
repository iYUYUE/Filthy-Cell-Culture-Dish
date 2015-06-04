using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace AssemblyCSharp {
	public class Block : MonoBehaviour {
		public Canvas demoCanvas;
		public Canvas traitCanvas;
		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			if (demoCanvas.isActiveAndEnabled || traitCanvas.isActiveAndEnabled)
				Global.block = true;
			else
				Global.block = false;
		}
	}
}
