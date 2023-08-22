using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //this.DragButton.MouseDown += new MouseEventHandler(this.Button_MouseDown);

            //this.splitContainer1.Panel1.DragEnter += new DragEventHandler(this.Panels_DragEnter);
            //this.splitContainer1.Panel1.DragDrop += new DragEventHandler(this.Panels_DragDrop);

            //this.splitContainer1.Panel2.DragEnter += new DragEventHandler(this.Panels_DragEnter);
            //this.splitContainer1.Panel2.DragDrop += new DragEventHandler(this.Panels_DragDrop);
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            this.button1.MouseDown += new MouseEventHandler(this.button1_MouseMove);//запуск события MouseDown-тащим
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            // при нажатии на кнопку вызывается перетаскивание указанного объекта
            this.button1.DoDragDrop(button1, DragDropEffects.Move); // метод, начинает операцию перетаскивания. 
        }


        private void Panels_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }


        /*private void Panels_DragDrop(object sender, DragEventArgs e)
        {
            ((Button)e.Data.GetData(typeof(Button))).Parent = (Panel)sender;

            if (sender == this.splitContainer1.Panel1) 
                this.button1.Location = splitContainer1.Panel1.PointToClient(new Point(e.X, e.Y));
            if (sender == this.splitContainer1.Panel2) 
                this.button1.Location = splitContainer1.Panel2.PointToClient(new Point(e.X, e.Y));
        }*/

        private void Pane1_DragDrop(object sender, DragEventArgs e)
        {
            this.button1.Parent = (Panel)sender;
            this.button1.Location = splitContainer1.Panel1.PointToClient(new Point (e.X, e.Y));

        }
        private void Pane2_DragDrop(object sender, DragEventArgs e)
        {
            this.button1.Parent = (Panel)sender;
            this.button1.Location = splitContainer1.Panel2.PointToClient(new Point(e.X, e.Y));

        }

    }
}
