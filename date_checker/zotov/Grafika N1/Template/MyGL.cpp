#include"Classes.h"
#include <cmath>


//#define NUMBER_OF_CUBES_PER_AXIS 10;
//#define CUBE_DIMENSION 1.0;

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
	wglMakeCurrent(hDC, hGLRC);
}

//Additional Drawing(Cube & Quadrate)======================================================================================
void PaintQuadrate(Point point1,Point point2,Point point3,Point point4)
{
	glBegin(GL_QUADS);
		point1.Draw();
		point2.Draw();
		point3.Draw();
		point4.Draw();
	glEnd();
}

void PaintCube(Point point1,Point point7)
{
	Point point2(point1.x,point7.y,point1.z),point3(point7.x,point7.y,point1.z),point4(point7.x,point1.y,point1.z);
	Point point5(point1.x,point1.y,point7.z),point6(point1.x,point7.y,point7.z),point8(point7.x,point1.y,point7.z);

	//glColor3f(1,1,1);	
	PaintQuadrate(point1,point2,point3,point4);
	glColor3f(0.8,0,0);
	PaintQuadrate(point5,point6,point7,point8);

	PaintQuadrate(point2,point6,point7,point3);
	PaintQuadrate(point1,point5,point8,point4);
}

//Additional Drawing(Tetrahedon & Triangle)================================================================================
void DrawTriangle(Point point1,Point point2,Point point3)
{
	glBegin(GL_TRIANGLES);
		point1.Draw();
		point2.Draw();
		point3.Draw();
	glEnd();
}

void PaintTetrahedron(Point point1,Point point2,Point point3,Point point4)
{
	glColor3f(0.8,0,0);
	DrawTriangle(point1,point2,point3);	
	DrawTriangle(point1,point2,point4);
	DrawTriangle(point1,point3,point4);
	DrawTriangle(point2,point3,point4);
}

//Drawing==================================================================================================================
void DrawScene(HDC hDC,GLfloat rotateX,GLfloat rotateY,GLfloat rotateZ,int drawingItem)
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glClearColor(0.0f, 0.5f, 0.0f, 1.0f);
	glViewport(0, 0, DEFAULT_WINDOW_SIZE, DEFAULT_WINDOW_SIZE);

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(-1, 1, -1, 1, -1, 1);		

	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();	

	glPolygonMode(GL_FRONT_AND_BACK,GL_LINE);
	glEnable(GL_DEPTH_TEST);
	glShadeModel(GL_FLAT);
	glLineWidth(1.3);

	glRotatef(rotateX,1,0,0);
	glRotatef(rotateY,0,1,0);
	glRotatef(rotateZ,0,0,1);
	glScalef(0.5,0.5,0.5);

	if(drawingItem==DRAW_CUBE)
	{			
		for(GLfloat x=-CUBE_DIMENSION;x<CUBE_DIMENSION;x+=2*CUBE_DIMENSION/NUMBER_OF_CUBES_PER_AXIS)
		{
			for(GLfloat y=-CUBE_DIMENSION;y<CUBE_DIMENSION;y+=2*CUBE_DIMENSION/NUMBER_OF_CUBES_PER_AXIS)
			{
				for(GLfloat z=-CUBE_DIMENSION;z<CUBE_DIMENSION;z+=2*CUBE_DIMENSION/NUMBER_OF_CUBES_PER_AXIS)
				{
					GLfloat delta=2*CUBE_DIMENSION/NUMBER_OF_CUBES_PER_AXIS;
					PaintCube(Point(x,y,z),Point(x+delta,y+delta,z+delta));	
				}
			}
		}
	}
	else if(drawingItem==DRAW_TETRAHEDRON)
	{
		Point point1(0,0,TETRAHEDRON_DIMENSION),point2(0,0,-TETRAHEDRON_DIMENSION),
					point3(2*TETRAHEDRON_DIMENSION*sin(pi/3),0,0),point4(point3.x/3,sqrt(8.0f/3)*TETRAHEDRON_DIMENSION,0);
		Point point5((point1+point2)/2.0f),point6((point2+point3)/2.0f),point7((point1+point3)/2.0f);
		Point point8((point1+point4)/2.0f),point9((point2+point4)/2.0f),point10((point3+point4)/2.0f);

		PaintTetrahedron(point1,point2,point3,point4);	

		PaintTetrahedron(point5,point7,point8,point1);
		PaintTetrahedron(point5,point6,point9,point2);
		PaintTetrahedron(point6,point7,point10,point3);
		PaintTetrahedron(point8,point9,point10,point4);

		PaintTetrahedron(point7,point8,point10,point5);
	}

	SwapBuffers(hDC);
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