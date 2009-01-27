#include <iostream>
#include <cmath>
#include <algorithm>

using namespace std;

template <typename T>
struct Point {
	T x, y;
	Point(const T _x=0, const T _y=0): x(_x), y(_y){}
	Point operator-(const Point<T> & p)const{return Point(x-p.x, y-p.y);}
	Point operator+(const Point<T> & p)const{return Point(x+p.x, y+p.y);}
	Point & operator-=(const Point<T> & p){x-=p.x; y-=p.y; return *this;}
	Point & operator+=(const Point<T> & p){x+=p.x; y+=p.y; return *this;}
};


template <typename T> T cross_prod(const Point<T> & p1, const Point<T> & p2); // cross product of 2 vectors
template <typename T> double dist(const Point<T> & p); // distance of vector
template <typename T> T dot_prod(const Point<T> & p1, const Point<T> & p2); // dot product of 2 vector
template <typename T> Point<T> find_first_pt(vector<Point<T>> & points); // find first left-bottom point
template <typename T> T area2(const vector<Point<T>> & points);  // compute 2*area of poligon
template <typename T> bool comp(const Point<T> & p1, const Point<T> & p2); // compate to point for building convex hull
template <typename T> double sin_ps(const Point<T> & p1, const Point<T> & p2); // compute sin of 2 vectors
template <typename T> void c_h_jarvis(vector<Point<T>> points, vector<Point<T>> & res); // build convex hull by Jarvis algorithm
template <typename T> void c_h_graham(vector<Point<T>> points, vector<Point<T>> & res); // build convex hull by Graham algorithm
template <typename T> istream & operator >> (istream & is, vector<Point<T>> & points); // read point
template <typename T> ostream & operator << (ostream & os, const vector<Point<T>> & points); // print point


template <typename T>
T cross_prod(const Point<T> & p1, const Point<T> & p2){
	return p1.x*p2.y - p2.x*p1.y;
}

template <typename T>
double dist(const Point<T> & p){
	return sqrt(double(p.x*p.x + p.y*p.y));
}


template <typename T>
T dot_prod(const Point<T> & p1, const Point<T> & p2){
	return p1.x*p2.x + p1.y*p2.y;
}

template <typename T>
Point<T> find_first_pt(vector<Point<T>> & points){
	Point<T> res=points[0];
	for (int i=1; i<(int)points.size(); ++i)
		if (points[i].x < res.x || (points[i].x == res.x && points[i].y < res.y))
			res = points[i];
	return res;
}

template <typename T>
T area2(const vector<Point<T>> & points){
	int n = points.size();
	T res=0;
	for (int i=0; i<n-1; ++i)
		res += cross_prod(points[i], points[i+1]);
	res += cross_prod(points[n-1], points[0]);
	return abs(res);
}

template <typename T>
bool comp(const Point<T> & p1, const Point<T> & p2){
	T sn = sin_ps(p1, p2);
	return sn>0 || (sn==0 && dist(p1)<dist(p2));
}

template <typename T> double sin_ps(const Point<T> & p1, const Point<T> & p2){
	return cross_prod(p1, p2)/dist(p1)/dist(p2);
}


template <typename T>
void c_h_jarvis(vector<Point<T>>  points, vector<Point<T>> & res){
	res.clear();
	if (points.size()==0) return;
	int n = points.size();
	Point<T> first = find_first_pt(points);
	for (int i=0; i<n; ++i)
		points[i] -= first;

	sort(points.begin()+1, points.end(), comp<T>);
	res.push_back(points[0]);
	res.push_back(points[1]);


	int i=2;
	while (i<n) {
		Point<T> p = points[i];
		double sn = sin_ps(points[i-1]-points[i-2], p-points[i-2]);
		int ind = i;
		for (int j = i+1; j<n; ++j)
			if (sin_ps(points[i-1]-points[i-2], points[j]-points[i-2]) < sn){
				p = points[j];
				sn = sin_ps(points[i-1]-points[i-2], points[j]-points[i-2]);
				ind = j;
			}
		i = ind+1;
		res.push_back(p);
	}
	for (int i=0; i<(int)res.size(); ++i)
		res[i] += first; 
}


template <typename T>
void c_h_graham(vector<Point<T>> points, vector<Point<T>> & res){
	res.clear();
	if (points.size()==0) return;
	int n = points.size();
	Point<T> first = find_first_pt(points);
	for (int i=0; i<n; ++i)
		points[i] -= first;

	sort(points.begin()+1, points.end(), comp<T>);
	res.push_back(points[0]);
	res.push_back(points[1]);

	int i=2, m=2;
	while (i<n){
		if (cross_prod(res[m-1]-res[m-2], points[i]-res[m-2])>=0){
			++m;
			res.push_back(points[i]);
			++i;
		}
		else{
			res.pop_back();
			--m;
		}
	}
	for (int i=0; i<(int)res.size(); ++i)
		res[i] += first; 
}

template <typename T>
istream & operator >> (istream & is, vector<Point<T>> & points){
	int n;
	is >> n;
	points.resize(n);
	for (int i=0; i<n; ++i)
		is >> points[i].x >> points[i].y;
	return is;
}

template <typename T>
ostream & operator << (ostream & os, const vector<Point<T>> & points){
	int n = points.size();
	os << "{";
	for (int i=0; i<n-1; ++i)
		os << "(" << points[i].x << "," << points[i].y << "), ";
	if (n>0)
		os << "(" << points[n-1].x << "," << points[n-1].y << ")";
	os << "}";
	return os;
}