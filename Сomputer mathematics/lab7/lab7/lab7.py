import matplotlib.pyplot as plt
import pylab
import math

def f(x,y):
    return (y**2+(x**2)*y)/(x**3)

def F(x):
    return (x**2)/(1+x)

def g(x, y, z):
    return (1/(x**(1/2)))*z-(1/(4*x**2))*(x+x**(1/2)-8)*y

def g1(x, y, z):
    return z

def G(x):
    return (x**2+1/x)*math.e**(x**(1/2))

def Euler2(x0, y0, x, h):
    y = y0
    result = []
    result.append(y)
    while (x0<x):
        y += h * f(x0, y) 
        x0 += h
        result.append(y)
    return result

def Euler3(x0, y0, z0, x, h):
    m = int((x - x0)/h)
    result = []
    result.append(y0)
    for _ in range(1, m+1):
        z0 += h*g(x0, y0, z0)
        y0 += h*g1(x0, y0, z0)
        x0 += h
        result.append(y0)
    return result

def Euler_mod2(x0, y0, x, h):
    y = y0
    result = []
    result.append(y)
    while (x0<x):
        y += (1/2)*h*(f(x0, y) + f(x0+h, y + h*f(x0, y)))
        x0 += h
        result.append(y)
    return result

def Euler_mod3(x0, y0, z0, x, h):
    m = int((x - x0)/h)
    result = []
    result2 = [z0]
    result.append(y0)
    for _ in range(1, m+1):
        y0 += (1/2)*h*(g1(x0, y0, z0) + g1(x0+h, y0+h*g1(x0, y0, z0), z0+h*g(x0, y0, z0)))
        z0 += (1/2)*h*(g(x0, y0, z0) + g(x0+h, y0+h*g1(x0, y0, z0), z0+h*g(x0, y0, z0)))
        x0 += h
        result.append(y0)
        result2.append(z0)
    return result

def Runge_Kutta2(x0, y0, x, h):
    y = y0
    result = []
    result.append(y)
    while (x0<x):
        k1 = f(x0, y)
        k2 = f(x0 + h/2, y + h/2 * k1)
        k3 = f(x0 + h/2, y + h/2 * k2)
        k4 = f(x0 + h, y + h * k3)
        y += (1/6)*h*(k1 + 2*k2 + 2*k3 +k4)
        result.append(y)
        x0 += h
    return result

def Runge_Kutta3(x0, y0, z0, x, h):
    m = int((x - x0)/h)
    result = []
    result.append(y0)
    result2 = [z0]
    for _ in range(1, m+1):
        k1 = h*g(x0, y0, z0)
        l1 = h*g1(x0, y0, z0)
        k2 = h*g(x0 + h/2, y0 + l1/2, z0 + k1/2)
        l2 = h*g1(x0 + h/2, y0 + l1/2, z0 + k1/2)
        k3 = h*g(x0 + h/2, y0 + l2/2, z0 + k2/2)
        l3 = h*g1(x0 + h/2, y0 + l2/2, z0 + k2/2)
        k4 = h*g(x0 + h, y0 + l3, z0 + k3)
        l4 = h*g1(x0 + h, y0 + l3, z0 + k3)
        z0 += (k1 + 2*k2 + 2*k3 + k4)/6
        y0 += (l1 + 2*l2 + 2*l3 + l4)/6
        x0 += h
        result.append(y0)
        result2.append(z0)
    return [result, result2]

def Adams2(x0, y0, x, h):
    m = int(((x-x0)/h))
    y = y0
    result = []
    result.append(y)
    rk = Runge_Kutta2(x0, y0, x, h)
    #первые 3 узла любым другим
    for i in range(1, 4):
        x0 += h
        result.append(rk[i])
    y = rk[3]
    for i in range(4, m+1): 
        y += h*(55*f(x0-h, result[i-1]) - 59*f(x0-2*h, result[i-2]) + 37*f(x0-3*h, result[i-3]) - 9*f(x0-4*h, result[i-4]))/24
        x0 += h
        result.append(y)
    return result

def Adams3(x0, y0, z0, x, h):
    m = int((x - x0)/h)
    y = y0
    z = z0
    result = []
    result.append(y0)
    result2 = [z0]
    runge_kutt = Runge_Kutta3(x0, y0, z0, x, h)
    for i in range(1, 4):
        x0 += h
        result.append(runge_kutt[0][i])
        result2.append(runge_kutt[1][i])
    y = runge_kutt[0][3]
    z = runge_kutt[1][3]
    for i in range(4, m+1):
        z += h*(55*g(x0 - h, result[i-1], result2[i-1]) - 59*g(x0-2*h, result[i-2], result2[i-2]) + 37*g(x0-3*h, result[i-3], result2[i-3]) - 9*g(x0-4*h, result[i-4], result2[i-4]))/24
        y += h*(55*g1(x0 - h, result[i-1], result2[i-1]) - 59*g1(x0-2*h, result[i-2], result2[i-2]) + 37*g1(x0-3*h, result[i-3], result2[i-3]) - 9*g1(x0-4*h, result[i-4], result2[i-4]))/24
        x0 += h
        result2.append(z)
        result.append(y)
    return result

def Lab1():
    h = 0.1
    a, b = 1, 2
    y1 = 0.5
    x1 = []
    x1.append(a)
    while a < b:
        a += h
        x1.append(a)
    a, b = 1, 2
    t = []
    for i in x1:
        t.append(F(i))
    print("\n1. Метод Эйлера")
    print("2. Модифицированный метод Эйлера")
    print("3. Метод Рунге-Кутта")
    print("4. Метод Адамса")
    print("\nВведите номер метода:")
    m = input()
    fig, ax = plt.subplots()
    ax.plot(x1, t, 'tab:blue', label='Точное решение')
    if m == '1': ax.plot(x1, Euler2(a, y1, b, h), 'tab:red', label='Метод Эйлера')
    elif m == '2': ax.plot(x1, Euler_mod2(a, y1, b, h), 'tab:red', label='Модифицированный метод Эйлера')
    elif m == '3': ax.plot(x1, Runge_Kutta2(a, y1, b, h), 'tab:red', label='Метод Рунге-Кутта')
    elif m == '4': ax.plot(x1, Adams2(a, y1, b, h), 'tab:red', label='Метод Адамса')
    ax.legend()
    plt.xlim([1, 2])
    plt.show()

def Lab2():
    h = 0.1
    a, b = 1, 2
    y1 = 2*math.e
    z1 = 2*math.e
    x1 = []
    x1.append(a)
    while a < b:
        a += h
        x1.append(a)
    a, b = 1, 2
    t = []
    for i in x1:
        t.append(G(i))
    print("\n1. Метод Эйлера")
    print("2. Модифицированный метод Эйлера")
    print("3. Метод Рунге-Кутта")
    print("4. Метод Адамса")
    print("\nВведите номер метода:")
    m = input()
    fig, ax = plt.subplots()
    ax.plot(x1, t, 'tab:blue', label='Точное решение')
    if m == '1': ax.plot(x1, Euler3(a, y1, z1, b, h), 'tab:red', label='Метод Эйлера')
    elif m == '2': ax.plot(x1, Euler_mod3(a, y1, z1, b, h), 'tab:red', label='Модифицированный метод Эйлера')
    elif m == '3': ax.plot(x1, Runge_Kutta3(a, y1, z1, b, h)[0], 'tab:red', label='Метод Рунге-Кутта')
    elif m == '4': ax.plot(x1, Adams3(a, y1, z1, b, h), 'tab:red', label='Метод Адамса')
    ax.legend()
    plt.xlim([1, 2])
    plt.show()

#Меню
while True:
    print("\nВведите номер задания:")
    a = input()
    if a == '1': Lab1()
    elif a == '2': Lab2()
    elif a == '0': break
    else: print("Неверный номер. Повторите попытку, либо введите '0' для выхода из программы.")