t = 0:0.01:20;
x0 = 3;
x = x0+cos(3*t);
phi = -20*sin(3*t);

h = 4;
L = 9;
R = 0.25;
a = 4;
b = 1;
l = 3;
N = 10;
Nv = 3;
r1 = b/3;
r2 = b;

% Косметика окна
figure % принудительное создание окна
xlim([-1 10])
ylim([-2 6])
xlim manual
ylim manual
axis equal
hold on

plot([0 0],[0 h]);  %стена
plot([0 L],[0 0]);  %земля

TelegsX = [0 0 a a 0];
TelegsY = [2.5*R 2.5*R+b 2.5*R+b 2.5*R 2.5*R]
Telega = plot(TelegsX+x(1),TelegsY);

xA = x+a/2;
yA = 2.5*R+b/2;
xB = xA+l*sin(phi);
yB = yA+l*cos(phi);
PointA = plot(xA(1),yA,'o');
PointB = plot(xB(1),yB(1),'o','markersize',20,'markerFaceColor',[0 1 0]);
AB = plot([xA(1) xB(1)],[yA yB(1)]);

p = 0:0.1:6.3;
Xk0 = R*cos(p);
Yk0 = R*sin(p);
K1 = plot(Xk0+x(1)+a/5,Yk0+R);
K2 = plot(Xk0+x(1)+4*a/5,Yk0+R);

V0x = [a/5-R/2 a/5 a/5+R/2];
V0y = [2.5*R R 2.5*R];
V1 = plot(V0x+x(1),V0y);
V2 = plot(V0x+x(1)+3*a/5,V0y);

for i=1:N
   Xpr(i) = (i-1)*1/(N-1);
   if i==1
       Ypr(i) = 2.5*R+b/2;
   elseif i==N
       Ypr(i) = 2.5*R+b/2;
   else
       Ypr(i) = 2.5*R+b/2+b/4*(-1)^i;
   end 
end

Pruzzhina = plot(Xpr*x(1),Ypr);

alpha = 0:0.01:2*pi*Nv-phi(1);
RPr = r1+(r2-r1)*alpha/(2*pi*Nv-phi(1));
XSPr = -RPr.*sin(alpha);
YSPr = RPr.*cos(alpha);
SPruzzhina = plot(XSPr+xA(1),YSPr+yA);

for i=l:length(t)
    set (Telega,'Xdata',TelegsX+x(i));
    set (PointA,'Xdata',xA(i));
    set (PointB,'Xdata',xB(i),'Ydata',yB(i));
    set (AB,'Xdata',[xA(i) xB(i)],'Ydata',[yA yB(i)]);
    set (K1,'Xdata',Xk0+x(i)+a/5);
    set (K2,'Xdata',Xk0+x(i)+4*a/5);
    set (V1,'Xdata',V0x+x(i));
    set (V2,'Xdata',V0x+x(i)+3*a/5);
    set (Pruzzhina,'Xdata',Xpr*x(i));

    alpha = 0:0.01:2*pi*Nv-phi(i);
    RPr = r1+(r2-r1)*alpha/(2*pi*Nv-phi(i));
    XSPr = -RPr.*sin(alpha);
    YSPr = RPr.*cos(alpha); 
    set (SPruzzhina,'Xdata',XSPr+xA(i),'Ydata',YSPr+yA);

    pause(0.01);
end











