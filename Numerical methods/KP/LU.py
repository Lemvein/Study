import numpy as np
from copy import copy, deepcopy
from functools import reduce

#Вывод матрицы
def Print_matrix(matrix):
    for i in range(len(matrix)):
        for j in range(len(matrix[i])):
            print('{:6.2f}'.format(float(matrix[i][j])), end=' ')
        print()
    print()

#Вывод вектора
def Print_vector(vec):
    for i in range(len(vec)):
        print('{:6.2f}'.format(float(vec[i])), end=' ')
    print('\n')

def Get_column(A, k):
    column = [[0] for i in range(len(A))]
    for i in range(len(A)):
        column[i][0] = A[i][k]
    return column

#Вектор из столбца
def Column_to_vec(column):
    vec = []
    for i in range (len(column)):
        vec.append(column[i][0])
    return vec

#Транспонирование
def Transpose(A):
    C = [[A[j][i] for j in range(len(A))]
    for i in range(len(A[0]))] 
    return  C

def Swap(a, i, j):
    t = a[i]
    a[i] = a[j]
    a[j]= t

def LU_decomposition(A):
    LU = deepcopy(A)
    size = len(LU)
    P = [i for i in range(size)]
    n = 0
    for k in range(size):   #поиск главного элемента
        flag = 0
        for i in range(k, size):
            if abs(LU[i][k]) > flag:
                flag = abs(LU[i][k])
                n = i 
        if flag == 0:
            print('Вырожденная матрица\n')
            return -1
            
        Swap(P, k ,n)
        Swap(LU, k, n)

        for i in range(k + 1, size):
            LU[i][k] = LU[i][k] / LU[k][k]
            for j in range(k + 1, size):
                LU[i][j] = LU[i][j] - LU[i][k] * LU[k][j] 
    return LU, P

#Решение LU
def LU_solve(LU, P, B):
    size = len(LU)
    X = [0 for i in range(size)]
    Y = [0 for i in range(size)]

    for i in range(size):
        if i == 0:
            Y[i] = B[P[i]]
        else:
            suma_y = sum(map(lambda u, y: u * y, LU[i][:i], Y[:i]))
            Y[i] = B[P[i]] - suma_y

    for i in range(size - 1, -1, -1):
        if i == size - 1:
            X[i] = Y[i] / LU[i][i]
        else:
            suma_x = sum(map(lambda l, x: l * x, LU[i][i + 1 :], X[i + 1 :]))
            X[i] = (Y[i] - suma_x) / LU[i][i]
    return X

#Решение СЛАУ
def LU_solution(A, B):
    A_ = deepcopy(A)
    B_ = B
    A_, P = LU_decomposition(A_)
    X = LU_solve(A_, P, B_)
    return X

def Inverse(A):
    A_ = deepcopy(A)
    E = [[1 if i == j else 0 for i in range (len(A))] for j in range(len(A))]
    inv =[]
    for i in range(len(E)):
        column = Get_column(E, i)
        column = Column_to_vec(column)
        inv.append(LU_solution(A_, column))
    inv = Transpose(inv)
    return inv

def Determinant(A):
    A_ = deepcopy(A)
    A_ = LU_decomposition(A_)[0]
    return reduce(lambda x, y: x * y, [A_[i][i] for i in range(len(A_))])
    #reduce(lambda x, y: x*y, [A[i][i]]) эквивалентно ((A[0][0]*A[1][1])*A[2][2])...
