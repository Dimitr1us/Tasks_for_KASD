using ComparatorLib;
using MyArrayListLibrary;
using MyIteratorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
namespace MyPriorityQueueLib
{
    internal class MyPriorityQueue<T>
    {
        private T[] queue;
        private int size;
        private PriorityQueueCompare comparator;

        public MyPriorityQueue()
        {
            queue = new T[11];
            size = 11;
        }
        public MyPriorityQueue(T[] a)
        {
            queue = new T[a.Length];
            for (int i = 0; i < a.Length;i++)
            {
                queue[i] = a[i];
            }
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(queue[i]);
            }
            size = a.Length;
            Creation();
        }
        public MyPriorityQueue(int initialCapacity)
        {
            queue = new T[initialCapacity];
            size = initialCapacity;
        }
        public MyPriorityQueue(int initialCapacity, PriorityQueueCompare comparator)
        {
            queue = new T[initialCapacity];
            size = initialCapacity;
            this.comparator = comparator;
        }
        public MyPriorityQueue(MyPriorityQueue<T> c)
        {
            queue = new T[c.size];
            size = c.size;
            for (int i = 0; i < size; i++) { Add(c.Peek()); }
            Creation();
        }

        private void Heapify(int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int result = 0;
            comparator = new PriorityQueueCompare();
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
                T number = queue[largest];
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
            T temp1 = queue[index1];
            queue[index1] = queue[index2];
            queue[index2] = temp1;
        }

        public void Add(T e)
        {
            if (queue.Length < 64 && size == queue.Length)
            {
                T[] array = new T[(queue.Length * 2) + 1];
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
                T[] array = new T[(int)(queue.Length * 1.5) + 1];
                for (int i = 0; i < size; i++)
                {
                    array[i] = queue[i];
                }
                array[size] = e;
                queue = array;
                size = size + 1;
                Creation();
            }
            else
            {
                T[] array = new T[(int)(queue.Length)];
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
        public void AddAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++) Add(a[i]);
        }

        public void Clear() => size = 0;
        public bool Contains(object o)
        {
            bool k = false;
            for (int i = 0; i < size; i++)
            {
                foreach (T t in queue)
                {
                    if (Equals(t, o)) k = true;
                }
            }
            return k;
        }
        public bool ContainsAll(T[] a)
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
        internal int FindIndex(object o)
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
                int k = FindIndex(o);
                T[] array = new T[size--];
                for (int i = 0; i < k; i++) array[i] = queue[i];
                for (int i = k + 1; i < size--; i++) array[i] = queue[i];
                queue = array;
                size--;
                Creation();
            }

        }
        public void RemoveAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Remove(a[i]);
            }

        }
        public void RetainAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++) queue[i] = a[i];
            size = a.Length;
        }
        public int Size()
        {
            return size;
        }
        public T[] ToArray()
        {
            T[] array = new T[size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = queue[i];
            }
            return array;
        }
        public void ToArray(ref T[] a)
        {
            for (int i = 0; i < size; i++)
            {
                Add(a[i]);
            }
            size += a.Length;
            T[] array = new T[size];
            for (int i = 0; i < array.Length; i++) array[i] = queue[i];
        }
        public T Element()
        {
            return queue[0];
        }
        public bool Offer(T obj)
        {
            try
            {
                Add(obj);
                return true;
            }
            catch { return false; }
        }
        public T Peek()
        {
            if (size == 0) return default(T);
            else return queue[0];
        }
        public T Poll()
        {
            if (size == 0) return default(T);
            else
            {
                T q = queue[0];
                Remove(queue[0]);
                return q;
            }
        }
        public void Output()
        {
            for (int i = 0; i < size; i++) Console.WriteLine(queue[i]);
        }

        public iMyIterator2<T> ListIterator()
        {
            return new MyItr(0, this);
        }

        internal class MyItr : iMyIterator2<T>
        {
            private int cursor;
            private MyPriorityQueue<T> queue;
            public bool HasNext()
            {
                if (cursor < queue.size) return true;
                else return false;
            }

            public T Next()
            {
                if (!HasNext()) throw new InvalidOperationException("Нет следующего элемента.");
                return queue.queue[cursor++];
            }

            
            public void Remove()
            {
                queue.Remove(cursor);
            }


            public MyItr(int index, MyPriorityQueue<T> queue)
            {
                cursor = index;
                this.queue = queue;
            }
        }

    }
}
