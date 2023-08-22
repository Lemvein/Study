using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab13
{
    public partial class Form1 : Form
    {
        Graphics graph;
        int cx;
        int cy;
        public Form1()
        {
            InitializeComponent();
        }

        Pen penRed5 = new Pen(Color.Red, 5);
        Pen penPink2 = new Pen(Color.Pink, 2);
        private void button1_Click(object sender, EventArgs e)
        {
            // берем дискриптор окна
            graph = Graphics.FromHwnd(this.pictureBox1.Handle);
            cx = pictureBox1.Size.Width;
            cy = pictureBox1.Size.Height - 60;
            graph.DrawLine(penRed5, 0, 0, cx, cy);
            graph.DrawLine(penRed5, cx, 0, 0, cy);
            graph.DrawLine(penRed5, 0, cy, cx, pictureBox1.Size.Height);
            graph.DrawLine(penRed5, cx, cy, 0, pictureBox1.Size.Height);
            graph.DrawLine(penPink2, 0, 0, cx, cy);
            graph.DrawLine(penPink2, cx, 0, 0, cy);
            graph.DrawLine(penPink2, 0, cy, cx, pictureBox1.Size.Height);
            graph.DrawLine(penPink2, cx, cy, 0, pictureBox1.Size.Height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graph = Graphics.FromHwnd(this.pictureBox2.Handle);
            cx = pictureBox2.Size.Width;
            cy = pictureBox2.Size.Height;
            PointF[] aptf = new PointF[cx];
            //grfx.Clear(Color.SkyBlue);
            for (int i = 0; i < cx; i++)
            {
                aptf[i].X = i;
                aptf[i].Y = cy / 2 * (1 - (float)Math.Sin(i * 2 * Math.PI / (cx - 1))); // синусоида разбита на 2 части, i - шаг
            }
            graph.DrawLines(new Pen(Color.Orange, 5), aptf);
            graph.DrawLines(new Pen(Color.LightYellow, 2), aptf);
        }

        SolidBrush solidBrushY = new SolidBrush(Color.Yellow);

        private void button3_Click(object sender, EventArgs e)
        {
            graph = Graphics.FromHwnd(this.pictureBox3.Handle);
            cx = pictureBox3.Size.Width;
            cy = pictureBox3.Size.Height;
            //grfx.Clear(Color.Silver);
            graph.FillEllipse(solidBrushY, 0, 0, cx, cy);
            graph.FillEllipse(new SolidBrush(Color.White), 0 + 5, 0 + 5, cx - 10, cy - 10);
            graph.FillEllipse(solidBrushY, 0 + 10, 0 + 10, cx - 20, cy - 20);
            graph.FillEllipse(new SolidBrush(Color.Black), 0 + 20, 0 + 20, cx - 40, cy - 40);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            graph = Graphics.FromHwnd(this.pictureBox4.Handle);
            cx = pictureBox4.Size.Width;
            cy = pictureBox4.Size.Height;
            //grfx.Clear(Color.Silver);
            graph.DrawEllipse(new Pen(Color.Green, 7), 0, 0, cx, cy);
            graph.DrawEllipse(new Pen(Color.LightSeaGreen, 4), 0, 0, cx, cy);
            graph.DrawEllipse(new Pen(Color.White, 1), 0, 0, cx, cy);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            graph = Graphics.FromHwnd(this.pictureBox5.Handle);
            cx = pictureBox5.Size.Width;
            cy = pictureBox5.Size.Height;
            //grfx.Clear(Color.Silver);

            graph.DrawBezier(new Pen(Color.SkyBlue, 8),
                            new PointF(0, 0),
                            new PointF(cx + 40, cy + 40),
                            new PointF(cx + 30, 0 - 100),
                            new PointF(0, cy));
            graph.DrawBezier(new Pen(Color.White, 2),
                            new PointF(0, 0),
                            new PointF(cx + 40, cy + 40),
                            new PointF(cx + 30, 0 - 100),
                            new PointF(0, cy));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            graph = Graphics.FromHwnd(this.pictureBox6.Handle);
            cx = pictureBox6.Size.Width;
            cy = pictureBox6.Size.Height;
            //grfx.Clear(Color.Silver);
            PointF[] points = new PointF[4] {new PointF(0+2,0+2),
                                             new PointF(cx/2-20,cy-20),
                                             new PointF(cx/2+20,cy-20),
                                             new PointF(cx,0)};

            graph.DrawCurve(new Pen(Color.Violet, 6), points);
            graph.DrawCurve(new Pen(Color.Pink, 2), points);
        }
    }
}
