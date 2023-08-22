import numpy

A1 = [
[-7, -9, 1, -9],
[-6, -8, -5, 2],
[-3, 6, 5, -9],
[-2, 0, -5, -9]
]
B1 = [29, 42, 11, 75]

A2 = [
[8, -4, 0, 0, 0],
[-2, 12, -7, 0, 0],
[0, 2, -9, 1, 0],
[0, 0, -8, 17, -4],
[0, 0, 0, -7, 13]
]
B2 = [32, 15, -10, 133, -76]

A3 = [
[12, -3, -1, 3],
[5, 20, 9, 1],
[6, -3, -21, -7],
[8, -7, 3, -27]
]
B3 = [-31, 90, 119, 71]

#Перемена местами двух строк системы
def Swap(A, B, row1, row2):
    A[row1], A[row2] = A[row2], A[row1]
    B[row1], B[row2] = B[row2], B[row1]

#Деление строки системы на число
def Divide(A, B, row, divider):
    A[row] = [a / divider for a in A[row]]
    B[row] /= divider
 
#Сложение строки системы с другой строкой, умноженной на число
def Combine(A, B, row, source, weight):
    A [row] = [(a + k * weight) for a, k in zip(A[row], A[source])]
    B[row] += B[source] * weight

#Метод Гаусса
def Gauss(A, B):
    column = 0
    while (column < len(B)):
        curr = None
        for r in range(column, len(A)):
            if curr is None or abs(A[r][column]) > abs(A[curr][column]):
                curr = r
        if curr is None:
            print("Решений нет")
            return None
        if curr != column:
            Swap(A, B, curr, column)
        Divide(A, B, column, A[column][column])
        for r in range(column + 1, len(A)):
            Combine(A, B, r, column, -A[r][column])
        column += 1
    X = [0 for b in B]
    for i in range(len(B) - 1, -1, -1):
        X[i] = B[i] - sum(x * a for x, a in zip(X[(i + 1):], A[i][(i+ 1):]))
    print("Ответ:")
    print("\n".join("X{0} =\t{1:10.2f}".format(i + 1, j) for i, j in enumerate(X)))

#Проверка на корректность
def Chek(a):
    n = len(a)
    for str in range(0, n):
        if( len(a[str]) != n ):
            print('Не соответствует размерность')
            return False
    if (abs(a[0][0]) < abs(a[0][1]))or(abs(a[n - 1][n - 1]) < abs(a[n - 1][n - 2])):
        print('Не выполнены условия достаточности')
        return False
    for stroka in range(0, len(a)):
        if( a[str][str] == 0 ):
            print('Нулевые элементы на главной диагонали')
            return False
    return True
 
#Метод прогонки
def Progon(A, B):
    if (not Chek(A)):
        print('Ошибка в исходных данных')
        return 0 
    n = len(A)
    x = [0 for k in range(0, n)]    
    a = [0 for k in range(0, n)]
    b = [0 for k in range(0, n)]
    a[0] = A[0][1] / (-A[0][0]) 
    b[0] = (- B[0]) / (-A[0][0]) 
    for i in range(1, n - 1):
        a[i] = A[i][i+1] / ( -A[i][i] - A[i][i-1]*a[i-1] )
        b[i] = ( A[i][i-1]*b[i-1] - B[i] ) / ( -A[i][i] - A[i][i-1]*a[i-1] )
    a[n-1] = 0
    b[n-1] = (A[n-1][n-2]*b[n-2] - B[n-1]) / (-A[n-1][n-1] - A[n-1][n-2]*a[n-2])
    x[n-1] = b[n-1] 
    for i in range(n-1, 0, -1):
        x[i-1] = a[i-1] * x[i] + b[i-1] 
    print("Ответ:")
    print("\n".join("X{0} =\t{1:10.2f}".format(i + 1, j) for i, j in enumerate(x)))

#Нормирование
def Norm(a):
    x = 0
    for i in range(0, len(a)):
        x += abs(a[i])
    if x > 0:
        return x
    return 0

#Метод простых итераций
def Iteration(A, B):
    a0 = [[0 for j in range(len(A[0]))] for i in range(len(A))]
    b0 = [0 for i in range(len(B))]
    for i in range(len(A)):
        for j in range(len(A[0])):
            if i == j:
                a0[i][j] = 0
            else:
                a0[i][j] = -A[i][j]/A[i][i]
        b0[i] = B[i]/A[i][i]
    x1 = b0
    x2 = list(numpy.array(list(numpy.dot(numpy.array(a0), numpy.array(x1)))) + numpy.array(b0))
    k = 0 
    while Norm(list(numpy.array(x1) - numpy.array(x2))) > 0.01:
        x1 = x2
        x2 = list(numpy.array(list(numpy.dot(numpy.array(a0), numpy.array(x1)))) + numpy.array(b0))
        k += 1
    for i in range(0, len(x1)):
        x1[i] = round(x1[i], 4)
    print("Ответ:")
    print("\n".join("X{0} =\t{1:10.2f}".format(i + 1, j) for i, j in enumerate(x1)))

#Метод Зейделя
def Seidel(A, b):
    eps = 0.01
    n = len(A)
    x = numpy.zeros(n)
    iter = 0
    converge = False
    while not converge:
        x_new = numpy.copy(x)
        for i in range(n):
            s1 = sum(A[i][j] * x_new[j] for j in range(i))
            s2 = sum(A[i][j] * x[j] for j in range(i + 1, n))
            x_new[i] = (b[i] - s1 - s2) / A[i][i]
        converge = numpy.sqrt(sum((x_new[i] - x[i]) ** 2 for i in range(n))) <= eps
        x = x_new
    print("Ответ:")
    print("\n".join("X{0} =\t{1:10.2f}".format(i + 1, j) for i, j in enumerate(x)))

#Меню
while True:
    print("\nЗадания:")
    print("1. Метод Гаусса")
    print("2. Метод прогонки")
    print("3. Метод простых итерация")
    print("2. Метод Зейделя")
    print("\nВведите номер задания:")
    a = input()
    if a == '1': Gauss(A1, B1)
    elif a == '2': Progon(A2, B2)
    elif a == '3': Iteration(A3, B3)
    elif a == '4': Seidel(A3, B3)
    elif a == '0': break
    else: print("Неверный номер. Повторите попытку, либо введите '0' для выхода из программы.")


