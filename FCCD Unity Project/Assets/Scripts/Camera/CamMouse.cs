using UnityEngine;
using System.Collections;

public class CamMouse : MonoBehaviour
{
	public float speed = 20.0F;
	public float rotationSpeed = 100.0F;
	Camera cam;
	float maxOrtho;

	public void Start ()
	{
		cam = this.camera;
		maxOrtho = cam.orthographicSize;
	}

	void Update ()
	{
		if (Input.GetMouseButton (1)) {
			
			double h = -40.0 * Input.GetAxis ("Mouse Y");
			double v = -40.0 * Input.GetAxis ("Mouse X");
			transform.Translate ((float)v, (float)h, 0);
		} 
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) { // back 
			cam.orthographicSize = Mathf.Max (cam.orthographicSize - speed, maxOrtho/3);
			
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) { // forward
			cam.orthographicSize = Mathf.Min (cam.orthographicSize + speed, maxOrtho*2);
		}
	}
}