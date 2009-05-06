#include"Classes.h"

//SetupPixelFormat=========================================================================================================
void SetupPixelFormat(HWND hWND,HDC &hDC,HGLRC &hGLRC)
{
	PIXELFORMATDESCRIPTOR pfd;
	memset(&pfd,0, sizeof(PIXELFORMATDESCRIPTOR));

	pfd.nSize	= sizeof(PIXELFORMATDESCRIPTOR);
	pfd.nVersion = 1;
	pfd.dwFlags = PFD_DRAW_TO_WINDOW | PFD_SUPPORT_OPENGL | PFD_DOUBLEBUFFER;
	pfd.iPixelType = PFD_TYPE_RGBA;
	pfd.cColorBits = 24;
	pfd.cAlphaBits=64;
	pfd.cAccumBits=64;
	pfd.cDepthBits = 64;
	pfd.cStencilBits = 64;
	pfd.iLayerType = PFD_MAIN_PLANE;

	hDC = GetDC(hWND);

	int pixelFormat = ChoosePixelFormat(hDC, &pfd);
	int i=SetPixelFormat(hDC, pixelFormat, &pfd);

	hGLRC = wglCreateContext(hDC);
}

//Drawing==================================================================================================================
void DrawScene(HWND hWND)
{
	glClear(GL_COLOR_BUFFER_BIT);
	glClearColor(0.8f, 0.8f, 0.0f, 1.0f);
	WindowsWindow *window = (WindowsWindow *)GetWindowLong(hWND,GWL_USERDATA);
	glViewport(0, 0, window->get_Width(), window->get_Height());

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();

	if(window->get_Height()!=0)
	{
		gluOrtho2D(-window->get_Width()/window->get_Height(), window->get_Width()/window->get_Height(), -1, 1);
	}

	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	glLineWidth(1);

	glPushMatrix;
	Point point1(RASTER_BORDER_X, RASTER_BORDER_Y);
	Point point2(0, 0);
	glTranslatef(point1.ToGLX(hWND)-point2.ToGLX(hWND), point1.ToGLY(hWND)-point2.ToGLY(hWND), 0);
	glColor3f(DEFAULT_PIXEL_COLOR.R, DEFAULT_PIXEL_COLOR.G, DEFAULT_PIXEL_COLOR.B);
	glBegin(GL_QUADS);
		Point(0, 0).Draw(hWND);
		Point(RASTER_SIZE_X*PIXEL_WIDTH, 0).Draw(hWND);
		Point(RASTER_SIZE_X*PIXEL_WIDTH, RASTER_SIZE_Y*PIXEL_HEIGHT).Draw(hWND);
		Point(0, RASTER_SIZE_Y*PIXEL_HEIGHT).Draw(hWND);
	glEnd();

	for(int i=0;i<window->Lines.size();i++)
	{
		window->Lines[i].Draw(hWND);
	}

	for(int i=0;i<=RASTER_SIZE_X;i++)
	{
		Point start(i*PIXEL_WIDTH, 0);
		Point end(i*PIXEL_WIDTH, RASTER_SIZE_Y*PIXEL_HEIGHT);
		Line(start, end, GRID_PIXEL_COLOR).Draw(hWND);
	}

	for(int i=0;i<=RASTER_SIZE_Y;i++)
	{
		Point start(0, i*PIXEL_HEIGHT);
		Point end(RASTER_SIZE_X*PIXEL_WIDTH, i*PIXEL_HEIGHT);
		Line(start, end, GRID_PIXEL_COLOR).Draw(hWND);
	}

	glPopMatrix;
}

//Point====================================================================================================================
GLfloat Point::ToGLX(HWND hWND)
{
	WindowsWindow *window = (WindowsWindow *)GetWindowLong(hWND,GWL_USERDATA);
	if(window->get_Height() && window->get_Width()){
		GLfloat ratio=window->get_Width()/window->get_Height();
		return (float(x)*2*ratio)/window->get_Width()-1;
	}
	else{
		return 0;
	}
}

GLfloat Point::ToGLY(HWND hWND)
{
	WindowsWindow *window = (WindowsWindow *)GetWindowLong(hWND,GWL_USERDATA);
	if(window->get_Height()){
		GLfloat ff=window->get_Height();
		return 1-(float(y)*2)/window->get_Height();
	}
	else{
		return 0;
	}
}

void Point::Draw(HWND hWND)
{
	glVertex2f(ToGLX(hWND),ToGLY(hWND));
}

//Pixel========================================================================================================
void Pixel::Draw(HWND hWND)
{
	int left=x*PIXEL_WIDTH;
	int up=y*PIXEL_HEIGHT;
	int right=left+PIXEL_WIDTH;
	int down=up+PIXEL_HEIGHT;
	glColor3f(color.R, color.G, color.B);
	glBegin(GL_QUADS);
		Point(left, up).Draw(hWND);
		Point(right, up).Draw(hWND);
		Point(right, down).Draw(hWND);
		Point(left, down).Draw(hWND);
	glEnd();
}

//Line==========================================================================================================
void Line::Draw(HWND hWND)
{
	glColor3f(_color.R, _color.G, _color.B);
	glBegin(GL_LINES);
		_start.Draw(hWND);
		_end.Draw(hWND);
	glEnd();
}

//PixelLine====================================================================================================
void PixelLine::Draw(HWND hWND)
{
	Pixel start=_start;
	Pixel end=_end;
	if(abs(start.x-end.x)>abs(start.y-end.y))
	{
		if(start.x>end.x)
		{
			swap(start,end);
		}
		for(int x=start.x;x<=end.x;x++)
		{
			int y=float((end.y-start.y)*(x-start.x))/(end.x-start.x)+start.y+0.5;
			Pixel(x, y, _color).Draw(hWND);
		}
	}
	else
	{
		if(start.y>end.y)
		{
			swap(start,end);
		}
		for(int y=start.y;y<=end.y;y++)
		{
			int x=start.x;
			if(end.y!=start.y){
				x=float((end.x-start.x)*(y-start.y))/(end.y-start.y)+start.x+0.5;
			}
			Pixel(x, y, _color).Draw(hWND);
		}
	}
}