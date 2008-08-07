using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Globalization;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;


namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public class RawSqlGenerator
	{
		public void AnalizeSqlAttributes(Type type)
		{
		}

		public string GetInsertSql(DomainObject obj)
		{
			return null;
		}

		public string GetUpdateSql(DomainObject obj)
		{
			return null;
		}

		public string GetDeleteSql(DomainObject obj)
		{
			return null;
		}

		public string GetSelectSql(DomainObject obj)
		{
			return null;
		}

		#region TranslateToSql
		/// <summary>
		/// Translate value to safe sql Script value
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string TranslateToSql(object value)
		{
			string strValue = "";
			if (value == DBNull.Value)
				strValue = Null;
			else
			{
				switch (value.GetType().Name)
				{
					case "String":
						strValue = "N'" + ((string)value).Replace("'", "''") + "'";
						break;
					case "DateTime":
						strValue = "'" + ((DateTime)value).ToString("yyyy-MM-dd hh:mm:ss", DateTimeFormatInfo.InvariantInfo) + "'";
						break;
					case "Double":
						strValue = ((Double)value).ToString(NumberFormatInfo.InvariantInfo);
						break;
					case "Single":
						strValue = ((Single)value).ToString(NumberFormatInfo.InvariantInfo);
						break;
					case "Boolean":
						strValue = ((Boolean)value) ? "1" : "0";
						break;
					case "Guid":
						strValue = "'" + value.ToString() + "'";
						break;
					default:
						strValue = value.ToString();
						break;
				}
			}
			return strValue;
		}
		#endregion

		/// <summary>
		/// Return "NULL"
		/// </summary>
		public const string Null = "NULL";
		public const string EndLine = "\r\n";
		public const string GoStatement = EndLine + "GO" + EndLine;
	}
}
