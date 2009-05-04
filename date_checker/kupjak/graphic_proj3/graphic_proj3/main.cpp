#include <windows.h>
#include <stdlib.h>
#include <string.h>
#include <tchar.h>
#include <time.h>

#include "grmanager.h"

#define VK_0	0x30
#define VK_1	0x31
#define VK_2	0x32
#define VK_3	0x33
#define VK_4	0x34
#define VK_5	0x35
#define VK_6	0x36
#define VK_7	0x37
#define VK_8	0x38
#define VK_9	0x39
///////
#define VK_A	0x041
#define VK_B	0x042
#define VK_C	0x043
#define VK_D	0x044
#define VK_E	0x045
#define VK_F	0x046
#define VK_G	0x047
#define VK_H	0x048
#define VK_I	0x049
#define VK_J	0x04A
#define VK_K	0x04B
#define VK_L	0x04C
#define VK_M	0x04D
#define VK_N	0x04E
#define VK_O	0x04F
#define VK_P	0x050
#define VK_Q	0x051
#define VK_R    0x052
#define VK_S	0x053
#define VK_T	0x054
#define VK_U	0x055
#define VK_V	0x056
#define VK_W	0x057
#define VK_X	0x058
#define VK_Y	0x059
#define VK_Z	0x05A



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

WCHAR to_wchar(char c){
	if (c == '0')
		return L'0';
	if (c == '1')
		return L'1';
	if (c == '2')
		return L'2';
	if (c == '3')
		return L'3';
	if (c == '4')
		return L'4';
	if (c == '5')
		return L'5';
	if (c == '6')
		return L'6';
	if (c == '7')
		return L'7';
	if (c == '8')
		return L'8';
	if (c == '9')
		return L'9';
	if (c == '.')
		return L'.';
	return L' ';
}

wstring to_wstring(double n){
	char s[100];
	sprintf(s, "%lf", n);
	wstring res;
	for (int i=0; s[i]!=0; ++i)
		res.push_back(to_wchar(s[i]));
	return res;
}

int old_x=0, old_y=0;
bool lb_pressed = false;
bool rb_pressed = false;
int old_time;

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    TCHAR greeting[] = _T("Hello, World!");

	int x, y;
	wstring s;
    switch (message)
    {
	case WM_CREATE:
		SetTimer(hWnd, IDTIMEOUT, gr.get_msec_per_frame(), NULL);
		break;
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
	case WM_TIMER:
		
		s = to_wstring(1.0/(double(old_time-clock())/CLOCKS_PER_SEC));
		old_time = clock();
		SetWindowText(hWnd, (LPCWSTR)s.c_str());
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
		case VK_SPACE:
			gr.change_pause();
			break;
		case VK_DELETE:
			gr.inc_function(+0.02);
			break;
		case VK_INSERT:
			gr.inc_function(-0.02);
			break;
		case VK_Z:
			gr.change_line_style();
			break;
		case VK_L:
			gr.change_light();
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