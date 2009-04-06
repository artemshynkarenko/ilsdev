using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Interlogic.Trainings.Plugs.Kernel.Logging
{
	public class Logger
	{
		public static void LogVerbose(string text)
		{
			Log("Verbose", text);
		}

		public static void LogInformation(object obj)
		{
			LogInformation(obj);
		}

		public static void LogInformation(string text)
		{
			Log("Information", text);
		}

		public static void LogError(Exception exception)
		{
			LogError(exception.ToString());
		}

		public static void LogError(string text)
		{
			Log("Error", text);
		}

		public static void LogFatal(string text)
		{
			Log("Fatal", text);
		}

		public static void Log(string category, string text)
		{
			Debug.WriteLine(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "[" + category.ToUpper() + "]: " + text);
		}
	}
}
