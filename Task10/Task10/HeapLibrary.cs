using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HeapLibrary
{
    internal class Heap<T> where T : IComparable
    {
        private List<T> massive=new List<T>();

        private void Heapify(int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < massive.Count() && massive[left].CompareTo(massive[largest])>0)
            {
                largest = left;
            }

            if (right < massive.Count() && massive[right].CompareTo(massive[largest]) > 0)
            {
                largest = right;
            }

            if (largest != i)
            {
                T number= massive[largest];
                massive[largest] = massive[i];
                massive[i] = number;
                Heapify(largest);
            }
        }

        private int Size()
        {
            return massive.Count();
        }

        private void Creation()
        {
            int n = massive.Count();
            for (int i = n/2+1;i>=0;i--)
            {
                Heapify(i);
            }
        }

        public Heap(T [] array) {
            foreach (T t in array) massive.Add(t);
            Creation();
        }

        public void Output()
        {
            int n= massive.Count();
            for(int i = 0; i < n; i++)
            {
                Console.Write(massive[i]+", ");
            }
        }

        public T Max()
        {
            return massive[0];
        }

        public T RemoveMax() {
            try
            {
                T number = massive[0];
                massive.RemoveAt(0);
                Creation();
                return number;
            }
            catch
            {
                Environment.Exit(0);
                return(default(T));
            }
        }

        public void Change(int index, T value) {
            if (value.CompareTo(massive[index]) > 0)
            {
                massive[index] = value;
                Creation();
            }
            else
            {
                Console.WriteLine("Новое число меньше предыдущего.");
            }
        }

        public void add(T value)
        {
            massive.Add(value);
            Creation();
        }

        public void merge(Heap<T> heap1)
        {
            for (int i=0; i < heap1.Size(); i++)
            {
                T number = heap1.RemoveMax();
                massive.Add(number);
            }
            Creation();
        }
    }
}
