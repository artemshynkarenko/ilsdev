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
    public partial class FormDialogCalcOporn : Form
    {
        public FormDialogCalcOporn()
        {
            InitializeComponent();
        }

        private void FormDialogCalcOporn_Load(object sender, EventArgs e)
        {

        }

        private void FormDialogCalcOporn_FormClosed(object sender, FormClosedEventArgs e)
        {
            form1.Visible = true;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            
            if (radioButton2.Checked)
            {
                form1.transport.getPlanNorthWest();
                form1.ShowPlan();
                //form1.CalcPotencial();

                form1.procces = "oporn plan NW";
                form1.textBox1.Text += "\r\n" + "Відшукання опорного плану методом північно-західного кута";
                form1.textBox1.SelectionStart = form1.textBox1.Text.Length;
                form1.textBox1.ScrollToCaret();
                form1.textBox1.Refresh();

                form1.ShowPlan();
            }
            if (radioButton3.Checked)
            {
                form1.procces = "oporn plan MinE";
                form1.textBox1.Text += "\r\n" + "Відшукання опорного плану методом мінімального елемента";
                form1.textBox1.SelectionStart = form1.textBox1.Text.Length;
                form1.textBox1.ScrollToCaret();
                form1.textBox1.Refresh();

                form1.ShowPlan();
            }
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
