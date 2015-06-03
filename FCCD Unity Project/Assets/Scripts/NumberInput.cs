using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using UnityEngine.UI;
using AssemblyCSharp;

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
		try
		{
			int playerNumber = Int32.Parse(arg0);
			if(playerNumber > 1) {
				Global.numberOfPlayers = playerNumber;
				GameStartManager.instance.SendMessage("StartLvl", "DiamondTest");
			}
		}
		catch (FormatException e)
		{

		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
