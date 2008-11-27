#include <iostream>
#include <fstream>
#include <string>
#include <list>
#include <stack>
#include <queue>
#include <algorithm>

using namespace std;


const int MAX_V = 400; // max count of vertices of graph

typedef pair<int, int> PII; // pair of two int (we use to store edge in list. Each vertex has a list of adjacency 
                            // edges (pair). First element of pair is a vertex, second is weight of edge)


// A graph is a structure of information. 
// It uses model pairwise relations between objects. Graph consists of a vertices set and a edges set. 
// Vertex can be any object, and relation between objects is a edge. Edge can be connected to vectices, or 
// one vertex to itself.
// A graph may be undirected (there is no defference in order of edge), or directed.
// for exemple, there is an edge e which connects vertex u and vertex v. e=(u, v). When graph is undirected, and 
// it has an edge e=(u, v), a vertex u is adjacent with a vertex v, and v is adjacent with u. In directed graph 
// e=(u, v) means only that u is adjacent with v. But we can add an edge e1=(v, u) and model an undirected graph.
// For simplification we can always use a directed graph, and add opposite enge to model an undirected graph.

// Weighted Graph is a graph, each edges has some weight.


int n;  // count of vertices

//There are 2 standard ways to represent a graph:

// 1. Adjacency matrix A. A[i, j] =1, when there is enge (i, j), and A[i, j] =0 otherwise.
int a[MAX_V][MAX_V]; // Adjacency matrix


// 2. Array of adjacency lists (for a sparse graph - where there isn't mane edges)
// Each vertex has a list of adjacency edges (pair) - l[i]. First element of pair is a vertex, second is weight of edge
list<PII> l[MAX_V]; // array of adjacency lists;


int parent[MAX_V];

int p[MAX_V];

void bfsM(int s){ 
	for (int i=0; i<n; ++i)
		parent[i] = -1;

	queue<int> q;
	q.push(s);
	parent[s] = s;
	while (!q.empty()){
		int v=q.back(); q.pop();
		for (int i=0; i<n; ++i)
			if (a[v][i] &&  parent[i]==-1){
				parent[i]= v;
				q.push(i);
			}
	}
}

void dfsM(int s){
	for (int i=0; i<n; ++i)
		parent[i] = -1;

	stack<int> st;
	st.push(s);
	parent[s] = s;
	while (!st.empty()){
		int v=st.top(); st.pop();
		for (int i=0; i<n; ++i)
			if (a[v][i] &&  parent[i]==-1){
				parent[i]= v;
				st.push(i);
			}
	}
}


int main(){

	ifstream filein("graph.txt"); // input graph. format: n(count of vertices)  m(count of edge)  a1 b1 c1   u2 v2 w2 ... um vm wm  ( there are edges (ui, vi) whith weight w) 
	
	filein >> n; // read count of vertices in graph
	for (int i=0; i<n; ++i)
		for (int j=0; j<n; ++j)
			a[i][j]=0;          // initialization of matrix - there aren't eny edges before

	int m;     // count of all 	edges
	filein >> m;
	for (int i=0; i<m; ++i){
		int u, v, w;
		filein >> u >> v >> w; // read edge
		a[u-1][v-1] = a[v-1][u-1] = w;     // store adge in adjacency matrix

		l[u].push_back(PII(v, w)); // store in  array of adjacency lists
	}

	cout << "There is a graph (graph.txt), represented like adjacency matrix : " <<endl;
	cout << "  ";  
	for (int j=0; j<n; ++j)
		cout << j+1 << " "; //
	cout << endl;
	for (int i=0; i<n; ++i){
		cout << i+1 << " ";
		for (int j=0; j<n; ++j)
			cout << a[i][j] << " ";
		cout << endl;
	}

	int source=0;
	dfsM(source);	// 

	int v=3;
	cout << "shortest path from " << source+1 << " to " << v+1 << " is (reverse): ";
	while (parent[v]!=v){
		cout << v+1 << "-";
		v = parent[v];
	}
	cout << v+1;



	return 0;
}