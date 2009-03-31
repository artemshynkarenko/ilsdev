using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Interlogic.Trainings.Plugs.Kernel;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using Interlogic.Trainings.Plugs.AbstractUI;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Threading;

namespace Interlogic.Trainings.Plugs.InstallApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AuthtenticationTypeComboBox.SelectedIndex = 0;

        }
        private SqlConnection CreateSQLConnection()
        {
            string serverName = SQLServerText.Text,
                   userName = Username.Text,
                   password = Password.Text,
                   dataBase = DBName.Text;

            bool isIntegratedSecurity = AuthtenticationTypeComboBox.SelectedIndex != 0;
            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();

            if (isIntegratedSecurity)
            {
                connectionString.Add("Integrated Security", "SSPI");
            }
            else
            {
                connectionString.UserID = userName;
                connectionString.Password = password;
            }
            if (!String.IsNullOrEmpty(dataBase))
                connectionString.InitialCatalog = dataBase;
            connectionString.DataSource = serverName;

            connection.ConnectionString = connectionString.ToString();
            return connection;
        }
        private void OK_Click(object sender, EventArgs e)
        {

            SqlTransactionContext context = new SqlTransactionContext();
            context.Connection = CreateSQLConnection();
            bool isNewIstallation = radioButton1.Checked;
            if (isNewIstallation)
            {
                //TODO:remove
                //connection.ConnectionString = "Data Source=TORAX;Initial Catalog=ASH;User ID=sa;Password=1";


                ProgressForm progressForm = new ProgressForm();
                progressForm.Show();
                SaveConnectionInfo();
                try
                {
                    KernelPlugInstaller kernelInstaller = new KernelPlugInstaller();
                    kernelInstaller.InitialDir = @"c:\temp";
                    kernelInstaller.RegisterPlug(context);


                    AbstractUIPlugInstaller abstractUIInstaller = new AbstractUIPlugInstaller();
                    abstractUIInstaller.RegisterPlug(context);

                    //Add ProgressForm here to see 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());

                }
                progressForm.TopMost = true;
                progressForm.Hide();
                progressForm.ShowDialog();

            }
            //context.Connection = connection;
            try
            {
                PlugListForm plugListForm = new PlugListForm();
                plugListForm.ShowPlugins(context);

                this.Hide();
                plugListForm.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveConnectionInfo()
        {


        }

        private void SQLServer_DropDown(object sender, EventArgs e)
        {
            FillListOfSQLServer();
        }

        private void FillListOfSQLServer()
        {
            if (SQLServerText.Items.Count != 0)
                return;
            try
            {
                DataTable servers = SmoApplication.EnumAvailableSqlServers(false);
                SQLServerText.Items.Clear();
                List<string> serverList = new List<string>();

                foreach (DataRow serverInfo in servers.Rows)
                {
                    serverList.Add(serverInfo["Name"].ToString());
                }
                serverList.Sort();
                foreach (string serverName in serverList)
                {
                    SQLServerText.Items.Add(serverName);
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

            if (!String.IsNullOrEmpty(SQLServerText.Name))
            {
                FillDatabaseList();

            }

        }
        private void FillDatabaseList()
        {
            try
            {
                SqlConnection sqlConnection = CreateSQLConnection();
                ServerConnection serverConnection = new ServerConnection(sqlConnection);
                Server server = new Server(serverConnection);
                DatabaseCollection databaseCollection = server.Databases;
                DBName.Items.Clear();
                List<string> dbList = new List<string>();
                foreach (Database db in databaseCollection)
                    dbList.Add(db.Name);
                dbList.Sort();
                foreach (string dbName in dbList)
                {
                    DBName.Items.Add(DBName);
                }
            }
            catch (Exception ex)
            {

            }
        }




        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBox1.SelectedIndex != 0)
            //{
            //    SQLServer.Text = "server" + comboBox1.SelectedIndex.ToString();
            //}
            //panel2.Visible = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PlugListForm form = new PlugListForm();
            form.Show();
        }



        private void AuthtenticationTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isIntegratedSecurity = AuthtenticationTypeComboBox.SelectedIndex != 0;
            Username.Enabled = !isIntegratedSecurity;
            Password.Enabled = !isIntegratedSecurity;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
