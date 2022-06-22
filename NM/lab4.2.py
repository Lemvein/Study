import numpy as np
import math
import matplotlib.pyplot as plt
import RK
import Progon

#Точное решение
def res(x):
    return - np.tan(x)

#Замена z=y'
def f_xyz(x, y, z):
    return 2 * (1 + (np.tan(x)) ** 2) * y

def accuracy(x, y):
    norm = 0.0
    for i in range(len(x)):
        norm += (y[i] - res(x[i]))**2
    return norm**0.5

#Поиск узлов
def find_node_points(a, b, h):
    return list(np.arange(a, b + h/2, h))


def runge_romberg_richardson(y1, y2, p):
    norm = 0.0
    for i in range(len(y2)):
        norm += (y1[i*2] - y2[i])**2
    return (norm**0.5) / (2**p - 1)


def next_iter(Fi0, Fi1, n0, n):
    return n - Fi1 * (n - n0) / (Fi1 - Fi0) #ф 4.34

def shooting_method(x, y0, y1, h, f = f_xyz, e = 0.00001):
    n0 = 1
    n = 0.8
    y_prev = RK.runge_kutta_method(x, y0, n0, h, f)
    y_i = RK.runge_kutta_method(x, y0, n, h, f)
    Fi_prev = y_prev[-1] - y1 #нахождение корня уравнения Ф
    Fi_i = y_i[-1] - y1
    while abs(Fi_i) > e:
        n0, n = n, next_iter(Fi_prev, Fi_i, n0, n)
        y_prev, y_i = y_i, RK.runge_kutta_method(x, y0, n, h, f)
        Fi_prev, Fi_i = Fi_i, y_i[-1] - y1
    for i in range (len(x)):
        print('x: ', round(x[i],2), 'y: ', y_i[i])
    return y_i

def p(x):
    return 0

def q(x):
    return -2 * (1 + (np.tan(x)) ** 2)

def f(x):
    return 0

#Cистема лин алгебр уравнений с трехдиагон матрицей
def find_tridig_A(h, p, q, x):
    A = [[1 - (p(x[i]))/2, (-2 + h*h*q(x[i])), 1 + (p(x[i])*h)/2] for i in range(1, len(x[:-1]))]
    A[0][0] = 0
    A[-1][-1] = 0
    return A

def find_b(h, p, f, x, y0, y1):
    b = [h*h*f(x[i]) for i in range(1, len(x[:-1]))]
    b[0] -= y0*(1 - p(x[1])*h/2) #1
    b[-1] -= y1*(1 + p(x[-2])*h/2) #последний
    return b

def finite_differences_method(x, y0, y1, h, p = p, q = q, f = f):
    A = find_tridig_A(h, p, q, x)
    b = find_b(h, p, f, x, y0, y1)
    y = [y0] + Progon.tridig_matrix_alg(A, b) + [y1]
    for i in range (len(x)):
        print('x: ', round(x[i],2), 'y: ', y[i])
    return y

h = 0.1
a = 0
b = np.pi / 6
y0 = 0
y1 = - math.sqrt(3) / 3

x = find_node_points(a, b, h)
print("Метод стрельбы")
y = shooting_method(x, y0, y1, h)
p = 4
x_ = find_node_points(a, b, h/2)
print()
y_ = shooting_method(x_, y0, y1, h/2)
print()
print('Погрешность решения относительно точного')
print(accuracy(x, y))
print('Погрешность с помощью метода Рунге-Ромберга-Ричардсона')
print(runge_romberg_richardson(y_, y, p))
print()

print("Конечно-разностный метод")
yd = finite_differences_method(x, y0, y1, h)
print()
yd_ = finite_differences_method(x_, y0, y1, h/2)
print('Погрешность решения относительно точного')
print(accuracy(x, yd))
p = 2
print('Погрешность с помощью метода Рунге-Ромберга-Ричардсона')
print(runge_romberg_richardson(yd_, yd, p))
x1, y1 = x, yd

solution = []
for i in x:
    solution.append(res(i))

print()
method = 10
while method != 0:
    print("Выберите рисунок: ")
    print("1. Метод стрельбы 2. Конечно-разностный метод")
    method = int(input())
    fig = plt.figure(figsize=(10, 5))
    plt.xlabel('x')
    plt.ylabel('y')
    if (method == 1):
        plt.plot(x, y,'r', linewidth = 2, label = 'метод стельбы')
        plt.plot(x, solution,'grey', linestyle = '--', label = 'решение')
        plt.grid()
        plt.legend(loc='upper center')
        plt.show()
    if (method == 2):
        plt.plot(x1, y1,'g', label = 'конечно-разностный метод')
        plt.plot(x, solution,'b', linestyle = '--', label = 'решение')
        plt.grid()
        plt.legend(loc='upper center')
        plt.show()