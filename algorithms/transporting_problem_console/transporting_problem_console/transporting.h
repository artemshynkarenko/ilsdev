#include <iostream>

using namespace std;

const int INF = 10000000;

class Graph;

class Transporting{
	int m, n;
	int *a;
	int *b;
	int **c;
	int **x;
	int result;
	friend class Graph;
public:
	Transporting():m(0), n(0){}
	Transporting(int x, int y):m(x), n(y){init();}
	friend istream & operator>>(istream & is, Transporting & t);
	void init();
	void calc_defoult_plan();
	void get_result(Graph &);
	void free();
	void print_result();
};

class Graph{
	int n;
	int **weight;
	int **cost;
	int **flow;
	int *parent;
	int source, dest;
	int m_, n_;
	friend class Transporting;
	void print_debug_result();
public:
	Graph(): n(0){}
	Graph(const Transporting &);
	void init_matrix();
	void calc_defoult_plan();
	int max_flow_min_const();
	bool find_shortest_path(int s, int d);
};

