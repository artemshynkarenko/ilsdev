#include "Classes.h"

int APIENTRY WinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,                     
										 LPSTR    lpCmdLine,
                     int      nCmdShow)
{
	MSG msg;

	WindowsWindow::hInstance=hInstance;

	WindowsWindowClass::RegisterClass(hInstance,DEFAULT_CLASS_NAME);	
	WindowsWindow MainWindow(hInstance,NULL,DEFAULT_CLASS_NAME,L"Grafika",
								WS_OVERLAPPED | WS_SYSMENU | WS_CAPTION | WS_MINIMIZEBOX | WS_MAXIMIZEBOX | WS_CLIPCHILDREN | WS_CLIPSIBLINGS,SW_SHOW); 

	// Main message loop:
	while (GetMessage(&msg, NULL, 0, 0))
	{		
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	return (int) msg.wParam;
}