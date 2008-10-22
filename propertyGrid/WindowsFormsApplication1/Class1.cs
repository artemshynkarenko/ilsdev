using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace WindowsFormsApplication1
{
	public struct Struct1
	{
		int z;
		int x;
		int y;

		public override string ToString()
		{
			return string.Format("({0},{1},{2})", x, y, z); ;
		}

	}
	
	public class Class1
	{
		[Category("First category")]
		public string MyString { get; set; }

		[Category("First category")]
		[DisplayName("This is not propertyName")]
		[Description("My long description")]
		public int MyInt { get; set; }

		[Category("First category")]
		public DateTime MyDateTime { get; set; }

		[Category("Second category")]
		public List<Class1> MyList { get; set; }

		public enum TestEnum
		{
			One, Two, Three
		}

		[Category("Second category")]
		public TestEnum MyEnum { get; set; }

		[Browsable(false)]
		public int MyHidden { get; set; }

		[Category("Second category")]
		public int MyReadonly { get; private set; }

		[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
		public string MyPath { get; set; }

		public Struct1 MyStruct { get; set; }

	}
}
