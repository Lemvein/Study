using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab12_2
{
    public partial class Form1 : Form
    {
        private Rectangle clipRect = new Rectangle(0, 0, 80, 80);
        public Form1()
        {
            InitializeComponent();
        }
        private double GetDistance(Point p1, Point p2)
        {
            int a = p2.X - p1.X;
            int b = p2.Y - p1.Y;
            return Math.Sqrt(a*a + b*b);
        }
        private Image ClipImage(Image origIm, Rectangle clipRect)
        {
            Bitmap clipBmp = new Bitmap(clipRect.Width, clipRect.Height);
            using (Graphics g = Graphics.FromImage(clipBmp))
            {
                g.DrawImage(origIm, new Rectangle(0, 0, clipRect.Width, clipRect.Height), clipRect, GraphicsUnit.Pixel);
            }
                return (Image)clipBmp;
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || (SystemInformation.MouseButtonsSwapped && e.Button == MouseButtons.Right))
            {
                Point topLeft = new Point(clipRect.Left, clipRect.Top);
                Point topRight = new Point(clipRect.Right, clipRect.Top);
                Point bottomLeft = new Point(clipRect.Left, clipRect.Bottom);
                Point bottomRight = new Point(clipRect.Right, clipRect.Bottom);

                Point mouse = e.Location;

                int threshold = 15;

                if (GetDistance(mouse, topLeft) <= threshold)
                {
                    topLeft = mouse;
                    bottomLeft.X = topLeft.X;
                    topRight.Y = topLeft.Y;
                }
                else if (GetDistance(mouse, topRight) <= threshold)
                {
                    topRight = mouse;
                    bottomRight.X = topLeft.X;
                    topLeft.Y = topLeft.Y;
                }
                else if (GetDistance(mouse, bottomLeft) <= threshold)
                {
                    bottomLeft = mouse;
                    topLeft.X = bottomLeft.X;
                    bottomRight.Y = bottomLeft.Y;
                }
                else if (GetDistance(mouse, bottomRight) <= threshold)
                {
                    bottomRight = mouse;
                    topLeft.X = bottomRight.X;
                    bottomLeft.Y = bottomRight.Y;
                }
                else if (clipRect.Contains(mouse))
                {
                    topLeft.X = mouse.X - (clipRect.Width / 2);
                    topLeft.Y = mouse.Y - (clipRect.Height / 2);
                    topRight.X = mouse.X + (clipRect.Width / 2);
                    topRight.Y = topLeft.Y;
                    bottomLeft.X = topLeft.X;
                    bottomLeft.Y = mouse.Y + (clipRect.Height / 2);
                    bottomRight.X = topLeft.X;
                    bottomRight.Y = bottomLeft.Y;
                }

                int width = topRight.X - topLeft.X;
                int height = bottomLeft.Y - topLeft.Y;
                clipRect = new Rectangle(topLeft.X, topLeft.Y, width, height);
                this.Refresh();
            }

        }

        //Load file
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dl = new OpenFileDialog();
            if (dl.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Dispose();
                    pictureBox1.Image = Image.FromFile(dl.FileName);
                }
                catch (Exception) { MessageBox.Show("Error"); }
            }
        }

        //Save file
        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                SaveFileDialog dl = new SaveFileDialog();
                if (dl.ShowDialog() == DialogResult.OK)
                {
                    if (!dl.FileName.EndsWith(".bmp"))
                        dl.FileName += ".bmp";
                    pictureBox2.Image.Save(dl.FileName);
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Lime, clipRect);
        }

        //Clip
        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
                pictureBox2.Image = ClipImage(pictureBox1.Image, clipRect);
        }
    }
}
