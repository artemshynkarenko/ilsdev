#include <windows.h>
#include <stdlib.h>
#include <string.h>
#include <tchar.h>


#include "grmanager.h"

static TCHAR szWindowClass[] = _T("win32app"); // The main window class name.

static TCHAR szTitle[] = _T("Win32 Guided Tour Application"); // The string that appears in the application's title bar.

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM); // Forward declarations of functions included in this code module
 

Grmanager gr;


int WINAPI WinMain(HINSTANCE hInstance,
                   HINSTANCE hPrevInstance,
                   LPSTR lpCmdLine,
                   int nCmdShow)
{
    WNDCLASSEX wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);
    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_APPLICATION));
    wcex.hCursor        = LoadCursor(NULL, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = NULL;
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_APPLICATION));

    if (!RegisterClassEx(&wcex))
    {
        MessageBox(NULL,
            _T("Call to RegisterClassEx failed!"),
            _T("Win32 Guided Tour"),
            NULL);

        return 1;
    }

       // The parameters to CreateWindow explained:
    // szWindowClass: the name of the application
    // szTitle: the text that appears in the title bar
    // WS_OVERLAPPEDWINDOW: the type of window to create
    // CW_USEDEFAULT, CW_USEDEFAULT: initial position (x, y)
    // 500, 100: initial size (width, length)
    // NULL: the parent of this window
    // NULL: this application dows not have a menu bar
    // hInstance: the first parameter from WinMain
    // NULL: not used in this application
    HWND hWnd = CreateWindow(
        szWindowClass,
        szTitle,
        WS_OVERLAPPEDWINDOW,
        CW_USEDEFAULT, CW_USEDEFAULT,
        800, 600,
        NULL,
        NULL,
        hInstance,
        NULL
    );
	//hwnd_main = hWnd;

    if (!hWnd)
    {
        MessageBox(NULL,
            _T("Call to CreateWindow failed!"),
            _T("Win32 Guided Tour"),
            NULL);

        return 1;
    }

	gr.set(hWnd, 800, 600);

    ShowWindow(hWnd,nCmdShow); // The parameters to ShowWindow explained: hWnd: the value returned from CreateWindow; nCmdShow: the fourth parameter from WinMain;
    UpdateWindow(hWnd);

  
    MSG msg;
    while (GetMessage(&msg, NULL, 0, 0)) // Main message loop
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return (int) msg.wParam;
}

//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE:  Processes messages for the main window.
//
//  WM_PAINT    - Paint the main window
//  WM_DESTROY  - post a quit message and return
//
//

int old_x=0, old_y=0;
bool lb_pressed = false;
bool rb_pressed = false;

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    TCHAR greeting[] = _T("Hello, World!");

	int x, y;

    switch (message)
    {
	case WM_CREATE:
		SetTimer(hWnd, IDTIMEOUT, gr.get_msec_per_frame(), NULL);
		break;
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
	case WM_TIMER:
		gr.display_frame();
		break;
	case WM_KEYDOWN:
		switch (wParam){
		case VK_UP:
			gr.move_up();
			//gr.mouse_vertical(1);

			break;
		case VK_DOWN:
			//gr.mouse_vertical(-1);
			gr.move_down();
			break;
		case VK_LEFT:
			gr.move_left();
			//gr.mouse_horizontal(1);
			break;
		case VK_RIGHT:
			gr.move_right();
			//gr.mouse_horizontal(-1);
			break;
		}
		break;
	case WM_MOUSEMOVE: 
		x = LOWORD(lParam); 
		y = HIWORD(lParam);
		if (lb_pressed){
			gr.mouse_horizontal(x-old_x);
			gr.mouse_vertical(y-old_y);
		}
		if (rb_pressed){
			/*if (abs(x-old_x) > abs(y-old_y))
				gr.scaled(x-old_x);
			else
				gr.scaled(y-old_y);
				*/
			gr.scaled(y-old_y);
		}
		old_x = x;
		old_y = y;
		break;
	case WM_LBUTTONDOWN:
		lb_pressed = true;
		break;
	case WM_LBUTTONUP:
		lb_pressed = false;
		break;
	case WM_RBUTTONDOWN:
		rb_pressed = true;
		break;
	case WM_RBUTTONUP:
		rb_pressed = false;
		break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
        break;
    }

    return 0;
}