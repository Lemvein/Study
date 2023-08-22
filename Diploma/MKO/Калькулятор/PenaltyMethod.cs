using System;
using System.Collections.Generic;

namespace MKO
{
    public class PenaltyMethod
    {
        public MultiCriteriaProblem Prob { get; set; }
        public double[] IdealF { get; }
        public double AlphaK { get; set; }
        double stepPenalty;
        double epsilon;
        double epsilonGrad;
        double[] simplexX;
        List<double[]> gradDescentIterations;
        public List<double[]> PenaltyIterations { get; set; }

        public PenaltyMethod(MultiCriteriaProblem prob, double[] idealF, double[] simplexX, double epsilon, double epsilonGrad, double alphaK, double step)
        {
            Prob = prob;
            IdealF = idealF;
            this.epsilon = epsilon;
            this.epsilonGrad = epsilonGrad;
            this.simplexX = simplexX;
            AlphaK = alphaK;
            stepPenalty = step;
            PenaltyIterations = new List<double[]>();
            gradDescentIterations = new List<double[]>();
        }

        public double[] Calculate()
        {
            double[] xk = new double[Prob.CountVariables];
            Array.Copy(simplexX, xk, xk.Length);
            double[] prev = new double[xk.Length];
            double norm;
            do
            {
                Array.Copy(xk, prev, xk.Length);
                xk = DeepGradientDescent(xk);
                PenaltyIterations.Add(xk);
                AlphaK += stepPenalty;
                norm = VectorNorm(SubstractVectors(prev, xk));
            }
            while (norm > epsilon);
            return xk;
        }

        public double[] DeepGradientDescent(double[] x0)
        {
            gradDescentIterations.Clear();
            double[] xk = new double[x0.Length];
            Array.Copy(x0, xk, x0.Length);
            for (int k = 0; ; k++)
            {
                gradDescentIterations.Add(xk);
                double[] prev = new double[x0.Length];
                Array.Copy(xk, prev, xk.Length);
                double[] sk = GetDerivativeInXk(xk);
                double[] interval = FindIntervalForBisectionMethod(xk, sk);
                double step = BisectionMethod(interval[0], interval[1], xk, sk);
                for (int i = 0; i < xk.Length; i++)
                {
                    xk[i] = xk[i] - step * sk[i];
                }
                double norm = VectorNorm(SubstractVectors(prev, xk));
                if (norm < epsilonGrad)
                {
                    break;
                }
            }
            return xk;
        }

        public double VectorNorm(double[] x)
        {
            double res = 0;
            for (int i = 0; i < x.Length; i++)
            {
                res += x[i] * x[i];
            }
            return Math.Sqrt(res);
        }

        public double[] SubstractVectors(double[] prev, double[] next)
        {
            double[] res = new double[prev.Length];
            for (int i = 0; i < prev.Length; i++)
            {
                res[i] = next[i] - prev[i];
            }
            return res;
        }

        public double[] FindIntervalForBisectionMethod(double[] xk, double[] sk)
        {
            double[] res = { 0, 0 };
            double step = 0.5;
            double[] x = new double[xk.Length];
            Array.Copy(xk, x, xk.Length);
            double funcPrev = GetFunctionValue(x) + GetPenaltyValue(x);
            while (true)
            {
                for (int i = 0; i < x.Length; i++)
                {
                    x[i] = xk[i] - step * sk[i];
                }
                double funcX = GetFunctionValue(x) + GetPenaltyValue(x);
                if (funcX >= funcPrev)
                {
                    res[0] = step - 1;
                    res[1] = step;
                    return res;
                }
                else
                {
                    funcPrev = funcX;
                    step += 0.5;
                }
            }
        }

        public double BisectionMethod(double a0, double b0, double[] xk, double[] grad)
        {
            double lk, mk;
            double epsilon = 0.000000000001;
            double delta = 0.5 * epsilon;
            double ak = a0, bk = b0;

            double[] x1 = new double[xk.Length];
            double[] x2 = new double[xk.Length];
            do
            {
                lk = (ak + bk - delta) / 2;
                mk = (ak + bk + delta) / 2;
                for (int i = 0; i < x1.Length; i++)
                {
                    x1[i] = xk[i] - lk * grad[i];
                    x2[i] = xk[i] - mk * grad[i];
                }
                if (GetFunctionValue(x1) + GetPenaltyValue(x1) <= GetFunctionValue(x2) + GetPenaltyValue(x2))
                {
                    bk = mk;
                }
                else
                {
                    ak = lk;
                }
            } while (bk - ak >= epsilon);
            return (ak + bk) / 2;
        }

        public double GetFunctionValue(double[] x)
        {
            double sum = 0;
            double temp = 0;
            for (int i = 0; i < Prob.CountCriteria; i++)
            {
                temp = 0;
                for (int j = 0; j < Prob.CountVariables; j++)
                {
                    temp += Prob.CriteriaCoefficients[i, j] * x[j];
                }
                temp -= IdealF[i];
                temp *= temp;
                sum += temp;
            }
            //sum += GetPenaltyValue(x);
            return sum;
        }

        public double[] GetDerivativeInXk(double[] x)
        {
            double[] sk = new double[x.Length];
            double[] sum = new double[Prob.CountCriteria];
            for (int i = 0; i < Prob.CountCriteria; i++)
            {
                for (int j = 0; j < Prob.CountVariables; j++)
                {
                    sum[i] += Prob.CriteriaCoefficients[i, j] * x[j];
                }
                sum[i] -= IdealF[i];
                sum[i] *= 2;
            }
            for (int i = 0; i < sk.Length; i++)
            {
                for (int j = 0; j < Prob.CountCriteria; j++)
                {
                    sk[i] += sum[j] * Prob.CriteriaCoefficients[j, i];
                }
            }
            double[] sum2 = new double[Prob.CountConstraint];
            for (int i = 0; i < Prob.CountConstraint; i++)
            {
                for (int j = 0; j < Prob.CountVariables; j++)
                {
                    sum2[i] += Prob.ConstraintCoefficients[i, j] * x[j];
                }
                sum2[i] -= Prob.Constants[i];
            }
            for (int i = 0; i < sk.Length; i++)
            {
                for (int j = 0; j < Prob.CountConstraint; j++)
                {
                    if (Prob.ConstraintSigns[j] == MathSign.LessThan && sum2[j] > 0 ||
                    Prob.ConstraintSigns[j] == MathSign.GreaterThan && sum2[j] < 0 ||
                    Prob.ConstraintSigns[j] == MathSign.Equal)
                    {
                        sk[i] += 2 * AlphaK * Math.Abs(sum2[j] * Prob.ConstraintCoefficients[j, i]);
                    }
                }
            }
            for (int i = 0; i < Prob.CountVariables; i++)
            {
                if (!ContainsValue(Prob.NotNonNegativeVarInd, i) && x[i] < 0)
                {
                    sk[i] += 2 * AlphaK * Math.Abs(x[i]);
                }
            }
            return sk;
        }

        public double GetPenaltyValue(double[] x)
        {
            double sum = 0;
            for (int i = 0; i < Prob.CountConstraint; i++)
            {
                double temp = 0;
                for (int j = 0; j < Prob.CountVariables; j++)
                {
                    temp += Prob.ConstraintCoefficients[i, j] * x[j];
                }
                temp -= Prob.Constants[i];

                if (Prob.ConstraintSigns[i] == MathSign.LessThan && temp > 0 ||
                    Prob.ConstraintSigns[i] == MathSign.GreaterThan && temp < 0 ||
                    Prob.ConstraintSigns[i] == MathSign.Equal)
                {
                    sum += temp * temp * AlphaK;
                }
            }
            for (int i = 0; i < Prob.CountVariables; i++)
            {
                if (!ContainsValue(Prob.NotNonNegativeVarInd, i) && x[i] < 0)
                {
                    sum += AlphaK * x[i] * x[i];
                }
            }
            return sum;
        }

        public bool ContainsValue(int[] arr, int val)
        {
            bool contains = false;
            if (arr != null)
            {
                for (int i = 0; i < Prob.NotNonNegativeVarInd.Length; i++)
                {
                    if (Prob.NotNonNegativeVarInd[i] == val)
                    {
                        contains = true;
                        break;
                    }
                }
            }
            return contains;
        }

        public double CountDifferenceForConstraints(double[] x, int constrInd)
        {
            double sum = 0;
            double temp = 0;
            for (int j = 0; j < Prob.CountVariables; j++)
            {
                temp += Prob.ConstraintCoefficients[constrInd, j] * x[j];
            }
            temp -= Prob.Constants[constrInd];

            if (Prob.ConstraintSigns[constrInd] == MathSign.LessThan && temp > 0 ||
                Prob.ConstraintSigns[constrInd] == MathSign.GreaterThan && temp < 0 ||
                Prob.ConstraintSigns[constrInd] == MathSign.Equal)
            {
                sum += Math.Abs(temp);
            }
            return sum;
        }

        public double CountDifferenceForNonNegativityConstraints(double[] x, int constrInd)
        {
            double sum = 0;
            if (!ContainsValue(Prob.NotNonNegativeVarInd, constrInd) && x[constrInd] < 0)
            {
                sum += Math.Abs(x[constrInd]);
            }
            return sum;
        }

    }
}
