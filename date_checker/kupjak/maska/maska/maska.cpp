#include <iostream>
#include <string>

using namespace std;

const int DAYS[12] = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

string mask[5], t;    // 5 mask
int res[5], now[5]; // date = {minut, hour, day of month, month, day of week}
bool B[5][60];   


void init(){
	for (int i=0; i<60; ++i)
		for (int j=0; j<5; ++j)
		B[j][i]=false;
}

bool is_num(char c){
	return c>='0' && c<='9';
}

bool good_range(int v, int ind){  // chack valid of range
	if (ind==0)   // chack range of minute
		return (v>=0 && v<60);
	if (ind==1)   // chack range of hour
		return (v>=0 && v<24);
	if (ind==2)				// chack range of day of month
		return v>=0 && v<31;
	if (ind==3)             // chack range of month
		return v>=0 && v<12;
	if (ind==4)             // chack range of day of week
		return v>=0 && v<7;
}

bool parse(const int ind){ // parse i-th mask and return valid of mask 
	if (mask[ind]=="*"){
		for (int i=0; i<60; ++i)
			B[ind][i]=true;
		return true;
	}
	if (!is_num(mask[ind][0]))
		return false;

	int length = mask[ind].length();
	string s=mask[ind];
	for (int i=0; i<length; ++i)
		if (s[i]!='-' && s[i]!=',' && !is_num(s[i]))
			return false;

	string new_s;
	s+=' ';
	int count=0; //count of num
	unsigned t=0; 
	for (int i=0; i<length; ++i){
		if (is_num(s[i])){
			++count;
			if (count>2)
				return false;
			t = t*10 + int(s[i]-'0');
		}
		else{
			count=0;
			new_s += char(t);
			new_s += -s[i];
			t=0;
		}
	}
	new_s += char(t);
	length = new_s.length();
	if (length%2==0)
		return false;

	for (int i=0; i<length; i+=2)
		if (!(new_s[i]>=0 && new_s[i]<=99))
			return false;
	for (int i=0; i<length; i+=2)
		if (!good_range(new_s[i], ind)){
			cout<<" out of range"<<endl;
			return false;
		}
	

	for (int i=0; i<length; ){
		if (i+1>=length || new_s[i+1]==-','){
			B[ind][new_s[i]]=true;
			i+=2;
		}else{ // '-'
			if (new_s[i]>new_s[i+2])
				return false;
			for (int j=new_s[i]; j<=new_s[i+2]; ++j)
				B[ind][j]=true;
			i+=4;
		}
		
	}

	return true;
}

bool find(){  // find result of problem
	int NextMinute[60], NextHour[24]; //next minute\hour after...
	NextMinute[59]=NextHour[23]=0;
	for (int i=58; i>=0; --i) // init next minute
		if (B[0][i+1])
			NextMinute[i]=i+1;
		else
			NextMinute[i]=NextMinute[i+1];
	for (int i=22; i>=0; --i) // init next hour
		if (B[1][i+1])
			NextHour[i]=i+1;
		else
			NextHour[i]=NextHour[i+1];
	
	int dw=6;   // day of week
	for (int mo=0; mo<12; ++mo)
		for (int d=0; d<DAYS[mo]; ++d){
			dw = (dw +1) %7; 
			if (B[2][d] && B[3][mo])
			if (mo==now[3] && d==now[2] && B[4][dw]){
				if (B[1][now[1]] && NextMinute[now[0]]!=0){
					res[0] = NextMinute[now[0]];
					res[1] = now[1];
					res[2] = d;
					res[3] = mo;
					res[4] = dw;
					return true;
				}
				if (NextHour[now[1]]!=0 && (NextMinute[0]!=0 || B[0][0])){
					if (B[0][0])res[0]=0; else	res[0] = NextMinute[0];
					res[1] = NextHour[now[1]];
					res[2] = d;
					res[3] = mo;
					res[4] = dw;
					return true;
				}
			}else
				if (B[4][dw])
				if ((mo==now[3] && d>now[2])|| mo>now[3]){
					if (B[0][0])res[0]=0;else res[0] = NextMinute[0];
					if (B[1][0])res[1]=0;else res[1] = NextHour[0];
					res[2] = d;
					res[3] = mo;
					res[4] = dw;
					return true;
				}
			
		}
	return false;
}


int main(){
	//cout<<int('-')<<" "<<int(',');
	init();

	cout<<"Enter date&time: Day Month Hour Minute"<<endl;
	cin>>now[2]>>now[3]>>now[1]>>now[0];
	if (!(good_range(now[0], 0)&&good_range(now[1], 1)&&good_range(now[2], 2)&&good_range(now[3], 3))){
		cout<<"bad date"<<endl;
		cin>>t;
		return 0;
	}
	cout<<"Enter 5 mask (Minute, Hour, Day, Month, Day of Week)"<<endl;
	for (int i=0; i<5; ++i)
		cin>>mask[i];      // read 5 mask

	bool wrong=false;
	for (int i=0; i<5; ++i)
		wrong = wrong || !parse(i);  
	if (wrong){             // is exist a bad mask
		cout<<"Wrong mask"<<endl;
	//	cin>>t;
		return 0;
	}
		
	if (find()){		// if we find the result(next date) 
		cout<<"result (Day Month day_week      Hour Minute)"<<endl;
		cout<<"  "<<res[2]<<" "<<res[3]<<" "<<res[4]<<"   time: "<<res[1]<<":"<<res[0]<<endl;
	}
	else
		cout<<"There isn\'t next date"<<endl;
/*
	string s;
	while (true) {
	cin>>mask[0];
	cout<<bool(parse(0))<<endl;}*/
	//cin>>t;
	return 0;
}