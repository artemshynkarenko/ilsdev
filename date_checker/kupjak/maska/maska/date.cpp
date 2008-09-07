#include <iostream>
#include <string>
#include <time.h>
#include <vector>
#include <algorithm>
#include "date.h"

using namespace std;

const int MAX_DAYS_OF_MONTH[12] = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};


int get_dayweek(const int day, const int month, const int year){ //return day since Sunday
	int a = (14 - month+1) / 12;
    int y = year - a;
    int m = month+1 + 12 * a - 2;
    return (7000 + (day+1 + y + y/4 - y/100 + y/400 + (31*m)/12)) % 7;
}

bool if_leap_year(const int year){
	if (year%100==0)
		return year%400==0;
	else
		return year%4==0;
}


DateTime::DateTime(){       // defoult: date&time is now    
	time_t t = time(0);     // get present time
	tm lt = *localtime(&t); // 
 
	day = lt.tm_mday-1;     // day of the month - [0,30] 
	month = lt.tm_mon;      // month [0, 11]
	year = lt.tm_year+1900; // year 
	hour = lt.tm_hour;      // hours since midnight - [0,23] 
	minute = lt.tm_min;     // minutes after the hour - [0,59] 
	day_week = (get_dayweek(day, month, year)-1+7)%7; //days since Monday - [0,6]
	leap_year = if_leap_year(year);
}

DateTime & DateTime::operator++(int){  // inc date, but do not inc time
	bool day_last; // if day of month is the last day of month
	if (month==1)
		day_last = day+1 >= MAX_DAYS_OF_MONTH[month] + leap_year; // if year is leap, then February has 29 days, else - 28
	else
		day_last = day+1 >= MAX_DAYS_OF_MONTH[month];
	if (day_last){ //the day of month is last day of month, then next month
		day=0;
		if (month>=11){ // the month is last month of the year, then next year
			month=0;
			++year; //next year
			leap_year = if_leap_year(year); 
		}
		else
			++month; // next month
	}
	else
		++day; // next day
	day_week = (day_week+1)%7; // next day of week
	return *this;
}

Condition::Condition(){ // defoult: all date if valid
	for (int i=0; i<31; ++i)
		valid_day_ofmonth[i] = true;
	for (int i=0; i<7; ++i)
		valid_day_ofweek[i] = true;
	for (int i=0; i<12; ++i)
		valid_month[12] = true;
	for (int i=0; i<24; ++i)
		valid_hour[i] = true;
	for (int i=0; i<60; ++i)
		valid_minute[12] = true;
	valid_masks = true;
}


int str_to_int(const string & s){ // string to int
	if (s.size()==0)
		return -1;
	bool valid = true;
	for (int i=0; i<s.size(); ++i)
		valid = valid && (s[i]>='0' && s[i]<='9');
	if (!valid)
		return -1;
	int res=0;
	for (int i=0; i<s.size(); ++i){
		res = res*10 + s[i]-'0';
	};
	return res;
}

int max_value(const int ind){
	if (ind==0) return 60;
	if (ind==1) return 24;
	if (ind==2) return 31;
	if (ind==3) return 12;
	if (ind==4) return 7;
}

bool valid_value(const int v, const int ind){
	if (ind==0) return 0<=v && v<60;
	if (ind==1) return 0<=v && v<24;
	if (ind==2) return 0<=v && v<31;
	if (ind==3) return 0<=v && v<12;
	if (ind==4) return 0<=v && v<7;
}

bool parse_mask(bool *bool_mask,const string & s, const int ind){ // parse mask_string to bool_mask
	if (s=="*"){
		for (int i=0; i<max_value(ind); ++i)
			bool_mask[i]=true;
	}
	else{
		for (int i=0; i<max_value(ind); ++i)
			bool_mask[i]=false;

		vector<string> list = split(s, ','); // split on a list
		if (list.size()==0)
			return false;
		for (int i=0; i<list.size(); ++i){
			vector<string> gap = split(list[i], '-'); // all element of list split on gap
			if (gap.size()==1){ // element of list is one elemeln
				int value=str_to_int(gap[0]);
				if (valid_value(value, ind)) // if is valid value
					bool_mask[value]= true;  
				else
					return false;
			}
			else
				if (gap.size()==2){ // element of list is a range (gap)
					int value1=str_to_int(gap[0]);
					int value2=str_to_int(gap[1]);
					if (valid_value(value1, ind) && valid_value(value2, ind) && value1<=value2){
						for (int i=value1; i<value2+1; ++i) // set the range
							bool_mask[i]=true;
					}
					else
						return false;
				}else
					return false;
		}
	}
	return true;
}

Condition::Condition(const string masks[5]){
	valid_masks = parse_mask(valid_minute, masks[0], 0)&&
                  parse_mask(valid_hour, masks[1], 1)&&
				  parse_mask(valid_day_ofmonth, masks[2], 2)&&
				  parse_mask(valid_month, masks[3], 3)&&
				  parse_mask(valid_day_ofweek, masks[4], 4);
}


bool Condition::valid_date_time(const DateTime &date_time)const{ // return valid of date&time
	return valid_day_ofmonth[date_time.day] && 
		valid_day_ofweek[date_time.day_week] &&
		valid_month[date_time.month] &&
		valid_hour[date_time.hour] &&
		valid_minute[date_time.minute];
}
bool Condition::valid_date(const DateTime &date_time)const{ // return valid of date
	return valid_day_ofmonth[date_time.day] && 
		valid_day_ofweek[date_time.day_week] &&
		valid_month[date_time.month];
}

vector<string> split(string s, char d)
{
	
    vector<string> result;
	size_t  pos;
	while ((pos=s.find(d))!=string::npos){
		result.push_back(s.substr(0, pos));
		s = s.substr(pos+1, s.length()-pos-1);
	}
	result.push_back(s);
    return result;
}

bool Condition::find_date(const DateTime & now, DateTime & next, const int count_of_day)const{
	next = now;
	bool finded_date=valid_date(next);

	bool finded_time=false;  
	for (next.hour; next.hour<24; ++next.hour){ // find first valid time after now
		for (next.minute++; next.minute<60; ++next.minute){
			finded_time = finded_time || valid_date_time(next);
			if (finded_time)
				break;
			}
		if (finded_time)
			break;
		next.minute = 0;
	}
	if (finded_time) // finded today!!!
		return true;

	next++;
	finded_date=false;
	for (int i=0; i<count_of_day && !finded_date; ++i){ //finded second valid date, becouse the first has not valid time
		finded_date = finded_date || valid_date(next);
		if (!finded_date)
			next++;
	}
	if (!finded_date)
		return false;
	finded_time=false;
	for (next.hour=0; next.hour<24; ++next.hour){
		for (next.minute=0; next.minute<60; ++next.minute){
			finded_time = finded_time || valid_date_time(next);
			if (finded_time)
				break;
		}
		if (finded_time)
			break;
		next.minute = 0;
	}
	if (finded_time)
		return true; //finded!!!
}