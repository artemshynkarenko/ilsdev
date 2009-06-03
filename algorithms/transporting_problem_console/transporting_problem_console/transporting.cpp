#include <iostream>
#include <iomanip>

#include "transporting.h"

using namespace std;

istream & operator>>(istream & is, Transporting & t){
	t.free();
	is >> t.m >> t.n;
	t.init();
	for (int i=0; i<t.m; ++i)
		is >> t.a[i];
	for (int i=0; i<t.n; ++i)
		is >> t.b[i];
	
	for (int i=0; i< t.m; ++i)
		for (int j=0; j< t.n; ++j)
			is >> t.c[i][j];
	return is;
}

void Transporting::init(){
	a = new int[m];
	b = new int[n];
	x = new int*[m];
	c = new int*[m];
	for (int i=0; i<m; ++i){
		c[i] = new int [n];
		x[i] = new int [n];
	}
	for (int i=0; i<m; ++i)
		for (int j=0; j<n; ++j)
			c[i][j] = x[i][j] = 0;
}

void Transporting::free(){
	if (a != 0)
		delete []a;
	if (b != 0)
		delete []b;
	if (c != 0){
		for (int i=0; i<m; ++i)
			if (c[i] != 0)
				delete [] (c[i]);
		delete []c;
	}
	if (x != 0){
		for (int i=0; i<m; ++i)
			if (x[i] != 0)
				delete [] (x[i]);
		delete []x;
	}
	m = 0;
	n = 0;
}

void Transporting::get_result(Graph & g){
	if (g.n<2)
		return;
	for (int i=0; i<m; ++i)
		for (int j=0; j<n; ++j)
			x[i][j]= g.flow[i][j+m];
}

void Transporting::print_result(){
	result = 0;
	for (int i=0; i<m; ++i)
		for (int j=0; j<n; ++j)
			result += x[i][j] * c[i][j];
	cout << "The cost of transporting is " << result << endl;
	for (int i=0; i<m; ++i){
		for (int j=0; j<n; ++j)
			cout <<  setw(3) << x[i][j] << " ";
		cout << endl;
	}
	cout << endl;
}

void Graph::init_matrix(){
	parent = new int[n];
	cost = new int*[n];
	weight = new int*[n];
	flow = new int*[n];
	for(int i=0; i<n; ++i){
		cost[i] = new int[n];
		weight[i] = new int[n];
		flow[i] = new int[n];
	}
	for (int i=0; i<n; ++i)
		for (int j=0; j<n; ++j)
			cost[i][j] = weight[i][j] = flow[i][j] = 0;
}

Graph::Graph(const Transporting & t){
	m_ = t.m;
	n_ = t.n;
	n = t.m + t.n + 2;
	source = t.m + t.n;
	dest = t.m + t.n + 1;
	init_matrix();
	int *fa = new int[t.m];
	int *fb = new int[t.n];
	for (int i=0; i<t.m; ++i){
		fa[i] = 0;
		for (int j=0; j<t.n; ++j)
			fa[i] += t.x[i][j];
	}
	for (int j=0; j<t.n; ++j){
		fb[j] = 0;
		for (int i=0; i<t.m; ++i)
			fb[j] += t.x[i][j];
	}

	for (int i=0; i<t.m; ++i){
		cost[source][i] = 0;
		weight[source][i] = t.a[i];
		flow[source][i] = fa[i];
		flow[i][source] = -fa[i];
	}
	for (int i=0; i<t.n; ++i){
		cost[i+t.m][dest] = 0;
		weight[i+t.m][dest] = t.b[i];
		flow[i+t.m][dest] = fb[i];
		flow[dest][i+t.m] = - fb[i];
	}
	for (int i=0; i<t.m; ++i)
		for (int j=0; j<t.n; ++j){
			cost[i][j+t.m] = t.c[i][j];
			weight[i][j+t.m] = INF-1;
		}

/*	for (int i=0; i<t.m; ++i)
		for (int j=0; j<t.n; ++j){
			flow[i][j+t.m] = t.x[i][j];
			flow[j+t.m][i] = -t.x[i][j];
		}

	for (int i=0; i<n; ++i){
		for (int j=0; j<n; ++j)
			cout << flow[i][j] << " ";
		cout << endl;
	}
	*/
}

void Transporting::calc_defoult_plan(){
	int s = a[0];
	for (int i=0, j=0; i<m && j<n; ){
		if (s >= b[j]){
			x[i][j] = b[j];
			s -= b[j];
			++j;
		}
		else{
			s += a[i];
			++i;
		}
	}
}

void Graph::print_debug_result(){
	Transporting t(m_, n_);
	t.x = new int*[t.m];
	t.c = new int*[t.m];
	for (int i=0; i<t.m; ++i){
		t.x[i] = new int [t.n];
		t.c[i] = new int [t.n];
	}
	for (int i=0; i<t.m; ++i)
		for (int j=0; j<t.n; ++j)
			t.c[i][j] = cost[i][j+t.m];
	t.get_result(*this);
	t.print_result();
}

int Graph::max_flow_min_const(){

	print_debug_result();

	while (find_shortest_path(source, dest)){
		int cm = INF;  // find minimum weight in next iteration, that we can increment all path;
		for (int i=dest; i!=source; i=parent[i])	// itarate all edge in path from destination to source (current edge is (parent[i],i) ) 
			cm = min(cm, weight[parent[i]][i]-flow[parent[i]][i]);
		for (int i= dest; i!=source; i=parent[i]){
			flow[parent[i]][i] += cm;   // increment flow of all edge in path
			flow[i][parent[i]] = - flow[parent[i]][i];  // when we have a flow from i to j [i][j], the flow from j to i is f[j][i]=-f[i][j]
		}
		
		print_debug_result();
	}

	int res=0; // weight of flow
	for (int i=0; i<n; ++i) 
		res += flow[source][i]; // total weight is we sum of flow from source vertex  
	return res;
}

bool Graph::find_shortest_path(int source, int dest){
	int *d = new int[n];
	for (int i=0; i<n; ++i)
		d[i] = INF;
	d[source] = 0;
	for(int k=0; k<n; ++k){
		int b = false;
		for (int i=0; i<n; ++i)
			for(int j=0; j<n; ++j)
				if (i!=j && weight[i][j]-flow[i][j]>0)
					if (d[j]>d[i]+cost[i][j]){
						d[j] = d[i] + cost[i][j];
						parent[j] = i;
						b = true;
					}
		if (!b)
			return d[dest] != INF;
	
	}
	int b = false;
	for (int i=0; i<n; ++i)
		for(int j=0; j<n; ++j)
			if (i!=j  && weight[i][j]-flow[i][j]>0){
				if (d[j]>d[i]+cost[i][j])
					return false;
			}

	return d[dest] != INF;
}
