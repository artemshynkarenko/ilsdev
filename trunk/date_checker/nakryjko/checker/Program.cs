using System;
using System.Collections.Generic;
using System.Text;

namespace Checker
{
    public class DateChecker
    {
        private long minMask=0, hourMask=0, dayMask=0, monMask=0, weekMask=0;
        private int[] monthsDayCnt = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        private long getRange(int L, int R)
        {
            return ((1L << (R - L + 1)) - 1) << L;
        }
        private bool checkRange(ref long mask, int L, int R)
        {
            if (mask == -1)
            {
                mask = getRange(L,R);
                return true;
            }
            return mask==0                                      // empty range
                || ((1L << L) <= mask && mask <= (1L << R));    // [L..R]
        }

        private long parseMask(string s, int L, int R)
        {
            if (s == "*") return -1;
            if (s.Contains("*"))
                throw new ArgumentException("Invalid mask: '*' must always appear alone");

            string[] ranges = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            long res=0;
            foreach(string range in ranges)
            {
                int ind = range.IndexOf('-');
                if (ind == -1)
                {
                    // single value (must be ;)
                    int tmp = int.Parse(range);
                    if (tmp < L || tmp > R)
                        throw new ArgumentOutOfRangeException(string.Format("Mask not in range [{0}..{1}]", L, R));
                    res |= (1L << tmp);
                }
                else
                {
                    if (ind==0 || range.LastIndexOf('-') != ind)
                        throw new ArgumentException("Invalid range specificator");
                    int left = int.Parse(range.Substring(0, ind));
                    int right = int.Parse(range.Substring(ind+1));
                    if (left > right)
                        throw new ArgumentException("Wrong range: first number must be less or equal of right");

                    if (left < L || right > R)
                        throw new ArgumentOutOfRangeException(string.Format("Mask not in range [{0}..{1}]", L, R));
                    res |= getRange(left, right);
                }
            }
            return res;
        }

        public DateChecker(string minutesMask, string hoursMask, string dayOfMonthMask, string monthMask, string dayOfWeekMask)
        {
            minMask     = parseMask(minutesMask, 0, 59);
            hourMask    = parseMask(hoursMask, 0, 23);
            dayMask     = parseMask(dayOfMonthMask, 0, 30);
            monMask     = parseMask(monthMask, 0, 11);
            weekMask    = parseMask(dayOfWeekMask, 0, 6);
        }
        private int dow(DayOfWeek d)
        {
            switch (d)
            {
                case DayOfWeek.Monday:      return 0;
                case DayOfWeek.Tuesday:     return 1;
                case DayOfWeek.Wednesday:   return 2;
                case DayOfWeek.Thursday:    return 3;
                case DayOfWeek.Friday:      return 4;
                case DayOfWeek.Saturday:    return 5;
                case DayOfWeek.Sunday:      return 6;
                default:
                    throw new ArgumentOutOfRangeException("Invalid DayOfWeek");
            }
        }
        public bool IsAppropriateDate(DateTime dt)
        {
            return ((minMask    & (1L << dt.Minute))        != 0)  // minutes in range
                && ((hourMask   & (1L << dt.Hour))          != 0)  // hours in range
                && ((dayMask    & (1L << dt.Day-1))         != 0)  // day of month in range
                && ((monMask    & (1L << dt.Month-1))       != 0)  // month in range
                && ((weekMask   & (1L << dow(dt.DayOfWeek)))!= 0); // day of week in range
        }
        public DateTime GetNextDate(DateTime dt)
        {
            dt = dt.AddMinutes(1);
            int curYear = dt.Year;
            int curMon = dt.Month - 1;
            int curDay = dt.Day - 1;
            int curHour = dt.Hour;
            int curMin = dt.Minute;
            int curWeekday = dow(dt.DayOfWeek);

            for (int tries = 0; tries < 2; ++tries)
            {
                for (int mon = 0; mon < 12; ++mon, ++curMon)
                {
                    if (curMon == 12)
                    {
                        curMon = 0;
                        ++curYear;
                    }
                    if ((monMask & (1L << curMon)) != 0)
                    {
                        int dayLim = monthsDayCnt[curMon] - ((curMon == 1 && !DateTime.IsLeapYear(curYear)) ? 1 : 0);
                        for (; curDay < dayLim; ++curDay)
                        {
                            if (((dayMask & (1L << curDay)) != 0) && ((weekMask & (1L << curWeekday)) != 0))
                            {
                                for (; curHour < 24; ++curHour)
                                {
                                    if ((hourMask & (1L << curHour)) != 0)
                                    {
                                        for (; curMin < 60; ++curMin)
                                        {
                                            if ((minMask & (1L << curMin)) != 0)
                                            {
                                                return new DateTime(curYear, curMon + 1, curDay + 1, curHour, curMin, 0);
                                            }
                                        }
                                    }
                                    curMin = 0;
                                }
                            }
                            else curWeekday = (curWeekday + 1) % 7;
                            curHour = curMin = 0;
                        }
                    }
                    else curWeekday = (curWeekday + monthsDayCnt[curMon] - curDay) % 7;
                    curDay = curHour = curMin = 0;
                }
                if (curMon == 12)
                {
                    curMon = 0;
                    ++curYear;
                }
                while (!DateTime.IsLeapYear(curYear))
                {
                    ++curYear;
                    curMon = curDay = curHour = curMin = 0;
                }
                curWeekday = dow(new DateTime(curYear, curMon+1, curDay+1, curHour, curMin, 0).DayOfWeek);
            }
            throw new Exception("No next date is possible!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DateChecker ch = new DateChecker("*", "*", "28", "1", "*");
            DateTime start = DateTime.Now;
            for (int i = 0; i < 10000; ++i)
            {
//                DateTime dt = ch.GetNextDate(new DateTime(2003, 2, 27, 0, 0, 0));
                DateTime dt = ch.GetNextDate(new DateTime(2003, 3, 1, 0, 0, 0));
                if (!ch.IsAppropriateDate(dt))
                    throw new Exception("Wrong answer!");
                Console.WriteLine(dt);
            }
            TimeSpan end = DateTime.Now - start;
            Console.WriteLine("Time for 1000 tries: " + end);

        }
    }
}
