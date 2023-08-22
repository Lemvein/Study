import math
import enum
import numpy as np
from numpy.linalg import norm

A5 = [
[-5, -8, 4],
[4, 2, 6],
[-2, 5, -6]
]

def Mul(A,B):
    C = []

    for i in range(0,len(A)):
        c_ = []
        for j in range(0,len(B[0])):
            elem = 0
            for k in range(0,len(B)):
                elem += A[i][k] * B[k][j]
            c_.append(elem)
        C.append(c_)
    return C

def Transpose(A):
    C = [[A[j][i] for j in range(len(A))]
    for i in range(len(A[0]))] 
    return  C

def Column_to_vec(column):
    vec = []
    for i in range (len(column)):
        vec.append(column[i][0])
    return vec

def Vec_to_column(vec):
    column = [[0] for i in range(len(vec))]
    for i in range(0, len(vec)):
        column[i][0] = vec[i]
    return column

def Get_column(A, k):
    column = [[0] for i in range(len(A))]
    for i in range(len(A)):
        column[i][0] = A[i][k]
    return column

def Sign(x):
    return -1 if x < 0 else 1 if x > 0 else 0

def Find_housholder_matrix(column, size, k):
    v = np.zeros(size)
    column = np.array(Column_to_vec(column))
    v[k] = column[k] + Sign(column[k]) * norm(column[k:])

    for i in range(k + 1, size):
        v[i] = column[i]

    v = v[:, np.newaxis]
    H = np.eye(size) - (2 / (v.T.dot(v))) * (v.dot(v.T))
    H = H.tolist()
    return H

def Find_QR(A):
    size = len(A)
    Q =  [[1 if i == j else 0 for j in range(size)] for i in range(size)]
    A_i = A

    for i in range(size - 1):
        column = Get_column(A_i, i)
        H = Find_housholder_matrix(column, len(A_i), i)
        Q = Mul(Q, H)
        A_i = Mul(H, A_i)

    return Q, A_i

def Get_roots_complex(A, i):
    sz = len(A)
    A11 = A[i][i]
    A12 = A[i][i + 1] if i + 1 < sz else 0
    A21 = A[i + 1][i] if i + 1 < sz else 0
    A22 = A[i + 1][i + 1] if i + 1 < sz else 0
    return np.roots((1, -A11 - A22, A11 * A22 - A12 * A21))

def Finish_iter_complex(A, eps, i):
    Q, R = Find_QR(A)
    A_next = Mul(R, Q)
    l1 = Get_roots_complex(A, i)
    l2 = Get_roots_complex(A_next, i)
    return True if abs(l1[0] - l2[0]) <= eps and \
            abs(l1[1] - l2[1]) <= eps else False


def Find_eigenval (A, eps, i):
    A_i = A
    while True:
        Q,R = Find_QR(A_i)
        A_i = Mul(R, Q)
        a = np.array(A_i)
        if norm(a[i + 1:, i]) <= eps:
            res = (a[i][i], False, A_i)
            break
        elif norm(a[i + 2:, i]) <= eps and Finish_iter_complex(A_i, eps, i):
            res = (Get_roots_complex(A_i, i), True, A_i)
            break
    return res

def QR (A, eps):
    res = []
    i = 0
    A_i = A
    while i < len(A):
        eigenval = Find_eigenval(A_i, eps, i)
        if eigenval[1]:
            res.extend(eigenval[0])
            i+=2
        else:
            res.append(eigenval[0])
            i+=1
        A_i = eigenval[2]
    return res

print('\n Собственные значения матрицы:\n')
print(QR(A5, 0.01))

#print('\n numpy:\n')
#print(np.linalg.eig(A5))