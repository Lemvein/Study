global m1 m2 k  g  % Параметры наших
% уравнений динамики делаем глобальными,
% чтобы передать их в Функцию уравнений.
m1 = 4;   % Задаём значения параметров 
m2 = 1;  % всякие массы, длины, 
k = 1;
g = 9.81;

tstep = 0.01;  % Задаём сетку по времени
tfin = 15;     % В эти моменты времени нам
tout = 0:tstep:tfin; % выдадут результат
                     % интегрирования
                     
y0 = [0.5 0 2 0.2];  % Задаём начальные
          % условия для наших переменных
% y1_0, y2_0, y3_0, y4_0
% x,    phi,  x',   phi'
   
[t,y] = ode45(@f,tout,y0); % вызываем
% решатель уравнений. Его аргументы:
% f - f(t,y) - функция с уравнениями,
% по t и y выдаёт y';
% tout - сетка по времени - моменты
% времени, в которые нам выдадут
% значения y;
% y0 - вектор начальных значений.
% Результат: t = tout,
% y - матрица. Первый столбец
% y(:,1) - s
% y(:,2) - phi
% y(:,3) - s'
% y(:,4) - phi'

X = y(:,1);     % Запишем результат
Phi = y(:,2);   % интегрирования в
Xt = y(:,3);    % удобные понятные
Phit = y(:,4);  % переменные

for i=1:length(t)   % Для вычисления нужных
  Res = f(t(i),y(i,:));  % реакций требуются
  Xtt(i,1) = Res(3);    % ещё x" 
end       


N = m1*(X.*(Phit.^2)-Xtt)-k.*Xt;

figure         % Построим графики зависимостей
subplot(4,1,1) % координат и реакции от времени.
plot(t,X)      % Команда сабплот рисует несколько
title('X(t) при k=0'); % отдельных графиков в одном окне.
subplot(4,1,2) % subplot(m,n,k): окно разбито на 
plot(t,Phi)    % m "строк", n "столбцов", 
title('Phi(t) при k=0'); % k - номер активного окна, куда
subplot(4,1,3) % сейчас будем рисовать
plot(t,N)      
title('N(t) при k=0');


%зависимости
x = X;
phi = Phi;

%стол
figure
TableX = [5,5,5,5,5];
TableY = [5,5,5,5,5];
TableZ = [0,0,0,0,0];
plot3(TableX,TableY,TableZ);

% Косметика окна
hold on
axis equal
xlim([-1 3])
ylim([-1 3])
zlim([-1 0])
xlim manual
ylim manual
zlim manual

%точки
xO = 1;
yO = 1;
zO = 0;
plot3(xO,yO,zO,'k.','markersize',20);
xA = xO-x.*cos(phi);
yA = yO-x.*sin(phi);
zA = 0;
A = plot3(xA(1),yA(1),zA,'k.','markersize',50)
xB = 1;
yB = 1;
zB = -1+(x);
B = plot3(xB,yB,zB(1),'k.','markersize',50)

%нити
OA = plot3([xO xA(1)],[yO yA(1)],[zO zA]);
OB = plot3([xO xB],[yO yB],[zO zB(1)]);

for i=1:length(t)
    set(A,'XData',xA(i),'YData',yA(i),'ZData',zA);
    set(B,'XData',xB,'YData',yB,'ZData',zB(i));
    set(OA,'XData',[xO xA(i)],'YData',[yO yA(i)],'ZData',[zO zA]);
    set(OB,'XData',[xO xB],'YData',[yO yB],'ZData',[zO zB(i)]);    
    pause(0.05);
end

