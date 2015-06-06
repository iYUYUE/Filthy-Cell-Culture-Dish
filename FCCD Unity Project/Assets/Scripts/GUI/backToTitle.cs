using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class backToTitle : MonoBehaviour {
	private Button quitBtn;
	// Use this for initialization
	void Start () {
		quitBtn = GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
		quitBtn.onClick.AddListener (() => {
			Application.LoadLevel("Start");
		});
	}
}
