// Attach this script to a camera and it will 
// draw a debug line pointing outward from the normal
var Pointer : GameObject;
var Splash : GameObject;
var Vertex : int;
var MeshX : int;
var MeshY : int;
var DisplayObject : GameObject;
var Hide : boolean = false;
var Elev : boolean = false;
var XCoord : int;
var ZCoord : int;
var XSelected : int;
var ZSelected : int;
var guiStyle : GUISkin;
var MapControl : GameObject;
var MapController;
var GridProjector : GameObject;
var hitSizeX : int;
var SectorOffsetX : int;
var SectorOffsetY : int;
var WorldXCoord : int;
var WorldZCoord : int;

var Selected : boolean = false;

function Start () {
	MapControl = GameObject.Find("MapControl");
	MapController = MapControl.GetComponent("MapControl");
	MeshX = MapController.MeshX;
	MeshY = MapController.MeshY;
	if (MapController.IsElevationMap == true) {
		Elev = true;
	}
}

function Update () {
	Hide = true;
	MapController.MinimapActive = true;
	if (Input.mousePosition.x<MapController.MinimapOriginX || Input.mousePosition.y>MapController.MinimapOriginY) {
		Hide = false;
		MapController.MinimapActive = false;
		// Only if we hit something, do we continue 
		var hit : RaycastHit;
		if (!Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), hit, Mathf.Infinity)) {
			return; 
		}
		// Just in case, also make sure the collider also has a renderer 
		// material and texture 
		var meshCollider = hit.collider as MeshCollider;
		var hitMeshObj = hit.transform.parent.gameObject;
		if (hitMeshObj) {
			var hitSector = hitMeshObj.GetComponent("GMeshControl").Sector;
			hitSizeX = hitMeshObj.GetComponent("GMeshControl").XSize;
			var hitYSize = hitMeshObj.GetComponent("GMeshControl").SizeY;
			SectorOffsetX = (32 * hitMeshObj.GetComponent("GMeshControl").SecX)-1;
			if (SectorOffsetX < 0) 
				SectorOffsetX = 0;
			SectorOffsetY = (32 * hitMeshObj.GetComponent("GMeshControl").SecY)-1;
			if (SectorOffsetY < 0) 
				SectorOffsetY = 0;
			if (meshCollider == null || meshCollider.sharedMesh == null) 
				return; 
			var mesh : Mesh = meshCollider.sharedMesh;
			var vertices = mesh.vertices; 
			var triangles = mesh.triangles; 
			// Extract local space normals of the triangle we hit
			var p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
			var p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
			var p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];
			var i0 = triangles[hit.triangleIndex * 3 + 0];
			var i1 = triangles[hit.triangleIndex * 3 + 1];
			var i2 = triangles[hit.triangleIndex * 3 + 2];
		
			// interpolate using the barycentric coordinate of the hitpoint 
			var baryCenter = hit.barycentricCoordinate;
			var VertPoint;
			if (baryCenter.x >= baryCenter.y && baryCenter.x >= baryCenter.z) {
				VertPoint = p0;
				Vertex = i0;
			}
			if (baryCenter.y > baryCenter.x && baryCenter.y > baryCenter.z) {
				VertPoint = p1;
				Vertex = i1;
			}
			if (baryCenter.z > baryCenter.y && baryCenter.z > baryCenter.x) {
				VertPoint = p2;
				Vertex = i2;
			}
			var hitTransform : Transform = hit.collider.transform; 
			Hide = false;
		
			XCoord = (Vertex % (hitSizeX+1));
			ZCoord = (Vertex / (hitSizeX+1));
			WorldXCoord = XCoord + SectorOffsetX;
			WorldZCoord = ZCoord + SectorOffsetY;
		}
	}
}

//function OnGUI () {
//	GUI.skin = guiStyle;
//	GUI.depth = 1;
//	var TerrainType : int;
//	var TerrainElev : int;
//	TerrainType = MapController.QueryTerrainType(WorldXCoord, WorldZCoord);
//	TerrainElev = MapController.QueryHeight(WorldXCoord, WorldZCoord);
//	var Terr : String = MapController.TerrainTypes[ TerrainType ];
//	var Pos : String = WorldXCoord.ToString() + " , " + WorldZCoord.ToString();
//	var Hgt : String = "Elev: " + TerrainElev.ToString();
//	
//	if (MapController.GUIMode == 0 && Hide == false) {
//		GUI.Box(new Rect(Input.mousePosition.x-52, Screen.height-Input.mousePosition.y+32, 100, 20), Terr, "DarkText");
//		GUI.Box(new Rect(Input.mousePosition.x-48, Screen.height-Input.mousePosition.y+32, 100, 20), Terr, "DarkText");
//		GUI.Box(new Rect(Input.mousePosition.x-50, Screen.height-Input.mousePosition.y+34, 100, 20), Terr, "DarkText");
//		GUI.Box(new Rect(Input.mousePosition.x-50, Screen.height-Input.mousePosition.y+30, 100, 20), Terr, "DarkText");
//		GUI.Box(new Rect(Input.mousePosition.x-50, Screen.height-Input.mousePosition.y+32, 100, 20), Terr, "LightText");
//	
//		GUI.Box(new Rect(Input.mousePosition.x-52, Screen.height-Input.mousePosition.y+20, 100, 20), Pos, "DarkCoord");
//		GUI.Box(new Rect(Input.mousePosition.x-48, Screen.height-Input.mousePosition.y+20, 100, 20), Pos, "DarkCoord");
//		GUI.Box(new Rect(Input.mousePosition.x-50, Screen.height-Input.mousePosition.y+22, 100, 20), Pos, "DarkCoord");
//		GUI.Box(new Rect(Input.mousePosition.x-50, Screen.height-Input.mousePosition.y+18, 100, 20), Pos, "DarkCoord");
//		GUI.Box(new Rect(Input.mousePosition.x-50, Screen.height-Input.mousePosition.y+20, 100, 20), Pos, "LightCoord");
//		
//		if (Elev == true) {
//			GUI.Box(new Rect(Input.mousePosition.x-52, Screen.height-Input.mousePosition.y+44, 100, 20), Hgt, "DarkText");
//			GUI.Box(new Rect(Input.mousePosition.x-48, Screen.height-Input.mousePosition.y+44, 100, 20), Hgt, "DarkText");
//			GUI.Box(new Rect(Input.mousePosition.x-50, Screen.height-Input.mousePosition.y+46, 100, 20), Hgt, "DarkText");
//			GUI.Box(new Rect(Input.mousePosition.x-50, Screen.height-Input.mousePosition.y+42, 100, 20), Hgt, "DarkText");
//			GUI.Box(new Rect(Input.mousePosition.x-50, Screen.height-Input.mousePosition.y+44, 100, 20), Hgt, "LightText");			
//		}
//		if (GUI.Button(new Rect(0,0,80,20), "TOGGLE GRID")) {
//			GridProjector.SetActive(!GridProjector.activeSelf);
//		}
//	}
//	if (MapController.GUIMode == 0 && Hide == true) {
//		if (GUI.Button(new Rect(0,0,80,20), "TOGGLE GRID")) {
//			GridProjector.SetActive(!GridProjector.activeSelf);
//		}
//	}
//	if (MapController.GUIMode == 1) {
//		var MapString : String = "CREATING MESH " + MapController.GUICount.ToString() + " OF " + MapController.Sectors.ToString();
//		GUI.Box(new Rect(199, 299, 400, 100), MapString, "DarkTitle");
//		GUI.Box(new Rect(201, 301, 400, 100), MapString, "DarkTitle");
//		GUI.Box(new Rect(200, 300, 400, 100), MapString, "LightTitle");
//		MapString = "HEX @ " + MapController.GUICountX.ToString() + ", " + MapController.GUICountY.ToString();
//		GUI.Box(new Rect(199, 339, 400, 100), MapString, "DarkTitle");
//		GUI.Box(new Rect(201, 341, 400, 100), MapString, "DarkTitle");
//		GUI.Box(new Rect(200, 340, 400, 100), MapString, "LightTitle");
//	}
//	if (MapController.GUIMode == 2) {
//		MapString = "STITCHING MESH EDGES";
//		GUI.Box(new Rect(199, 299, 400, 100), MapString, "DarkTitle");
//		GUI.Box(new Rect(201, 301, 400, 100), MapString, "DarkTitle");
//		GUI.Box(new Rect(200, 300, 400, 100), MapString, "LightTitle");
//	}
//	if (MapController.GUIMode == 3) {
//		MapString = "CREATING TEXTURE MAP " + MapController.GUICount.ToString() + " OF " + MapController.Sectors.ToString();
//		GUI.Box(new Rect(199, 299, 400, 100), MapString, "DarkTitle");
//		GUI.Box(new Rect(201, 301, 400, 100), MapString, "DarkTitle");
//		GUI.Box(new Rect(200, 300, 400, 100), MapString, "LightTitle");
//	}
//}