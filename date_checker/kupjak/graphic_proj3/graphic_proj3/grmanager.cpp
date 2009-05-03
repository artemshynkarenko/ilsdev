#include "grmanager.h"


//Grmanager
void Grmanager::display_frame(){
	//objects.perspective(screen.cam.from, screen.cam.to);
	objects.rotate_z(p);
	objects.rotate_y(t);
	objects.scale(scale);
	objects.calc_all();
	objects.matrix_simple();
	screen.display_frame(objects);
	//Beep(1000, 1);
}

	double f_xy(double x, double y){
		double r = 2*x*x + y*y;
		return 7*cos(1.2*r)/(r+1);
	};


void Grmanager::init(){
	h_count = 10;
	l_count = 20;
	scale = 1;
	Object axis(RGB(90, 90, 90));
	Point O(0, 0, 0), X(500, 0, 0), Y(0, 500, 0), Z(0, 0, 500);
	axis.add_line(Line(O, X));
	axis.add_line(Line(O, Y));
	axis.add_line(Line(O, Z));
	//objects.add(axis);



	add_horizontal_surface(f_xy, RGB(10, 80, 10));

}

void Grmanager::add_horizontal_surface(Func2d f, const  int col){
	int x_count_of_point_surface = 70;
	int y_count_of_point_surface = 70;
	double x_range = 2*3.1314;
	double y_range = 2*3.1314;
	double dx = 2*x_range/x_count_of_point_surface;
	double dy = 2*y_range/y_count_of_point_surface;
	double scale = 35;


	Object o(0, col, false, true);
	for (int i = 0; i<x_count_of_point_surface;  ++i)
		for (int j = 1; j<y_count_of_point_surface;  ++j){
			double x = (-x_range + dx*i);
			double y = (-y_range + dy*j);
			double y_ = (-y_range + dy*(j-1));
			Point A(scale*x, scale*y_, scale*(f)(x, y_));
			Point B(scale*x, scale*y, scale*(f)(x, y));
			o.add_line(Line(A, B));
		}

	for (int j = 0; j<y_count_of_point_surface;  ++j)
		for (int i = 1; i<x_count_of_point_surface;  ++i){
			double x = (-x_range + dx*i);
			double y = (-y_range + dy*j);
			double x_ = (-x_range + dx*(i-1));
			Point A(scale*x_, scale*y, scale*(f)(x_, y));
			Point B(scale*x, scale*y, scale*(f)(x, y));
			o.add_line(Line(A, B));
		}



		objects.add(o);

	
}

void Grmanager::add_curve(const Poligon & p1, const Poligon &  p2){
	Point R(50, 50, 200);
	Point P;
	Point Q;
	double t;
	Object o1(p1);	
	Object o2(p2);	
	Object old;
	Object res;	
	cubic_spline cs;
	double x[1000];
	double y[1000];
	for (int i = 0; i < h_count+1; ++i){
		Object o = o1;	
		for (int j = 0; j<o1.size(); ++j){
			t = (double)i/h_count;
			P = Point(o1.source[j][0], o1.source[j][1], o1.source[j][2]);
			Q = Point(o2.source[j][0], o2.source[j][1], o2.source[j][2]);

			o.source[j] = t*t*(Q-2*R+P) + 2*t*(R-P) + P;
			for (int k = 0; k<3; ++k){				
				o.source[j][k] += (o2.source[j][k] - o1.source[j][k])*t;
			}
			x[i] = o.source[j][0];
			y[i] = o.source[j][1];
		}
		old = o;
	}

	cs.build_spline(x, y, h_count);

	for (int i = 0; i < h_count+1; ++i){
		Object o = o1;	
		for (int j = 0; j<o1.size(); ++j){
			t = (double)i/h_count;
			P = Point(o1.source[j][0], o1.source[j][1], o1.source[j][2]);
			Q = Point(o2.source[j][0], o2.source[j][1], o2.source[j][2]);
			o.source[j][1] = cs.f(o2.source[j][0]);
			for (int k = 0; k<3; ++k){
				o.source[j][k] += (o2.source[j][k] - o1.source[j][k])*t;
			}
			o.source[j] = t*t*(Q-2*R+P) + 2*t*(R-P) + P;
			
		}
		if (i>0){
			connect_two_objects(old, o, res);
		}
		old = o;
		res.marge(o);
	}
	objects.add(res);
}

void Grmanager::connect_two_objects(const Object & o1, const Object & o2, Object & res){
	for (int i=0; i<o1.size()-1; ++i){
		res.add_line(Line(Vector4d_to_Point(o1.source[i]), Vector4d_to_Point(o2.source[i])));
		res.add_line(Line(Vector4d_to_Point(o1.source[i]), Vector4d_to_Point(o2.source[i+1])));
	}
}

void Grmanager::move_up(){
	Point vect = (screen.cam.to - screen.cam.from);
	vect /= length(vect);
	vect *= 2;
	screen.cam.from += vect;
	screen.cam.to += vect;
	//Beep(1000, 1);
	p += 0.02;
}

void Grmanager::move_down(){
	Point vect = (screen.cam.to - screen.cam.from);
	vect /= length(vect);
	vect *= 2;
	screen.cam.from -= vect;
	screen.cam.to -= vect;
	p -= 0.02;
}

void Grmanager::move_left(){
	t += 0.02;
	
}

void Grmanager::move_right(){
	t -= 0.02;
}

void Grmanager::mouse_vertical(const int a){
	p += a/100.0;
	/*Point p = screen.cam.to - screen.cam.from;
	Sphere_point sp = point_to_sphere_point(p);
	Matrix44d m;
	m.rotate_z(-sp.phi);
	m.rotate_y(-(pi/2-sp.theta));
	sp.theta += a/1000.0;
	m.rotate_y((pi/2-sp.theta));
	m.rotate_z(sp.phi);
	p*=m;
	screen.cam.to  = screen.cam.from + p;
	*/
}

void Grmanager::mouse_horizontal(const int a){
	t += a/100.0;
	/*Point p = screen.cam.to - screen.cam.from;
	Sphere_point sp = point_to_sphere_point(p);
	Matrix44d m;
	m.rotate_z(-sp.phi);
	m.rotate_y(-(pi/2-sp.theta));
	sp.phi += a/1000.0;
	m.rotate_y((pi/2-sp.theta));
	m.rotate_z(sp.phi);
	p*=m;
	screen.cam.to  = screen.cam.from + p;
	*/
}

void Grmanager::scaled(const int a){
	scale += scale*a/1000.0;
}