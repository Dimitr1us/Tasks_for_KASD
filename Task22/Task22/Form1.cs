using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyHashMapLib;
using MyTreeMapLib;
using ZedGraph;
namespace Task22
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Размер отображения, шт";
            pane.YAxis.Title.Text = "Время выполнения, мс";
            pane.Title.Text = "Зависимость времени от количества элементов в отображениях";
        }

        /*private List<double[]> TimeOfOperation(int methodIndex, int minSize, int maxSize, int step)
        {
            List<double[]> result = new List<double[]>();
            for (int i = minSize; i <= maxSize; i =i*step)
            {   Random rand = new Random();
                MyHashMap<int,int> hashMap = new MyHashMap<int,int>(rand.Next(1,i));
                MyTreeMap<int,int> treeMap = new MyTreeMap<int,int>();
                int[,] array = new int[i,2];
                
                // Заполняем массив случайными числами
                for (int j = 0; j < i; j++)
                {
                    array[j, 0] = rand.Next(0, 1000);
                    array[j,1]=rand.Next(0, 1000);
                    hashMap.Put(array[j,0], array[j,1]);
                    treeMap.put(array[j,0],array[j,1]);
                }


                double timeHashMap = 0;
                double timeTreeMap = 0;
                for (int j = 0; j < 10; j++)
                {
                    int index;
                    int key;
                    Stopwatch sw = new Stopwatch();
                    switch (methodIndex)
                    {
                        case 0:
                            key=rand.Next(0,i);
                            int value=rand.Next(0,i);
                            sw.Start();
                            hashMap.Put(key,value);
                            sw.Stop();
                            timeHashMap += sw.ElapsedMilliseconds;
                            sw = new Stopwatch();
                            sw.Start();
                            treeMap.put(key,value);
                            sw.Stop();
                            timeTreeMap += sw.ElapsedMilliseconds;
                            break;
                        case 1:
                            index=rand.Next(0,i);
                            key=array[index,0];
                            sw.Start();
                            hashMap.get(index);
                            sw.Stop();
                            timeHashMap += sw.ElapsedMilliseconds;
                            sw.Start();
                            treeMap.get(index);
                            sw.Stop();
                            timeTreeMap += sw.ElapsedMilliseconds;
                            break;
                        case 2:
                            index = rand.Next(0,i);
                            key=array[index,0];
                            sw.Start();
                            hashMap.Remove(key);
                            sw.Stop();
                            timeHashMap += sw.ElapsedMilliseconds;
                            sw.Start();
                            treeMap.Remove(key);
                            sw.Stop();
                            timeTreeMap += sw.ElapsedMilliseconds;
                            break;
                        default: throw new ArgumentException("Не существует данного метода");
                    }
                }
                result.Add(new double[] { timeHashMap, timeTreeMap});
            }
            return result;
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            int minSize = 10;
            int maxSize = 1000000;
            int step = 10;
            //List<double[]> time = TimeOfOperation(comboBox1.SelectedIndex, minSize, maxSize, step);


            int methodIndex = comboBox1.SelectedIndex;
            List<double[]> result = new List<double[]>();
            Random rand = new Random();
            for (int i = minSize; i <= maxSize; i = i * step)
            {
                MyHashMap<int, int> hashMap = new MyHashMap<int, int>(rand.Next(1, i));
                MyTreeMap<int, int> treeMap = new MyTreeMap<int, int>();
                int[,] array = new int[i, 2];

                // Заполняем массив случайными числами
                for (int j = 0; j < i; j++)
                {
                    array[j, 0] = rand.Next(0, 1000);
                    array[j, 1] = rand.Next(0, 1000);
                    hashMap.Put(array[j, 0], array[j, 1]);
                    treeMap.put(array[j, 0], array[j, 1]);
                }


                double timeHashMap = 0;
                double timeTreeMap = 0;
                for (int j = 0; j < 20; j++)
                {
                    int index;
                    int key;
                    Stopwatch sw = new Stopwatch();
                    switch (methodIndex)
                    {
                        case 0:
                            key = rand.Next(0, i);
                            int value = rand.Next(0, i);
                            sw.Start();
                            hashMap.Put(key, value);
                            sw.Stop();
                            timeHashMap += sw.Elapsed.TotalMilliseconds;
                            sw = new Stopwatch();
                            sw.Start();
                            treeMap.put(key, value);
                            sw.Stop();
                            timeTreeMap += sw.Elapsed.TotalMilliseconds;
                            break;
                        case 1:
                            index = rand.Next(0, i);
                            key = array[index, 0];
                            sw.Start();
                            hashMap.get(index);
                            sw.Stop();
                            timeHashMap += sw.Elapsed.TotalMilliseconds;
                            sw.Start();
                            treeMap.get(index);
                            sw.Stop();
                            timeTreeMap += sw.Elapsed.TotalMilliseconds;
                            break;
                        case 2:
                            index = rand.Next(0, i);
                            key = array[index, 0];
                            sw.Start();
                            hashMap.Remove(key);
                            sw.Stop();
                            timeHashMap += sw.Elapsed.TotalMilliseconds;
                            sw.Start();
                            treeMap.Remove(key);
                            sw.Stop();
                            timeTreeMap += sw.Elapsed.TotalMilliseconds;
                            break;
                        default: throw new ArgumentException("Не существует данного метода");
                    }
                }
                result.Add(new double[] { timeHashMap/20, timeTreeMap/20 });
            }



            PointPairList pointHash = new PointPairList();
            PointPairList pointTree = new PointPairList();
            for (int i = 1; i < result.Count; i++)
            {
                pointHash.Add(10*Math.Pow(10,i), result[i][0]);
                pointTree.Add(10*Math.Pow(10,i), result[i][1]);
            }



            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.AddCurve("HashMap", pointHash, Color.Red, SymbolType.Default);
            pane.AddCurve("TreeMap", pointTree, Color.Blue, SymbolType.Plus);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
