using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
/*
        public static void SetStart(object sender, DataGridView dataView)
        {
            showSimulatedDataTable(dataView);

            model.settingStartNode = true;

            (sender as Button).Enabled = false;
        }

        public static void SetEnd(object sender)
        {
            model.settingEndNode = true;

            (sender as Button).Enabled = false;

            Console.WriteLine("Enable end node selecting");
        }

        public static void printPath(RichTextBox debugLog, int start, int finish, int[] path)
        {
            int currentNode = finish;
            string tracePath = "";
            while (currentNode != start)
            {
                tracePath += currentNode + " <- ";
                Model.Node.drawLineBetween(graphics, Color.Green, model.graphNodes[currentNode], model.graphNodes[path[currentNode]]);
                currentNode = path[currentNode];
            }
            Console.Write(start);
            tracePath += start;

            outputLog(debugLog, tracePath);
        }

        public static void outputLog(RichTextBox debugLog, string text)
        {
            debugLog.AppendText("\r\n" + text);
            debugLog.ScrollToCaret();
        }

        public static void drawNodes(bool drawLine)
        {
            graphics.Clear(Color.White);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (drawLine)
            {
                for (int i = 0; i < model.graphNodes.Count; i++)
                {
                    for (int j = 0; j < model.graphNodes.Count; j++)
                    {
                        if (model.weightBetween[i, j] != 0)
                        {
                            Model.Node.drawLineBetween(graphics, Color.Black, model.weightBetween[i, j], model.graphNodes[i], model.graphNodes[j]);
                        }
                    }
                }
            }

            foreach (Model.Node node in model.graphNodes)
            {
                node.Draw(graphics);
            }
        }

        public static void OneClick(object sender, EventArgs e, RichTextBox debugLog)
        {
            MouseEventArgs mouseEvent = (e as MouseEventArgs);
            Point mousePosition = new Point(mouseEvent.X, mouseEvent.Y);

            if (model.stopCreateNode == true)
            {
                if (model.settingStartNode == true)
                {
                    model.settingStartNode = false;
                    Model.Node closestNode = Model.Node.closestNodeTo(ref model.graphNodes, mousePosition);
                    model.startNode = closestNode.ID;

                    outputLog(debugLog, "Start node: " + model.startNode);

                    model.graphNodes[model.startNode].HighLight(graphics, Color.Yellow);

                }
                else if (model.settingEndNode == true)
                {
                    model.settingEndNode = false;
                    Model.Node closestNode = Model.Node.closestNodeTo(ref model.graphNodes, mousePosition);
                    model.endNode = closestNode.ID;
                    model.graphNodes[model.endNode].HighLight(graphics, Color.Red);

                    outputLog(debugLog, "End node: " + model.endNode);
                }
                return;
            }
            else
            {
                Model.Node newNode = new Model.Node(mousePosition, model.color);
                model.graphNodes.Add(newNode);

                drawNodes(false);
            }
        }

        public static void TwoClick(object sender, EventArgs e, RichTextBox debugLog, DataGridView dataView)
        {
            if (!model.stopCreateNode || model.graphNodes.Count == 0)
            {
                return;
            }
            MouseEventArgs mouseEvent = e as MouseEventArgs;
            Model.Node closestNode = Model.Node.closestNodeTo(ref model.graphNodes, new Point(mouseEvent.X, mouseEvent.Y));


            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            closestNode.Select(graphics);

            if (model.selectedNode.Count < 1)
            {
                model.selectedNode.Add(closestNode);
            }
            else
            {
                model.selectedNode.Add(closestNode);

                Model.Node.connectNodes(graphics, ref model.weightBetween, model.selectedNode[0], model.selectedNode[1]);

                outputLog(debugLog, "The distance between the nodes is determined: " + model.selectedNode[0].ID + " " + model.selectedNode[1].ID);

                drawNodes(true);

                model.selectedNode.Clear();
                showSimulatedDataTable(dataView);

            }
        }

        public static void showSimulatedDataTable(DataGridView dataView)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("NODES");
            for (int i = 0; i < model.graphNodes.Count; i++)
            { dataTable.Columns.Add("Node " + i); }

            for (int i = 0; i < model.graphNodes.Count; i++)
            {
                DataRow row;
                row = dataTable.NewRow();
                String rowName = "Node " + i;
                row["NODES"] = rowName;

                for (int j = 0; j < model.graphNodes.Count; j++)
                {
                    row["Node " + j] = String.Format("{0:0.0}", model.weightBetween[i, j]);
                }

                dataTable.Rows.Add(row);
            }

            dataView.DataSource = dataTable;
        }

        public static void Dijkstra(object sender, EventArgs e, RichTextBox debugLog)
        {

            int[] traceNode = new int[model.graphNodes.Count];
            double[] weightNode = new double[model.graphNodes.Count];
            int[] mark = new int[model.graphNodes.Count];
            int dijkstraStartNode = model.startNode;
            int dijkstraEndNode = model.endNode;

            outputLog(debugLog, "Start searching for the minimum path");
            for (int i = 0; i < model.graphNodes.Count; i++)
            {
                traceNode[i] = -1;
                mark[i] = 0;
                weightNode[i] = double.MaxValue;
            }
            traceNode[dijkstraStartNode] = 0; //Start node to start node = 0 distance right?
            weightNode[dijkstraStartNode] = 0;

            int connect;
            do
            {
                connect = -1;
                double minDistance = double.MaxValue;

                for (int ID = 0; ID < model.graphNodes.Count; ID++)
                {
                    if (mark[ID] == 0)
                    {
                        outputLog(debugLog, "Node " + ID + " research");
                        if (model.weightBetween[dijkstraStartNode, ID] != 0 &&
                        (weightNode[ID] > weightNode[dijkstraStartNode] + model.weightBetween[dijkstraStartNode, ID]))
                        {
                            outputLog(debugLog, "Checked the distance between " + dijkstraStartNode + " " + ID);
                            weightNode[ID] = weightNode[dijkstraStartNode] + model.weightBetween[dijkstraStartNode, ID];
                            traceNode[ID] = dijkstraStartNode;

                            Model.Node.drawLineBetween(graphics, Color.Blue, model.graphNodes[ID], model.graphNodes[dijkstraStartNode]);
                        }

                        if (minDistance > weightNode[ID])
                        {
                            outputLog(debugLog, "Found a way");
                            minDistance = weightNode[ID];
                            connect = ID;
                            //Node.drawLineBetween(graphics, Color.Green, graphNodes[connect], graphNodes[dijkstraStartNode]);

                        }
                    }

                }
                dijkstraStartNode = connect;
                mark[dijkstraStartNode] = 1;

                Thread.Sleep(1000);
            } while (connect != -1 && dijkstraStartNode != dijkstraEndNode);


            outputLog(debugLog, "Minimum path length between given nodes " + weightNode[model.endNode]);

            printPath(debugLog, model.startNode, model.endNode, traceNode);

            model.graphNodes[model.startNode].HighLight(graphics, Color.Yellow);
            model.graphNodes[model.endNode].HighLight(graphics, Color.Red);
        }

        public static void ValueChanged(object sender, DataGridViewCellEventArgs e, RichTextBox debugLog, DataGridView dataView)
        {
            double editedValue = Convert.ToDouble((sender as DataGridView).CurrentCell.Value);

            model.weightBetween[e.ColumnIndex - 1, e.RowIndex] = editedValue;
            model.weightBetween[e.RowIndex, e.ColumnIndex - 1] = editedValue;

            drawNodes(true);

            outputLog(debugLog, "The adjacency matrix is constructed");
            showSimulatedDataTable(dataView);
        }

        public static void finish()
        {
            model.stopCreateNode = true;
        }

        public static void colorNode(ColorDialog colorDialog)
        {
            DialogResult result = colorDialog.ShowDialog();
            switch (result)
            {
                case DialogResult.OK:
                    model.color = colorDialog.Color;
                    break;
            }
        }
*/
    }
}
