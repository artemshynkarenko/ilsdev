#include "Classes.h"

HINSTANCE WindowsWindow::hInstance=0;

#define KEY_LEFT 37
#define KEY_RIGHT 39
#define KEY_UP 38
#define KEY_DOWN 40
#define KEY_PLUS 187
#define KEY_MINUS 189
#define STEP_OF_DRAW_DETAILING 0.0005
#define DELTA 0.01

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
	wcex.hIcon			= NULL;//LoadIcon(hInstance, MAKEINTRESOURCE(IDI_WA3));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= NULL;//MAKEINTRESOURCE(IDR_MENU1);
	wcex.lpszClassName	= className;
	wcex.hIconSm		= NULL;//LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

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

	_hWND = CreateWindow(className, title, windowStyle,
						x, y, width, height, parent, NULL, hInstance, &mDICreateStruct);

	ShowWindowsWindow(nCmdShow);
}

void WindowsWindow::ShowWindowsWindow(int nCmdShow)
{
	::ShowWindow(_hWND, nCmdShow);
	UpdateWindow(_hWND);
}

LRESULT CALLBACK WindowsWindow::WindowProc(HWND hWND, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	static HDC hDC=0;
	static HGLRC hGLRC=0;
	static GLfloat a=0.3,b=0.3,detailing=0.01;

	switch (message)
	{
	case WM_CREATE:
		SetupPixelFormat(hWND,hDC,hGLRC);
		
		break;
	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		// Parse the menu selections:
		switch (wmId)
		{
		default:
			return DefWindowProc(hWND, message, wParam, lParam);
		}
		break;
	case WM_KEYDOWN:
	{
		int key=(int)wParam;
		if(key==KEY_LEFT)
		{
			a-=DELTA;
		}
		else if(key==KEY_RIGHT)
		{
			a+=DELTA;
		}
		else if(key==KEY_UP)
		{
			b+=DELTA;
		}
		else if(key==KEY_DOWN)
		{
			b-=DELTA;
		}
		else if(key==KEY_PLUS)
		{
			if(detailing-STEP_OF_DRAW_DETAILING>0)
			{
				detailing-=STEP_OF_DRAW_DETAILING;
			}
		}
		else if(key==KEY_MINUS)
		{
			detailing+=STEP_OF_DRAW_DETAILING;
		}
		break;
	}
	case WM_PAINT:
	{
		wstringstream ss;
		wglMakeCurrent(hDC, hGLRC);
		ss<<"Grafika:Number of Points - "<<DrawScene(hWND, a, b, detailing);

		this->set_Title(ss.str().c_str());
		SwapBuffers(hDC);
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