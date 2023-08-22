function A = Rot2D(Vers, phi)
    A(1,:) = Vers(1,:)*cos(phi)-Vers(2,:)*sin(phi);
    A(2,:) = Vers(1,:)*sin(phi)+Vers(2,:)*cos(phi);
end

