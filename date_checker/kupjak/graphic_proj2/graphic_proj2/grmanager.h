#include "screen.h"
#include "graph.h"
#include "objects.h"

#include "spline.h"

class Grmanager{
	Objects objects;
	Screen screen;
	double p, t;
	void connect_two_objects(const Object & o1, const Object & o2, Object & res);
	void add_curve(const Poligon & p1, const Poligon & p2);
	int h_count;
	int l_count;
public:
	Grmanager(){init();}
	Grmanager(HWND hw, const int w, const int h): screen(hw, w, h){init();}
	void set(HWND hw, const int w, const int h){screen.set(hw, w, h);}
	void init();
	int get_msec_per_frame()const {return screen.msec_per_frame;}
	void display_frame();
	
	void move_up();
	void move_down();
	void move_left();
	void move_right();
	void mouse_vertical(const int a);
	void mouse_horizontal(const int a);
};