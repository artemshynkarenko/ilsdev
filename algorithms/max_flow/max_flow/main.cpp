#include <iostream>
#include <iomanip>
#include <fstream>
#include <string>
#include <queue>

using namespace std;

const int MAX_V = 400;
const int INF = 100000000;

int c[MAX_V][MAX_V];	// Adjacency matrix of graph
int f[MAX_V][MAX_V];	// Adjacency matrix of flow
int parent[MAX_V];		// parent[i] - previous vertex of path, that finding in funñtion exist_path
int n;					// Count of vertices (0-based) 
int start, finish;


bool exist_path(int source, int dest){ // find path from source to destination that can increase the flow
	for (int i=0; i<n; ++i)
		parent[i] = -1;     // in begin, threre isn't a path, and any vertex 
	/* BFS searching from source in graph with adjacency matrix (c-f)*/
	queue<int> q;	
	q.push(source);		
	parent[source] = source;
	while (! q.empty()){
		int v = q.front();
		q.pop();
		for (int i=0; i<n; ++i)
			if (parent[i]==-1 && (c[v][i]-f[v][i]>0)){
				parent[i] = v;
				q.push(i);
			}
	}
	return parent[dest]!=-1; // return true if reach the vertex dest (and there is a path, we store path in array parent)
}

int max_flow(int source, int dest){ // return weight of maximum flow from source to destination (all flow is store in adjacency matrix f)
	for (int i=0; i<n; ++i)
		for (int j=0; j<n; ++j)
			f[i][j]=0;  // in begin, the flow is empty;

	while (exist_path(source, dest)){
		int cm = INF;  // find minimum weight in next iteration, that we can increment all path;
		for (int i=dest; i!=source; i=parent[i])	// itarate all edge in path from destination to source (current edge is (parent[i],i) ) 
			cm = min(cm, c[parent[i]][i]-f[parent[i]][i]);
		for (int i= dest; i!=source; i=parent[i]){
			f[parent[i]][i] += cm;   // increment flow of all edge in path
			f[i][parent[i]] = - f[parent[i]][i];  // when we have a flow from i to j [i][j], the flow from j to i is f[j][i]=-f[i][j]
		}
	}

	int res=0; // weight of flow
	for (int i=0; i<n; ++i) 
		res += f[source][i]; // total weight is we sum of flow from source vertex  
	return res;
}

void read_graph(char * file_name){
	ifstream fin(file_name);
	int m; // count  of edges
	fin >> n >> start >> finish >> m;
	for (int i=0; i<m; ++i){
		int a, b, w;
		fin >> a >> b >> w; 
		//--a; --b; // vertices must be 0-based. if vertices isn't 0-based, increment all.
		c[a][b] = w;
	}
}

void print_matrix(int m[MAX_V][MAX_V]){
	cout << "  ";
	for (int j=0; j<n; ++j)
		cout << setw(2) << j;
	cout << endl;
	for (int i=0; i<n; ++i){
		cout << setw(2) << i;
		for (int j=0; j<n; ++j)
			cout << setw(2) << m[i][j];
		cout << endl;
	}
}

int  main(){
	read_graph("graph1.txt");

	cout << "Adjacency matrix of graph1 is:" << endl;
	print_matrix(c);

	int mf1 = max_flow(start, finish);

	cout << "The matrix of max flow of graph1 is:" << endl;
	print_matrix(f);

	cout << "The max flow of graph1 is " << mf1 << endl;
	
	cout << endl; 

	read_graph("graph2.txt");

	cout << "Adjacency matrix of graph2 is:" << endl;
	print_matrix(c);

	int mf2 = max_flow(start, finish);

	cout << "The matrix of max flow of graph2 is:" << endl;
	print_matrix(f);

	cout << "The max flow of graph2 is " << mf2 << endl;
	return 0;
}