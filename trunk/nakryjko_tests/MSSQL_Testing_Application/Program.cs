using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient; 

namespace MSSQL_Testing_Application
{
    class Program
    {
        static void Main(string[] args)
        {
			//SqlConnection oSQLConn = new SqlConnection(); 
			//oSQLConn.ConnectionString =
			//    "Data Source=(local);" +
			//    "Initial Catalog=ASH_Trainings_RemoteAdmin;" +
			//    "Integrated Security=SSPI";
			//Or
			// "Server=(local);" + 
			// "Database=myDatabaseName;" + 
			// "Trusted_Connection=Yes";
			//oSQLConn.Open();
			//oSQLConn.Close();

			string source = "server=(local)\\NetSDK;" +
									"uid=QSUser;pwd=QSPassword;" +
									"database=Northwind";
			string select = "SELECT ContactName, CompanyName FROM Customers";
			SqlConnection conn = new SqlConnection(source);
			conn.Open();

			SqlCommand cmd = new SqlCommand(select, conn);

			conn.Close();
		}
    }
}
