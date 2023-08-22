using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MKO
{
    public struct Marks
    {
        public string mark_test;
        public string mark_lab;
    }

    public partial class Menu : Form
    {
        static public Marks M;

        public Menu()
        {
            InitializeComponent();

            string[] row0 = { "Тестовая часть", "-" };
            string[] row1 = { "Практическая часть", "-" };


            dataGridView1.Rows.Add(row0);
            dataGridView1.Rows.Add(row1);

        }

        //Модуль лабораторной работы
        private void buttonLab_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
        }

        //Модуль решения задачи
        private void buttonCalc_Click(object sender, EventArgs e)
        {
            Calculation calculation = new Calculation();
            calculation.ShowDialog();
        }

        //Тестовая часть
        private void buttonT_Click(object sender, EventArgs e)
        {
            Test test = new Test();
            test.ShowDialog();
            dataGridView1.Rows[0].Cells[1].Value = M.mark_test;
        }

        //Практическая часть
        private void buttonP_Click(object sender, EventArgs e)
        {
            Lab lab = new Lab();
            lab.ShowDialog();
            dataGridView1.Rows[1].Cells[1].Value = M.mark_lab;
        }

        //Возврат в главное меню
        private void buttonM_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("После выхода в главное меню результаты работы будут удалены. Вы действительно хотите выйти? ", "Подтвердите действие", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK)
            {
                panel1.Visible = true;
                panel2.Visible = false;

                dataGridView1.Rows[0].Cells[1].Value = "-";
                dataGridView1.Rows[1].Cells[1].Value = "-";
            }
        }
    }
}
