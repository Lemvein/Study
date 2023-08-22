using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MKO
{
    public partial class Test : Form
    {
        string[,] date = new string[5, 16];
        string[] f = new string[80];
        string[] tmp = new string[3];
        string ans;
        bool check;
        int number;
        int right;

        public Test()
        {
            string[] f = Properties.Resources._base.Split('\n');

            for (int i = 0; i < 16; i++)
            {
                date[0, i] = f[i * 5];
                date[1, i] = f[(i * 5) + 1];
                date[2, i] = f[(i * 5) + 2];
                date[3, i] = f[(i * 5) + 3];
                date[4, i] = f[(i * 5) + 4];
            }

            tmp[0] = date[4, 0];
            tmp[1] = date[4, 1];
            tmp[2] = date[4, 3];


            Random random = new Random();
            for (int i = 15; i >= 0; i--)
            {
                int j = random.Next(i);
                for (int k = 0; k < 5; k++)
                {
                    var temp = date[k, j];
                    date[k, j] = date[k, i];
                    date[k, i] = temp;
                }
            }


            InitializeComponent();

            label5.Text = date[0, 0];
            radioButton1.Text = date[1, 0];
            radioButton2.Text = date[2, 0];
            radioButton3.Text = date[3, 0];

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) { ans = tmp[0]; }
            else if (radioButton2.Checked == true) { ans = tmp[1]; }
            else if (radioButton3.Checked == true) { ans = tmp[2]; }
            else
            {
                MessageBox.Show("Ответ не выбран.", "Ошибка", MessageBoxButtons.OK);
                return;
            }


            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;

            if (ans == date[4, number])
            {
                check = true;
                right++;
            }
            else { check = false; }

            if (number == 0)
            {
                if (check == true) { textBox1.BackColor = Color.PaleGreen; }
                else { textBox1.BackColor = Color.LightCoral; }
            }
            else if (number == 1)
            {
                if (check == true) { textBox2.BackColor = Color.PaleGreen; }
                else { textBox2.BackColor = Color.LightCoral; }
            }
            else if (number == 2)
            {
                if (check == true) { textBox3.BackColor = Color.PaleGreen; }
                else { textBox3.BackColor = Color.LightCoral; }
            }
            else if (number == 3)
            {
                if (check == true) { textBox4.BackColor = Color.PaleGreen; }
                else { textBox4.BackColor = Color.LightCoral; }
            }
            else if (number == 4)
            {
                if (check == true) { textBox5.BackColor = Color.PaleGreen; }
                else { textBox5.BackColor = Color.LightCoral; }
            }
            else if (number == 5)
            {
                if (check == true) { textBox6.BackColor = Color.PaleGreen; }
                else { textBox6.BackColor = Color.LightCoral; }
            }
            else if (number == 6)
            {
                if (check == true) { textBox7.BackColor = Color.PaleGreen; }
                else { textBox7.BackColor = Color.LightCoral; }
            }
            else if (number == 7)
            {
                if (check == true) { textBox8.BackColor = Color.PaleGreen; }
                else { textBox8.BackColor = Color.LightCoral; }
            }
            else if (number == 8)
            {
                if (check == true) { textBox9.BackColor = Color.PaleGreen; }
                else { textBox9.BackColor = Color.LightCoral; }
            }
            else if (number == 9)
            {
                if (check == true) { textBox10.BackColor = Color.PaleGreen; }
                else { textBox10.BackColor = Color.LightCoral; }

                label5.Visible = false;
                groupBox1.Visible = false;
                panel1.Visible = true;
                button1.Visible = false;
                button2.Visible = true;

                label2.Text = right.ToString();

                if (right >= 9) { ans = "5"; }
                else if (right >= 7) { ans = "4"; }
                else if (right >= 5) { ans = "3"; }
                else { ans = "2"; }
                label3.Text = ans;

            }

            number++;
            label5.Text = date[0, number];
            radioButton1.Text = date[1, number];
            radioButton2.Text = date[2, number];
            radioButton3.Text = date[3, number];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu.M.mark_test = ans;
            this.Close();
        }
    }
}
