namespace Transporting3
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.вихідToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новаЗадачаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.випадковаЗадачаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зберегтиЗадачуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.завантадитиЗадачуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ВиробникАToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.додатиВиробникаAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ВидалитивиробникаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.СпоживачToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.додатиСпоживачаBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.видалитиСпоживачаBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.допомогаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проПрограмуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelOk = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStripStatusLabelOnlyChek = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem1,
            this.файлToolStripMenuItem,
            this.допомогаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem1
            // 
            this.файлToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вихідToolStripMenuItem});
            this.файлToolStripMenuItem1.Name = "файлToolStripMenuItem1";
            this.файлToolStripMenuItem1.Size = new System.Drawing.Size(45, 20);
            this.файлToolStripMenuItem1.Text = "Файл";
            // 
            // вихідToolStripMenuItem
            // 
            this.вихідToolStripMenuItem.Name = "вихідToolStripMenuItem";
            this.вихідToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.вихідToolStripMenuItem.Text = "Вихід";
            this.вихідToolStripMenuItem.Click += new System.EventHandler(this.вихідToolStripMenuItem_Click);
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаЗадачаToolStripMenuItem,
            this.випадковаЗадачаToolStripMenuItem,
            this.зберегтиЗадачуToolStripMenuItem,
            this.завантадитиЗадачуToolStripMenuItem,
            this.toolStripMenuItem1,
            this.ВиробникАToolStripMenuItem,
            this.СпоживачToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.файлToolStripMenuItem.Text = "Задача";
            // 
            // новаЗадачаToolStripMenuItem
            // 
            this.новаЗадачаToolStripMenuItem.Name = "новаЗадачаToolStripMenuItem";
            this.новаЗадачаToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.новаЗадачаToolStripMenuItem.Text = "Нова задача";
            this.новаЗадачаToolStripMenuItem.Click += new System.EventHandler(this.новаЗадачаToolStripMenuItem_Click);
            // 
            // випадковаЗадачаToolStripMenuItem
            // 
            this.випадковаЗадачаToolStripMenuItem.Enabled = false;
            this.випадковаЗадачаToolStripMenuItem.Name = "випадковаЗадачаToolStripMenuItem";
            this.випадковаЗадачаToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.випадковаЗадачаToolStripMenuItem.Text = "Випадкова задача";
            this.випадковаЗадачаToolStripMenuItem.Click += new System.EventHandler(this.випадковаЗадачаToolStripMenuItem_Click);
            // 
            // зберегтиЗадачуToolStripMenuItem
            // 
            this.зберегтиЗадачуToolStripMenuItem.Enabled = false;
            this.зберегтиЗадачуToolStripMenuItem.Name = "зберегтиЗадачуToolStripMenuItem";
            this.зберегтиЗадачуToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.зберегтиЗадачуToolStripMenuItem.Text = "Зберегти задачу";
            this.зберегтиЗадачуToolStripMenuItem.Click += new System.EventHandler(this.зберегтиЗадачуToolStripMenuItem_Click);
            // 
            // завантадитиЗадачуToolStripMenuItem
            // 
            this.завантадитиЗадачуToolStripMenuItem.Name = "завантадитиЗадачуToolStripMenuItem";
            this.завантадитиЗадачуToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.завантадитиЗадачуToolStripMenuItem.Text = "Завантажити задачу";
            this.завантадитиЗадачуToolStripMenuItem.Click += new System.EventHandler(this.завантадитиЗадачуToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(189, 6);
            // 
            // ВиробникАToolStripMenuItem
            // 
            this.ВиробникАToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.додатиВиробникаAToolStripMenuItem,
            this.ВидалитивиробникаToolStripMenuItem});
            this.ВиробникАToolStripMenuItem.Enabled = false;
            this.ВиробникАToolStripMenuItem.Name = "ВиробникАToolStripMenuItem";
            this.ВиробникАToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.ВиробникАToolStripMenuItem.Text = "Виробник (A)";
            // 
            // додатиВиробникаAToolStripMenuItem
            // 
            this.додатиВиробникаAToolStripMenuItem.Name = "додатиВиробникаAToolStripMenuItem";
            this.додатиВиробникаAToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.додатиВиробникаAToolStripMenuItem.Text = "Додати виробника (A)";
            this.додатиВиробникаAToolStripMenuItem.Click += new System.EventHandler(this.додатиВиробникаAToolStripMenuItem_Click);
            // 
            // ВидалитивиробникаToolStripMenuItem
            // 
            this.ВидалитивиробникаToolStripMenuItem.Name = "ВидалитивиробникаToolStripMenuItem";
            this.ВидалитивиробникаToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.ВидалитивиробникаToolStripMenuItem.Text = "Видалити виробника (A)";
            this.ВидалитивиробникаToolStripMenuItem.Click += new System.EventHandler(this.ВидалитивиробникаToolStripMenuItem_Click);
            // 
            // СпоживачToolStripMenuItem
            // 
            this.СпоживачToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.додатиСпоживачаBToolStripMenuItem,
            this.видалитиСпоживачаBToolStripMenuItem});
            this.СпоживачToolStripMenuItem.Enabled = false;
            this.СпоживачToolStripMenuItem.Name = "СпоживачToolStripMenuItem";
            this.СпоживачToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.СпоживачToolStripMenuItem.Text = "Споживача (B)";
            // 
            // додатиСпоживачаBToolStripMenuItem
            // 
            this.додатиСпоживачаBToolStripMenuItem.Name = "додатиСпоживачаBToolStripMenuItem";
            this.додатиСпоживачаBToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.додатиСпоживачаBToolStripMenuItem.Text = "Додати споживача (B)";
            this.додатиСпоживачаBToolStripMenuItem.Click += new System.EventHandler(this.додатиСпоживачаBToolStripMenuItem_Click);
            // 
            // видалитиСпоживачаBToolStripMenuItem
            // 
            this.видалитиСпоживачаBToolStripMenuItem.Name = "видалитиСпоживачаBToolStripMenuItem";
            this.видалитиСпоживачаBToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.видалитиСпоживачаBToolStripMenuItem.Text = "Видалити споживача (B)";
            this.видалитиСпоживачаBToolStripMenuItem.Click += new System.EventHandler(this.видалитиСпоживачаBToolStripMenuItem_Click);
            // 
            // допомогаToolStripMenuItem
            // 
            this.допомогаToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.допомогаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.проПрограмуToolStripMenuItem});
            this.допомогаToolStripMenuItem.Name = "допомогаToolStripMenuItem";
            this.допомогаToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.допомогаToolStripMenuItem.Text = "Допомога";
            this.допомогаToolStripMenuItem.Visible = false;
            // 
            // проПрограмуToolStripMenuItem
            // 
            this.проПрограмуToolStripMenuItem.Name = "проПрограмуToolStripMenuItem";
            this.проПрограмуToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.проПрограмуToolStripMenuItem.Text = "Про програму";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Умова транспортної задачі  (*.trp)|*.prt";
            this.saveFileDialog1.InitialDirectory = "save\\\\";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Умова транспортної задачі  (*.trp)|*.prt";
            this.openFileDialog1.InitialDirectory = "\\save\\";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(584, 80);
            this.textBox1.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(584, 282);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabelOk,
            this.toolStripStatusLabelOnlyChek});
            this.statusStrip1.Location = new System.Drawing.Point(0, 386);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(127, 17);
            this.toolStripStatusLabelStatus.Text = "Створити нову задачу?";
            // 
            // toolStripStatusLabelOk
            // 
            this.toolStripStatusLabelOk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabelOk.IsLink = true;
            this.toolStripStatusLabelOk.Name = "toolStripStatusLabelOk";
            this.toolStripStatusLabelOk.Size = new System.Drawing.Size(20, 17);
            this.toolStripStatusLabelOk.Text = "Ok";
            this.toolStripStatusLabelOk.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripStatusLabelOk.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripStatusLabelOk.Click += new System.EventHandler(this.toolStripStatusLabelOk_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 306);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 80);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // toolStripStatusLabelOnlyChek
            // 
            this.toolStripStatusLabelOnlyChek.IsLink = true;
            this.toolStripStatusLabelOnlyChek.Name = "toolStripStatusLabelOnlyChek";
            this.toolStripStatusLabelOnlyChek.Size = new System.Drawing.Size(165, 17);
            this.toolStripStatusLabelOnlyChek.Text = "Режим лише перевірки крока: -";
            this.toolStripStatusLabelOnlyChek.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxInput.Location = new System.Drawing.Point(0, 24);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(584, 20);
            this.textBoxInput.TabIndex = 3;
            this.textBoxInput.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 408);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Тнанспортна задача";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private FormNewProblem formNewProblem;
        private FormDialogCalcOporn formDialogCalcOporn;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новаЗадачаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem зберегтиЗадачуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem завантадитиЗадачуToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelOk;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem вихідToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ВиробникАToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem СпоживачToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem додатиВиробникаAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem додатиСпоживачаBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ВидалитивиробникаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem видалитиСпоживачаBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem допомогаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проПрограмуToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem випадковаЗадачаToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelOnlyChek;
        private System.Windows.Forms.TextBox textBoxInput;
    }
}

