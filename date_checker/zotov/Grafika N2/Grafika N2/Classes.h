#pragma once

#include<string>
#include<vector>
#include<windows.h>
#include<string>
#include<map>
#include<sstream>
#include <gl/gl.h>
#include <gl/glu.h>

using namespace std;

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

/*class WindowsChildWindow{
	WindowsWindow *_parentWindow;
public:
	WindowsWindow(HINSTANCE hInstance, HWND parent, LPCTSTR className, LPCTSTR  title, DWORD windowStyle, int x, int y, int width, int height, int nCmdShow);
};*/

//MyGl.cpp=================================================================================================================
extern HDC hDC;
extern HGLRC hGLRC;

#define DRAW_CUBE 1
#define DRAW_TETRAHEDRON 2

struct Point{
	GLfloat x;
	GLfloat y;
	GLfloat z;
	Point():x(0),y(0),z(0){}
	Point(GLfloat x_, GLfloat y_, GLfloat z_):x(x_),y(y_),z(z_){} 
	void Draw();
	Point operator+(const Point &point)const;
	Point operator/(GLfloat number)const;
};

void SetupPixelFormat(HWND hWND,HDC &hDC,HGLRC &hGLRC);
int DrawScene(HWND hWND, GLfloat a, GLfloat b, GLfloat detailing);