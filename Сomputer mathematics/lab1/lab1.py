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
    result = Gorner(p,a)
    result.pop(len(p) - 1)
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

#Схема Горнера
def Gorner(polinom,a):
    b = [polinom[0]]
    for i in range(len(polinom) - 1):
        b.append(a * b[i] + polinom[i + 1])
    return b

#Поиск верхней границы
def BordersUp(polinom):
    if polinom[0] < 0: polinom = [i * -1 for i in polinom]
    for i in range(100000):
        b = Gorner(polinom, i)
        f = True
        for j in b:
            if j < 0:
                f = False
                break
        if f == True:
            break
    return i

#Поиск нижней границы
def BordersDown(polinom):
    polinom = [i * (-1) ** (len(polinom) - 1) for i in polinom]
    for i in range(len(polinom)):
        polinom[i] = polinom[i] * (-1) ** (len(polinom) - i - 1)
    for i in range(1000000):
        b = Gorner(polinom, i)
        f = True
        for j in b:
            if j < 0:
                f = False
                break
        if f == True:
            break
    return -1*i

#Метод дихотомии
def Dihotomy(polinom):
    a = BorderUp(polinom)
    b = BorderDown(polinom)

    return

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
