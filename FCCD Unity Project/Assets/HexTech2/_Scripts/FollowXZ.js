#pragma strict

var FollowTarget : GameObject;

function Start () {

}

function Update () {
	gameObject.transform.position = new Vector3 (FollowTarget.transform.position.x, gameObject.transform.position.y, FollowTarget.transform.position.z);
}