using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using Interlogic.Trainings.Plugs.Kernel;
using Microsoft.SqlServer.Management.Smo;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Data.SqlClient;
namespace Interlogic.Trainings.Plugs.InstallApp
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void OK_Click(object sender, EventArgs e)
		{
			string serverName = SQLServer.Text,
							userName = Username.Text,
							password = Password.Text,
							dataBase = DBName.Text;

			SqlTransactionContext context = new SqlTransactionContext();
			SqlConnection connection = new SqlConnection();
			SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
			connectionString.UserID = userName;
			connectionString.Password = password;
			connectionString.DataSource = serverName;
			connectionString.InitialCatalog = dataBase;
			connection.ConnectionString = connectionString.ToString();

			bool isNewIstallation = true;
			if (isNewIstallation)
			{
				//TODO:remove
				connection.ConnectionString = "Data Source=stranger;Initial Catalog=ASH_Trainings_RemoteAdmin2;User ID=sa;Password=1";

				context.Connection = connection;
				KernelPlugInstaller installer = new KernelPlugInstaller();
				installer.InitialDir = @"c:\temp";
				installer.RegisterPlug(context);
				//Add ProgressForm here to see 
			}

			PlugInController controller = new PlugInController(context);
			List<PlugIn> plugs = controller.LoadAll();
			//Show plugs in property grid
		}

		private void SQLServer_DropDown(object sender, EventArgs e)
		{
			FillListOfSQLServer();
		}

		private void FillListOfSQLServer()
		{
			if (SQLServer.Items.Count != 0)
				return;
			try
			{
				DataTable servers = SmoApplication.EnumAvailableSqlServers(false);
				SQLServer.Items.Clear();
				foreach (DataRow serverInfo in servers.Rows)
				{
					SQLServer.Items.Add(serverInfo["Name"].ToString());
				}
			}
			catch (Exception e)
			{
				//Sorry guys we can't enumerate SQL servers
				//TODO: Write something else here
			}
		}

		private void DBName_DropDown(object sender, EventArgs e)
		{

			if (!String.IsNullOrEmpty(SQLServer.Name))
			{
				FillDBList(SQLServer.Text, Username.Text, Password.Text);

			}

		}

		private void FillDBList(string serverName, string userName, string password)
		{
			try
			{

				ServerConnection connection = new ServerConnection(serverName, userName, password);
				Server server = new Server(connection);
				DatabaseCollection databaseCollection = server.Databases;
				DBName.Items.Clear();
				foreach (Database db in databaseCollection)
					DBName.Items.Add(db.Name);
			}
			catch (Exception ex)
			{

			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox1.SelectedIndex != 0)
			{
				SQLServer.Text = "server" + comboBox1.SelectedIndex.ToString();
			}
			panel2.Visible = true;

		}

		private void button1_Click(object sender, EventArgs e)
		{
			PlugListForm form = new PlugListForm();
			form.Show();
		}

	}
}
