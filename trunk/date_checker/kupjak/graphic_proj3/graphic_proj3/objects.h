#pragma once

#include <windows.h>
#include <vector>
#include <iostream>
#include <fstream>
#include <string>
#include <cmath>


#include "graph.h"

using namespace std;

class Vector4d{
	double data[4];
public:
	Vector4d(){memset(data, 0, sizeof(data));}
	Vector4d(const double a1, const double a2, const double a3, const double a4){data[0]=a1; data[1]=a2; data[2]=a3; data[3]=a4;}
	Vector4d(const Point &p){data[0]=p.x; data[1]=p.y; data[2]=p.z; data[3]=1;};
	const double& operator[] (const int i)const{return data[i];}
	double& operator[] (const int i){return data[i];} 
};

class Matrix44d{
	Vector4d data[4];
public:
	Matrix44d(){memset(data, 0, sizeof(data)); data[0][0]=data[1][1]=data[2][2]=data[3][3]=1;}
	const Vector4d& operator[] (const int i)const{return data[i];}
	Vector4d& operator[] (const int i){return data[i];} 
	void mult(const Matrix44d & a);
	void simple(){*this = Matrix44d();}
	void rotate_x(double a);
	void rotate_y(double b);
	void rotate_z(double c);
	void scale(double a);
	void position(const Point & p);
	void rotate_vector_z(const Point & p);
	void rotate_vector_abc(const Rotation_point & rp);
	void perspective(const Point & from, const Point & to);
};

Point & operator*=(Point & p, const Matrix44d & m);
Vector4d & operator*=(Vector4d & v, const Matrix44d & m);

class Poligon{
	friend class Object;
	vector<Point2> vertex;
	Point position;
	Point normal;
public:
	Poligon(){}
	Poligon(string file_name){ifstream f(file_name.c_str()); if (f) f >> *this;}
	friend istream& operator >> (istream & is, Poligon & p);
	void split(const int count);
};


class Object{
	vector<Vector4d> source;
	vector<Vector4d> dest;
	int color_point;
	int color_line;
	bool drawed_point;
	bool drawed_line;
	friend class Objects;
	friend class Grmanager;
public:
	Object(const int cp=RGB(180, 180, 180), const int cl=RGB(80, 80, 80) ,const bool pp=true, const bool pl=true):color_point(cp), color_line(cl), drawed_point(pp), drawed_line(pl){}
	Object(const Poligon & p, const int cp=RGB(180, 180, 180), const int cl=RGB(80, 80, 80) ,const bool pp=true, const bool pl=true);
	const Vector4d& operator[] (const int i)const{return dest[i];}
	Vector4d& operator[] (const int i){return dest[i];} 
	int size()const{return source.size();}
	void marge(const Object & o);
	vector<vector<int>> polylines;
	vector<vector<int>> solids;

	int get_color_line()const{return color_line;}
	int get_color_point()const{return color_point;}
	bool get_drawed_point()const{return drawed_point;}
	bool get_drawed_line()const{return drawed_line;}
	void add_line(Line & l);
	void calc(const Matrix44d & matrix);
};

Point Vector4d_to_Point(const Vector4d & v); 

class Objects{
	vector<Object> all;
	Matrix44d matrix;
	void calc_obj(int ind); 

public:
	Point pos;
	double theta;
	double phi;
	Objects(): theta(0), phi(0){}
	int size()const{return all.size();}
	const Object& operator[] (const int i)const{return all[i];}
	Object& operator[] (const int i){return all[i];} 
	void add(Object o){all.push_back(o);}
	void add_from_file(string file_name);
	void calc_all(); 
	void matrix_mult(const Matrix44d & a){matrix.mult(a);}
	void matrix_simple(){matrix.simple();}
	void rotate_z(double c){matrix.rotate_z(c);};
	void rotate_y(double b){matrix.rotate_y(b);};
	void rotate_x(double a){matrix.rotate_y(a);};
	void scale(double a){matrix.scale(a);}
	void rorate_vector_z(const Point & p){matrix.rotate_vector_z(p);}
	void position(const Point & p){matrix.position(p);}
	void perspective(const Point & from, const Point & to){matrix.perspective(from, to);};
};