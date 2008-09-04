namespace Calendar
{
  partial class frmCalendar
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.mtbDateDay = new System.Windows.Forms.MaskedTextBox();
      this.mtbDateMinute = new System.Windows.Forms.MaskedTextBox();
      this.mtbDateHour = new System.Windows.Forms.MaskedTextBox();
      this.mtbDateMonth = new System.Windows.Forms.MaskedTextBox();
      this.mtbDateYear = new System.Windows.Forms.MaskedTextBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label10 = new System.Windows.Forms.Label();
      this.txtMaskDaysInWeek = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.txtMaskMonths = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.txtMaskDaysInMonth = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.txtMaskHours = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txtMaskMinutes = new System.Windows.Forms.TextBox();
      this.btnGetNextDate = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.mtbDateDay);
      this.groupBox1.Controls.Add(this.mtbDateMinute);
      this.groupBox1.Controls.Add(this.mtbDateHour);
      this.groupBox1.Controls.Add(this.mtbDateMonth);
      this.groupBox1.Controls.Add(this.mtbDateYear);
      this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(291, 233);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Введіть дату";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label5.Location = new System.Drawing.Point(21, 189);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(64, 16);
      this.label5.TabIndex = 5;
      this.label5.Text = "Хвилина";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label4.Location = new System.Drawing.Point(21, 146);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(55, 16);
      this.label4.TabIndex = 5;
      this.label4.Text = "Година";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label3.Location = new System.Drawing.Point(21, 105);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(40, 16);
      this.label3.TabIndex = 5;
      this.label3.Text = "День";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label2.Location = new System.Drawing.Point(21, 66);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(51, 16);
      this.label2.TabIndex = 5;
      this.label2.Text = "Місяць";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label1.Location = new System.Drawing.Point(21, 28);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(27, 16);
      this.label1.TabIndex = 5;
      this.label1.Text = "Рік";
      // 
      // mtbDateDay
      // 
      this.mtbDateDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.mtbDateDay.Location = new System.Drawing.Point(102, 102);
      this.mtbDateDay.Mask = "00";
      this.mtbDateDay.Name = "mtbDateDay";
      this.mtbDateDay.PromptChar = ' ';
      this.mtbDateDay.Size = new System.Drawing.Size(100, 22);
      this.mtbDateDay.TabIndex = 2;
      // 
      // mtbDateMinute
      // 
      this.mtbDateMinute.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.mtbDateMinute.Location = new System.Drawing.Point(102, 186);
      this.mtbDateMinute.Mask = "00";
      this.mtbDateMinute.Name = "mtbDateMinute";
      this.mtbDateMinute.PromptChar = ' ';
      this.mtbDateMinute.Size = new System.Drawing.Size(100, 22);
      this.mtbDateMinute.TabIndex = 4;
      // 
      // mtbDateHour
      // 
      this.mtbDateHour.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.mtbDateHour.Location = new System.Drawing.Point(102, 143);
      this.mtbDateHour.Mask = "00";
      this.mtbDateHour.Name = "mtbDateHour";
      this.mtbDateHour.PromptChar = ' ';
      this.mtbDateHour.Size = new System.Drawing.Size(100, 22);
      this.mtbDateHour.TabIndex = 3;
      // 
      // mtbDateMonth
      // 
      this.mtbDateMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.mtbDateMonth.Location = new System.Drawing.Point(102, 63);
      this.mtbDateMonth.Mask = "00";
      this.mtbDateMonth.Name = "mtbDateMonth";
      this.mtbDateMonth.PromptChar = ' ';
      this.mtbDateMonth.Size = new System.Drawing.Size(100, 22);
      this.mtbDateMonth.TabIndex = 1;
      // 
      // mtbDateYear
      // 
      this.mtbDateYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.mtbDateYear.Location = new System.Drawing.Point(102, 25);
      this.mtbDateYear.Mask = "0000";
      this.mtbDateYear.Name = "mtbDateYear";
      this.mtbDateYear.PromptChar = ' ';
      this.mtbDateYear.Size = new System.Drawing.Size(100, 22);
      this.mtbDateYear.TabIndex = 0;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label10);
      this.groupBox2.Controls.Add(this.txtMaskDaysInWeek);
      this.groupBox2.Controls.Add(this.label9);
      this.groupBox2.Controls.Add(this.txtMaskMonths);
      this.groupBox2.Controls.Add(this.label8);
      this.groupBox2.Controls.Add(this.txtMaskDaysInMonth);
      this.groupBox2.Controls.Add(this.label7);
      this.groupBox2.Controls.Add(this.txtMaskHours);
      this.groupBox2.Controls.Add(this.label6);
      this.groupBox2.Controls.Add(this.txtMaskMinutes);
      this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.groupBox2.Location = new System.Drawing.Point(12, 263);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(291, 250);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Введіть маску";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label10.Location = new System.Drawing.Point(21, 199);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(78, 16);
      this.label10.TabIndex = 1;
      this.label10.Text = "Днів тижня";
      // 
      // txtMaskDaysInWeek
      // 
      this.txtMaskDaysInWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.txtMaskDaysInWeek.Location = new System.Drawing.Point(105, 199);
      this.txtMaskDaysInWeek.Name = "txtMaskDaysInWeek";
      this.txtMaskDaysInWeek.Size = new System.Drawing.Size(165, 22);
      this.txtMaskDaysInWeek.TabIndex = 4;
      this.txtMaskDaysInWeek.Text = "*";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label9.Location = new System.Drawing.Point(21, 155);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(55, 16);
      this.label9.TabIndex = 1;
      this.label9.Text = "Місяців";
      // 
      // txtMaskMonths
      // 
      this.txtMaskMonths.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.txtMaskMonths.Location = new System.Drawing.Point(92, 155);
      this.txtMaskMonths.Name = "txtMaskMonths";
      this.txtMaskMonths.Size = new System.Drawing.Size(178, 22);
      this.txtMaskMonths.TabIndex = 3;
      this.txtMaskMonths.Text = "*";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label8.Location = new System.Drawing.Point(21, 113);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(87, 16);
      this.label8.TabIndex = 1;
      this.label8.Text = "Днів у місяці";
      // 
      // txtMaskDaysInMonth
      // 
      this.txtMaskDaysInMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.txtMaskDaysInMonth.Location = new System.Drawing.Point(114, 113);
      this.txtMaskDaysInMonth.Name = "txtMaskDaysInMonth";
      this.txtMaskDaysInMonth.Size = new System.Drawing.Size(156, 22);
      this.txtMaskDaysInMonth.TabIndex = 2;
      this.txtMaskDaysInMonth.Text = "*";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label7.Location = new System.Drawing.Point(21, 73);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(55, 16);
      this.label7.TabIndex = 1;
      this.label7.Text = "Години";
      // 
      // txtMaskHours
      // 
      this.txtMaskHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.txtMaskHours.Location = new System.Drawing.Point(92, 73);
      this.txtMaskHours.Name = "txtMaskHours";
      this.txtMaskHours.Size = new System.Drawing.Size(178, 22);
      this.txtMaskHours.TabIndex = 1;
      this.txtMaskHours.Text = "*";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label6.Location = new System.Drawing.Point(20, 32);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(64, 16);
      this.label6.TabIndex = 1;
      this.label6.Text = "Хвилини";
      // 
      // txtMaskMinutes
      // 
      this.txtMaskMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.txtMaskMinutes.Location = new System.Drawing.Point(91, 32);
      this.txtMaskMinutes.Name = "txtMaskMinutes";
      this.txtMaskMinutes.Size = new System.Drawing.Size(178, 22);
      this.txtMaskMinutes.TabIndex = 0;
      this.txtMaskMinutes.Text = "10-15,24,35";
      // 
      // btnGetNextDate
      // 
      this.btnGetNextDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnGetNextDate.Location = new System.Drawing.Point(15, 522);
      this.btnGetNextDate.Name = "btnGetNextDate";
      this.btnGetNextDate.Size = new System.Drawing.Size(287, 41);
      this.btnGetNextDate.TabIndex = 2;
      this.btnGetNextDate.Text = "Get Next Date!";
      this.btnGetNextDate.UseVisualStyleBackColor = true;
      this.btnGetNextDate.Click += new System.EventHandler(this.btnGetNextDate_Click);
      // 
      // frmCalendar
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(317, 574);
      this.Controls.Add(this.btnGetNextDate);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Name = "frmCalendar";
      this.Text = "Калєндар";
      this.Shown += new System.EventHandler(this.frmCalendar_Shown);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.MaskedTextBox mtbDateDay;
    private System.Windows.Forms.MaskedTextBox mtbDateMinute;
    private System.Windows.Forms.MaskedTextBox mtbDateHour;
    private System.Windows.Forms.MaskedTextBox mtbDateMonth;
    private System.Windows.Forms.MaskedTextBox mtbDateYear;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtMaskMinutes;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox txtMaskDaysInWeek;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtMaskMonths;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtMaskDaysInMonth;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtMaskHours;
    private System.Windows.Forms.Button btnGetNextDate;

  }
}

