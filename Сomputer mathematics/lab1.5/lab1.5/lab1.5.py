
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

#Поиск верхней границы
def BorderUp(p):
    j = 0
    for i in range(len(p)*10):
        z = Gorner(p, i)
        for j in z:
            if j < 0:
                break
        if j > 0:
            j = i
            break
    return j

#Поиск нижней границы
def BorderDown(p):
    n = p[:]
    p.reverse()
    for i in range(0, len(p)):
        if ((i % 2) == 0):
            n[i]=p[i]
        else:
            n[i]=p[i]*(-1)
    n.reverse()
    p.reverse()

    if (len(n)-1) % 2 != 0:
        for i in range(0, len(n)):
            n[i] = n[i]*(-1)
    k = 0
    k=BorderUp(n)
    return -k

#Схема Горнера
def Gorner(p, j):
    p_new = [p[0]]
    for i in range (len(p) - 1):
        p_new.append(j * p_new[i] + p[i+1])
    return p_new

#Значение функции в точке
def f(p, j):
    p = Gorner(p, j)
    return p[len(p) - 1]

#Частное от деления полинома
def remove(p, j):
    p = Gorner(p, j)
    p.pop(len(p) - 1)
    return p

#Метод дихотомии
def Dichotomy(p):
    e = 0.001
    root = []
    j = 0
    for i in range(len(p)-1):
        a = BorderDown(p)
        b = BorderUp(p)
        if f(p, 0) == 0:
            root.append(0)
            p = remove(p, 0)
            continue
        if f(p, a) == 0:
            root.append(a)
            p = remove(p, a)
            continue
        if f(p, b) == 0:
            root.append(b)
            p = remove(p, b)
            continue
        while abs(b-a) >= e:
            middle = (a + b) / 2
            if f(p, middle) == 0:
                break
            elif (f(p, a) * f(p, middle)) < 0:
                b = middle
            else:
                a = middle
        root.append(middle)
        p = remove(p, middle)
    return root

print("Введите коэффициенты полинома:")
p = [float(i) for i in input().split()]
print("Степень полинома:",len(p)-1)
print("Полином:")
PrintPolinom(p)
dih = Dichotomy(p)
print("Корни: ")
for i in range(0, len(dih)):
    print(round(dih[i], 1), end=' ')
print()

'''
1 -7 7 15 0 0 0
4 0 -95 75 226 -120
1 3 -14 -30 49 27 -36
'''