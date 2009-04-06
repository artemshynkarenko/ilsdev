#include "grmanager.h"


//Grmanager
void Grmanager::display_frame(){
	//objects.perspective(screen.cam.from, screen.cam.to);
	objects.rotate_z(p);
	objects.rotate_y(t);
	objects.calc_all();
	objects.matrix_simple();
	screen.display_frame(objects);
	//Beep(1000, 1);
}

void Grmanager::init(){
	h_count = 10;
	l_count = 20;
	Object axis(255);
	Point O(0, 0, 0), X(500, 0, 0), Y(0, 500, 0), Z(0, 0, 500);
	axis.add_line(Line(O, X));
	axis.add_line(Line(O, Y));
	axis.add_line(Line(O, Z));
	objects.add(axis);

	Poligon p1("poligon1.plg");
	p1.split(l_count);

	Poligon p2("poligon2.plg");
	p2.split(l_count);
	
	add_curve(p1, p2);
	objects.add(Object(p1, 200));
	objects.add(Object(p2, 200));
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