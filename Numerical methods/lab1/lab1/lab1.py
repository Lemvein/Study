import numpy 

#Входные данные
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

A4 = [
[4, 7, -1],
[7, -9, -6],
[-1, -6, -4]
]

A5 = [
[-5, -8, 4],
[4, 2, 6],
[-2, 5, -6]
]

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
 
#Метод прогонки!!!!!
def Progon(A, B):
    if (not Chek(A)):
        print('Ошибка в исходных данных')
        return 0 
    n = len(A)
    x = [0 for k in range(0, n)]   
    #Прогоночные коэффициенты  
    a = [0 for k in range(0, n)]
    b = [0 for k in range(0, n)]
    a[0] = -A[0][1] / A[0][0] 
    b[0] = B[0] / A[0][0] 
    for i in range(1, n - 1):
        a[i] = -A[i][i+1] / ( A[i][i] + A[i][i-1]*a[i-1] )
        b[i] = (B[i] - A[i][i-1]*b[i-1] ) / ( A[i][i] + A[i][i-1]*a[i-1] )
    a[n-1] = 0
    b[n-1] = (B[n-1] - A[n-1][n-2]*b[n-2]) / (A[n-1][n-1] + A[n-1][n-2]*a[n-2])
    #Обратный ход
    x[n-1] = b[n-1] 
    for i in range(n-1, 0, -1):
        x[i-1] = a[i-1] * x[i] + b[i-1] 
    print("Ответ:")
    print("\n".join("X{0} =\t{1:10.2f}".format(i + 1, j) for i, j in enumerate(x)))

#Метод простых итераций
def Iteration(A, B, eps):
    flag = False
    #Приведение СЛАУ к эквивалентному виду
    a = [[0 for j in range(len(A[0]))] for i in range(len(A))]
    b = [0 for i in range(len(B))]
    for i in range(len(A)):
        for j in range(len(A[0])):
            if i == j:
                a[i][j] = 0
            else:
                a[i][j] = -A[i][j]/A[i][i]
            if numpy.linalg.norm(a) < 1:    #Достаточное условие сходимости
                flag = True
        b[i] = B[i]/A[i][i]
    if flag == False:
        print('Достаточное условие сходимости не выполнено')
        return

    x1 = b
    x2 = list(numpy.array(list(numpy.dot(numpy.array(a), numpy.array(x1)))) + numpy.array(b))
    k = 0 
    while numpy.linalg.norm(list(numpy.array(x1) - numpy.array(x2))) > eps:     #Условие выхода
        x1 = x2
        x2 = list(numpy.array(list(numpy.dot(numpy.array(a), numpy.array(x1)))) + numpy.array(b))
        k += 1
    print("Ответ:")
    print("\n".join("X{0} =\t{1:10.2f}".format(i + 1, j) for i, j in enumerate(x1)))
    print("Колличество итераций: ", k)

#Метод Зейделя
def Seidel(A, B, eps):
    n = len(A)
    x = numpy.zeros(n)
    iter = 0
    converge = False
    k = 0
    while not converge:
        x_new = numpy.copy(x)
        for i in range(n):
            s1 = sum(A[i][j] * x_new[j] for j in range(i))
            s2 = sum(A[i][j] * x[j] for j in range(i + 1, n))
            x_new[i] = (B[i] - s1 - s2) / A[i][i]
        converge = numpy.linalg.norm(list(numpy.array(x_new) - numpy.array(x))) <= eps
        x = x_new
        k += 1
    print("Ответ:")
    print("\n".join("X{0} =\t{1:10.2f}".format(i + 1, j) for i, j in enumerate(x)))
    print("Колличество итераций: ", k)

#Метод вращений
def Rotation(A):
    n = len(A)
    for i in range(n-1):
        for j in range(i + 1, n):
            c = A[i][i] / (A[i][i]**2 + A[j][i]**2)**.5
            s = A[j][i] / (A[i][i]**2 + A[j][i]**2)**.5
            tmp1 = A[i][i] * c + A[j][j] * s
            tmp2 = A[i][i] * -s + A[j][j] * c
            A[i][i] = tmp1
            A[j][j] = tmp2
    if numpy.any(numpy.diag(A) == 0):
        print('Система имеет бесконечное количество решений.')
        return
    x = numpy.matrix([0.0 for i in range(n)]).T

    #Обратный ход
    for i in range(n - 1, -1, -1):
        x[i][0] = (A[i][-1] - A[i][i] * x[i][0]) / A[i][i]
        print("Ответ:")
        print(x[i][0])

#Алгоритм QR-разложения
def QR(A, eps):
    res = Vector()
    i = 0
    A_i = Matrix(A)
    while i < len(A):
        eigenval = get_eigenvalue(A_i, eps, i)
        if eigenval[1]:
            res.extend(eigenval[0])
            i += 2
        else:
            res.append(eigenval[0])
            i += 1
        A_i = eigenval[2]
    return res

#Меню
while True:
    print("\nЗадания:")
    print("1. Алгоритм LU-разложения")
    print("2. Метод прогонки")
    print("3. Метод простых итерация")
    print("4. Метод Зейделя")
    print("5. Метод вращений")
    print("6. Алгоритм QR-разложения")
    print("\nВведите номер задания:")
    a = input()
    if a == '1': LU(A1, B1)
    elif a == '2': Progon(A2, B2)
    elif a == '3': Iteration(A3, B3, 0.001)
    elif a == '4': Seidel(A3, B3, 0.001)
    elif a == '5': Rotation(A4)
    elif a == '6': QR(A5, 0.001)
    elif a == '0': break
    else: print("Неверный номер. Повторите попытку, либо введите '0' для выхода из программы.")