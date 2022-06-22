import numpy 

#Входные данные
A3 = [
[12, -3, -1, 3],
[5, 20, 9, 1],
[6, -3, -21, -7],
[8, -7, 3, -27]
]
B3 = [-31, 90, 119, 71]

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

print("Введите точность вычислений:")
eps = float(input())
print("Метод простых итераций:")
Iteration(A3, B3, eps)
print("Метод Зейделя:")
Seidel(A3, B3, eps)
