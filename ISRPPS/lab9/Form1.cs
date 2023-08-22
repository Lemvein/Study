using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISRPPS_Lab_9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.toolStripLabel1.Text = "X: " + e.X.ToString() + "  Y: " + e.Y.ToString();
            this.toolStripStatusLabel1.Text = "Status:  ";
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int s = toolStripComboBox1.SelectedIndex;
            
            switch (s)
            {
                case 0: { if (s == 0) MessageBox.Show("Choosed: Name_Line_1"); } break;
                case 1: { if (s == 1) MessageBox.Show("Choosed: Name_Line_2"); } break;
                case 2: { if (s == 2) MessageBox.Show("Choosed: Name_Line_3"); } break;
                case 3: { if (s == 3) MessageBox.Show("Choosed: Name_Line_4"); } break;
                case 4: { if (s == 4) MessageBox.Show("Choosed: Name_Line_5"); } break;

            }
            

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Operation Undo");

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Operation Redo");
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opened");
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Exit");
            Application.Exit();  
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Program launched");
            this.toolStripProgressBar1.Value = 0;
            do this.toolStripProgressBar1.Value += 50;
            while (this.toolStripProgressBar1.Value == 100);
            MessageBox.Show("Error.Try again!");
            
            this.toolStripStatusLabel1.Text += "Error";

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("toolStripLabel1_Click");
            this.toolStripComboBox1.Text = "Label1 Selected";
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("Launch");
            //this.toolStripProgressBar1.Value = 0;
            while (this.toolStripProgressBar1.Value < 100)
            { this.toolStripProgressBar1.Value += 1; }
            if (this.toolStripProgressBar1.Value == 100)
            {
                MessageBox.Show("Complete");
                this.toolStripStatusLabel1.Text += "Complete";
            }
            this.toolStripProgressBar1.Value = 0;

        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {
            while (this.toolStripProgressBar1.Value < 100)
            { this.toolStripProgressBar1.Value += 50; }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if((Control.ModifierKeys==Keys.Control) && (e.KeyCode==Keys.Space))
            {
                Application.Exit();
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (e.X > 200) { this.contextMenuStrip2.Show(PointToScreen(e.Location)); }
            else this.contextMenuStrip1.Show(PointToScreen(e.Location));
        }
    }
}
