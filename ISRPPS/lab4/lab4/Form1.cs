using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hi 1");
            MessageBox.Show("hi","test 2");
            MessageBox.Show("hi", "test 3", MessageBoxButtons.OKCancel);
            MessageBox.Show("hi", "test 4", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            MessageBox.Show("hi", "test 5", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            switch (result)
            {
                case DialogResult.OK:
                    Application.Exit();
                    break;
                case DialogResult.Cancel:
                    MessageBox.Show("Cancel");
                    break;
                default: break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            this.BackColor = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            DialogResult result = MessageBox.Show("Maybe you want to change the color else?", "Color", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.Retry:
                    this.button3_Click(sender, e);
                    break;
                case DialogResult.Cancel:
                    MessageBox.Show("Well, OK");
                    break;
                default: break;
            }
        }
    }
}
