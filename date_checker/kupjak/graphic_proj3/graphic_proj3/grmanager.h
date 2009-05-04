#include "screen.h"
#include "graph.h"
#include "objects.h"

#include "spline.h"


typedef double (*Func2d)(double,double,double);

class Grmanager{
	Objects objects;
	Screen screen;
	double p, t;
	double scale;
	void connect_two_objects(const Object & o1, const Object & o2, Object & res);
	void add_curve(const Poligon & p1, const Poligon & p2);
	int h_count;
	int l_count;
	bool function_pause;
	double time;
	
public:
	Grmanager(){init();}
	Grmanager(HWND hw, const int w, const int h): screen(hw, w, h){init();}
	void set(HWND hw, const int w, const int h){screen.set(hw, w, h);}
	void init();
	void clear();
	int get_msec_per_frame()const {return screen.msec_per_frame;}
	void display_frame();
	void add_horizontal_surface(Func2d f, const  int col);
	void move_up();
	void move_down();
	void move_left();
	void move_right();
	void mouse_vertical(const int a);
	void mouse_horizontal(const int a);
	void change_pause(){function_pause = !function_pause;}
	void change_line_style(){screen.line_api = !screen.line_api;}
	void change_light(){screen.light = !screen.light;};
	void inc_function(double d);
	void scaled(const int a);
};