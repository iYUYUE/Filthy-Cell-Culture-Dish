var MainCamera : GameObject;
var MinimapCamera : GameObject;
var Orbiter;
var delay : float;
var amount : float;
var increment : float;
var distAmount : float;
var MapControl;
var MapController;

function Start () {
	MapControl = GameObject.Find("MapControl");
	MapController = MapControl.GetComponent("MapControl");
	Orbiter = MainCamera.GetComponent("MouseOrbit");
	gameObject.transform.LookAt(MainCamera.transform);
}


function Update () {
	if (MapController.MinimapActive == false) {
		var Temp : float = distAmount * amount;
		if (Time.time > delay) {
			if ( (Input.GetKey (KeyCode.UpArrow)) || (Input.GetKey (KeyCode.W)) ) {
				gameObject.transform.Translate(new Vector3(0, 1, -1) * Temp);
			}
			if ( (Input.GetKey (KeyCode.DownArrow)) || (Input.GetKey (KeyCode.S)) ) {
				gameObject.transform.Translate(new Vector3(0, -1, 1) * Temp);	
			}
			if ( (Input.GetKey (KeyCode.LeftArrow)) || (Input.GetKey (KeyCode.A)) ) {
				gameObject.transform.Translate(Vector3.right * Temp);
			}
			if ( (Input.GetKey (KeyCode.RightArrow)) || (Input.GetKey (KeyCode.D)) ) {
				gameObject.transform.Translate(Vector3.left * Temp);
			}
			gameObject.transform.position.y = 0;
		}
		if(gameObject.transform.position.x < MapController.TopLeft.x + 1.0) {
			gameObject.transform.position.x = MapController.TopLeft.x + 1.0;
		}
		if(gameObject.transform.position.x > MapController.BottomRight.x - 1.0) {
			gameObject.transform.position.x = MapController.BottomRight.x - 1.0;
		}
		if(gameObject.transform.position.z > MapController.TopLeft.z - 1.0) {
			gameObject.transform.position.z = MapController.TopLeft.z - 1.0;
		}
		if(gameObject.transform.position.z < MapController.BottomRight.z + 1.0) {
			gameObject.transform.position.z = MapController.BottomRight.z + 1.0;
		}
		if(Input.GetMouseButton(0)) {
			MinimapCamera.transform.position.x = gameObject.transform.position.x;
			MinimapCamera.transform.position.z = gameObject.transform.position.z;
			MinimapCamera.GetComponent(Camera).orthographicSize = MainCamera.GetComponent("MouseOrbit").Dist / 2.0;
		}
		gameObject.transform.LookAt(MainCamera.transform);
	} else {
		Temp = distAmount * amount;
		if ( (Input.GetKey (KeyCode.UpArrow)) || (Input.GetKey (KeyCode.W)) ) {
			MinimapCamera.transform.Translate(Vector3.up * Temp);
		}
		if ( (Input.GetKey (KeyCode.DownArrow)) || (Input.GetKey (KeyCode.S)) ) {
			MinimapCamera.transform.Translate(Vector3.down * Temp);	
		}
		if ( (Input.GetKey (KeyCode.LeftArrow)) || (Input.GetKey (KeyCode.A)) ) {
			MinimapCamera.transform.Translate(Vector3.left * Temp);
		}
		if ( (Input.GetKey (KeyCode.RightArrow)) || (Input.GetKey (KeyCode.D)) ) {
			MinimapCamera.transform.Translate(Vector3.right * Temp);
		}
		var deadZone : float = 0.01;
		if (Input.GetAxis("Mouse ScrollWheel") < -deadZone || Input.GetAxis("Mouse ScrollWheel") > deadZone) {
			MinimapCamera.GetComponent(Camera).orthographicSize -= Input.GetAxis("Mouse ScrollWheel");
			if (MinimapCamera.GetComponent(Camera).orthographicSize < 2.0) {
				MinimapCamera.GetComponent(Camera).orthographicSize = 2.0;
			}
			if (MinimapCamera.GetComponent(Camera).orthographicSize > 20.0) {
				MinimapCamera.GetComponent(Camera).orthographicSize = 20.0;
			}
		}
		if(Input.GetMouseButton(0)) {
			gameObject.transform.position.x = MinimapCamera.transform.position.x;
			gameObject.transform.position.z = MinimapCamera.transform.position.z;
			MainCamera.GetComponent("MouseOrbit").Dist = MinimapCamera.GetComponent(Camera).orthographicSize * 2.0;
		}
		if(MinimapCamera.transform.position.x < MapController.TopLeft.x + 1.0) {
			MinimapCamera.transform.position.x = MapController.TopLeft.x + 1.0;
		}
		if(MinimapCamera.transform.position.x > MapController.BottomRight.x - 1.0) {
			MinimapCamera.transform.position.x = MapController.BottomRight.x - 1.0;
		}
		if(MinimapCamera.transform.position.z > MapController.TopLeft.z - 1.0) {
			MinimapCamera.transform.position.z = MapController.TopLeft.z - 1.0;
		}
		if(MinimapCamera.transform.position.z < MapController.BottomRight.z + 1.0) {
			MinimapCamera.transform.position.z = MapController.BottomRight.z + 1.0;
		}
	}
}


