using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace AssemblyCSharp {
	public class SwitchDemo : MonoBehaviour {
		Button demoButton;
		[SerializeField] private Canvas demoCanvas;
		[SerializeField] private Canvas traitCanvas;
		[SerializeField] private Canvas sysCanvas;
		// Use this for initialization
		void Start () {
			demoButton = GetComponent<Button> ();

			demoButton.onClick.AddListener(() => { 
				if (traitCanvas.enabled)
					traitCanvas.enabled = false;
				if (sysCanvas.enabled)
					sysCanvas.enabled = false;
				demoCanvas.enabled = !demoCanvas.enabled; 
			});
		}
		
		// Update is called once per frame
		void Update () {
		}
	}
}