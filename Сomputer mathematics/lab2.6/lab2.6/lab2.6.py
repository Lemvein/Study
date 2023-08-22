import numpy as np
import copy


def print_p(m):
    s = ''
    for i in range(len(m)):
        for j in range(len(m[i])):
            if m[i][j] != 0:
                if (len(m[i]) - 1 - j) != 0 or ((len(m) - 1 - i) != 0):
                    f = 1
                    if (len(m[i]) - 1 - j) != 0:
                        s += str(m[i][j]) + 'x^' + str(len(m[i]) - j - 1)
                    else:
                        s += str(m[i][j]) + 'y^' + str(len(m) - 1 - i)
                        f = 0
                    if (len(m) - 1 - i) != 0 and (f == 1):
                        s += 'y^' + str(len(m) - 1 - i)
                    s += '+'
                else:
                    s += str(m[i][j])

    for i in range(len(s)):
        if i < len(s) - 4:
            if (((s[i] == '1') and (s[i + 1] == ".") and (s[i - 1] == '+' or s[i - 1] == '-')) or (
                    (s[i] == '1') and (s[i + 1] == '.') and (s[i + 3] == 'x' or s[i + 3] == 'y'))):
                s = s[:i] + s[(i + 3):]
    for i in range(len(s)):
        if i < len(s) - 1:
            if (s[i] == '^') and (s[i + 1] == '1'):
                s = s[:i] + s[(i + 2):]
    for i in range(len(s)):
        if i < len(s) - 1:
            if (s[i] == '+') and (s[i + 1] == '-'):
                s = s[:i] + s[(i + 1):]
    if s[len(s)-1] == '+':
        s = s[:len(s)-1]
    return s


def dx(m):
    m_1 = copy.deepcopy(m)
    m1 = []
    for i in range(len(m_1)):
        m1.append(m_1[i][:(len(m_1[i]) - 1)])
    for i in range(len(m1)):
        for j in range(len(m1[i])):
            m1[i][j] *= (len(m_1[i]) - 1 - j)
    return m1


def dy(m):
    m_2 = copy.deepcopy(m)
    m1 = []
    for i in range(len(m_2)-1):
        m1.append(m_2[i])
    for i in range(len(m1)):
        for j in range(len(m1[i])):
            m1[i][j] *= (len(m_2) - 1 - i)
    return m1


def dp_numbers(a1):
    d = len(a1) - 1
    k = []
    for i in range(d):
        k.append(d * a1[i])
        d -= 1
    return k


def f1(x, y, m):  # вспомогательная функция 1
    k = 0
    for i in range(len(m)):
        for j in range(len(m[i])):
            k += float(m[i][j]) * (x**(len(m[i]) - j - 1) * (y**(len(m) - 1 - i)))
    return k


def f2(x, a1):  # вспомогательная функция 2
    k = 0
    for i in range(len(a1)):
        k += float(a1[i]) * (x**(len(a1) - 1 - i))
    return k


def J(xn, yn, m1, m2, f):  # якобиан
    j = f1(xn, yn, dx(m1)) * f1(xn, yn, dy(m2)) - f1(xn, yn, dy(m1)) * f1(xn, yn, dx(m2))
    return j


def make_gorn(m1, x, y):
    m = copy.deepcopy(m1)
    while len(m) > 1:
        for i in range(len(m[0])):
            m[0][i] = m[0][i] * x + m[1][i]
        m = m[:1] + m[2:]
    m = m[0]
    return m


def f_solve(m_1, x1, m_2, x2, f1):
    m1 = copy.deepcopy(m_1)
    m2 = copy.deepcopy(m_2)
    eps = 0.000001
    while 1:
        yprev = x2
        hn = -1.0 / J(x1, x2, m1, m2, f1) * (f1(x1, x2, m1) * f1(x1, x2, dy(m2)) - f1(x1, x2, dy(m1)) * f1(x1, x2, m2))
        kn = -1.0 / J(x1, x2, m1, m2, f1) * (f1(x1, x2, dx(m1)) * f1(x1, x2, m2) - f1(x1, x2, m1) * f1(x1, x2, dx(m2)))
        x1 += hn
        x2 += kn
        if abs(x2-yprev) < eps:
            break

    x1 = round(x1, 5)
    x2 = round(x2, 5)
    roots = [x1, x2]
    return roots


def complex_roots(p):

    p = p.copy()
    p = p.astype(np.complex64)  # преобразуем тип в комплексный
    non_zero = np.nonzero(p)[0]

    if len(non_zero) == 0:
        return np.zeros(0, dtype=p.dtype)

    trailing_zeros = len(p) - non_zero[-1] - 1

    p = p[int(non_zero[0]):int(non_zero[-1])+1]

    N = len(p)
    if N > 1:
        A = np.diag(np.ones((N-2,), p.dtype), -1)
        A[0,:] = -p[1:] / p[0]
        roots, _ = np.linalg.eig(A)  # сз и св матрицы
    else:
        roots = np.zeros(0, dtype=p.dtype)  # массив нулей

    for k in range(roots.shape[0]):
        if abs(roots[k].imag) < 1.0E-6:
            roots[k] = complex(roots[k].real, 0.0)
    for k in range(roots.shape[0]):
        if abs(roots[k].real) < 1.0E-6:
            roots[k] = complex(0.0 ,roots[k].imag)
    roots = np.concatenate((roots, np.zeros(trailing_zeros, roots.dtype)))
    return roots


# Начало

print('Введите 1 матрицу: ')
m = np.array(list(map(lambda f: list(map(float, f.split())), input().split(','))))
print('Введите 2 матрицу: ')
m1 = np.array(list(map(lambda f: list(map(float, f.split())), input().split(','))))
print("\nF(x,y)=", print_p(m))
print("G(x,y)=", print_p(m1))
roots = []
x = -100.0
y = -100.0
roots.append(f_solve(m, x, m1, y, f1))
x1 = x
y1 = y
for i in range(100):
    y += 0.5
    k = f_solve(m, x, m1, y, f1)
    if k not in roots:
        roots.append(k)
y = y1
for i in range(100):
    x += 0.5
    k = f_solve(m, x, m1, y, f1)
    if k not in roots:
        roots.append(k)
x = 100
y = 100
x1 = x
y1 = y
for i in range(100):
    y -= 0.5
    k = f_solve(m, x, m1, y, f1)
    if k not in roots:
        roots.append(k)
y = y1
for i in range(100):
    x -= 0.5
    k = f_solve(m, x, m1, y, f1)
    if k not in roots:
        roots.append(k)


roots1 = []
for i in range(len(roots)):
    if f1(roots[i][0], roots[i][1], m) < 0.001 and f1(roots[i][0], roots[i][1], m1) < 0.001:
        roots1.append(roots[i])
# print("\nroots:", roots1)
for i in range(len(roots) - 1):
    k = make_gorn(m, roots[i][1], 1)
    if ((f2(-10000, k) > 0 and f2(roots[i][1], dp_numbers(k)) > 0 and f2(10000, k) > 0) or (
            f2(-10000, k) > 0 and f2(roots[i][1], dp_numbers(k)) > 0 and f2(10000, k) > 0)):
        r = complex_roots(np.array(k))
        break
for i in range(len(roots1)):
    print("\nroot:", roots1[i][0], "+ (", roots1[i][1], ") * i")


'''
0 0 -2, 0 0 0, 2 2 3
0 0 0, 0 4 2, 0 0 0

0 0 -5, 0 0 0, 5 7 10
0 0 0, 0 10 7, 0 0 0

0 0 -2, 0 0 0, 2 3 7
0 0 0, 0 4 3, 0 0 0
'''