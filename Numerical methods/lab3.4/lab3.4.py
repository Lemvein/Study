x0 = 1
xi = [-1, 0, 1, 2, 3]
yi = [2.3562, 1.5708, 0.7854, 0.46365, 0.32175]

def Derivative1(x, y, x0, i):
    num1 = (y[i + 1] - y[i]) / (x[i + 1] - x[i])
    num2 = (y[i + 2] - y[i + 1]) / (x[i + 2] - x[i + 1]) - num1
    num2 = num2 / (x[i + 2] - x[i])
    result = num1 + num2 * (2 * x0 - x[i] - x[i + 1])
def Derivative2(x, y, i):
    num1 = (y[i + 2] - y[i + 1]) / (x[i + 2] - x[i + 1])
    num2 = (y[i + 1] - y[i]) / (x[i + 1] - x[i])
    result = 2 * (num1 - num2) / (x[i + 2] - x[i])
    print('Первый способ:', result)

def Derivative22(y, i):
    result = y[i] - 2 * y[i+1] + y[i+2]
    print('Второй способ:', result)

#Меню
while True:
    print("\nЗадания:")
    print("1. Первая производная")
    print("2. Вторая производная")
    print("\nВведите номер задания:")
    a = input()
    k = 0
    for i, j in zip(xi, xi[1:]):
        if i <= x0 <= j:
            break
        k += 1
    if a == '1': 
        Derivative1(xi, yi, x0, k)
    elif a == '2': 
        Derivative2(xi, yi, k)
        Derivative22(yi, k)
    elif a == '0': break
    else: print("Неверный номер. Повторите попытку, либо введите '0' для выхода из программы.")