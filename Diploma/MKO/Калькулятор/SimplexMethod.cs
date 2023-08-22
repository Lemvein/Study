using System;
using System.Collections.Generic;

namespace MKO
{
    class SimplexMethod
    {
        public MultiCriteriaProblem Prob { get; set; }
        public int CriteriaInd { get; set; }
        public double[,] SimplexTable { get; set; }
        // Симплексная таблица:
        // 0 строка - коэффициенты при переменных в целевой функции
        // 1-n строки 1-m столбцы - коэффициенты при переменных в соответсвующих ограничениях
        // n+1 строка - оценки
        // n+2 строка - оценки: коэффициент при M
        // 0 столбец - свободные члены
        // Базис хранится отдельно
        public int[] Basis { get; set; }
        private int varInd;
        private int costMRow;
        private int costRow;
        private const double M = double.PositiveInfinity;

        private Dictionary<int, int> additionalVars;

        public SimplexMethod(MultiCriteriaProblem prob, int criteriaInd)
        {
            Prob = prob;
            CriteriaInd = criteriaInd;
            SimplexTable = new double[Prob.CountConstraint + 3,
                Prob.CountVariables + CountTotalNewVars() + 1];
            varInd = Prob.CountVariables + 1;
            costMRow = SimplexTable.GetLength(0) - 1;
            costRow = costMRow - 1;
            Basis = new int[Prob.CountConstraint];
            additionalVars = new Dictionary<int, int>();
            for (int i = 0; i < Prob.CountConstraint + 1; i++)
            {
                for (int j = 0; j < Prob.CountVariables + 1; j++)
                {
                    if (i == 0 && j != 0)
                    {
                        SimplexTable[0, j] = prob.CriteriaCoefficients[CriteriaInd, j - 1];
                    }
                    else if (i != 0 && j == 0)
                    {
                        SimplexTable[i, 0] = prob.Constants[i - 1];
                    }
                    else if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    else
                    {
                        SimplexTable[i, j] = prob.ConstraintCoefficients[i - 1, j - 1];
                    }
                }
            }

        }

        public Tuple<double, double[]> Calculate()
        {
            ConvertToStandardForm();
            AddArtificialVariables();
            FindInitialCosts();
            int prev = -1;
            while (true)
            {
                int pivotColumn = FindPivotColumn();
                if (pivotColumn == -1 || pivotColumn == prev)
                {
                    if (Math.Round(SimplexTable[SimplexTable.GetLength(0) - 1, 0], 10) != 0)
                    {
                        return null;
                    }
                    else
                    {
                        double[] xOpt = new double[Prob.CountVariables];
                        for (int i = 0; i < Basis.Length; i++)
                        {
                            if (Basis[i] - 1 < Prob.CountVariables)
                            {
                                xOpt[Basis[i] - 1] = SimplexTable[i + 1, 0];
                            }
                        }
                        if (additionalVars.Count != 0)
                        {
                            foreach (KeyValuePair<int, int> pair in additionalVars)
                            {
                                xOpt[pair.Key] -= xOpt[pair.Value];
                            }
                        }
                        double optObjectiveValue = SimplexTable[SimplexTable.GetLength(0) - 2, 0];
                        if (Prob.Minimize[CriteriaInd] == true)
                        {
                            optObjectiveValue *= -1;
                        }
                        return new Tuple<double, double[]>(optObjectiveValue, xOpt);
                    }
                }
                else
                {
                    int pivotRow = FindPivotRow(pivotColumn);
                    if (pivotRow == -1)
                    {
                        return null;
                    }
                    Basis[pivotRow - 1] = pivotColumn;
                    FindNewSimplexTable(pivotRow, pivotColumn);
                    prev = pivotColumn;
                }
            }
        }

        public int CountTotalNewVars()
        {
            int count = 0;
            for (int i = 0; i < Prob.CountConstraint; i++)
            {
                if (Prob.ConstraintSigns[i] == MathSign.LessThan && Prob.Constants[i] >= 0 ||
                    Prob.ConstraintSigns[i] == MathSign.GreaterThan && Prob.Constants[i] < 0)
                {
                    count++;
                }
                else if (Prob.ConstraintSigns[i] == MathSign.GreaterThan && Prob.Constants[i] >= 0 ||
                    Prob.ConstraintSigns[i] == MathSign.LessThan && Prob.Constants[i] < 0)
                {
                    count += 2;
                }
                else if (Prob.ConstraintSigns[i] == MathSign.Equal)
                {
                    count++;
                }
            }
            if (Prob.NotNonNegativeVarInd != null)
            {
                count += Prob.NotNonNegativeVarInd.Length;
            }
            return count;
        }

        public void ConvertToStandardForm()
        {
            ToMax();
            MakeConstantsPositive();
            MakeAllVarNonNegative();
            AddSlackAndSurplusVariables();
        }

        public void ToMax()
        {
            if (Prob.Minimize[CriteriaInd] == true)
            {
                for (int i = 1; i < Prob.CountVariables + 1; i++)
                {
                    SimplexTable[0, i] *= -1;
                }
            }
        }

        public void MakeConstantsPositive()
        {
            for (int i = 0; i < Prob.CountConstraint; i++)
            {
                if (Prob.Constants[i] < 0)
                {
                    for (int j = 0; j < SimplexTable.GetLength(1); j++)
                    {
                        SimplexTable[i + 1, j] *= -1;
                    }
                }
            }
        }

        public void MakeAllVarNonNegative()
        {
            if (Prob.NotNonNegativeVarInd != null)
            {
                foreach (int ind in Prob.NotNonNegativeVarInd)
                {
                    for (int i = 0; i < Prob.CountConstraint + 1; i++)
                    {
                        SimplexTable[i, varInd] = SimplexTable[i, ind + 1] * -1;
                    }
                    additionalVars.Add(ind, varInd - 1);
                    varInd++;
                }
            }
        }

        public void AddSlackAndSurplusVariables()
        {
            for (int i = 0; i < Prob.CountConstraint; i++)
            {
                if (Prob.ConstraintSigns[i] == MathSign.LessThan && Prob.Constants[i] >= 0 ||
                    Prob.ConstraintSigns[i] == MathSign.GreaterThan && Prob.Constants[i] < 0)
                {
                    SimplexTable[i + 1, varInd] = 1;
                    Basis[i] = varInd;
                    varInd++;
                }
                else if (Prob.ConstraintSigns[i] == MathSign.GreaterThan && Prob.Constants[i] >= 0 ||
                    Prob.ConstraintSigns[i] == MathSign.LessThan && Prob.Constants[i] < 0)
                {
                    SimplexTable[i + 1, varInd] = -1;
                    varInd++;
                }
            }
        }

        public void AddArtificialVariables()
        {
            for (int i = 0; i < Prob.CountConstraint; i++)
            {
                if (Prob.ConstraintSigns[i] == MathSign.GreaterThan && Prob.Constants[i] >= 0 ||
                    Prob.ConstraintSigns[i] == MathSign.LessThan && Prob.Constants[i] < 0 ||
                    Prob.ConstraintSigns[i] == MathSign.Equal)
                {
                    SimplexTable[i + 1, varInd] = 1;
                    SimplexTable[0, varInd] = -M;
                    Basis[i] = varInd;
                    varInd++;
                }
            }
        }

        private void FindInitialCosts()
        {
            int costMRow = SimplexTable.GetLength(0) - 1;
            int costRow = costMRow - 1;
            for (int j = 0; j < SimplexTable.GetLength(1); j++)
            {
                SimplexTable[costRow, j] = 0;
                SimplexTable[costMRow, j] = 0;
                for (int i = 1; i < SimplexTable.GetLength(0) - 2; i++)
                {
                    if (SimplexTable[0, Basis[i - 1]] == -M)
                    {
                        SimplexTable[costMRow, j] += SimplexTable[i, j] * -1;
                    }
                    else
                    {
                        SimplexTable[costRow, j] += SimplexTable[i, j] * SimplexTable[0, Basis[i - 1]];
                    }
                }
                if (SimplexTable[0, j] == -M)
                {
                    SimplexTable[costMRow, j] += 1;
                }
                else
                {
                    SimplexTable[costRow, j] -= SimplexTable[0, j];
                }
            }
        }

        private void FindNewSimplexTable(int pivotRow, int pivotColumn)
        {
            double[,] oldTable = new double[SimplexTable.GetLength(0), SimplexTable.GetLength(1)];
            Array.Copy(SimplexTable, oldTable, SimplexTable.Length);
            double pivotElem = SimplexTable[pivotRow, pivotColumn];
            for (int j = 0; j < SimplexTable.GetLength(1); j++)
            {
                SimplexTable[pivotRow, j] = oldTable[pivotRow, j] / pivotElem;
            }
            for (int i = 1; i < SimplexTable.GetLength(0); i++)
            {
                if (i == pivotRow || oldTable[i, pivotColumn] == 0)
                    continue;
                for (int j = 0; j < SimplexTable.GetLength(1); j++)
                {
                    SimplexTable[i, j] = oldTable[i, j] - oldTable[i, pivotColumn] * SimplexTable[pivotRow, j];
                }
            }
        }

        private int FindPivotColumn()
        {
            List<int> minInd = new List<int>();
            double minM = SimplexTable[costMRow, 1];
            for (int j = 2; j < SimplexTable.GetLength(1); j++)
            {
                if (SimplexTable[costMRow, j] < minM)
                {
                    minM = SimplexTable[costMRow, j];
                }
            }
            for (int j = 1; j < SimplexTable.GetLength(1); j++)
            {
                if (SimplexTable[costMRow, j] == minM)
                {
                    minInd.Add(j);
                }
            }
            if (minInd.Count == 1)
            {
                return minInd[0];
            }
            else
            {
                double min = SimplexTable[costRow, 1];
                int pivotCol = minInd[0];
                foreach (int j in minInd)
                {
                    if (SimplexTable[costRow, j] < min)
                    {
                        min = SimplexTable[costRow, j];
                        pivotCol = j;
                    }
                }
                if (min >= 0 && minM >= 0)
                {
                    return -1;
                }
                else
                    return pivotCol;
            }
        }

        private int FindPivotRow(int pivotCol)
        {
            int pivotRow = -1;
            for (int i = 1; i < SimplexTable.GetLength(0) - 2; i++)
            {
                if (SimplexTable[i, pivotCol] > 0)
                {
                    pivotRow = i;
                    break;
                }
            }
            if (pivotRow == -1)
                return pivotRow;
            for (int i = pivotRow + 1; i < SimplexTable.GetLength(0) - 2; i++)
            {
                if (SimplexTable[i, pivotCol] > 0 && SimplexTable[i, 0] / SimplexTable[i, pivotCol] <
                    SimplexTable[pivotRow, 0] / SimplexTable[pivotRow, pivotCol])
                {
                    pivotRow = i;
                }
            }
            return pivotRow;
        }
    }
}
