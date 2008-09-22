using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BobSpace
{
  public class Lox:IComparable
  {
    public int i = 10;
    public Lox(int i_) { i = i_; }
    public static implicit operator Lox(int i)
    {
      return new Lox(i);
    }
    int IComparable.CompareTo(object y)
    {
      return this.i.CompareTo(((Lox)y).i);
    }
  }


  class Bob{
    public static void Main()
    {           
    }
  }
}
