using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using UnityEngine.UI;
using AssemblyCSharp;

public class NumberInput : MonoBehaviour {
	public InputField PlayerNumber;
	public InputField MaxTurns;
	public InputField MapWidth;
	public InputField MapHeight;
	// Use this for initialization
	void Start () {
		PlayerNumber.Select ();
		var se= new InputField.SubmitEvent();
		se.AddListener(SubmitName);
		PlayerNumber.onEndEdit = se;
		MaxTurns.onEndEdit = se;
		MapWidth.onEndEdit = se;
		MapHeight.onEndEdit = se;
	}

	private void SubmitName(string arg0)
	{
		try
		{
			int playerNumber = Int32.Parse(PlayerNumber.text);
			int maxTurns = Int32.Parse(MaxTurns.text);
			int mapWidth = Int32.Parse(MapWidth.text);
			int mapHeight = Int32.Parse(MapHeight.text);
			if(playerNumber > 1 && maxTurns > 9 && mapWidth > 9 && mapHeight > 9) {
				Global.numberOfPlayers = playerNumber;
				Global.maxTurn = maxTurns;
				Global.WIDTH = mapWidth;
				Global.HEIGHT = mapHeight;
				if(GameStartManager.instance != null && Input.GetKeyDown(KeyCode.Return))
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
