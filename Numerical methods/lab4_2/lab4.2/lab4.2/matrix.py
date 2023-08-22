import numpy as np
import math
import cmath

from sys import stdin
from copy import deepcopy

############################## CLASS MATRIX
class Matrix:
    
# ИНИЦИАЛИЗАЦИЯ
    def __init__(self, matrix, LU=False, history=False):
        self.matrix = deepcopy(matrix)
        self.size = self._Size()
        if LU == True:
            self.LU, self.P, self.p = self._LUP(history)
        else:
            self.LU = None
            self.P = None
            self.p = 0
        
# ПЕЧАТЬ МАТРИЦЫ
    def __str__(self):
        return '\n'.join([''.join(['%f\t' % i for i in row]) for
                          row in self.matrix])
# РАЗМЕР МАТРИЦЫ
    def _Size(self):
        return np.array(self.matrix).shape
    
# LUP РАЗЛОЖЕНИЕ
    def _LUP(self, history=False):
        if self.size[0] != self.size[1]:
            raise Exception("Матрица должна быть квадратной")
    
        P = [i for i in range(self.size[0])]
        LU = self
        p = 0
 
        for k in range(self.size[0]):
            m = 0
            row, col = LU.Max_by_axis(k)
            if (row != k) & (LU.matrix[row][col] != 0):
                p += 1
            if LU.matrix[row][col] == 0:
                raise Exception("Столбец нулевой")
            P[k], P[row] = P[row], P[k]
            LU = Matrix.Permutation(row, col, self.size[0]).Multiply(LU)
            for i in range(k + 1, self.size[0]):
                LU.matrix[i][k] = LU.matrix[i][k] / LU.matrix[k][k]
                for j in range(k + 1, self.size[0]):
                    LU.matrix[i][j] = LU.matrix[i][j] - LU.matrix[i][k] * LU.matrix[k][j] 
            
        if history == True:
            print("P:\n{}".format(P))
        return LU, P, p

# LU
    def LU_(self, history=False):
        if self.size[0] != self.size[1]:
            raise Exception("Матрица должна быть квадратной")
            
        L = Matrix.E(self.size[0])
        U = Matrix.E(self.size[0])
        
        for i in range(self.size[0]):
            U.matrix[i][i] = self.LU.matrix[i][i]
            for j in range(self.size[0]):
                if (j < i):
                    L.matrix[i][j] = self.LU.matrix[i][j]
                else:
                    U.matrix[i][j] = self.LU.matrix[i][j]
        
        if history == True:
            print("L:\n{}".format(L))
            print("U:\n{}".format(U))
            print("LU:\n{}".format(L.Multiply(U)))
        return L, U

# УМНОЖЕНИЕ   
    def Multiply(self, m):
        if self.size[1] != m.size[0]:
            raise Exception("Несоответствие размерностей: {0} {1}".format(self.size, m.size))
        res = []
        rows = []
        for i in range(self.size[0]):
            for j in range(m.size[1]):
                val = 0
                for k in range(self.size[1]):
                    val += self.matrix[i][k] * m.matrix[k][j]                
                rows.append(val)    
            res.append(rows)
            rows = []
        if np.array(res).shape == (1, 1):
            return res[0][0]
        return Matrix(res)
    
# СУММА   
    def Sum(self, m):
        if self.size != m.size:
            raise Exception("Несоответствие размерностей: {0} {1}".format(self.size, m.size))
        res = []
        rows = []
        for i, row in enumerate(self.matrix):
            for j, col in enumerate(row):
                rows.append(self.matrix[i][j] + m.matrix[i][j])    
            res.append(rows)
            rows = []
        return Matrix(res)
    
# УМНОЖЕНИЕ НА ЧИСЛО   
    def MultiNum(self, n):
        res = []
        rows = []
        for i, row in enumerate(self.matrix):
            for j, col in enumerate(row):
                rows.append(n * self.matrix[i][j])    
            res.append(rows)
            rows = []
        return Matrix(res)
    
# МАКСИМАЛЬНЫЙ ЭЕЛЕМЕНТ СРОКИ ИЛИ СТОЛБЦА ПО МОДУЛЮ  
    def Max_by_axis(self, num, axis=1):
        m = 0
        num = num
        if axis == 1:
            for i in range(num, self.size[0]):
                if abs(self.matrix[i][num]) > m:
                    m = self.matrix[i][num]
                    row = i
                    col = num
        elif axis == 0:
            for i in range(self.size[1]):
                if abs(self.matrix[num][i]) > m:
                    m = self.matrix[num][i]
                    row = num
                    col = i
        else:
            raise Exception("Недопустимое значение axis")
        return row, col
    
# МАКСИМАЛЬНЫЙ ЭЛЕМЕНТ МАТРИЦЫ    
    def Max(self):
        m = -10000000000
        for i in range(self.size[0]):
            for j in range(self.size[1]):
                if abs(self.matrix[i][j]) > m:
                    m = self.matrix[i][j]
        return m
    
# ОПРЕДЕЛИТЕЛЬ
    def Det(self):
        if self.size[0] != self.size[1]:
            raise Exception("Матрица должна быть квадратной")
        if self.LU == None:
            self.LU, self.P, self.p = self._LUP()
        det = pow(-1, self.p)
        for k in range(self.size[0]):
            det *= self.LU.matrix[k][k]
        return det
            
# ОБРАТНАЯ МАТРИЦА
    def Reverse(self):
        if self.size[0] != self.size[1]:
            raise Exception("Матрица должна быть квадратной")
        if self.LU == None:
            self.LU, self.P, self.p = self._LUP()
        det = self.Det()
        if det == 0:
            raise Exception("Определитель равен 0")
        res = []
        for k in range(self.size[0]):
            res.append(Gauss_LU(self, e(k, self.size[0])))
        return Matrix(res).Transpose()
    
# ТРАНСПОНИРОВАНИЕ
    def Transpose(self):
        res = self
        if self.size[0] == self.size[1]:
            for i in range(self.size[0]):
                for j in range(i + 1, self.size[0]):
                    a = res.matrix[i][j]
                    res.matrix[i][j] = res.matrix[j][i]
                    res.matrix[j][i] = a
            return res
        else:
            res = []            
            for i in range(self.size[1]):
                rows = []
                for j in range(self.size[0]):
                    rows.append(self.matrix[j][i])
                res.append(rows)
            return Matrix(res)
        
# РАВЕНСТВО МАТРИЦ
    def Equal(A, B):
        if (A.size[0] != B.size[0]) | (A.size[1] != B.size[1]):
            return False
        else:
            for i in range(A.size[0]):
                for j in range(A.size[1]):
                    if A.matrix[i][j] != B.matrix[i][j]:
                        return False
            return True
        
# СИММЕТРИЧНОСТЬ МАТРИЦЫ
    def Simmetric(m):
        if m.size[0] != m.size[1]:
            return False
        else:
            for i in range(m.size[0]):
                for j in range(i + 1, m.size[1]):
                    if m.matrix[i][j] != m.matrix[j][i]:
                        return False
            return True
    
##################  СТАТИЧЕСКИЕ  #######################

# ЕДИНИЧНАЯ МАТРИЦА
    def E(n):
        e = []
        rows = []
        for i in range(n):
            for j in range(n):
                if i == j:
                    rows.append(1)
                else:
                    rows.append(0)
            e.append(rows)
            rows = []
        return Matrix(e)

# НУЛЕВАЯ МАТРИЦА   
    def Zero(n):
        z = []
        rows = []
        for i in range(n):
            for j in range(n):
                rows.append(0)
            e.append(rows)
            rows = []
        return Matrix(z)
    
# МАТРИЦА ПЕРЕСТАНОВОК   
    def Permutation(row_col_1, row_col_2, n):
        if (row_col_1 > n) | (row_col_2 > n):
            raise Exception("Индексы за пределами массива")
        row_col_1 = row_col_1
        row_col_2 = row_col_2
        p = []
        rows = []
        for i in range(n):
            for j in range(n):
                if ((i == row_col_1) & (j == row_col_2)) | ((i == row_col_2) & (j == row_col_1)):
                    rows.append(1)
                elif (i == j) & ((i != row_col_1) & (j != row_col_2) & (i != row_col_2) & (j != row_col_1)):#(flag == True):
                    rows.append(1)                    
                else:
                    rows.append(0)
            p.append(rows)
            rows = []
        return Matrix(p)

# ВЕКТОР НАПРАВЛЕНИЯ
    def e(i, n):
        e = []
        for j in range(n):
            if j == i:
                e.append(1)
            else:
                e.append(0)
    #return Matrix(e)
        return e

###################### GAUSS METHOD
def Gauss_LU(A, b, history=False):
    if (A.size[0] != A.size[1]) | (A.size[0] != len(b)):
        raise Exception("Система имеет бесконечное число решений") 
    L, U = A.LU_(history)
    
    x = [0] * A.size[0]
    z = [0] * A.size[0]
    n = A.size[0] 
    
    for i in range(n):
        summ = 0
        for j in range(i):
            summ += L.matrix[i][j] * z[j]
 
        z[i] = b[A.P[i]] - summ
    
    for i in range(n - 1, -1, -1):
        summ = 0
        for j in range(i + 1, n):
            summ += U.matrix[i][j] * x[j]
 
        x[i] = (z[i] - summ) / U.matrix[i][i]
    return x

########################## PROGONKA METHOD
def Progonka(A, b):
    if (A.size[0] != A.size[1]) | (A.size[0] != len(b)):
        raise Exception("Система имеет бесконечное число решений")   
    X = [0] * A.size[0]
    P = [0] * A.size[0]
    Q = [0] * A.size[0]
    P[0] = -A.matrix[0][1] / A.matrix[0][0]
    Q[0] = b[0] / A.matrix[0][0]
    for i in range(1, A.size[0]):
        if i != A.size[0] - 1:
            P[i] = -A.matrix[i][i + 1] / (A.matrix[i][i] + P[i - 1] * A.matrix[i][i - 1])
        else:
            P[i] = 0
        Q[i] = (b[i] - Q[i - 1] * A.matrix[i][i - 1]) / (A.matrix[i][i] + P[i - 1] * A.matrix[i][i - 1])
    for i in range(A.size[0] - 1, -1, -1):
        if i != A.size[0] - 1:
            X[i] = X[i + 1] * P[i] + Q[i]
        else:
            X[i] = Q[i]
    return X
        

########################## SIMPLE ITERATIONS
def alpha_beta(A, b):
    beta = []
    alpha = []
    rows_a = []
    rows_b = []
    for i in range(A.size[0]):
        rows_b.append(b.matrix[i] / A.matrix[i][i])
        beta.append(rows_b)
        rows_b = []
        for j in range(A.size[0]):
            if i == j:
                rows_a.append(0)
            else:
                rows_a.append(-(A.matrix[i][j] / A.matrix[i][i]))
        alpha.append(rows_a)
        rows_a = []
    return Matrix(alpha), Matrix(beta)

def norm(x):
    return x.Max()

def Simple_iterations(A, b, eps=0.01):
    if (A.size[0] != A.size[1]) | (A.size[0] != b.size[0]):
        raise Exception("Система имеет бесконечное число решений") 
    alpha, beta = alpha_beta(A, b)
    x = beta.Sum(alpha.Multiply(beta))
    flag = True
    if norm(alpha) < 1:
        e = (norm(alpha) * norm(beta)) / (1 - norm(alpha))
    else:
        e = norm(x.Sum(beta.MultiNum(-1)))
        flag = False
    k = 1
    x_new = []
    while e > eps:
        x_new = beta.Sum(alpha.Multiply(x))
        if flag == True:
            e = (norm(alpha) * norm(x_new.Sum(x.MultiNum(-1)))) / (1 - norm(alpha))
        else:
            e = norm(x_new.Sum(x.MultiNum(-1)))
        k = k + 1
        x = x_new0
    return x_new



####################### ZEIDEL METHOD
def alpha_beta_(A, b):
    beta = []
    B = []
    C = []
    rows_B = []
    rows_C = []
    rows_b = []
    for i in range(A.size[0]):
        rows_b.append(b.matrix[i] / A.matrix[i][i])
        beta.append(rows_b)
        rows_b = []
        for j in range(A.size[0]):
            if i == j:
                rows_B.append(0)
                rows_C.append(0)
            elif i > j:
                rows_B.append(-(A.matrix[i][j] / A.matrix[i][i]))
                rows_C.append(0)
            else:
                rows_C.append(-(A.matrix[i][j] / A.matrix[i][i]))
                rows_B.append(0)
        B.append(rows_B)
        C.append(rows_C)
        rows_B = []
        rows_C = []
    return Matrix(B), Matrix(C), Matrix(beta)

def Zeidel(A, b, eps=0.01):
    if (A.size[0] != A.size[1]) | (A.size[0] != b.size[0]):
        raise Exception("Система имеет бесконечное число решений") 
    B, C, beta = alpha_beta_(A, b)
    alpha = B.Sum(C)
    x = beta
    flag = True
    if norm(alpha) < 1:
        e = (norm(C) * norm(x)) / (1 - norm(alpha))
    else:
        e = norm(x)
        flag = False
    k = 1
    x_new = []
    while e > eps:
        x_new = (((((Matrix.E(A.size[0])).Sum(B.MultiNum(-1))).Reverse()).Multiply(C)).Multiply(x)).Sum((((Matrix.E(A.size[0])).Sum(B.MultiNum(-1))).Reverse()).Multiply(beta))
        if flag == True:
            e = (norm(C) * norm(x_new.Sum(x.MultiNum(-1)))) / (1 - norm(alpha))
        else:
            e = norm(x_new.Sum(x.MultiNum(-1)))
        k = k + 1
        x = x_new
    return x_new

########################## ROTATION METHOD
def max_ij(A):
    m = 0
    row, col = 0, 0
    for i in range(A.size[0]):
        for j in range(A.size[0]):
            if i == j:
                continue
            if (math.fabs(A.matrix[i][j]) > m) & (math.fabs(A.matrix[i][j]) != 0):
                m = A.matrix[i][j]
                row = i
                col = j
    if (row == 0) & (col == 0):
        raise Exception("Матрица вырожденная")
    return m, row, col

def t(A):
    a = 0
    for l in range(A.size[0]):
        for m in range(l + 1, A.size[0]):
            a += A.matrix[l][m] * A.matrix[l][m]
    a = math.sqrt(a)
    return a

def Rotation(A, eps=0.01):
    if Matrix.Simmetric(A) == False:
        raise Exception("Матрица несимметричная")
    Ak = A
    k = 0
    e = t(A)
    V = Matrix.E(A.size[0])
    U = []
    rows_U = []
    while e > eps:
        U = []
        k += 1
        m, i, j = max_ij(Ak)
        if Ak.matrix[i][i] != Ak.matrix[j][j]:
            phi = math.atan((2 * m) / (Ak.matrix[i][i] - Ak.matrix[j][j])) / 2
        else:
            phi = math.pi / 4
        for r in range(Ak.size[0]):
            for c in range(Ak.size[0]):
                if (r == i) & (c == j):
                    rows_U.append(-math.sin(phi))
                elif (r == j) & (c == i):
                    rows_U.append(math.sin(phi))
                elif ((r == i) & (c == i)) | ((c == j) & (r == j)):
                    rows_U.append(math.cos(phi))
                elif r == c:
                    rows_U.append(1)
                else:
                    rows_U.append(0) 
            U.append(rows_U)
            rows_U = []
        Ak = ((Matrix(U).Transpose()).Multiply(Ak)).Multiply(Matrix(U))
        e = t(Ak)
        V = V.Multiply(Matrix(U))
    res = []
    for l in range(Ak.size[0]):
        res.append(Ak.matrix[l][l])
    return res, V

########################## QR
def H(v):
    v1 = []    
    for i in range(len(v)):
        rows = []
        for j in range(len(v)):
            rows.append(v[i] * v[j])
        v1.append(rows)
    v2 = 0
    for i in range(len(v)):
        v2 += v[i] * v[i]
    return Matrix.E(len(v)).Sum(Matrix(v1).MultiNum(-2 / v2))

def sign(x):
    if x < 0:
        return -1
    elif x > 0:
        return 1
    else:
        return 0

def QR(A, history=False):
    if A.size[0] != A.size[1]:
        raise Exception("Матрица должна быть квадратной")
    if history == True:
        print("QR algorithm")
    Ak = A
    Q = Matrix.E(A.size[0])
    for k in range(A.size[0]):
        v = []
        for l in range(A.size[0]):
            if l < k:
                v.append(0)
            elif l == k:
                a = 0
                for j in range(A.size[0]):
                    a += Ak.matrix[j][k] * Ak.matrix[j][k]
                v.append(Ak.matrix[l][k] + sign(Ak.matrix[l][k]) * math.sqrt(a))
            else:
                v.append(A.matrix[l][k])
        Ak = H(v).Multiply(Ak)
        Q = Q.Multiply(H(v))
    if history == True:
        print("Q = \n {}".format(Q))
        print("R = \n {}".format(Ak))
        print("A = QR = \n {}".format(Q.Multiply(Ak)))
    return Q, Ak

# ДЛЯ КОМПЛЕКСНЫХ
def eps_1(A, k):
    e = 0
    for l in range(k + 2, A.size[0]):
        e += A.matrix[l][k] * A.matrix[l][k]
    e = math.sqrt(e)
    return e

def eps_l(l):
    return abs(l)

# ДЛЯ ВЕЩЕСТВЕННЫХ
def eps_2(A, k):
    e = 0
    for l in range(k + 1, A.size[0]):
        e += A.matrix[l][k] * A.matrix[l][k]
    e = math.sqrt(e)
    return e

def solve_lambda(A, k):
    b = A.matrix[k][k] + A.matrix[k + 1][k + 1]
    c = A.matrix[k][k] * A.matrix[k + 1][k + 1] - A.matrix[k][k + 1] * A.matrix[k + 1][k]
    d = b * b - 4 * c
    
    return complex((complex(-b) + cmath.sqrt(d)) / complex(2)), complex((complex(-b) - cmath.sqrt(d)) / complex(2))
def QR_values(A, eps=0.01):
    if A.size[0] != A.size[1]:
        raise Exception("Матрица должна быть квадратной")
    it = 0
    Q, R = QR(A)
    res = []
    cmplx = False
    for k in range(A.size[0]):
        if cmplx == True:
            cmplx = False
            continue
        it += 1
        cmplx = False
        Ak = R.Multiply(Q)
        e_1 = eps_1(Ak, k)
        e_2 = eps_2(Ak, k)
        count = 0
        
        while e_1 > eps:
            it += 1
            Q, R = QR(Ak)
            Ak = R.Multiply(Q)
            e_1 = eps_1(Ak, k)            
        
            l_1, l_2 = solve_lambda(Ak, k)            
        while e_2 > eps:
            count += 1           
            it += 1
            
            Q, R = QR(Ak)
            Ak = R.Multiply(Q)
            e_2 = eps_2(Ak, k)
            
            lk_1, lk_2 = solve_lambda(Ak, k)
            
            e_l_1 = eps_l(l_1 - lk_1)
            e_l_2 = eps_l(l_2 - lk_2)
            
            l_1 = lk_1
            l_2 = lk_2
            if count > 10000:
                cmplx = True
                while (e_l_1 > eps) & (e_l_2 > eps):
                    it += 1
                    lk_1, lk_2 = solve_lambda(Ak, k)
                    
                    e_l_1 = eps_l(l_1 - lk_1)
                    e_l_2 = eps_l(l_2 - lk_2)
                    
                    l_1 = lk_1
                    l_2 = lk_2
                break              
        
        if cmplx == True:
            res.append(l_1)
            res.append(l_2)
        else:
            res.append(Ak.matrix[k][k])
    return res