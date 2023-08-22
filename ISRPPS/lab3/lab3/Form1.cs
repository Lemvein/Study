using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    delegate void DelegateTool(MouseEventArgs e);
    public partial class Form1 : Form
    {
        Bitmap bitmap = null;
        Graphics gr = null;
        Pen pen = new Pen(Color.Black, 3.5f);
        Pen pBrush = new Pen(new SolidBrush(Color.Blue), 5.5f);

        private DelegateTool _operation;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            gr = Graphics.FromImage(bitmap);
            //gr = this.pictureBox1.CreateGraphics();
            this._operation = tBrush;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //Point p = e.Location;
            //PointF pf1 = p;     //new PointF(e,X, e.Y);   
            //MessageBox.Show("X = " + e.X + "Y = " + e.Y + "");
            this.textBox1.Text = "X = " + e.X + "   Y = " + e.Y + "";

            if (e.Button != MouseButtons.Left) return;
            //gr.DrawLine(pBrush, e.X, e.Y, e.X + 1, e.Y + 1);
            this._operation(e);
            this.pictureBox1.Image = this.bitmap;
            button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            this.gr.Clear(this.pictureBox1.BackColor);
            this.pictureBox1.Image = this.bitmap;
            button1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ToolBrush(); //_operation = tBrush;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ToolPen(); //_operation = tPen; 
        }
        private void button4_Click(object sender, EventArgs e)
        {
            _operation = DrawWeb;
        }

        public void ToolDraw(MouseEventArgs e) { _operation(e); }
        public void ToolBrush() { _operation = tBrush; }
        public void ToolPen() { _operation = tPen; }


        private void tBrush(MouseEventArgs e)
        {
            gr.DrawLine(pBrush, e.X, e.Y, e.X + 1, e.Y + 1);
        }
        private void tPen(MouseEventArgs e)
        {
            gr.DrawLine(pen, e.X, e.Y, e.X + 1, e.Y + 1);
        }

        private void DrawWeb(MouseEventArgs e)
        {
            PointF pointf;
            int step = 6;

            this.gr.Clear(this.pictureBox1.BackColor);
            this.pictureBox1.Image = this.bitmap;

            for (int i = 0; i < step; i++) 
            {
                pointf = new PointF(0, (i * (this.pictureBox1.Size.Height)/(step-1)));
                this.gr.DrawLine(pen, pointf, e.Location);

                pointf.X = this.pictureBox1.Size.Width;
                this.gr.DrawLine(pen, pointf, e.Location);

                pointf.X = (i * (this.pictureBox1.Size.Width)/(step-1));
                pointf.Y = 0;
                this.gr.DrawLine(pen, pointf, e.Location);

                pointf.Y = this.pictureBox1.Size.Height;
                this.gr.DrawLine(pen, pointf, e.Location);
            }
        }

    } // end class
}
