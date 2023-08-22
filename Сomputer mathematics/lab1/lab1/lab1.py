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
def PrintPolinom(p):
    s = "p(x) = "
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

#Задание №1
def Lab1():
    print("Введите коэффициенты полинома:")
    p = [float(i) for i in input().split()]
    print("Степень полинома:",len(p)-1)
    print("Полином:")
    PrintPolinom(p)
    print("Введите число a:")
    a = float(input())
    result = Gorner(p,a)	
    print(round(result[len(result) - 1], 3))

#Задание №2
def Lab2():
    print("Введите коэффициенты полинома:")
    p = [float(i) for i in input().split()]
    print("Степень полинома:",len(p)-1)
    print("Полином:")
    PrintPolinom(p)
    print("Введите число a:")
    a = float(input())
    result = Root(p,a)
    print(result)

#Задание №3
def Lab3():
    print("Введите коэффициенты полинома:")
    p = [float(i) for i in input().split()]
    print("Степень полинома:",len(p)-1)
    print("Полином:")
    PrintPolinom(p)
    print("Введите число a:")
    a = float(input())
    result2 = []
    for i in range(len(p)):
        result = Gorner(p,a)
        result2.append(result[-1])
        p = result[0:len(result)-1]
    result2.reverse()
    print("Найденный полином:")
    PrintPolinom(result2)

#Задание №4
def Lab4():
    print("Введите коэффициенты полинома:")
    p = [float(i) for i in input().split()]
    print("Степень полинома:",len(p)-1)
    print("Полином:")
    PrintPolinom(p)
    up = BordersUp(p)
    print("Верхняя граница: ",up)
    down = BordersDown(p)
    print("Нижняя граница: ",down)

#Задание №5
def Lab5():
    print("Введите коэффициенты полинома:")
    p = [float(i) for i in input().split()]
    print("Степень полинома:",len(p)-1)
    print("Полином:")
    PrintPolinom(p)
    dih = Dihotomy(p)
    print("Корни: ", sep='')
    for i in range(0, len(dih)):
            print(round(dih[i], 1), end=' ')
    print()


#Схема Горнера
def Gorner(polinom,a):
    b = [polinom[0]]
    for i in range(len(polinom) - 1):
        b.append(a * b[i] + polinom[i + 1])
    return b

#Частное от деления полинома
def Root(polinom, a):
    result = Gorner(polinom,a)
    result.pop(len(polinom) - 1)
    return result

#Поиск верхней границы
def BordersUp(polinom):
    j=0
    for i in range(len(polinom)*10):
        z = Gorner(polinom, i)
        for j in z:
            if j < 0:
                break
        if j > 0:
            j = i
            break
    return j

#Поиск нижней границы
def BordersDown(polinom):
    n = polinom[:]
    polinom.reverse()
    for i in range(0, len(polinom)):
        if ((i % 2) == 0):
            n[i]=polinom[i]
        else:
            n[i]=polinom[i]*(-1)
    n.reverse()
    polinom.reverse()

    if (len(n)-1) % 2 != 0:
        for i in range(0, len(n)):
            n[i] = n[i]*(-1)
    k = 0
    k=BordersUp(polinom)
    return -k

#Значение функции в точке
def f(p, n):
    rez = Gorner(p, n)
    return rez[len(rez) - 1]

#Метод дихотомии
def Dihotomy(polinom):
    epsilon = 0.001;
    result = []
    for i in range(len(polinom)-1):
        a = BordersUp(polinom)
        b = BordersDown(polinom)
        if f(polinom, 0) == 0:
            result.append(0)
            polinom = Root(polinom, 0)
            continue
        elif f(polinom, a) == 0:
            result.append(a)
            polinom = Root(polinom, a)
            continue
        elif f(polinom, b) == 0:
            result.append(a)
            polinom = Root(polinom, a)
            continue
        while abs(b-a) >= epsilon:
            middle = (a + b) / 2
            if f(polinom, middle) == 0:
                break
            elif (f(polinom, a) * f(polinom, middle)) < 0:
                b = middle
            else:
                a = middle
        result.append(middle)
        polinom = Root(polinom, middle)
    return result

#Меню
while True:
    print("\nВведите номер задания:")
    a = input()
    if a == '1': Lab1()
    elif a == '2': Lab2()
    elif a == '3': Lab3()
    elif a == '4': Lab4()
    elif a == '5': Lab5()
    elif a == '0': break
    else: print("Неверный номер. Повторите попытку, либо введите '0' для выхода из программы.")
