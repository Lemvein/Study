import numpy as np
import matrix

def delta(f, xk, yk, zk, h, flag):
    K1 = h * zk
    L1 = h * f(xk, yk, zk)

    K2 = h * (zk + 0.5 * L1)
    L2 = h * f(xk + 0.5 * h, yk + 0.5 * K1, zk + 0.5 * L1)
    
    K3 = h * (zk + 0.5 * L2)
    L3 = h * f(xk + 0.5 * h, yk + 0.5 * K2, zk + 0.5 * L2)
    
    K4 = h * (zk + L3)
    L4 = h * f(xk + h, yk + K3, zk + L3)
    
    if flag == True:
        return (K1 + 2 * K2 + 2 * K3 + K4) / 6
    return (L1 + 2 * L2 + 2 * L3 + L4) / 6

def runge_kutta(f, y0=1, z0=0, h=0.1, a=0, b=1):
    x = np.arange(a, b + h, h)
    y = [y0]
    z = [z0]
    
    for i in range(1, len(x)):
        y.append(y[i - 1] + delta(f, x[i - 1], y[i - 1], z[i - 1], h, True))
        z.append(z[i - 1] + delta(f, x[i - 1], y[i - 1], z[i - 1], h, False))
    return x, y, z

def eps(x, y, f):
    return sum([(y[i] - f(x[i])) ** 2 for i in range(len(x))]) ** 0.5

def d_eps(x, z, dy):
    return sum([(z[i] - dy(x[i])) ** 2 for i in range(len(x))]) ** 0.5