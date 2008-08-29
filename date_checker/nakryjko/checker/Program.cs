using System;
using System.Collections.Generic;
using System.Text;

namespace Checker
{
    public class DateChecker
    {
        private long minMask=0;
        private int hourMask=0, dayMask=0, monMask=0, weekMask=0;
        
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
                    if (range.LastIndexOf('-') != ind)
                        throw new ArgumentException("More than one instance of '-' delimiter in single range");
                    res |= getRange(int.Parse(range.Substring(0,ind)), int.Parse(range.Substring(ind+1)));
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
            hourMask = (int)msk;

            msk = parseMask(dayOfMonthMask);
            if (!checkRange(ref msk, 0, 30))
                throw new ArgumentOutOfRangeException("Days of month mask not in range [0..30]");
            dayMask = (int)msk;

            msk = parseMask(monthMask);
            if (!checkRange(ref msk, 0, 11))
                throw new ArgumentOutOfRangeException("Months mask not in range [0..11]");
            monMask = (int)msk;

            msk = parseMask(dayOfWeekMask);
            if (!checkRange(ref msk, 0, 6))
                throw new ArgumentOutOfRangeException("Days of week mask not in range [0..6]");
            weekMask = (int)msk;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DateChecker ch = new DateChecker("1-2-,8", "*", "*", "*", "*");
        }
    }
}
