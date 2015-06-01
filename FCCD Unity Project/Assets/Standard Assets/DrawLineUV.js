static function LineUV (tex : Texture2D, p1 : Vector2, p2 : Vector2, col : Color) {
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
    }
    else {
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