using UnityEngine;
using System.Collections;

public class EscListener : MonoBehaviour {
	[SerializeField] private Canvas demoCanvas;
	[SerializeField] private Canvas traitCanvas;
	[SerializeField] private Canvas sysCanvas;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//ESC Listener
		if (Input.GetKeyDown (KeyCode.Escape)) {
			demoCanvas.enabled = false;
			traitCanvas.enabled = false;
			sysCanvas.enabled = !sysCanvas.enabled;
		}
	}
}
