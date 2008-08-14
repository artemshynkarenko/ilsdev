using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Data.SqlClient;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Tranings.Plugs.Kernel.Test.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			
			
			Plug plug = new Plug();
			plug.Active = false;
			plug.PlugName = "Aloha oe!";
			plug.PlugFriendlyName = "Hello world plug";
			plug.PlugVersion = "0.0.0";
			plug.PlugDescription = "test plug";

			using (PlugFactory factory = new PlugFactory())
			{
				SqlTransactionContext context = new SqlTransactionContext();
				context.Connection = new SqlConnection("server=localhost;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
				//Should be somewhere in SqlAction.
				context.Connection.Open();
				factory.Context = context;
				factory.Insert(plug);
			}
			
		}
	}
}
