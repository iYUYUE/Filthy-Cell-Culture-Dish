HEXTECH 3.0 DOCUMENTATION

For a basic intro and tutorial of how this sytem works view:

https://www.youtube.com/watch?v=kkouwY8LLfg

The "MapMaker" scene contains six objects:

CameraGroup - a camera rig that always looks where the base is located, has a script to control via mouse, keyboard, and touch.

a directional light, replace with your own lights if you wish

GridProjector - a projector that projects the hexagon grid down onto the map.  if you want to change the color or alpha, change the material here, or SetActive(false) to disable grid.

MapControl - The MapControl script is attached to this object, this is the core of the system.  More on this below...

MapObjects - an empty holder object that the parts of the map get grouped under

MiniMapCamera - a camera that looks down on the map, set to cull all layers except the minimap layer.  Has a script that causes it to follow CameraGroup.


The MapControl script -

I've left most of the variables exposed, so the user can see what is happening.
The only variable we need to be concerned with at first is "Map Texture".
Drag a map data texture from the Textures directory into this slot.  If the first two letters of the filename are "E-",
HexTech will treat the map as a small-scale Elevation Map, otherwise, it will be treated as a 'Country Map'.

What's the difference?
A Country Map is basically flat.  mountains are represented on it, but are about the same height as a hex width.
This map shows large areas, such as a world map.

An Elevation Map is much more 3D, as the name implies, elevation varies over a much larger range in this type of map.

When HexTech runs, it looks in /Resources/MapData/ for textures named after the Map Texture.
If no textures are found, HexTech creates them.  This process can take a few minutes, depending on the speed of your machine and the size of the map.
If the textures already exist, HexTech will create and display the map in just a few seconds.

See the YouTube video above for instructions on how the map data files are created.
