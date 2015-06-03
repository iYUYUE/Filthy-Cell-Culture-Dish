using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using UnityEngine.UI;

public class NumberInput : MonoBehaviour {
	public InputField numberInput;
	// Use this for initialization
	void Start () {
		numberInput.Select ();
		var se= new InputField.SubmitEvent();
		se.AddListener(SubmitName);
		numberInput.onEndEdit = se;

	}

	private void SubmitName(string arg0)
	{
		Debug.Log("User Number: "+arg0);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
