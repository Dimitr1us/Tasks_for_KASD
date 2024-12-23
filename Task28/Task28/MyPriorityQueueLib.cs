using ComparatorLib;
using MyArrayListLibrary;
using MyInterFaceLib;
using MyIteratorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
namespace MyPriorityQueueLib
{
    internal class MyPriorityQueue<T>: MyQueue<T>
    {
        private T[] queue;
        private int Size;
        private PriorityQueueCompare comparator;

        public MyPriorityQueue()
        {
            queue = new T[11];
            Size = 11;
        }
        public MyPriorityQueue(MyCollection<T> C)
        {
            T[] a = C.toArray();
            queue = new T[a.Length];
            for (int i = 0; i < a.Length;i++)
            {
                queue[i] = a[i];
            }
            for (int i = 0; i < Size; i++)
            {
                Console.WriteLine(queue[i]);
            }
            Size = a.Length;
            Creation();
        }
        public MyPriorityQueue(int initialCapacity)
        {
            queue = new T[initialCapacity];
            Size = initialCapacity;
        }
        public MyPriorityQueue(int initialCapacity, PriorityQueueCompare comparator)
        {
            queue = new T[initialCapacity];
            Size = initialCapacity;
            this.comparator = comparator;
        }
        public MyPriorityQueue(MyPriorityQueue<T> c)
        {
            queue = new T[c.Size];
            Size = c.Size;
            for (int i = 0; i < Size; i++) { add(c.peek()); }
            Creation();
        }

        private void Heapify(int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int result = 0;
            comparator = new PriorityQueueCompare();
            if (left < Size)
            {
                result = comparator.Compare(queue[left], queue[largest]);
            }
            if (left < Size && result > 0)
            {
                largest = left;
            }
            result = 0;
            if (right < Size)
            {
                result = comparator.Compare(queue[right], queue[largest]);
            }
            if (right < Size && result > 0)
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
            for (int i = Size / 2 + 1; i >= 0; i--)
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

        public void add(T e)
        {
            if (queue.Length < 64 && Size == queue.Length)
            {
                T[] array = new T[(queue.Length * 2) + 1];
                for (int i = 0; i < Size; i++)
                {
                    array[i] = queue[i];
                }
                array[Size] = e;
                Size = Size + 1;
                queue = array;
                Creation();
            }
            else if (Size == queue.Length)
            {
                T[] array = new T[(int)(queue.Length * 1.5) + 1];
                for (int i = 0; i < Size; i++)
                {
                    array[i] = queue[i];
                }
                array[Size] = e;
                queue = array;
                Size = Size + 1;
                Creation();
            }
            else
            {
                T[] array = new T[(int)(queue.Length)];
                for (int i = 0; i < Size; i++)
                {
                    array[i] = queue[i];
                }
                array[Size] = e;
                queue = array;
                Size = Size + 1;
                Creation();
            }
        }
        public void addAll(MyCollection<T> C)
        {
            T[] a =C.toArray();
            for (int i = 0; i < a.Length; i++) add(a[i]);
        }

        public void clear() => Size = 0;
        public bool contains(T a)
        {
            bool k = false;
            for (int i = 0; i < Size; i++)
            {
                foreach (T t in queue)
                {
                    if (Equals(t, a)) k = true;
                }
            }
            return k;
        }
        public bool containsAll(T[] a)
        {
            bool k = false;
            for (int i = 0; i < a.Length; i++)
            {
                if (contains(a[i])) k = true;
            }
            return k;
        }
        public bool isEmpty()
        {
            if (Size == 0) return true;
            else return false;
        }
        internal int FindIndex(object o)
        {
            for (int i = 0; i < Size; i++)
            {
                if (o.Equals(queue[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        public void remove(T a)
        {
            if (contains(a))
            {
                int k = FindIndex(a);
                T[] array = new T[Size--];
                for (int i = 0; i < k; i++) array[i] = queue[i];
                for (int i = k + 1; i < Size--; i++) array[i] = queue[i];
                queue = array;
                Size--;
                Creation();
            }

        }
        public void removeAll(MyCollection<T> C)
        {
            T[] a=C.toArray();
            for (int i = 0; i < a.Length; i++)
            {
                remove(a[i]);
            }

        }
        public void retainAll(MyCollection<T> C)
        {
            T[] a =C.toArray();
            for (int i = 0; i < a.Length; i++) queue[i] = a[i];
            Size = a.Length;
        }
        public int size()
        {
            return Size;
        }
        public T[] toArray()
        {
            T[] array = new T[Size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = queue[i];
            }
            return array;
        }
        public T[] toArray(T[] a)
        {
            for (int i = 0; i < Size; i++)
            {
                add(a[i]);
            }
            Size += a.Length;
            T[] array = new T[Size];
            for (int i = 0; i < array.Length; i++) array[i] = queue[i];
            return array;
        }
        public T element()
        {
            return queue[0];
        }
        public bool offer(T obj)
        {
            try
            {
                add(obj);
                return true;
            }
            catch { return false; }
        }
        public T peek()
        {
            if (Size == 0) return default(T);
            else return queue[0];
        }
        public T poll()
        {
            if (Size == 0) return default(T);
            else
            {
                T q = queue[0];
                remove(queue[0]);
                return q;
            }
        }
        public void Output()
        {
            for (int i = 0; i < Size; i++) Console.WriteLine(queue[i]);
        }

        public iMyIter<T> ListIterator()
        {
            return new MyItr(0, this);
        }

        internal class MyItr : iMyIter<T>
        {
            private int cursor;
            private MyPriorityQueue<T> queue;
            public bool HasNext()
            {
                if (cursor < queue.Size) return true;
                else return false;
            }

            public T Next()
            {
                if (!HasNext()) throw new InvalidOperationException("Нет следующего элемента.");
                return queue.queue[cursor++];
            }

            
            public void Remove()
            {

                queue.remove(queue.queue[cursor]);
            }


            public MyItr(int index, MyPriorityQueue<T> queue)
            {
                cursor = index;
                this.queue = queue;
            }
        }

    }
}
