using UnityEngine;
using System.Collections;

public class SuperCamera : MonoBehaviour {

	public string PointedObject;
	RaycastHit hit;
	
	GameObject CamBase;
	GameObject CamFocus;

	public float ZoomMin;
	public float ZoomMax;
	public float PitchMin;
	public float PitchMax;
	public float LimitLeft;
	public float LimitRight;
	public float LimitTop;
	public float LimitBottom;

	public bool on;
	bool clickState;
	bool lastState;
	float timer;
	float delay = .5f;
	public string state = "IDLE";

	public bool dragging;
	public bool clicked;

	void Start () {
		CamBase = GameObject.Find ("CameraGroup");
		CamFocus = GameObject.Find ("CameraFocus");
	}
	
	void LateUpdate() {
		if (Input.touchCount >= 3) {
			// Don't do anything  :)  This is here as a stub, and to block operation if too many touches
		}
		if (Input.touchCount == 2) {
			// TWO-FINGER TOUCH CONTROLS
			// PINCH = Zoom in and out, as mousewheel
			// TWIST = Rotate entire camera assembly around the base (in the Y axis)
			// VERTICAL SWEEP = Rotate camera pitch up and down
			//
			// All of these functions can happen at once, using a helper function that needs to be in
			// Standard Assets: 'TouchHandler'

			float pinchAmount = 0;
			Quaternion desiredRotation = transform.rotation;
		 	// Run the TouchHandler
			TouchHandler.Calculate();
			// get pitch sweep distance
			float rotX = TouchHandler.sweepDistanceDelta;
			// clamp it so it doesn't go too fast
			Mathf.Clamp (rotX, -.05f, .05f);
			if ((CamFocus.transform.localEulerAngles.x >= PitchMax) ) {
				rotX = -1f;
			}
			if ( (CamFocus.transform.localEulerAngles.x <= PitchMin) ) {
					rotX = 1f;
			}
			// apply it
			CamFocus.transform.Rotate(new Vector3(rotX, 0f, 0f), Space.Self);
			// zoom
			if (Mathf.Abs(TouchHandler.pinchDistanceDelta) > 0) {
				pinchAmount = TouchHandler.pinchDistanceDelta * .5f;
			}
			//rotate
			if (Mathf.Abs(TouchHandler.turnAngleDelta) > 0) {
				Vector3 rotationDeg = Vector3.zero;
				rotationDeg.z = -TouchHandler.turnAngleDelta * .5f;
				desiredRotation *= Quaternion.Euler(rotationDeg);
			}
		 	// apply pitch
			gameObject.transform.root.transform.Rotate(0f, TouchHandler.turnAngleDelta, 0f);
			// clamp zoom
			float temp = transform.localPosition.z;
			temp += pinchAmount;
			if (temp < -ZoomMax) {
				temp = -ZoomMax;
			}
			if (temp > -ZoomMin) {
				temp = -ZoomMin;
			}
			transform.localPosition = new Vector3(0f, 0f, temp);
		}
	}
	
	void Update () {
		if (on && Input.touchCount < 2) {
			// zooming with mouse wheel
			if (Input.GetAxis("Mouse ScrollWheel") < 0) {
				Vector3 pos = Camera.main.transform.localPosition;
				pos.z *= 1.1f;
				if (pos.z < -ZoomMax) {
					pos.z = -ZoomMax;
				}
				Camera.main.transform.localPosition = pos;
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0) {
				Vector3 pos = Camera.main.transform.localPosition;
				pos.z *= .9f;
				if (pos.z > -ZoomMin) {
					pos.z = -ZoomMin;
				}
				Camera.main.transform.localPosition = pos;
			}
			// if a button is down
			if( (Input.GetMouseButton(0) == true || Input.GetMouseButton(2) == true) ) {

				if (lastState == false) {			// if this is the first time this has registered
					clickState = true;				// set these flags
					lastState = true;
					timer = Time.time + .25f;		// init the timer
				}
				if (Time.time > timer) {			// if the timer has expired and stil clicking, we're DRAGGING
					if (lastState == true) {
						state = "DRAGGING";
						dragging = true;
						clicked = false;
						Screen.lockCursor = true;	// hide the cursor
					}
				}
			}
			// camera rotation with mouse
			if (Input.GetMouseButton(1) == true && Input.GetMouseButton(0) == false) {
				Screen.lockCursor = true;
				float h = 2.0F * Input.GetAxis("Mouse X");
				float v = 2.0F * Input.GetAxis("Mouse Y");
				Vector3 camAngle = CamBase.transform.eulerAngles;
				camAngle.y += h;
				CamBase.transform.eulerAngles = camAngle;
				camAngle = CamFocus.transform.eulerAngles;
				camAngle.x -= v;
				if (camAngle.x < PitchMin) {
					camAngle.x = PitchMin;
				}
				if (camAngle.x > PitchMax) {
					camAngle.x = PitchMax;
				}
				CamFocus.transform.eulerAngles = camAngle;
			}
			// if no buttons down, release the cursor
			if (Input.GetMouseButton(0) == false && Input.GetMouseButton(1) == false && Input.GetMouseButton(2) == false) {
				Screen.lockCursor = false;
			}
			// if clicked
			if (clickState == true 
					&& Input.GetMouseButton(0) == false 
				    && Input.GetMouseButton(2) == false 
				    && state != "DRAGGING" 
			    ) {
				lastState = false;
				clickState = false;
				state = "CLICKED";
				dragging = false;
				clicked = true;
				timer = Time.time + delay;
			}
			
			// if no click, and not dragging, set state to IDLE
			if (Time.time > timer && (state == "CLICKED" || state == "DRAGGING") && lastState == false && clickState == false) {
				state = "IDLE";
				dragging = false;
			}
			lastState = clickState;
			if ( (Input.GetMouseButton(0) || Input.GetMouseButton(2)) ) {
				clickState = true;
			} else {
				clickState = false;
			}

			//panning with mouse
			if (dragging || Input.touchCount == 1) {
				float h = -1.0f * Input.GetAxis("Mouse X");
				float v = -1.0f * Input.GetAxis("Mouse Y");
				CamBase.transform.Translate(new Vector3(h, 0f, v));
			}

			// keyboard control: I did not use the Input.Axis keys for this, I wanted it to stand separately
			float key_h = 1.0f;
			float key_v = 1.0f;
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				key_h = 0.1f;
				CamBase.transform.Translate(new Vector3(0f, 0f, key_h));
			}
			if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
				key_h = -0.1f;
				CamBase.transform.Translate(new Vector3(0f, 0f, key_h));
			}
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
				key_v = -0.1f;
				CamBase.transform.Translate(new Vector3(key_v, 0f, 0f));
			}
			if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
				key_v = 0.1f;
				CamBase.transform.Translate(new Vector3(key_v, 0f, 0f));
			}
			
			// limit camera position
			Vector3 CamPos = CamBase.transform.position;
			if (CamPos.x > LimitRight || CamPos.x < LimitLeft || CamPos.z > LimitTop || CamPos.z < LimitBottom) {
				if (CamPos.x < LimitLeft) CamPos.x = LimitLeft;
				if (CamPos.x > LimitRight) CamPos.x = LimitRight;
				if (CamPos.z < LimitBottom) CamPos.z = LimitBottom;
				if (CamPos.z > LimitTop) CamPos.z = LimitTop;
				CamBase.transform.position = CamPos;
			}
			
			// a raycast that stores the object under the cursor in the variable 'PointedObject'
			if ( Time.frameCount %2 == 0 ) {
				if (!Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
					PointedObject = null;
					return;
				}
				PointedObject = hit.collider.gameObject.name;
			}
		}
	}
}