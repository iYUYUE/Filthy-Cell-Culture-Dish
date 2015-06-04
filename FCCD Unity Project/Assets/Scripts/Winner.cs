using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using UnityEngine.UI;
using AssemblyCSharp;

public class Winner : MonoBehaviour {
	public TextMesh WinnerName;
	// Use this for initialization
	void Start () {
		WinnerName.color = Global.winner.getColor();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
