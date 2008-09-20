#include <iostream>

using namespace std;


template<class Iter>
	void bsort(Iter first, Iter last){
		for (Iter i=first; i!=last; ++i)
			for (Iter j=i+1; j!=last; ++j)
				if (*j<*(j-1))
					swap(*i, *j);
	}

template<class Iter>
	void selection_sort(Iter first, Iter last){
		for (Iter i=first; i!=last-1; ++i){
			Iter i_min = i;
			for (Iter j=i+1; j!=last; ++j)
				if (*j < *i_min)
					i_min = j;
			swap(*i, *i_min);
		}
	}
template<class Iter, class T>
	void insertion_sort1(Iter first, Iter last, T* t){
		for (Iter i=first+1; i!=last; ++i) 
			if (*i < *first)	swap(*i, *first);
		for (Iter i=first+2; i!=last; ++i){
			T v= (*i); Iter j;
			for (j=i; v < *(j-1); --j)
				*j=*(j-1);
			*j=v;
		}
	}
template<class Iter>
	void insertion_sort(Iter first, Iter last){
		insertion_sort1(first, last, _Val_type(first));
	}



template<class Iter>
	Iter partition(Iter first, Iter last){
		typedef typename iterator_traits<Iter>::value_type T;
		T x = *(last-1);
		Iter i = first-1;
		for(Iter j=first; j!=last-1; j++)
			if(*j <= x){
				i++;
				swap(*i,*j);
			}
		swap(*(i+1) , *last);
		return i+1;
	}

template<class Iter>
	void quick_sort(Iter first, Iter last){
		if(first < last){
		Iter p = partition(first, last);
			quick_sort(first, p-1);
			quick_sort(p+1, last);
		}
	}


template< class T >
int partition(T a[], int p, int r){
 T x = a[r];
 int i = p - 1;
 for(int j = p;j < r;j++){
  if(a[j] <= x){
   i++;
   swap(a[i],a[j]);
  }
 }
 swap(a[i+1] , a[r]);
 return i + 1;
}


template< class T >
void quickSort(T a[], int p, int r){
 if(p < r){
  int q = partition(a, p , r);
  quickSort(a, p , q-1);
  quickSort(a, q+1 , r);
 }
}