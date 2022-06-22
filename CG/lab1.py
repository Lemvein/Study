#Вариант №2
import matplotlib.pyplot as plt
import numpy as np

print("Введите константу a:")
a = float(input())

#Масштабирование по центру
x1 = a*2
y1 = a*2

#Уравнение
step = 0.05
xrange = np.arange(-x1, y1, step)
yrange = np.arange(-x1, y1, step)
x, y = np.meshgrid(xrange, yrange)
result = (x**2+y**2)**2-(a**2*(x**2-y**2))

#Отрисовка
plt.contour(x, y, result, [0])
plt.grid(True)
plt.title('function: (x^2+y^2)^2=a^2*(x^2-y^2)')
plt.show()
