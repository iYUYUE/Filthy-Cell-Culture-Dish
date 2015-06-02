using Gamelogic;
using UnityEngine;

public class CubeGameGUI : GLMonoBehaviour
{
	public void OnGUI()
	{

		GUILayout.BeginArea(new Rect(100, 0, 200, 200));
		GUILayout.Label(GameManager.CubeCount.ToString());
		
		if (GUILayout.Button("New Cube"))
		{
			GameManager.CreateNewCube();
		}

		if (GUILayout.Button("Make Darker"))
		{
			GameManager.MakeCubeDarker();
		}

		if (GUILayout.Button("Make Lighter"))
		{
			GameManager.MakeCubeLighter();
		}

		GUILayout.EndArea();
	}
}
