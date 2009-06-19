using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Transporting3
{
    public partial class FormNewProblem : Form
    {

        

        public FormNewProblem()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0 && numericUpDown1.Value <= 1000 &&
                numericUpDown2.Value > 0 && numericUpDown2.Value <= 1000)
            {
                form1.Show();
                Close();
                form1.NewProblem((int)numericUpDown1.Value, (int)numericUpDown2.Value);
            }

        }

        private void FormNewProblem_FormClosed(object sender, FormClosedEventArgs e)
        {
            form1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form1.Show();
            Close();
        }


    }
}
