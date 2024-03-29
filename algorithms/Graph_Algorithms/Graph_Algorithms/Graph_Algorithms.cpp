#include <iostream>
#include <fstream>
#include <string>
#include <list>
#include <stack>
#include <queue>
#include <algorithm>

using namespace std;


const int MAX_V = 400; // max count of vertices of graph

typedef pair<int, int> PII; // pair of two int
// We use it to store weight edge in the list. Each vertex has a list of adjacency 
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

// Weighted Graph is a graph, each edges has some weight. In unweighted graph, we suppose that weight of all 
// vertices is 1.


int n;  // count of vertices

//There are 2 standard ways to represent a graph:

// 1. Adjacency matrix A. A[i, j] =1, when there is enge (i, j), and A[i, j] =0 otherwise.
int a[MAX_V][MAX_V]; // Adjacency matrix


// 2. Array of adjacency lists (for a sparse graph - where there isn't mane edges)
// Each vertex has a list of adjacency edges (pair) - l[i]. First element of pair is a vertex, second is weight of edge

list<int> l[MAX_V]; // array of adjacency lists of graph (unweighted graph);

list<PII> lw[MAX_V]; // array of adjacency lists of weighted graph;


int parent[MAX_V]; // searching tree

// "Breadth-first search (BFS) is a graph search algorithm that begins at the root node and explores all the neighboring nodes.
// Then for each of those nearest nodes, it explores their unexplored neighbor nodes, and so on, until it finds the goal." (From Wikipedia, the free encyclopedia)

// "Depth-first search (DFS) is an algorithm for traversing or searching a tree, tree structure, or graph. One starts at the root (selecting some node as the root
// in the graph case) and explores as far as possible along each branch before backtracking." (From Wikipedia, the free encyclopedia)

// in BFS and DFS, we procces all vertices once. In proccesing current vertex, we find all not-visited adjacency
// vertices to the current vertex and we add them to proccesing set (on some order). BFS proccesing set is queue 
// (FIFO - first in, first out).  DFS proccesing set is stack (LIFO - last in, first out) . 


// in BFS and DFS to simplify, we use unweighted graph. 

// BFS algorithm by adjacency matrix
void bfsM(int s){ // BFS algorithm takes source vertex "s" as input and use global graph(  Adjacency matrix and count if vertices )
	              // As result, BFS algorithm changes the array "parent", which represents the searching tree.
	for (int i=0; i<n; ++i)
		parent[i] = -1;  // previous vertex of i


	queue<int> q;
	q.push(s);     // first processing-vertex
	
	parent[s] = s; // the root of searching tree
	while (!q.empty()){
		int v=q.back();  q.pop(); // take current proccesing vertex from queue
		for (int i=0; i<n; ++i)   // find all not-visited adjacency vertices to the current vertex
			if (a[v][i] &&  parent[i]==-1){ // if is not-visited adjacency vertices to the current vertex
				parent[i]= v; // store previous vertex from which we came (from v to i)
				q.push(i);    // add to the queue
			}
	}
}

// DFS algorithm by adjacency matrix
void dfsM(int s){ // DFS algorithm takes source vertex "s" as input and use global graph(  Adjacency matrix and count if vertices )
	              // As result, DFS algorithm changes the array "parent", which represents the searching tree.
	for (int i=0; i<n; ++i)
		parent[i] = -1;  // previous vertex of i

	stack<int> st;
	st.push(s);  // first processing-vertex
	parent[s] = s;  // the root of searching tree
	while (!st.empty()){
		int v=st.top(); st.pop();  // take current proccesing vertex from stack
		for (int i=0; i<n; ++i)   // find all not-visited adjacency vertices to the current vertex
			if (a[v][i] &&  parent[i]==-1){  // if is not-visited adjacency vertices to the current vertex
				parent[i]= v;  // store previous vertex from which we came (from v to i)
				st.push(i); // add to the stack
			}
	}
}

// BFS algorithm by array of adjacency lists
void bfsL(int s){ // BFS algorithm takes source vertex "s" as input and use global graph(Array of adjacency lists and count if vertices )
	              // As result, BFS algorithm changes the array "parent", which represents the searching tree.
	for (int i=0; i<n; ++i)
		parent[i] = -1;  // previous vertex of i


	queue<int> q;
	q.push(s);     // first processing-vertex
	
	parent[s] = s; // the root of searching tree
	while (!q.empty()){
		int v=q.back();  q.pop(); // take current proccesing vertex from queue
		for (list<int>::const_iterator it=l[v].begin(); it != l[v].end(); ++it)   // find all not-visited adjacency vertices to the current vertex
			if (parent[*it]==-1){ // if is not-visited adjacency vertices to the current vertex
				parent[*it]= v;; // store previous vertex from which we came (from v to i)
				q.push(*it);   // add to the queue
			}
	}
}

// DFS algorithm by array of adjacency lists
void dfsL(int s){ // DFS algorithm takes source vertex "s" as input and use global graph(Array of adjacency lists and count if vertices )
	              // As result, DFS algorithm changes the array "parent", which represents the searching tree.
	for (int i=0; i<n; ++i)
		parent[i] = -1;  // previous vertex of i

	stack<int> st;
	st.push(s);  // first processing-vertex
	parent[s] = s;  // the root of searching tree
	while (!st.empty()){
		int v = st.top(); st.pop();  // take current proccesing vertex from stack
		for (list<int>::const_iterator it=l[v].begin(); it != l[v].end(); ++it)   // find all not-visited adjacency vertices to the current vertex
			if (parent[*it]==-1){  // if is not-visited adjacency vertices to the current vertex
				parent[*it]= v;  // store previous vertex from which we came (from v to i)
				st.push(*it); // add to the stack
			}
	}
}


int dest[100];

int primM(){
	for (int v=0; v<n; ++v){
		parent[v] = v;
		dest[v] = 1000000000;
	}

	priority_queue<PII, vector<PII >, greater<PII > > q;
	q.push(make_pair(0, 0));
	dest[0]=0;

	int res = 0;

	while (!q.empty()){

		int v = q.top().second;
		int d = q.top().first;
		q.pop();
		
		if (d <= dest[v]){ 

			if (parent[v]!=-1)
				res += a[parent[v]][v];

			for (int v2=0; v2<n; ++v2)
				if (a[v][v2]>0){
					if (a[v][v2]<dest[v2]){
						dest[v2] = a[v][v2];
						q.push(make_pair(dest[v2], v2));
						parent[v2] = v;
					}
				}
		}
				
	}
	return res;
}

void dijkstraM(int s){

	for (int v=0; v<n; ++v){
		parent[v] = v;
		dest[v] = 1000000000;
	}

	priority_queue<PII, vector< PII >, greater< PII > > q;
	dest[s]=0;
	q.push(make_pair(dest[s], s));
	

	while (!q.empty()){
		int v = q.top().second;
		int d = q.top().first;
		q.pop();
		
		if (d <= dest[v]){ 

			for (int v2=0; v2<n; ++v2)
				if (a[v][v2]>0){
					if (a[v][v2] < dest[v2]){
						dest[v2] = a[v][v2];
						q.push(make_pair(dest[v2], v2));
						parent[v2] = v;
					}
				}
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
		int u, v;
		filein >> u >> v; // read edge
		a[u-1][v-1] = a[v-1][u-1] = 1;     // store adge in adjacency matrix

		l[u].push_back(v); // store in  array of adjacency lists
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
	cout << "shortest path from " << source+1 << " to " << v+1 << " by BFS is (reverse): ";
	while (parent[v]!=v){
		cout << v+1 << "-";
		v = parent[v];
	}
	cout << v+1 << endl << endl;

	source=0;
	dijkstraM(source);	// 

	v=3;
	cout << "shortest path from " << source+1 << " to " << v+1 << " by Dijkstra algorithm is (reverse): ";
	while (parent[v]!=v){
		cout << v+1 << "-";
		v = parent[v];
	}
	cout << v+1 << endl << endl;


	return 0;
}