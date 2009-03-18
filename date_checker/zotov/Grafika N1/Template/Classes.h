#pragma once

#include<string>
#include<vector>
#include<windows.h>
#include<string>
#include<map>
#include <gl/gl.h>
#include <gl/glu.h>

using namespace std;

//Classes.cpp==============================================================================================================
#define DEFAULT_CLASS_NAME L"MyClass"
#define MAX_LOADSTRING 100
#define DEFAULT_WINDOW_SIZE 800

class WindowsWindowClass{
public: 
	static ATOM RegisterClass(HINSTANCE hInstance, LPCTSTR className);
};

class WindowsWindow{
	HWND _hWND;
public:
	static HINSTANCE hInstance;

	WindowsWindow(){}
	WindowsWindow(HINSTANCE hInstance, HWND parent, LPCTSTR className, LPCTSTR  title, DWORD windowStyle, int nCmdShow);
	
	HWND get_HWND();
	wstring get_Title()const;
	void set_Title(LPCWSTR title);

	int get_Width()const;
	int get_Height()const;	

	void CreateWindowsWindow(HINSTANCE hInstance, HWND parent, LPCTSTR className, LPCTSTR  title, DWORD windowStyle, int nCmdShow);
	void ShowWindowsWindow(int nCmdShow);
	virtual LRESULT CALLBACK WindowProc(HWND hWND, UINT message, WPARAM wParam, LPARAM lParam);
	static LRESULT CALLBACK WndProc(HWND hWND, UINT message, WPARAM wParam, LPARAM lParam);
};

class WindowsWindowContainer{
	map<HWND,WindowsWindow *> _windows;
public:
	typedef pair<HWND,WindowsWindow *> pair_type;
	typedef map<HWND,WindowsWindow *> my_type;
	void AddWindow(WindowsWindow &window);
	WindowsWindow *get_WindowByHWND(HWND hWND);
	WindowsWindow *operator[](int i);
};

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
void DrawScene(HDC hDC,GLfloat rotateX,GLfloat rotateY,GLfloat rotateZ,int drawingItem);