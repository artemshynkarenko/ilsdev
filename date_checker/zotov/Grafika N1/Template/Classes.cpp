#include "Classes.h"
#include "resource.h"

HINSTANCE WindowsWindow::hInstance=0;

#define KEY_LEFT 37
#define KEY_RIGHT 39
#define KEY_UP 38
#define KEY_DOWN 40
#define KEY_ONE 49
#define KEY_TWO 50
#define DELTA 5

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
WindowsWindow::WindowsWindow(HINSTANCE hInstance, HWND parent, LPCTSTR className, LPCTSTR  title, DWORD windowStyle, int nCmdShow)
{
	CreateWindowsWindow(hInstance,parent,className,title,windowStyle,nCmdShow);
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
	GetWindowRect(_hWND,&rect);
	return rect.right-rect.left;
}

int WindowsWindow::get_Height()const
{
	RECT rect;
	GetWindowRect(_hWND,&rect);
	return rect.bottom-rect.top;
}

void WindowsWindow::CreateWindowsWindow(HINSTANCE hInstance, HWND parent, LPCTSTR className, LPCTSTR  title, DWORD windowStyle, int nCmdShow)
{
	MDICREATESTRUCT mDICreateStruct;
	mDICreateStruct.lParam=(LPARAM)this;	

	_hWND = CreateWindow(className, title, windowStyle,
						CW_USEDEFAULT, 0, DEFAULT_WINDOW_SIZE, DEFAULT_WINDOW_SIZE, parent, NULL, hInstance, &mDICreateStruct);
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
	static GLfloat rotateX=0,rotateY=0,rotateZ=0;
	static int drawingItem=1;//що малювати

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
			rotateY+=DELTA;
		}
		else if(key==KEY_RIGHT)
		{
			rotateY-=DELTA;
		}
		else if(key==KEY_UP)
		{
			rotateX+=DELTA;
		}
		else if(key==KEY_DOWN)
		{
			rotateX-=DELTA;
		}
		else if(key==KEY_ONE)
		{
			drawingItem=DRAW_CUBE;
		}
		else if(key==KEY_TWO)
		{
			drawingItem=DRAW_TETRAHEDRON;
		}
		break;
	}
	case WM_PAINT:
		DrawScene(hDC,rotateX,rotateY,rotateZ,drawingItem);
		break;
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
		MDICREATESTRUCT *pmDICreateStruct = (MDICREATESTRUCT* ) ((LPCREATESTRUCT) lParam)->lpCreateParams;	
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

//WindowsWindowContainer===================================================================================================
void WindowsWindowContainer::AddWindow(WindowsWindow &window)
{
	_windows.insert(make_pair(window.get_HWND(),&window));
}

WindowsWindow *WindowsWindowContainer::get_WindowByHWND(HWND hWND)
{
	return _windows[hWND];
}

WindowsWindow *WindowsWindowContainer::operator[](int i)
{
	my_type::iterator j=_windows.begin();	
	for(int i1=0;i1!=i;j++,i1++);
	return j->second;
}