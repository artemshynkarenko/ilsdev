#include <iostream>
#include "sorting.h"

using namespace std;

char a[3000];
int n=3000;

int main(){

	for (int i=0; i<n; ++i)
		a[i]='a'+ rand()%26;
	cout<<endl<<"buble sort"<<endl;
	buble_sort(a,n);
	for (int i=0; i<n; ++i)
		cout<<a[i];
	cout<<endl;

	for (int i=0; i<n; ++i)
		a[i]='a'+ rand()%26;
	cout<<endl<<"insert sort"<<endl;
	insert_sort(a,n);
	for (int i=0; i<n; ++i)
		cout<<a[i];
	cout<<endl;

	for (int i=0; i<n; ++i)
		a[i]='a'+ rand()%26;
	cout<<endl<<"select sort"<<endl;
	select_sort(a,n);
	for (int i=0; i<n; ++i)
		cout<<a[i];
	cout<<endl;
	
	for (int i=0; i<n; ++i)
		a[i]='a'+ rand()%26;
	cout<<endl<<"merge sort"<<endl;
	merge_sort(a,n);
	for (int i=0; i<n; ++i)
		cout<<a[i];
	cout<<endl;


	for (int i=0; i<n; ++i)
		a[i]='a'+ rand()%26;
	cout<<endl<<"quick sort"<<endl;
	quick_sort(a, 0, n-1);
	for (int i=0; i<n; ++i)
		cout<<a[i];
	cout<<endl;
	
	return 0;
}