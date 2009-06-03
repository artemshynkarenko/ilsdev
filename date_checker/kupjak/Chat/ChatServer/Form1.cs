using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ChatServer
{
    public partial class form_server : Form
    {
        private delegate void UpdateStatusCallback(string strMessage);

        public form_server()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            // Parse the server's IP address out of the TextBox
            IPAddress ipAddr = IPAddress.Parse(text_ip.Text);
            // Create a new instance of the ChatServer object
            ChatServer mainServer = new ChatServer(ipAddr);
            // Hook the StatusChanged event handler to mainServer_StatusChanged
            ChatServer.StatusChanged += new StatusChangedEventHandler(mainServer_StatusChanged);
            // Start listening for connections
            mainServer.StartListening();
            // Show that we started to listen for connections
            text_log.AppendText("Monitoring for connections...\r\n");
        }

        public void mainServer_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Call the method that updates the form
            this.Invoke(new UpdateStatusCallback(this.UpdateStatus), new object[] { e.EventMessage });
        }

        private void UpdateStatus(string strMessage)
        {
            // Updates the log with the message
            text_log.AppendText(strMessage + "\r\n");
        }

        private void form_server_Load(object sender, EventArgs e)
        {
            string strHostName = "";
            // Getting Ip address of local machine...
            // First get the host name of local machine.
            strHostName = Dns.GetHostName();

            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostByName(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            text_ip.Text = addr[0].ToString();

             
        }
    }
}