using Gamelogic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public GameObject cubePrefab;

	private GameObject currentCube;
	private int cubeCount;

	public static int CubeCount
	{
		get { return Instance.cubeCount; }
		set { Instance.cubeCount = value; }
	}

	public void Start()
	{
		cubeCount = 0;
	}

	public static void CreateNewCube()
	{
		Instance.SpawnCubeImpl();
	}

	private void SpawnCubeImpl()
	{
		if (currentCube != null)
		{
			Destroy(currentCube);
		}

		currentCube = Instantiate(cubePrefab);
		cubeCount++;
	}

	public static void MakeCubeDarker()
	{
		Instance.MakeCubeDarkerImpl();
	}

	private void MakeCubeDarkerImpl()
	{
		if (currentCube == null) return;

		var color = currentCube.GetComponent<Renderer>().material.color;
		currentCube.GetComponent<Renderer>().material.color = color.Darker();
	}

	public static void MakeCubeLighter()
	{
		Instance.MakeCubeLighterImpl();
	}

	private void MakeCubeLighterImpl()
	{
		if (currentCube == null) return;

		var color = currentCube.GetComponent<Renderer>().material.color;
		currentCube.GetComponent<Renderer>().material.color = color.Lighter();
	}
}