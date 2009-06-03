namespace ChatClient
{
    partial class form_client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_connect = new System.Windows.Forms.Button();
            this.text_ip_server = new System.Windows.Forms.TextBox();
            this.label_ip_server = new System.Windows.Forms.Label();
            this.button_send = new System.Windows.Forms.Button();
            this.text_message = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.label_name = new System.Windows.Forms.Label();
            this.text_name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(216, 34);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(75, 23);
            this.button_connect.TabIndex = 0;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // text_ip_server
            // 
            this.text_ip_server.Location = new System.Drawing.Point(82, 11);
            this.text_ip_server.Name = "text_ip_server";
            this.text_ip_server.Size = new System.Drawing.Size(128, 20);
            this.text_ip_server.TabIndex = 1;
            // 
            // label_ip_server
            // 
            this.label_ip_server.AutoSize = true;
            this.label_ip_server.Location = new System.Drawing.Point(13, 14);
            this.label_ip_server.Name = "label_ip_server";
            this.label_ip_server.Size = new System.Drawing.Size(54, 13);
            this.label_ip_server.TabIndex = 2;
            this.label_ip_server.Text = "Server IP:";
            // 
            // button_send
            // 
            this.button_send.Enabled = false;
            this.button_send.Location = new System.Drawing.Point(216, 351);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(75, 23);
            this.button_send.TabIndex = 3;
            this.button_send.Text = "Send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // text_message
            // 
            this.text_message.Enabled = false;
            this.text_message.Location = new System.Drawing.Point(16, 353);
            this.text_message.Name = "text_message";
            this.text_message.Size = new System.Drawing.Size(194, 20);
            this.text_message.TabIndex = 4;
            this.text_message.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMessage_KeyPress);
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLog.Location = new System.Drawing.Point(16, 63);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(275, 282);
            this.txtLog.TabIndex = 5;
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(13, 40);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(63, 13);
            this.label_name.TabIndex = 6;
            this.label_name.Text = "User Name:";
            // 
            // text_name
            // 
            this.text_name.Location = new System.Drawing.Point(82, 37);
            this.text_name.Name = "text_name";
            this.text_name.Size = new System.Drawing.Size(128, 20);
            this.text_name.TabIndex = 7;
            this.text_name.Text = "noname";
            // 
            // form_client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 388);
            this.Controls.Add(this.text_name);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.text_message);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.label_ip_server);
            this.Controls.Add(this.text_ip_server);
            this.Controls.Add(this.button_connect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "form_client";
            this.Text = "Chat Client";
            this.Load += new System.EventHandler(this.form_client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.TextBox text_ip_server;
        private System.Windows.Forms.Label label_ip_server;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.TextBox text_message;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.TextBox text_name;
    }
}

