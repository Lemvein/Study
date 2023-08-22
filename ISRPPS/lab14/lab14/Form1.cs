using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab14
{
    public partial class Form1 : Form
    {
        float angle = 30;
        public Form1()
        {
            InitializeComponent();
            this.timer1.Stop();
            //this.timer1.Interval = 5;
            //this.timer1.Enabled = true;
            //this.timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(this.pictureBox1.Width/2, this.pictureBox1.Height/2);
            e.Graphics.RotateTransform(angle);
            e.Graphics.DrawRectangle(new Pen(Color.Pink, 50), 30, 30, 60, 60);
            e.Graphics.DrawLine(new Pen(Color.BlueViolet, 50), 120, 120, 80, 80);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            angle += 0.5f;
            this.pictureBox1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();

        }
    }
}
