#pragma once
#include <iostream>
#include <cmath>


using namespace std;

const double pi = 3.1415926535897932384626433832795;
//const double PI = acos(-1.0);

const double eps = 1e-7;

struct Point2{
	double x;
	double y;
	Point2(): x(0), y(0){}
	Point2(const double & _x, const double & _y): x(_x), y(_y){}
	friend istream & operator >> (istream & is, Point2 & p){is >> p.x >> p.y; return is;}
};


struct Point{
	double x;
	double y;
	double z;
	Point(): x(0), y(0), z(0){}
	Point(const double & _x, const double & _y, const double & _z): x(_x), y(_y), z(_z){}
	Point(const Point2 p2): x(p2.x), y(p2.y), z(0){}
	friend istream & operator >> (istream & is, Point & p){is >> p.x >> p.y >> p.z; return is;}
};

Point operator-(const Point & a, const Point & b);
Point operator-(const Point & p);
Point operator+(const Point & a, const Point & b);
Point operator*(const double d, const Point & p);
Point & operator+=(Point & p1, const Point & p2);
Point & operator-=(Point & p1, const Point & p2);

Point & operator/=(Point & p, const double d);
Point & operator*=(Point & p, const double d);
double length(const Point & p);

double length2(const Point2 & p);
Point2 operator-(const Point2 & a, const Point2 & b);
Point2 operator*(const Point2 & p, const double d);
Point2 operator+(const Point2 & a, const Point2 & b);


struct Sphere_point{
	double r;
	double theta;
	double phi;
	Sphere_point(): r(0), theta(0), phi(0){}
	Sphere_point(const double & _r, const double & _theta, const double & _phi): r(_r), theta(_theta), phi(_phi){}
};

struct Rotation_point{
	double r;
	double a, b, c;
	Rotation_point(): r(0), a(0), b(0), c(0){}
	Rotation_point(const double & _r, const double & _a, const double & _b, const double & _c): r(_r), a(_a), b(_b), c(_c){}
};

Rotation_point operator-(const Rotation_point & rp);


Sphere_point point_to_sphere_point(const Point & p);
Point sphere_point_to_point(const Sphere_point & sp);
Rotation_point point_to_rotation_point(const Point & p);

struct Point2i{
	int x;
	int y;
	Point2i(): x(0), y(0){}
	Point2i(const int _x, const int _y): x(_x), y(_y){}
};


struct Line{
	Point begin;
	Point end;
	Line(const Point & a, const Point & b): begin(a), end(b){} 
};

