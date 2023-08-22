using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6  
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.richTextBox1.Text == "") { this.richTextBox1.Text = 
                    "High above the city, on a tall column, stood the statue of the Happy Prince. He was gilded all over with thin leaves of fine gold, for eyes he had two bright sapphires, and a large red ruby glowed on his sword-hilt"; }
            else
            {
                Random random = new Random();
                if (this.button1.BackColor != Color.Gold) { this.richTextBox1.SelectionColor = this.button1.BackColor; }
                Color color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
                this.button1.BackColor = color;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
                //MessageBox.Show("checkBox1.Checked");
                this.button1.Visible = true;
            else
                //MessageBox.Show("un checkBox1.Checked");
                this.button1.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.richTextBox1.Font = new Font(this.richTextBox1.Font.FontFamily, trackBar1.Value);
            this.label1.Text = this.trackBar1.Value.ToString();
        }
    }
}
