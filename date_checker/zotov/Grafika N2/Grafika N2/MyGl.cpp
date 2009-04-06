#include"Classes.h"
#include <cmath>

const GLfloat NUMBER_OF_CUBES_PER_AXIS =3;
const GLfloat CUBE_DIMENSION =1.0;
const GLfloat TETRAHEDRON_DIMENSION=1.0;
const GLfloat pi=3.1415926535;

//SetupPixelFormat=========================================================================================================
void SetupPixelFormat(HWND hWND,HDC &hDC,HGLRC &hGLRC)
{
	PIXELFORMATDESCRIPTOR pfd;
	memset(&pfd,0, sizeof(PIXELFORMATDESCRIPTOR));

	pfd.nSize	= sizeof(PIXELFORMATDESCRIPTOR);
	pfd.nVersion = 1;
	pfd.dwFlags = PFD_DRAW_TO_WINDOW |	PFD_SUPPORT_OPENGL |	PFD_DOUBLEBUFFER;
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
int DrawScene(HWND hWND, GLfloat a, GLfloat b, GLfloat detailing)
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

	glPointSize(2);
	glColor3f(1,0,0);

	glTranslatef(-0.5,0,0);
	glBegin(GL_POINTS);	
	for(GLfloat x=-a;x<=a;x+=detailing)
	{
		GLfloat y=b*sqrt(1-(x/a)*(x/a));
		Point(x,y,0).Draw();
		Point(x,-y,0).Draw();
		
	}
	glEnd();

	glTranslatef(1,0,0);
	//glColor3f(0,1,0);
	glBegin(GL_POINTS);
	int i=0;
	for(GLfloat f=0;f<=2*pi;f+=pi*detailing/(2*a))
	{
		GLfloat x=a*cos(f);
		GLfloat y=b*sin(f);
		Point(x,y,0).Draw();
		i++;
	}
	glEnd();

	return i;
}

//Point====================================================================================================================
void Point::Draw()
{
	glVertex3f(x,y,z);
}

Point Point::operator+(const Point &point)const
{
	return Point(x+point.x,y+point.y,z+point.z);
}

Point Point::operator/(GLfloat number)const
{
	return Point(x/number,y/number,z/number);
}