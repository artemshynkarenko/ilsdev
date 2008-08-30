using System;
using System.Collections.Generic;
using System.Text;

namespace Checker
{
    public class DateChecker
    {
        private long minMask=0, hourMask=0, dayMask=0, monMask=0, weekMask=0;
        private int[] monthsDayCnt = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        private long getRange(int l, int r)
        {
            return ((1L << (r - l + 1)) - 1) << l;
        }
        private bool checkRange(ref long mask, int l, int r)
        {
            if (mask == -1)
            {
                mask = getRange(l,r);
                return true;
            }
            return mask==0                                      // empty range
                || ((1L << l) <= mask && mask <= (1L << r));    // [l..r]
        }

        private long parseMask(string s)
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
                    res |= (1L << tmp);
                }
                else
                {
                    if (ind==0 || range.LastIndexOf('-') != ind)
                        throw new ArgumentException("Invalid range specificator");
                    int l = int.Parse(range.Substring(0, ind));
                    int r = int.Parse(range.Substring(ind+1));
                    if (l > r)
                        throw new ArgumentException("Wrong range: first number must be less or equal of right");
                    if (r >= 60)
                        throw new ArgumentException("Range value is too large");
                    res |= getRange(l, r);
                }
            }
            return res;
        }

        public DateChecker(string minutesMask, string hoursMask, string dayOfMonthMask, string monthMask, string dayOfWeekMask)
        {
            long msk = parseMask(minutesMask);
            if (!checkRange(ref msk, 0, 59))
                throw new ArgumentOutOfRangeException("Minutes mask not in range [0..59]");
            minMask = msk;

            msk = parseMask(hoursMask);
            if (!checkRange(ref msk, 0, 23))
                throw new ArgumentOutOfRangeException("Hours mask not in range [0..23]");
            hourMask = msk;

            msk = parseMask(dayOfMonthMask);
            if (!checkRange(ref msk, 0, 30))
                throw new ArgumentOutOfRangeException("Days of month mask not in range [0..30]");
            dayMask = msk;

            msk = parseMask(monthMask);
            if (!checkRange(ref msk, 0, 11))
                throw new ArgumentOutOfRangeException("Months mask not in range [0..11]");
            monMask = msk;

            msk = parseMask(dayOfWeekMask);
            if (!checkRange(ref msk, 0, 6))
                throw new ArgumentOutOfRangeException("Days of week mask not in range [0..6]");
            weekMask = msk;
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
            }
            throw new ArgumentOutOfRangeException("Invalid DayOfWeek");
        }
        public bool IsGoodTime(DateTime dt)
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
                if (!ch.IsGoodTime(dt))
                    throw new Exception("Wrong answer!");
                Console.WriteLine(dt);
            }
            TimeSpan end = DateTime.Now - start;
            Console.WriteLine("Time for 1000 tries: " + end);

        }
    }
}
