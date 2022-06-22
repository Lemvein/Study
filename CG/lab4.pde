float rotX = 0.0, rotY = 0.0;
float distX = 0.0, distY = 0.0;
int lastX, lastY;
int scale_value = 100;
int a = 50, b = 80, h = 250;
float last = 0;
PVector point, zero, curr;
boolean fillStatus = true;
HScrollbar hs1, hs2;

void setup() {
  size(1000, 800, P3D); 
  zero = new PVector(0, 0, 0);  
  
  hs1 = new HScrollbar(0, 10, width, 16, 16);
  hs2 = new HScrollbar(0, 30, width, 16, 16);    
}

void draw() {
  background(255);
  if (hs1.getPos() != last) {
    last = hs1.getPos();
  }
  hs1.update();
  hs1.display();
  hs2.update();
  hs2.display();  
  
  pushMatrix();  
  translate(width/2, width/3, height/4);
  directionalLight(167, 243, 255, width/2, width/3, height/4); //направленное освещение
  ambientLight(102, 102, 102); //освещение внешней среды (рассеянный свет)
  
  ortho(-width/2, width/2, -height/2, height/2);
    
  rotateY(rotY + distX);
  rotateX(rotX + distY);

  scale((float)scale_value * 1.21 / (float)100);
  color c1 = color(0);
  color c2 = color(100, 200, 220);  
  int sides = (int)(hs1.getPos()); //параметр, задающий мелкость разбиения
  while (360 % sides != 0){
   sides++;
  }
  int light = (int)(hs2.getPos());
  emissive(light); //свойство материала - количество отражаемого света
  shininess(10);  
  drawCylinder(sides, a, b, h, c1, c2, fillStatus, zero);
  popMatrix();  
}

void drawCylinder( int sides, float a, float b, float h, color c1, color c2, boolean filling, PVector zero)
{
    if (filling) {
      fill(c2);
    } else {
      noFill();
    }
    
    strokeWeight(1);
    stroke(c1);
    ArrayList<PVector> nodes = new ArrayList<PVector>();
    ArrayList<PVector> nodes2 = new ArrayList<PVector>();    
    float angle = 360 / sides;
    float halfHeight = h / 2;

    for (int i = 0; i < sides; i++) {
        float x = a*cos( radians( i * angle ) );
        float y = b*sin( radians( i * angle ) );
        PVector v1 = new PVector(x + zero.x, y + zero.y, halfHeight); //добавление точек в список точек
        nodes.add(v1);
        PVector v2 = new PVector(x + zero.x, y + zero.y, -halfHeight);
        nodes2.add(v2);          
    }   

    // рисуем верх цилиндра
    beginShape();              
    for (int i = 0; i < nodes.size() - 1; i++) {
      PVector v1 = nodes.get(i);
      PVector v1next = nodes.get(i + 1);
      vertex(v1.x, v1.y, v1.z);         
      vertex(v1next.x, v1next.y, v1next.z);   
    }
    endShape(CLOSE);         

    // рисуем низ цилиндра
    beginShape();          
    for (int i = 0; i < nodes2.size() - 1; i++) {
      PVector v2 = nodes2.get(i);
      PVector v2next = nodes2.get(i + 1); 
      vertex(v2.x, v2.y, v2.z);       
      vertex(v2next.x, v2next.y, v2next.z);         
    }
    endShape(CLOSE);         
    
    // рисуем боковую часть
    beginShape(TRIANGLE_STRIP);       
    for (int i = 0; i < nodes.size() - 1; i++) {
      PVector v1 = nodes.get(i);
      PVector v1next = nodes.get(i + 1);
      PVector v2 = nodes2.get(i);
      PVector v2next = nodes2.get(i + 1);
      vertex(v1.x, v1.y, v1.z); 
      vertex(v2.x, v2.y, v2.z);                 
      vertex(v1next.x, v1next.y, v1next.z);      
      vertex(v2next.x, v2next.y, v2next.z);          
    }    
    PVector v1 = nodes.get(nodes.size() - 1);
    PVector v1next = nodes.get(0);
    PVector v2 = nodes2.get(nodes.size() - 1);
    PVector v2next = nodes2.get(0);
    vertex(v1.x, v1.y, v1.z); 
    vertex(v2.x, v2.y, v2.z);                 
    vertex(v1next.x, v1next.y, v1next.z);      
    vertex(v2next.x, v2next.y, v2next.z);         
    endShape(CLOSE);          
}


void mousePressed() {
  lastX = mouseX;
  lastY = mouseY;
}

void mouseDragged() {
  distX = radians(mouseX - lastX);
  distY = radians(lastY - mouseY);
}

void mouseReleased() {
  rotX += distY;
  rotY += distX;

  distX = distY = 0.0;
}

void mouseWheel(MouseEvent event) {
  if (scale_value >= 1) {
    scale_value -= event.getCount();
  }
}

class HScrollbar {
  int swidth, sheight;    // ширина и высота ползунков
  float xpos, ypos;       
  float spos, newspos;    
  float sposMin, sposMax; // максимальное и минимальное значения ползунков
  int loose;             
  boolean over;          
  boolean locked;
  float ratio;

  HScrollbar (float xp, float yp, int sw, int sh, int l) {
    swidth = sw;
    sheight = sh;
    int widthtoheight = sw - sh;
    ratio = (float)16 / (float)widthtoheight;
    xpos = xp;
    ypos = yp-sheight/2;
    spos = xpos + swidth/2 - sheight/2;
    newspos = spos;
    sposMin = xpos;
    sposMax = xpos + swidth - sheight;
    loose = l;
  }

  void update() {
    if (overEvent()) {
      over = true;
    } else {
      over = false;
    }
    if (mousePressed && over) {
      locked = true;
    }
    if (!mousePressed) {
      locked = false;
    }
    if (locked) {
      newspos = constrain(mouseX-sheight/2, sposMin, sposMax);
    }
    if (abs(newspos - spos) > 1) {
      spos = spos + (newspos-spos)/loose;
    }
  }

  float constrain(float val, float minv, float maxv) {
    return min(max(val, minv), maxv);
  }

  boolean overEvent() {
    if (mouseX > xpos && mouseX < xpos+swidth &&
      mouseY > ypos && mouseY < ypos+sheight) {
      return true;
    } else {
      return false;
    }
  }

  void display() {
    noStroke();
    fill(210);
    rect(xpos, ypos, swidth, sheight);
    if (over || locked) {
      fill(0, 0, 0);
    } else {
      fill(0, 0, 255);
    }
    rect(spos, ypos, sheight, sheight);
  }

  float getPos() {
    return spos * ratio;
  }
}
