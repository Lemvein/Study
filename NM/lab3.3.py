import matplotlib.pyplot as plt
from functools import reduce
import LU

x = [-0.7, -0.4, -0.1, 0.2, 0.5, 0.8]
y = [2.3462, 1.9823, 1.671, 1.3694, 1.0472, 0.6435]

def error_value(F, X, Y):
    return reduce(lambda x, y: x + y, map(lambda v: (F(v[0]) - v[1])**2, zip(X, Y)))

def count_a(X, Y, n):
    A = []
    b = []
    N = len(X)
    for k in range(n + 1):
        A.append([sum(map(lambda x: x**(i + k), X)) for i in range(n + 1)])
        b.append(sum(map(lambda x: x[0] * x[1]**k, zip(Y, X))))
    return LU.LU_solution(A, b)

def MNK_func(X, Y, n):
    a = count_a(X, Y, n)
    #print("Коэффицеты а приближающего многочлена:", a)
    return lambda x: sum([a[i] * x**i for i in range(n + 1)])

F1 = MNK_func(x, y, 1)
F2 = MNK_func(x, y, 2)

print("Значение квадрата ошибки n=1:", error_value(F1, x, y))
print("Значение квадрата ошибки n=2:", error_value(F2, x, y))


y1 = list(map(F1, x)) 
plt.figure(figsize=(10,7))
plt.scatter(x, y)
plt.title('Приближенный многочлен 1-ой степени методом МНК')
plt.plot(x, y1, color='g')
plt.grid()


y2 = list(map(F2, x))
plt.figure(figsize=(10,7))
plt.scatter(x, y)
plt.title('Приближенный многочлен 2-ой степени методом МНК')
plt.plot(x, y2, color='g')
plt.grid()
plt.show()