using System.Security.Cryptography.X509Certificates;
using UnityEngine;

using Gamelogic;

public class PlayerPrefsTest : GLMonoBehaviour
{
	public const string ShowItemsKey = "ShowItems";
	public const string NameSpace = "Gamelogic";

	private bool[] showItems;

	public void Start()
	{
		if (GLPlayerPrefs.HasKey(NameSpace, ShowItemsKey))
		{
			showItems = GLPlayerPrefs.GetBoolArray(NameSpace, ShowItemsKey);
		}
		else
		{
			showItems = new[] { false, false, false };
		}
	}

	public void OnGUI()
	{
		for (int i = 0; i < showItems.Length; i++)
		{
			showItems[i] = GUILayout.Toggle(showItems[i], "Show Item " + i + "?");

			if (showItems[i])
			{
				GUILayout.Label("Item " + i);
			}
		}

		if(GUILayout.Button("Save"))
		{
			GLPlayerPrefs.SetBoolArray(NameSpace, ShowItemsKey, showItems);
		}
	}
}
