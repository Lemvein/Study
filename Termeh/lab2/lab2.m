%зависимости
t = 0:0.1:20;
x0 = 0.5;
x = x0-(0.025*t);
phi = 2*t;

%стол
TableX = [0,2,2,0,0];
TableY = [0,0,2,2,0];
TableZ = [0,0,0,0,0];
plot3(TableX,TableY,TableZ);

% Косметика окна
hold on
axis equal
xlim([0 2])
ylim([0 2])
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

