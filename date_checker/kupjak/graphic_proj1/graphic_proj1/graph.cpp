#include "graph.h"


Sphere_point point_to_sphere_point(const Point & p){
	double phi = atan2(p.y, p.x);
	if (phi<0)
		phi += 2*pi;
	return Sphere_point(sqrt(p.x*p.x+p.y*p.y+p.z*p.z), acos(p.z/sqrt(p.x*p.x+p.y*p.y+p.z*p.z)), phi);
}

Point sphere_point_to_point(const Sphere_point & sp){
	return Point(sp.r*sin(sp.theta)*cos(sp.phi), sp.r*sin(sp.theta)*sin(sp.phi), sp.r*cos(sp.theta));
}

Rotation_point point_to_rotation_point(const Point & p){
	return Rotation_point(sqrt(p.x*p.x+p.y*p.y+p.z*p.z), atan2(p.z, p.y), atan2(p.x, p.z), atan2(p.y, p.x));
}


Point operator-(const Point & a, const Point & b){
	return Point(a.x - b.x, a.y - b.y, a.z - b.z);
}

Point operator+(const Point & a, const Point & b){
	return Point(a.x + b.x, a.y + b.y, a.z + b.z);
}


Point2 operator-(const Point2 & a, const Point2 & b){
	return Point2(a.x - b.x, a.y - b.y);
}

Point2 operator+(const Point2 & a, const Point2 & b){
	return Point2(a.x + b.x, a.y + b.y);
}

Point operator-(const Point & p){
	return Point(-p.x, -p.y, -p.z);
}

double length(const Point & p){
	return sqrt(p.x*p.x + p.y*p.y + p.z*p.z);
}

double length2(const Point2 & p){
	return sqrt(p.x*p.x + p.y*p.y);
}

Point2 operator*(const Point2 & p, const double d){
	return Point2(p.x*d, p.y*d);
}


Point & operator/=(Point & p, const double d){
	p.x /= d;
	p.y /= d;
	p.z /= d;
	return p;
}

Point & operator*=(Point & p, const double d){
	p.x *= d;
	p.y *= d;
	p.z *= d;
	return p;
}


Point & operator+=(Point & p1, const Point & p2){
	p1.x += p2.x;
	p1.y += p2.y;
	p1.z += p2.z;
	return p1;
}

Point & operator-=(Point & p1, const Point & p2){
	p1.x -= p2.x;
	p1.y -= p2.y;
	p1.z -= p2.z;
	return p1;
}

Rotation_point operator-(const Rotation_point & rp){
	return Rotation_point(rp.r, -rp.a, -rp.b, -rp.c);
}