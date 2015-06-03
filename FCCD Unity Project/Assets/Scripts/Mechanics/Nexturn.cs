﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AssemblyCSharp;
public class Nexturn : MonoBehaviour {

	[SerializeField] private Button MyButton = null; // assign in the editor
	
	void Start() { MyButton.onClick.AddListener(() => { nextTurn();  });
	}


	public void nextTurn(){
		if (!Global.explored||Global.players[Global.currentPlayer].researhDone()) 
			return;
		Global.currentPlayer = (Global.currentPlayer + 1)% Global.numberOfPlayers;
		if (Global.currentPlayer == 0) {
			Global.numTurns += 1;
			Global.updateDemo();
			Global.updateWinStatus();
		}

		Global.explored = false;
		Debug.Log(Global.numberOfPlayers);

	}
}
