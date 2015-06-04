using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class changeBtnColor : MonoBehaviour {
	Button button;
	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
		button.onClick.AddListener(() => { 
			if(button.image.color.Equals(Color.grey)) button.image.color = Color.black;
			else button.image.color = Color.white;
		});
	}
}
