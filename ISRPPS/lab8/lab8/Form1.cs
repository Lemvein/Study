using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab8
{
    public partial class Form1 : Form
    {
        User user = new User();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(" " + Convert.ToInt32(this.textBox1.Text));
            user.Compute('+', Convert.ToInt32(this.textBox1.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = user.getResult().ToString();
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            user.Undo(1);
            this.textBox1.Text = user.getResult().ToString();
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            user.Redo(1);
            this.textBox1.Text = user.getResult().ToString();
        }

        private void button4_Click(object sender, EventArgs e) //-
        {
            user.Compute('-', Convert.ToInt32(this.textBox1.Text));
        }

        private void button3_Click(object sender, EventArgs e) //*
        {
            user.Compute('*', Convert.ToInt32(this.textBox1.Text));
        }

        private void button5_Click(object sender, EventArgs e) ///
        {
            user.Compute('/', Convert.ToInt32(this.textBox1.Text));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }
    }
}
