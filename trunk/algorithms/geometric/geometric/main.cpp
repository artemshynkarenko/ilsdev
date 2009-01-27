#include <iostream>
#include <fstream>
#include <cmath>
#include <vector>

#include "geom.h"

using namespace std;


int main(){
	vector<Point<int>> points;
	ifstream("file1.txt") >> points;  // there is overload template operator >> for istream and vector<Point<T>> in geom.h 
	
	cout << "Points from file1.txt : " << endl;
	cout << points << endl; // there is overload template operator << for ostream and vector<Point<T>> in geom.h
	cout << "The area of polygon is " << area2(points)/2.0 << endl;
	
	points.clear();
	ifstream("file2.txt") >> points;

	cout << "Points from file2.txt : " << endl;
	cout << points << endl;

	vector<Point<int>> res;
	cout << "Convex hull using Graham algorithm is: " << endl;
	c_h_graham(points, res);
	cout << res << endl;

	cout << "Convex hull using Jarvis algorithm is: " << endl;
	res.clear();
	c_h_jarvis(points, res);
	cout << res << endl;


	return 0;
}