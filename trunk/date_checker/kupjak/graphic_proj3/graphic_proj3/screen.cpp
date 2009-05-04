#include "screen.h"
#include "objects.h"


int a[800][600];
bool steep;
int deltax, deltay, error, ystep, x, y;

inline void Screen::draw_line(Point2i from, Point2i to, int c){
	from.x += (width>>1);
	to.x += (width>>1);
	from.y = (height>>1)-from.y;
	to.y = (height>>1)-to.y;
	if (line_api || !light){
		MoveToEx(hdc_mem, from.x, from.y, NULL);
		LineTo(hdc_mem, to.x, to.y);
	}
	else{
	
	
	steep = abs(to.y - from.y) > abs(to.x - from.x);
	if (steep){
		swap(from.x, from.y);
		swap(to.x, to.y);
	}
	if (from.x > to.x){
		swap(from.x, to.x);
		swap(from.y, to.y);
	}
	deltax = to.x - from.x;
	deltay = abs(to.y - from.y);
	error = deltax >> 1;
	ystep;
	y = from.y;
	if (from.y < to.y)
		ystep = 1;
	else 
		ystep = -1;
	//x;
	for (x=from.x; x<=to.x; ++x){
		if (steep){
			if (y>=0 && y<800 && x>=0 && x<600)
			//	SetPixel(hdc_mem, y, x, max(c, GetPixel(hdc_mem, y, x)));
				a[y][x] = max(c, a[y][x]);
		}
		else{
			if (x>=0 && x<800 && y>=0 && y<600)
			//	SetPixel(hdc_mem, x, y, max(c, GetPixel(hdc_mem, x, y)));
				a[x][y] = max(c, a[x][y]);
		}
		error = error - deltay;
		if (error < 0){
			y = y + ystep;
			error = error + deltax;
		}
	}
	}
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
	HPEN p;

	//memset(a, 0, sizeof(a));
	for(int i=0; i<n; ++i){
		if (objects[i].get_drawed_line()){
			double d;
			int color;
			int c = objects[i].get_color_line();

			if (line_api || !light){
					p = CreatePen(PS_SOLID, 1, c);
					SelectObject(hdc_mem, p);
			}

			for(int j=0; j<(objects[i].size()>>1); ++j){
				if (light){
					//d = 0.3 + 2*(pi/2+ atan(objects[i][2*j][2]/60.0))/pi;
					d = 3000.5*(0.0005 + objects[i][2*j][2]/600000.0);
					if (d<0)
						d = 0;
					color = RGB(GetRValue(c)*d, GetGValue(c)*d, GetBValue(c)*d);
				}
				else
					color = c;
				if (line_api && light){
					p = CreatePen(PS_SOLID, 1, color);
					SelectObject(hdc_mem, p);
				}
				//d = 0.3 + 2*(pi/2+ atan(objects[i][2*j][2]/60.0))/pi;
				draw_line(Point2i((int)objects[i][2*j][0], (int)objects[i][2*j][1]),
					Point2i((int)objects[i][2*j+1][0], (int)objects[i][2*j+1][1]), 
					color);
				if (line_api || !light){
					SelectObject(hdc_mem, old_pen);
					DeleteObject(p);
				}
			}

			if (line_api && !light){
					SelectObject(hdc_mem, old_pen);
					DeleteObject(p);
			}

		}
	}
	if (!line_api && light)
	for (int i=0; i<width; ++i)
		for (int j=0; j<height; ++j)
			if (a[i][j]!=0){
				SetPixel(hdc_mem, i, j, a[i][j]);
				a[i][j] = 0;
			}
			
	for(int i=0; i<n; ++i){
		if (objects[i].get_drawed_point()){
			HPEN p = CreatePen(PS_SOLID, 1, objects[i].get_color_point());
			SelectObject(hdc_mem, p);
			for(int j=0; j<objects[i].size(); ++j)
				draw_point(Point2i((int)objects[i][j][0], (int)objects[i][j][1]));
			SelectObject(hdc_mem, old_pen);
			DeleteObject(p);
		}
	}
	
}

void Screen::draw_to_buffer(const Objects & objects){
	hdc_mem = CreateCompatibleDC(hdc);	
	if (hdc_mem)
	{
		memoryBitmap = CreateCompatibleBitmap(hdc, width, height);
		memoryBitmapOld = (HBITMAP) SelectObject(hdc_mem, memoryBitmap);
		old_pen = (HPEN) SelectObject(hdc_mem, 0);

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
	line_api = false;
	light = true;
	/*for(int i=0; i<256; ++i)
		pen_gray[i] = CreatePen(PS_SOLID, 1, RGB(i, i, i));
	*/
}

Screen::~Screen(){
	/*for(int i=0; i<256; ++i)
		DeleteObject(pen_gray[i]);
	*/
}