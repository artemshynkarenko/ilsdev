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
    public partial class Form1 : Form
    {
        public TransportProblem transport;

        public string procces = "new problem";
        public bool goNext = true;

        public Form1()
        {
            InitializeComponent();
        }

        public void inputProblem() 
        {
            int m = transport.m;
            int n = transport.n;

            for (int i = 0; i < m; ++i)
                if (dataGridView1.Rows[i + 2].Cells[2].Value.ToString() == "" || dataGridView1.Rows[i + 2].Cells[2].Value.ToString() == "-")
                    transport.a[i] = 0;
                else
                    transport.a[i] = (int)dataGridView1.Rows[i + 2].Cells[2].Value;

            for (int i = 0; i < n; ++i)
                if (dataGridView1.Rows[1].Cells[i + 3].Value.ToString() == "" || dataGridView1.Rows[1].Cells[i + 3].Value.ToString() == "-")
                    transport.b[i] = 0;
                else
                    transport.b[i] = (int)dataGridView1.Rows[1].Cells[i + 3].Value;

            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    if (dataGridView1.Rows[i + 2].Cells[j + 3].Value.ToString() == "" || dataGridView1.Rows[i + 2].Cells[j + 3].Value.ToString() == "-")
                        transport.c[i, j] = 0;
                    else
                        transport.c[i, j] = (int)dataGridView1.Rows[i + 2].Cells[j + 3].Value;
        }

        public void ShowProblem() 
        {
            int m = transport.m;
            int n = transport.n;
            dataGridView1.RowCount = m + 2;
            dataGridView1.ColumnCount = n + 2 +1;
            dataGridView1.ReadOnly = false;

            dataGridView1.Rows[0].Cells[1].ReadOnly = true;
            dataGridView1.Rows[0].Cells[2].ReadOnly = true;
            dataGridView1.Rows[1].Cells[1].ReadOnly = true;
            dataGridView1.Rows[1].Cells[2].ReadOnly = true;

            dataGridView1.Rows[0].Cells[0].ReadOnly = true;
            dataGridView1.Rows[0].Cells[0].Value = "Умова задачі";

            for (int i = 0; i < m; ++i)
            {
                dataGridView1.Rows[i + 2].Cells[1].Value = "A" + (i + 1);
                dataGridView1.Rows[i + 2].Cells[1].ReadOnly = true;
                dataGridView1.Rows[i + 2].Cells[2].ValueType = typeof(int);
                dataGridView1.Rows[i + 2].Cells[2].ReadOnly = false;
                dataGridView1.Rows[i + 2].Cells[2].Value = transport.a[i];

            }
            for (int i = 0; i < n; ++i)
            {
                dataGridView1.Rows[0].Cells[i + 3].Value = "B" + (i + 1);
                dataGridView1.Rows[0].Cells[i + 3].ReadOnly = true;
                dataGridView1.Rows[1].Cells[i + 3].ValueType = typeof(int);
                dataGridView1.Rows[1].Cells[i + 3].ReadOnly = false;
                dataGridView1.Rows[1].Cells[i + 3].Value = transport.b[i];
            }
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                {
                    dataGridView1.Rows[i + 2].Cells[j + 3].ReadOnly = false;
                    dataGridView1.Rows[i + 2].Cells[j + 3].ValueType = typeof(int);
                    dataGridView1.Rows[i + 2].Cells[j + 3].Value = transport.c[i, j];
                }

        }

        public void ShowPlan()
        {
            int m = transport.m;
            int n = transport.n;
            dataGridView1.Rows.Clear();
            ShowProblem();
            dataGridView1.RowCount += m + 3;
            dataGridView1.ReadOnly = false;
            for (int i = 0; i < m + 2; ++i)
                for (int j = 0; j < n + 3; ++j)
                    dataGridView1.Rows[i].Cells[j].ReadOnly = true;

            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    transport.x[i, j] = -1;

            dataGridView1.Rows[m + 3].Cells[0].ReadOnly = true;
            dataGridView1.Rows[m + 3].Cells[0].Value = "Опорний план";

            for (int i = 0; i < m; ++i)
            {
                dataGridView1.Rows[i + 2 + m+3].Cells[1].Value = "A" + (i + 1);
                dataGridView1.Rows[i + 2 + m+3].Cells[2].ValueType = typeof(int);
                dataGridView1.Rows[i + 2 + m+3].Cells[2].Value = transport.a[i];

            }
            for (int i = 0; i < n; ++i)
            {
                dataGridView1.Rows[0 + m+3].Cells[i + 3].Value = "B" + (i + 1);
                dataGridView1.Rows[1 + m+3].Cells[i + 3].ValueType = typeof(int);
                dataGridView1.Rows[1 + m+3].Cells[i + 3].Value = transport.b[i];
            }

            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                {
                    dataGridView1.Rows[i + 2 + m + 3].Cells[j + 3].ReadOnly = false;
                    //dataGridView1.Rows[i + 2 + m+3].Cells[j + 3].ValueType = typeof(int);
                    if (transport.x[i, j] == -1)
                        dataGridView1.Rows[i + 2 + m + 3].Cells[j + 3].Value = "-";
                    else
                        dataGridView1.Rows[i + 2 + m+3].Cells[j + 3].Value = transport.x[i, j];
                }
            ВиробникАToolStripMenuItem.Enabled = false;
            СпоживачToolStripMenuItem.Enabled = false;
        }

        public void ShowPotencials()
        {

            for (int i = 0; i < dataGridView1.RowCount; ++i)
                for (int j = 0; j < dataGridView1.ColumnCount; ++j)
                    dataGridView1.Rows[i].Cells[j].ReadOnly = true;

            dataGridView1.ReadOnly = false;
        
            int m = transport.m;
            int n = transport.n;
            int r = dataGridView1.RowCount;
            dataGridView1.RowCount += m + 3 + 2;
            dataGridView1.ColumnCount = n + 5;

            dataGridView1.Rows[r + 1].Cells[0].ReadOnly = true;
            dataGridView1.Rows[r + 1].Cells[0].Value = "Потенціали";

            for (int i = 0; i < m; ++i)
            {
                dataGridView1.Rows[i + r + 3].Cells[1].Value = "A" + (i + 1);
                dataGridView1.Rows[i + r + 3].Cells[1].ReadOnly = true;
                dataGridView1.Rows[i + r + 3].Cells[2].ValueType = typeof(int);
                dataGridView1.Rows[i + r + 3].Cells[2].Value = transport.a[i];
                dataGridView1.Rows[i + r + 3].Cells[2].ReadOnly = true;

                dataGridView1.Rows[i + r + 3].Cells[1+ 2+n+1].Value = "a" + (i + 1);
                dataGridView1.Rows[i + r + 3].Cells[1 + 2 + n + 1].ReadOnly = true;
                dataGridView1.Rows[i + r + 3].Cells[1+ 2+n].ValueType = typeof(int);
                dataGridView1.Rows[i + r + 3].Cells[1+ 2+n].Value = 0;
                transport.alpha[i] = 0;

            }
            for (int i = 0; i < n; ++i)
            {
                dataGridView1.Rows[0 + r + 1].Cells[i + 3].Value = "B" + (i + 1);
                dataGridView1.Rows[0 + r + 1].Cells[i + 3].ReadOnly = true; 
                dataGridView1.Rows[1 + r + 1].Cells[i + 3].ValueType = typeof(int);
                dataGridView1.Rows[1 + r + 1].Cells[i + 3].Value = transport.b[i];
                dataGridView1.Rows[1 + r + 1].Cells[i + 3].ReadOnly = true;

                dataGridView1.Rows[1 + r + 1 + 2+m].Cells[i + 3].Value = "b" + (i + 1);
                dataGridView1.Rows[1 + r + 1 + 2 + m].Cells[i + 3].ReadOnly = true;
                dataGridView1.Rows[0 + r + 1 + 2+m].Cells[i + 3].ValueType = typeof(int);
                dataGridView1.Rows[0 + r + 1 + 2+m].Cells[i + 3].Value = 0;
                transport.beta[i] = 0;
            }



            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                {
                    dataGridView1.Rows[i + r + 3].Cells[j + 3].ValueType = typeof(int);
                    dataGridView1.Rows[i + r + 3].Cells[j + 3].Value = 0;
                    transport.delta[i, j] = 0;
                }

        }

        public void ShowRes()
        {
            dataGridView1.ReadOnly = false;
            for (int i = 0; i < dataGridView1.RowCount; ++i)
                for (int j = 0; j < dataGridView1.ColumnCount; ++j)
                    dataGridView1.Rows[i].Cells[j].ReadOnly = true;

            int m = transport.m;
            int n = transport.n;
            int r = dataGridView1.RowCount;
            dataGridView1.RowCount += m + 3;
            dataGridView1.Rows[r + 1].Cells[0].ReadOnly = false;
            dataGridView1.Rows[r + 1].Cells[0].Value = "План";

            for (int i = 0; i < m; ++i)
            {
                dataGridView1.Rows[i + r + 3].Cells[1].Value = "A" + (i + 1);
                dataGridView1.Rows[i + r + 3].Cells[1].ReadOnly = true;
                dataGridView1.Rows[i + r + 3].Cells[2].ValueType = typeof(int);
                dataGridView1.Rows[i + r + 3].Cells[2].Value = transport.a[i];
                dataGridView1.Rows[i + r + 3].Cells[2].ReadOnly = true;
            }
            for (int i = 0; i < n; ++i)
            {
                dataGridView1.Rows[0 + r + 1].Cells[i + 3].Value = "B" + (i + 1);
                dataGridView1.Rows[0 + r + 1].Cells[i + 3].ReadOnly = true;
                dataGridView1.Rows[1 + r + 1].Cells[i + 3].ValueType = typeof(int);
                dataGridView1.Rows[1 + r + 1].Cells[i + 3].Value = transport.b[i];
                dataGridView1.Rows[1 + r + 1].Cells[i + 3].ReadOnly = true;
            }

            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                {
                    if (transport.x[i, j] == -1)
                        dataGridView1.Rows[i + r + 3].Cells[j + 3].Value = "-";
                    else
                        dataGridView1.Rows[i + r + 3].Cells[j + 3].Value = transport.x[i, j];
                    dataGridView1.Rows[i + r + 3].Cells[j + 3].ReadOnly = false;
                }

        }



        public void dialogFindCycle()
        {

        }
        


        public void NewProblem(int m, int n)
        {
            випадковаЗадачаToolStripMenuItem.Enabled = true;
            transport.SetSize(m, n);
            ShowProblem();
            textBox1.Text += "\r\n" + "Нова транспортна задача розміром " + m + "x" + n;
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.Refresh();

            toolStripStatusLabelStatus.Text = "Перевірити дані?";
            procces = "input";

            ВиробникАToolStripMenuItem.Enabled = true;
            СпоживачToolStripMenuItem.Enabled = true;
            зберегтиЗадачуToolStripMenuItem.Enabled = true;
        }

        private void новаЗадачаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formNewProblem = new FormNewProblem();
            formNewProblem.form1 = this;
            formNewProblem.Show();
            this.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            transport = new TransportProblem();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void завантадитиЗадачуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                випадковаЗадачаToolStripMenuItem.Enabled = true ;
                transport.LoadFromFile(openFileDialog1.FileName);
                //NewProblem(transport.m, transport.n);
                ShowProblem();
                textBox1.Text += "\r\n" + "Завантажина із файла Транспортна задача розміром " + transport.m + "x" + transport.n;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
                textBox1.Refresh();

                toolStripStatusLabelStatus.Text = "Введення даних транспортної задачі";
                procces = "input";

                ВиробникАToolStripMenuItem.Enabled = true;
                СпоживачToolStripMenuItem.Enabled = true;
                зберегтиЗадачуToolStripMenuItem.Enabled = true;
            }
        }

        private void зберегтиЗадачуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputProblem();
            DialogResult res = saveFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                transport.SaveToFile(saveFileDialog1.FileName);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void toolStripStatusLabelOk_Click(object sender, EventArgs e)
        {
            if (procces == "new problem")
            {
                
                новаЗадачаToolStripMenuItem_Click(sender, e);
                return;
            }
            if (procces == "input")
            {
                int m = transport.m;
                int n = transport.n;

                for (int i=0; i<m; ++i)
                    transport.a[i] = (int)dataGridView1.Rows[i + 2].Cells[2].Value;
            
                for (int i=0; i<n; ++i)
                    transport.b[i] = (int)dataGridView1.Rows[1].Cells[i+3].Value;

                for (int i=0; i<m; ++i)
                    for (int j=0; j<n; ++j)
                        transport.c[i,j] = (int)dataGridView1.Rows[i + 2].Cells[j + 3].Value;
                int sumA = 0;
                for (int i = 0; i < m; ++i)
                    sumA += transport.a[i];
                int sumB = 0;
                for (int i = 0; i < n; ++i)
                    sumB += transport.b[i];

                if (sumA == sumB)
                {
                    textBox1.Text += "\r\n" + "Задача закрита";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();

                    if (goNext)
                    {
                        випадковаЗадачаToolStripMenuItem.Enabled = false;
                        formDialogCalcOporn = new FormDialogCalcOporn();
                        formDialogCalcOporn.form1 = this;
                        formDialogCalcOporn.Show();
                        this.Visible = false;
                    }
                }
                else
                {
                    textBox1.Text += "\r\n" + "Задача відкрита: ";// +"sum Ai = " + sumA + " , sum Bi = " + sumB;
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();

                }
                return;
            }

            if (procces == "oporn plan NW")
            {
                int m = transport.m;
                int n = transport.n;
                for (int i = 0; i < m; ++i)
                    for (int j = 0; j < n; ++j)
                        if (dataGridView1.Rows[i + dataGridView1.RowCount - m].Cells[j + 3].Value.ToString() == "-")
                            transport.x[i, j] = -1;
                        else
                            transport.x[i, j] = int.Parse(dataGridView1.Rows[i + dataGridView1.RowCount-m].Cells[j + 3].Value.ToString());

                string s = transport.ChekOpornNW();
                if (s == "") 
                {
                    textBox1.Text += "\r\n" + "Опорний план знайдено правильно методом північно-західного кута";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();

                    if (goNext)
                    {
                        procces = "Calc potencial";
                        ShowPotencials();
                        toolStripStatusLabelStatus.Text = "Введіть потенціали. Перевірити?";
                    }
                }
                else
                {
                    textBox1.Text += "\r\n" + "Опорний план знайдено НЕ правильно методом північно-західного кута:";
                    textBox1.Text += "\r\n" + "  " + s;
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();
                }
                return;
            }

            if (procces == "oporn plan MinE")
            {
                int m = transport.m;
                int n = transport.n;
                for (int i = 0; i < m; ++i)
                    for (int j = 0; j < n; ++j)
                        if (dataGridView1.Rows[i + dataGridView1.RowCount - m].Cells[j + 3].Value.ToString() == "-")
                            transport.x[i, j] = -1;
                        else
                            transport.x[i, j] = int.Parse(dataGridView1.Rows[i + dataGridView1.RowCount - m].Cells[j + 3].Value.ToString());

                string s = transport.ChekOpornMinE();
                if (s == "")
                {
                    textBox1.Text += "\r\n" + "Опорний план знайдено правильно методом мінімального едемента";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();

                    if (goNext)
                    {
                        procces = "Calc potencial";
                        ShowPotencials();
                        toolStripStatusLabelStatus.Text = "Введіть потенціали. Перевірити?";
                    }
                }
                else
                {
                    textBox1.Text += "\r\n" + "Опорний план знайдено НЕ правильно методом мінімального елемента:";
                    textBox1.Text += "\r\n" + "  " + s;
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();
                }
                return;
            }

            if (procces == "Calc potencial")
            {
                int m = transport.m;
                int n = transport.n;
                for (int i = 0; i < m; ++i)
                    for (int j = 0; j < n; ++j)
                        transport.delta[i, j] = int.Parse(dataGridView1.Rows[i + dataGridView1.RowCount - m-2].Cells[j + 3].Value.ToString());
                for (int i = 0; i < m; ++i)
                    transport.alpha[i] = int.Parse(dataGridView1.Rows[i + dataGridView1.RowCount - m-2].Cells[3 + n].Value.ToString());
                for (int i = 0; i < n; ++i)
                    transport.beta[i] = int.Parse(dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[i + 3].Value.ToString());

                string s = transport.CheckPotencial();
                if (s == "")
                {
                    textBox1.Text += "\r\n" + "Потенціали знайдено правильно.";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();

                    if (goNext)
                    {
                        procces = "Exist Cycle";
                        toolStripStatusLabelStatus.Text = "Чи план є оптимальний? Введіть + чи -. Перевірити?";
                        textBoxInput.Visible = true;
                    }
                }
                else 
                {
                    textBox1.Text += "\r\n" + "Потенціали знайдено НЕ правильно: ";
                    textBox1.Text += "\r\n" + "  " + s;
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();
                }
                return;
            }

            if (procces == "Exist Cycle")
            {
                if (textBoxInput.Text == "+" || textBoxInput.Text == "-")
                {
                    string s = transport.NextCycle();
                    if (s == "finish" && textBoxInput.Text == "+")
                    {
                        
                        textBox1.Text += "\r\n" + "Транспортна задача розв'язана";
                        textBox1.Text += "\r\n" + "Вартість перевезення - " + transport.GetCost();
                        textBox1.SelectionStart = textBox1.Text.Length;
                        textBox1.ScrollToCaret();
                        textBox1.Refresh();
                        if (goNext)
                        {
                            textBoxInput.Visible = false;
                            toolStripStatusLabelStatus.Text = "Задача розв'язана";
                            procces = "done";
                        }
                        return;
                    }
                    if (s != "finish" && textBoxInput.Text == "-")
                    {
                        textBox1.Text += "\r\n" + "Відповідь правильна";
                        if (goNext)
                        {
                            textBox1.Text += "\r\n" + "Перехід до пошуку циклу";
                            textBox1.SelectionStart = textBox1.Text.Length;
                            textBox1.ScrollToCaret();
                            textBox1.Refresh();
                            procces = "Find Cycle";
                            toolStripStatusLabelStatus.Text = "Введіть в поле цикл, наприклад: A1B1-A1B2-A2B2-A2B1-A1B1.Перевірити?";
                            textBoxInput.Text = "A1B1 - A2B1 - A2B2 - A1B2";
                        }
                        return;
                    }
                    textBox1.Text += "\r\n" + "Відповідь НЕ правильна";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();
                    return;
                }
                else
                {
                    textBox1.Text += "\r\n" + "Введення не коректне: потрібно ввести + або -";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();
                }
                return;
            }

            if (procces == "Find Cycle") 
            {
                string s1 = transport.FindCycle(true);
                string s2 = transport.FindCycle(false);

                string []s = textBoxInput.Text.Split();
                string ss = ""; ;
                foreach (string t in s)
                {
                    ss += t;
                }

                string f1="", f="";
                for (int i = 0; s1[i] != '-' && s1[i] != ' ' && i<s1.Length; ++i)
                    f1 += s1[i];
                for (int i = 0; ss[i] != '-' && ss[i] != ' ' && i<ss.Length; ++i)
                    f += ss[i];

                if (f == f1)
                {
                    textBox1.Text += "\r\n" + "Перший елемент циклу знайдено правильно: " + f;
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();
                }
                else 
                {
                    textBox1.Text += "\r\n" + "Перший елемент циклу знайдено НЕ правильно";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();
                    return;
                }

                if (ss == s1 || ss == s2)
                {
                    
                    textBox1.Text += "\r\n" + "Цикл знайдено правильно: " + ss;
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();
                    if (goNext)
                    {
                        textBoxInput.Visible = false;
                        toolStripStatusLabelStatus.Text = "Зробіть перерозподіл по циклу. Перевірити?";
                        ShowRes();
                        procces = "Go Cycle";
                    }
                    return;
                }
                else 
                {
                    textBox1.Text += "\r\n" + "Цикл знайдено НЕ правильно.";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();
                }
            }
            if (procces == "Go Cycle") 
            {
                int m = transport.m;
                int n = transport.n;
                TransportProblem t = new TransportProblem(m, n);
                for (int i = 0; i < m; ++i)
                    t.a[i] = transport.a[i];
                for (int i = 0; i < n; ++i)
                    t.b[i] = transport.b[i];
                for (int i = 0; i < m; ++i)
                    for (int j = 0; j < n; ++j)
                        if (dataGridView1.Rows[i + dataGridView1.RowCount - m].Cells[j + 3].Value.ToString() == "-")
                            t.x[i, j] = -1;
                        else
                            t.x[i, j] = int.Parse(dataGridView1.Rows[i + dataGridView1.RowCount - m].Cells[j + 3].Value.ToString());
                if (t.ChekOporn()!="") 
                {
                    textBox1.Text += "\r\n" + "Перерозподіл НЕ правильний";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                    textBox1.Refresh();
                    return;
                }
                transport.CalcCycle();
                for (int i = 0; i < m; ++i)
                    for (int j = 0; j < n; ++j) 
                    {
                        if (transport.x[i,j] > 0 && transport.x[i,j] != t.x[i,j] ||
                            transport.x[i,j] <= 0 && t.x[i,j] > 0)
                        {
                            textBox1.Text += "\r\n" + "Перерозподіл НЕ правильний";
                            textBox1.SelectionStart = textBox1.Text.Length;
                            textBox1.ScrollToCaret();
                            textBox1.Refresh();
                            return;
                        }

                    }


                textBox1.Text += "\r\n" + "Перерозподіл правильний";
                textBox1.Text += "\r\n" + "Вартість перевезення - " + transport.GetCost();
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
                textBox1.Refresh();
                if (goNext)
                {
                for (int i = 0; i < m; ++i)
                    for (int j = 0; j < n; ++j)
                        transport.x[i,j] = t.x[i, j];
                procces = "Calc potencial";
                ShowPotencials();
                toolStripStatusLabelStatus.Text = "Введіть потенціали. Перевірити?";
                }
            }
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void додатиВиробникаAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransportProblem t = new TransportProblem(transport.m+1, transport.n);
            for (int i = 0; i < transport.m; ++i)
                t.a[i] = transport.a[i];
            for (int i = 0; i < transport.n; ++i)
                t.b[i] = transport.b[i];
            for (int i = 0; i < transport.m; ++i)
                for (int j = 0; j < transport.n; ++j)
                    t.c[i, j] = transport.c[i, j];
            transport = t;
            ShowProblem();
            textBox1.Text += "\r\n" + "Додано виробника A" + transport.m + ".";
            textBox1.Text += "\r\n" + "Розмір задачі " + transport.m + "x" + transport.n + ".";
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.Refresh();

        }

        private void додатиСпоживачаBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransportProblem t = new TransportProblem(transport.m, transport.n + 1);
            for (int i = 0; i < transport.m; ++i)
                t.a[i] = transport.a[i];
            for (int i = 0; i < transport.n; ++i)
                t.b[i] = transport.b[i];
            for (int i = 0; i < transport.m; ++i)
                for (int j = 0; j < transport.n; ++j)
                    t.c[i, j] = transport.c[i, j];
            transport = t;
            ShowProblem();
            textBox1.Text += "\r\n" + "Додано споживача B" + transport.n + ".";
            textBox1.Text += "\r\n" + "Розмір задачі " + transport.m + "x" + transport.n + ".";
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.Refresh();
        }



        private void видалитиСпоживачаBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transport.n <= 1)
                return;

            TransportProblem t = new TransportProblem(transport.m, transport.n - 1);
            for (int i = 0; i < t.m; ++i)
                t.a[i] = transport.a[i];
            for (int i = 0; i < t.n; ++i)
                t.b[i] = transport.b[i];
            for (int i = 0; i < t.m; ++i)
                for (int j = 0; j < t.n; ++j)
                    t.c[i, j] = transport.c[i, j];
            transport = t;
            ShowProblem();
            textBox1.Text += "\r\n" + "Видалино сподивача B" + (transport.n+1) + ".";
            textBox1.Text += "\r\n" + "Розмір задачі " + transport.m + "x" + transport.n + ".";
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.Refresh();

        }

        private void ВидалитивиробникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transport.m <= 1)
                return;
            TransportProblem t = new TransportProblem(transport.m - 1, transport.n);
            for (int i = 0; i < t.m; ++i)
                t.a[i] = transport.a[i];
            for (int i = 0; i < t.n; ++i)
                t.b[i] = transport.b[i];
            for (int i = 0; i < t.m; ++i)
                for (int j = 0; j < t.n; ++j)
                    t.c[i, j] = t.c[i, j];
            transport = t;
            ShowProblem();
            textBox1.Text += "\r\n" + "Видалино виробника A" + (transport.m+1) + ".";
            textBox1.Text += "\r\n" + "Розмір задачі " + transport.m + "x" + transport.n + ".";
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.Refresh();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void випадковаЗадачаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int m = transport.m;
            int n = transport.n;
            Random r = new Random();
            for (int i = 0; i < m; ++i)
                transport.a[i] = r.Next(1, m * n);

            for (int i = 0; i < n; ++i)
                transport.b[i] = r.Next(1, m * n);
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    transport.c[i, j] = r.Next(1, m * n);
            ShowProblem();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            goNext = !goNext;
            if (goNext)
                toolStripStatusLabelOnlyChek.Text = "Режим лише перевірки крока: -";
            else
                toolStripStatusLabelOnlyChek.Text = "Режим лише перевірки крока: +";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            transport.getPlanNorthWest();
            transport.CalcPotencial();
            transport.NextCycle();
            transport.CalcCycle();
            transport.CalcPotencial();

            textBoxInput.Visible = true;
            procces = "Exist Cycle";
            ShowRes();
        }

  

        
    }
}
