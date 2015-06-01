private var MeshX : int = 64;
private var MeshY : int = 64;
private var Sectors : int;
private var SectorsX : int;
private var SectorsY : int;
private var SectorBorders = new Vector2[1, 1];
private var SectorArray = new GameObject[1, 1];
private var TopLeft : Vector3;
private var BottomRight : Vector3;

var SuperCamera : GameObject;
var MapHolder : GameObject;
var BuildMaps : boolean;
var SaveMaps : boolean;
var IsElevationMap : boolean;
var TieredMap : boolean;

private var GUIMode : int = 0;
private var GUICount : int = 0;
private var GUICountX : int = 0;
private var GUICountY : int = 0;

var MapTexture : Texture2D;
private var MapName : String;
var MapColors = new Color[10];
var RoadColor : Color;
var MapFeatureColors = new Color[7];
var DesertColors = new Color[7];
var SnowColors = new Color[7];
var StoneColors = new Color[7];
var RiverColors = new Color[7];
var GrassColors = new Color[7];
var ForestColors = new Color[7];
var LavaColors = new Color[7];
private var Vertices = new Array();
private var FlatVertices = new Array();
private var Triangles = new Array();
private var UV = new Array();
private var TriOffset : Vector3;

var MasterMaterial : Material;
private var SectorMaterials = new Material[1];
var MasterTexture : Texture2D;
private var Textures = new Texture2D[1];

var MinimapMasterMaterial : Material;
private var MinimapSectorMaterials = new Material[1];
var MinimapMasterTexture : Texture2D;
private var MinimapTextures = new Texture2D[1];

private var TypeArray = new int[1, 1];
private var HeightArray = new int[1, 1];
private var RiverArray = new int[1, 1];
private var RoadArray = new int[1, 1];

private var HexCoords = new Vector2[6];
private var MinX : float = 10000.0;
private var MaxX : float = -10000.0;
private var MinZ : float = 10000.0;
private var MaxZ : float = -10000.0;
private var TriWidth : float = 0.5;
private var TriHeight : float = -0.425;
private var NewMesh : Mesh;
private var MoveMesh;
private var GraphicMeshes = new Mesh[1];
private var GraphicMeshObjects = new GameObject[1];
var MovePrefab : GameObject;
var GraphicPrefab : GameObject;
var WaterPrefab : GameObject;
var DeepPrefab : GameObject;
var MarkerPrefab : GameObject;
//var HiddenNodePrefab : GameObject;
var VolcanoSmokePrefab : GameObject;

private var MasterUV = new Vector2[3];
private var HexSidePoints = new Vector2[24];

private var MinimapCamera;
var MinimapOriginX : int;
var MinimapOriginY : int;
var MinimapActive : boolean = false;

var Features = new GameObject[5];
var FeatureOffset = new Vector3[6];

private var UVSet0 = new Array();
private var UVSet1 = new Array();
private var UVSet2 = new Array();
private var UVSet3 = new Array();

private var UMatrix = [
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65],
[ 0, 2, 4, 6, 8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64],
[ 1, 3, 5, 7, 9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63,65]
];

private var VMatrix =
	[ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32];

private var TerrainTypes = [
	"Plains",		//0
	"Hills",		//1
	"Mountains",	//2
	"Forest",		//3
	"Barren",		//4*
	"Swamp",		//5
	"Sea",			//6
	"Ocean",		//7
	"Jungle",		//8
	"Forest Hills",	//9
	"Jungle Hills",	//10
	"Desert",		//11
	"Desert Hills",	//12
	"Peaks",		//13
	"Volcano",		//14
	"Barren Hills",	//15*
	"Tundra",		//16*
	"Frozen Hills",	//17*
	"Glacial Mountains",	//18*
	"Glacial Peaks"			//19*
];

private var NewTerrainTypes = [
	"Ocean",				//0		Main Type 0		Feature Type 0 (default)
	"Sea",					//1		Main Type 1		Feature Type 0 (default)
	"Marsh",				//2		Main Type 2		Feature Type 0 (default)
	"Plains",				//3		Main Type 3		Feature Type 0 (default)
	"Hills",				//4		Main Type 4		Feature Type 0 (default)
	"Mountains",			//5		Main Type 5		Feature Type 0 (default)
	"Mountain Summit",		//6		Main Type 6		Feature Type 0 (default)
	"Swamp",				//7		Main Type 2		Feature Type 12 (jungle) 
	"Badland",				//8		Main Type 3		Feature Type 1 (rock)
	"Arid",					//9		Main Type 3		Feature Type 2 (arid)
	"Desert",				//10	Main Type 3		Feature Type 3 (sand)
	"Grass",				//11	Main Type 3		Feature Type 4 (grass)
	"Ash Plain",			//12	Main Type 3		Feature Type 5 (ash)
	"Burnt Forest",			//13	Main Type 3		Feature Type 6 (burnt trees)
	"Tundra",				//14	Main Type 3		Feature Type 7 (tundra)
	"Snowfield",			//15	Main Type 3		Feature Type 8 (snow)
	"Scrub",				//16	Main Type 3		Feature Type 9 (scrub)
	"Forest",				//17	Main Type 3		Feature Type 10 (trees)
	"Dense Forest",			//18	Main Type 3		Feature Type 11 (dense trees)
	"Jungle",				//19	Main Type 3		Feature Type 12 (jungle)
	"Glacier",				//20	Main Type 3		Feature Type 13 (ice)
	"Lava Fields",			//21	Main Type 3		Feature Type 14 (lava)
	"Broken Land",			//22	Main Type 4		Feature Type 1 (rock)
	"Arid Hills",			//23	Main Type 4		Feature Type 2 (arid)
	"Desert Hills",			//24	Main Type 4		Feature Type 3 (sand)
	"Grassland Hills",		//25	Main Type 4		Feature Type 4 (grass)
	"Ashen Hills",			//26	Main Type 4		Feature Type 5 (ash)
	"Burnt Forest Hills",	//27	Main Type 4		Feature Type 6 (burnt trees)
	"Tundra Hills",			//28	Main Type 4		Feature Type 7 (tundra)
	"Snowy Hills",			//29	Main Type 4		Feature Type 8 (snow)
	"Scrub Hills",			//30	Main Type 4		Feature Type 9 (scrub)
	"Forest Hills",			//31	Main Type 4		Feature Type 10 (trees)
	"Dense Forest Hills",	//32	Main Type 4		Feature Type 11 (dense trees)
	"Jungle Hills",			//33	Main Type 4		Feature Type 12 (jungle)
	"Rocky Mountains",		//34	Main Type 5		Feature Type 1 (rock)
	"Snowy Mountains",		//35	Main Type 5		Feature Type 8 (snow)
	"Rocky Peak",			//36	Main Type 6		Feature Type 1 (rock)
	"Snowy Peak",			//37	Main Type 6		Feature Type 8 (snow)
	"Volcano"				//38	Main Type 6		Feature Type 14 (lava)
];

function Start () {
	MapName = MapTexture.name;
	if(MapName.Substring(0, 1) == "E") {
		IsElevationMap = true;
	} else {
		IsElevationMap = false;
	}
	if (System.IO.File.Exists(Application.dataPath + "/Resources/MapFiles/" + MapName + "-s0.png")) {
		BuildMaps = false;
		SaveMaps = false;
	} else {
		BuildMaps = true;
		SaveMaps = true;
	}
	NewMesh = new Mesh();
	MinimapCamera = GameObject.Find("MinimapCamera");
	MinimapOriginX = MinimapCamera.GetComponent(Camera).pixelRect.xMin;
	MinimapOriginY = MinimapCamera.GetComponent(Camera).pixelRect.yMax;
// get size of mesh from size of map data image file "MapTexture"
	MeshX = (MapTexture.width/10)-1;
	MeshY = (MapTexture.height/9)-1;
	SuperCamera.GetComponent("SuperCamera").LimitRight = MeshX * 0.5;
	SuperCamera.GetComponent("SuperCamera").LimitBottom = 0.0 - (MeshY * 0.425);
	SuperCamera.transform.position = new Vector3(((SuperCamera.GetComponent("SuperCamera").LimitLeft + SuperCamera.GetComponent("SuperCamera").LimitRight)/2.0),
													0,
													((SuperCamera.GetComponent("SuperCamera").LimitTop + SuperCamera.GetComponent("SuperCamera").LimitBottom)/2.0));
// sectors are 32 x 32 hexes
	SectorsX = MeshX/32;
	SectorsY = MeshY/32;
	Sectors = SectorsX * SectorsY;
	SectorArray = new GameObject[SectorsX, SectorsY];
// set up sector materials
	SectorMaterials = new Material[Sectors];
	MinimapSectorMaterials = new Material[Sectors];
	Textures = new Texture2D[Sectors];
	MinimapTextures = new Texture2D[Sectors];
// set up meshes for each sector
	GraphicMeshes = new Mesh[Sectors];
	GraphicMeshObjects = new GameObject[Sectors];
// set up arrays for map
	TypeArray = new int[MeshX, MeshY];
	HeightArray = new int[MeshX, MeshY];
	RiverArray = new int[MeshX, MeshY];
	RoadArray = new int[MeshX, MeshY];
// SectorBorders contains the hex coordinates of the corners of the sectors
// this is used to determine which hex is part of which sector
	SectorBorders = new Vector2[SectorsX+1, SectorsY+1];
	for (y=0; y<SectorsY+1; y++) {
		for (x=0; x<SectorsX+1; x++) {
			SectorBorders[x, y] = new Vector2( ((x*32)-1), ((y*32)-1) );
			if (x==0) {
				SectorBorders[x,y].x = 0;
			}
			if (y==0) {
				SectorBorders[x,y].y = 0;
			}
		}
	}
// reads the map texture keys into arrays
// (these are at lower left of the map texture, terrain height on the left, and features on the right)
	MapColors = new Color[20];
	MapFeatureColors = new Color[20];
	for (i=0; i<20; i++) {
		MapColors[i] = MapTexture.GetPixel(0,i);
		MapFeatureColors[i] = MapTexture.GetPixel(0,i+20);
	}
	for (i=0; i<7; i++) {
		DesertColors[i] = MapTexture.GetPixel(0,i+40);
		SnowColors[i] = MapTexture.GetPixel(0,i+50);
		StoneColors[i] = MapTexture.GetPixel(0,i+60);
		RiverColors[i] = MapTexture.GetPixel(0,i+70);
		GrassColors[i] = MapTexture.GetPixel(0,i+80);
		ForestColors[i] = MapTexture.GetPixel(0,i+90);
		LavaColors[i] = MapTexture.GetPixel(0,i+100);
	}
// one pixel that determines the road color, could be expanded into an array
	RoadColor = MapTexture.GetPixel(1, 1);
	for (y=0; y<MeshY; y++) {
		for (x=0; x<MeshX; x++) {
// get values from map image
// center of square
			var CenterX = ((x+1)*10);
			var CenterY = (MapTexture.height-((y+1)*9));
			var TempColor : Color = MapTexture.GetPixel(CenterX, CenterY-1);
// 'features' one pixel right of center modify the terrain
			var TempFeatureColor : Color = MapTexture.GetPixel(CenterX, CenterY);
// pixel check for rivers
			var TempRiverColor1 : Color = MapTexture.GetPixel(CenterX+3, CenterY+4);
			var TempRiverColor2 : Color = MapTexture.GetPixel(CenterX+5, CenterY);
			var TempRiverColor3 : Color = MapTexture.GetPixel(CenterX+3, CenterY-4);
//// pixel check for roads
			var TempRoadColor1 : Color = MapTexture.GetPixel(CenterX+2, CenterY+3);
			var TempRoadColor2 : Color = MapTexture.GetPixel(CenterX+3, CenterY);
			var TempRoadColor3 : Color = MapTexture.GetPixel(CenterX+2, CenterY-3);
			var TempRoadColor4 : Color = MapTexture.GetPixel(CenterX-2, CenterY-3);
			var TempRoadColor5 : Color = MapTexture.GetPixel(CenterX-3, CenterY);
			var TempRoadColor6 : Color = MapTexture.GetPixel(CenterX-2, CenterY+3);
//// stagger for odd rows
			if (y%2!=0) {
				TempColor = 		MapTexture.GetPixel(CenterX+5, CenterY-1);
				TempFeatureColor = 	MapTexture.GetPixel(CenterX+5, CenterY);
				TempRiverColor1 = 	MapTexture.GetPixel(CenterX+8, CenterY+4);
				TempRiverColor2 = 	MapTexture.GetPixel(CenterX+10, CenterY);
				TempRiverColor3 = 	MapTexture.GetPixel(CenterX+8, CenterY-4);
				TempRoadColor1  = MapTexture.GetPixel(CenterX+7, CenterY+3);
				TempRoadColor2  = MapTexture.GetPixel(CenterX+8, CenterY);
				TempRoadColor3  = MapTexture.GetPixel(CenterX+7, CenterY-3);
				TempRoadColor4  = MapTexture.GetPixel(CenterX+3, CenterY-3);
				TempRoadColor5  = MapTexture.GetPixel(CenterX+2, CenterY);
				TempRoadColor6  = MapTexture.GetPixel(CenterX+3, CenterY+3);
			}
			if (IsElevationMap == true) {
				for(i=0; i<20; i++) {
					if (TempColor == MapColors[i]) {
						HeightArray[x, y] = i-2;
						if (HeightArray[x, y] >= 0) {
							HeightArray[x, y]++;
						}
					}
				}
				if(HeightArray[x, y] == -2) {
					HeightArray[x, y] = -3;
					TypeArray[x, y] = 0;
				}
				if(HeightArray[x, y] == -1) {
					HeightArray[x, y] = -2;
					TypeArray[x, y] = 1;
				}
				if(HeightArray[x, y] > 0) {
					TypeArray[x, y] = 10;//sand
					if(TempFeatureColor == MapFeatureColors[1]) {
						TypeArray[x, y] = 11;//grass
					}
					if(TempFeatureColor == MapFeatureColors[2]) {
						TypeArray[x, y] = 39;//dirt
					}
					if(TempFeatureColor == MapFeatureColors[3]) {
						TypeArray[x, y] = 12;//stone
					}
					if(TempFeatureColor == MapFeatureColors[4]) {
						TypeArray[x, y] = 15;//snow
					}
					if(TempFeatureColor == MapFeatureColors[5]) {
						TypeArray[x, y] = 7;//swamp
					}
					if(TempFeatureColor == MapFeatureColors[6]) {
						TypeArray[x, y] = 18;//dense forest
					}
					if(TempFeatureColor == MapFeatureColors[7]) {
						TypeArray[x, y] = 17;//forest
					}
				}
			}
			if (IsElevationMap == false) {
				RiverArray[x,y] = 0;	// init
				if (TempRiverColor1 == RiverColors[1])
					RiverArray[x,y] += 1;
				if (TempRiverColor2 == RiverColors[1])
					RiverArray[x,y] += 2;
				if (TempRiverColor3 == RiverColors[1])
					RiverArray[x,y] += 4;
				if (TempRiverColor1 == RiverColors[3])
					RiverArray[x,y] += 8;
				if (TempRiverColor2 == RiverColors[3])
					RiverArray[x,y] += 16;
				if (TempRiverColor3 == RiverColors[3])
					RiverArray[x,y] += 32;
				if (TempRiverColor1 == RiverColors[5])
					RiverArray[x,y] += 64;
				if (TempRiverColor2 == RiverColors[5])
					RiverArray[x,y] += 128;
				if (TempRiverColor3 == RiverColors[5])
					RiverArray[x,y] += 256;
				RoadArray[x,y] = 0;		// init
				if (TempRoadColor1 == MapFeatureColors[0])
					RoadArray[x,y] += 1;
				if (TempRoadColor2 == MapFeatureColors[0])
					RoadArray[x,y] += 2;
				if (TempRoadColor3 == MapFeatureColors[0])
					RoadArray[x,y] += 4;
				if (TempRoadColor4 == MapFeatureColors[0])
					RoadArray[x,y] += 8;
				if (TempRoadColor5 == MapFeatureColors[0])
					RoadArray[x,y] += 16;
				if (TempRoadColor6 == MapFeatureColors[0])
					RoadArray[x,y] += 32;
				HeightArray[x, y] = 0;  // init
				TypeArray[x, y] = 0;	// init
				if (TempColor == MapColors[0]) {					// OCEAN
					HeightArray[x, y] = -3;
					TypeArray[x, y] = 0;	}
				if (TempColor == MapColors[1]) {					// SEA
					HeightArray[x, y] = -1;
					TypeArray[x, y] = 1;	}
				if (TempColor == MapColors[2]) {					// LOW LAND
					HeightArray[x, y] = 0;
					TypeArray[x, y] = 2;								// marsh
					if (TempFeatureColor == MapFeatureColors[12]) {		// swamp
						TypeArray[x, y] = 7;
					}
				}
				if (TempColor == MapColors[3]) {					// PLAINS
					HeightArray[x, y] = 1;
					TypeArray[x, y] = 3;
					if (TempFeatureColor == MapFeatureColors[1]) {		// badlands
						TypeArray[x, y] = 8;}
					if (TempFeatureColor == MapFeatureColors[2]) {		// arid
						TypeArray[x, y] = 9;}				
					if (TempFeatureColor == MapFeatureColors[3]) {		// desert
						TypeArray[x, y] = 10;}
					if (TempFeatureColor == MapFeatureColors[4]) {		// grass
						TypeArray[x, y] = 11;}				
					if (TempFeatureColor == MapFeatureColors[5]) {		// ash
						TypeArray[x, y] = 12;}				
					if (TempFeatureColor == MapFeatureColors[6]) {		// burnt trees
						TypeArray[x, y] = 13;}				
					if (TempFeatureColor == MapFeatureColors[7]) {		// tundra
						TypeArray[x, y] = 14;}				
					if (TempFeatureColor == MapFeatureColors[8]) {		// snow
						TypeArray[x, y] = 15;}				
					if (TempFeatureColor == MapFeatureColors[9]) {		// scrub
						TypeArray[x, y] = 16;}				
					if (TempFeatureColor == MapFeatureColors[10]) {		// trees
						TypeArray[x, y] = 17;}				
					if (TempFeatureColor == MapFeatureColors[11]) {		// TREES
						TypeArray[x, y] = 18;}				
					if (TempFeatureColor == MapFeatureColors[12]) {		// jungle
						TypeArray[x, y] = 19;}				
					if (TempFeatureColor == MapFeatureColors[13]) {		// ice
						TypeArray[x, y] = 20;}				
					if (TempFeatureColor == MapFeatureColors[14]) {		// lava
						TypeArray[x, y] = 21;}				
				}
				if (TempColor == MapColors[4]) {					// HILLS
					HeightArray[x, y] = Random.Range(3, 5);
					TypeArray[x, y] = 4;
					if (TempFeatureColor == MapFeatureColors[1]) {		// badlands
						TypeArray[x, y] = 22;}
					if (TempFeatureColor == MapFeatureColors[2]) {		// arid
						TypeArray[x, y] = 23;}				
					if (TempFeatureColor == MapFeatureColors[3]) {		// desert
						TypeArray[x, y] = 24;}
					if (TempFeatureColor == MapFeatureColors[4]) {		// grass
						TypeArray[x, y] = 25;}				
					if (TempFeatureColor == MapFeatureColors[5]) {		// ash
						TypeArray[x, y] = 26;}				
					if (TempFeatureColor == MapFeatureColors[6]) {		// burnt trees
						TypeArray[x, y] = 27;}				
					if (TempFeatureColor == MapFeatureColors[7]) {		// tundra
						TypeArray[x, y] = 28;}				
					if (TempFeatureColor == MapFeatureColors[8]) {		// snow
						TypeArray[x, y] = 29;}				
					if (TempFeatureColor == MapFeatureColors[9]) {		// scrub
						TypeArray[x, y] = 30;}				
					if (TempFeatureColor == MapFeatureColors[10]) {		// trees
						TypeArray[x, y] = 31;}				
					if (TempFeatureColor == MapFeatureColors[11]) {		// TREES
						TypeArray[x, y] = 32;}				
					if (TempFeatureColor == MapFeatureColors[12]) {		// jungle
						TypeArray[x, y] = 33;}				
				}
				if (TempColor == MapColors[5]) {					// MOUNTAINS
					HeightArray[x, y] = 8;
					TypeArray[x, y] = 5;		
					if (TempFeatureColor == MapFeatureColors[1]) {		// rocky
						TypeArray[x, y] = 34;}
					if (TempFeatureColor == MapFeatureColors[8]) {		// snowy
						TypeArray[x, y] = 35;}
				}
				if (TempColor == MapColors[6]) {					// PEAK
					HeightArray[x, y] = 12;
					TypeArray[x, y] = 6;
					if (TempFeatureColor == MapFeatureColors[1]) {		// rocky
						TypeArray[x, y] = 36;}
					if (TempFeatureColor == MapFeatureColors[8]) {		// snowy
						TypeArray[x, y] = 37;}
					if (TempFeatureColor == MapFeatureColors[14]) {		// lava
						TypeArray[x, y] = 38;}
				}
				if(x==0 || y==0 || x==MeshX-1 || y==MeshY-1) {
					if (HeightArray[x, y] < 0) {
						HeightArray[x, y] = -3;
					} else {
						HeightArray[x, y] = 1;
					}
					TypeArray[x, y] = -1;
				}
			}
// stagger the rows
			TriOffest = new Vector3(0.0, 0.0, 0.0);
			if (y%2!=0)
				TriOffest = new Vector3(TriWidth/2.0, 0.0, 0.0);
// construct new vertex
			var NewVertex : Vector3;
			if (IsElevationMap == true) {
				var tempHeight : float = 0.1*(HeightArray[x, y]);
				
				if (HeightArray[x, y] < 0) {
					tempHeight += 0.05;
				}
				if (HeightArray[x, y] > 0) {
					tempHeight -= 0.05;
				}
				NewVertex = new Vector3(x*TriWidth, tempHeight, y*TriHeight);
			} else {
				NewVertex = new Vector3(x*TriWidth, (0.03*HeightArray[x, y]), y*TriHeight);
			}
// add stagger (if any)
			NewVertex += TriOffest;
// add to vertex array
			Vertices.Add(NewVertex);
			if (TypeArray[x, y] == 38) { // volcano?
				var TempObj = Instantiate(VolcanoSmokePrefab, NewVertex, Quaternion.identity);
				TempObj.transform.parent = MapHolder.transform;
			}
//			if (x == 0 || x == MeshX-1 || y == 0 || y == MeshY-1) { // edge?
//				TempObj = Instantiate(HiddenNodePrefab, NewVertex, Quaternion.identity);
//				TempObj.transform.parent = MapHolder.transform;
//			}
		}
	}
	TopLeft = Vertices[0];
	BottomRight = Vertices[Vertices.length-1];
// create GraphicMeshes
	for (i=0; i<Sectors; i++) {
// instantiate material
		SectorMaterials[i] = Instantiate(MasterMaterial);
		MinimapSectorMaterials[i] = Instantiate(MinimapMasterMaterial);
// instantiate textures for each channel
		if (BuildMaps == true) {
			Textures[i] = Instantiate(MasterTexture);
			MinimapTextures[i] = Instantiate(MinimapMasterTexture);
			MinimapTextures[i].name = i.ToString();
		}
		if (BuildMaps == false) {
			var FileName : String = "MapFiles/" + MapName + "-s" + i.ToString();
			Textures[i] = Resources.Load(FileName) as Texture2D;
			FileName = "MapFiles/" + MapName + "-m" + i.ToString();
			MinimapTextures[i] = Resources.Load(FileName) as Texture2D;
		}
// assign textures to material
		SectorMaterials[i].SetTexture("_MainTex", Textures[i]);
		MinimapSectorMaterials[i].mainTexture = MinimapTextures[i];
// create sector meshes
		var GraphicMeshObj : GameObject = Instantiate(GraphicPrefab, Vector3.zero, Quaternion.identity);
// rename
		GraphicMeshObj.name = "Sector "+i.ToString();
		GraphicMeshObj.GetComponent("GMeshControl").Sector = i;
		for (y=0; y<MeshY; y++) {
			for (x=0; x<MeshX; x++) {	
				GraphicMeshObj.GetComponent("GMeshControl").TempTypeArray.Add(TypeArray[x,y]);
			}
		}
		GraphicMeshObj.transform.parent = MapHolder.transform;
		GraphicMeshObj.GetComponent("GMeshControl").SetUp();
// assign meshes
		GraphicMeshObjects[i] = GraphicMeshObj;
		GraphicMeshes[i] = GraphicMeshObjects[i].GetComponent(MeshFilter).mesh;
		GraphicMeshObjects[i].renderer.material = SectorMaterials[i];
		GraphicMeshObjects[i].renderer.material.SetTexture("_MainTex", Textures[i]);
		
		MinimapSectorMaterials[i].mainTexture = MinimapTextures[i];

		GraphicMeshObjects[i].GetComponent("GMeshControl").MinimapMeshObj.renderer.material = MinimapSectorMaterials[i];

//		GraphicMeshObjects[i].GetComponent("GMeshControl").Textures[0] = Textures[i];
	}
	var StartPoint : int;
	// generate triangles
	Triangles.Clear();
	for (y=0; y<MeshY-1; y++) {
		for (x=0; x<(MeshX-1)*2; x++) {
			if (y%2==0) {// if y even
				StartPoint = (y*(MeshX)) + ((x+1)/2);
				if (x%2==0) {// if x even
					Triangles.Add(StartPoint);
					Triangles.Add(StartPoint+1);
					Triangles.Add(StartPoint+MeshX);
				}
				if (x%2!=0) {// if x odd
					Triangles.Add(StartPoint);
					Triangles.Add(StartPoint+MeshX);
					Triangles.Add((StartPoint+MeshX)-1);
				}
			}
			if (y%2!=0) {// if y odd
				StartPoint = (y*(MeshX)) + (x/2);
				if (x%2==0) {// if x even
					Triangles.Add(StartPoint);
					Triangles.Add(StartPoint+MeshX+1);
					Triangles.Add(StartPoint+MeshX);
				}
				if (x%2!=0) {// if x odd
					Triangles.Add(StartPoint);
					Triangles.Add(StartPoint+1);
					Triangles.Add(StartPoint+MeshX+1);
				}
			}
		}
	}

// BUILD GRAPHIC MESHES
	for (i=0; i<Vertices.length; i++) {
		var TempX = i%MeshX;
		var TempY = i/MeshX;
		for (j=0; j<Sectors; j++) {
			IsInSector(TempX, TempY, j, i);
		}
	}

// BUILD GMESH UVS
	var MeshNumber : int = 0;
	for (m=0; m<4; m++) {
		for (row=0; row<33; row++) {
			for (cell=0; cell<33; cell++) {
				var NewU = (((UMatrix[row][cell] * 31)+16) / 2048.0) - 1.0/4096.0;
				var NewV =Mathf.Abs( 1.0 - ((((VMatrix[row] * 63)+16) / 2048.0) - 1.0/4096.0));
				var NewUV = new Vector2( NewU, NewV);
				if (m==0 && row>0 && cell>0) {
					UVSet0.Add(NewUV); }
				if (m==1 && row>0) {
					UVSet1.Add(NewUV); }
				if (m==2 && cell>0) {
					UVSet2.Add(NewUV); }
				if (m==3) {
					UVSet3.Add(NewUV); }
			}
		}
	}

// Set up vectors for hex filling. HexSidePoints[0,4,8,12,16, & 20] are the actual corners
// grab three sides of Trangle #0
	MasterUV[0] = UVSet0[0];
	MasterUV[1] = UVSet0[1];
	MasterUV[2] = UVSet0[32];
// setup offests for all six directions, these are the neighboring verts
	var TempCorners = new Vector2[6];
	TempCorners[0] = MasterUV[1] - MasterUV[2];
	TempCorners[1] = MasterUV[1] - MasterUV[0];
	TempCorners[2] = MasterUV[2] - MasterUV[0];
	TempCorners[3] = MasterUV[2] - MasterUV[1];
	TempCorners[4] = MasterUV[0] - MasterUV[1];
	TempCorners[5] = MasterUV[0] - MasterUV[2];
// build the corners by averaging two corners and the center (since this is an offset, the center is 0)
	HexSidePoints[0] = (TempCorners[5] + TempCorners[0]) / 3.0;
	HexSidePoints[4] = (TempCorners[0] + TempCorners[1]) / 3.0;
	HexSidePoints[8] = (TempCorners[1] + TempCorners[2]) / 3.0;
	HexSidePoints[12] = (TempCorners[2] + TempCorners[3]) / 3.0;
	HexSidePoints[16] = (TempCorners[3] + TempCorners[4]) / 3.0;
	HexSidePoints[20] = (TempCorners[4] + TempCorners[5]) / 3.0;
// fill in the midpoints
	HexSidePoints[2] = (HexSidePoints[0] + HexSidePoints[4]) / 2.0;
	HexSidePoints[6] = (HexSidePoints[4] + HexSidePoints[8]) / 2.0;
	HexSidePoints[10] = (HexSidePoints[8] + HexSidePoints[12]) / 2.0;
	HexSidePoints[14] = (HexSidePoints[12] + HexSidePoints[16]) / 2.0;
	HexSidePoints[18] = (HexSidePoints[16] + HexSidePoints[20]) / 2.0;
	HexSidePoints[22] = (HexSidePoints[20] + HexSidePoints[0]) / 2.0;
// fill in the remainder of points
	for(i=1; i<24; i+=2) {
		if (i==23) {
			HexSidePoints[i] = (HexSidePoints[22] + HexSidePoints[0]) / 2.0;
		} else {
			HexSidePoints[i] = (HexSidePoints[i-1] + HexSidePoints[i+1]) / 2.0;
		}
	}
	yield;
	
	if (IsElevationMap == false) {	
		for (i=0; i<Sectors; i++) {
			GraphicMeshes[i] = GraphicMeshObjects[i].GetComponent(MeshFilter).mesh;
			GraphicMeshes[i].vertices = GraphicMeshObjects[i].GetComponent("GMeshControl").GVertices;	// assign the verts
			GraphicMeshObjects[i].GetComponent("GMeshControl").MakeTriangles();	// build the triangles
			GraphicMeshObjects[i].GetComponent("GMeshControl").MakeMoveMesh();	// build the Move Meshes
			GraphicMeshObjects[i].GetComponent("GMeshControl").MakeWater();		// build the Water Meshes
			FractalHelper.Subdivide9(GraphicMeshes[i]);							// this line subdivides three times
			MeshHelper.Subdivide(GraphicMeshes[i]);								// fractalize
			GraphicMeshes[i].RecalculateNormals();								// normals
			GraphicMeshObjects[i].renderer.material = SectorMaterials[i];			// assign shaders
		}
	} else {
		for (i=0; i<Sectors; i++) {
			GraphicMeshes[i] = GraphicMeshObjects[i].GetComponent(MeshFilter).mesh;
			GraphicMeshes[i].vertices = GraphicMeshObjects[i].GetComponent("GMeshControl").GVertices;	// assign the verts
			GraphicMeshObjects[i].GetComponent("GMeshControl").MakeTriangles();	// build the triangles
			GraphicMeshObjects[i].GetComponent("GMeshControl").MakeMoveMesh();	// build the Move Meshes
			GraphicMeshObjects[i].GetComponent("GMeshControl").MakeWater();		// build the Water Meshes
			if (TieredMap) {
				ElevationHelper.Subdivide9(GraphicMeshes[i]);							// this line subdivides three times, keeping centers flat
				ElevationHelper.Subdivide4(GraphicMeshes[i]);							// this line subdivides twice
			} else {
				ElevationHelper.Subdivide4(GraphicMeshes[i]);							// this line subdivides twice
				ElevationHelper.Subdivide4(GraphicMeshes[i]);							// this line subdivides twice
			}
//			MeshHelper.Subdivide(GraphicMeshes[i]);								// fractalize
			GraphicMeshes[i].RecalculateNormals();								// normals
			GraphicMeshObjects[i].renderer.material = SectorMaterials[i];			// assign shaders
			
		}
	}
	
	FeatureOffset[0] = Vector3(TriWidth/2.0,		0.0,	0.0-TriHeight);
	FeatureOffset[1] = Vector3(TriWidth,			0.0,	0.0);
	FeatureOffset[2] = Vector3(TriWidth/2.0,		0.0,	TriHeight);
	FeatureOffset[3] = Vector3(0.0-(TriWidth/2.0),	0.0,	TriHeight);
	FeatureOffset[4] = Vector3(0.0-TriWidth,		0.0,	0.0);
	FeatureOffset[5] = Vector3(0.0-(TriWidth/2.0),	0.0,	0.0-TriHeight);

	for (i=0; i<Sectors; i++) {
		GraphicMeshObjects[i].GetComponent("GMeshControl").StitchEdges();	// stitch the mesh edges
	}
	for (i=0; i<Sectors; i++) {
		GUICount = i+1;
			if (BuildMaps == true) {
				GraphicMeshObjects[i].GetComponent("GMeshControl").PaintHexes();	// paint hexes
			}
			if (IsElevationMap == true) {
				GraphicMeshObjects[i].GetComponent("GMeshControl").PopulateHexes();	// populate hexes
			}
	}
	for (i=0; i<Sectors; i++) {
		if (BuildMaps == true) {
			GraphicMeshObjects[i].GetComponent("GMeshControl").PaintHexRivers();	// paint rivers
		}
	}
	for (i=0; i<Sectors; i++) {
		if (BuildMaps == true) {
			GraphicMeshObjects[i].GetComponent("GMeshControl").PaintHexRoads();	// paint roads (uses same code as rivers, but has to be done in a separate, later pass
		}
	}
	if (BuildMaps == true) {
		UpdateMaps();
		if (SaveMaps == true) {
			SaveTextures();
		}
	}
}



function SaveTextures() {
	for (n=0; n<Sectors; n++) {
		var tex = GraphicMeshObjects[n].renderer.material.GetTexture("_MainTex");
		var byt = tex.EncodeToPNG();
		var fileName : String = "Assets/Resources/MapFiles/" + MapName + "-s" + n.ToString() + ".png";
		System.IO.File.WriteAllBytes(fileName, byt);
		
		tex = GraphicMeshObjects[n].GetComponent("GMeshControl").MinimapMeshObj.renderer.material.GetTexture("_MainTex");
		byt = tex.EncodeToPNG();
		fileName = "Assets/Resources/MapFiles/" + MapName + "-m" + n.ToString() + ".png";
		System.IO.File.WriteAllBytes(fileName, byt);
	}
}

function IsInSector(X : int, Y : int, sector : int, vertex : int) {
	var GMesh : GameObject = GraphicMeshObjects[sector];
	var SecX : int = sector%SectorsX;
	var SecY : int = sector/SectorsX;
	var Top : int = SectorBorders[SecX, SecY].y;
	var Left : int = SectorBorders[SecX, SecY].x;
	var Bottom : int = SectorBorders[SecX+1, SecY+1].y;
	var Right : int = SectorBorders[SecX+1, SecY+1].x;
	
	if ((X>=Left && X<=Right) && (Y>=Top && Y<=Bottom)) {
		GraphicMeshObjects[sector].GetComponent("GMeshControl").GVertices.Add(Vertices[vertex]);
	}
}

function UpdateMaps() {
	for (i=0; i<Sectors; i++) {
		GraphicMeshObjects[i].renderer.material.GetTexture("_MainTex").Apply();
		MinimapSectorMaterials[i].mainTexture.Apply();
	}
}

function QueryTerrainType(QX : int, QZ : int) {
	var TQT : int = TypeArray[QX, QZ];
	return TQT;
}

function QueryHeight(QX : int, QZ : int) {
	var TQH : int = HeightArray[QX, QZ];
	return TQH;
}

function QueryRiver(QX : int, QZ : int) {
	var RQT : int = RiverArray[QX, QZ];
	return RQT;
}

function QueryRoad(QX : int, QZ : int) {
	var RQT : int = RoadArray[QX, QZ];
	return RQT;
}

function PointUV (tex : Texture2D, point : Vector2, col : Color, trans : float) {
	var px : int = point.x * tex.width;
	var py : int = point.y * tex.height;
	
	var OldColor : Color = tex.GetPixel(px, py);
	
	tex.SetPixel(px, py, (col*trans)+(OldColor*(1.0-trans)));
}

function RandomFlatSphere () {
	var SphereRand = Random.insideUnitSphere;
	var FlatSphRand = new Vector2(SphereRand.x, SphereRand.z);
	return FlatSphRand;
}

function LineUV (tex : Texture2D, p1 : Vector2, p2 : Vector2, col : Color) {
	var x0 : int = p1.x * tex.width;
	var x1 : int = p2.x * tex.width;
	var y0 : int = p1.y * tex.height;
	var y1 : int = p2.y * tex.height;
    var dy = y1-y0;
    var dx = x1-x0;
   
    if (dy < 0) {dy = -dy; var stepy = -1;}
    else {stepy = 1;}
    if (dx < 0) {dx = -dx; var stepx = -1;}
    else {stepx = 1;}
    dy <<= 1;
    dx <<= 1;
   
    tex.SetPixel(x0, y0, col);
    if (dx > dy) {
        var fraction = dy - (dx >> 1);
        while (x0 != x1) {
            if (fraction >= 0) {
                y0 += stepy;
                fraction -= dx;
            }
            x0 += stepx;
            fraction += dy;
            tex.SetPixel(x0, y0, col);
        }
    } else {
        fraction = dx - (dy >> 1);
        while (y0 != y1) {
            if (fraction >= 0) {
                x0 += stepx;
                fraction -= dy;
            }
            y0 += stepy;
            fraction += dx;
            tex.SetPixel(x0, y0, col);
        }
    }
}