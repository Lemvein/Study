namespace MKO
{
    partial class Menu
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            buttonLab = new Button();
            buttonCalc = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            buttonM = new Button();
            label2 = new Label();
            buttonT = new Button();
            buttonP = new Button();
            label1 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // buttonLab
            // 
            buttonLab.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            buttonLab.Location = new Point(53, 31);
            buttonLab.Name = "buttonLab";
            buttonLab.Size = new Size(200, 30);
            buttonLab.TabIndex = 0;
            buttonLab.Text = "Лабораторная работа";
            buttonLab.UseVisualStyleBackColor = true;
            buttonLab.Click += buttonLab_Click;
            // 
            // buttonCalc
            // 
            buttonCalc.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCalc.Location = new Point(53, 118);
            buttonCalc.Name = "buttonCalc";
            buttonCalc.Size = new Size(200, 30);
            buttonCalc.TabIndex = 2;
            buttonCalc.Text = "Решение задачи";
            buttonCalc.UseVisualStyleBackColor = true;
            buttonCalc.Click += buttonCalc_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonLab);
            panel1.Controls.Add(buttonCalc);
            panel1.Location = new Point(107, 82);
            panel1.Name = "panel1";
            panel1.Size = new Size(307, 196);
            panel1.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.Controls.Add(dataGridView1);
            panel2.Controls.Add(buttonM);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(buttonT);
            panel2.Controls.Add(buttonP);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(12, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(497, 333);
            panel2.TabIndex = 4;
            panel2.Visible = false;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Location = new Point(62, 171);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(365, 82);
            dataGridView1.TabIndex = 7;
            // 
            // Column1
            // 
            Column1.HeaderText = "Вид работы";
            Column1.Name = "Column1";
            Column1.Width = 220;
            // 
            // Column2
            // 
            Column2.HeaderText = "Оценка";
            Column2.Name = "Column2";
            // 
            // buttonM
            // 
            buttonM.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            buttonM.Location = new Point(139, 282);
            buttonM.Name = "buttonM";
            buttonM.Size = new Size(214, 34);
            buttonM.TabIndex = 6;
            buttonM.Text = "Вернуться в главное меню";
            buttonM.UseVisualStyleBackColor = true;
            buttonM.Click += buttonM_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(128, 130);
            label2.Name = "label2";
            label2.Size = new Size(232, 21);
            label2.TabIndex = 5;
            label2.Text = "Результат выполнения работы:";
            // 
            // buttonT
            // 
            buttonT.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            buttonT.Location = new Point(271, 75);
            buttonT.Name = "buttonT";
            buttonT.Size = new Size(172, 34);
            buttonT.TabIndex = 4;
            buttonT.Text = "Тестовая часть";
            buttonT.UseVisualStyleBackColor = true;
            buttonT.Click += buttonT_Click;
            // 
            // buttonP
            // 
            buttonP.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            buttonP.Location = new Point(48, 75);
            buttonP.Name = "buttonP";
            buttonP.Size = new Size(172, 34);
            buttonP.TabIndex = 2;
            buttonP.Text = "Практическая часть";
            buttonP.UseVisualStyleBackColor = true;
            buttonP.Click += buttonP_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(492, 50);
            label1.TabIndex = 0;
            label1.Text = "Лабораторная работа\r\nРешение задач многокритериальной оптимизации\r\n";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.GhostWhite;
            ClientSize = new Size(521, 356);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Menu";
            Text = "Меню";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonLab;
        private Button buttonTable;
        private Button buttonCalc;
        private Panel panel1;
        private Panel panel2;
        private TextBox textBox2;
        private TextBox textBox1;
        private Button buttonP;
        private Label label1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private Button buttonM;
        private Button buttonT;
        private Label label2;
    }
}