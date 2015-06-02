//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic;
using UnityEngine;

public class ScreenShotTaker : GLMonoBehaviour
{
	public const KeyCode ScreenShotKey = KeyCode.F9;
	
	private int screenshotCount;

	private static ScreenShotTaker instance;
	static ScreenShotTaker Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<ScreenShotTaker>();
			}

			return instance;
		}
	}

	public void Start()
	{
		if(!PlayerPrefs.HasKey("code-spot::screenshotCount"))
		{
			PlayerPrefs.SetInt("code-spot::screenshotCount", 0);
		}
		else
		{
			screenshotCount = PlayerPrefs.GetInt("code-spot::screenshotCount");
		}
	}
	
	public void Update()
	{
		if(Input.GetKeyDown(ScreenShotKey))
		{
			Take();
		}
	}

	public static void Take()
	{
		Instance.Take__();
	}

	private void Take__()
	{
		string path = "screen_" + screenshotCount + ".png";
		Application.CaptureScreenshot(path, 2);

		screenshotCount++;
		PlayerPrefs.SetInt("code-spot::screenshotCount", screenshotCount);
	}
}
