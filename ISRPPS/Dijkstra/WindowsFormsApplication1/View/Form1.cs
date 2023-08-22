using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Controller cont = Controller();
        public Form1()
        {
            InitializeComponent();
        }

        private void onClickOnForm(object sender, EventArgs e) 
        {
            Controller.OneClick(sender, e, debugLog);
        }
 
        private void onDoubleClickOnForm(object sender, EventArgs e)
        {
            Program.TwoClick(sender, e, debugLog, dataView);
        }

        private void dataView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Program.ValueChanged(sender, e, debugLog, dataView);
        }

        private void finishCreatingNodes_Click(object sender, EventArgs e) 
        {
            Program.finish();
        }

        private void SetStartPoint(object sender, EventArgs e)
        {
            Program.SetStart(sender, dataView);
        }

        private void SetEndPoint(object sender, EventArgs e)
        {
            Program.SetEnd(sender);
        }

        private void startDijkstra(object sender, EventArgs e)
        {
            Program.Dijkstra(sender, e, debugLog);
        }

        private void savePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                saveFileDialog.Title = "Save picture";

                var result = saveFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Bitmap b = new Bitmap(this.Width, this.Height);
                    Rectangle rectBounds1 = new Rectangle(0, 0, this.Width, this.Height);
                    this.DrawToBitmap(b, rectBounds1);
                    b.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.colorNode(colorDialog1);
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            
        }
    }
}
