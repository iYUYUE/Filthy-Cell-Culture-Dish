using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchDemo : MonoBehaviour {
	Button demoButton;
	[SerializeField] private Canvas demoCanvas;
	// Use this for initialization
	void Start () {
		demoButton = GetComponent<Button> ();
		demoButton.onClick.AddListener(() => { demoCanvas.enabled = !demoCanvas.enabled; });
	}
	
	// Update is called once per frame
	void Update () {

	}
}
