#include <iostream>
#include <fstream>

#include "transporting.h"

using namespace std;

Transporting t;

int main(){
	ifstream file_in("transport.txt");
	file_in >> t;
	Graph g(t);
	g.max_flow_min_const();
	
	return 0;
}