namespace ChatServer
{
    partial class form_server
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
            this.button_listen = new System.Windows.Forms.Button();
            this.text_ip = new System.Windows.Forms.TextBox();
            this.label_ip = new System.Windows.Forms.Label();
            this.text_log = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_listen
            // 
            this.button_listen.Location = new System.Drawing.Point(245, 10);
            this.button_listen.Name = "button_listen";
            this.button_listen.Size = new System.Drawing.Size(113, 23);
            this.button_listen.TabIndex = 0;
            this.button_listen.Text = "Start Listening";
            this.button_listen.UseVisualStyleBackColor = true;
            this.button_listen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // text_ip
            // 
            this.text_ip.Location = new System.Drawing.Point(82, 12);
            this.text_ip.Name = "text_ip";
            this.text_ip.Size = new System.Drawing.Size(157, 20);
            this.text_ip.TabIndex = 1;
            // 
            // label_ip
            // 
            this.label_ip.AutoSize = true;
            this.label_ip.Location = new System.Drawing.Point(15, 17);
            this.label_ip.Name = "label_ip";
            this.label_ip.Size = new System.Drawing.Size(61, 13);
            this.label_ip.TabIndex = 2;
            this.label_ip.Text = "IP Address:";
            // 
            // text_log
            // 
            this.text_log.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.text_log.Location = new System.Drawing.Point(12, 39);
            this.text_log.Multiline = true;
            this.text_log.Name = "text_log";
            this.text_log.ReadOnly = true;
            this.text_log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.text_log.Size = new System.Drawing.Size(346, 375);
            this.text_log.TabIndex = 3;
            // 
            // form_server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 426);
            this.Controls.Add(this.text_log);
            this.Controls.Add(this.label_ip);
            this.Controls.Add(this.text_ip);
            this.Controls.Add(this.button_listen);
            this.Name = "form_server";
            this.Text = "Chat Server";
            this.Load += new System.EventHandler(this.form_server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_listen;
        private System.Windows.Forms.TextBox text_ip;
        private System.Windows.Forms.Label label_ip;
        private System.Windows.Forms.TextBox text_log;
    }
}

