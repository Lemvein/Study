import numpy
import matplotlib.pyplot as plt
import LU


def Fredgolm_I(K, g, x, h):
    n = x.size

    A = numpy.zeros((n, n))

    for i in range(n):
            A[i, 0] = 0.5*h*K(x[i], x[0])
            A[i, n-1] = 0.5*h*K(x[i], x[n-1])

    for i in range(n):
        for j in range(n):
                A[i, j] = h * K(x[i], x[j])
    return numpy.linalg.solve(A, g(x, a, b))

def Fredgolm_II(K, g, x, h, lamb):
    n = x.size

    A = numpy.zeros((n, n))

    for i in range(n):
        if i == 0:
            A[i, 0] = 1-0.5*lamb*h*K(x[i], x[0])
        else:
            A[i, 0] = - 0.5 * lamb * h * K(x[i], x[0])

    for i in range(n):
        for j in range(n):
            if j == i:
                A[i, j] = 1 - lamb * h * K(x[i], x[j])
            else:
                A[i, j] = - lamb * h * K(x[i], x[j])

    for i in range(n):
        if i == n-1:
            A[i, n-1] = 1-lamb * 0.5 * h * K(x[i], x[n-1])
        else:
            A[i, n - 1] = - lamb * 0.5 * h * K(x[i], x[n - 1])
 
    return numpy.linalg.solve(A, g(x))

def error(x, y, f):
    max = 0
    for i in range(x.size):
        e = numpy.abs(f(x[i]) - y[i])
        if (e > max):
            max = e
    return max



print('1 or 2')
a = input()
if a == "2":

    #a = 0
    #b = 1
    #lamb = 1/2
    a = -numpy.pi
    b = numpy.pi
    lamb = 3/(10*numpy.pi)

    def K(x, t):
        return 1/(0.64*numpy.cos((x+t)/2)**2 - 1)
        #return numpy.exp(x-t)


    def g(x):
        return 25 - 16*numpy.sin(x)**2
        #return numpy.exp(x)


    def f(x):
        return 17/2 + (128/17)*numpy.cos(2*x)
        #return 2*numpy.exp(x)

    h_exact = 0.01
    x_exact = numpy.arange(a, b, h_exact)
    y_exact = f(x_exact)
    plt.plot(x_exact, y_exact, 'C9', label = 'точное', linewidth=3, linestyle='--')

    h_approx = 0.2
    x_approx = numpy.arange(a, b, h_approx)
    y_approx = Fredgolm_II(K, g, x_approx, h_approx, lamb)
    y_approx[0]=f(x_approx[0])
    y_approx[len(y_approx)-1] = f(x_approx[x_approx.size-1])
    plt.plot(x_approx, y_approx, 'C3', label = 'численное')

    plt.title('h = ' + str(h_approx) + ' Погрешность = ' + str(error(x_approx, y_approx, f)))
    plt.grid(True)
    plt.legend()
    plt.show()

elif a == "1":
    a = 0
    b = 2*numpy.pi

    def K(t, x):
        return numpy.abs(x - t)

    def f(x):
        #return -0.5*numpy.sin(x)
        return -0.5*numpy.cos(x)

    def F(x):
        #return numpy.sin(x)
        return numpy.cos(x)

    def dF(x):
        #return numpy.cos(x)
        return -numpy.sin(x)

    def g(x, a, b):
        A = -0.5 *(dF(a) + dF(b))
        B = 0.5*(a*dF(a)+b*dF(b) - F(a) - F(b))
        return F(x) + A*x + B

    h_exact = 0.001
    x_exact = numpy.arange(a, b, h_exact)
    y_exact = f(x_exact)
    plt.plot(x_exact, y_exact, 'C9', label = 'точное', linewidth=3, linestyle='--')

    # строим численное решение
    h_approx = 0.5
    x_approx = numpy.arange(a, b, h_approx)
    y_approx = Fredgolm_I(K, g, x_approx, h_approx)
    y_approx[0]=f(x_approx[0])
    y_approx[1]=f(x_approx[1])
    y_approx[y_approx.size-2] = f(x_approx[x_approx.size-2])
    y_approx[y_approx.size-1] = f(x_approx[x_approx.size-1])
    plt.plot(x_approx, y_approx, 'C3', label = 'численное')

    plt.title('h = ' + str(h_approx) + ' Погрешность = ' + str(error(x_approx, y_approx, f)))
    plt.grid(True)
    plt.legend()
    plt.show()