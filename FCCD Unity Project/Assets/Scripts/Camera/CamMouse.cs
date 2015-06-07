using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System;
public class CamMouse : MonoBehaviour
{
	public float speed = 20.0F;
	public float rotationSpeed = 100.0F;
	public Camera cam;
	float maxOrtho;
	float minOrtho;

	public void Start ()
	{
		maxOrtho = cam.orthographicSize*Math.Max(Global.WIDTH/10,Global.HEIGHT/10);
		minOrtho = cam.orthographicSize/2;
//		Debug.Log (Camera.main.Equals(cam));
//		Debug.Log("Transform Tag is: " + cam.tag);
	}
	
	void Update ()
	{
	//	Debug.Log ("dsds"+(Camera.main.Equals(cam)));
		if (Input.GetMouseButton (1)) {
			
			double h = -40.0 * Input.GetAxis ("Mouse Y");
			double v = -40.0 * Input.GetAxis ("Mouse X");
			cam.transform.Translate ((float)v, (float)h, 0);
		} 
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) { // back 
			cam.orthographicSize = Mathf.Max (cam.orthographicSize - speed, minOrtho);

			
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) { // forward
			cam.orthographicSize = Mathf.Min (cam.orthographicSize + speed, maxOrtho);
		}
	}
}