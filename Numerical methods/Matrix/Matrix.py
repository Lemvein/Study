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
        rows = len(self.matrix)
        cols = 0
        for row in self.matrix:
            if (type(row) == int) | (type(row) == float):
                break
            if len(row) > cols:
                cols = len(row)
        return (rows, cols)
    
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
    return e
