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
        static Plug ParsePlug(string s)
        {
            string[] ss = s.Split(new string[] {" ", "\t", "\n", "\r"}, StringSplitOptions.RemoveEmptyEntries);
            Plug plug = new Plug();
            plug.PlugName           = ss[0];
            plug.PlugFriendlyName   = ss[1];
            plug.PlugDescription    = ss[2];
            plug.PlugVersion        = ss[3];
            plug.Active             = int.Parse(ss[4]) != 0;
            return plug;
        }
        static void PrintPlug(Plug plug)
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
            PlugFactory factory = factory = new PlugFactory();
			SqlTransactionContext context = new SqlTransactionContext();
			context.Connection = new SqlConnection("server=localhost;database=ASH_Trainings_RemoteAdmin;uid=sa;pwd=1");
			//Should be somewhere in SqlAction.
			context.Connection.Open();
			factory.Context = context;

            System.Console.WriteLine("Possible commands:\n\tadd\n\tdelete\n\tupdate\n\tloadall\n\tloadbyid\n\tloadbyname\n\tquit\n");
            bool done = false;
            while (!done)
            {
                System.Console.Write("#");
                string cmd = System.Console.ReadLine();
                switch (cmd)
                {
                    case "add":
                        System.Console.WriteLine("Input <PlugName> <PlugFriendlyName> <PlugDescription> <PlugVersion> <Active(0|1)>");
                        string tmp = System.Console.ReadLine();
                        Plug plug = ParsePlug(tmp);
                        factory.Insert(plug);
                        break;
                    case "quit":
                        done = true;
                        break;
                    case "delete":
                    case "update":
                    case "loadall":
                        List<Plug> lst = factory.LoadAll();
                        foreach(Plug pl in lst)
                        {
                            PrintPlug(pl);
                            System.Console.WriteLine();
                        }
                        break;
                    case "loadbyid":
                    case "loadbyname":
                        break;
                    default:
                        System.Console.WriteLine("Possible commands:\n\tadd\tdelete\tupdate\tloadall\tloadbyid\tloadbyname\n");
                        break;
                }
                System.Console.WriteLine("<OK>");
            }
		}
	}
}
