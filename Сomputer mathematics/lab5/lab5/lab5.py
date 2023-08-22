import numpy as np

indexes = {"0": "\u2070",
           "1": "\u00B9",
           "2": "\u00B2",
           "3": "\u00B3",
           "4": "\u2074",
           "5": "\u2075",
           "6": "\u2076",
           "7": "\u2077",
           "8": "\u2078",
           "9": "\u2079",
           }
def degree(a: int):
    degrees = ""
    s = str(a)
    for char in s:
        degrees += indexes[char] or ""
    return degrees

#Вывод полинома
def PrintPolinom(p, q):
    if q == 1:
        s = "f'(x) = "
    else:
        s = "f(x) = "
    for i in range(len(p)):
        if p[i] != 0:
            if i == len(p)-1:
                if p[i] > 0:
                    s += "+"
                s += str(p[i])
            else:
                if i != 0:
                    if (p[i] > 0) and (s != "p(x) = "):
                        s += "+"
                if p[i] != 1:
                    if i == len(p)-2:    
                        s += str(p[i]) + "x"
                    else:
                        s += str(p[i]) + "x" + degree(len(p)-i-1)
                else:
                    if i == len(p)-2:    
                        s += "x"
                    else:
                        s += "x" + degree(len(p)-i-1)            
    print(s)

#Ввод матрицы
def GetMatrix(s):
    matrix = []
    for line in s.split(';'):
        matrix.append(line.split())
    return np.array(matrix).astype(float)

#Метод Леверрье
def Leverrye(matrix):
    result = []
    s1 = [matrix]
    s = []
    for i in range(len(matrix) - 1):
        s1.append(np.dot(s1[i], matrix))    
        s.append(np.trace(s1[i]))           
    s.append(np.trace(s1[-1]))             
    for i in range(len(s)):                 #поиск р по формуле (с.419)
        p = s[i]
        for j in range(len(result)):
            p += result[j] * s[len(result) - j - 1]
        p = -p / (i + 1)
        result.append(p)
    result = [1] + result
    return result

#Задание №1
def Lab1():
    s = input()
    matrix = GetMatrix(s)
    result = Leverrye(matrix)
    print("Характеристический полином:")
    PrintPolinom(result,0)

#Задание №2
def Lab2():
    nat = []
    kom = []
    s = input()
    matrix = GetMatrix(s)
    coef = Leverrye(matrix)
    root = np.unique(np.roots(coef))
    for i in range(len(root)):
        if abs(root[i].imag) < 0.001:
            nat.append(root[i].real)
        else:
            kom.append(root[i])
    print("Натуральные значения = ", nat)
    print("Комплексные значения =", kom)
    print("Искомое значение = ", max(nat))

#Меню
while True:
    print("\nВведите номер задания:")
    a = input()
    if a == '1': Lab1()
    elif a == '2': Lab2()
    elif a == '0': break
    else: print("Неверный номер. Повторите попытку, либо введите '0' для выхода из программы.")

'''
1
5 4; 3 -2
4 -4 0; 3 1 3; 1 2 -3
3 4 -1 -1; -1 -3 1 -1; -2 -4 4 2; -1 0 -5 0
-6.5 7.8 8.1; 1.6 2.4 -9.1; -8.1 3.1 -1

2
2 1 -4; -3 4 0; -3 -1 8
1 -3 -2; -1 4 4; -2 3 6
-1 7 2; 9 8 1; 5 2 7
-10 1 -1; -4 -8 -1; -2 -5 -9
'''