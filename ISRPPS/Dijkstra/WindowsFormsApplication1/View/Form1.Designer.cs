namespace WindowsFormsApplication1
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
            this.finishCreatingNodes = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dataView = new System.Windows.Forms.DataGridView();
            this.debugLog = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.richTextBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodesRedactorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Undo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // finishCreatingNodes
            // 
            this.finishCreatingNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.finishCreatingNodes.Location = new System.Drawing.Point(27, 12);
            this.finishCreatingNodes.Name = "finishCreatingNodes";
            this.finishCreatingNodes.Size = new System.Drawing.Size(176, 37);
            this.finishCreatingNodes.TabIndex = 0;
            this.finishCreatingNodes.Text = "Finish Creating Nodes";
            this.finishCreatingNodes.UseVisualStyleBackColor = true;
            this.finishCreatingNodes.Click += new System.EventHandler(this.finishCreatingNodes_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(309, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 37);
            this.button2.TabIndex = 1;
            this.button2.Text = "Set START Point";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SetStartPoint);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(468, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 37);
            this.button3.TabIndex = 2;
            this.button3.Text = "Set END Point";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.SetEndPoint);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.Location = new System.Drawing.Point(27, 55);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(556, 37);
            this.button4.TabIndex = 3;
            this.button4.Text = "START DIJKSTRA";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.startDijkstra);
            // 
            // dataView
            // 
            this.dataView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataView.Location = new System.Drawing.Point(611, 27);
            this.dataView.Name = "dataView";
            this.dataView.RowTemplate.Height = 24;
            this.dataView.Size = new System.Drawing.Size(638, 439);
            this.dataView.TabIndex = 4;
            this.dataView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataView_CellValueChanged);
            // 
            // debugLog
            // 
            this.debugLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debugLog.Location = new System.Drawing.Point(611, 472);
            this.debugLog.Name = "debugLog";
            this.debugLog.Size = new System.Drawing.Size(638, 191);
            this.debugLog.TabIndex = 5;
            this.debugLog.Text = "";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.LightCoral;
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.finishCreatingNodes);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Location = new System.Drawing.Point(1, 568);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(604, 95);
            this.panel1.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.richTextBoxToolStripMenuItem,
            this.nodesRedactorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1249, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // richTextBoxToolStripMenuItem
            // 
            this.richTextBoxToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.savePictureToolStripMenuItem});
            this.richTextBoxToolStripMenuItem.Name = "richTextBoxToolStripMenuItem";
            this.richTextBoxToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.richTextBoxToolStripMenuItem.Text = "File";
            // 
            // savePictureToolStripMenuItem
            // 
            this.savePictureToolStripMenuItem.Name = "savePictureToolStripMenuItem";
            this.savePictureToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.savePictureToolStripMenuItem.Text = "Save picture";
            this.savePictureToolStripMenuItem.Click += new System.EventHandler(this.savePictureToolStripMenuItem_Click);
            // 
            // nodesRedactorToolStripMenuItem
            // 
            this.nodesRedactorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorToolStripMenuItem});
            this.nodesRedactorToolStripMenuItem.Name = "nodesRedactorToolStripMenuItem";
            this.nodesRedactorToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.nodesRedactorToolStripMenuItem.Text = "Nodes redactor";
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.colorToolStripMenuItem.Text = "Color";
            this.colorToolStripMenuItem.Click += new System.EventHandler(this.colorToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Undo
            // 
            this.Undo.Location = new System.Drawing.Point(145, 0);
            this.Undo.Name = "Undo";
            this.Undo.Size = new System.Drawing.Size(25, 24);
            this.Undo.TabIndex = 8;
            this.Undo.Text = "Undo";
            this.Undo.UseVisualStyleBackColor = true;
            this.Undo.Click += new System.EventHandler(this.Undo_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1249, 666);
            this.Controls.Add(this.Undo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.debugLog);
            this.Controls.Add(this.dataView);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Dijkstra\'s algorithm";
            this.Click += new System.EventHandler(this.onClickOnForm);
            this.DoubleClick += new System.EventHandler(this.onDoubleClickOnForm);
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button finishCreatingNodes;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dataView;
        private System.Windows.Forms.RichTextBox debugLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem richTextBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePictureToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem nodesRedactorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.Button Undo;
    }
}

