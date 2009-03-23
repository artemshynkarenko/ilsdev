#include "screen.h"
#include "objects.h"


inline void Screen::draw_line(Point2i from, Point2i to){
    MoveToEx(hdc_mem, from.x+(width>>1), (height>>1)-from.y, NULL);
	LineTo(hdc_mem, to.x+(width>>1), (height>>1)-to.y);
	/*from.x += (width>>1);
	to.x += (width>>1);
	from.y = (height>>1)-from.y;
	to.y = (height>>1)-to.y;
	if (from.x>to.x)
		swap(from, to);
     int deltax = abs(to.x - from.x);
     int deltay = abs(to.y - from.y);
     int error = 0;
     int deltaerr = deltay;
     int y = from.y, x;
	 for (x = from.x; x<=to.x; ++x){
		 SetPixel(hdc_mem, x,y, RGB(255, 255, 255));
         error += deltaerr;
		 if ((error<<1) >= deltax){
			 ++y;
             error -= deltax;
		 }
	 }
	 */
}

inline void Screen::draw_point(Point2i p){
	int r = 2;
	Ellipse(hdc_mem, (p.x-r)+(width>>1), (height>>1)-(p.y-r), (p.x+r)+(width>>1), (height>>1)-(p.y+r));
}

void Screen::display_frame(const Objects & objects){
	hdc = GetDC(hwnd);
	draw_to_buffer(objects);
	ReleaseDC(hwnd, hdc);
}

void Screen::draw_to_DC(HDC dc, const Objects & objects){
	int n = objects.size();
	for(int i=0; i<n; ++i){
		SelectObject(hdc_mem, pen_gray[objects[i].get_color()]);
		for(int j=0; j<(objects[i].size())>>1; ++j)
			draw_line(Point2i((int)objects[i][2*j][0], (int)objects[i][2*j][1]),
			          Point2i((int)objects[i][2*j+1][0], (int)objects[i][2*j+1][1]));
		SelectObject(hdc_mem, pen_gray[255]);
		for(int j=0; j<objects[i].size(); ++j)
			draw_point(Point2i((int)objects[i][j][0], (int)objects[i][j][1]));

	}
}

void Screen::draw_to_buffer(const Objects & objects){
	hdc_mem = CreateCompatibleDC(hdc);	
	if (hdc_mem)
	{
		memoryBitmap = CreateCompatibleBitmap(hdc, width, height);
		memoryBitmapOld = (HBITMAP) SelectObject(hdc_mem, memoryBitmap);
		old_pen = (HPEN) SelectObject(hdc_mem, pen_gray[200]);

		draw_to_DC(hdc_mem, objects);

		SelectObject(hdc_mem, old_pen);
		BitBlt(hdc, 0, 0, width, height, hdc_mem, 0, 0, SRCCOPY); //copy from buffer to screen
		SelectObject(hdc_mem, memoryBitmapOld);
		DeleteObject(memoryBitmap);
		DeleteDC(hdc_mem);
	}
	else{

	}

}

void Screen::display_frame_wm_paint(const Objects & objects){
	hdc = BeginPaint(hwnd, &ps);
	draw_to_buffer(objects);
	EndPaint(hwnd, &ps);

}


void Screen::init(){
	for(int i=0; i<256; ++i)
		pen_gray[i] = CreatePen(PS_SOLID, 1, RGB(i, i, i));
}

Screen::~Screen(){
	for(int i=0; i<256; ++i)
		DeleteObject(pen_gray[i]);
}