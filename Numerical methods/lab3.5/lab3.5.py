import numpy as np

def f(x):
    return x / (x ** 2 + 9)

x0 = 0
xk = 2
h1 = 0.5
h2 = 0.25

#Метод Рунге-Ромберга 
def Runge_Romberg(F1, F2, h1, h2, p):
    if h1 < h2:
        return F1 + (F1 - F2) / ((h2 / h1) ** p - 1)
    return F2 + (F2 - F1) / ((h1 / h2) ** p - 1)

#Метод прямоугольников
def Rectangle(f, h, x0, xk):
    x = np.arange(x0, xk, h)
    return h * sum(f((x[i-1] + x[i]) / 2) for i in range(1, len(x)))

#Метод трапеций
def Trapezoidal(f, h, x0, xk):
    x = np.arange(x0, xk, h)
    return h * ((f(x[0]) + f(x[len(x) - 1])) / 2 + sum(f(x[i]) for i in range(1, len(x) - 1)))

#Метод Симпсона
def Simpson(f, h, x0, xk):
    x = np.arange(x0, xk, h)
    return (h / 3) * (f(x[0]) + f(x[len(x) - 1]) + sum(4 * f(x[i]) for i in range(1, len(x)-1, 2)) + sum(2 * f(x[i]) for i in range(2, len(x)-1, 2)))

#Вывод
p1 = Rectangle(f, h1, x0, xk)
t1 = Trapezoidal(f, h1, x0, xk)
s1 = Simpson(f, h1, x0, xk)

p2 = Rectangle(f, h2, x0, xk)
t2 = Trapezoidal(f, h2, x0, xk)
s2 = Simpson(f, h2, x0, xk)

p = 2   #второй порядок
rp = Runge_Romberg(p1, p2, h1, h2, p)
rt = Runge_Romberg(t1, t2, h1, h2, p)
rs = Runge_Romberg(s1, s2, h1, h2, p)

print('h1 = ', h1)
print("Прямоугольник: ", p1)
print("Трапеция: ", t1)
print("Симпсон: ", s1)

print('h2 = ', h2)
print("Прямоугольник: ", p2)
print("Трапеция: ", t2)
print("Симпсон: ", s2)

print('\nТочное значение по Рунге-Ромбергу для прямоугольника: {0};\nПогрешность: {1} и {2}'.format(rp, abs(rp - p1), abs(rp - p2)))
print('\nТочное значение по Рунге-Ромбергу для трапеции: {0};\nПогрешность: {1} и {2}'.format(rt, abs(rt - t1), abs(rt - t2)))
print('\nТочное значение по Рунге-Ромбергу для Симпсона: {0};\nПогрешность: {1} и {2}'.format(rs, abs(rs - s1), abs(rs - s2)))