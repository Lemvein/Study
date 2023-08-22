using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System;

namespace MKO
{
    public partial class Lab : Form
    {
        double[,] A = new double[4, 4];   //матрица 
        double[,] B = new double[2, 4];   //матрица 
        double[] Atmp = new double[4];    //вспомогательный вектор
        string[] result = new string[4];
        double[,] norm = new double[4, 4]; //матрица нормировки
        int mode;   //Номер "режима"
        int checkM; //Проверка значений в таблице
        int checkP; //Кнопки проверки
        int mistake; //Счетчик ошибок
        bool mistakeOne; //Счетчик каждой ошибки
        bool mistakeTwo;
        int num;
        string mark;

        public Lab()
        {
            InitializeComponent();

            mode = 0;
            checkM = 0;
            checkP = 0;
            mistake = 0;
            panel2.Visible = false;
            panelMode2.Visible = false;
            panelMode3.Visible = false;
            buttonCheckM.Visible = false;
            label4.Visible = false;


            Random random = new Random();
            int rand;
            /*
            for (int i = 0; i < 4; i++)
            {
                rand = random.Next(7, 15);
                A[i, 0] = rand;
            }
            for (int i = 0; i < 4; i++)
            {
                rand = random.Next(10, 30);
                A[i, 2] = rand;
            }
            for (int i = 0; i < 4; i++)
            {
                rand = random.Next(1, 10);
                A[i, 3] = rand;
            }
            */


            A[0, 0] = random.Next(12, 15);
            A[1, 0] = random.Next(8, 9);
            A[2, 0] = random.Next(10, 11);
            A[3, 0] = random.Next(6, 7);

            /*
            A[0, 1] = 10500;
            A[1, 1] = 12000;
            A[2, 1] = 7500;
            A[3, 1] = 6000;
            */

            for (int i = 0; i < 4; i++)
            {
                rand = random.Next(6000, 12000);
                A[i, 1] = rand;
            }

            A[0, 2] = random.Next(25, 30);
            A[1, 2] = random.Next(20, 24);
            A[2, 2] = random.Next(15, 19);
            A[3, 2] = random.Next(10, 14);

            A[0, 3] = random.Next(1, 3); ;
            A[1, 3] = random.Next(8, 10);
            A[2, 3] = random.Next(6, 7); ;
            A[3, 3] = random.Next(4, 5); ;



            //Заполнение таблицы из матрицы
            string[] row0 = { "A1", A[0, 0].ToString(), A[0, 1].ToString(), A[0, 2].ToString(), A[0, 3].ToString() };
            string[] row1 = { "A2", A[1, 0].ToString(), A[1, 1].ToString(), A[1, 2].ToString(), A[1, 3].ToString() };
            string[] row2 = { "A3", A[2, 0].ToString(), A[2, 1].ToString(), A[2, 2].ToString(), A[2, 3].ToString() };
            string[] row3 = { "A4", A[3, 0].ToString(), A[3, 1].ToString(), A[3, 2].ToString(), A[3, 3].ToString() };

            Matrix.Rows.Add(row0);
            Matrix.Rows.Add(row1);
            Matrix.Rows.Add(row2);
            Matrix.Rows.Add(row3);

            //Запрет на редактирование таблицы (если запретить для всей появляются проблема с разрешением)
            for (int i = 0; i <= 3; i++)
            {
                Matrix.Rows[i].ReadOnly = true;
            }

            //МАКС МИН по каждому столбцу таблицы (для глав крит)
            for (int i = 0; i <= 3; i++)
            {
                if ((i == 1) || (i == 3))
                {
                    for (int k = 0; k < 4; k++)
                    {
                        Atmp[k] = A[k, i];
                    }
                }
                else
                {
                    for (int k = 0; k < 4; k++)
                    {
                        Atmp[k] = -A[k, i];
                    }
                }
                B[0, i] = Atmp.Min();
                B[1, i] = Atmp.Max();
            }

        }

        //Размерность математической задачи (1 режим)
        private void buttonNext1_Click(object sender, EventArgs e)
        {
            if (text2.Text == "4")
            {
                mistakeOne = false;
                mistakeTwo = false;

                text2.Enabled = false;
                buttonNext1.Visible = false;
                panel2.Visible = true;
            }
            else if (mistakeOne == false)
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else
            {
                mistake++;
                text2.Text = "4";
                buttonNext1_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }
        }

        //Математическая модель (1 режим)
        private void button1_Click(object sender, EventArgs e)
        {
            if ((domainUpDown1.Text == "min") && (domainUpDown2.Text == "max") && (domainUpDown3.Text == "min") && (domainUpDown4.Text == "max") &&
                (system11.Text == A[0, 0].ToString()) && (system12.Text == A[1, 0].ToString()) && (system13.Text == A[2, 0].ToString()) && (system14.Text == A[3, 0].ToString()) &&
                (system21.Text == A[0, 1].ToString()) && (system22.Text == A[1, 1].ToString()) && (system23.Text == A[2, 1].ToString()) && (system24.Text == A[3, 1].ToString()) &&
                (system31.Text == A[0, 2].ToString()) && (system32.Text == A[1, 2].ToString()) && (system33.Text == A[2, 2].ToString()) && (system34.Text == A[3, 2].ToString()) &&
                (system41.Text == A[0, 3].ToString()) && (system42.Text == A[1, 3].ToString()) && (system43.Text == A[2, 3].ToString()) && (system44.Text == A[3, 3].ToString()) &&
                radioButton2.Checked == true
                )
            {
                mistakeOne = false;
                mistakeTwo = false;

                label4.Visible = true;
                buttonNext2.Visible = true;
                button1.Visible = false;

                system11.Enabled = false;
                system12.Enabled = false;
                system13.Enabled = false;
                system14.Enabled = false;
                system21.Enabled = false;
                system22.Enabled = false;
                system23.Enabled = false;
                system24.Enabled = false;
                system31.Enabled = false;
                system32.Enabled = false;
                system33.Enabled = false;
                system34.Enabled = false;
                system41.Enabled = false;
                system42.Enabled = false;
                system43.Enabled = false;
                system44.Enabled = false;
                domainUpDown1.Enabled = false;
                domainUpDown2.Enabled = false;
                domainUpDown3.Enabled = false;
                domainUpDown4.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
            }
            else if (mistakeOne == false)
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else
            {
                mistake++;
                domainUpDown1.Text = "min";
                domainUpDown2.Text = "max";
                domainUpDown3.Text = "min";
                domainUpDown4.Text = "max";
                system11.Text = A[0, 0].ToString();
                system12.Text = A[1, 0].ToString();
                system13.Text = A[2, 0].ToString();
                system14.Text = A[3, 0].ToString();
                system21.Text = A[0, 1].ToString();
                system22.Text = A[1, 1].ToString();
                system23.Text = A[2, 1].ToString();
                system24.Text = A[3, 1].ToString();
                system31.Text = A[0, 2].ToString();
                system32.Text = A[1, 2].ToString();
                system33.Text = A[2, 2].ToString();
                system34.Text = A[3, 2].ToString();
                system41.Text = A[0, 3].ToString();
                system42.Text = A[1, 3].ToString();
                system43.Text = A[2, 3].ToString();
                system44.Text = A[3, 3].ToString();
                radioButton2.Checked = true;
                button1_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }
        }

        //Кнопка перехода между "режимами" внизу справа
        private void buttonNext2_CLick(object sender, EventArgs e)
        {
            if (mode == 0)  //матмодель-уступки
            {
                mode = 1;
                buttonNext2.Visible = false;
                panelMode1.Visible = false;
                panelMode2.Visible = true;
                pictureBox1.Visible = true;
                Title1.Text = "Метод последовательных уступок";

                Matrix.Height = 182;
                string[] row4 = { "Ранг", "", "", "", "" };
                string[] row5 = { "Допустимые уступки", "", "", "", "" };
                Matrix.Rows.Add(row4);
                Matrix.Rows.Add(row5);
                Matrix.Rows[4].DefaultCellStyle.BackColor = Color.LightYellow;
                Matrix.Rows[5].DefaultCellStyle.BackColor = Color.LightYellow;
                Matrix.Rows[5].ReadOnly = true;

                buttonCheckM.Visible = true;

            }

            else if (mode == 1) //уступки-главкрит
            {
                mode = 2;
                buttonNext2.Visible = false;
                panelMode2.Visible = false;
                panelMode3.Visible = true;
                buttonCheckM.Visible = true;
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
                Title1.Text = "Метод главного критерия";

                for (int i = 0; i <= 3; i++)
                {
                    Matrix.Rows[i].ReadOnly = false;
                }

                //Matrix.Height = 131;
                Matrix.Rows.RemoveAt(5);
                Matrix.Rows.RemoveAt(4);
                string[] row4 = { "min", "", "", "", "-" };
                string[] row5 = { "max", "", "", "", "-" };
                Matrix.Rows.Add(row4);
                Matrix.Rows.Add(row5);

                Matrix.Rows[0].DefaultCellStyle.BackColor = Color.White;
                Matrix.Rows[1].DefaultCellStyle.BackColor = Color.White;
                Matrix.Rows[2].DefaultCellStyle.BackColor = Color.White;
                Matrix.Rows[3].DefaultCellStyle.BackColor = Color.White;
                Matrix.Rows[4].DefaultCellStyle.BackColor = Color.LightYellow;
                Matrix.Rows[5].DefaultCellStyle.BackColor = Color.LightYellow;

                Matrix.ReadOnly = false;
            }

            else if (mode == 2) //главкрит-свертка
            {
                mode = 3;
                buttonNext2.Visible = false;
                panelMode3.Visible = false;
                panelMode4.Visible = true;
                pictureBox2.Visible = false;
                pictureBox3.Visible = true;
                Title1.Text = "Метод свертывания критериев";

                Matrix.Height = 131;
                Matrix.Rows.RemoveAt(5);
                Matrix.Rows.RemoveAt(4);

            }

            else if (mode == 3) //адд-мульт
            {
                mode = 4;
                buttonNext2.Visible = false;

                label4Title.Text = "Мультипликативная свертка";
                label14.Text = "При мультипликативной свертке суперкритерий строится как:";
                label15.Text = "При использовании мультипликативной свертки не требуется проводить нормировку критериев. Однако, следует учитывать к чему стремятся целевые функции.\r\n" +
                            "Если функции минимизуется, в формуле вместо умножения выбираем деление. \r\n" +
                            "Выберем весовые коэффициенты на основе ранее заданного ранжирования: а1 = 0,3 ; а2 = 0,1 ; а3 = 0,2 ; а4 = 0,4. Новая таблица критериев с учетом весовых коэффициентов:\r\n";
                label18.Text = "Результат: при решении поставленной задачи методом мультипликативной свертки наилучшей является альтернатива ";

                Matrix.Rows[0].Cells[1].Value = (A[0, 0]).ToString();
                Matrix.Rows[1].Cells[1].Value = (A[1, 0]).ToString();
                Matrix.Rows[2].Cells[1].Value = (A[2, 0]).ToString();
                Matrix.Rows[3].Cells[1].Value = (A[3, 0]).ToString();
                Matrix.Rows[0].Cells[3].Value = (A[0, 2]).ToString();
                Matrix.Rows[1].Cells[3].Value = (A[1, 2]).ToString();
                Matrix.Rows[2].Cells[3].Value = (A[2, 2]).ToString();
                Matrix.Rows[3].Cells[3].Value = (A[3, 2]).ToString();

                MatrixNorm2.Rows.RemoveAt(3);
                MatrixNorm2.Rows.RemoveAt(2);
                MatrixNorm2.Rows.RemoveAt(1);
                MatrixNorm2.Rows.RemoveAt(0);

                button4Check.Location = new Point(784, 99);

                radioButtonAdd.Checked = false;
                textBoxF1.Text = "";
                textBoxF2.Text = "";
                textBoxF3.Text = "";
                textBoxF4.Text = "";
                domainUpDownF.Text = "-";
                radioButton11.Checked = false;
                radioButton12.Checked = false;
                radioButton13.Checked = false;
                radioButton14.Checked = false;


                radioButtonAdd.Enabled = true;
                radioButtonExtr.Enabled = true;
                radioButtonMult.Enabled = true;
                textBoxF1.Enabled = true;
                textBoxF2.Enabled = true;
                textBoxF3.Enabled = true;
                textBoxF4.Enabled = true;
                domainUpDownF.Enabled = true;
                radioButton11.Enabled = true;
                radioButton12.Enabled = true;
                radioButton13.Enabled = true;
                radioButton14.Enabled = true;

                button4Check.Visible = true;
                MatrixNorm2.Visible = false;
                textBoxF1.Visible = false;
                textBoxF2.Visible = false;
                textBoxF3.Visible = false;
                textBoxF4.Visible = false;
                domainUpDownF.Visible = false;
                groupBox1.Visible = false;
                label15.Visible = false;
                label16.Visible = false;
                label18.Visible = false;

            }
            else if (mode == 4) //вывод
            {
                panelMode4.Visible = false;
                panelMode5.Visible = true; //5
                pictureBox3.Visible = false;
                Title1.Text = "Результаты выполнения работы";

                buttonNext2.Visible = false;

                string[] row0 = { "Метод последовательных уступок", result[0] };
                string[] row1 = { "Метод главного критерия", result[1] };
                string[] row2 = { "Метод аддитивной свертки", result[2] };
                string[] row3 = { "Метод мультипликативной свертки", result[3] };

                dataGridView1.Rows.Add(row0);
                dataGridView1.Rows.Add(row1);
                dataGridView1.Rows.Add(row2);
                dataGridView1.Rows.Add(row3);

                label17.Text += mistake.ToString();

                if (mistake <= 2)
                {
                    mark += "5";
                }
                else if (mistake <= 5)
                {
                    mark += "4";
                }
                else if (mistake <= 10)
                {
                    mark += "3";
                }
                else
                {
                    mark += "2";
                }
                label20.Text += mark;
            }

        }

        //Кнопка проверки у таблицы
        private void buttonCheckM_Click(object sender, EventArgs e)
        {
            //проверка ранга (1 1)
            if ((checkM == 0) &&
               (Matrix.Rows[4].Cells[1].Value.ToString() == "2") && (Matrix.Rows[4].Cells[2].Value.ToString() == "4") &&
               (Matrix.Rows[4].Cells[3].Value.ToString() == "3") && (Matrix.Rows[4].Cells[4].Value.ToString() == "1"))
            {
                checkM = 1;
                mistakeOne = false;
                mistakeTwo = false;

                labelAlg2.Visible = true;
                label8.Visible = true;
                domainUpDownMM.Visible = true;
                buttonMode21.Visible = true;
                buttonCheckM.Visible = false;
                Matrix.Rows[4].ReadOnly = true; //запрет на редактирование ранга

                //вектор К4
                for (int i = 0; i < 4; i++)
                {
                    Atmp[i] = A[i, 3];
                }
                Array.Sort(Atmp);
            }
            else if ((checkM == 0) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkM == 0)
            {
                mistake++;
                Matrix.Rows[4].Cells[1].Value = "2";
                Matrix.Rows[4].Cells[2].Value = "4";
                Matrix.Rows[4].Cells[3].Value = "3";
                Matrix.Rows[4].Cells[4].Value = "1";
                buttonCheckM_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }


            //оптимальное  решение по К4
            else if ((checkM == 1) &&
                    (Matrix.CurrentCell.Value.ToString() == Atmp[3].ToString()))
            {
                checkM = 2;
                mistakeOne = false;
                mistakeTwo = false;

                for (int i = 0; i < 4; i++)
                {
                    if (Atmp[3].ToString() == Matrix.Rows[i].Cells[4].Value.ToString())
                    {
                        Matrix.Rows[i].DefaultCellStyle.BackColor = Color.Honeydew;
                    }
                }

                pictureBoxGist.Visible = false;
                labelAlg2.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                domainUpDownMM.Visible = false;
                buttonMode21.Visible = false;

                labelAlg.Text = "3) Задать допустимые пределы изменения ΔK4 (уступка)";
                label6.Text = "Так как эксперты считают критерий 'Качество' самым важным, то не стоит рассматривать слишком плохие варианты.\n" +
                    "Выберете еще 2 значения из столбца 'Качетсво', максимально близких к оптимальному.\n" +
                    "Введите в таблицу значение уступки и нажмите кнопку проверки.\n";
                Matrix.Rows[5].Cells[4].ReadOnly = false;
            }
            else if ((checkM == 1) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkM == 1)
            {
                mistake++;
                checkM = 2;
                mistakeOne = false;
                mistakeTwo = false;

                for (int i = 0; i < 4; i++)
                {
                    if (Atmp[3].ToString() == Matrix.Rows[i].Cells[4].Value.ToString())
                    {
                        Matrix.Rows[i].DefaultCellStyle.BackColor = Color.Honeydew;
                    }
                }

                pictureBoxGist.Visible = false;
                labelAlg2.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                domainUpDownMM.Visible = false;
                buttonMode21.Visible = false;

                labelAlg.Text = "3) Задать допустимые пределы изменения ΔK4 (уступка)";
                label6.Text = "Так как эксперты считают критерий 'Качество' самым важным, то не стоит рассматривать слишком плохие варианты.\n" +
                    "Выберете еще 2 значения из столбца 'Качетсво', максимально близких к оптимальному.\n" +
                    "Введите в таблицу значение уступки и нажмите кнопку проверки.\n";
                Matrix.Rows[5].Cells[4].ReadOnly = false;
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }


            //Уступки по К4
            else if ((checkM == 2) &&
                    (Matrix.Rows[5].Cells[4].Value.ToString() == (Atmp[3] - Atmp[1]).ToString()))
            {
                checkM = 3;
                mistakeOne = false;
                mistakeTwo = false;

                for (int i = 0; i < 4; i++)
                {
                    if ((Atmp[2].ToString() == Matrix.Rows[i].Cells[4].Value.ToString()) || (Atmp[1].ToString() == Matrix.Rows[i].Cells[4].Value.ToString()))
                    {
                        Matrix.Rows[i].DefaultCellStyle.BackColor = Color.Honeydew;
                    }
                }

                buttonCheckM.Visible = false;
                Matrix.Rows[5].Cells[4].ReadOnly = true;

                labelAlg2.Location = new Point(3, 110);
                labelAlg2.Visible = true;
                labelAlg2.Text = "4) Решается задача оптимизации второго частного критерия";

                label7.Location = new Point(19, 140);
                label7.Visible = true;
                label7.Text = "Поставленная задача имеет вид:                  →                  , при K4 ∈ [               ;               ]";
                textBox22.Visible = true;
                textBox23.Visible = true;
                domainUpDownKK.Visible = true;
                domainUpDownMM.Visible = true;
                domainUpDownMM.Enabled = true;
                domainUpDownMM.Text = "-";
                buttonMode21.Visible = true;
                domainUpDownMM.Location = new Point(350, 140);
                buttonMode21.Location = new Point(650, 140);
            }
            else if ((checkM == 2) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkM == 2)
            {
                mistake++;
                Matrix.Rows[5].Cells[4].Value = (Atmp[3] - Atmp[1]).ToString();
                buttonCheckM_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }


            //оптимальное  решение по К1
            else if ((checkM == 3) &&
                    (Matrix.CurrentCell.Value.ToString() == Atmp[1].ToString()))
            {
                checkM = 4;
                mistakeOne = false;
                mistakeTwo = false;

                Matrix.Rows[5].Cells[1].ReadOnly = false;

                for (int i = 0; i < 4; i++)
                {
                    if (Atmp[1].ToString() == Matrix.Rows[i].Cells[1].Value.ToString())
                    {
                        Matrix.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                    }
                }

                labelAlg3.Visible = true;
                label22.Visible = true;
            }
            else if ((checkM == 3) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkM == 3)
            {
                mistake++;

                checkM = 4;
                mistakeOne = false;
                mistakeTwo = false;

                Matrix.Rows[5].Cells[1].ReadOnly = false;

                for (int i = 0; i < 4; i++)
                {
                    if (Atmp[1].ToString() == Matrix.Rows[i].Cells[1].Value.ToString())
                    {
                        Matrix.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                    }
                }

                labelAlg3.Visible = true;
                label22.Visible = true;
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }


            //Уступки по К1
            else if ((checkM == 4) &&
                    (Matrix.Rows[5].Cells[1].Value.ToString() == (Atmp[2] - Atmp[1]).ToString()))
            {
                checkM = 5;
                mistakeOne = false;
                mistakeTwo = false;

                Matrix.Rows[5].Cells[4].ReadOnly = true;

                for (int i = 0; i < 4; i++)
                {
                    if ((Atmp[2].ToString() == Matrix.Rows[i].Cells[1].Value.ToString()))
                    {
                        Matrix.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                    }
                }

                buttonCheckM.Visible = false;
                label23.Visible = true;
                label24.Visible = true;
                domainUpDownKK2.Visible = true;
                domainUpDownMM2.Visible = true;
                textBox24.Visible = true;
                textBox25.Visible = true;

                buttonMode21.Visible = true;
                buttonMode21.Location = new Point(650, 300);
            }
            else if ((checkM == 4) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkM == 4)
            {
                mistake++;
                Matrix.Rows[5].Cells[1].Value = (Atmp[2] - Atmp[1]).ToString();
                buttonCheckM_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }


            //оптимальное  решение по К3
            else if ((checkM == 5) &&
                    (Matrix.CurrentCell.Value.ToString() == Atmp[2].ToString()))
            {
                checkM = 6;
                mistakeOne = false;
                mistakeTwo = false;

                buttonCheckM.Visible = false;

                for (int i = 0; i < 4; i++)
                {
                    if (Atmp[2].ToString() != Matrix.Rows[i].Cells[3].Value.ToString())
                    {
                        Matrix.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                }

                label26.Visible = true;
                if (Matrix.Rows[0].DefaultCellStyle.BackColor == Color.PaleGreen)
                {
                    result[0] = "А1";
                }
                else if (Matrix.Rows[1].DefaultCellStyle.BackColor == Color.PaleGreen)
                {
                    result[0] = "А2";
                }
                else if (Matrix.Rows[2].DefaultCellStyle.BackColor == Color.PaleGreen)
                {
                    result[0] = "А3";
                }
                else if (Matrix.Rows[3].DefaultCellStyle.BackColor == Color.PaleGreen)
                {
                    result[0] = "А4";
                }
                label26.Text += result[0];

                buttonNext2.Visible = true;
            }
            else if ((checkM == 5) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkM == 5)
            {
                mistake++;

                checkM = 6;
                mistakeOne = false;
                mistakeTwo = false;

                buttonCheckM.Visible = false;


                for (int i = 0; i < 4; i++)
                {
                    if (Atmp[2].ToString() != Matrix.Rows[i].Cells[3].Value.ToString())
                    {
                        Matrix.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                }

                label26.Visible = true;
                if (Matrix.Rows[0].DefaultCellStyle.BackColor == Color.PaleGreen)
                {
                    result[0] = "А1";
                }
                else if (Matrix.Rows[1].DefaultCellStyle.BackColor == Color.PaleGreen)
                {
                    result[0] = "А2";
                }
                else if (Matrix.Rows[2].DefaultCellStyle.BackColor == Color.PaleGreen)
                {
                    result[0] = "А3";
                }
                else if (Matrix.Rows[3].DefaultCellStyle.BackColor == Color.PaleGreen)
                {
                    result[0] = "А4";
                }
                label26.Text += result[0];

                buttonNext2.Visible = true;
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }



            //МЕТОД ГЛАВНОГО КРИТЕРИЯ

            //максимизация критериев
            else if ((checkM == 6) &&
                (Matrix.Rows[0].Cells[1].Value.ToString() == (-A[0, 0]).ToString()) && (Matrix.Rows[1].Cells[1].Value.ToString() == (-A[1, 0]).ToString()) && (Matrix.Rows[2].Cells[1].Value.ToString() == (-A[2, 0]).ToString()) && (Matrix.Rows[3].Cells[1].Value.ToString() == (-A[3, 0]).ToString()) &&
                (Matrix.Rows[0].Cells[3].Value.ToString() == (-A[0, 2]).ToString()) && (Matrix.Rows[1].Cells[3].Value.ToString() == (-A[1, 2]).ToString()) && (Matrix.Rows[2].Cells[3].Value.ToString() == (-A[2, 2]).ToString()) && (Matrix.Rows[3].Cells[3].Value.ToString() == (-A[3, 2]).ToString())
                )
            {
                checkM = 7;
                mistakeOne = false;
                mistakeTwo = false;

                buttonCheckM.Visible = false;

                Matrix.Rows[0].ReadOnly = true;
                Matrix.Rows[1].ReadOnly = true;
                Matrix.Rows[2].ReadOnly = true;
                Matrix.Rows[3].ReadOnly = true;


                label10.Visible = true;
                label11.Visible = true;
                button3Check.Visible = true;
                comboBoxNorm1.Visible = true;
                comboBoxNorm2.Visible = true;
                comboBoxNorm3.Visible = true;
                comboBoxNorm4.Visible = true;
                domainUpDownPM1.Visible = true;
                domainUpDownPM2.Visible = true;
            }
            else if ((checkM == 6) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkM == 6)
            {
                mistake++;
                Matrix.Rows[0].Cells[1].Value = (-A[0, 0]).ToString();
                Matrix.Rows[1].Cells[1].Value = (-A[1, 0]).ToString();
                Matrix.Rows[2].Cells[1].Value = (-A[2, 0]).ToString();
                Matrix.Rows[3].Cells[1].Value = (-A[3, 0]).ToString();
                Matrix.Rows[0].Cells[3].Value = (-A[0, 2]).ToString();
                Matrix.Rows[1].Cells[3].Value = (-A[1, 2]).ToString();
                Matrix.Rows[2].Cells[3].Value = (-A[2, 2]).ToString();
                Matrix.Rows[3].Cells[3].Value = (-A[3, 2]).ToString();
                buttonCheckM_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }

            //max min
            else if ((checkM == 7) &&
                    (Matrix.Rows[4].Cells[1].Value.ToString() == B[0, 0].ToString()) && (Matrix.Rows[5].Cells[1].Value.ToString() == B[1, 0].ToString()) &&
                    (Matrix.Rows[4].Cells[2].Value.ToString() == B[0, 1].ToString()) && (Matrix.Rows[5].Cells[2].Value.ToString() == B[1, 1].ToString()) &&
                    (Matrix.Rows[4].Cells[3].Value.ToString() == B[0, 2].ToString()) && (Matrix.Rows[5].Cells[3].Value.ToString() == B[1, 2].ToString()))
            {
                checkM = 8;
                mistakeOne = false;
                mistakeTwo = false;

                radioButton7.Visible = true;
                radioButton8.Visible = true;
                radioButton9.Visible = true;
                radioButton10.Visible = true;

                Matrix.Rows[4].ReadOnly = true;
                Matrix.Rows[5].ReadOnly = true;

                MatrixNorm.Visible = true;
                buttonCheckM.Visible = false;
                button3Check.Visible = true;
                button3Check.Location = new Point(1105, 361);
                label13.Visible = true;

                //Заполнение таблицы нормировки
                for (int i = 0; i < 4; i++)
                {
                    norm[i, 0] = Math.Round((-A[i, 0] - B[0, 0]) / (B[1, 0] - B[0, 0]), 2);
                    norm[i, 1] = Math.Round((A[i, 1] - B[0, 1]) / (B[1, 1] - B[0, 1]), 2);
                    norm[i, 2] = Math.Round((-A[i, 2] - B[0, 2]) / (B[1, 2] - B[0, 2]), 2);
                    norm[i, 3] = Math.Round((A[i, 3] - B[0, 3]) / (B[1, 3] - B[0, 3]), 2);
                }

                string[] row0 = { "A1", norm[0, 0].ToString(), norm[0, 1].ToString(), norm[0, 2].ToString(), A[0, 3].ToString() };
                string[] row1 = { "A2", norm[1, 0].ToString(), norm[1, 1].ToString(), norm[1, 2].ToString(), A[1, 3].ToString() };
                string[] row2 = { "A3", norm[2, 0].ToString(), norm[2, 1].ToString(), norm[2, 2].ToString(), A[2, 3].ToString() };
                string[] row3 = { "A4", norm[3, 0].ToString(), norm[3, 1].ToString(), norm[3, 2].ToString(), A[3, 3].ToString() };


                MatrixNorm.Rows.Add(row0);
                MatrixNorm.Rows.Add(row1);
                MatrixNorm.Rows.Add(row2);
                MatrixNorm.Rows.Add(row3);

                //Временный вектор подходящих альтернатив 
                Array.Clear(Atmp);
                for (int i = 0; i < 4; i++)
                {
                    if ((norm[i, 0] > 0.2) && (norm[i, 1] > 0.2) && (norm[i, 2] > 0.2))
                    {
                        Atmp[i] = A[i, 3];
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (Atmp.Max() == Atmp[i])
                    {
                        num = i;
                    }
                }

            }
            else if ((checkM == 7) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkM == 7)
            {
                mistake++;
                Matrix.Rows[4].Cells[1].Value = B[0, 0].ToString();
                Matrix.Rows[5].Cells[1].Value = B[1, 0].ToString();
                Matrix.Rows[4].Cells[2].Value = B[0, 1].ToString();
                Matrix.Rows[5].Cells[2].Value = B[1, 1].ToString();
                Matrix.Rows[4].Cells[3].Value = B[0, 2].ToString();
                Matrix.Rows[5].Cells[3].Value = B[1, 2].ToString();
                buttonCheckM_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }


        }

        //Кнопка проверки на панели метода последовательных уступок
        private void buttonMode21_Click(object sender, EventArgs e)
        {
            //Первый частный критерий
            if ((checkP == 0) &&
                (domainUpDownMM.Text == "max"))
            {
                checkP = 1;
                mistakeOne = false;
                mistakeTwo = false;

                buttonMode21.Visible = false;
                buttonCheckM.Visible = true;
                label9.Visible = true;
                domainUpDownMM.Enabled = false;
            }
            else if ((checkP == 0) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkP == 0)
            {
                mistake++;
                domainUpDownMM.Text = "max";
                buttonMode21_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }


            //Второй частный критерий
            else if ((checkP == 1) &&
                (domainUpDownKK.Text == "K1") && (domainUpDownMM.Text == "min") && (textBox22.Text == Atmp[1].ToString()) && (textBox23.Text == Atmp[3].ToString()))
            {
                checkP = 2;
                mistakeOne = false;
                mistakeTwo = false;

                buttonCheckM.Visible = true;
                buttonMode21.Visible = false;
                label9.Visible = true;
                label9.Location = new Point(19, 170);

                domainUpDownKK.Enabled = false;
                domainUpDownMM.Enabled = false;
                textBox22.Enabled = false;
                textBox23.Enabled = false;

                //вектор К1
                double tmp = Atmp[0];
                for (int i = 0; i < 4; i++)
                {
                    if (tmp != A[i, 3])
                    {
                        Atmp[i] = A[i, 0];
                    }
                }
                Array.Sort(Atmp);
            }
            else if ((checkP == 1) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkP == 1)
            {
                mistake++;
                domainUpDownKK.Text = "K1";
                domainUpDownMM.Text = "min";
                textBox22.Text = Atmp[1].ToString();
                textBox23.Text = Atmp[3].ToString();
                buttonMode21_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }


            //Третий частный критерий
            else if ((checkP == 2) &&
                (domainUpDownKK2.Text == "K3") && (domainUpDownMM2.Text == "min") && (textBox24.Text == Atmp[1].ToString()) && (textBox25.Text == Atmp[2].ToString()))
            {
                checkP = 3;
                mistakeOne = false;
                mistakeTwo = false;

                buttonCheckM.Visible = true;
                buttonMode21.Visible = false;
                label25.Visible = true;

                domainUpDownKK2.Enabled = false;
                domainUpDownMM2.Enabled = false;
                textBox24.Enabled = false;
                textBox25.Enabled = false;

                //вектор К3
                double tmp1 = Atmp[1];
                double tmp2 = Atmp[2];
                for (int i = 0; i < 4; i++)
                {
                    if (tmp1 == A[i, 0])
                    {
                        Atmp[0] = A[i, 2];
                    }
                    else if (tmp2 == A[i, 0])
                    {
                        Atmp[1] = A[i, 2];
                    }
                    else
                    {
                        Atmp[2] = 0;
                        Atmp[3] = 0;
                    }
                }
                Array.Sort(Atmp);

                checkP = 0;
            }
            else if ((checkP == 2) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkP == 2)
            {
                mistake++;
                domainUpDownKK2.Text = "K3";
                domainUpDownMM2.Text = "min";
                textBox24.Text = Atmp[1].ToString();
                textBox25.Text = Atmp[2].ToString();
                buttonMode21_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }
        }


        //Кнопка проверки на панели метода главного критерия
        private void button3Check_Click(object sender, EventArgs e)
        {
            //Формула нормировки
            if ((checkP == 0) &&
                (comboBoxNorm1.Text == "K[n](A[i])") && (comboBoxNorm2.Text == "min(K[n](A[i]))") && (comboBoxNorm3.Text == "max(K[n](A[i]))") && (comboBoxNorm4.Text == "min(K[n](A[i]))") &&
                (domainUpDownPM1.Text == "-") && (domainUpDownPM2.Text == "-"))
            {
                checkP = 1;
                mistakeOne = false;
                mistakeTwo = false;

                label12.Visible = true;
                buttonCheckM.Visible = true;
                button3Check.Visible = false;

                comboBoxNorm1.Enabled = false;
                comboBoxNorm2.Enabled = false;
                comboBoxNorm3.Enabled = false;
                comboBoxNorm4.Enabled = false;
                domainUpDownPM1.Enabled = false;
                domainUpDownPM2.Enabled = false;

            }
            else if ((checkP == 0) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkP == 0)
            {
                mistake++;
                comboBoxNorm1.Text = "K[n](A[i])";
                comboBoxNorm2.Text = "min(K[n](A[i]))";
                comboBoxNorm3.Text = "max(K[n](A[i]))";
                comboBoxNorm4.Text = "min(K[n](A[i]))";
                domainUpDownPM1.Text = "-";
                domainUpDownPM2.Text = "-";
                button3Check_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }

            else if ((checkP == 1) && ((radioButton7.Checked && num == 0) || (radioButton8.Checked && num == 1) || (radioButton9.Checked && num == 2) || (radioButton10.Checked && num == 3)))
            {
                mistakeOne = false;
                mistakeTwo = false;

                checkP = 0;
                buttonNext2.Visible = true;

                button3Check.Visible = false;
                label29.Visible = true;

                radioButton7.Enabled = false;
                radioButton8.Enabled = false;
                radioButton9.Enabled = false;
                radioButton10.Enabled = false;

                //выделение верной строки и вывод ответа

                for (int i = 0; i < 4; i++)
                {
                    if (num == i)
                    {
                        MatrixNorm.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                    }
                }

                if (MatrixNorm.Rows[0].DefaultCellStyle.BackColor == Color.PaleGreen)
                {
                    result[1] += "А1";
                }
                else if (MatrixNorm.Rows[1].DefaultCellStyle.BackColor == Color.PaleGreen)
                {
                    result[1] += "А2";
                }
                else if (MatrixNorm.Rows[2].DefaultCellStyle.BackColor == Color.PaleGreen)
                {
                    result[1] += "А3";
                }
                else if (MatrixNorm.Rows[3].DefaultCellStyle.BackColor == Color.PaleGreen)
                {
                    result[1] += "А4";
                }

                label29.Text += result[1];

                buttonNext2.Visible = true;
            }
            else if ((checkP == 1) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkP == 1)
            {
                mistake++;
                if (num == 0) { radioButton7.Checked = true; }
                if (num == 1) { radioButton8.Checked = true; }
                if (num == 2) { radioButton9.Checked = true; }
                if (num == 3) { radioButton10.Checked = true; }
                button3Check_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }
        }

        //Кнопка проверки на панели метода свертки
        private void button4Check_Click(object sender, EventArgs e)
        {
            if ((checkP == 0) && (radioButtonAdd.Checked == true))
            {
                checkP = 1;
                mistakeOne = false;
                mistakeTwo = false;

                button4Check.Location = new Point(1111, 405);
                radioButtonAdd.Enabled = false;
                radioButtonExtr.Enabled = false;
                radioButtonMult.Enabled = false;

                label15.Visible = true;
                label16.Visible = true;
                MatrixNorm2.Visible = true;
                textBoxF1.Visible = true;
                textBoxF2.Visible = true;
                textBoxF3.Visible = true;
                textBoxF4.Visible = true;
                domainUpDownF.Visible = true;

                //Матрица нормировки с коэф
                for (int i = 0; i < 4; i++)
                {
                    norm[i, 0] = Math.Round(0.3 * norm[i, 0], 2);
                    norm[i, 1] = Math.Round(0.1 * norm[i, 1], 2);
                    norm[i, 2] = Math.Round(0.2 * norm[i, 2], 2);
                    norm[i, 3] = Math.Round(0.4 * norm[i, 3], 2);
                }

                string[] row0 = { "A1", norm[0, 0].ToString(), norm[0, 1].ToString(), norm[0, 2].ToString(), norm[0, 3].ToString() };
                string[] row1 = { "A2", norm[1, 0].ToString(), norm[1, 1].ToString(), norm[1, 2].ToString(), norm[1, 3].ToString() };
                string[] row2 = { "A3", norm[2, 0].ToString(), norm[2, 1].ToString(), norm[2, 2].ToString(), norm[2, 3].ToString() };
                string[] row3 = { "A4", norm[3, 0].ToString(), norm[3, 1].ToString(), norm[3, 2].ToString(), norm[3, 3].ToString() };


                MatrixNorm2.Rows.Add(row0);
                MatrixNorm2.Rows.Add(row1);
                MatrixNorm2.Rows.Add(row2);
                MatrixNorm2.Rows.Add(row3);

                //Решение
                Array.Clear(Atmp);
                for (int i = 0; i < 4; i++)
                {
                    Atmp[i] = Math.Round(norm[i, 0] + norm[i, 1] + norm[i, 2] + norm[i, 3], 2);
                }
                for (int i = 0; i < 4; i++)
                {
                    if (Atmp[i] == Atmp.Max()) { num = i; }
                }


            }
            else if ((checkP == 0) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkP == 0)
            {
                mistake++;
                radioButtonAdd.Checked = true;
                button4Check_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }

            //Суперкритерий
            else if ((checkP == 1) &&
                (textBoxF1.Text == (Atmp[0]).ToString()) &&
                (textBoxF2.Text == (Atmp[1]).ToString()) &&
                (textBoxF3.Text == (Atmp[2]).ToString()) &&
                (textBoxF4.Text == (Atmp[3]).ToString()) &&
                (domainUpDownF.Text == "max")
                )
            {
                checkP = 2;
                mistakeOne = false;
                mistakeTwo = false;

                textBoxF1.Enabled = false;
                textBoxF2.Enabled = false;
                textBoxF3.Enabled = false;
                textBoxF4.Enabled = false;
                domainUpDownF.Enabled = false;
                groupBox1.Visible = true;
            }
            else if ((checkP == 1) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkP == 1)
            {
                mistake++;
                textBoxF1.Text = (Atmp[0]).ToString();
                textBoxF2.Text = (Atmp[1]).ToString();
                textBoxF3.Text = (Atmp[2]).ToString();
                textBoxF4.Text = (Atmp[3]).ToString();
                domainUpDownF.Text = "max";
                button4Check_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }

            //Альтернатива
            else if ((checkP == 2) && ((radioButton11.Checked && num == 0) || (radioButton12.Checked && num == 1) || (radioButton13.Checked && num == 2) || (radioButton14.Checked && num == 3)))
            {
                checkP = 3;
                mistakeOne = false;
                mistakeTwo = false;

                button4Check.Visible = false;
                label18.Visible = true;

                radioButton11.Enabled = false;
                radioButton12.Enabled = false;
                radioButton13.Enabled = false;
                radioButton14.Enabled = false;

                //выделение верной строки и вывод ответа
                for (int i = 0; i < 4; i++)
                {
                    if (num == i)
                    {
                        MatrixNorm2.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                    }
                }

                if (num == 0)
                {
                    result[2] += "А1";
                }
                else if (num == 1)
                {
                    result[2] += "А2";
                }
                else if (num == 2)
                {
                    result[2] += "А3";
                }
                else if (num == 3)
                {
                    result[2] += "А4";
                }

                label18.Text += result[2];

                buttonNext2.Visible = true;

            }
            else if ((checkP == 2) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkP == 2)
            {
                mistake++;
                if (num == 0) { radioButton11.Checked = true; }
                if (num == 1) { radioButton12.Checked = true; }
                if (num == 2) { radioButton13.Checked = true; }
                if (num == 3) { radioButton14.Checked = true; }
                button4Check_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }



            //МУЛЬТИПЛИКАТИВНАЯ
            else if ((checkP == 3) && (radioButtonMult.Checked == true))
            {
                checkP = 4;
                mistakeOne = false;
                mistakeTwo = false;

                button4Check.Location = new Point(1111, 405);
                radioButtonAdd.Enabled = false;
                radioButtonExtr.Enabled = false;
                radioButtonMult.Enabled = false;

                label15.Visible = true;
                label16.Visible = true;
                MatrixNorm2.Visible = true;
                textBoxF1.Visible = true;
                textBoxF2.Visible = true;
                textBoxF3.Visible = true;
                textBoxF4.Visible = true;
                domainUpDownF.Visible = true;

                //Матрица нормировки с коэф
                for (int i = 0; i < 4; i++)
                {
                    norm[i, 0] = Math.Round(Math.Pow(A[i, 0], 0.3), 2);
                    norm[i, 1] = Math.Round(Math.Pow(A[i, 1], 0.1), 2);
                    norm[i, 2] = Math.Round(Math.Pow(A[i, 2], 0.2), 2);
                    norm[i, 3] = Math.Round(Math.Pow(A[i, 3], 0.4), 2);
                }

                string[] row0 = { "A1", norm[0, 0].ToString(), norm[0, 1].ToString(), norm[0, 2].ToString(), norm[0, 3].ToString() };
                string[] row1 = { "A2", norm[1, 0].ToString(), norm[1, 1].ToString(), norm[1, 2].ToString(), norm[1, 3].ToString() };
                string[] row2 = { "A3", norm[2, 0].ToString(), norm[2, 1].ToString(), norm[2, 2].ToString(), norm[2, 3].ToString() };
                string[] row3 = { "A4", norm[3, 0].ToString(), norm[3, 1].ToString(), norm[3, 2].ToString(), norm[3, 3].ToString() };


                MatrixNorm2.Rows.Add(row0);
                MatrixNorm2.Rows.Add(row1);
                MatrixNorm2.Rows.Add(row2);
                MatrixNorm2.Rows.Add(row3);

                //Решение
                Array.Clear(Atmp);
                for (int i = 0; i < 4; i++)
                {
                    Atmp[i] = Math.Round((norm[i, 1] * norm[i, 3]) / (norm[i, 0] * norm[i, 2]), 2);
                }
                for (int i = 0; i < 4; i++)
                {
                    if (Atmp[i] == Atmp.Max()) { num = i; }
                }

            }
            else if ((checkP == 3) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkP == 3)
            {
                mistake++;
                radioButtonMult.Checked = true;
                button4Check_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }

            //Суперкритерий
            else if ((checkP == 4) &&
                (textBoxF1.Text == (Atmp[0]).ToString()) &&
                (textBoxF2.Text == (Atmp[1]).ToString()) &&
                (textBoxF3.Text == (Atmp[2]).ToString()) &&
                (textBoxF4.Text == (Atmp[3]).ToString()) &&
                (domainUpDownF.Text == "max")
                )
            {
                checkP = 5;
                mistakeOne = false;
                mistakeTwo = false;

                textBoxF1.Enabled = false;
                textBoxF2.Enabled = false;
                textBoxF3.Enabled = false;
                textBoxF4.Enabled = false;
                domainUpDownF.Enabled = false;
                groupBox1.Visible = true;
            }
            else if ((checkP == 4) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkP == 4)
            {
                mistake++;
                textBoxF1.Text = (Atmp[0]).ToString();
                textBoxF2.Text = (Atmp[1]).ToString();
                textBoxF3.Text = (Atmp[2]).ToString();
                textBoxF4.Text = (Atmp[3]).ToString();
                domainUpDownF.Text = "max";
                button4Check_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }

            //Альтернатива
            else if ((checkP == 5) && ((radioButton11.Checked && num == 0) || (radioButton12.Checked && num == 1) || (radioButton13.Checked && num == 2) || (radioButton14.Checked && num == 3)))
            {
                mistakeOne = false;
                mistakeTwo = false;

                button4Check.Visible = false;
                label18.Visible = true;

                radioButton11.Enabled = false;
                radioButton12.Enabled = false;
                radioButton13.Enabled = false;
                radioButton14.Enabled = false;

                //выделение верной строки и вывод ответа
                for (int i = 0; i < 4; i++)
                {
                    if (num == i)
                    {
                        MatrixNorm2.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                    }
                }

                if (num == 0)
                {
                    result[3] += "А1";
                }
                else if (num == 1)
                {
                    result[3] += "А2";
                }
                else if (num == 2)
                {
                    result[3] += "А3";
                }
                else if (num == 3)
                {
                    result[3] += "А4";
                }

                label18.Text += result[3];

                buttonNext2.Visible = true;

            }
            else if ((checkP == 5) && (mistakeOne == false))
            {
                if (mistakeTwo == false)
                {
                    mistakeTwo = true;
                    MessageBox.Show("Задание выполнено неверно. Осталось 2 попытки.", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    mistakeOne = true;
                    MessageBox.Show("Задание выполнено неверно. Осталась 1 попытка.", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else if (checkP == 5)
            {
                mistake++;
                if (num == 0) { radioButton11.Checked = true; }
                if (num == 1) { radioButton12.Checked = true; }
                if (num == 2) { radioButton13.Checked = true; }
                if (num == 3) { radioButton14.Checked = true; }
                button4Check_Click(sender, e);
                MessageBox.Show("Задание выполнено неверно.", "Ошибка", MessageBoxButtons.OK);
            }


        }

        //Menu
        private void button2_Click(object sender, EventArgs e)
        {
            Menu.M.mark_lab = mark;
            this.Close();
        }
    }

}