#include "Classes.h"

HINSTANCE WindowsWindow::hInstance=0;

#define KEY_CLEAR 67
#define CIRCLE_RADIUS 300

const GLfloat DELTA=0.1;
const GLfloat pi=3.1415926535;

//WindowsWindowClass======================================================================================================
ATOM WindowsWindowClass::RegisterClass(HINSTANCE hInstance, LPCTSTR className)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= WindowsWindow::WndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= 0;
	wcex.hInstance		= hInstance;
	wcex.hIcon			= NULL;
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= NULL;
	wcex.lpszClassName	= className;
	wcex.hIconSm		= NULL;

	return RegisterClassEx(&wcex);
}

//WindowsWindow=============================================================================================================
WindowsWindow::WindowsWindow(HINSTANCE hInstance, HWND parent, LPCTSTR className, LPCTSTR  title,
                             DWORD windowStyle, int x, int y, int width, int height, int nCmdShow)
{
	CreateWindowsWindow(hInstance, parent, className, title, windowStyle, x, y, width, height, nCmdShow);
}

HWND WindowsWindow::get_HWND()
{
	return _hWND;
}

wstring WindowsWindow::get_Title()const
{
	TCHAR bufer[MAX_LOADSTRING];
	GetWindowText(_hWND,bufer,MAX_LOADSTRING);
	return wstring(bufer);
}

void WindowsWindow::set_Title(LPCWSTR title)
{
	SetWindowText(_hWND,title);
}

int WindowsWindow::get_Width()const
{
	RECT rect;
	GetClientRect(_hWND,&rect);
	return rect.right-rect.left;
}

int WindowsWindow::get_Height()const
{
	RECT rect;
	GetClientRect(_hWND,&rect);
	return rect.bottom-rect.top;
}

void WindowsWindow::CreateWindowsWindow(HINSTANCE hInstance, HWND parent, LPCTSTR className, LPCTSTR  title,
                                        DWORD windowStyle, int x, int y, int width, int height, int nCmdShow)
{
	MDICREATESTRUCT mDICreateStruct;
	mDICreateStruct.lParam=(LPARAM)this;

	_hWND = CreateWindow(className, title, windowStyle, x, y, width, height, parent, NULL, hInstance, &mDICreateStruct);

	ShowWindowsWindow(nCmdShow);
}

void WindowsWindow::ShowWindowsWindow(int nCmdShow)
{
	::ShowWindow(_hWND, nCmdShow);
	UpdateWindow(_hWND);
}

LRESULT CALLBACK WindowsWindow::WindowProc(HWND hWND, UINT message, WPARAM wParam, LPARAM lParam)
{
	static HDC hDC=0;
	static HGLRC hGLRC=0;
	static bool ifLineDrawingStart=false;
	GLfloat xMousePos=GLfloat(LOWORD(lParam)-RASTER_BORDER_X)/PIXEL_WIDTH;
	GLfloat yMousePos=GLfloat(HIWORD(lParam)-RASTER_BORDER_Y)/PIXEL_HEIGHT;
	int xPos=xMousePos<0?-1:int(xMousePos);
	int yPos=yMousePos<0?-1:int(yMousePos);

	switch (message)
	{
	case WM_CREATE:
	{
		SetupPixelFormat(hWND,hDC,hGLRC);
		break;
	}
	case WM_ACTIVATE:
	{
		for(GLfloat f=0;f<=2*pi;f+=DELTA)
		{
			Pixel pixel1(get_Width()/(2*PIXEL_WIDTH), get_Height()/(2*PIXEL_HEIGHT), LINE_PIXEL_COLOR);
			Pixel pixel2((get_Width()/2+CIRCLE_RADIUS*cos(f))/PIXEL_WIDTH, (get_Height()/2+CIRCLE_RADIUS*sin(f))/PIXEL_HEIGHT, LINE_PIXEL_COLOR);
			Lines.push_back(PixelLine(pixel1, pixel2, LINE_PIXEL_COLOR));
		}
		break;
	}
	case WM_KEYDOWN:
	{
		int key=(int)wParam;
		if(key==KEY_CLEAR)
		{
			Lines.clear();
		}
		break;
	}
	case WM_PAINT:
	{
		wglMakeCurrent(hDC, hGLRC);
		DrawScene(hWND);
		SwapBuffers(hDC);
		break;
	}
	case WM_MOUSEMOVE:
	{
		if(xPos>=0 && xPos<RASTER_SIZE_X && yPos>=0 && yPos<RASTER_SIZE_Y)
		{
			xPos=int(xPos);
			yPos=int(yPos);
			if(ifLineDrawingStart)
			{
				Pixel newEnd(xPos, yPos, LINE_PIXEL_COLOR);
				Lines.back().SetEndPixel(newEnd);
			}
		}
		break;
	}
	case WM_LBUTTONDOWN:
	{
		if(xPos>=0 && xPos<RASTER_SIZE_X && yPos>=0 && yPos<RASTER_SIZE_Y)
		{
			xPos=int(xPos);
			yPos=int(yPos);
			if(ifLineDrawingStart)
			{
				Pixel newEnd(xPos, yPos, LINE_PIXEL_COLOR);
				Lines.back().SetEndPixel(newEnd);
			}
			else{
				Pixel start(xPos, yPos, LINE_PIXEL_COLOR);
				Lines.push_back(PixelLine(start, start, LINE_PIXEL_COLOR));
			}
			ifLineDrawingStart=!ifLineDrawingStart;
		}
		break;
	}
	case WM_DESTROY:
		wglMakeCurrent(0,0);
		DeleteDC(hDC);
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWND, message, wParam, lParam);
	}
	return 0;
}

LRESULT CALLBACK WindowsWindow::WndProc(HWND hWND, UINT message, WPARAM wParam, LPARAM lParam)
{
	WindowsWindow *pwindow=0;
	if(message==WM_NCCREATE){
		MDICREATESTRUCT *pmDICreateStruct = (MDICREATESTRUCT *) ((LPCREATESTRUCT) lParam)->lpCreateParams;	
		pwindow = (WindowsWindow *)(pmDICreateStruct->lParam);
		SetWindowLong(hWND,GWL_USERDATA,(LONG)pwindow);
	}
	else{
		pwindow = (WindowsWindow *)GetWindowLong(hWND,GWL_USERDATA);
	}	

	if(pwindow){
		return pwindow->WindowProc(hWND,message,wParam,lParam);
	}
	else{
		return DefWindowProc(hWND,message,wParam,lParam);
	}
}