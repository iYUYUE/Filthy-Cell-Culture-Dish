using System.Collections;
using Gamelogic;
using UnityEngine;

public class Cube : MonoBehaviour 
{

	public enum CubeStates 
	{
		Swivel,
		MoveStraight
	}

	private StateMachine<CubeStates> stateMachine;
	private float moveSpeed = 1;
	private float switchTime = 3; 
	private float swivelSpeed = 2;

	public void Start()
	{
		stateMachine = new StateMachine<CubeStates>();

		stateMachine.AddState(CubeStates.Swivel, OnSwivelStart, OnSwivelUpdate, OnSwivelStop);
		stateMachine.AddState(CubeStates.MoveStraight, null, OnMoveStraightUpdate, null);

		stateMachine.ChangeState(CubeStates.MoveStraight);

		StartCoroutine(SwitchStates());
	}

	public IEnumerator SwitchStates()
	{
		while (true)
		{
			stateMachine.ChangeState(CubeStates.Swivel);
			yield return new WaitForSeconds(switchTime);

			stateMachine.ChangeState(CubeStates.MoveStraight);
			yield return new WaitForSeconds(switchTime);
		}
	}

	public void Update()
	{
		stateMachine.Update();
	}

	public void OnGUI()
	{
		GUILayout.TextField(stateMachine.CurrentState.ToString());
	}

	public void OnMoveStraightUpdate()
	{
		float dx = -Time.deltaTime * moveSpeed;

		transform.TranslateX(dx);
	}

	public void OnSwivelUpdate()
	{
		float dx = Time.deltaTime*moveSpeed;
		float y = Mathf.Sin(swivelSpeed * transform.position.x * 2 * Mathf.PI/switchTime);

		transform.TranslateX(dx);
		transform.SetY(y);
	}

	public void OnSwivelStart()
	{
		GetComponent<Renderer>().material.color = Color.red;
	}

	public void OnSwivelStop()
	{
		GetComponent<Renderer>().material.color = Color.green;
		transform.SetY(0); //recallibrate
	}
}
