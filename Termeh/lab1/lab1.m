% Массивы и зависимости
t = 0:0.01:10;
r = 2+sin(6*t);
phi = 5*t+0.2*cos(6*t);
x = r.*cos(phi);
y = r.*sin(phi);

Vx = diff(x); % массив на 1 короче, чем предыдущий
Vy = diff(y);
Wx = diff(Vx); % массив на 1 короче, чем предыдущий
Wy = diff(Vy);
k = 25;
k1 = 500;
Vphi = atan2(Vy,Vx);
Wphi = atan2(Wy,Wx);

% Косметика окна
figure % принудительное создание окна
xlim([1.7*min(x) 1.7*max(x)])
ylim([1.7*min(y) 1.7*max(y)])
xlim manual
ylim manual
axis equal
hold on

% Рисовашки
plot(x,y)
P = plot(x(1),y(1),'o','markersize',20,'markerfacecolor',[0 1 0])
Vline = plot([x(1) x(1)+k*Vx(1)],[y(1) y(1)+k*Vy(1)], 'color', [1 0 0])
Wline = plot([x(1) x(1)+k1*Wx(1)],[y(1) y(1)+k1*Wy(1)], 'color', [0 0 1])


ARR = [-0.25 0 -0.25; 0.125 0 -0.125];
RotARR = Rot2D(ARR,Vphi(1));
VArrow = plot(RotARR(1,:)+x(1)+k*Vx(1),RotARR(2,:)+y(1)+k*Vy(1),'color', [1 0 0]);

RotARR = Rot2D(ARR,Wphi(1));
WArrow = plot(RotARR(1,:)+x(1)+k1*Wx(1),RotARR(2,:)+y(1)+k1*Wy(1),'color', [0 0 1]);


for i=1:length(t)
    set(P,'XData',x(i),'YData',y(i));
    set(Vline,'XData',[x(i) x(i)+k*Vx(i)],'YData',[y(i) y(i)+k*Vy(i)]);
    set(Wline,'XData',[x(i) x(i)+k1*Wx(i)],'YData',[y(i) y(i)+k1*Wy(i)]);

    RotARR = Rot2D(ARR,Vphi(i));
    set(VArrow,'XData',RotARR(1,:)+x(i)+k*Vx(i),'YData',RotARR(2,:)+y(i)+k*Vy(i));
    RotARR = Rot2D(ARR,Wphi(i));
    set(WArrow,'XData',RotARR(1,:)+x(i)+k1*Wx(i),'YData',RotARR(2,:)+y(i)+k1*Wy(i));
    pause(0.02);
end




