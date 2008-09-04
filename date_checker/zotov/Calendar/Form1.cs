using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calendar
{
  public partial class frmCalendar : Form
  {
    static public bool SAINT_HARRY = false;
    static public int MAX_YEAR = 9998;

    //Constructor=============================================================================
    public frmCalendar()
    {
      InitializeComponent();
    }

    //Mask====================================================================================
    public class Mask
    {
      int length;
      bool[] mask;

      public Mask(int mask_length)
      {
        length = mask_length;
        mask = new bool[length];
      }

      public bool this[int pos]
      {
        get{
          if (pos < length && pos >= 0)
          {
            return mask[pos];
          }
          else
          {
            throw new BoundException(); 
          }
        }
      }

      public int Length
      {
        get{
          return length; 
        }
      }

      public void Fill(int start,int finish,string error_type)
      {
        if (start >= 0 && finish < length && start<=finish)
        {
          for (int i = start; i <= finish; i++)
          {
            mask[i] = true;
          }
        }
        else
        {
          throw new MaskException(error_type);
        }
      }
    }

    //BoundException===========================================================================
    class BoundException : ApplicationException
    {
      public void ShowException()
      {
        MessageBox.Show("Ви вийшли за межі масиву!", "Калєндар");
      }
    }

    //DateNotFoundException===========================================================================
    class DateNotFoundException : ApplicationException
    {
      public void ShowException()
      {
        MessageBox.Show("Аблом!Програмірування така штука:)!Такої дати в найближчі 9998 років не існує!", "Калєндар");
      }
    }

    //DateException===========================================================================
    class DateException:ApplicationException{
      string exception_type;
      public DateException(string type)
      {
        exception_type=type;
      }
      public void ShowException()
      {
        MessageBox.Show("Введіть правильно дату в полі "+exception_type+"!","Калєндар");
      }
    }

    //MaskException===========================================================================
    class MaskException : ApplicationException
    {
      string exception_type;
      public MaskException(string type)
      {
        exception_type = type;
      }
      public void ShowException()
      {
        MessageBox.Show("Введіть правильно маску в полі " + exception_type + "!", "Калєндар");
      }
    }

    //Show Form==============================================================================
    private void frmCalendar_Shown(object sender, EventArgs e)
    {
      DateTime date = DateTime.Now;
      mtbDateYear.Text = date.Year.ToString();
      mtbDateHour.Text = date.Hour.ToString();
      mtbDateMinute.Text = date.Minute.ToString();
      mtbDateMonth.Text = date.Month.ToString();
      mtbDateDay.Text = date.Day.ToString();      
    }

    //GetDate================================================================================
    public DateTime GetDate()
    {
      DateTime date = new DateTime();
      int i;

      //Year
      i = int.Parse(mtbDateYear.Text);
      if (i > 0)
      {
        date = date.AddYears(i - 1);
      }
      else
      {
        throw new DateException("Рік");
      }

      //Month
      i = int.Parse(mtbDateMonth.Text);
      if (i <= 12 && i > 0)
      {
        date = date.AddMonths(i - 1);
      }
      else
      {
        throw new DateException("Місяць");
      }

      //Day
      i = int.Parse(mtbDateDay.Text);
      if (i <= 31 && i > 0)
      {
        DateTime temp_date = date;
        date = date.AddDays(i - 1);
        if (temp_date.Month != date.Month)
        {
          throw new DateException("День");
        }
      }
      else
      {
        throw new DateException("День");
      }

      //Hour
      i = int.Parse(mtbDateHour.Text);
      if (i <= 23)
      {
        DateTime temp_date = date;
        date = date.AddHours(i);
      }
      else
      {
        throw new DateException("Година");
      }

      //Hour
      i = int.Parse(mtbDateMinute.Text);
      if (i <= 59)
      {
        DateTime temp_date = date;
        date = date.AddMinutes(i);
      }
      else
      {
        throw new DateException("Хвилина");
      }      

      return date;
    }

    //ParseString============================================================================
    public void ParseString(Mask mask,string text,string error_type)
    {
      char[] delimiterChars = {','};
      string[] words = text.Split(delimiterChars);

      for (int i = 0; i < words.Length; i++){
        string s = words[i];
        int start = -1, finish = -1, temp = -1;

        for (int j = 0; j < s.Length; j++)
        {          
          if (s[j] == '*')
          {
            if (i > 0 || j > 0 || s.Length>1 || words.Length>1)
            {
              throw new MaskException(error_type);
            }
            else
            {
              start = 0;
              finish=temp = mask.Length - 1;              
            }
          }
          else if (s[j] == '-')
          {
            if (start >= 0 || temp==-1)
            {
              throw new MaskException(error_type);
            }
            else
            {
              start = temp;
              temp = -1; 
            }
          }
          else if ((s[j] - (int)'0' >= 0) && (s[j] - (int)'0' <= 9))
          {
            if (temp == -1)
            {
              temp = 0;
            }
            temp = temp * 10 + s[j] - (int)'0';
          }
          else
          {
            throw new MaskException(error_type);
          }
        }

        //fill
        if (temp == -1)
        {
          throw new MaskException(error_type);
        }
        else
        {
          if (start != -1)
          {
            finish = temp;
          }
          else
          {
            start = finish = temp;
          }
          mask.Fill(start, finish, error_type);
        }
      }
    }

    //SetMinutesHours================================================================================
    public void SetMinutesHours(ref DateTime date,Mask mask_minutes,Mask mask_hours)
    {
      DateTime temp=date;
      while (!mask_minutes[date.Minute])
      {
        if(temp.AddHours(2).Hour==date.Hour){
          throw new DateNotFoundException();           
        }
        date=date.AddMinutes(1);
        SAINT_HARRY = true;
      }

      temp=date;
      while (!mask_hours[date.Hour])
      {
        if(temp.AddDays(2).Day==date.Day){
          throw new DateNotFoundException();           
        }
        date = date.AddHours(1);
        SAINT_HARRY = true;
      }
    }

    //SetToMinimumMinutesHours=======================================================================
    void SetToMinimumMinutesHours(ref DateTime date,Mask mask_minutes,Mask mask_hours)
    {
      while(date.Minute!=0 || date.Hour!=0){
        date=date.AddMinutes(1);
      }
      TimeSpan timespan=new TimeSpan(1);
      date.Subtract(timespan);
      SetMinutesHours(ref date, mask_minutes, mask_hours);
    }

    //SetWeeksMonthsYears============================================================================
    public void SetWeeksMonthsYears(ref DateTime date, Mask mask_days_in_month, Mask mask_months, Mask days_in_week, Mask mask_minutes, Mask mask_hours)
    {
      while (!mask_days_in_month[date.Day - 1] || !mask_months[date.Month - 1] || !days_in_week[((int)date.DayOfWeek+6)%7])
      {
        if(date.Year>MAX_YEAR){
          throw new DateNotFoundException();           
        }
        SetToMinimumMinutesHours(ref date,mask_minutes,mask_hours);
        date = date.AddDays(1);
        SAINT_HARRY = true;
      }
    }

    //Button Click===========================================================================
    private void btnGetNextDate_Click(object sender, EventArgs e)
    {
      try
      {
        SAINT_HARRY = false;
        DateTime date = GetDate();

        Mask mask_minutes = new Mask(60);
        Mask mask_hours = new Mask(24);
        Mask mask_days_in_month = new Mask(31);
        Mask mask_months = new Mask(12);
        Mask days_in_week = new Mask(7);

        ParseString(mask_minutes, txtMaskMinutes.Text, "Хвилини");
        ParseString(mask_hours, txtMaskHours.Text, "Години");
        ParseString(mask_days_in_month, txtMaskDaysInMonth.Text, "Днів у місяці");
        ParseString(mask_months, txtMaskMonths.Text, "Місяців");
        ParseString(days_in_week, txtMaskDaysInWeek.Text, "Днів тижня");

        SetMinutesHours(ref date, mask_minutes, mask_hours);
        SetWeeksMonthsYears(ref date, mask_days_in_month, mask_months, days_in_week, mask_minutes, mask_hours);
        if (!SAINT_HARRY)
        {
          date = date.AddMinutes(1);
          SetMinutesHours(ref date, mask_minutes, mask_hours);
          SetWeeksMonthsYears(ref date, mask_days_in_month, mask_months, days_in_week, mask_minutes, mask_hours);
        }

        MessageBox.Show(date.ToString(),"Палучі сваю дату!");        
      }
      catch (DateException exc)
      {
        exc.ShowException();
      }
      catch (MaskException exc)
      {
        exc.ShowException();
      }
      catch (BoundException exc)
      {
        exc.ShowException();
      }
      catch (DateNotFoundException exc)
      {
        exc.ShowException();
      }
    }
  }
}
