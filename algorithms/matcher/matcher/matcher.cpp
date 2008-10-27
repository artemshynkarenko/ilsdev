#include <iostream>
using namespace std;

// find first matcher pattern p in text t


bool is_equal(char *a, char *b, int n){ // return true if the strings a & b is equal
	bool res=true;  
	for (int i=0; i<n; ++i)        
		res = res && (a[i] == b[i]);
	return res;
}


int matcher_simple(char *p, int n, char *t, int m){  // simple
	for (int s=0; s<m-n+1; ++s){ // shift string p on text t
		if (is_equal(t+s, p, n))
			return s;
	}
	return -1;
}

//----------------------------------------------------//

int matcher_Rabik_Karp(char *p, int n, char *t, int m){
	const int mod=12345678; // module of hash function
	const int base=14;     // power base

	long long hp=0, ht=0; // hash for pattern and for text 
	for (int i=0; i<n; ++i){
		hp= (hp*base+(char)p[i]) %mod;  // hashing pattern
		ht= (ht*base+(char)t[i]) %mod;  // hashing first n chatacters of text
	}

	int pow=1;
	for (int i=0; i<n-1; ++i)
		pow= pow*base % mod;        // base in power n-1	
	for (int s=0; s<m-n+1; ++s){
		if ((hp-ht)%mod==0)               // if hash of pattern is equal to hash of n characters of text with shift s
			if (is_equal(t+s, p, n)) // then, we need to chack strings 
				return s;
		if (s<m-n) // if it is'n the last shift, hashing text for next shift
			ht = ((ht-pow*(char)t[s])*base + (char)t[s+n] ) % mod;
	}

	return -1;
} 

//---------------------------------------------------//


int matcher_state_machine(char *p, int n, char *t, int m){
	int **fi = new int*[n+1];                       // state-transition function
	for (int i=0; i<n+1; ++i) fi[i] = new int[256]; // 
	for (int q=0; q<n+1; ++q){
		for (unsigned char a=0; a<255; ++a){ // calculate state-transition function for each stats and characters
			int k=min(n, q+1)+1; 
			do {
				k--;
			}while (!(k==0 || (is_equal(p, p+q+1-k, k-1) && p[k-1]==char(a)))); 
			fi[q][a] = k;
		}
	}

	// work of state machine
	int q=0; // state of machine (on begin, state is 0)
	for (int i=0; i<m; ++i){ // each caracters of t push to state machine
		q = fi[q][(unsigned char)t[i]]; // stat-transition
		if (q==n)      // find successful
			return i-n+1;  // return first matcher
	}
	delete []fi;
	return -1;
}

//---------------------------------------------------//
// Knuth-Morris-Pratt algorithm
int matcher_KMP(char *p, int n, char *t, int m){
	int *pi = new int[n+1]; // prefix function
	pi[1] = 0;
	int k=0;
	for (int q=2; q<=n; ++q){  // calculate prefix function for each q
		while (k>0 && p[k]!=p[q-1])
			k=pi[k];
		if (p[k]==p[q-1])
			k++;
		pi[q]=k;
	}

	// work of state_machine
	int q=0;  // state of machine (on begin, state is 0)
	for (int i=0; i<m; ++i){  // each caracters of t push to state machine
		while (q>0 && p[q]!=t[i]) // optimization to find transition to next state, using prefix function pi
			q=pi[q];              //  
		if (p[q]==t[i])           //
			q++;                  //
		if (q==n){       // find successful
			return i-n+1;  // return first matcher
			//q=pi[q];
		}
	}
	delete []pi;
	return -1;
}