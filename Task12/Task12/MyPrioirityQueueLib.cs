using ComparatorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationLib;
using System.Threading.Tasks;
namespace MyPriorityQueueLib
{
    internal class MyPriorityQueue
    {
        private Application[] queue;
        private int size;
        private PriorityQueueCompare comparator;

        public MyPriorityQueue()
        {
            queue = new Application[11];
            size = 0;
            comparator = new PriorityQueueCompare();
        }
        public MyPriorityQueue(Application[] a)
        {
            queue = new Application[a.Length];
            for (int i = 0; i < a.Length;i++)
            {
                queue[i] = a[i];
            }
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(queue[i]);
            }
            size = a.Length;
            comparator = new PriorityQueueCompare();
            Creation();
        }
        public MyPriorityQueue(int initialCapacity)
        {
            queue = null;
            size = initialCapacity;
            comparator = new PriorityQueueCompare();
        }

        public MyPriorityQueue(int initialCapacity,PriorityQueueCompare queueCompare)
        {
            queue = null;
            size = initialCapacity;
            comparator = queueCompare;
        }

        public MyPriorityQueue(MyPriorityQueue c)
        {
            queue = new Application[c.size];
            size = c.size;
            for (int i = 0; i < size; i++) { Add(c.Peek()); }
            comparator = new PriorityQueueCompare();
            Creation();
        }


        private void Heapify(int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int result=0;
            if (left < size)
            {
                result = comparator.Compare(queue[left], queue[largest]);
            }
            if (left < size && result > 0)
            {
                largest = left;
            }
            result = 0;
            if (right < size)
            {
                result = comparator.Compare(queue[right], queue[largest]);
            }
            if (right < size && result > 0)
            {
                largest = right;
            }

            if (largest != i)
            {
                Application number = queue[largest];
                queue[largest] = queue[i];
                queue[i] = number;
                Heapify(largest);
            }
        }

        private void Creation()
        {
            for (int i = size / 2 + 1; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        private void Swap(int index1, int index2)
        {
            Application temp1 = queue[index1];
            queue[index1] = queue[index2];
            queue[index2] = temp1;
        }

        public void Add(Application e)
        {
            if (queue.Length < 64 && size == queue.Length)
            {
                Application[] array = new Application[(queue.Length * 2) + 1];
                for (int i = 0; i < size; i++)
                {
                    array[i] = queue[i];
                }
                array[size] = e;
                size = size + 1;
                queue = array;
                Creation();
            }
            else if (size == queue.Length)
            {
                Application[] array = new Application[(int)(queue.Length * 1.5) + 1];
                for (int i = 0; i < size; i++)
                {
                    array[i] = queue[i];
                }
                array[size] = e;
                queue = array;
                size = size + 1;
                Creation();
            }
            else {
                Application[] array = new Application[(int)(queue.Length)];
                for (int i = 0; i < size; i++)
                {
                    array[i] = queue[i];
                }
                array[size] = e;
                queue = array;
                size = size + 1;
                Creation();
            }
        }
        public void AddAll(Application[] a)
        {
            for (int i = 0; i < a.Length; i++) Add(a[i]);
        }

        public void Clear() => size = 0;
        public bool Contains(object o)
        {
            bool k = false;
            for (int i = 0; i < size; i++)
            {
                foreach (Application t in queue)
                {
                    if (Equals(t, o)) k = true;
                }
            }
            return k;
        }
        public bool ContainsAll(Application[] a)
        {
            bool k = false;
            for (int i = 0; i < a.Length; i++)
            {
                if (Contains(a[i])) k = true;
            }
            return k;
        }
        public bool IsEmpty()
        {
            if (size == 0) return true;
            else return false;
        }
        private int FindIndex(object o)
        {
            for (int i = 0; i < size; i++)
            {
                if (o.Equals(queue[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        public void Remove(object o)
        {
            if (Contains(o))
            {
                int k = FindIndex(o); // Индекс удаляемого элемента
                Application[] array = new Application[size - 1]; // Новый массив на 1 элемент меньше

                // Копируем элементы, пропуская удаляемый
                for (int i = 0, j = 0; i < size; ++i)
                {
                    if (i != k) // Если это не удаляемый элемент
                    {
                        array[j++] = queue[i]; // Копируем в новый массив
                    }
                }

                queue = array; // Присваиваем новый массив
                size--; // Уменьшаем размер
                Creation();
            }
        }
        public void RemoveAll(Application[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Remove(a[i]);
            }

        }
        public void RetainAll(Application[] a)
        {
            for (int i = 0; i < a.Length; i++) queue[i] = a[i];
            size = a.Length;
        }
        public int Size()
        {
            return size;
        }
        public Application[] ToArray()
        {
            Application[] array = new  Application[size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = queue[i];
            }
            return array;
        }
        public void ToArray(ref Application[] a)
        {
            for (int i = 0; i < size; i++)
            {
                Add(a[i]);
            }
            size += a.Length;
            Application[] array = new Application[size];
            for (int i = 0; i < array.Length; i++) array[i] = queue[i];
        }
        public Application Element()
        {
            return queue[0];
        }
        public bool Offer(Application obj)
        {
            try
            {
                Add(obj);
                return true;
            }
            catch { return false; }
        }
        public Application Peek()
        {
            if (size == 0) return default(Application);
            else return queue[0];
        }
        public Application Poll()
        {
            if (size == 0) return default(Application);
            else
            {
                Application q = queue[0];
                Remove(queue[0]);
                Creation();
                return q;
            }
        }
        public void Output()
        {
            for (int i = 0; i < size; i++) Console.WriteLine($"{queue[i].NumberOfApplication} {queue[i].NumberOfPriority} {queue[i].NumberOfStep}");
        }
    }
}
