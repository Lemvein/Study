import matplotlib.pyplot as plt
import pylab
import math

def g(x, y, z):
    return (1/(x**(1/2)))*z-(1/(4*x**2))*(x+x**(1/2)-8)*y

def g1(x, y, z):
    return z

def G(x):
    return (x**2+1/x)*math.e**(x**(1/2))

def Runge_Romberg(y1, y2, p):
    norm = 0
    for i in range(len(y2)):
        norm += (y1[i*2] - y2[i])**2
    return (norm**0.5) / (2**p - 1)

def Accuracy(x, ans, y):
    norm = 0
    for i in range(len(x)):
        norm += (ans[i] - y(x[i]))**2
    return norm**0.5

def Euler(x0, y0, z0, x, h):
    m = int((x - x0)/h)
    result = []
    result.append(y0)
    result2 = [z0]
    for _ in range(1, m+1):
        z0 += h*g(x0, y0, z0)
        y0 += h*g1(x0, y0, z0)
        x0 += h
        result.append(y0)
        result2.append(z0)
    return [result, result2]

def Runge_Kutta(x0, y0, z0, x, h):
    m = int((x - x0)/h)
    x_ = []
    x_.append(x0)
    result = []
    result.append(y0)
    result2 = [z0]
    for _ in range(1, m+1):
        k1 = h * g(x0, y0, z0)
        l1 = h * g1(x0, y0, z0)

        k2 = h * g(x0 + h/2, y0 + l1/2, z0 + k1/2)
        l2 = h * g1(x0 + h/2, y0 + l1/2, z0 + k1/2)

        k3 = h * g(x0 + h/2, y0 + l2/2, z0 + k2/2)
        l3 = h * g1(x0 + h/2, y0 + l2/2, z0 + k2/2)

        k4 = h * g(x0 + h, y0 + l3, z0 + k3)
        l4 = h * g1(x0 + h, y0 + l3, z0 + k3)

        z0 += (k1 + 2*k2 + 2*k3 + k4)/6
        y0 += (l1 + 2*l2 + 2*l3 + l4)/6

        if abs(k1-k2) > 0:
            teta = abs((k2 - k3) / (k1 - k2))
        if teta > 1/10: h = h - 0.00001
        if teta < 1/100: h = h + 0.00001
        x0 += h

        x_.append(x0)
        result.append(y0)
        result2.append(z0)
    return [x_, result, result2]

def Adams(x, y, z, h):
    n = len(x)
    x = x[:4]
    x1 = x[:5]
    y = y[:4]
    y1 = y[:5]
    z = z[:4]
    z1 = z[:5]
    #этап предиктор
    for i in range(3, n - 1):
        z_i = z[i] + h * (55 * g(x[i], y[i], z[i]) -
                          59 * g(x[i - 1], y[i - 1], z[i - 1]) +
                          37 * g(x[i - 2], y[i - 2], z[i - 2]) -
                           9 * g(x[i - 3], y[i - 3], z[i - 3])) / 24
        z1.append(z_i)
        y_i = y[i] + h * (55 * g1(x[i], y[i], z[i]) -
                          59 * g1(x[i - 1], y[i - 1], z[i - 1]) +
                          37 * g1(x[i - 2], y[i - 2], z[i - 2]) -
                           9 * g1(x[i - 3], y[i - 3], z[i - 3])) / 24
        y1.append(y_i)
        x1.append(x[i] + h)
        #этап корректор
        z_i = z[i] + h / 24 * (9 * g(x1[i+1], y1[i+1], z1[i+1]) +
                         19 * g(x1[i], y1[i], z[i]) -
                         5 * g(x1[i - 1], y1[i - 1], z1[i - 1]) +
                         1 * g(x1[i - 2], y1[i - 2], z1[i - 2]))
        z.append(z_i)
        y_i = y[i] + h / 24 * (9 * g1(x1[i+1], y1[i+1], z1[i+1]) +
                          19 * g1(x1[i], y1[i], z1[i]) -
                          5 * g1(x1[i - 1], y1[i - 1], z1[i - 1]) +
                          1 * g1(x1[i - 2], y1[i - 2], z1[i - 2]))
        y.append(y_i)
        x.append(x[i] + h)
    return [y, z]

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
print("2. Метод Рунге-Кутта")
print("3. Метод Адамса")
print("\nВведите номер метода:")
m = input()
fig, ax = plt.subplots()
ax.plot(x1, t, 'tab:blue', label='Точное решение')
if m == '1': 
    [ans, dans] = Euler(a, y1, z1, b, h)
    [ans_, dans_] = Euler(a, y1, z1, b, h/2)
    print('Точное решение в узлах сетки:\n')
    for i in x1:
        print(G(i))
    print('\nРешение методом Эйлера в узлах сетки:')
    for i in range(len(x1)):
        print(ans[i])
    print('\nПогрешность решения (относительно точного решения):', Accuracy(x1, ans, G))
    print('\nПогрешность решения (Рунге-Ромберг):', Runge_Romberg(ans_, ans, 2))
    ax.plot(x1, Euler(a, y1, z1, b, h)[0], 'tab:red', label='Метод Эйлера', linestyle = "--")
elif m == '2': 
    [xa, ans, dans] = Runge_Kutta(a, y1, z1, b, h)
    [xa, ans_, dans_] = Runge_Kutta(a, y1, z1, b, h/2)
    print('Точное решение в узлах сетки:\n')
    for i in x1:
        print(G(i))
    print('\nРешение методом Рунге-Кутта в узлах сетки:')
    for i in range(len(x1)):
        print(ans[i])
    print('\nПогрешность решения (относительно точного решения):', Accuracy(x1, ans, G))
    print('\nПогрешность решения (Рунге-Ромберг):', Runge_Romberg(ans_, ans, 2))    
    ax.plot(x1, Runge_Kutta(a, y1, z1, b, h)[1], 'tab:red', label='Метод Рунге-Кутта', linestyle = "--")
elif m == '3':   
    runge_x, runge_y, runge_z = Runge_Kutta(a, y1, z1, b, h)
    runge_x_, runge_y_, runge_z_ = Runge_Kutta(a, y1, z1, b, h/2)
    [ans, dans] = Adams(runge_x, runge_y, runge_z, h)
    [ans_, dans_] = Adams(runge_x_, runge_y_, runge_z_, h/2)
    print('Точное решение в узлах сетки:\n')
    for i in x1:
        print(G(i))
    print('\nРешение методом Адамса в узлах сетки:')
    for i in range(len(x1)):
        print(ans[i])
    print('\nПогрешность решения (относительно точного решения):', Accuracy(x1, ans, G))
    print('\nПогрешность решения (Рунге-Ромберг):', Runge_Romberg(ans_, ans, 2))       
    ax.plot(x1, Adams(runge_x, runge_y, runge_z, h)[0], 'tab:red', label='Метод Адамса', linestyle = "--")
ax.legend()
plt.xlim([1, 2])
plt.show()
