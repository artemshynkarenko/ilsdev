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
        static PlugIn ParsePlug(string s)
        {
            string[] ss = s.Split(new string[] {" ", "\t", "\n", "\r"}, StringSplitOptions.RemoveEmptyEntries);
            PlugIn plug = new PlugIn();
            plug.PlugName           = ss[0];
            plug.PlugFriendlyName   = ss[1];
            plug.PlugDescription    = ss[2];
            plug.PlugVersion        = ss[3];
            plug.Active             = int.Parse(ss[4]) != 0;
            return plug;
        }
        static void PrintPlug(PlugIn plug)
        {
            System.Console.WriteLine("Id\t\t = {0}", plug.PlugId);
            System.Console.WriteLine("Name\t\t = {0}", plug.PlugName);
            System.Console.WriteLine("FriendlyName\t = {0}", plug.PlugFriendlyName);
            System.Console.WriteLine("Description\t = {0}", plug.PlugDescription);
            System.Console.WriteLine("Version\t\t = {0}", plug.PlugVersion);
            System.Console.WriteLine("Active\t\t = {0}", plug.Active);
        }
		static void Main(string[] args)
		{
            SqlTransactionContext context = new SqlTransactionContext();
            //context.Connection = new SqlConnection("server=stranger;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
            context.Connection = new SqlConnection("server=localhost;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
            PlugInController manager = new PlugInController(context);
			try
			{
				System.Console.WriteLine("Possible commands:\n\tadd\n\tdelete\n\tupdate\n\tloadall\n\tloadbyid\n\tloadbyname\n\tquit\n");
				bool done = false;
				while (!done)
				{
					System.Console.Write("#");
					string cmd = System.Console.ReadLine();
                    PlugIn plug = new PlugIn();
					switch (cmd)
					{
						case "add":
							System.Console.WriteLine("Input <PlugName> <PlugFriendlyName> <PlugDescription> <PlugVersion> <Active(0|1)>");
							string tmp = System.Console.ReadLine();
							plug = ParsePlug(tmp);
							manager.Insert(plug);
							break;

						case "quit":
							done = true;
							break;

						case "delete":
                            System.Console.Write("Input PlugId: ");
                            plug.PlugId = int.Parse(System.Console.ReadLine());
                            //manager.Delete(plug);
                            break;

                        case "update":
                            System.Console.WriteLine("Not implemented yet");
                            break;

						case "loadall":
							List<PlugIn> lst = manager.LoadAll();
							foreach (PlugIn pl in lst)
							{
								PrintPlug(pl);
								System.Console.WriteLine();
							}
							break;

						case "loadbyid":
                            System.Console.Write("Input PlugId: ");
                            //plug = manager.LoadByPrimaryKey(int.Parse(System.Console.ReadLine()));
                            PrintPlug(plug);
                            break;

						case "loadbyname":
                            System.Console.Write("Input PlugName: ");
                            //plug = manager.LoadByName(System.Console.ReadLine());
                            PrintPlug(plug);
                            break;

						default:
							System.Console.WriteLine("Possible commands:\n\tadd\tdelete\tupdate\tloadall\tloadbyid\tloadbyname\n");
							break;
					}
					System.Console.WriteLine("<OK>");
				}
				
			}
			finally
			{
				//this is very very important
				context.Connection.Close();
			}
		}
	}
}
