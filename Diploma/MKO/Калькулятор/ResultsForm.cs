using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using MKO;

namespace MKO
{
    public partial class ResultsForm : Form
    {
        PenaltyMethod penalty;
        double[] xOpt;
        List<double[]> simplexXOpt;
        Stopwatch sw;

        public ResultsForm(PenaltyMethod pm, List<double[]> simplexXOpt)
        {
            InitializeComponent();
            this.penalty = pm;
            this.simplexXOpt = simplexXOpt;
            sw = Stopwatch.StartNew();
            xOpt = pm.Calculate();
            sw.Stop();
            WriteResults();
            label1.Text = "Время работы программы: " + sw.Elapsed.ToString(@"m\:ss\.ff");
        }

        private void WriteResults()
        {
            for (int i = 0; i < penalty.PenaltyIterations.Count; i++)
            {
                textBox1.Text += "Итерация " + (i + 1) + ": ";
                for (int j = 0; j < penalty.PenaltyIterations[i].Length; j++)
                {
                    textBox1.Text += penalty.PenaltyIterations[i][j] + "   ";
                }
                textBox1.Text += "\r\n";
            }
            penalty.AlphaK = 1;
            textBox1.Text += "Расстояние до идеальной точки f*: " + (Math.Sqrt(penalty.GetFunctionValue(xOpt)))+ "\r\n";
            textBox1.Text += "f* = (";
            for (int i = 0; i < penalty.Prob.CountCriteria; i++)
            {
                textBox1.Text += penalty.IdealF[i];
                if (i != penalty.Prob.CountCriteria - 1)
                {
                    textBox1.Text += "; ";
                }
            }
            textBox1.Text += ") \r\n";
            textBox1.Text += "f = (";
            double[] funcValues = penalty.Prob.GetCriteriaValue(xOpt);
            for (int i = 0; i < penalty.Prob.CountCriteria; i++)
            {
                textBox1.Text += funcValues[i];
                if (i != penalty.Prob.CountCriteria-1)
                {
                    textBox1.Text += "; ";
                }
            }
            textBox1.Text += ") \r\n";
            textBox1.Text += "Решения, полученные по каждому частному критерию:\r\n";
            for (int i = 0; i < penalty.Prob.CountCriteria; i++)
            {
                textBox1.Text += "f"+ (i+1) +" = (";
                double[] fv = penalty.Prob.GetCriteriaValue(simplexXOpt[i]);
                for (int j = 0; j < penalty.Prob.CountCriteria; j++)
                {
                    textBox1.Text += fv[j];
                    if (j != penalty.Prob.CountCriteria - 1)
                    {
                        textBox1.Text += "; ";
                    }
                }
                textBox1.Text += ") \r\n";
                textBox1.Text += "Расстояние от f" + (i +1) + " до точки, соответствующей решению f: " + penalty.VectorNorm(penalty.SubstractVectors(funcValues, fv)) + "\r\n";
            }

            double totalSum = 0;
            for (int i = 0; i < penalty.Prob.CountConstraint; i++)
            {
                double penForConstr = penalty.CountDifferenceForConstraints(xOpt,i);
                totalSum += penForConstr;
                textBox1.Text += (penForConstr!=0)? "Ограничение " + (i+1) + " нарушено на " + penForConstr + " единиц. \r\n":"Ограничение "+(i+1)+ " не нарушено. \r\n";
            }
            for (int i = 0; i < penalty.Prob.CountVariables; i++)
            {
                double penForNonNeg = penalty.CountDifferenceForNonNegativityConstraints(xOpt, i);
                totalSum += penForNonNeg;
                if (!penalty.ContainsValue(penalty.Prob.NotNonNegativeVarInd, i))
                {
                    textBox1.Text += (penForNonNeg != 0) ? "Ограничение на неотрицательность X" + (i + 1) + " нарушено на " + penForNonNeg + " единиц. \r\n" : "Ограничение на неотрицательность X" + (i + 1) + " не нарушено. \r\n";
                }
            }
            textBox1.Text += "Штраф (сумма невязок): " + totalSum + "\r\n";
            textBox1.Text += "Время работы программы: " + sw.Elapsed.ToString(@"m\:ss\.fffff") + "\r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Текстовый документ(.txt)| *.txt| Excel - файл(.xlsx) | *.xlsx";
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
                            sw.Write(textBox1.Text);
                        }
                    }
                    else if (extension == ".xlsx")
                    {
                        Excel.Application xlApp = new Excel.Application();
                        xlApp.Visible = false;
                        Excel.Workbook xlWorkbook = xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                        Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets[1];

                        for (int i = 0; i < penalty.PenaltyIterations.Count; i++)
                        {
                            for (int j = 0; j < penalty.PenaltyIterations[i].Length; j++)
                            {
                                xlWorksheet.Cells[i + 1, j + 1] = penalty.PenaltyIterations[i][j];
                            }
                        }
                        xlWorkbook.SaveAs(filename);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        xlWorkbook.Close();
                        xlApp.Quit();
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
    }
}
