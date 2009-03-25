using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Interlogic.Trainings.Plugs.InstallApp
{
  
	public partial class ProgressForm : Form
	{
        private class TextBoxWriter : System.IO.TextWriter
        {
            private Encoding encoding;
            private System.Windows.Forms.TextBox textBox;

            public TextBoxWriter(System.Windows.Forms.TextBox textBox)
            {
                if (textBox == null)
                    throw new NullReferenceException();
                this.textBox = textBox;
            }
            public override Encoding Encoding
            {
                get
                {
                    if (this.encoding == null)
                    {
                        this.encoding = new UnicodeEncoding(false, false);
                    }
                    return encoding;
                }
            }
            public override void Write(string value)
            {
                this.textBox.AppendText(value);
            }

            public override void Write(char[] buffer)
            {
                this.Write(new string(buffer));
                Application.DoEvents();
            }
            public override void Write(char[] buffer, int index, int count)
            {
                this.Write(new string(buffer, index, count));
            }
        }

        TextBoxWriter log;   

		public ProgressForm()
		{
			InitializeComponent();
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            log = new TextBoxWriter(ConsoleTextbox);
            Console.SetOut(log);
            

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
   
}
