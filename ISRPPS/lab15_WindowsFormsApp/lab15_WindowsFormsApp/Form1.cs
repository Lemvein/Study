using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lab15_ClassLibrary;

namespace lab15_WindowsFormsApp
{
    public partial class Form1 : Form
    {
        Reestr reestr = new Reestr();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = reestr.F + " " + reestr.I + " " + reestr.year;
        }
    }
}
