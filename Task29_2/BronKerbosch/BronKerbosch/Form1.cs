using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BronKerboschLib;
namespace BronKerbosch
{
    public partial class Form1 : Form
    {
        int index = 1;
        Random random = new Random();
        private Dictionary<string, Point> nodePositions = new Dictionary<string, Point>();
        private List<Tuple<string, string>> edges = new List<Tuple<string, string>>();
        public Form1()
        {
            InitializeComponent();
            Size = new Size(900, 600);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int CoordX = 0;
            int CoordY = 0;
            GenerationOfCords(ref CoordX, ref CoordY);
            nodePositions.Add(Convert.ToString(index), new Point(CoordX, CoordY));
            index++;
            this.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            edges.Add(new Tuple<string, string>(textBox1.Text, textBox2.Text));
            this.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
              BronKerboschGraph graph = new BronKerboschGraph();
            foreach (String point in nodePositions.Keys)
            {
                graph.AddVertex(Convert.ToInt32(point));
            }
            foreach (Tuple<string, string> tuple in edges)
            {
                graph.AddEdge(Convert.ToInt32(tuple.Item1), Convert.ToInt32(tuple.Item2));
            }
            HashSet<int> nodes = graph.FindMaxClique();
            edges.Clear();
            int [] array = new int[nodes.Count];
            nodes.CopyTo(array, 0);
            for (int i = 0; i < array.Count(); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (i != j)
                    {
                        edges.Add(new Tuple<string, string>(array[i].ToString(), array[j].ToString()));
                    }
                }
            }
            this.Invalidate();
        }
        private void GenerationOfCords(ref int cordX, ref int cordY)
        {
            bool check;
            while (true)
            {
                cordX = random.Next(160, 840);
                cordY = random.Next(60, 540);
                check = true;
                foreach (Point point in nodePositions.Values)
                {
                    if (Math.Sqrt(Math.Abs(cordX - point.X) * Math.Abs(cordX - point.X) + Math.Abs(cordY - point.Y) * Math.Abs(cordY - point.Y)) <= 40)
                    {
                        check = false;
                        break;
                    }
                }
                if (check == true)
                {
                    break;
                }

            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 2);
            Brush brush = new SolidBrush(Color.LightBlue);


            //Рисуем вершины (узлы)
            foreach (KeyValuePair<string, Point> kvp in nodePositions)
            {
                g.FillEllipse(brush, kvp.Value.X - 20, kvp.Value.Y - 20, 40, 40);
                g.DrawString(kvp.Key, this.Font, Brushes.Black, kvp.Value);
            }

            //Рисуем ребра (связи)
            foreach (Tuple<string, string> edge in edges)
            {
                Point start = nodePositions[edge.Item1];
                Point end = nodePositions[edge.Item2];
                g.DrawLine(pen, start, end);

                float arrowAngle = (float)(Math.PI / 6); // 30 градусов
                float arrowLength = 10; // Длина наконечника стрелки

                // Создаем точки для наконечника стрелки
                PointF arrowPoint1 = new PointF(
                    end.X - arrowLength * (float)Math.Cos(Math.Atan2(end.Y - start.Y, end.X - start.X) + arrowAngle),
                    end.Y - arrowLength * (float)Math.Sin(Math.Atan2(end.Y - start.Y, end.X - start.X) + arrowAngle)
                );

                PointF arrowPoint2 = new PointF(
                    end.X - arrowLength * (float)Math.Cos(Math.Atan2(end.Y - start.Y, end.X - start.X) - arrowAngle),
                    end.Y - arrowLength * (float)Math.Sin(Math.Atan2(end.Y - start.Y, end.X - start.X) - arrowAngle)
                );

                // Рисуем наконечник стрелки
                g.DrawLine(pen, end, arrowPoint1);
                g.DrawLine(pen, end, arrowPoint2);

                start = nodePositions[edge.Item2];
                end = nodePositions[edge.Item1];
                g.DrawLine(pen, start, end);

                arrowAngle = (float)(Math.PI / 6); // 30 градусов
                arrowLength = 10; // Длина наконечника стрелки

                // Создаем точки для наконечника стрелки
                arrowPoint1 = new PointF(
                    end.X - arrowLength * (float)Math.Cos(Math.Atan2(end.Y - start.Y, end.X - start.X) + arrowAngle),
                    end.Y - arrowLength * (float)Math.Sin(Math.Atan2(end.Y - start.Y, end.X - start.X) + arrowAngle)
                );

                arrowPoint2 = new PointF(
                    end.X - arrowLength * (float)Math.Cos(Math.Atan2(end.Y - start.Y, end.X - start.X) - arrowAngle),
                    end.Y - arrowLength * (float)Math.Sin(Math.Atan2(end.Y - start.Y, end.X - start.X) - arrowAngle)
                );

                // Рисуем наконечник стрелки
                g.DrawLine(pen, end, arrowPoint1);
                g.DrawLine(pen, end, arrowPoint2);
            }

            pen.Dispose();
            brush.Dispose();
        }
    }
}
