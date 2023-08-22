namespace MKO
{
    partial class Calculation
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calculation));
            groupBox1 = new GroupBox();
            buttonGenerate = new Button();
            pictureBox1 = new PictureBox();
            textBoxCountVar = new TextBox();
            textBoxCountConstraint = new TextBox();
            textBoxCountCriteria = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            buttonCreateEmptyProblem = new Button();
            buttonOpenFromFile = new Button();
            buttonComputeF = new Button();
            panel1 = new Panel();
            toolTip1 = new ToolTip(components);
            groupBox2 = new GroupBox();
            label4 = new Label();
            textBoxStep = new TextBox();
            textBoxAlpha = new TextBox();
            textBoxEps = new TextBox();
            label7 = new Label();
            label5 = new Label();
            buttonComputePenalty = new Button();
            labelOptF = new Label();
            labelProblem = new Label();
            textBoxProb = new TextBox();
            groupBox3 = new GroupBox();
            textBoxEpsGrad = new TextBox();
            label9 = new Label();
            buttonSaveProb = new Button();
            button1 = new Button();
            buttonM = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.FloralWhite;
            groupBox1.Controls.Add(buttonGenerate);
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Controls.Add(textBoxCountVar);
            groupBox1.Controls.Add(textBoxCountConstraint);
            groupBox1.Controls.Add(textBoxCountCriteria);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(buttonCreateEmptyProblem);
            groupBox1.Controls.Add(buttonOpenFromFile);
            groupBox1.Location = new Point(14, 28);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(322, 212);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Постановка задачи";
            // 
            // buttonGenerate
            // 
            buttonGenerate.Location = new Point(170, 172);
            buttonGenerate.Margin = new Padding(4, 3, 4, 3);
            buttonGenerate.Name = "buttonGenerate";
            buttonGenerate.Size = new Size(113, 27);
            buttonGenerate.TabIndex = 9;
            buttonGenerate.Text = "Сгенерировать";
            buttonGenerate.UseVisualStyleBackColor = true;
            buttonGenerate.Click += buttonGenerate_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.rsz_help;
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(170, 35);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(27, 27);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            toolTip1.SetToolTip(pictureBox1, resources.GetString("pictureBox1.ToolTip"));
            // 
            // textBoxCountVar
            // 
            textBoxCountVar.Location = new Point(155, 142);
            textBoxCountVar.Margin = new Padding(4, 3, 4, 3);
            textBoxCountVar.Name = "textBoxCountVar";
            textBoxCountVar.Size = new Size(48, 23);
            textBoxCountVar.TabIndex = 7;
            // 
            // textBoxCountConstraint
            // 
            textBoxCountConstraint.Location = new Point(155, 112);
            textBoxCountConstraint.Margin = new Padding(4, 3, 4, 3);
            textBoxCountConstraint.Name = "textBoxCountConstraint";
            textBoxCountConstraint.Size = new Size(48, 23);
            textBoxCountConstraint.TabIndex = 6;
            // 
            // textBoxCountCriteria
            // 
            textBoxCountCriteria.Location = new Point(155, 82);
            textBoxCountCriteria.Margin = new Padding(4, 3, 4, 3);
            textBoxCountCriteria.Name = "textBoxCountCriteria";
            textBoxCountCriteria.Size = new Size(48, 23);
            textBoxCountCriteria.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 83);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(102, 15);
            label3.TabIndex = 4;
            label3.Text = "Число критериев";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 112);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(118, 15);
            label2.TabIndex = 3;
            label2.Text = "Число ограничений";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 141);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(115, 15);
            label1.TabIndex = 2;
            label1.Text = "Число переменных";
            // 
            // buttonCreateEmptyProblem
            // 
            buttonCreateEmptyProblem.Location = new Point(16, 172);
            buttonCreateEmptyProblem.Margin = new Padding(4, 3, 4, 3);
            buttonCreateEmptyProblem.Name = "buttonCreateEmptyProblem";
            buttonCreateEmptyProblem.Size = new Size(146, 27);
            buttonCreateEmptyProblem.TabIndex = 1;
            buttonCreateEmptyProblem.Text = "Заполнить вручную";
            buttonCreateEmptyProblem.UseVisualStyleBackColor = true;
            buttonCreateEmptyProblem.Click += buttonCreateEmptyProblem_Click;
            // 
            // buttonOpenFromFile
            // 
            buttonOpenFromFile.Location = new Point(16, 35);
            buttonOpenFromFile.Margin = new Padding(4, 3, 4, 3);
            buttonOpenFromFile.Name = "buttonOpenFromFile";
            buttonOpenFromFile.Size = new Size(146, 27);
            buttonOpenFromFile.TabIndex = 0;
            buttonOpenFromFile.Text = "Открыть из файла";
            buttonOpenFromFile.UseVisualStyleBackColor = true;
            buttonOpenFromFile.Click += buttonOpenFromFile_Click;
            // 
            // buttonComputeF
            // 
            buttonComputeF.Location = new Point(371, 503);
            buttonComputeF.Margin = new Padding(4, 3, 4, 3);
            buttonComputeF.Name = "buttonComputeF";
            buttonComputeF.Size = new Size(146, 27);
            buttonComputeF.TabIndex = 2;
            buttonComputeF.Text = "Рассчитать вектор f*";
            buttonComputeF.UseVisualStyleBackColor = true;
            buttonComputeF.Visible = false;
            buttonComputeF.Click += buttonCompute_Click;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.BackColor = Color.WhiteSmoke;
            panel1.Location = new Point(371, 37);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(640, 459);
            panel1.TabIndex = 3;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.FloralWhite;
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(textBoxStep);
            groupBox2.Controls.Add(textBoxAlpha);
            groupBox2.Controls.Add(textBoxEps);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label5);
            groupBox2.Location = new Point(14, 260);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(322, 134);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Метод штрафных функций";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(128, 96);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(29, 15);
            label4.TabIndex = 11;
            label4.Text = "Шаг";
            // 
            // textBoxStep
            // 
            textBoxStep.Location = new Point(160, 96);
            textBoxStep.Margin = new Padding(4, 3, 4, 3);
            textBoxStep.Name = "textBoxStep";
            textBoxStep.Size = new Size(95, 23);
            textBoxStep.TabIndex = 10;
            // 
            // textBoxAlpha
            // 
            textBoxAlpha.Location = new Point(160, 69);
            textBoxAlpha.Margin = new Padding(4, 3, 4, 3);
            textBoxAlpha.Name = "textBoxAlpha";
            textBoxAlpha.Size = new Size(95, 23);
            textBoxAlpha.TabIndex = 8;
            // 
            // textBoxEps
            // 
            textBoxEps.Location = new Point(160, 39);
            textBoxEps.Margin = new Padding(4, 3, 4, 3);
            textBoxEps.Name = "textBoxEps";
            textBoxEps.Size = new Size(95, 23);
            textBoxEps.TabIndex = 6;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(7, 69);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(145, 15);
            label7.TabIndex = 4;
            label7.Text = "Штрафной коэффициент";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(62, 39);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(92, 15);
            label5.TabIndex = 2;
            label5.Text = "Погрешность e";
            // 
            // buttonComputePenalty
            // 
            buttonComputePenalty.Location = new Point(14, 497);
            buttonComputePenalty.Margin = new Padding(4, 3, 4, 3);
            buttonComputePenalty.Name = "buttonComputePenalty";
            buttonComputePenalty.Size = new Size(88, 27);
            buttonComputePenalty.TabIndex = 9;
            buttonComputePenalty.Text = "Рассчитать";
            buttonComputePenalty.UseVisualStyleBackColor = true;
            buttonComputePenalty.Visible = false;
            buttonComputePenalty.Click += buttonComputePenalty_Click;
            // 
            // labelOptF
            // 
            labelOptF.AutoSize = true;
            labelOptF.Location = new Point(525, 504);
            labelOptF.Margin = new Padding(4, 0, 4, 0);
            labelOptF.Name = "labelOptF";
            labelOptF.Size = new Size(38, 15);
            labelOptF.TabIndex = 5;
            labelOptF.Text = "label5";
            labelOptF.Visible = false;
            // 
            // labelProblem
            // 
            labelProblem.AutoSize = true;
            labelProblem.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            labelProblem.Location = new Point(368, 537);
            labelProblem.Margin = new Padding(4, 0, 4, 0);
            labelProblem.Name = "labelProblem";
            labelProblem.Size = new Size(81, 15);
            labelProblem.TabIndex = 6;
            labelProblem.Text = "labelProblem";
            labelProblem.Visible = false;
            // 
            // textBoxProb
            // 
            textBoxProb.Location = new Point(371, 558);
            textBoxProb.Margin = new Padding(4, 3, 4, 3);
            textBoxProb.Name = "textBoxProb";
            textBoxProb.Size = new Size(640, 23);
            textBoxProb.TabIndex = 7;
            textBoxProb.Visible = false;
            // 
            // groupBox3
            // 
            groupBox3.BackColor = Color.FloralWhite;
            groupBox3.Controls.Add(textBoxEpsGrad);
            groupBox3.Controls.Add(label9);
            groupBox3.Location = new Point(14, 396);
            groupBox3.Margin = new Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 3, 4, 3);
            groupBox3.Size = new Size(322, 83);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "Метод наискорейшего спуска";
            // 
            // textBoxEpsGrad
            // 
            textBoxEpsGrad.Location = new Point(160, 39);
            textBoxEpsGrad.Margin = new Padding(4, 3, 4, 3);
            textBoxEpsGrad.Name = "textBoxEpsGrad";
            textBoxEpsGrad.Size = new Size(95, 23);
            textBoxEpsGrad.TabIndex = 6;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(55, 39);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(92, 15);
            label9.TabIndex = 2;
            label9.Text = "Погрешность e";
            // 
            // buttonSaveProb
            // 
            buttonSaveProb.Location = new Point(876, 504);
            buttonSaveProb.Margin = new Padding(4, 3, 4, 3);
            buttonSaveProb.Name = "buttonSaveProb";
            buttonSaveProb.Size = new Size(134, 27);
            buttonSaveProb.TabIndex = 11;
            buttonSaveProb.Text = "Сохранить задачу";
            buttonSaveProb.UseVisualStyleBackColor = true;
            buttonSaveProb.Visible = false;
            buttonSaveProb.Click += buttonSaveProb_Click;
            // 
            // button1
            // 
            button1.Location = new Point(805, 504);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(88, 27);
            button1.TabIndex = 12;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // buttonM
            // 
            buttonM.Location = new Point(14, 554);
            buttonM.Margin = new Padding(4, 3, 4, 3);
            buttonM.Name = "buttonM";
            buttonM.Size = new Size(162, 27);
            buttonM.TabIndex = 13;
            buttonM.Text = "Вернуться в главное меню";
            buttonM.UseVisualStyleBackColor = true;
            buttonM.Click += buttonM_Click;
            // 
            // Calculation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.GhostWhite;
            ClientSize = new Size(1072, 598);
            Controls.Add(buttonM);
            Controls.Add(button1);
            Controls.Add(buttonSaveProb);
            Controls.Add(groupBox3);
            Controls.Add(textBoxProb);
            Controls.Add(labelProblem);
            Controls.Add(buttonComputePenalty);
            Controls.Add(labelOptF);
            Controls.Add(groupBox2);
            Controls.Add(panel1);
            Controls.Add(buttonComputeF);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "Calculation";
            Text = "Решение задач многокритериальной оптимизации";
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxCountVar;
        private System.Windows.Forms.TextBox textBoxCountConstraint;
        private System.Windows.Forms.TextBox textBoxCountCriteria;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCreateEmptyProblem;
        private System.Windows.Forms.Button buttonOpenFromFile;
        private System.Windows.Forms.Button buttonComputeF;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelOptF;
        private System.Windows.Forms.TextBox textBoxAlpha;
        private System.Windows.Forms.TextBox textBoxEps;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonComputePenalty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxStep;
        private System.Windows.Forms.Label labelProblem;
        private System.Windows.Forms.TextBox textBoxProb;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxEpsGrad;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Button buttonSaveProb;
        private System.Windows.Forms.Button button1;
        private Button buttonM;
    }
}

