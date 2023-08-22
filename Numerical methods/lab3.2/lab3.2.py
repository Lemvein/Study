import matplotlib.pyplot as plt

xi = [-0.4, -0.1, 0.2, 0.5, 0.8]
yi = [1.9823, 1.6710, 1.3694, 1.0472, 0.6435]

point_x = 0.1

#Метод прогонки
def race_method(A, b):
    P = [-item[2] for item in A]
    Q = [item for item in b]
    
    P[0] /= A[0][1]
    Q[0] /= A[0][1]
    
    for i in range(1, len(b)):
        z = (A[i][1] + A[i][0] * P[i-1])
        P[i] /= z
        Q[i] -= A[i][0] * Q[i-1]
        Q[i] /= z
    
    x = [item for item in Q]
    
    for i in range(len(x) - 2, -1, -1):
        x[i] += P[i] * x[i + 1]
    
    return x

def h_coeff(x):
    h = [x[i] - x[i - 1] for i in range(1, len(x))]
    return h

def c_coeff(h, f):
    A = [[h[i - 1], 2.0 * (h[i - 1] + h[i]), h[i]] for i in range(1, len(h))]
    A[0][0] = A[-1][2] = 0.0
    B = [3.0 * ((f[i + 1] - f[i]) / h[i] - (f[i] - f[i - 1]) / h[i - 1]) for i in range(1, len(h))]
    return [0.0] + race_method(A, B)

def a_coeff(f):
    return f[:len(f) - 1]

def b_coeff(f, h, c):
    b = [(f[i] - f[i - 1]) / h[i - 1] - (1.0/3.0) * h[i - 1] * (c[i] + 2.0 * c[i - 1]) for i in range(1, len(h))]
    b.append((f[-1] - f[-2]) / h[-1] - (2.0/3.0) * h[-1] * c[-1])
    return b

def d_coeff(h, c):
    d = [(c[i + 1] - c[i]) / (3.0 * h[i]) for i in range(len(h) - 1)]
    d.append(-c[-1] / (3.0 * h[-1]))
    return d

def find_position(x, arr):
    for i in range(len(arr) - 1):
        if (arr[i] <= x and x <= arr[i + 1]):
            return i

def interpolation(X, Y):
    h = h_coeff(X)
    c = c_coeff(h, Y)
    a = a_coeff(Y)
    b = b_coeff(Y, h, c)
    d = d_coeff(h, c)   
    def interpol(x):
        pos = find_position(x, X)
        if pos < 0:
            return b[0]*x + a[0] - b[0]*X[0]
        elif pos == len(X) - 1:
            return Y[-1] + (b[-1] + 2.0*c[-1]*h[-1] + 3.0*d[-1]*h[-1]*h[-1])*(x - X[-1])
        return a[pos] + b[pos]*(x - X[pos]) + c[pos]*((x - X[pos])**2) + d[pos] * ((x - X[pos])**3)
    return interpol


Cubic = interpolation(xi, yi)
print("Значение интерполяции:", Cubic(point_x))

x = xi
y = list(map(Cubic, x)) 

fig = plt.figure(figsize=(12, 8))
ax1 = fig.add_subplot(111)

line1, = ax1.plot(x, y, 'y', label="f(x)")
ax1.set_xlabel('x')
ax1.set_ylabel('y', color='g')

ax1.plot(list(xi), list(yi), 'o')

plt.title('Кубическая интерполяция')
plt.xlim(-0.5, 1)

plt.show()