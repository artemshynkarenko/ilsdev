#ifndef __CLASSES
#define __CLASSES

#include<string>
#include<vector>
#include<windows.h>
#include<string>
#include<map>
#include<sstream>
#include<gl/gl.h>
#include<gl/glu.h>
#include<cmath>

using namespace std;

//MyGl.cpp=================================================================================================================
extern HDC hDC;
extern HGLRC hGLRC;

#define RASTER_SIZE_X 250
#define RASTER_SIZE_Y 250
#define RASTER_BORDER_X 20
#define RASTER_BORDER_Y 20
#define PIXEL_WIDTH 3
#define PIXEL_HEIGHT 3

struct GLColor{
	GLfloat R;
	GLfloat G;
	GLfloat B;
	GLColor():R(0), G(0), B(0){}
	GLColor(GLfloat R_, GLfloat G_, GLfloat B_):R(R_), G(G_), B(B_){}
};
#define DEFAULT_PIXEL_COLOR GLColor(0, 0, 1)
#define LINE_PIXEL_COLOR GLColor(1, 0, 0)
#define GRID_PIXEL_COLOR GLColor(0, 0, 0)

struct Point{
	int x;
	int y;
	Point():x(0),y(0){}
	Point(int x_, int y_):x(x_),y(y_){}
	GLfloat ToGLX(HWND hWND);
	GLfloat ToGLY(HWND hWND);
	virtual void Draw(HWND hWND);
};

struct Pixel:public Point{
	GLColor color;
	Pixel(){}
	Pixel(int x_, int y_, GLColor color_):Point(x_, y_), color(color_){}
	void Draw(HWND hWND);
};

class Line{
	Point _start;
	Point _end;
	GLColor _color;
public:
	Line(Point start, Point end, GLColor color):_start(start), _end(end), _color(color){}
	void Draw(HWND hWND);
};

class PixelLine{
	Pixel _start;
	Pixel _end;
	GLColor _color;
public:
	PixelLine(Pixel start, Pixel end, GLColor color):_start(start), _end(end), _color(color){}
	void SetEndPixel(Pixel newEnd){_end=newEnd;}
	void Draw(HWND hWND);
};

void SetupPixelFormat(HWND hWND,HDC &hDC,HGLRC &hGLRC);
void DrawScene(HWND hWND);

//Classes.cpp==============================================================================================================
#define DEFAULT_CLASS_NAME L"MyClass"
#define MAX_LOADSTRING 100
#define DEFAULT_WINDOW_X_POSITION 100
#define DEFAULT_WINDOW_Y_POSITION 100
#define DEFAULT_WINDOW_WIDTH 800
#define DEFAULT_WINDOW_HEIGHT 800

class WindowsWindowClass{
public: 
	static ATOM RegisterClass(HINSTANCE hInstance, LPCTSTR className);
};

class WindowsWindow{
	HWND _hWND;
	virtual LRESULT CALLBACK WindowProc(HWND hWND, UINT message, WPARAM wParam, LPARAM lParam);
	void CreateWindowsWindow(HINSTANCE hInstance, HWND parent, LPCTSTR className, LPCTSTR  title, DWORD windowStyle, int x, int y, int width, int height, int nCmdShow);
public:
	static HINSTANCE hInstance;
	vector<PixelLine> Lines;

	WindowsWindow(){}
	WindowsWindow(HINSTANCE hInstance, HWND parent, LPCTSTR className, LPCTSTR  title, DWORD windowStyle, int x, int y, int width, int height, int nCmdShow);
	
	HWND get_HWND();
	wstring get_Title()const;
	void set_Title(LPCWSTR title);

	int get_Width()const;
	int get_Height()const;

	void ShowWindowsWindow(int nCmdShow);
	static LRESULT CALLBACK WndProc(HWND hWND, UINT message, WPARAM wParam, LPARAM lParam);
};

#endif