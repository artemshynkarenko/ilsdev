#include <iostream>
#include <string>
#include <vector>
#include "date.h"

using namespace std;



int main(){

	DateTime now;
	cout<<"all digit is in range [0, ...]"<<endl;
	cout<<"now is "<<now<<endl;

	string mask[5];
	cout<<"enter masks: (min, hour, day, month, day of week)"<<endl;
	for (int i=0; i<5; ++i)
		cin>>mask[i];
	Condition cond(mask);
	if (!cond.valid())
		cout<<"Masks is not correct."<<endl;
	else{
		DateTime next;
		if (cond.find_date(now, next, 80*366))
			cout<<"next date is "<<next<<endl;
		else
			cout<<"ther is not next date"<<endl;
	}
	return 0;
}