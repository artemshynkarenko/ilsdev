#pragma once
#include <windows.h>
#include <stdlib.h>
#include <string.h>
#include <tchar.h>

#include "graph.h"
#include "objects.h"

struct Cam{
	Point from;
	Point to;
//public:
	Cam():from(1000, 1000, 1000), to(0, 0, 0){};
	Cam(const Point & f, const Point & t): from(f), to(t){}
};

class Screen{
	friend class Grmanager;
	int width, height;
	HWND hwnd;
	HDC hdc; 
	HDC hdc_mem; // buffer
	PAINTSTRUCT ps;  
	HPEN pen_gray[256];
	HPEN old_pen;
	HBITMAP memoryBitmap;
	HBITMAP memoryBitmapOld;

	Cam cam;
	void draw_to_DC(HDC dc, const Objects & objects);
	void draw_to_buffer(const Objects & objects);
public:
	static const int frame_count_per_sec = 25;
	static const int msec_per_frame = 1000/frame_count_per_sec;
	Screen(){init();}
	Screen(HWND hw, const int w, const int h): hwnd(hw), width(w), height(h){init();}
	~Screen();
	void set(HWND hw, const int w, const int h){hwnd = hw; width = w; height = h;}
	void init();
	inline void draw_line(Point2i from, Point2i to);
	inline void draw_point(Point2i p);
	void display_frame(const Objects & objects);
	void display_frame_wm_paint(const Objects & objects);
};
