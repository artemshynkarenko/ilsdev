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
                           WS_OVERLAPPED | WS_SYSMENU | WS_CAPTION | WS_MINIMIZEBOX | WS_CLIPCHILDREN | WS_CLIPSIBLINGS,
                           DEFAULT_WINDOW_X_POSITION, DEFAULT_WINDOW_Y_POSITION, DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT,
                           SW_SHOW);

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