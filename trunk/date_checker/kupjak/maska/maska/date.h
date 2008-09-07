#include <iostream>
#include <string>
#include <ctime>

using namespace std;

int get_dayweek(const int day, const int month, const int year);
bool if_leap_year(const int year);
vector<string> split(string s, char d);
bool parse_mask(bool *bool_mask,const string & s, const int ind);

class DateTime{ 
	int day;
	int month;
	int year;
	int day_week;
	int hour;
	int minute;
	bool leap_year; // leap year
public:
	friend class Condition;
	DateTime(); // default date&time is present
	DateTime(const int _day, const int _month, const int _year, const int _day_week, const int _hour, const int _minute):
	day(_day), month(_month), year(_year), day_week(get_dayweek(_day, _month, _year)), hour(_hour), minute(_minute), leap_year(if_leap_year(_year)){}
	DateTime & operator++(int); // next date;
	friend ostream & operator<<(ostream & os, const DateTime & dt){os<<dt.day<<"."<<dt.month<<"."<<dt.year<<" "<<dt.day_week<<" "<<dt.hour<<":"<<dt.minute; return os;}
};

class Condition{ // condition of valid date&time
	bool valid_day_ofmonth[30];
	bool valid_day_ofweek[7];
	bool valid_month[12];
	bool valid_hour[24];
	bool valid_minute[60];
	bool valid_masks;
public:
	const bool valid()const{return valid_masks;} // selector of valid_masks
	Condition(); // default: all date is valid;
	Condition(const string masks[5]); // init condition by 5 maskes
	bool valid_date(const DateTime &date_time)const; // return valid of date
	bool valid_date_time(const DateTime &date_time)const; // return valid of date&time
	bool find_date(const DateTime & now, DateTime & next, const int count_of_day)const;
};
