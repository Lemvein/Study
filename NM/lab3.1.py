import matplotlib.pyplot as plt
import math
import numpy

def f(x):
    return math.acos(x)

X1 = numpy.array([-0.4, -0.1, 0.2, 0.5])
X2 = numpy.array([-0.4, 0, 0.2, 0.5])
X = 0.1


def omega(i, X, x):
    res = 1
    for j in range(len(X)):
        if i != j:
            res *= x - X[j]
    return res

def L(x, X):
    l = 0
    for i in range(len(X)):
        fomega = f(X[i]) / omega(i, X, X[i])
        l += fomega * omega(i, X, x)
    return l



def X_new(i, k, X):
    return [X[j] for j in range(i, k)]

#Разделенная разность
def separate(X):
    if len(X) == 2:
        return (f(X[0]) - f(X[1])) / (X[0] - X[1])
    else:
        return (separate(X_new(0, len(X) - 1, X)) - separate(X_new(1, len(X), X))) / (X[0] - X[len(X) - 1])

def xxx(x, i, X):
    res = 1
    for j in range(i):
        res *= (x - X[j])
    return res
    
def P(x, X):
    p = f(X[0])
    for i in range(1, len(X)):
        X_ = X_new(0, i + 1, X)
        p += xxx(x, i, X) * separate(X_)
    return p



print("1. Пункт а) -0.4, -0.1, 0.2, 0.2")
print("2. Пункт б) -0.4, 0, 0.2, 0.5")
print("\nВведите номер задания:")
a = input()
if a == '1': X_a = X1
elif a == '2': X_a = X2

print("МНОГОЧЛЕН ЛАГРАНЖА\n")
print("\tf(x*) = {0}\n\tL(x*) = {1}\n\tПогрешность: {2}\n".format(f(X), L(X, X_a), f(X) - L(X, X_a)))

print("МНОГОЧЛЕН НЬЮТОНА\n")
print("\tf(x*) = {0}\n\tP(x*) = {1}\n\tПогрешность: {2}\n".format(f(X), P(X, X_a), f(X) - P(X, X_a)))


xmin = -0.5
xmax = 0.6
dx = 0.01

xarr = numpy.arange(xmin, xmax, dx)
ylist = [f(x) for x in xarr]

y_X_a = [f(x) for x in X_a]

Larr = X_a
Llist = [L(x, X_a) for x in Larr]

Parr = X_a
Plist = [P(x, X_a) for x in Parr]

plt.figure(figsize=(12, 8))

plt.plot(xarr, ylist)
plt.plot(Larr, Llist, linestyle = '--')
plt.plot(Parr, Plist, linestyle = ':')
plt.plot(X_a, y_X_a, 'o')
plt.xlabel('X')
plt.ylabel('Y')
plt.legend(['f(x)', 'L(x)', 'P(x)', 'X'])
plt.show()