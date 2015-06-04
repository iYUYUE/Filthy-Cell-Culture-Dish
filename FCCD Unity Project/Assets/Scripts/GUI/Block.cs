using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace AssemblyCSharp {
	public class Block : MonoBehaviour {
		public Canvas demoCanvas = null;
		public Canvas traitCanvas = null;
		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			if (demoCanvas.enabled || traitCanvas.enabled)
				Global.block = true;
			else
				Global.block = false;
		}
	}
}
