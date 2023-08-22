import numpy as np

#Вывод 
def PrintMatrix(matrix: np.array):
    for i in range(len(matrix)):
        for j in range(len(matrix[0, :])):
            print(round(matrix[i, j], 3), end=' ')
        print(end='\n')

#Ввод
def GetMatrix(s):
    matrix = []
    for line in s.split(';'):
        matrix.append(line.split())
    return np.array(matrix).astype(float)

#Разделение на блоки
def Block(mat, n):
    if n == 1:
        result = mat[0:len(mat)//2, 0:len(mat[0])//2]
    elif n == 2:
        result = mat[0:len(mat)//2, len(mat[0])//2:]
    elif n == 3:
        result = mat[len(mat)//2:, 0:len(mat[0])//2]
    elif n == 4:
        result = mat[len(mat)//2:, len(mat[0])//2:]
    return result

#Реккурсивное умножение
def Multiplication(mat1, mat2):
    if len(mat1[0]) == len(mat2):
        if mat1.shape == (2, 2) or mat1.shape == (1, 1) or mat1.shape == (1, 2) or mat1.shape == (2, 1):
            result = np.zeros((len(mat1), len(mat2[0])))
            s = 0
            for i in range(len(mat1)):
                for j in range(len(mat2[0])):
                    for k in range(len(mat1[0])):
                        s += mat1[i, k] * mat2[k, j]
                    result[i, j] = s
                    s = 0
            return result
    r11 = Multiplication(Block(mat1, 1), Block(mat2, 1)) + Multiplication(Block(mat1, 2), Block(mat2, 3))
    r12 = Multiplication(Block(mat1, 1), Block(mat2, 2)) + Multiplication(Block(mat1, 2), Block(mat2, 4))
    r21 = Multiplication(Block(mat1, 3), Block(mat2, 1)) + Multiplication(Block(mat1, 4), Block(mat2, 3))
    r22 = Multiplication(Block(mat1, 3), Block(mat2, 2)) + Multiplication(Block(mat1, 4), Block(mat2, 4))
    result = np.concatenate((np.concatenate((r11, r12), axis=1), np.concatenate((r21, r22), axis=1)), axis=0)
    return result

#Обратная матрица
def Reverse(mat):
    if len(mat) == 1 and len(mat[0]) == 1: return mat / (mat[0, 0] ** 2)
    if len(mat) == 2 and len(mat[0]) == 2:
        result = np.zeros((len(mat), len(mat[0])))
        for i in range(len(mat)):
            for j in range(len(mat[0, :])):
                minor = np.delete(np.delete(mat, i, axis=0), j, axis=1)
                result[j, i] = ((-1) ** (j + i)) * Determinant(minor)
        return result / Determinant(mat)

    a11 = Reverse(np.delete(np.delete(mat, len(mat) - 1, axis=0), len(mat[0]) - 1, axis=1))
    a22 = np.array([mat[len(mat) - 1, len(mat[0]) - 1]])
    a12 = np.array(mat[0:len(mat) - 1, len(mat[0]) - 1]).reshape(len(mat) - 1, 1)
    a21 = np.array(mat[len(mat) - 1, 0: len(mat[0]) - 1]).reshape(1, len(mat[0]) - 1)

    x = Multiplication(a11, a12)
    y = Multiplication(a21, a11)
    teta = a22 - Multiplication(a21, x)
    teta_inv = np.array([1 / teta[0, 0]]).reshape(1, 1)

    b11 = a11 + Multiplication(Multiplication(x, teta_inv), y)
    b12 = Multiplication(-1 * x, teta_inv)
    b21 = Multiplication(-1 * teta_inv, y)
    b22 = teta_inv

    result = np.concatenate((np.concatenate((b11, b12), axis=1), np.concatenate((b21, b22), axis=1)), axis=0)
    return result

def Swap(mat):
    n = 0
    for i in range(len(mat)):
        if mat[0, i] != 0:
            n = i
            break
    tmp = np.copy(mat[:, 0])
    mat[:, 0] = mat[:, n]
    mat[:, n] = tmp

def f(mat):
    if mat[0, 0] == 0:
        if Swap(mat)== 0: return 0
    for i in range(1, len(mat)):
        for j in range(1, len(mat[:, 1])):
            mat[i, j] = mat[i, j] - mat[i, 0] * mat[0, j] / mat[0, 0]
    mat = np.delete(mat, 0, axis=1)
    mat = np.delete(mat, 0, axis=0)
    return mat

def Determinant(mat):
    help = np.copy(mat)
    if help[0, 0] == 0:
        if np.allclose(help[0, :], np.zeros(len(help[0]))): return 0
        Swap(help)
    det = help[0, 0]
    for i in range(len(mat) - 1):
        if np.allclose(help[0, :], np.zeros(len(help[0]))): return 0
        help = f(help)
        det *= help[0, 0]
    if det == -0: 
        det = 0
    return det

#Задание №1
def Lab1():
    print("Введите 1 матрицу:")
    s = input()
    matrix1 = GetMatrix(s)
    print("Введите 2 матрицу:")
    s = input()
    matrix2 = GetMatrix(s)
    print("Первая матрица")
    PrintMatrix(matrix1)
    print("Вторая матрица")
    PrintMatrix(matrix2)
    print("Произведение матриц")
    PrintMatrix(Multiplication(matrix1, matrix2))

#Задание №2
def Lab2():
    print("Введите матрицу:")
    s = input()
    matrix = GetMatrix(s)
    print("Mатрица")
    PrintMatrix(matrix)
    if Determinant(matrix) == 0:
        print("Обратной матрицы не существует")
    else:
        print("Обратная матрица:")
        PrintMatrix(Reverse(matrix))

#Задание №3
def Lab3():
    print("Введите матрицу:")
    s = input()
    matrix = GetMatrix(s)
    print("Mатрица")
    PrintMatrix(matrix)
    print("Определитель матрицы = ", round(Determinant(matrix), 3))

#Меню
while True:
    print("\nВведите номер задания:")
    a = input()
    if a == '1': Lab1()
    elif a == '2': Lab2()
    elif a == '3': Lab3()
    else: print("Неверный номер. Повторите попытку, либо введите '0' для выхода из программы.")
'''
1
2 0; 1 0          1 -3; 4 -2
0 1; 1 0          0 1; 1 0
0 -1 2; 1 0 3; 4 -3 -2            1 0 0; 0 1 0; 0 0 1 
7 7 3; -2 -4 1; 1 7 -7            2 -8 -4; -4 2 1; -5 -6 -1

2
1 -2; 3 -2
1 0 0; 0 1 0; 0 0 1
1 6 -4; -8 6 7; -7 0 8
1 2 3; 4 5 6; 7 8 9

3
1 6 -4; -8 6 7; -7 0 8
-4.6 -1.71 -3.06; 2.66 -3.52 0.22; -0.79 -1.9 -4.04
1 0 0; 0 1 0; 0 0 1
1 2 3; 4 5 6; 7 8 9
'''