using System;
using System.Collections.Generic;
using System.Text;

namespace Task
{
    internal class Program
    {
        private class Set
        {
            private bool[] _mask;
            private int _minValue;
            private int _maxValue;
            private int _first;
            private int _last;

            public int MaxValue
            {
                get { return _maxValue; }
            }

            public int MinValue
            {
                get { return _minValue; }
            }

            private static bool inRange(int value, int minValue, int maxValue)
            {
                return !(value < minValue || maxValue < value);
            }

            public bool Find(int value)
            {
                return _mask[value - MinValue];
            }

     /*       static int _lastValue=-1;
            static int _lastIndex=-1;*/
            public bool TryGetNextValueFrom(int value, out int nextValue)
            {
                for(int i=value+1; i <=_last ; i++)
                {
                    if (_mask[i]) 
                    {
                        nextValue = i + _minValue;
                        return true;
                    }
                }
                nextValue = _first;
                return false;
           // At first I think that better to create seperate objects
           // 1. List for finding next value
           // 2. Bitarray for checking
           
           /*     int i = 0;

                if (value >= _lastValue && _lastIndex != -1)
                    i = _lastIndex;
                
                while (value > _values[i])
                {
                    i++;
                    if (i <= _values.Length)
                    {
                        // if we can't find next value, we return first allowed value
                        nextValue = _values[0];
                        return false;
                    }
                }
                nextValue = _values[i];
                _lastValue = nextValue;
                _lastIndex = i;
                return true;*/
            }

            public Set(int minValue, int maxValue, string expression)
            {
                _minValue = minValue;
                _maxValue = maxValue;
                if (minValue >= maxValue)
                    throw new Exception("minValue must be lesser then maxValue");
                string[] numbers = expression.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
                _mask = new bool[maxValue - minValue + 1];
                if (expression == "*")
                {
                    //_values = new int[maxValue - minValue + 1];
                    _first = 0;
                    _last = _maxValue - _minValue;
                    for (int i = 0; i <= maxValue - minValue; i++)
                    {
                        //_values[i] = i + minValue;
                        _mask[i] = true;
                    }
                }
                else
                {
                    foreach (string number in numbers)
                    {
                        if (number.Contains("-"))
                        {
                            int rangeSeperator = number.IndexOf('-');
                            int rangeFrom = int.Parse(number.Substring(0, rangeSeperator ));
                            int rangeTo = int.Parse(number.Substring(rangeSeperator + 1));
                            if (!inRange(rangeFrom, minValue, maxValue))
                                throw new ArgumentOutOfRangeException(rangeFrom + " value is out of range " + minValue +
                                                                      ".." + maxValue + "]");
                            if (!inRange(rangeTo, minValue, maxValue))
                                throw new ArgumentOutOfRangeException(rangeTo + " value is out of range " + minValue +
                                                                      ".." + maxValue + "]");
                            for (int i = 0; i <= rangeTo - rangeFrom; i++)
                                _mask[i] = true;
                        }
                        else if (number == "*")
                        {
                            throw new Exception("Can't combine other ranges with *");
                        }
                        else
                        {
                            int i = int.Parse(number);
                            _mask[i - minValue] = true;
                        }
                    }
                }
                //Init first and last values

                _first = -1;
                for (int i = 0; i <= maxValue - minValue; i++)
                    if (_mask[i] )
                    {
                        if(_first==-1) 
                            _first = i;
                        _last = i;
                    }
                
              /*                 
              int count = 0;
              for (int i = 0; i <= maxValue - minValue; i++)
                  if (_mask[i]) count++;
              if (count == 0)
                  throw new Exception("Set is empty");
            _values = new int[count];

              for (int i = 0, j = 0; i <= maxValue - minValue; i++)
                  if (_mask[i])
                  {
                      _values[j] = i + minValue;
                      j++;
                  }*/
            }
        }

        private class DateChecker
        {
            private Set _minutes, _hours, _days, _months, _weekdays;

            public DateChecker(string maskMinutes, string maskHours, string maskDays, string maskMonth,
                               string maskWeekDay)
            {
                _minutes = new Set(0, 59, maskMinutes);
                _hours = new Set(0, 23, maskHours);
                _days = new Set(0, 30, maskDays);
                _months = new Set(0, 11, maskMonth);
                _weekdays = new Set(0, 6, maskWeekDay);
            }
            private bool GetAllowedDeltaValue(int value, Set allowedValues, out int delta, int maxValue)
            {
                int nextValue;
                if (!allowedValues.Find(value))
                    if (allowedValues.TryGetNextValueFrom(value, out nextValue))
                    {
                        delta = nextValue - value;
                        return true;
                    }
                    else
                    {
                        delta = nextValue + maxValue - value ;
                        return true;
                    }
                else
                {
                    delta = 0;
                    return false;
                }
            }

            private bool GetAllowedDeltaValue(int value, Set allowedValues, out int delta)
            {
                return GetAllowedDeltaValue(value, allowedValues, out delta,
                                            allowedValues.MaxValue - allowedValues.MinValue+1);

            }

            public DateTime GetNextDate(DateTime currentTime)
            {
                DateTime time = new DateTime(currentTime.Ticks);
                

                while (true)
                {
                    int delta;
                    // I don't sure about 100 years.
                    // You can correct this 
                    if(time.Year - currentTime.Year > 100)
                    {
                        throw new Exception("Can't find such date!");
                    }
                    if (GetAllowedDeltaValue(time.Month - 1, _months, out delta))
                        time = time.AddMonths(delta);
                    bool monthIsAllowed = false;
                    bool changed = false;
                    while (!monthIsAllowed)
                    {
                        if (GetAllowedDeltaValue(time.Day - 1, _days, out delta, DateTime.DaysInMonth(time.Year, time.Month)))
                        {
                            time = time.AddDays(delta);
                            changed = true;
                        }
                        if (!_weekdays.Find(DayOfWeekToInteger(time.DayOfWeek)))
                        {
                            time = time.AddDays(1);
                            changed = true;
                        }
                        else
                        {
                            monthIsAllowed = true;
                        }
                    }
                    if (changed == true)
                        continue;
                    if (GetAllowedDeltaValue(time.Hour, _hours, out delta))
                    {
                        time = time.AddHours(delta);
                        continue;
                    }
                    if (GetAllowedDeltaValue(time.Minute, _minutes, out delta))
                    {
                        time = time.AddMinutes(delta);
                        continue;
                    }
                    if (time.Equals(currentTime))
                    {
                        time = time.AddMinutes(1);
                    }
                    else
                    {
                        return time;
                    }
                }
            }

            private static int DayOfWeekToInteger(DayOfWeek week)
            {
                // Hm, what this means?
                // In .NET Sunday == 0 but in our normal system Sunday == 6
                int value = (int) week;
                if (value == 0)
                    return 6;
                else
                    return value -1;
            }
        }

        private static void Main(string[] args)
        {
            DateChecker ch = new DateChecker("0", "0", "1", "*", "*");
            DateTime timer = DateTime.Now;
            for (int i = 0; i < 10000; i++)
            {

                try
                {
                    DateTime dt = ch.GetNextDate(DateTime.Now );
                    if(i==0) 
                        Console.WriteLine(dt);
                }
                catch(Exception e)
                {
                    
                    Console.WriteLine(e);
                }
                
            }
            
         

        Console.WriteLine("{0} ms ", (DateTime.Now - timer).TotalMilliseconds);
            
            Console.ReadKey();
        }
    }
}
