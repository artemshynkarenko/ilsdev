using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Common;

namespace ChatClient
{
    public partial class form_client : Form
    {
        // Will hold the user name
        private string UserName = "Unknown";
        private StreamWriter swSender;
        private StreamReader srReceiver;
        private TcpClient tcpServer;
        // Needed to update the form with messages from another thread
        private delegate void UpdateLogCallback(string strMessage);
        // Needed to set the form to a "disconnected" state from another thread
        private delegate void CloseConnectionCallback(string strReason);
        private Thread thrMessaging;
        private IPAddress ipAddr;

        private bool Connected;

        public form_client()
        {
            // On application exit, don't forget to disconnect first
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            InitializeComponent();
        }

        // The event handler for application exit
        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (Connected == true)
            {
                // Closes the connections, streams, etc.
                Connected = false;
                swSender.Close();
                srReceiver.Close();
                tcpServer.Close();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // If we are not currently connected but awaiting to connect
            if (Connected == false)
            {
                // Initialize the connection
                InitializeConnection();
            }
            else // We are connected, thus disconnect
            {
                CloseConnection("Disconnected at user's request.");
            }
        }

        private void InitializeConnection()
        {
            // Parse the IP address from the TextBox into an IPAddress object
            ipAddr = IPAddress.Parse(text_ip_server.Text);
            // Start a new TCP connections to the chat server
            tcpServer = new TcpClient();
            tcpServer.Connect(ipAddr, Consts.port);

            // Helps us track whether we're connected or not
            Connected = true;
            // Prepare the form
            UserName = text_name.Text;

            // Disable and enable the appropriate fields
            text_ip_server.Enabled = false;
            text_name.Enabled = false;
            text_message.Enabled = true;
            button_send.Enabled = true;
            button_connect.Text = "Disconnect";

            // Send the desired username to the server
            swSender = new StreamWriter(tcpServer.GetStream());
            swSender.WriteLine(text_name.Text);
            swSender.Flush();

            // Start the thread for receiving messages and further communication
            thrMessaging = new Thread(new ThreadStart(ReceiveMessages));
            thrMessaging.Start();
        }

        private void ReceiveMessages()
        {
            // Receive the response from the server
            srReceiver = new StreamReader(tcpServer.GetStream());
            // If the first character of the response is 1, connection was successful
            string ConResponse = srReceiver.ReadLine();
            // If the first character is a 1, connection was successful
            if (ConResponse[0] == '1')
            {
                // Update the form to tell it we are now connected
                this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { "Connected Successfully!" });
            }
            else // If the first character is not a 1 (probably a 0), the connection was unsuccessful
            {
                string Reason = "Not Connected: ";
                // Extract the reason out of the response message. The reason starts at the 3rd character
                Reason += ConResponse.Substring(2, ConResponse.Length - 2);
                // Update the form with the reason why we couldn't connect
                this.Invoke(new CloseConnectionCallback(this.CloseConnection), new object[] { Reason });
                // Exit the method
                return;
            }
            // While we are successfully connected, read incoming lines from the server
            while (Connected)
            {
                // Show the messages in the log TextBox
                this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { srReceiver.ReadLine() });
            }
        }

        // This method is called from a different thread in order to update the log TextBox
        private void UpdateLog(string strMessage)
        {
            // Append text also scrolls the TextBox to the bottom each time
            txtLog.AppendText(strMessage + "\r\n");
        }

        // Closes a current connection
        private void CloseConnection(string Reason)
        {
            // Show the reason why the connection is ending
            txtLog.AppendText(Reason + "\r\n");
            // Enable and disable the appropriate controls on the form
            text_ip_server.Enabled = true;
            text_name.Enabled = true;
            text_message.Enabled = false;
            button_send.Enabled = false;
            button_connect.Text = "Connect";

            // Close the objects
            Connected = false;
            swSender.Close();
            srReceiver.Close();
            tcpServer.Close();
        }

        // Sends the message typed in to the server
        private void SendMessage()
        {
            if (text_message.Lines.Length >= 1)
            {
                swSender.WriteLine(text_message.Text);
                swSender.Flush();
                text_message.Lines = null;
            }
            text_message.Text = "";
        }

        // We want to send the message when the Send button is clicked
        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        // But we also want to send the message once Enter is pressed
        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If the key is Enter
            if (e.KeyChar == (char)13)
            {
                SendMessage();
            }
        }

        private void form_client_Load(object sender, EventArgs e)
        {
            string strHostName = "";
            // Getting Ip address of local machine...
            // First get the host name of local machine.
            strHostName = Dns.GetHostName();

            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostByName(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            text_ip_server.Text = addr[0].ToString();
        }
    }
}