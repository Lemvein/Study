float rotX = 0.0, rotY = 0.0;
float distX = 0.0, distY = 0.0;
int lastX, lastY;
int scale_value = 100;
boolean fillStatus = false;
boolean projectionStatus = false;

void setup() {
  size(1000, 800, P3D);
}

void draw() {
  background(255);
  translate(width/2, width/2.5, height/4);

  if (projectionStatus) {
    ortho(-width/2, width/2, -height/2, height/2);
  } else {
    perspective();
  }

  rotateY(rotY + distX);
  rotateX(rotX + distY);

  scale((float)scale_value * 1.21 / (float)100);
  color c1 = color(200,50,100); 
  color c2 = color(0);
  Octahedron(100, c1, c2, fillStatus);
}

void Octahedron(int R, color c1, color c2, boolean filling) { //постороение фигуры
  int h = R; 
  if (filling) {
    fill(c1);
  } else {
    noFill();
  }
  stroke(c2);
  ArrayList<PVector> nodes = new ArrayList<PVector>();
  for (int i = 0; i <= 4; i++) {  //вычисляем координаты точек основания
    float xi = 0 + R * cos(0 + 2 * PI * i / 4);
    float yi = 0 + R * sin(0 + 2 * PI * i / 4);
    PVector v = new PVector(xi, yi, 0); //добавляем посчитанные координаты в вектор, а вектор затем в список векторов
    nodes.add(v);
  }
  for (int i = 0; i < nodes.size() - 1; i++) { //строим верхнюю половину
    PVector v = nodes.get(i);
    PVector vnext = nodes.get(i + 1);
    beginShape();
    vertex(vnext.x, vnext.y, 0);
    vertex(v.x, v.y, 0);
    vertex(0, 0, h);
    endShape();
  }

  for (int i = 0; i < nodes.size() - 1; i++) { //строим нижнюю половину
    PVector v = nodes.get(i);
    PVector vnext = nodes.get(i + 1);
    beginShape();
    vertex(vnext.x, vnext.y, 0);
    vertex(v.x, v.y, 0);
    vertex(0, 0, -h);
    endShape();
  }

}

void mousePressed() { //координаты мыши при нажатии на кнопку
  lastX = mouseX;
  lastY = mouseY;
}

void mouseDragged() { //вычисление разницы между начальным и текущим положением мыши
  distX = radians(mouseX - lastX);
  distY = radians(lastY - mouseY);
}

void mouseReleased() { //вычисление углов для поворота фигуры
  rotX += distY;
  rotY += distX;

  if (mouseButton == RIGHT && (distX == 0) && (distY == 0)) { //при разжатии правой кнопки мыши и неподвижной мыши нам надо залить фигуру
    fillStatus = (fillStatus) ? false : true;
  }
  if (mouseButton == LEFT && (distX == 0) && (distY == 0)) { //изменение проекции фигуры по нажатии на левую кнопку мыши
    projectionStatus = (projectionStatus) ? false : true;
  }
  distX = distY = 0.0;
}

void mouseWheel(MouseEvent event) { //изменение масштаба по движению колёсика мыши
  if (scale_value >= 1) {
    scale_value += event.getCount();
  }
}
