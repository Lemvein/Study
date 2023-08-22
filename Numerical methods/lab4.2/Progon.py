#Проверка на корректность
def Chek(a):
    n = len(a)
    k = 0
    for str in range(0, n):
        if( len(a[str]) != n ):
            print('Не соответствует размерность')
            return False
    for i in range(1, n - 1):
        if (abs(a[i][i]) >= abs(a[i][i-1]) + abs(a[i][i+1])):
            if (abs(a[i][i]) > abs(a[i][i-1]) + abs(a[i][i+1])): 
                k += 1      
        else:
            print('Не выполнены условия достаточности')
            return False
        if k == 0:
            print('Не выполнены условия достаточности')
            return False
    for stroka in range(0, len(a)):
        if( a[str][str] == 0 ):
            print('Нулевые элементы на главной диагонали')
            return False
    return True
 
#Метод прогонки
def Progon(A, B):
    if (not Chek(A)):
        print('Ошибка в исходных данных')
        return 0 
    n = len(A)
    x = [0 for k in range(0, n)]   
    #Прогоночные коэффициенты  
    a = [0 for k in range(0, n)]
    b = [0 for k in range(0, n)]
    a[0] = -A[0][1] / A[0][0] 
    b[0] = B[0] / A[0][0] 
    for i in range(1, n - 1):
        a[i] = -A[i][i+1] / ( A[i][i] + A[i][i-1]*a[i-1] )
        b[i] = (B[i] - A[i][i-1]*b[i-1] ) / ( A[i][i] + A[i][i-1]*a[i-1] )
    a[n-1] = 0
    b[n-1] = (B[n-1] - A[n-1][n-2]*b[n-2]) / (A[n-1][n-1] + A[n-1][n-2]*a[n-2])
    #Обратный ход
    x[n-1] = b[n-1] 
    for i in range(n-1, 0, -1):
        x[i-1] = a[i-1] * x[i] + b[i-1] 
    return x 

#Метод прогонки
def tridig_matrix_alg(A, b):
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