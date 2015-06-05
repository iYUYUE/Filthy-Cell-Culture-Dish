using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AssemblyCSharp;
public class Nexturn : MonoBehaviour {
	
	[SerializeField] private Button MyButton = null; // assign in the editor
	
	void Start() { MyButton.onClick.AddListener(() => { //nextTurn();  
		});
	}

}
