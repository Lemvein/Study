import numpy as np
import math
import matrix

def p(x):
    return 2 / x

def q(x):
    return -x

def f(x):
    return 0

def razn(p=p, q=q, f=f, ya=math.exp(-1), yb=0.5 * math.exp(-2), h=0.1, a=1, b=2):
    A = []
    B = []
    rows = []
    x = np.arange(a, b + h, h)
    
    n = len(x)
    
    for i in range(n):
        if i == 0:
            rows.append(1)
        else:
            rows.append(0)
    A.append(rows)
    B.append(ya)
    
    for i in range(1, n - 1):
        rows = []
        B.append(f(x[i]))
        for j in range(n):            
            if j == i - 1:
                rows.append(1 / h ** 2 - p(x[i]) / (2 * h))
            elif i == j:
                rows.append(-2 / h ** 2 + q(x[i]))
            elif j == i + 1:
                rows.append(1 / h ** 2 + p(x[i]) / (2 * h))
            else:
                rows.append(0)
        A.append(rows)
    
    rows = []
    B.append(yb)
    
    for i in range(n):
        if i == n - 1:
            rows.append(1)
        else:
            rows.append(0)
            
    A.append(rows)    
    y = matrix.Progonka(matrix.Matrix(A), B)
    for i in range (len(x)):
        print('x: ', round(x[i],2), 'y: ', y[i])
    
    return x, y