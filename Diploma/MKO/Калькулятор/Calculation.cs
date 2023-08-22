using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using MKO;

namespace MKO
{
    public partial class Calculation : Form
    {
        int countCrit;
        int countConstr;
        int countVar;
        MultiCriteriaProblem prob;
        double[] solutionsF;
        List<double[]> simplexXOpt;
        DataGridView criteriaTable;
        DataGridView coeffTable;
        DataGridView constTable;
        string wrongFileFormatMessage = "Неверный формат данных в файле! Проверьте файл на соответствие правилам оформления исходных данных.";

        public Calculation()
        {
            InitializeComponent();
            textBoxCountConstraint.Text = "5";
            textBoxCountCriteria.Text = "2";
            textBoxCountVar.Text = "4";
            textBoxAlpha.Text = "100";
            textBoxEps.Text = "0,01";
            textBoxEpsGrad.Text = "0,01";
            textBoxStep.Text = "10";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonCreateEmptyProblem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            prob = null;
            countCrit = Convert.ToInt32(textBoxCountCriteria.Text);
            countVar = Convert.ToInt32(textBoxCountVar.Text);
            countConstr = Convert.ToInt32(textBoxCountConstraint.Text);
            createUIProblem(countCrit, countVar, countConstr, false);
            buttonComputeF.Visible = true;
            buttonSaveProb.Visible = true;
            labelOptF.Visible = false;
            labelProblem.Visible = false;
            textBoxProb.Visible = false;
        }

        private void createUIProblem(int countCrit, int countVar, int countConstr, bool generate)
        {
            Label criteriaLabel = new Label();
            criteriaLabel.Location = new Point(0, 0);
            criteriaLabel.Text = "Критерии";
            criteriaLabel.AutoSize = true;
            panel1.Controls.Add(criteriaLabel);

            criteriaTable = new DataGridView();
            criteriaTable.AllowUserToAddRows = false;
            criteriaTable.Width = 60 * countVar;
            criteriaTable.RowHeadersVisible = false;
            criteriaTable.Location = new Point(criteriaLabel.Location.X + 60, groupBox1.Location.Y + 5);
            Random rand = new Random();
            for (int i = 0; i < countVar; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = "X" + (i + 1);
                col.Width = 60;
                criteriaTable.Columns.Add(col);
            }
            criteriaTable.RowCount = countCrit;

            int distance = 53;
            for (int i = 0; i < countCrit; i++)
            {
                ComboBox min = new ComboBox();
                min.Width = 50;
                min.Items.Add("MIN");
                min.Items.Add("MAX");
                if (prob != null)
                {
                    min.SelectedIndex = (prob.Minimize[i] == true) ? 0 : 1;
                }
                else if (generate == true)
                {
                    min.SelectedIndex = (rand.NextDouble() >= 0.5) ? 0 : 1;
                }
                else
                {
                    min.SelectedIndex = 0;
                }
                min.Location = new Point(criteriaLabel.Location.X, criteriaLabel.Location.Y + distance);
                distance += 22;
                panel1.Controls.Add(min);
            }

            int sum = criteriaTable.ColumnHeadersHeight;
            foreach (DataGridViewRow row in criteriaTable.Rows)
                sum += row.Height;
            criteriaTable.Height = sum;
            criteriaTable.ScrollBars = ScrollBars.None;
            for (int i = 0; i < criteriaTable.RowCount; i++)
            {
                for (int j = 0; j < criteriaTable.ColumnCount; j++)
                {
                    if (prob != null)
                    {
                        criteriaTable[j, i].Value = prob.CriteriaCoefficients[i, j];
                    }
                    else if (generate == true)
                    {
                        criteriaTable[j, i].Value = Math.Round(rand.NextDouble() * 10, 1);
                    }
                    else
                    {
                        criteriaTable[j, i].Value = 0;
                    }
                }
            }
            panel1.Controls.Add(criteriaTable);

            Label coeffLabel = new Label();
            coeffLabel.Location = new Point(0, criteriaTable.Location.Y + criteriaTable.Height + 15);
            coeffLabel.Text = "Коэффициенты ограничений";
            coeffLabel.AutoSize = true;
            panel1.Controls.Add(coeffLabel);

            coeffTable = new DataGridView();
            coeffTable.AllowUserToAddRows = false;
            coeffTable.Width = 60 * countVar;
            coeffTable.RowHeadersVisible = false;
            coeffTable.Location = new Point(coeffLabel.Location.X + 60, coeffLabel.Location.Y + 20);
            for (int i = 0; i < countVar; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = "X" + (i + 1);
                col.Width = 60;
                coeffTable.Columns.Add(col);
            }
            coeffTable.RowCount = countConstr;
            sum = coeffTable.ColumnHeadersHeight;
            foreach (DataGridViewRow row in coeffTable.Rows)
                sum += row.Height;
            coeffTable.Height = sum;
            coeffTable.ScrollBars = ScrollBars.None;
            for (int i = 0; i < coeffTable.RowCount; i++)
            {
                for (int j = 0; j < coeffTable.ColumnCount; j++)
                {
                    if (prob != null)
                    {
                        coeffTable[j, i].Value = prob.ConstraintCoefficients[i, j];
                    }
                    else if (generate == true)
                    {
                        coeffTable[j, i].Value = Math.Round(rand.NextDouble() * 5, 1);
                    }
                    else
                    {
                        coeffTable[j, i].Value = 0;
                    }
                }
            }
            panel1.Controls.Add(coeffTable);

            distance = 24;
            ComboBox lessThan = new ComboBox();
            for (int i = 0; i < countConstr; i++)
            {
                lessThan = new ComboBox();
                lessThan.Width = 50;
                lessThan.Items.Add("<=");
                lessThan.Items.Add(">=");
                lessThan.Items.Add("=");
                if (prob != null)
                {
                    switch (prob.ConstraintSigns[i])
                    {
                        case MathSign.LessThan:
                            lessThan.SelectedIndex = 0; break;
                        case MathSign.GreaterThan:
                            lessThan.SelectedIndex = 1; break;
                        case MathSign.Equal:
                            lessThan.SelectedIndex = 2; break;
                    }
                }
                else if (generate == true)
                {
                    double r = rand.NextDouble();
                    if (r <= 0.7)
                    {
                        lessThan.SelectedIndex = 0;
                    }
                    else if (r > 0.7 && r < 0.99)
                    {
                        lessThan.SelectedIndex = 1;
                    }
                    else if (r >= 0.99)
                    {
                        lessThan.SelectedIndex = 2;
                    }
                }
                else
                {

                    lessThan.SelectedIndex = 0;
                }
                lessThan.Location = new Point(coeffTable.Location.X + coeffTable.Width + 15, coeffTable.Location.Y + distance);
                distance += 22;
                panel1.Controls.Add(lessThan);
            }

            Label constLabel = new Label();
            constLabel.Location = new Point(lessThan.Location.X + lessThan.Width + 15, criteriaTable.Location.Y + criteriaTable.Height + 15);
            constLabel.Text = "Свободные члены";
            constLabel.AutoSize = true;
            panel1.Controls.Add(constLabel);
            constTable = new DataGridView();
            constTable.AllowUserToAddRows = false;
            constTable.Width = 60;
            constTable.RowHeadersVisible = false;
            constTable.Location = new Point(constLabel.Location.X, coeffTable.Location.Y);
            constTable.ColumnCount = 1;
            constTable.Columns[0].Name = "B";
            constTable.RowCount = countConstr;
            sum = constTable.ColumnHeadersHeight;
            foreach (DataGridViewRow row in constTable.Rows)
                sum += row.Height;
            constTable.Height = sum;
            constTable.ScrollBars = ScrollBars.None;
            for (int i = 0; i < constTable.RowCount; i++)
            {
                if (prob != null)
                {
                    constTable[0, i].Value = prob.Constants[i];
                }
                else if (generate == true)
                {
                    constTable[0, i].Value = Math.Round(rand.NextDouble() * 150);
                }
                else
                {
                    constTable[0, i].Value = 0;
                }
            }
            panel1.Controls.Add(constTable);

            distance = 15;
            for (int i = 0; i < countVar; i++)
            {
                Label X = new Label();
                X.Location = new Point(criteriaLabel.Location.X, coeffTable.Location.Y + coeffTable.Height + distance);
                X.Text = "X" + (i + 1);
                X.AutoSize = true;
                panel1.Controls.Add(X);
                ComboBox zero = new ComboBox();
                zero.Width = 50;
                zero.Location = new Point(X.Location.X + X.Width + 10, coeffTable.Location.Y + coeffTable.Height + distance);
                zero.Items.Add(">= 0");
                zero.Items.Add(" ");
                if (prob != null && prob.NotNonNegativeVarInd != null && prob.NotNonNegativeVarInd.Contains(i))
                {
                    zero.SelectedIndex = 1;
                }
                else
                {
                    zero.SelectedIndex = 0;
                }
                panel1.Controls.Add(zero);
                distance += 20;
            }
        }

        private void buttonOpenFromFile_Click(object sender, EventArgs e)
        {
            prob = null;
            panel1.Controls.Clear();
            labelOptF.Visible = false;
            labelProblem.Visible = false;
            textBoxProb.Visible = false;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Текстовый документ (.txt)| *.txt";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string filename = dlg.FileName;
                string extension = Path.GetExtension(filename);
                try
                {
                    if (extension == ".txt")
                    {
                        string[] fileLines = File.ReadAllLines(filename);
                        if (fileLines.Length == 0)
                        {
                            throw new Exception("Пустой файл!");
                        }
                        string[] split = fileLines[0].Split(' ');
                        if (!Int32.TryParse(split[0], out countCrit))
                            throw new Exception(wrongFileFormatMessage);
                        if (!Int32.TryParse(split[1], out countConstr))
                            throw new Exception(wrongFileFormatMessage);
                        if (!Int32.TryParse(split[2], out countVar))
                            throw new Exception(wrongFileFormatMessage);

                        bool[] minimize = new bool[countCrit];
                        double[,] criteriaCoefficients = new double[countCrit, countVar];
                        double[,] constraintCoefficients = new double[countConstr, countVar];
                        double[] constants = new double[countConstr];
                        MathSign[] constraintSigns = new MathSign[countConstr];
                        int[] NotNonNegativeVarInd;
                        for (int i = 1; i <= countCrit; i++)
                        {
                            string line = fileLines[i];
                            string[] splitStr = line.Split(' ');
                            minimize[i - 1] = splitStr[0] == "1";
                            for (int j = 1; j <= countVar; j++)
                            {
                                if (!Double.TryParse(splitStr[j], out criteriaCoefficients[i - 1, j - 1]))
                                    throw new Exception(wrongFileFormatMessage);
                            }
                        }
                        for (int i = countCrit + 1; i < countConstr + countCrit + 1; i++)
                        {
                            string line = fileLines[i];
                            string[] splitStr = line.Split(' ');
                            for (int j = 0; j < countVar; j++)
                            {
                                if (!Double.TryParse(splitStr[j], out constraintCoefficients[i - countCrit - 1, j]))
                                    throw new Exception(wrongFileFormatMessage);
                            }
                            if (!Double.TryParse(splitStr[countVar + 1], out constants[i - countCrit - 1]))
                                throw new Exception(wrongFileFormatMessage);
                            switch (splitStr[countVar])
                            {
                                case "<=": constraintSigns[i - countCrit - 1] = MathSign.LessThan; break;
                                case ">=": constraintSigns[i - countCrit - 1] = MathSign.GreaterThan; break;
                                case "=": constraintSigns[i - countCrit - 1] = MathSign.Equal; break;
                                default: throw new Exception(wrongFileFormatMessage);
                            }
                        }
                        if (fileLines.Length == countConstr + countCrit + 2)
                        {
                            string[] splitStr = fileLines[countConstr + countCrit + 1].Split(' ');
                            NotNonNegativeVarInd = new int[splitStr.Length];
                            for (int i = 0; i < splitStr.Length; i++)
                            {
                                if (!Int32.TryParse(splitStr[i], out NotNonNegativeVarInd[i]))
                                    throw new Exception(wrongFileFormatMessage);
                            }
                        }
                        else
                        {
                            NotNonNegativeVarInd = null;
                        }
                        try
                        {
                            prob = new MultiCriteriaProblem(minimize, criteriaCoefficients, constraintCoefficients, constants, constraintSigns, NotNonNegativeVarInd);
                            createUIProblem(countCrit, countVar, countConstr, false);
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                    buttonComputeF.Visible = true;
                    buttonSaveProb.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void buttonCompute_Click(object sender, EventArgs e)
        {
            bool[] minimize = new bool[countCrit];
            double[,] criteriaCoefficients = new double[countCrit, countVar];
            double[,] constraintCoefficients = new double[countConstr, countVar];
            double[] constants = new double[countConstr];
            MathSign[] constraintSigns = new MathSign[countConstr];
            int[] NotNonNegativeVarInd;
            List<string> comboboxText = new List<string>();
            foreach (Control c in panel1.Controls)
            {
                if (c is ComboBox)
                    comboboxText.Add(((ComboBox)c).SelectedItem.ToString());
            }
            for (int i = 0; i < countCrit; i++)
            {
                minimize[i] = comboboxText[i] == "MIN";
            }
            for (int i = countCrit; i < countCrit + countConstr; i++)
            {
                switch (comboboxText[i])
                {
                    case "<=": constraintSigns[i - countCrit] = MathSign.LessThan; break;
                    case ">=": constraintSigns[i - countCrit] = MathSign.GreaterThan; break;
                    case "=": constraintSigns[i - countCrit] = MathSign.Equal; break;
                }
            }
            List<int> tempNotNonNeg = new List<int>();
            for (int i = countCrit + countConstr; i < countCrit + countConstr + countVar; i++)
            {
                if (comboboxText[i] == " ")
                {
                    tempNotNonNeg.Add(i - countCrit - countConstr);
                }
            }
            NotNonNegativeVarInd = tempNotNonNeg.ToArray();
            for (int i = 0; i < criteriaTable.RowCount; i++)
            {
                for (int j = 0; j < criteriaTable.ColumnCount; j++)
                {
                    criteriaCoefficients[i, j] = Convert.ToDouble(criteriaTable[j, i].Value);
                }
            }
            for (int i = 0; i < coeffTable.RowCount; i++)
            {
                for (int j = 0; j < coeffTable.ColumnCount; j++)
                {
                    constraintCoefficients[i, j] = Convert.ToDouble(coeffTable[j, i].Value);
                }
            }
            for (int i = 0; i < constTable.RowCount; i++)
            {
                constants[i] = Convert.ToDouble(constTable[0, i].Value);
            }
            prob = new MultiCriteriaProblem(minimize, criteriaCoefficients, constraintCoefficients, constants, constraintSigns, NotNonNegativeVarInd);
            solutionsF = new double[prob.Minimize.Length];
            SimplexMethod sm;
            labelOptF.Text = "Идеальная точка, найденная симлекс-методом:\r\n(";
            simplexXOpt = new List<double[]>();
            double[] x = new double[prob.CountVariables];
            for (int i = 0; i < prob.CountCriteria; i++)
            {
                sm = new SimplexMethod(prob, i);
                Tuple<double, double[]> res;
                res = sm.Calculate();
                if (res != null)
                {
                    solutionsF[i] = res.Item1;
                    simplexXOpt.Add(res.Item2);
                }
                else
                {
                    solutionsF = null;
                    simplexXOpt = null;
                    break;
                }
                labelOptF.Text += solutionsF[i] + " ";
            }
            if (solutionsF != null)
            {
                labelOptF.Text += ")";
                labelOptF.Visible = true;
                labelProblem.Text = "Решается следующая задача на заданном множестве ограничений:";
                textBoxProb.Text = "min ";
                for (int i = 0; i < prob.CriteriaCoefficients.GetLength(0); i++)
                {
                    textBoxProb.Text += "(";
                    for (int j = 0; j < prob.CriteriaCoefficients.GetLength(1); j++)
                    {
                        textBoxProb.Text += prob.CriteriaCoefficients[i, j] + "X" + (j + 1);
                        if (j != prob.CriteriaCoefficients.GetLength(1) - 1)
                        {
                            textBoxProb.Text += (prob.CriteriaCoefficients[i, j + 1] >= 0) ? "+" : "";
                        }
                    }
                    textBoxProb.Text += (solutionsF[i] > 0) ? "-" : "+";
                    textBoxProb.Text += Math.Abs(Math.Round(solutionsF[i], 1));
                    textBoxProb.Text += ")^2";
                    if (i != prob.CriteriaCoefficients.GetLength(0) - 1)
                    {
                        textBoxProb.Text += " + ";
                    }
                }
                labelProblem.Visible = true;
                textBoxProb.Visible = true;
                buttonComputePenalty.Visible = true;
            }
            else
            {
                labelOptF.Text = "Система ограничений задачи несовместна";
                labelOptF.Visible = true;
            }
        }

        private void buttonComputePenalty_Click(object sender, EventArgs e)
        {
            try
            {
                double eps, epsGrad, a, step;
                if (!Double.TryParse(textBoxEps.Text, out eps))
                    throw new Exception("Неверный формат погрешности для метода штрафных функций");
                if (!Double.TryParse(textBoxEpsGrad.Text, out epsGrad))
                    throw new Exception("Неверный формат погрешности для метода наискорейшего спуска");
                if (!Double.TryParse(textBoxAlpha.Text, out a))
                    throw new Exception("Неверный формат штрафного коэффициента");
                if (!Double.TryParse(textBoxStep.Text, out step))
                    throw new Exception("Неверный формат шага");
                PenaltyMethod pm = new PenaltyMethod(prob, solutionsF, simplexXOpt[0], eps, epsGrad, a, step);
                ResultsForm res = new ResultsForm(pm, simplexXOpt);
                res.Show();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            prob = null;
            countCrit = Convert.ToInt32(textBoxCountCriteria.Text);
            countVar = Convert.ToInt32(textBoxCountVar.Text);
            countConstr = Convert.ToInt32(textBoxCountConstraint.Text);
            createUIProblem(countCrit, countVar, countConstr, true);
            buttonComputeF.Visible = true;
            buttonSaveProb.Visible = true;
            labelOptF.Visible = false;
            labelProblem.Visible = false;
            textBoxProb.Visible = false;
        }

        private void buttonSaveProb_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Текстовый документ(.txt)| *.txt";
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FilterIndex = 1;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFileDialog1.FileName;
                    string extension = Path.GetExtension(filename);
                    if (extension == ".txt")
                    {
                        using (StreamWriter sw = new StreamWriter(filename))
                        {
                            sw.Write(countCrit + " " + countConstr + " " + countVar);
                            sw.WriteLine();
                            List<string> comboboxText = new List<string>();
                            foreach (Control c in panel1.Controls)
                            {
                                if (c is ComboBox)
                                    comboboxText.Add(((ComboBox)c).SelectedItem.ToString());
                            }
                            for (int i = 0; i < countCrit; i++)
                            {
                                sw.Write((comboboxText[i] == "MIN") ? "1 " : "0 ");
                                for (int j = 0; j < countVar; j++)
                                {
                                    sw.Write(Convert.ToDouble(criteriaTable[j, i].Value));
                                    if (j != countVar - 1) sw.Write(" ");
                                }
                                sw.WriteLine();
                            }
                            int k = countCrit;
                            for (int i = 0; i < countConstr; i++)
                            {
                                for (int j = 0; j < countVar; j++)
                                {
                                    sw.Write(Convert.ToDouble(coeffTable[j, i].Value) + " ");
                                }
                                sw.Write(comboboxText[k] + " ");
                                k++;
                                sw.Write(Convert.ToDouble(constTable[0, i].Value));
                                sw.WriteLine();
                            }

                        }
                    }
                    else
                        throw new Exception("Неверное расширение файла!");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Текстовый документ(.txt)| *.txt";
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FilterIndex = 1;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFileDialog1.FileName;
                    string extension = Path.GetExtension(filename);
                    if (extension == ".txt")
                    {
                        using (StreamWriter sw = new StreamWriter(filename))
                        {
                            List<string> comboboxText = new List<string>();
                            foreach (Control c in panel1.Controls)
                            {
                                if (c is ComboBox)
                                    comboboxText.Add(((ComboBox)c).SelectedItem.ToString());
                            }
                            for (int i = 0; i < countCrit; i++)
                            {
                                sw.Write((comboboxText[i] == "MIN") ? "min " : "max ");
                                for (int j = 0; j < countVar; j++)
                                {
                                    if (Convert.ToDouble(criteriaTable[j, i].Value) != 0)
                                    {
                                        sw.Write(Convert.ToDouble(criteriaTable[j, i].Value) + "x" + (j + 1) + " ");
                                        if (j != countVar - 1) sw.Write("+ ");
                                    }
                                }
                                sw.WriteLine();
                            }
                            int k = countCrit;
                            for (int i = 0; i < countConstr; i++)
                            {
                                for (int j = 0; j < countVar; j++)
                                {
                                    if (Convert.ToDouble(coeffTable[j, i].Value) != 0)
                                    {
                                        sw.Write(Convert.ToDouble(coeffTable[j, i].Value) + "x" + (j + 1) + " ");
                                    }
                                    if (j != countVar - 1) sw.Write("+ ");
                                }
                                sw.Write(comboboxText[k] + " ");
                                k++;
                                sw.Write(Convert.ToDouble(constTable[0, i].Value));
                                sw.WriteLine();
                            }

                        }
                    }
                    else
                        throw new Exception("Неверное расширение файла!");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonM_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
