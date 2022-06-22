import math
import numpy 

#Нелинейное уравнение
def fun(x):
    return x ** 3 + x ** 2 - 2 * x - 1

def dfun(x):
    return 3 * (x ** 2) + 2 * x - 2

def ddfun(x):
    return 6 * x + 2

def phi1(x):
    return (x ** 3 + x ** 2 - 1) / 2

def dphi1(x):
    return (3 * x ** 2 + 2 * x) / 2

def phi2(x):
    return (-x ** 3 + 2 * x + 1) ** (1/2)

def dphi2(x):
    return (2 - 3 * x ** 2) / (2 * ((-x ** 3 + 2 * x + 1) ** (1/2)))

def phi3(x):
    if x < 0:
        return abs((-x ** 2 + 2 * x + 1)) ** (1/3)*(-1)
    else:
        return (-x ** 2 + 2 * x + 1) ** (1/3)

def dphi3(x):
    return (2 - 2 * x) / (3 * ((-x ** 2 + 2 * x + 1) ** (2/3)))

#Первое уравнение системы
def f1(x1, x2):
    return x1 ** 2  + x2 ** 2 - 16

def df1_dx1(x1, x2):
    return 2 * x1

def df1_dx2(x1, x2):
    return 2 * x2

def ph1(x1, x2):
    return (-x2 ** 2 + 16) ** (1/2)

def dph1_dx1(x1, x2):
    return 0

def dph1_dx2(x1, x2):
    return x2 / ((x2 ** 2 + 16) ** (1/2))

#Второе уравнение системы
def f2(x1, x2):
    return x1 - (numpy.exp(x2)) + 4

def df2_dx1(x1, x2):
    return 1

def df2_dx2(x1, x2):
    return -(numpy.exp(x2))

def ph2(x1, x2):
    return math.log(x1 + 4)

def dph2_dx1(x1, x2):
    return 1/(x1 + 4)

def dph2_dx2(x1, x2):
    return 0

def Jacobi(a11, a12, a21, a22):
	return [
    [a11, a12],
    [a21, a22]
    ]

def norm(a ,b):
  return (a ** 2 + b ** 2) ** (1/2)

def norm_matrix(A):
  n = 0
  for i in range(len(A)):
    for j in range (len(A)):
      n = n + (A[i][j]) ** 2 
  return n **(1/2)

def det2(A):
  return A[0][0] * A[1][1] - A[1][0] * A[0][1] 

def inv2(A):
  detA = det2(A)
  B = A
  B[0][0] = (1/detA) * A[1][1]
  B[1][0] = -(1/detA) * A[1][0]
  B[0][1] = -(1/detA) * A[0][1]
  B[1][1] = (1/detA) * A[0][0]  
  return B

def dot(A,B):
  C = B
  C[0][0] = A[0][0]*B[0][0]+A[0][1]*B[1][0]
  C[1][0] = A[1][0]*B[0][0]+A[1][1]*B[1][0]
  return(C)

#Метод простой итерации решения нелинейных уравнений
def simple_iteration1(ph, dph, a, b, eps):
    x = (a + b) / 2
    k = 0
    cov = True
    while cov:
        if (abs(dph(x)) >= 1):    #Достаточное условие сходимости
            print('Достаточное условие сходимости не выполнено')
            return
        k += 1
        x_next = ph(x)
        print(f"x: {x_next} k: {k} |x_next - x|: {abs(x_next - x)}")
        if abs(x_next - x) <= eps:     #Условие выхода
            cov = False
        x = x_next

#Метод Ньютона решения нелинейных уравнений
def newton1(f, df, ddf, a, b, exp):
    if (f(a) * ddf(a) < (df(a)) ** 2):
        x = a
    else:
        if (f(b) * ddf(b) < (df(b)) ** 2):
            x = b
        else:
            print('Достаточное условие сходимости не выполнено')
            return        
    k = 0
    cov = True
    while cov:
        if (df(x) == 0):
            print('Достаточное условие сходимости не выполнено')
            return        
        k += 1
        x_next = x - f(x) / df(x)
        print(f"x: {x_next} k: {k} |x_next - x|: {abs(x_next - x)}")
        if abs(x_next - x) <= exp:
            cov = False
        x = x_next

#Метод простой итерации решения систем нелинейных уравнений
def simple_iteration2(x1, x2, eps):
    x1_next = x1
    x2_next = x2
    k = 0   
    while True:
        J = Jacobi(dph1_dx1(x1, x2), dph1_dx2(x1, x2), dph2_dx1(x1, x2), dph2_dx2(x1, x2))
        if norm_matrix(J) >= 1:
            print('Достаточное условие сходимости не выполнено')
            return
        k += 1
        x1_next, x2_next = ph1(x1, x2), ph2(x1, x2)      
        print(f"x1: {x1_next} x2: {x2_next} k: {k}")
        if norm(x1_next - x1, x2_next - x2) <= eps:
        	break
        x1, x2 = x1_next, x2_next

#Метод Ньютона решения систем нелинейных уравнений
def newton2(x1, x2, eps):
    x1_next = x1
    x2_next = x2
    k = 0
    while True:
        J = Jacobi(df1_dx1(x1, x2), df1_dx2(x1, x2), df2_dx1(x1, x2), df2_dx2(x1, x2))
        if det2(J) == 0:
            print('Достаточное условие сходимости не выполнено')
        k += 1
        [[x1_next],[x2_next]] = [[x1],[x2]] - numpy.dot(inv2(J), [[f1(x1, x2)], [f2(x1, x2)]])
        print(f"x1: {x1_next} x2: {x2_next} k: {k}")
        if norm(x1_next - x1, x2_next - x2) <= eps:
            break
        x1, x2 = x1_next, x2_next

#Меню
while True:
    print("\nЗадания:")
    print("1. Метод простой итерации решения нелинейных уравнений")
    print("2. Метод Ньютона решения нелинейных уравнений")
    print("3. Метод простой итерации решения систем нелинейных уравнений")
    print("4. Метод Ньютона решения систем нелинейных уравнений")
    print("\nВведите номер задания:")
    a = input()
    #print("\nВведите точность вычислений:")
    #eps = float(input())
    eps = 0.00001
    if a == '1': simple_iteration1(phi3, dphi3, 1, 2, eps)
    elif a == '2': newton1(fun, dfun, ddfun, 1, 2, eps)
    elif a == '3': simple_iteration2(3.5, 2, eps)
    elif a == '4': newton2(3.5, 2, eps)
    elif a == '0': break
    else: print("Неверный номер. Повторите попытку, либо введите '0' для выхода из программы.")