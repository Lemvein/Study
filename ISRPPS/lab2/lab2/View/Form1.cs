using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form1 : Form
    {
        Model model = new Model();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            model.op();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.label1.Text = model.mdl().ToString();
        }

        public static class Facade
        {
            static F2 Sub2 = new F2();
            static F3 Sub3 = new F3();
            static Form2 f2 = new Form2();

            public static void Run()
            {
                Sub2.Run_2();
            }

            public static void Run1()
            {
                Sub3.Run_3();
            }

            internal class F2
            {
                Form2 Sub2 = new Form2();
                public void Run_2()
                {
                    Sub2.ShowDialog();
                }
            }
            internal class F3
            {
                Form3 Sub3 = new Form3();
                public void Run_3()
                {
                    Sub3.ShowDialog();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Facade.Run();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Facade.Run1();
        }
    }
}