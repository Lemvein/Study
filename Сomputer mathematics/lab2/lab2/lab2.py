import numpy as np
import copy

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

#Вывод двумерных
def PrintMatrix(polinom, n):
    if n == 0:
        s = " f(x,y) = "
    if n == 1:
           s = " dx = "
    if n == 2:
           s = " dy = "
    for i in range(len(polinom)):
        for j in range(len(polinom[i])):
            #один коэфициент
            if (i == len(polinom) - 1) and (j == len(polinom[i]) - 1):
                if polinom[i][j] > 0:
                    s += " + " + str(polinom[i][j])
                elif polinom[i][j] < 0:
                    s += " - " + str(-1*polinom[i][j])
                else: continue
            #только Х
            elif i == len(polinom) - 1:
                #без степени
                if j == len(polinom[i]) - 2:
                    if polinom[i][j] == 1:
                        s += " + " + "x"
                    elif polinom[i][j] == -1:
                        s += " - " + "x"
                    elif polinom[i][j] > 0:
                        s += " + " + str(polinom[i][j]) + "x"
                    elif polinom[i][j] < 0:
                        s += " - " + str(-1 * polinom[i][j]) + "x"
                    else: continue
                #cо степенью
                else:
                    if polinom[i][j] == 1:
                        s += " + " + "x" + degree(len(polinom[i]) - j - 1)
                    elif polinom[i][j] == -1:
                        s += " - " + "x" + degree(len(polinom[i]) - j - 1)
                    elif polinom[i][j] > 0:
                        s += " + " + str(polinom[i][j]) + "x" + degree(len(polinom[i]) - j - 1)
                    elif polinom[i][j] < 0:
                        s += " - " + str(-1*polinom[i][j]) + "x" + degree(len(polinom[i]) - j - 1)
                    else: continue
            #только У (аналогично)
            elif j == len(polinom[i])-1:
                #без степени
                if i == len(polinom)-2:
                    if polinom[i][j] == 1:
                        s += " + " + "y"
                    elif polinom[i][j] == -1:
                        s += " - " + "y"
                    elif polinom[i][j] > 0:
                        s += " + " + str(polinom[i][j]) + "y"
                    elif polinom[i][j] < 0:
                        s += " - " + str(-1 * polinom[i][j]) + "y"
                    else: continue
                #со степенью
                else:
                    if polinom[i][j] == 1:
                        s += " + " + "y" + degree(len(polinom) - i - 1)
                    elif polinom[i][j] == -1:
                        s += " - " + "y" + degree(len(polinom) - i - 1)
                    elif polinom[i][j] > 0:
                        s += " + " + str(polinom[i][j]) + "y" + degree(len(polinom) - i - 1)
                    elif polinom[i][j] < 0:
                        s += " - " + str(-1 * polinom[i][j]) + "y" + degree(len(polinom) - i - 1)
                    else:
                        continue
           #с ХУ 
            else:
                #без степени
                if (j == len(polinom[i])-2) and (i == len(polinom)-2):
                    if polinom[i][j] == 1:
                        s += " + " + "xy"
                    elif polinom[i][j] == -1:
                        s += " - " + "xy"
                    elif polinom[i][j] > 0:
                        s += " + " + str(polinom[i][j]) + "xy"
                    elif polinom[i][j] < 0:
                        s += " - " + str(-1 * polinom[i][j]) + "xy"
                    else: continue
                #со степенью у У
                elif j == len(polinom[i])-2:
                    if polinom[i][j] == 1:
                        s += " + " + "x" + "y" + degree(len(polinom) - i - 1)
                    elif polinom[i][j] == -1:
                        s += " + " + "x" + "y" + degree(len(polinom) - i - 1)
                    elif polinom[i][j] > 0:
                        s += " + " + str(polinom[i][j]) + "x" + "y" + degree(len(polinom) - i - 1)
                    elif polinom[i][j] < 0:
                        s += " - " + str(-1 * polinom[i][j]) + "x" + "y" + degree(len(polinom) - i - 1)
                    else: continue
                #со степенью у Х
                elif i == len(polinom)-2:
                    if polinom[i][j] == 1:
                        s += " + " + "x" + degree(len(polinom[i]) - j - 1) + "y"
                    elif polinom[i][j] == -1:
                        s += " - " + "x" + degree(len(polinom[i]) - j - 1) + "y"
                    elif polinom[i][j] < 0:
                        s += " - " + str(-1 * polinom[i][j]) + "x" + degree(len(polinom[i]) - j - 1) + "y"
                    elif polinom[i][j] > 0:
                        s += " + " + str(polinom[i][j]) + "x" + degree(len(polinom[i]) - j - 1) + "y"
                    else: continue
                #степень у Х и у У
                else:
                    if polinom[i][j] == 1:
                        s += " + " + "x" + degree(len(polinom[i]) - j - 1) + "y" + degree(len(polinom) - i - 1)
                    elif polinom[i][j] == -1:
                        s += " - " + "x" + degree(len(polinom[i]) - j - 1) + "y" + degree(len(polinom) - i - 1)
                    elif polinom[i][j] < 0:
                        s += " - " + str(-1 * polinom[i][j]) + "x" + degree(len(polinom[i]) - j - 1) + "y" + degree(len(polinom) - i - 1)
                    elif polinom[i][j] > 0:
                        s += " + " + str(polinom[i][j]) + "x" + degree(len(polinom[i]) - j - 1) + "y" + degree(len(polinom) - i - 1)
                    else: continue
    if s[1] == '+':
         print(s[3:])
    else: print(s[1:])

def Border(p):
    x = 1
    while 1:
        flag = 1
        p0 = [p[0]]
        for i in range(1, len(p) - 1):
            c = p[i] + p0[i - 1] * x
            if c < 0:
                x += 1
                flag = 0
                break
            p0.append(c)
        if flag:
            break
    return x

#Функция
def f(x, p):
    k = 0
    for i in range(len(p)):
        k += float(p[i]) * (x ** (len(p) - 1 - i))
    return k

#Производная
def Derivative(polinom):
    b = []
    size = len(polinom) - 1
    for i in range(size):
        b.append(polinom[i] * size)
        size -= 1
    return b

#Метод хорд
def Chord(PrevX, CurrX, p):
    e=0.001
    NextX = 0
    while 1:
        tmp = NextX
        if (f(PrevX, p) - f(CurrX, p) != 0):
            NextX = CurrX - f(CurrX, p) * (PrevX - CurrX) / (f(PrevX, p) - f(CurrX, p))
        PrevX = CurrX
        CurrX = tmp
        if ((abs(NextX - CurrX) <= e)): break
    return NextX

#Метод Ньютона
def Newton(a,b,p,der):
    e=0.001
    if (f(a,p)*f(a, Derivative(der)))>0:
        x=a
    else: x=b
    if(f(x,der)==0): return x0
    xn = x-f(x,p)/f(x,der)
    while abs(x-xn)>e:
        x=xn
        xn=x-f(x,p)/f(x,der)
    return xn

def Gorner(p,x,y):
    while len(p)>1:
        for i in range(len(p[0])):
            p[0][i]= p[0][i] * x + p[1][i]
        p = p[:1] + p[2:]
    p = p[0]
    while len(p)>1:
        p[0] *= y
        p[1] += p[0]
        p = p[1:]
    return p

def Gorner2(p, x, y):
    k = Gorner(p, y, x)
    ans = float(k[0])
    return ans

#производная по Х
def DX(polinom):
    ans = [[0 for j in range(len(polinom[i]))] for i in range(len(polinom))]
    for i in range (len(polinom)):
        for j in range (len(polinom[i])):
            ans[i][j] = polinom[i][j] * (len(polinom[i]) - j - 1)
    return [[ans[i][j] for j in range(len(ans[i]) - 1)] for i in range(len(ans))]

#производная по Y
def DY(polinom):
    ans = [[0 for j in range(len(polinom[i]))] for i in range(len(polinom))]
    for i in range (len(polinom)):
       for j in range (len(polinom[i])):
            ans[i][j] = polinom[i][j] * (len(polinom) - i - 1)
    return ans[:-1]

#Задание №1
def Lab1():
    print("Введите коэффициенты полинома:")
    p = [float(i) for i in input().split()]
    print("Степень полинома:",len(p)-1)
    print("Полином:")
    PrintPolinom(p, 0)
    b = Derivative(p)
    print("Производная:")
    PrintPolinom(b, 1)

#Задание №2
def Lab2():
    print("Введите коэффициенты полинома:")
    p = [float(i) for i in input().split()]
    print("Степень полинома:",len(p)-1)
    print("Полином:")
    PrintPolinom(p, 0)
    print("Введите верхнюю границу:")
    up_in = float(input())
    up = up_in - 0.5
    down = -Border(p)
    korni = []
    while down <= up:
       if (f(up, p) * f(up_in, p) <= 0):
           x = Chord(up_in, up, p)
           x = round(x, 2)
           if x not in korni:
               korni.append(x)
           up_in = up
           up = up_in - 0.5
       up -= 0.5
    print("Корни полинома:", korni)
    print("Максимальный корень полинома:", max(korni))

#Задание №3
def Lab3():
    print("Введите коэффициенты полинома:")
    p = [float(i) for i in input().split()]
    print("Степень полинома:",len(p)-1)
    print("Полином:")
    PrintPolinom(p, 0)
    print("Введите верхнюю границу:")
    up_in = float(input())
    up = up_in - 0.5
    down = -Border(p)
    der = Derivative(p)
    korni = []
    while down <= up:
       if (f(up, p) * f(up_in, p) <= 0):
           x = Newton(up_in, up, p, der)
           x = round(x, 2)
           if x not in korni:
               korni.append(x)
           up_in = up
           up = up_in - 0.5
       up -= 0.5
    print("Корни полинома:", korni)
    print("Максимальный корень полинома:", max(korni))

#Задание №4
def Lab4():
    print("Введите матрицу (чтобы завершить ввод оставте последнюю строку пустой):")
    a = list(map(float, input().split()))
    p = []
    while a != []:
        p.append(a)
        a = list(map(float, input().split()))
    print("Введите точки x и y:")
    x,y=map(float,input().split())
    PrintMatrix(p, 0)
    k = Gorner(p, y, x)
    ans = float(k[0])
    print("f(x0,y0) = ", ans)

#Задание №5
def Lab5():
    print("Введите матрицу (чтобы завершить ввод оставте последнюю строку пустой):")
    a = list(map(float, input().split()))
    p = []
    while a != []:
        p.append(a)
        a = list(map(float, input().split()))
    dx = DX(p)
    dy = DY(p)
    PrintMatrix(p, 0)
    print("Частная производная по х:")
    PrintMatrix(dx, 1)
    print("Частная производная по y:")
    PrintMatrix(dy, 2)

#Задание №6
def Lab6():
    print("Введите 1 матрицу: ")
    a1 = list(map(float, input().split()))
    p1 = []
    while a1 != []:
        p1.append(a1)
        a1 = list(map(float, input().split()))
    print("Первый полином: ")
    PrintMatrix(p1, 0)
    print("Введите 2 матрицу: ")
    a2 = list(map(float, input().split()))
    p2 = []
    while a2 != []:
        p2.append(a2)
        a2 = list(map(float, input().split()))
    print("Второй полином: ")
    PrintMatrix(p2, 0)
    prib = [0, 0]
    toch = [0, 0]
    print("Введите приближенные х и у: ")
    for i in range (len(prib)):
        prib[i] = float(input())
    e = 0.0000001
    #производные функций
    Fdx = DX(p1)
    Fdy = DY(p1)
    Gdx = DX(p2)
    Gdy = DY(p2)
    #Якобиан
    J = Gorner2(Fdx, prib[0], prib[1]) * Gorner2(Gdy, prib[0], prib[1]) - Gorner2(Fdy, prib[0], prib[1]) * Gorner2(Gdx, prib[0], prib[1])
    #первое приближение к корню
    toch[0] = prib[0] - (1 / J) * (Gorner2(Gdy, prib[0], prib[1]) * Gorner2(p1, prib[0], prib[1]) - Gorner2(Fdy, prib[0], prib[1]) * Gorner2(p2, prib[0], prib[1]))
    toch[1] = prib[1] - (1 / J) * (Gorner2(Fdx, prib[0], prib[1]) * Gorner2(p2, prib[0], prib[1]) - Gorner2(p1, prib[0], prib[1]) * Gorner2(Gdx, prib[0], prib[1]))
    #продолжение приближения до заданной точности
    while abs(prib[0] - toch[0]) > e or abs(prib[1] - toch[1]) > e:
        prib = toch
        J = Gorner2(Fdx, prib[0], prib[1]) * Gorner2(Gdy, prib[0], prib[1]) - Gorner2(Fdy, prib[0], prib[1]) * Gorner2(Gdx, prib[0], prib[1])
        toch[0] = prib[0] - (1 / J) * (Gorner2(Gdy, prib[0], prib[1]) * Gorner2(p1, prib[0], prib[1]) - Gorner2(Fdy, prib[0], prib[1]) * Gorner2(p2, prib[0], prib[1]))
        toch[1] = prib[1] - (1 / J) * (Gorner2(Fdx, prib[0], prib[1]) * Gorner2(p2, prib[0], prib[1]) - Gorner2(p1, prib[0], prib[1]) * Gorner2(Gdx, prib[0], prib[1]))
    print("Найденный корень: " + str(round(toch[0], 3)) + " + " + str(round(toch[1], 3)) + "i")

#Меню
while True:
    print("\nВведите номер задания:")
    a = input()
    if a == '1': Lab1()
    elif a == '2': Lab2()
    elif a == '3': Lab3()
    elif a == '4': Lab4()
    elif a == '5': Lab5()
    elif a == '6': Lab6()
    elif a == '0': break
    else: print("Неверный номер. Повторите попытку, либо введите '0' для выхода из программы.")

'''
1:
1 1 1 1 1 1
3 7 -5 -2 4

2:
1 -4 -42 104 361 -420
10 42 -137 -604 -615 -100
1 -2 -39 148 -140
1 -13 47 -23 -48 36
1 -1 -3 -9 

4:
8 0 3 8
1 7 1 4

-3 2 3
2 -1 -4
2 2 4
1 -4 4

3.2 4.5
2.3 -4.5

5:
-1 4 3
1 3 -4
-4 1 -3

4 3 0 -3
1 1 3 -4

3 -4
0 -1
-2 -4
'''