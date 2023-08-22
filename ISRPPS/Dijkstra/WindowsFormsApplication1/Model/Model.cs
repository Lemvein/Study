using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class Base
    {
        public List<Node> graphNodes { get; set; }
        public List<Node> selectedNode { get; set; }

        public bool stopCreateNode { get; set; }
        public bool settingStartNode { get; set; }
        public bool settingEndNode { get; set; }

        public double[,] weightBetween { get; set; }
        public int startNode { get; set; }

        public Color color { get; set; }
    }

        public class Node
        {
            public Point position;
            public int ID;
            public bool isSelected = false;

            public static int nodeID = 0;
            Pen pen;

            public Node(Point position, Color color)
            {
                this.position = position;
                pen = new Pen(color, 20);
                ID = nodeID;

                nodeID++;
            }

            public void DeleteLast(Point position)
            {
                pen = new Pen(Color.White, 20);
                nodeID--;
            }


            public void Draw(Graphics graphics)
            {
                graphics.DrawEllipse(pen, position.X, position.Y, 20, 20); //draw node circle

                Font drawFont = new Font("Arial", 16);
                SolidBrush drawBrush = new SolidBrush(Color.White);
                StringFormat drawFormat = new StringFormat();
                graphics.DrawString(ID.ToString(), drawFont, drawBrush, position.X, position.Y, drawFormat);
                drawFont.Dispose();
                drawBrush.Dispose();
            }

            public void HighLight(Graphics graphics, Color color)
            {
                Pen pen = new Pen(color, 20);
                graphics.DrawEllipse(pen, position.X, position.Y, 20, 20); //draw node circle

                Font drawFont = new Font("Arial", 16);
                SolidBrush drawBrush = new SolidBrush(Color.White);
                StringFormat drawFormat = new StringFormat();
                graphics.DrawString(ID.ToString(), drawFont, drawBrush, position.X, position.Y, drawFormat);
                drawFont.Dispose();
                drawBrush.Dispose();
            }

            public void UnSelect(Graphics graphics)
            {
                isSelected = false;
                Draw(graphics);
            }

            public void Select(Graphics graphics)
            {
                isSelected = true;
                Draw(graphics);

                Pen selectedCircle = new Pen(Color.White, 5);
                graphics.DrawEllipse(selectedCircle, position.X, position.Y, 20, 20);
            }

            public static double distanceFrom(Node node1, Node node2)
            {
                float dX = node2.position.X - node1.position.X;
                float dY = node2.position.Y - node1.position.Y;

                return Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));
            }

            public static double distanceFrom(Node node1, Point point2)
            {
                float dX = point2.X - node1.position.X;
                float dY = point2.Y - node1.position.Y;

                return Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));
            }

            public static void drawLineBetween(Graphics graphics, Color color, Node node1, Node node2)
            {
                Pen pen = new Pen(color, 3);
                graphics.DrawLine(pen, node1.position, node2.position);

                node1.Draw(graphics);
                node2.Draw(graphics);
            }

            public static void drawLineBetween(Graphics graphics, Color color, double weight, Node node1, Node node2)
            {
                Pen pen = new Pen(color, 3);
                graphics.DrawLine(pen, node1.position, node2.position);

                node1.Draw(graphics);
                node2.Draw(graphics);


                Point middlePoint = new Point((node1.position.X + node2.position.X) / 2, (node1.position.Y + node2.position.Y) / 2);

                Font drawFont = new Font("Arial", 16);
                SolidBrush drawBrush = new SolidBrush(Color.DeepPink);
                StringFormat drawFormat = new StringFormat();

                graphics.DrawString(String.Format("{0:0.0}", weight), drawFont, drawBrush, middlePoint.X, middlePoint.Y, drawFormat);
                drawFont.Dispose();
                drawBrush.Dispose();

            }

            public static void connectNodes(Graphics graphics, ref double[,] weightBetween, Node node1, Node node2)
            {
                Pen linePen = new Pen(Color.Black, 1);
                graphics.DrawLine(linePen, node1.position, node2.position);

                node1.Draw(graphics); //Overlay to hide the line
                node2.Draw(graphics); //Overlay to hide the line



                weightBetween[node1.ID, node2.ID] = distanceFrom(node1, node2);
                weightBetween[node2.ID, node1.ID] = weightBetween[node1.ID, node2.ID];

                Point middlePoint = new Point((node1.position.X + node2.position.X) / 2, (node1.position.Y + node2.position.Y) / 2);

                Font drawFont = new Font("Arial", 16);
                SolidBrush drawBrush = new SolidBrush(Color.DeepPink);
                StringFormat drawFormat = new StringFormat();

                graphics.DrawString(String.Format("{0:0.0}", weightBetween[node1.ID, node2.ID]), drawFont, drawBrush, middlePoint.X, middlePoint.Y, drawFormat);
                drawFont.Dispose();
                drawBrush.Dispose();

            }

            public static Node closestNodeTo(ref List<Node> graphNodes, Point point)
            {
                Node closestNode = graphNodes[0];
                double minDistance = Node.distanceFrom(closestNode, point);
                foreach (Node node in graphNodes)
                {
                    double currentDistance = Node.distanceFrom(node, point);
                    if (currentDistance < minDistance)
                    {
                        minDistance = currentDistance;
                        closestNode = node;
                    }
                }

                return closestNode;
            }
        }
}
