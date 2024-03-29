function yt = f(t,y)
%   представим систему уравнений движения в виде
%   линейной относительно вторых производных
%                 системы   A*q'' = B, где
%                 q = (x; phi), A = A(x, phi), 
%                 B = B(x, phi, x', phi'):
%
%                 a11*x'' + a12*phi'' = b1
%                 a21*x'' + a22*phi'' = b2

global m1 m2 g k;    % требующиеся параметры

 yt(1)=y(3);           % тривиальные
 yt(2)=y(4);           % уравнения
 
 a12=0;   % коэффициенты уравнений движения
 a11=m1+m2;            
 b1=m1*y(1)*(y(4))^2-k*y(3)-m2*g; 
                    
 a21=0;          
 a22=m1*y(1);           
 b2=-2*y(3)*y(4)*m1-k*y(1)*y(4);
 
 A=[a11 a12;a21 a22];  %   решим систему 
 A1=[b1 a12;b2 a22];   %   линейных уравнений
 A2=[a11 b1;a21 b2];   %   методом Крамера
 
yt(3)=det(A1)/det(A);
yt(4)=det(A2)/det(A);
yt=yt';                %  в ответ выдаём строку
