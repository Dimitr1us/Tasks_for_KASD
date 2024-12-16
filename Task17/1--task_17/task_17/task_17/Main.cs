using Mylib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace task_17
{
    public partial class Main : Form
    {
        MyArrayList<int> list1;
        MyLInkedList<int> list2;
        int[] t = { 0 };
        private ZedGraphControl zedGraphControl;
        public Main()
        {
            InitializeComponent();
            zedGraphControl = new ZedGraphControl();
            zedGraphControl.Location = new Point(10, 10);
            zedGraphControl.Size = new Size(600, 400);
            Controls.Add(zedGraphControl);

            // Создание массива и двунаправленного списка
            list1 = new MyArrayList<int>(t);
            list2 = new MyLInkedList<int>(t);
        }
        
       

        

        private void button1_Click(object sender, EventArgs e)
        {
            zedGraphControl.GraphPane.CurveList.Clear();
            //PointPairList listArray = new PointPairList();
            //PointPairList listLinkedList = new PointPairList()
            GraphPane pane = zedGraphControl.GraphPane;

            const string gr1 = "Add";
            const string gr2 = "Set";
            const string gr3 = "Get";
            const string gr4 = "Remove";

            //LineItem curveArray = zedGraphControl.GraphPane.AddCurve("Массив", new PointPairList(), Color.Blue, SymbolType.None);
            //LineItem curveLinkedList = zedGraphControl.GraphPane.AddCurve("Двунаправленный список", new PointPairList(), Color.Red, SymbolType.None);

            PointPairList listArray = new PointPairList();
            PointPairList listLinkedList = new PointPairList();

            zedGraphControl.GraphPane.XAxis.Title.Text = "Количество элементов";
            zedGraphControl.GraphPane.YAxis.Title.Text = "Время выполнения (мс)";

            switch (comboBox1.SelectedItem.ToString())
            {
                
                case gr1:
                    for (int i = 0; i < 1000000; i++)
                    {
                        DateTime startArray = DateTime.Now;
                        list1.Add(i);
                        DateTime endArray = DateTime.Now;
                        TimeSpan durationArray = endArray - startArray;
                        listArray.Add(i, durationArray.TotalMilliseconds);

                        DateTime startLinkedList = DateTime.Now;
                        list2.AddLast(i);
                        DateTime endLinkedList = DateTime.Now;
                        TimeSpan durationLinkedList = endLinkedList - startLinkedList;
                        listLinkedList.Add(i, durationLinkedList.TotalMilliseconds);
                    }
                    break;
                case gr2:
                    for (int i = 0; i < 1000000; i++)
                    {
                        DateTime startArray = DateTime.Now;
                        list1.Set(i, i + 1);
                        DateTime endArray = DateTime.Now;
                        TimeSpan durationArray = endArray - startArray;
                        listArray.Add(i, durationArray.TotalMilliseconds);
                        DateTime startLinkedList = DateTime.Now;
                        list2.Set(i, i + 1);
                        DateTime endLinkedList = DateTime.Now;
                        TimeSpan durationLinkedList = endLinkedList - startLinkedList;
                        listLinkedList.Add(i, durationLinkedList.TotalMilliseconds);
                    }
                    break;
                case gr3:
                    for (int i = 0; i < 1000000; i++)
                    {
                        DateTime startArray = DateTime.Now;
                        list1.Add(i);
                        list1.Get(i);
                        DateTime endArray = DateTime.Now;
                        TimeSpan durationArray = endArray - startArray;
                        listArray.Add(i, durationArray.TotalMilliseconds);
                        DateTime startLinkedList = DateTime.Now;
                        list2.Add(i);
                        list2.Get(i);
                        DateTime endLinkedList = DateTime.Now;
                        TimeSpan durationLinkedList = endLinkedList - startLinkedList;
                        listLinkedList.Add(i, durationLinkedList.TotalMilliseconds);
                    }
                    break;
                case gr4:
                    for (int i = 0; i < 1000000; i++)
                    {
                        DateTime startArray = DateTime.Now;
                        list1.Add(i);
                        list1.Remove(i);
                        DateTime endArray = DateTime.Now;
                        TimeSpan durationArray = endArray - startArray;
                        listArray.Add(i, durationArray.TotalMilliseconds);
                        DateTime startLinkedList = DateTime.Now;
                        list2.Add(i);
                        list2.Remove(i);
                        DateTime endLinkedList = DateTime.Now;
                        TimeSpan durationLinkedList = endLinkedList - startLinkedList;
                        listLinkedList.Add(i, durationLinkedList.TotalMilliseconds);
                    }

                    break;
                default:
                    MessageBox.Show("Не были выбраны тестовые данные");
                    break;


            }
            /*if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Не были выбраны тестовые данные");
                return;
            }*/
           /* if (comboBox1.SelectedItem.ToString() == gr1)
            {
               

                for (int i = 0; i < 1000000; i++)
                {
                    DateTime startArray = DateTime.Now;
                    list1.Add(i);
                    DateTime endArray = DateTime.Now;
                    TimeSpan durationArray = endArray - startArray;
                    listArray.Add(i, durationArray.TotalMilliseconds);

                    DateTime startLinkedList = DateTime.Now;
                    list2.AddLast(i);
                    DateTime endLinkedList = DateTime.Now;
                    TimeSpan durationLinkedList = endLinkedList - startLinkedList;
                    listLinkedList.Add(i, durationLinkedList.TotalMilliseconds);
                }

                // Обновите данные на кривых:
               // curveArray.Points = listArray;
               // curveLinkedList.Points = listLinkedList;
            

            }*/
            /*else if (comboBox1.SelectedItem.ToString() == gr2)
            {

                for (int i = 0; i < 1000000; i++)
                {
                    DateTime startArray = DateTime.Now;
                    list1.Set(i, i + 1);
                    DateTime endArray = DateTime.Now;
                    TimeSpan durationArray = endArray - startArray;
                    listArray.Add(i, durationArray.TotalMilliseconds);
                    DateTime startLinkedList = DateTime.Now;
                    list2.Set(i, i + 1);
                    DateTime endLinkedList = DateTime.Now;
                    TimeSpan durationLinkedList = endLinkedList - startLinkedList;
                    listLinkedList.Add(i, durationLinkedList.TotalMilliseconds);
                }

                // Обновите данные на кривых:
                //curveArray.Points = listArray;
                //curveLinkedList.Points = listLinkedList;
                

            }*/
            /*else if (comboBox1.SelectedItem.ToString() == gr3)
            {
                
                for (int i = 0; i < 1000000; i++)
                {
                    DateTime startArray = DateTime.Now;
                    list1.Add(i);
                    list1.Get(i);
                    DateTime endArray = DateTime.Now;
                    TimeSpan durationArray = endArray - startArray;
                    listArray.Add(i, durationArray.TotalMilliseconds);
                    DateTime startLinkedList = DateTime.Now;
                    list2.Add(i);
                    list2.Get(i);
                    DateTime endLinkedList = DateTime.Now;
                    TimeSpan durationLinkedList = endLinkedList - startLinkedList;
                    listLinkedList.Add(i, durationLinkedList.TotalMilliseconds);
                }

                // Обновите данные на кривых:
                //curveArray.Points = listArray;
               // curveLinkedList.Points = listLinkedList;
              
            }*/
           /* else if (comboBox1.SelectedItem.ToString() == gr4)
            {
                for (int i = 0; i < 1000000; i++)
                {
                    DateTime startArray = DateTime.Now;
                    list1.Add(i);
                    list1.Remove(i);
                    DateTime endArray = DateTime.Now;
                    TimeSpan durationArray = endArray - startArray;
                    listArray.Add(i, durationArray.TotalMilliseconds);
                    DateTime startLinkedList = DateTime.Now;
                    list2.Add(i);
                    list2.Remove(i);
                    DateTime endLinkedList = DateTime.Now;
                    TimeSpan durationLinkedList = endLinkedList - startLinkedList;
                    listLinkedList.Add(i, durationLinkedList.TotalMilliseconds);
                }

                // Обновите данные на кривых:
                //curveArray.Points = listArray;
                //curveLinkedList.Points = listLinkedList;
                
            }*/


            LineItem myh = pane.AddCurve("Список", listArray, Color.Black, SymbolType.None);
            myh.Line.Width = 5;
            myh.Line.Color = Color.Black;
            myh.Color = Color.Black;


            LineItem myhh = pane.AddCurve("динам", listLinkedList, Color.Green, SymbolType.None);
            myhh.Line.Width = 5;
            myhh.Line.Color = Color.Green;
            myhh.Color = Color.Green;



            // Обновление графика
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }
    }
}
