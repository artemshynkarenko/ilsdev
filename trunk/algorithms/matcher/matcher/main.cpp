#include <iostream>
#include <string>
#include <ctime>
#include "matcher.h"

using namespace std;

int main(){
	int n=10; // length of pattern
	int m=10000000; // length of text
	char *pattern="lalalalala"; // pattern
	char *text= new char[m];  
	for (int i=0; i<m; ++i){//  random text
		int r=rand();
		if (r%2==0)
			text[i]= 'a' + r/2%26;
		else
			text[i]= (r/2%2==0)?'l':'a' ;
	}

	cout<<"finding \""<<pattern<<"\" in random text with length "<<m<<endl<<endl;


	cout<<"simple algorithm to find substring:"<<endl;
	int currtime = clock(); 
	int s1=matcher_simple(pattern, n, text, m);
	cout<<"time: "<<clock()-currtime<<" ms"<<endl;
	cout<<"position of  \""<<pattern<<"\" in text is "<<s1<<endl;
	cout<<"text from this position: \"";
	for (int i=0; i<n; ++i)
		cout<<text[s1+i];
	cout<<"\""<<endl<<endl;

	cout<<"Rabin Karp algorithm to find substring, using hash function:"<<endl;
	currtime = clock();
	int s2=matcher_Rabik_Karp(pattern, n, text, m);
	cout<<"time: "<<clock()-currtime<<" ms"<<endl;
	cout<<"position of  \""<<pattern<<"\" in text is "<<s2<<endl;
	cout<<"text from this position: \"";
	for (int i=0; i<n; ++i)
		cout<<text[s2+i];
	cout<<"\""<<endl<<endl;


	cout<<"algorithm to find substring, using finite state machine:"<<endl;
	currtime = clock();
	int s3=matcher_state_machine(pattern, n, text, m);
	cout<<"time: "<<clock()-currtime<<" ms"<<endl;
	cout<<"position of  \""<<pattern<<"\" in text is "<<s3<<endl;
	cout<<"text from this position: \"";
	for (int i=0; i<n; ++i)
		cout<<text[s3+i];
	cout<<"\""<<endl<<endl;


	cout<<"Knuth-Morris-Pratt algorithm to find substring:"<<endl;
	currtime = clock();
	int s4=matcher_KMP(pattern, n, text, m);
	cout<<"time: "<<clock()-currtime<<" ms"<<endl;
	cout<<"position of  \""<<pattern<<"\" in text is "<<s4<<endl;
	cout<<"text from this position: \"";
	for (int i=0; i<n; ++i)
		cout<<text[s4+i];
	cout<<"\""<<endl<<endl;

	return 0;
}