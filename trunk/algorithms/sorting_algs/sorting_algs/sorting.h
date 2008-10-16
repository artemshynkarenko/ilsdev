#include <iostream>

using namespace std;

template<class T>
void buble_sort(T a[], int n){
	for (int i=0; i<n-1; ++i)  // (n-1) times...
		for (int j=1; j<n; ++j) // for each neighbour...
			if (a[j]<a[j-1]) swap(a[j], a[j-1]); // direct
}

template<class T>
void insert_sort(T a[], int n){
	for (int i=1; i<n; ++i)     // 
		a[0] = min(a[0], a[i]); //  find min element and replace to begin
	// set of 2 elements (a[0], a[1]) is sorted
	for (int i=2; i<n; ++i)  // all elements a[2]...a[n-1] is inserting in sorted sets
		for (int j=i; a[j-1]>a[j]; --j) // inserting
			swap(a[j-1], a[j]);
}

template<class T>
void select_sort(T a[], int n){
	for (int i=0; i<n-1; ++i)  // find min element, then find min element from ather without min, ... 
		for (int j=i+1; j<n; ++j) // find min element from {a[i+1], ..., a[n-1]} and replace in valid position
			if (a[j]<a[i]) swap(a[i], a[j]);
}


template<class T>
void merge(T a[], int n, T b[], int m, T c[]){ // marging 2 sorted array to one sorted
	int i=0, j=0, len=0; 
	while (i<n && j<m)
		if (a[i]<b[j])    // push less element   (a[i] - non-used min element from a[], b[j] - ...)
			c[len++]=a[i++]; // push first non-used element from a[]
		else
			c[len++]=b[j++]; //push first non-used element from a[]
	//loop is over when one of the array (a[] or b[]) is over, but another array isn't over
	// one if the next loops will run, becouse another array is over in previous loop!
	while (i<n) c[len++]=a[i++]; // pushing all non-used elements form a[]
	while (j<m) c[len++]=b[j++]; // pushing all non-used elements form b[]
}
template<class T>
void merge_sort(T a[], int n){
	if (n>1){
		int middle=n/2;                 // get middle element (middle element is begin of right part, first - begin of left part)
		merge_sort(a, middle);          // sort left part
		merge_sort(a+middle, n-middle); // sort reight part 
		T* sorted = new T[n];           // new temporary array, where function merge write merger element of 2 parts
		merge(a, middle, a+middle, n-middle, sorted); // merge 2 parts
		for (int i=0; i<n; ++i) 
			a[i]=sorted[i];             // replace resoult of merging to array a[]
		delete []sorted;				// delete temporary array		
	}
	
}


template<class T>
int partition(T a[], int l, int r){ // after working, all elements on the left of a[p] is less then a[p], and elements of the right if a[p] if great then a[p]
	                         // and p - is the result of function (a[0] in begin of function is a[p] in end of function)
	int pivot=a[r];  // pivot element alvays is in position r, in end of function pivot element is replace in good order
	int i=l-1;       // i - bound:  a[0],...,a[i] <=pivot ; a[i+1],...,a[j-1] > pivot  
	for (int j=l; j<=r-1; ++j)  // all element is replase on a bound in valid place (<=pivot or >pivot)
		if (a[j]<=pivot){
			++i;
			swap(a[i], a[j]);
		}
	swap(a[i+1], a[r]); //replace pivot in valid place
	return i+1;  // return position of pivot
}
template<class T>
void quick_sort(T a[], int l, int r){  // sort array a[] in range [l, r]
	if (l<r){ // if it's false, then it isn't necessary  to sort, becouse array of 1 or less element is sorted
		int p=partition(a, l, r);  
		// a[p] is in valid position, becouse all less elements is before a[p] and all great element is after a[p]
		quick_sort(a, l, p-1); // sort left part (a[0], ..., a[p-1])
		quick_sort(a, p+1, r); // sort right part (a[p-1], ..., a[r])
	}
}
