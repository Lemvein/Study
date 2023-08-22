using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // add

namespace lab10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //this.richTextBox1.Text = " Example with richTextBox ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.colorDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    this.label1.ForeColor = this.colorDialog1.Color;
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.fontDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    this.label1.Font = this.fontDialog1.Font;
                    break;
            }
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.openFileDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    using (Stream fileopen = this.openFileDialog1.OpenFile())
                        this.richTextBox1.LoadFile(fileopen, RichTextBoxStreamType.RichText);
                    MessageBox.Show(this.openFileDialog1.FileName);
                    break;
            }


        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.saveFileDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    MessageBox.Show(this.saveFileDialog1.FileName);
                    try
                    {
                        using (Stream fileopen = this.saveFileDialog1.OpenFile())
                            this.richTextBox1.SaveFile(fileopen, RichTextBoxStreamType.RichText);
                    }
                    catch (IOException exc)
                    {
                        MessageBox.Show(exc.Message, "Error");
                        return;
                    }
                    break;
                default:
                    break;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.folderBrowserDialog1.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    MessageBox.Show(this.folderBrowserDialog1.RootFolder.ToString());
                    MessageBox.Show(this.folderBrowserDialog1.SelectedPath.ToString());
                    break;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionColor = this.label1.ForeColor;
            this.richTextBox1.Font = this.label1.Font;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.richTextBox1.Text);

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = Clipboard.GetText();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.label1.ForeColor = DefaultBackColor;
            this.label1.Font = DefaultFont;
        }
    }
}
