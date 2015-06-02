var MasterVertices = new Vector3[1];
var GVertices = new Array();
var GTriangles = new Array();
var UV = new Array();
var MapControl : GameObject;
var VectorOffset = new Vector3[6];
var MapController;
var Sector : int;
var SecX : int;
var SecY : int;
var SectorsX : int;
var SectorsY : int;
var MapX : int;
var MapY : int;
var XOffset : int;
var YOffset : int;
var SectorBorders = new Vector2[1,1];
var TempTypeArray = new Array();
var TypeArray = new int[1,1];
var Left : int;
var Right : int;
var Top : int;
var Bottom : int;
var LeftHex : int;
var RightHex : int;
var TopHex : int;
var BottomHex : int;
var IsRightSide : boolean = false;
var IsBottomSide : boolean = false;
var XSize : int;
var YSize : int;
var REdgeVert1 : Vector3;
var REdgeVert2 : Vector3;
var LEdgeVert1 : Vector3;
var LEdgeVert2 : Vector3;
var BEdgeVert : Vector3;
var IsStitching : boolean = false;
var MoveMeshObj : GameObject;
var Water1MeshObj : GameObject;
var Water2MeshObj : GameObject;
var MinimapMeshObj : GameObject;
var Textures : Texture2D;
var ColorArray = new Color[1];
var MasterMaterial : Material;
var MasterTextures = new Texture2D[3];
var MiniMapIcons = new Texture2D[3];
var BlankIcon : Texture2D;
var HexPattern : Texture2D;
var SnowPattern : Texture2D;
var RiverPath = new Vector2[5];
var AmountMult:float = 1.00;
var Features = new GameObject[5];
var FrameSet : GameObject;
var Frames = new GameObject[6];

function SetUp () {
	MapControl = GameObject.Find("MapControl");
	MapController = MapControl.GetComponent("MapControl");
	MapController.GraphicMeshObjects[Sector] = gameObject;
	MapController.GraphicMeshes[Sector] = gameObject.GetComponent(MeshFilter).mesh;
	SectorsX = MapController.SectorsX;
	SectorsY = MapController.SectorsY;
	SectorBorders = new Vector2[SectorsX+1,SectorsY+1];
	SectorBorders = MapController.SectorBorders;
	SecX = Sector % SectorsX;
	SecY = Sector / SectorsX;
// move edge frames into place
	FrameSet.transform.position = new Vector3( SecX * 16.0 , 0.0 , SecY * -13.6 );
// move right edge in
	if (SecX == SectorsX-1){
		IsRightSide = true;
		Frames[3].transform.Translate(-0.5, 0.0, 0.0);
	}
// move bottom edge up
	if (SecY == SectorsY-1) {
		IsBottomSide = true;
		Frames[2].transform.Translate(-0.25, 0.0, 0.425);
	}
// hide right edge when needed
	if (SecX < SectorsX-1 && SectorsX > 1) {
		Frames[3].SetActive(false);
	} 
// hide left edge when needed
	if (SecX > 0 && SectorsX > 1) {
		Frames[1].SetActive(false);
	} 
// hide bottom edge when needed
	if (SecY < SectorsY-1 && SectorsY > 1) {
		Frames[2].SetActive(false);
	} 
// hide top edge when needed
	if (SecY > 0 && SectorsY > 1) {
		Frames[0].SetActive(false);
	} 
	LeftHex = 0;
	TopHex = 0;
	RightHex = 32;
	BottomHex = 32;
	if (SecX == 0){
		LeftHex = 0;
		RightHex = 31;
	}
	if (SecY == 0){
		TopHex = 0;
		BottomHex = 31;
	}
	Left = SectorBorders[SecX, SecY].x;
	Right = SectorBorders[SecX+1, SecY].x;
	Top = SectorBorders[SecX, SecY].y;
	Bottom = SectorBorders[SecX, SecY+1].y;
	XSize = (Right - Left);
	YSize = (Bottom - Top);
	XOffset = (32 * SecX)-1;
	YOffset = (32 * SecY)-1;
	if (XOffset<0)
		XOffset=0;
	if (YOffset<0)
		YOffset=0;
	MapX = MapController.MeshX;
	MapY = MapController.MeshY;
	TypeArray = new int[SectorsX*32, SectorsY*32];
	for (y=0; y<SectorsY*32; y++) {
		for (x=0; x<SectorsX*32; x++) {
			TypeArray[x,y] = TempTypeArray[(y*MapX) + x];
		}
	}
}

function DrawMiniMapIcon(Center:Vector2, icon:int) {
	var OriginX = Center.x * MinimapMeshObj.GetComponent(MeshRenderer).material.mainTexture.width-31;
	var OriginY = Center.y * MinimapMeshObj.GetComponent(MeshRenderer).material.mainTexture.height-42;
	if (icon==39) {
		icon=8;
	}
	for (var iconY=0; iconY<BlankIcon.height; iconY++) {
		for (var iconX=0; iconX<BlankIcon.width; iconX++) {
			var pixelColor : Color = MiniMapIcons[icon].GetPixel(iconX, iconY);
			if (pixelColor.a != 0.0) {
				MinimapMeshObj.GetComponent(MeshRenderer).material.mainTexture.SetPixel(OriginX+iconX, OriginY+iconY, pixelColor);
			}
		}	
	}
}

function DrawMiniMapEdge(Center:Vector2) {
	var OriginX = Center.x * MinimapMeshObj.GetComponent(MeshRenderer).material.mainTexture.width-31;
	var OriginY = Center.y * MinimapMeshObj.GetComponent(MeshRenderer).material.mainTexture.height-42;
	for (var iconY=0; iconY<BlankIcon.height; iconY++) {
		for (var iconX=0; iconX<BlankIcon.width; iconX++) {
			var pixelColor : Color = BlankIcon.GetPixel(iconX, iconY);
			if (pixelColor.a != 0.0) {
				MinimapMeshObj.GetComponent(MeshRenderer).material.mainTexture.SetPixel(OriginX+iconX, OriginY+iconY, pixelColor);
			}
		}	
	}
}

function HexSplat(Center:Vector2, paintRadius:float, amount:int, paintTex:int, transbot:float, transtop:float, tight:boolean) {
	var MapTexture : Texture2D = gameObject.GetComponent(MeshRenderer).material.mainTexture;
	for (i=0; i<amount; i++) {
		var trans : float = Random.Range(transbot, transtop);
		var CircleOffset = (Random.insideUnitSphere + Random.insideUnitSphere)/2.0;
		if(tight==true) {
			var tempCircle = Random.insideUnitCircle;
			CircleOffset = new Vector3(tempCircle.x, 0.0, tempCircle.y);
		}
		CircleOffset.y = 0.0;
		var RandomTable : int = Random.Range(0, 36);
		var ExtraOffset : Vector2;
		ExtraOffset = new Vector2(0, 0);
		if (RandomTable < 24) {
			ExtraOffset = new Vector2(MapController.HexSidePoints[RandomTable].x * 0.8, 
										MapController.HexSidePoints[RandomTable].y * 0.8);
			var Offset = (new Vector2(CircleOffset.x, CircleOffset.z) * 0.012) + ExtraOffset;
		}
		if (RandomTable >= 24) {
			Offset = (new Vector2(CircleOffset.x, CircleOffset.z) * 0.017);
		}
		var TempCenter = Center + Offset;
		var CenterX : int = TempCenter.x * MapTexture.width;
		var CenterY : int = TempCenter.y * MapTexture.height;
		var RandomColor : Color = ColorArray[Random.Range(0,ColorArray.length-1)];
		var OldColor : Color = MapTexture.GetPixel(CenterX, CenterY);
		var NewColor : Color = (RandomColor*trans) + (OldColor*(1.0-trans));
		if (CenterY >= 0 && CenterY < (MapTexture.height) && CenterX >= 0 && CenterX < MapTexture.width) {
			MapTexture.SetPixel(CenterX, CenterY, NewColor);
		}
	}
}

function DrawHexFeatures(x_coord:int, y_coord:int, hex_location:Vector3, type:int) {
	var feature : GameObject;
	var feature_scale : Vector3;
	var feature_sphere : Vector3;
	SetupOffsetVectors(x_coord, y_coord);
	var random : float;
	var scale : float;
	if (type==10) {
		for (i=0; i<19; i++) {
			if (Random.Range(0.0, 1.0) < 0.1) {
				feature_sphere = (Random.insideUnitSphere*.05);
				feature_sphere.y /= 3.0;
				random = Random.Range(0.0,1.0);
				if (random < 0.05) {
					feature = Instantiate(MapController.Features[1], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				}
				if (random > 0.05 && random < 0.15) {
					feature = Instantiate(MapController.Features[0], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				}
				if (random > 0.15 && random < 0.175) {
					feature = Instantiate(MapController.Features[2], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				}
				if (feature!=null) {
					scale = Random.Range(0.2, 0.3);
					feature_scale = new Vector3(scale, scale * Random.Range(0.8, 1.2), scale );
					feature.transform.localScale = feature_scale;
					feature.transform.Rotate(Random.Range(-5.0, 5.0), Random.Range(-45.0, 45.0), Random.Range(-5.0, 5.0));
					feature.transform.parent = MapController.MapHolder.transform;
				}
			}
		}
	}
	if (type==7) {
		for (i=0; i<19; i++) {
			if (Random.Range(0.0, 1.0) < 0.25) {
				feature_sphere = (Random.insideUnitSphere*.05);
				feature_sphere.y /= 3.0;
				random = Random.Range(0.0,1.0);
				if (random < 0.3) {
					feature = Instantiate(MapController.Features[1], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				}
				if (random > 0.3 && random < 0.35) {
					feature = Instantiate(MapController.Features[3], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				}
				if (random > 0.35 && random < 0.4) {
					feature = Instantiate(MapController.Features[4], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				}
				if (feature!=null) {
					scale = Random.Range(0.2, 0.3);
					feature_scale = new Vector3(scale, scale * Random.Range(0.8, 1.2), scale );
					feature.transform.localScale = feature_scale;
					feature.transform.Rotate(Random.Range(-5.0, 5.0), Random.Range(-45.0, 45.0), Random.Range(-5.0, 5.0));
					feature.transform.parent = MapController.MapHolder.transform;
				}
			}
		}
	}
	if (type==11) {
		for (i=0; i<19; i++) {
			if (Random.Range(0.0, 1.0) < 0.5) {
				feature_sphere = (Random.insideUnitSphere*.05);
				feature_sphere.y /= 3.0;
				random = Random.Range(0.0,1.0);
				if (random < 0.3) {
					feature = Instantiate(MapController.Features[1], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				}
				if (random > 0.3 && random < 0.35) {
					feature = Instantiate(MapController.Features[0], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				}
				if (random > 0.35 && random < 0.4) {
					feature = Instantiate(MapController.Features[2], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				}
				if (feature!=null) {
					scale = Random.Range(0.2, 0.3);
					feature_scale = new Vector3(scale, scale * Random.Range(0.8, 1.2), scale );
					feature.transform.localScale = feature_scale;
					feature.transform.Rotate(Random.Range(-5.0, 5.0), Random.Range(-45.0, 45.0), Random.Range(-5.0, 5.0));
					feature.transform.parent = MapController.MapHolder.transform;
				}
			}
		}
	}
	if (type==39) {
		for (i=0; i<19; i++) {
			if (Random.Range(0.0, 1.0) < 0.1) {
				feature_sphere = (Random.insideUnitSphere*.05);
				feature_sphere.y /= 3.0;
				random = Random.Range(0.0,1.0);
				if (random < 0.05) {
					feature = Instantiate(MapController.Features[1], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				}
				if (random > 0.05 && random < 0.15) {
					feature = Instantiate(MapController.Features[0], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				}
				if (random > 0.15 && random < 0.2) {
					feature = Instantiate(MapController.Features[2], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				}
				if (feature!=null) {
					scale = Random.Range(0.2, 0.3);
					feature_scale = new Vector3(scale, scale * Random.Range(0.8, 1.2), scale );
					feature.transform.localScale = feature_scale;
					feature.transform.Rotate(Random.Range(-5.0, 5.0), Random.Range(-45.0, 45.0), Random.Range(-5.0, 5.0));
					feature.transform.parent = MapController.MapHolder.transform;
				}
			}
		}
	}
	if (type==12) {
		for (i=0; i<19; i++) {
			if (Random.Range(0.0, 1.0) < 0.85) {
				feature_sphere = (Random.insideUnitSphere*.05);
				feature_sphere.y /= 3.0;
				feature = Instantiate(MapController.Features[0], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				feature_scale = new Vector3(Random.Range(0.75, 1.0), Random.Range(0.5, 0.75), Random.Range(0.75, 1.0) );
				feature.transform.localScale = feature_scale;
				feature.transform.Rotate(Random.Range(-20.0, 20.0), Random.Range(-180.0, 180.0), Random.Range(-20.0, 20.0));
				feature.transform.parent = MapController.MapHolder.transform;
			}
		}
	}
	if (type==18) {
		for (i=0; i<19; i++) {
			if (Random.Range(0.0, 1.0) < 0.85) {
				feature_sphere = (Random.insideUnitSphere*.05);
				feature_sphere.y /= 3.0;
				if (Random.Range(0.0,1.0)<0.5) {
					feature = Instantiate(MapController.Features[3], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				} else {
					feature = Instantiate(MapController.Features[4], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				}
				scale = Random.Range(0.2, 0.3);
				feature_scale = new Vector3(scale, scale * Random.Range(0.8, 1.2), scale );
				feature.transform.localScale = feature_scale;
				feature.transform.Rotate(Random.Range(-5.0, 5.0), Random.Range(-45.0, 45.0), Random.Range(-5.0, 5.0));
				feature.transform.parent = MapController.MapHolder.transform;
			}
		}
	}
	if (type==17) {
		for (i=0; i<19; i++) {
			if (Random.Range(0.0, 1.0) < 0.5) {
				feature_sphere = (Random.insideUnitSphere*.05);
				feature_sphere.y /= 3.0;
				if (Random.Range(0.0,1.0)<0.5) {
					feature = Instantiate(MapController.Features[3], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				} else {
					feature = Instantiate(MapController.Features[4], hex_location+(VectorOffset[i])+feature_sphere, Quaternion.identity);
				}
				scale = Random.Range(0.1, 0.25);
				feature_scale = new Vector3(scale, scale * Random.Range(0.8, 1.2), scale );
				feature.transform.localScale = feature_scale;
				feature.transform.Rotate(Random.Range(-5.0, 5.0), Random.Range(-45.0, 45.0), Random.Range(-5.0, 5.0));
				feature.transform.parent = MapController.MapHolder.transform;
			}
		}
	}
}

// alters the base vectors that are used to place objects in the hex based on the neighbors
function SetupOffsetVectors(x_coord:int, y_coord:int) {
	var BaseVectors = new Vector3[6];
	BaseVectors = MapController.FeatureOffset;
	var BaseHeight = MapController.QueryHeight(x_coord, y_coord);
	for (i=0; i<6; i++) {
		var neighbor_hex : Vector2 = Neighbor(x_coord, y_coord, i);
		var neighbor_x : int = neighbor_hex.x;
		var neighbor_y : int = neighbor_hex.y;
		if(neighbor_x != -1) {
			var neighbor_z : int = MapController.QueryHeight(neighbor_x, neighbor_y);
			var z_diff : int = neighbor_z - BaseHeight;
			BaseVectors[i].y = 0.1 * z_diff;
		}
	}
	VectorOffset[0] = Vector3.zero;
	
	VectorOffset[1] = ((BaseVectors[5] + BaseVectors[0])/8.0);
	VectorOffset[2] = ((BaseVectors[0] + BaseVectors[1])/8.0);
	VectorOffset[3] = ((BaseVectors[1] + BaseVectors[2])/8.0);
	VectorOffset[4] = ((BaseVectors[2] + BaseVectors[3])/8.0);
	VectorOffset[5] = ((BaseVectors[3] + BaseVectors[4])/8.0);
	VectorOffset[6] = ((BaseVectors[4] + BaseVectors[5])/8.0);
	
	VectorOffset[7] = ((BaseVectors[5] + BaseVectors[0])/4.0);
	VectorOffset[8] = ((BaseVectors[0] + BaseVectors[1])/4.0);
	VectorOffset[9] = ((BaseVectors[1] + BaseVectors[2])/4.0);
	VectorOffset[10] = ((BaseVectors[2] + BaseVectors[3])/4.0);
	VectorOffset[11] = ((BaseVectors[3] + BaseVectors[4])/4.0);
	VectorOffset[12] = ((BaseVectors[4] + BaseVectors[5])/4.0);
	
	VectorOffset[13] = (BaseVectors[0]/3.0);
	VectorOffset[14] = (BaseVectors[1]/3.0);
	VectorOffset[15] = (BaseVectors[2]/3.0);
	VectorOffset[16] = (BaseVectors[3]/3.0);
	VectorOffset[17] = (BaseVectors[4]/3.0);
	VectorOffset[18] = (BaseVectors[5]/3.0);
// bring the middle points back in line with the middle if tiered
	if (MapController.TieredMap) {
		VectorOffset[1].y = 0.0;
		VectorOffset[2].y = 0.0;
		VectorOffset[3].y = 0.0;
		VectorOffset[4].y = 0.0;
		VectorOffset[5].y = 0.0;
		VectorOffset[6].y = 0.0;
	}
}

// finds the neighbor of a given hex in a given direction; NE = 0; goes clockwise
function Neighbor(x_coord:int, y_coord:int, direction:int) {
	var neighbor_coord:Vector2;
	var new_x : int;
	var new_y : int;
	if (y_coord%2==0) {//y is even
		if (direction==0) {
			new_x = x_coord;
			new_y = y_coord-1;
		}
		if (direction==1) {
			new_x = x_coord+1;
			new_y = y_coord;
		}
		if (direction==2) {
			new_x = x_coord;
			new_y = y_coord+1;
		}
		if (direction==3) {
			new_x = x_coord-1;
			new_y = y_coord+1;
		}
		if (direction==4) {
			new_x = x_coord-1;
			new_y = y_coord;
		}
		if (direction==5) {
			new_x = x_coord-1;
			new_y = y_coord-1;
		}
	}
	if (y_coord%2==1) {//y is odd
		if (direction==0) {
			new_x = x_coord+1;
			new_y = y_coord-1;
		}
		if (direction==1) {
			new_x = x_coord+1;
			new_y = y_coord;
		}
		if (direction==2) {
			new_x = x_coord+1;
			new_y = y_coord+1;
		}
		if (direction==3) {
			new_x = x_coord;
			new_y = y_coord+1;
		}
		if (direction==4) {
			new_x = x_coord-1;
			new_y = y_coord;
		}
		if (direction==5) {
			new_x = x_coord;
			new_y = y_coord-1;
		}
	}
	if (new_x==0 || new_x>=MapController.MeshX || new_y==0 || new_y>=MapController.MeshY ) {
		new_x = -1;
		new_y = -1;
	}
	neighbor_coord = new Vector2(new_x, new_y);
	return neighbor_coord;
}

// draws the textures on the map
function DrawMapHexagon(Center:Vector2, type:int) {
	var MapTexture : Texture2D = gameObject.GetComponent(MeshRenderer).material.mainTexture;
	var OriginX = Center.x * MapTexture.width-35;
	var OriginY = Center.y * MapTexture.height-48;
	for (var iconY=0; iconY<HexPattern.height; iconY++) {
		for (var iconX=0; iconX<HexPattern.width; iconX++) {
			var testColor : Color = HexPattern.GetPixel(iconX, iconY);
			if (UnityEngine.Random.Range(0.0, 1.0) < testColor.r) {
				
				var pixelColorNumber : int = UnityEngine.Random.Range(0, 7);
				// EDGE
				if (type == -1) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, Color.black);
				}
				// OCEAN
				if (type == 0) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.DesertColors[pixelColorNumber]*0.5));
				}
				// SEA
				if (type == 1) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.DesertColors[pixelColorNumber]*0.5));
				}
				// MARSH
				if (type == 2) {
					var percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.25 && percent <= 0.5) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.5) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.5));
					}
				}
				// PLAINS
				if (type == 3) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 0.7) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.65));
					}
					if (percent > 0.7 && percent <= 1.0) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.5));
					}
				}
				// HILLS
				if (type == 4) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 0.7) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.65));
					}
					if (percent > 0.7 && percent <= 1.0) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.5));
					}
				}
				// MOUNTAINS
				if (type == 5) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.StoneColors[pixelColorNumber]*0.5));
				}
				// MOUNTAIN SUMMIT
				if (type == 6) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.StoneColors[pixelColorNumber]*0.5));
				}
				// SWAMP
				if (type == 7) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.1 && percent <= 0.7) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.7) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.5));
					}
				}
				// BADLAND
				if (type == 8) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.2 && percent <= 0.8) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.DesertColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.8) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.StoneColors[pixelColorNumber]*0.6));
					}
				}
				// ARID
				if (type == 9) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.2 && percent <= 0.8) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.DesertColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.8) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.5));
					}
				}
				// DESERT
				if (type == 10) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.DesertColors[pixelColorNumber]*0.5));
				}
				// GRASS
				if (type == 11) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 0.7) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.65));
					}
					if (percent > 0.7 && percent <= 1.0) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.5));
					}
				}
				// ASH
				if (type == 12) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.StoneColors[pixelColorNumber]*0.5));
				}
				// BURNT FOREST
				if (type == 13) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.StoneColors[pixelColorNumber]*0.5));
				}
				// Tundra
				if (type == 14) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.1 && percent <= 0.9) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.SnowColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.9) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.35));
					}
				}
				// SNOW
				if (type == 15) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 1.0) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.SnowColors[pixelColorNumber]*0.5));
					}
				}
				// SCRUB
				if (type == 16) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 0.5) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.65));
					}
					if (percent > 0.5 && percent <= 0.75) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.75 && percent <= 1.0) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.35));
					}
				}
				// FOREST
				if (type == 17) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.1 && percent <= 0.7) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.7) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.35));
					}
				}
				// DENSE FOREST
				if (type == 18) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 0.8) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.8) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.35));
					}
				}
				// JUNGLE
				if (type == 19) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 0.3) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.3 && percent <= 1.0) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.35));
					}
				}
				// Glacier
				if (type == 20) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 0.1) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.RiverColors[pixelColorNumber]));
					}
					if (percent > 0.1 && percent <= 1.0) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.SnowColors[pixelColorNumber]*0.5));
					}
				}
				// LAVA
				if (type == 21) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.LavaColors[pixelColorNumber]*0.5));
				}
				// BROKEN LAND
				if (type == 22) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.2 && percent <= 0.8) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.DesertColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.8) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.StoneColors[pixelColorNumber]*0.6));
					}
				}
				// ARID HILLS
				if (type == 23) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.2 && percent <= 0.8) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.DesertColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.8) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.5));
					}
				}
				// DESERT HILLS
				if (type == 24) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.DesertColors[pixelColorNumber]*0.5));
				}
				// GRASS HILLS
				if (type == 25) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 0.7) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.65));
					}
					if (percent > 0.7 && percent <= 1.0) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.5));
					}
				}
				// ASH HILLS
				if (type == 26) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.StoneColors[pixelColorNumber]*0.5));
				}
				// BURNT FOREST HILLS
				if (type == 27) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.StoneColors[pixelColorNumber]*0.5));
				}
				// TUNDRA HILLS
				if (type == 28) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.1 && percent <= 0.9) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.SnowColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.9) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.35));
					}
				}
				// SNOW HILLS
				if (type == 29) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 1.0) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.SnowColors[pixelColorNumber]*0.5));
					}
				}
				// SCRUB HILLS
				if (type == 30) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 0.5) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.65));
					}
					if (percent > 0.5 && percent <= 0.75) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.75 && percent <= 1.0) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.35));
					}
				}
				// FOREST HILLS
				if (type == 31) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.1 && percent <= 0.7) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.7) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.GrassColors[pixelColorNumber]*0.35));
					}
				}
				// DENSE FOREST HILLS
				if (type == 32) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 0.8) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.8) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.35));
					}
				}
				// JUNGLE HILLS
				if (type == 33) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 0.3) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.5));
					}
					if (percent > 0.4 && percent <= 1.0) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.ForestColors[pixelColorNumber]*0.35));
					}
				}
				// ROCKY MOUNTAINS
				if (type == 34) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.StoneColors[pixelColorNumber]*0.5));
				}
				// SNOWY MOUNTAINS
				if (type == 35) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 1.0) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.SnowColors[pixelColorNumber]*0.5));
					}
				}
				// ROCKY PEAK
				if (type == 36) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.StoneColors[pixelColorNumber]*0.5));
				}
				// SNOWY SUMMIT
				if (type == 37) {
					percent = UnityEngine.Random.Range(0.0, 1.0);
					if (percent > 0.025 && percent <= 1.0) {
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.SnowColors[pixelColorNumber]*0.5));
					}
				}
				// VOLCANO
				if (type == 38) {
					MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, (MapController.StoneColors[pixelColorNumber]*0.5));
				}
			}
		}
	}
}

// second pass for snow caps
function DrawMapHexExtras(Center:Vector2, type:int) {
	if (type==5 || type==6 || type==35 || type==37) {
		var MapTexture : Texture2D = gameObject.GetComponent(MeshRenderer).material.mainTexture;
		var OriginX = Center.x * MapTexture.width-35;
		var OriginY = Center.y * MapTexture.height-48;
		for (var iconY=0; iconY<SnowPattern.height; iconY++) {
			for (var iconX=0; iconX<SnowPattern.width; iconX++) {
				for (var i=0; i<10; i++) {
					var testColor : Color = SnowPattern.GetPixel(iconX, iconY);
					if (UnityEngine.Random.Range(0.0, 1.0) < testColor.r) {
						var TempColor = MapTexture.GetPixel(OriginX+iconX, OriginY+iconY);
						var NewColor = (TempColor * 0.9) + (new Color(0.6, 0.6, 0.65) * 0.1);
						MapTexture.SetPixel(OriginX+iconX, OriginY+iconY, NewColor);
					}
				}
			}
		}
	}			
}

// cycle through map and draw extra features (trees, rocks, etc.) on map
function PopulateHexes() {
	for (y=TopHex; y<=BottomHex; y++) {
		for (x=LeftHex; x<=RightHex; x++) {
			var WorldCoordX : int = x+XOffset;
			var WorldCoordY : int = y+YOffset;
			var TerrainType : int = TypeArray[WorldCoordX, WorldCoordY];
			var SectorVert : int = x + (y * (XSize+1));
			TerrainType = TypeArray[WorldCoordX, WorldCoordY];
			
			if (y>TopHex && x>LeftHex) {
				var featureLocation : Vector3 = GVertices[SectorVert];
				DrawHexFeatures(x, y, featureLocation, TerrainType);
			}
		}
	}
}
			
function PaintHexes() {
	for (y=TopHex; y<=BottomHex; y++) {
		for (x=LeftHex; x<=RightHex; x++) {
			var WorldCoordX : int = x+XOffset;
			var WorldCoordY : int = y+YOffset;
			var TerrainType : int = TypeArray[WorldCoordX, WorldCoordY];
			var RiverType : int = MapController.QueryRiver(WorldCoordX, WorldCoordY);
			var SectorVert : int = x + (y * (XSize+1));
			var PaintUV = UV[SectorVert];
			TerrainType = TypeArray[WorldCoordX, WorldCoordY];

			var tempLineColor : Color;
			var tempLineScale : float;
			
			if (TerrainType >= 0) {
				DrawMiniMapIcon(PaintUV, TerrainType);
			} else {
				DrawMiniMapEdge(PaintUV);
			}
			DrawMapHexagon(PaintUV, TerrainType);
			DrawMapHexExtras(PaintUV, TerrainType);
		}
	}
}

function PaintHexRivers () {
	for (y=TopHex; y<=BottomHex; y++) {
		for (x=LeftHex; x<=RightHex; x++) {
			WorldCoordX = x+XOffset;
			WorldCoordY = y+YOffset;
			CarryRight = false;
			CarryDown = false;
			if (x==RightHex && !IsRightSide)
				CarryRight = true;
			if (y==BottomHex && !IsBottomSide)
				CarryDown = true;
			TerrainType = TypeArray[WorldCoordX, WorldCoordY];
			RiverType = MapController.QueryRiver(WorldCoordX, WorldCoordY);
			SectorVert = x + (y * (XSize+1));
			PaintUV = UV[SectorVert];
			TerrainType = TypeArray[WorldCoordX, WorldCoordY];
// Rivers
			if (RiverType!=0) {
				if (RiverType >= 256) {
					// Big River Lower Right
					RiverStart = PaintUV + MapController.HexSidePoints[8];
					RiverEnd = PaintUV + MapController.HexSidePoints[12];
					DrawRiver(RiverStart, RiverEnd, 3, CarryDown, CarryRight);
					RiverType -= 256;
				}
				if (RiverType >= 128) {
					// Big River Right
					RiverStart = PaintUV + MapController.HexSidePoints[4];
					RiverEnd = PaintUV + MapController.HexSidePoints[8];
					DrawRiver(RiverStart, RiverEnd, 3, CarryDown, CarryRight);
					RiverType -= 128;
				}
				if (RiverType >= 64) {
					// Big River Upper Right
					RiverStart = PaintUV + MapController.HexSidePoints[0];
					RiverEnd = PaintUV + MapController.HexSidePoints[4];
					DrawRiver(RiverStart, RiverEnd, 3, CarryDown, CarryRight);
					RiverType -= 64;
				}
				if (RiverType >= 32) {
					// Med River Lower Right
					RiverStart = PaintUV + MapController.HexSidePoints[8];
					RiverEnd = PaintUV + MapController.HexSidePoints[12];
					DrawRiver(RiverStart, RiverEnd, 2, CarryDown, CarryRight);
					RiverType -= 32;
				}
				if (RiverType >= 16) {
					// Med River Right
					RiverStart = PaintUV + MapController.HexSidePoints[4];
					RiverEnd = PaintUV + MapController.HexSidePoints[8];
					DrawRiver(RiverStart, RiverEnd, 2, CarryDown, CarryRight);
					RiverType -= 16;
				}
				if (RiverType >= 8) {
					// Med River Upper Right
					RiverStart = PaintUV + MapController.HexSidePoints[0];
					RiverEnd = PaintUV + MapController.HexSidePoints[4];
					DrawRiver(RiverStart, RiverEnd, 2, CarryDown, CarryRight);
					RiverType -= 8;
				}
				if (RiverType >= 4) {
					// Small River Lower Right
					RiverStart = PaintUV + MapController.HexSidePoints[8];
					RiverEnd = PaintUV + MapController.HexSidePoints[12];
					DrawRiver(RiverStart, RiverEnd, 1, CarryDown, CarryRight);
					RiverType -= 4;
				}
				if (RiverType >= 2) {
					// Small River Right
					RiverStart = PaintUV + MapController.HexSidePoints[4];
					RiverEnd = PaintUV + MapController.HexSidePoints[8];
					DrawRiver(RiverStart, RiverEnd, 1, CarryDown, CarryRight);
					RiverType -= 2;
				}
				if (RiverType >= 1) {
					// Small River Upper Right
					RiverStart = PaintUV + MapController.HexSidePoints[0];
					RiverEnd = PaintUV + MapController.HexSidePoints[4];
					DrawRiver(RiverStart, RiverEnd, 1, CarryDown, CarryRight);
					RiverType -= 1;
				}
			}
		}
	}
}

// draws rivers and roads on whole map
function PaintHexRoads () {
	for (y=TopHex; y<=BottomHex; y++) {
		for (x=LeftHex; x<=RightHex; x++) {
			WorldCoordX = x+XOffset;
			WorldCoordY = y+YOffset;
			CarryRight = false;
			CarryDown = false;
			if (x==RightHex && !IsRightSide)
				CarryRight = true;
			if (y==BottomHex && !IsBottomSide)
				CarryDown = true;
			TerrainType = TypeArray[WorldCoordX, WorldCoordY];
			RoadType = MapController.QueryRoad(WorldCoordX, WorldCoordY);
			SectorVert = x + (y * (XSize+1));
			PaintUV = UV[SectorVert];
			TerrainType = TypeArray[WorldCoordX, WorldCoordY];
// Roads
			if (RoadType!=0) {
				if (RoadType >= 32) {
					// Road Up
					var RoadStart = PaintUV;
					var RoadEnd = PaintUV + MapController.HexSidePoints[22];
					DrawRiver(RoadStart, RoadEnd, 0, CarryDown, CarryRight);
					RoadType -= 32;
				}
				if (RoadType >= 16) {
					// Road Up Right
					RoadStart = PaintUV;
					RoadEnd = PaintUV + MapController.HexSidePoints[18];
					DrawRiver(RoadStart, RoadEnd, 0, CarryDown, CarryRight);
					RoadType -= 16;
				}
				if (RoadType >= 8) {
					// Road Down Right
					RoadStart = PaintUV;
					RoadEnd = PaintUV + MapController.HexSidePoints[14];
					DrawRiver(RoadStart, RoadEnd, 0, CarryDown, CarryRight);
					RoadType -= 8;
				}
				if (RoadType >= 4) {
					// Road Down
					RoadStart = PaintUV;
					RoadEnd = PaintUV + MapController.HexSidePoints[10];
					DrawRiver(RoadStart, RoadEnd, 0, CarryDown, CarryRight);
					RoadType -= 4;
				}
				if (RoadType >= 2) {
					// Road Down Left
					RoadStart = PaintUV;
					RoadEnd = PaintUV + MapController.HexSidePoints[6];
					DrawRiver(RoadStart, RoadEnd, 0, CarryDown, CarryRight);
					RoadType -= 2;
				}
				if (RoadType >= 1) {
					// Road Up Left
					RoadStart = PaintUV;
					RoadEnd = PaintUV + MapController.HexSidePoints[2];
					DrawRiver(RoadStart, RoadEnd, 0, CarryDown, CarryRight);
					RoadType -= 1;
				}
			}
		}
	}
}

// draws a point
function DrawPoint (tex:Texture2D, aPoint:Vector3) {
	var x0 : int = aPoint.x * tex.width;
	var y0 : int = aPoint.y * tex.height;
	tex.SetPixel(x0,y0,Color.red);
}

// draws a curve
function DrawCurve (tex:Texture2D, aPoint:Vector3, aPoints:Array, aColor:Color) {
	for (i=0; i<aPoints.length-1; i++) {
		LineUV(tex, aPoints[i]+aPoint, aPoints[i+1]+aPoint, aColor);
	}
}

// function used by DrawCurve
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
   
    if(x0>=0 && x0<=tex.width && y0>=0 && y0<=tex.height) {
	    tex.SetPixel(x0, y0, col);
    } 
    if (dx > dy) {
        var fraction = dy - (dx >> 1);
        while (x0 != x1) {
            if (fraction >= 0) {
                y0 += stepy;
                fraction -= dx;
            }
            x0 += stepx;
            fraction += dy;
		    if(x0>=0 && x0<=tex.width && y0>=0 && y0<=tex.height) {
			    tex.SetPixel(x0, y0, col);
		    } 
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
		    if(x0>=0 && x0<=tex.width && y0>=0 && y0<=tex.height) {
			    tex.SetPixel(x0, y0, col);
		    } 
        }
    }
}

// draws rivers and roads on both map and minimap
function DrawRiver (riverStart:Vector2, riverEnd:Vector2, size:int, mirrorDown:boolean, mirrorRight:boolean) {
	RiverPath = new Vector2[5];
	var RLength : float = ((riverEnd-riverStart).magnitude)/8.0;
	RiverPath[0] = riverStart;
	RiverPath[1] = (((riverStart * 3.0) + riverEnd) / 4.0) + (Random.insideUnitCircle * RLength);
	RiverPath[2] = ((riverStart + riverEnd) / 2.0);// + (Random.insideUnitCircle * RLength);
	RiverPath[3] = ((riverStart + (riverEnd * 3.0)) / 4.0) + (Random.insideUnitCircle * RLength);
	RiverPath[4] = riverEnd;

    if (size == 3) {// major river
		ColorArray = new Color[3];
		for (i=0; i<ColorArray.length; i++) {
			ColorArray[i] = MapController.RiverColors[i+3];
		}
    }
    if (size == 2) {// medium river
		ColorArray = new Color[3];
		for (i=0; i<ColorArray.length; i++) {
			ColorArray[i] = MapController.RiverColors[i+2];
		}
    }
    if (size == 1) {// small river
		ColorArray = new Color[3];
		for (i=0; i<ColorArray.length; i++) {
			ColorArray[i] = MapController.RiverColors[i+1];
		}
    }
    if (size == 0) {// road
		ColorArray = new Color[1];
		for (i=0; i<ColorArray.length; i++) {
			ColorArray[i] = MapController.RoadColor;
		}
    }
	for (i=0; i<4; i++) {
		var MapTexture : Texture2D = gameObject.GetComponent(MeshRenderer).material.mainTexture;
		var x0 : int = RiverPath[i].x * MapTexture.width;
		var x1 : int = RiverPath[i+1].x * MapTexture.width;
		var y0 : int = RiverPath[i].y * MapTexture.height;
		var y1 : int = RiverPath[i+1].y * MapTexture.height;
	    var dy = y1-y0;
	    var dx = x1-x0;
	    if (dy < 0) {dy = -dy; var stepy = -1;}
	    else {stepy = 1;}
	    if (dx < 0) {dx = -dx; var stepx = -1;}
	    else {stepx = 1;}
	    dy <<= 1;
	    dx <<= 1;
	    if (size==0 && x0>=0 && x0<=MapTexture.width && y0>=0 && y0<=MapTexture.height) {
	    	// draws road
	    	TinySplat(x0, y0, 4, 45);
	    	// draws road on minimap
	    	TinyMMSplat(x0, y0, 5, 45);
	    }
	    if (size==1 && x0>=0 && x0<=MapTexture.width && y0>=0 && y0<=MapTexture.height) {
	    	// draws small river
	    	TinySplat(x0, y0, 3, 15);
	    	// draws small river on minimap
	    	TinyMMSplat(x0, y0, 3, 15);
	    }
	    if (size==2 && x0>=0 && x0<=MapTexture.width && y0>=0 && y0<=MapTexture.height) {
	    	// draws medium river
	    	TinySplat(x0, y0, 5, 30);
	    	// draws medium river on minimap
	    	TinyMMSplat(x0, y0, 5, 45);
	    }
	    if (size==3 && x0>=0 && x0<=MapTexture.width && y0>=0 && y0<=MapTexture.height) {
	    	// draws major river
	    	TinySplat(x0, y0, 7, 60);
	    	// draws major river on minimap
	    	TinyMMSplat(x0, y0, 7, 75);
	    }

	    if (dx > dy) {
	        var fraction = dy - (dx >> 1);
	        while (x0 != x1) {
	            if (fraction >= 0) {
	                y0 += stepy;
	                fraction -= dx;
	            }
	            x0 += stepx;
	            fraction += dy;
			    if (size==0 && x0>=0 && x0<=MapTexture.width && y0>=0 && y0<=MapTexture.height) {
			    	TinySplat(x0, y0, 4, 45);
			    	TinyMMRoad(x0, y0, 5, 45);
			    }
			    if (size==1 && x0>=0 && x0<=MapTexture.width && y0>=0 && y0<=MapTexture.height) {
			    	TinySplat(x0, y0, 3, 15);
			    	TinyMMSplat(x0, y0, 3, 15);
			    }
			    if (size==2 && x0>=0 && x0<=MapTexture.width && y0>=0 && y0<=MapTexture.height) {
			    	TinySplat(x0, y0, 5, 30);
			    	TinyMMSplat(x0, y0, 5, 45);
			    }
			    if (size==3 && x0>=0 && x0<=MapTexture.width && y0>=0 && y0<=MapTexture.height) {
			    	TinySplat(x0, y0, 7, 60);
			    	TinyMMSplat(x0, y0, 7, 75);
			    }
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
			    if (size==0 && x0>=0 && x0<=MapTexture.width && y0>=0 && y0<=MapTexture.height) {
			    	TinySplat(x0, y0, 4, 45);
			    	TinyMMRoad(x0, y0, 5, 45);
			    }
			    if (size==1 && x0>=0 && x0<=MapTexture.width && y0>=0 && y0<=MapTexture.height) {
			    	TinySplat(x0, y0, 3, 15);
			    	TinyMMSplat(x0, y0, 3, 15);
			    }
			    if (size==2 && x0>=0 && x0<=MapTexture.width && y0>=0 && y0<=MapTexture.height) {
			    	TinySplat(x0, y0, 5, 30);
			    	TinyMMSplat(x0, y0, 5, 45);
			    }
			    if (size==3 && x0>=0 && x0<=MapTexture.width && y0>=0 && y0<=MapTexture.height) {
			    	TinySplat(x0, y0, 7, 60);
			    	TinyMMSplat(x0, y0, 7, 75);
			    }
			}
		}
	}
}

// draws the roads on the minimap
function TinyMMRoad(originX:int, originY:int, size:int, number:int) {
	var MapTexture : Texture2D = MapController.MinimapTextures[Sector];
	var transp : float = 0.5;
	var invtransp : float = 0.5;
	var sourcePixel : Color = MapTexture.GetPixel(originX, originY);
	MapTexture.SetPixel(originX, originY, (ColorArray[Random.Range(0,ColorArray.length)]*transp) + (sourcePixel*invtransp) );
	for (i=0; i<number; i++) {
		RandomJitterX = Random.Range(0, size)-(size/2);
		RandomJitterY = Random.Range(0, size)-(size/2);
		sourcePixel = MapTexture.GetPixel(originX+RandomJitterX, originY+RandomJitterY);
	    MapTexture.SetPixel(originX+RandomJitterX, originY+RandomJitterY, (ColorArray[Random.Range(0,ColorArray.length)]*transp) + (sourcePixel*invtransp) );
	}
}

// draws a small splat on the minimap
function TinyMMSplat(originX:int, originY:int, size:int, number:int) {
	var MapTexture : Texture2D = MapController.MinimapTextures[Sector];
	var transp : float = 0.5;
	var invtransp : float = 0.5;
	var sourcePixel : Color = MapTexture.GetPixel(originX, originY);
	MapTexture.SetPixel(originX, originY, (ColorArray[Random.Range(0,ColorArray.length)]*transp) + (sourcePixel*invtransp) );
	for (i=0; i<number; i++) {
		RandomJitterX = Random.Range(0, size)-(size/2);
		RandomJitterY = Random.Range(0, size)-(size/2);
		sourcePixel = MapTexture.GetPixel(originX+RandomJitterX, originY+RandomJitterY);
	    MapTexture.SetPixel(originX+RandomJitterX, originY+RandomJitterY, (ColorArray[Random.Range(0,ColorArray.length)]*transp) + (sourcePixel*invtransp) );
	}
}

// makes a small splat on the regular map
function TinySplat(originX:int, originY:int, size:int, number:int) {
	var MapTexture : Texture2D = gameObject.GetComponent(MeshRenderer).material.mainTexture;
	var transp : float = 0.5;
	var invtransp : float = 0.5;
	var sourcePixel : Color = MapTexture.GetPixel(originX, originY);
	MapTexture.SetPixel(originX, originY, (ColorArray[Random.Range(0,ColorArray.length)]*transp) + (sourcePixel*invtransp) );
	for (i=0; i<number; i++) {
		RandomJitterX = Random.Range(0, size)-(size/2);
		RandomJitterY = Random.Range(0, size)-(size/2);
		sourcePixel = MapTexture.GetPixel(originX+RandomJitterX, originY+RandomJitterY);
	    MapTexture.SetPixel(originX+RandomJitterX, originY+RandomJitterY, (ColorArray[Random.Range(0,ColorArray.length)]*transp) + (sourcePixel*invtransp) );
	}
}

function MakeWater () {
	var WaterMesh = new Mesh();
	var FlatVertices = new Array();
	for (i=0; i<GVertices.length; i++) {
		var TempMoveVert = new Vector3(GVertices[i].x, 0.0, GVertices[i].z);
		FlatVertices.Add(TempMoveVert);
	}
	WaterMesh.vertices = FlatVertices;
	WaterMesh.triangles = gameObject.GetComponent(MeshFilter).mesh.triangles;
	WaterMesh.uv = UV;
	WaterMesh.RecalculateNormals();
	Water1MeshObj.GetComponent(MeshFilter).mesh = WaterMesh;
	Water2MeshObj.GetComponent(MeshFilter).mesh = WaterMesh;
	MinimapMeshObj.GetComponent(MeshFilter).mesh = WaterMesh;
	if (MapController.IsElevationMap == false) {
		Water1MeshObj.transform.position.y = -0.005;
		Water2MeshObj.transform.position.y = -0.06;
	} else {
		Water1MeshObj.transform.position.y = -0.01;
		Water2MeshObj.transform.position.y = -0.2;
	}
}

// sets up the MoveMesh
function MakeMoveMesh () {
	var MoveMesh = new Mesh();
	MoveMesh.vertices = GVertices;
	MoveMesh.triangles = GTriangles;
	MoveMesh.uv = UV;
	MoveMeshObj.GetComponent(MeshFilter).mesh = MoveMesh;
	MoveMeshObj.GetComponent(MeshCollider).mesh = MoveMesh;
}

// makes sure the edges of the zones match
function StitchEdges() {
	if (!IsRightSide) {
		var TempGVertices = new Vector3[MapController.GraphicMeshes[Sector].vertices.length];
		TempGVertices = MapController.GraphicMeshes[Sector].vertices;
		var PrimaryList1 = new Array();
		var PrimaryListIndex1 = new Array();
		for (i=0; i<TempGVertices.length; i++) {
			if (TempGVertices[i].x > REdgeVert1.x - 0.01) {
				PrimaryList1.Add(TempGVertices[i]);
				PrimaryListIndex1.Add(i);
			}
		}
		var TempGVertices2 = new Vector3[MapController.GraphicMeshes[Sector+1].vertices.length];
		TempGVertices2 = MapController.GraphicMeshes[Sector+1].vertices;
		var PrimaryList2 = new Array();
		var PrimaryListIndex2 = new Array();
		var RightMap = MapController.GraphicMeshObjects[Sector+1].GetComponent("GMeshControl");
		TempGVertices2 = MapController.GraphicMeshes[RightMap.Sector].vertices;
		for (i=0; i<TempGVertices2.length; i++) {
			if (TempGVertices2[i].x < REdgeVert2.x + 0.01) {
				PrimaryList2.Add(TempGVertices2[i]);
				PrimaryListIndex2.Add(i);
			}
		}
	// compare these lists and build shared lists
		var SharedList = new Array();
		var SharedIndex1 = new Array();
		var SharedIndex2 = new Array();
		for (i=0; i<PrimaryList1.length; i++) {
			for (j=0; j<PrimaryList2.length; j++){
				if ( (PrimaryList1[i].x == PrimaryList2[j].x) && (PrimaryList1[i].z == PrimaryList2[j].z) ) {
					var TempSharedVertex = new Vector3 (PrimaryList1[i].x,
														Random.Range(PrimaryList1[i].y, PrimaryList2[j].y),
														PrimaryList1[i].z );
					SharedList.Add(TempSharedVertex);
					SharedIndex1.Add(PrimaryListIndex1[i]);
					SharedIndex2.Add(PrimaryListIndex2[j]);
				}
			}
		}
		var TempNormals1 = new Vector3[MapController.GraphicMeshes[Sector].normals.length];
		var TempNormals2 = new Vector3[MapController.GraphicMeshes[Sector+1].normals.length];
		TempNormals1 = MapController.GraphicMeshes[Sector].normals;
		TempNormals2 = MapController.GraphicMeshes[Sector+1].normals;
// reassign values to the original arrays
		for (i=0; i<SharedList.length; i++) {
			TempGVertices[SharedIndex1[i]] = SharedList[i];
			TempGVertices2[SharedIndex2[i]] = SharedList[i];
			var WorkNormal : Vector3 = TempNormals1[SharedIndex1[i]] + TempNormals2[SharedIndex2[i]];
			WorkNormal.Normalize();
			TempNormals1[SharedIndex1[i]] = WorkNormal;
			TempNormals2[SharedIndex2[i]] = WorkNormal;
		}
		MapController.GraphicMeshes[Sector].vertices = TempGVertices;
		MapController.GraphicMeshes[Sector+1].vertices = TempGVertices2;
		MapController.GraphicMeshObjects[Sector].GetComponent(MeshFilter).mesh.normals = TempNormals1;
		MapController.GraphicMeshObjects[Sector+1].GetComponent(MeshFilter).mesh.normals = TempNormals2;
		IsStitching = false;
	}

	if (!IsBottomSide) {
		TempGVertices = new Vector3[MapController.GraphicMeshes[Sector].vertices.length];
		TempGVertices = MapController.GraphicMeshes[Sector].vertices;
		PrimaryList1 = new Array();
		PrimaryListIndex1 = new Array();
		for (i=0; i<TempGVertices.length; i++) {
			if (TempGVertices[i].z < BEdgeVert.z + 0.01) {
				PrimaryList1.Add(TempGVertices[i]);
				PrimaryListIndex1.Add(i);
			}
		}
		TempGVertices2 = new Vector3[MapController.GraphicMeshes[Sector+SectorsX].vertices.length];
		TempGVertices2 = MapController.GraphicMeshes[Sector+SectorsX].vertices;
		PrimaryList2 = new Array();
		PrimaryListIndex2 = new Array();
		RightMap = MapController.GraphicMeshObjects[Sector+SectorsX].GetComponent("GMeshControl");
		TempGVertices2 = MapController.GraphicMeshes[RightMap.Sector].vertices;
		for (i=0; i<TempGVertices2.length; i++) {
			if (TempGVertices2[i].z > BEdgeVert.z - 0.01) {
				PrimaryList2.Add(TempGVertices2[i]);
				PrimaryListIndex2.Add(i);
			}
		}
// compare these lists and build shared lists
		SharedList = new Array();
		SharedIndex1 = new Array();
		SharedIndex2 = new Array();
		for (i=0; i<PrimaryList1.length; i++) {
			for (j=0; j<PrimaryList2.length; j++){
				if ( (PrimaryList1[i].x == PrimaryList2[j].x) && (PrimaryList1[i].z == PrimaryList2[j].z) ) {
					TempSharedVertex = new Vector3 (PrimaryList1[i].x,
													Random.Range(PrimaryList1[i].y, PrimaryList2[j].y),
													PrimaryList1[i].z );
					SharedList.Add(TempSharedVertex);
					SharedIndex1.Add(PrimaryListIndex1[i]);
					SharedIndex2.Add(PrimaryListIndex2[j]);
				}
			}
		}
		TempNormals1 = new Vector3[MapController.GraphicMeshes[Sector].normals.length];
		TempNormals2 = new Vector3[MapController.GraphicMeshes[Sector+SectorsX].normals.length];
		TempNormals1 = MapController.GraphicMeshes[Sector].normals;
		TempNormals2 = MapController.GraphicMeshes[Sector+SectorsX].normals;
// reassign values to the original arrays
		for (i=0; i<SharedList.length; i++) {
			TempGVertices[SharedIndex1[i]] = SharedList[i];
			TempGVertices2[SharedIndex2[i]] = SharedList[i];
			WorkNormal = TempNormals1[SharedIndex1[i]] + TempNormals2[SharedIndex2[i]];
			WorkNormal.Normalize();
			TempNormals1[SharedIndex1[i]] = WorkNormal;
			TempNormals2[SharedIndex2[i]] = WorkNormal;
		}
		MapController.GraphicMeshes[Sector].vertices = TempGVertices;
		MapController.GraphicMeshes[Sector+SectorsX].vertices = TempGVertices2;
		MapController.GraphicMeshObjects[Sector].GetComponent(MeshFilter).mesh.normals = TempNormals1;
		MapController.GraphicMeshObjects[Sector+SectorsX].GetComponent(MeshFilter).mesh.normals = TempNormals2;
	}
}

// draws a dot
function DrawDot (aPoint:Vector2) {
	var aColor = Color.red;
	var aTexture = MapController.MinimapTextures[Sector];
	DrawPoint(aTexture, aPoint);
}

// creates triangle list
function MakeTriangles () {
	GTriangles.Clear();
	if (YSize%2 == 0) {
		for (yy=0; yy<YSize; yy++) {
			for (xx=0; xx<XSize; xx++) {
				StartPoint = (yy*(XSize+1)) + xx;
				if (yy%2==0) {// if yy even
					GTriangles.Add(StartPoint);
					GTriangles.Add(StartPoint + XSize+1 + 1);
					GTriangles.Add(StartPoint + XSize+1);

					GTriangles.Add(StartPoint);
					GTriangles.Add(StartPoint + 1);
					GTriangles.Add(StartPoint + XSize+1 + 1);
				}
				if (yy%2!=0) {// if yy odd
					GTriangles.Add(StartPoint);
					GTriangles.Add(StartPoint+1);
					GTriangles.Add(StartPoint+XSize+1);

					GTriangles.Add(StartPoint + 1);
					GTriangles.Add(StartPoint + XSize+1 + 1);
					GTriangles.Add(StartPoint + XSize+1);
				}
			}
		}
	}
	if (YSize%2 != 0) {
		for (yy=0; yy<YSize; yy++) {
			for (xx=0; xx<XSize; xx++) {
				StartPoint = (yy * (XSize+1)) + xx;
				if (yy%2==0) {// if yy even
					GTriangles.Add(StartPoint);
					GTriangles.Add(StartPoint + 1);
					GTriangles.Add(StartPoint + XSize+1);
					
					GTriangles.Add(StartPoint + 1);
					GTriangles.Add(StartPoint + XSize+1 + 1);
					GTriangles.Add(StartPoint + XSize+1);
				}
				if (yy%2!=0) {// if yy odd
					GTriangles.Add(StartPoint);
					GTriangles.Add(StartPoint + XSize+1 + 1);
					GTriangles.Add(StartPoint + XSize+1);
					
					GTriangles.Add(StartPoint);
					GTriangles.Add(StartPoint + 1);
					GTriangles.Add(StartPoint + XSize+1 + 1);
				}
			}
		}
	}
	REdgeVert1 = GVertices[XSize];
	REdgeVert2 = GVertices[XSize + (XSize + 1)];
	BEdgeVert = GVertices[GVertices.length-1];
	if (REdgeVert1.x > REdgeVert2.x) {
		var TempREV = REdgeVert1;
		REdgeVert1 = REdgeVert2;
		REdgeVert2 = TempREV;
	}
	LEdgeVert1 = GVertices[0];
	LEdgeVert2 = GVertices[XSize + 1];
	if (LEdgeVert1.x > LEdgeVert2.x) {
		TempREV = LEdgeVert1;
		LEdgeVert1 = LEdgeVert2;
		LEdgeVert2 = TempREV;
	}
	gameObject.GetComponent(MeshFilter).mesh.vertices = GVertices;
	gameObject.GetComponent(MeshFilter).mesh.triangles = GTriangles;
	
	MapControl = GameObject.Find("MapControl");
	MapController = MapControl.GetComponent("MapControl");

	if (SecX == 0 && SecY == 0) {
		gameObject.GetComponent(MeshFilter).mesh.uv = MapControl.GetComponent("MapControl").UVSet0;
		UV = MapControl.GetComponent("MapControl").UVSet0;
	}
	if (SecX > 0 && SecY == 0) {
		gameObject.GetComponent(MeshFilter).mesh.uv = MapControl.GetComponent("MapControl").UVSet1;
		UV = MapControl.GetComponent("MapControl").UVSet1;
	}
	if (SecX == 0 && SecY > 0) {
		gameObject.GetComponent(MeshFilter).mesh.uv = MapControl.GetComponent("MapControl").UVSet2;
		UV = MapControl.GetComponent("MapControl").UVSet2;
	}
	if (SecX > 0 && SecY > 0) {
		gameObject.GetComponent(MeshFilter).mesh.uv = MapControl.GetComponent("MapControl").UVSet3;
		UV = MapControl.GetComponent("MapControl").UVSet3;
	}
}
