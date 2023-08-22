import matplotlib.pyplot as plt
import pylab
import math

#Нахождениие производной по таблице разностей (с.564)
def Derivative(vec, h):
    res = 0
    for i in range(len(vec)):
        res += ((-1) ** (i % 2)) * vec[i] / (i + 1) 
    return res / h

#Нахождение производной по заданной сеточной функции
def diff(y, x):
    # таблица разностей
    delta = [y]
    delta_count = 5
    # строим таблицу разностей функции
    for i in range(delta_count):
        # добавляем в таблицу разность с номером i
        delta.append([delta[i][j + 1] - delta[i][j] for j in range(len(delta[i]) - 1)])
    delta.pop(0)
    # находим производную в каждой точке 
    dy = []
    for i in range(len(x) - 1):
        h = x[i + 1] - x[i]
        # Столбец таблицы разностей (для конкретного значения x)
        col = []
        for row in range(len(delta)):
            if len(delta[row]) - 1 < i:
                break
            col.append(delta[row][i])
        dy_i = Derivative(col, h)
        dy.append(dy_i)
    return dy

# Построение графика функции и ее производной
def Graph(y, interval, step):
    x = pylab.arange(interval[0], interval[1] + step, step)

    # Вид окна
    fig, ax = plt.subplots()
    # график функции
    ax.plot(x, y, 'g', label='заданная функция')

    dy = diff(y, x)
    x = pylab.arange(interval[0], interval[1], step)
    # график произвожной
    ax.plot(x, dy, 'r', label='производная этой функции')

    ax.legend(loc=2)
    ax.set_xlabel('x')
    ax.set_ylabel('y')
    plt.show()

def Simpson(f, interval, h):    #c.590
    x = pylab.arange(interval[0], interval[1], h)
    y = [f(t) for t in x]
    ydx = y[0] + y[len(y) - 1]
    for i in range(1,len(y)):
        if i%2==1:
            ydx += 4 * y[i]
    for i in range(2,len(y)):
        if i % 2 == 0:
            ydx += 2 * y[i]
    ydx = ydx * h / 3
    return ydx

#Задание №1
def Lab1():
    print("Введите коэффициенты через запятую:")
    f = [float(number) for number in input().split(', ')]
    print("Введите интервал:")
    interval = [float(number) for number in input().split()]
    print("Введите шаг:")
    step = float(input())
    Graph(f, interval, step)

#Задание №2
def Lab2():
    print("Введите функцию: ")
    fin = input()
    f = eval(fin)
    print("Введите интервал: ")
    x = [float(number) for number in input().split()]
    print("Введите шаг: ")
    step = float(input())
    print('Результат интегрирования: ', Simpson(f, x, step))

#Меню
while True:
    print("\nВведите номер задания:")
    a = input()
    if a == '1': Lab1()
    elif a == '2': Lab2()
    elif a == '0': break
    else: print("Неверный номер. Повторите попытку, либо введите '0' для выхода из программы.")

'''
1
-0.008, -0.066, -0.209, -0.439, -0.734, -1.044, -1.31, -1.484, -1.542, -1.491, -1.366, -1.218, -1.093, -1.02, -0.998, -1, -0.982, -0.901, -0.733, -0.479, -0.174, 0.128, 0.372, 0.513, 0.537, 0.46, 0.323, 0.177, 0.065, 0.009, -0.002
-5, -4.597, -4.178, -3.727, -3.234, -2.688, -2.082, -1.411, -0.674, 0.131, 1, 1.929, 2.91, 3.935, 4.99, 6.063, 7.134, 8.187, 9.198, 10.145, 11
0, 1.483, 2.739, 3.782, 4.647, 5.437, 6.363, 7.723, 9.749, 12.314, 14.659, 15.538, 14.109, 11.222, 9.578, 11.331, 14.75, 15.096, 11.393, 9.66, 13.403
-4.045, 1.782, 2.279, 2.398, 2.322, 2.109, 1.784, 1.355, 0.83, 0.281, 0, 0.281, 0.83, 1.355, 1.784, 2.109, 2.322, 2.398, 2.279, 1.782, -4.045

2
lambda x: (math.sin(x)) ** 3 - (math.cos(x/2)) ** 2 + 2
lambda x: -x ** 4 + 3 * x ** 3 + 4 * x - 5
lambda x: (math.sin(x)) ** 3 - 3 * math.sin(x ** 2) + 4 * math.sin(x) + 4 * x
lambda x: math.log(10 * math.sin((3 * x/5) ** 2) + 1)
'''