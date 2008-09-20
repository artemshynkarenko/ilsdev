#include <iostream>
#include <algorithm>
#include <vector>
#include "my_sorting.h"

using namespace std;

int main(){
	int n=10000;
	//vector<char> a(n);
	char *a=new char[n];

	for (int i=0; i<n; ++i)
		a[i]= 'a'+i%26;

	//bsort(a, a+n);
	quickSort(a, 0, n);

	for (int i=0; i<n; ++i)
		cout<<a[i];
	return 0;
}