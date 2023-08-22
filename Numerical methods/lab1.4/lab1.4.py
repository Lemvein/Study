import math
import numpy as np

A4 = [
[4,7,-1],
[7,-9,-6],
[-1,-6,-4],
]

def Print_matrix(matrix):
    for i in range(len(matrix)):
        for j in range(len(matrix[i])):
            print('{:6.2f}'.format(float(matrix[i][j])), end=' ')
        print()
    print()

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

def find_id_max(A):
    i_max = j_max = 0
    elem_max = A[0][0]
    for i in range(len(A)):
        for j in range(i+1, len(A)):
            if abs(A[i][j]) > elem_max:
                elem_max = abs(A[i][j])
                i_max = i
                j_max = j
    return i_max, j_max


def find_phi(a_ii, a_jj, a_ij):
     return math.pi / 4 if a_ii == a_jj else \
            0.5 * math.atan(2 * a_ij / (a_ii - a_jj))

def find_t(A):
    C = math.sqrt(sum([A[i][j] ** 2 for i in range(len(A)) 
        for j in range(i + 1, len(A))]))
    return C

def Rotation(A, eps):
    size = len(A)
    eigen_vectors = [[1 if i == j else 0 for j in range(size)] for i in range(size)]
    while True:
        matrix_U = [[1 if i == j else 0 for j in range(size)] for i in range(size)]
        i, j = find_id_max(A)
        phi = find_phi(A[i][i], A[j][j], A[i][j])
        
        matrix_U[i][j] = -math.sin(phi)
        matrix_U[j][i] = math.sin(phi)
        matrix_U[i][i] = matrix_U[j][j] = math.cos(phi)

        matrix_UT = Transpose(matrix_U)
        A = Mul(Mul(matrix_UT,A), matrix_U)

        eigen_vectors = Mul(eigen_vectors, matrix_U)

        if find_t(A) < eps:
            break
    eigen_values = np.diagonal(A)
    return eigen_values, eigen_vectors
 

val, vec = Rotation(A4, 0.001)
print('Собственные значения:')
print(val)
print('\nСобственные векторы:')
Print_matrix(vec)