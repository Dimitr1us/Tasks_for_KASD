using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyIteratorLib;
using MyPriorityQueueLib;
namespace MyArrayDequeLib
{
    internal class MyArrayDeque<T>
    {
        T[] elements;
        int head;
        int tail;


        public MyArrayDeque()
        {
            elements = new T[16];
            tail = -1;
            head = 0;
        }


        public MyArrayDeque(T[] a)
        {
            elements = new T[a.Length];
            for (int i = 0; i < a.Length; i++)
                elements[i] = a[i];
            head = 0;
            tail = elements.Length - 1;
        }


        public MyArrayDeque(int cap)
        {
            elements = new T[cap];
            head = 0;
            tail = -1;
        }


        public void Add(T value)
        {
            if (tail + 1 == elements.Length)
            {
                T[] BiggerArray = new T[elements.Length * 2];
                for (int i = 0; i <= tail; i++)
                    BiggerArray[i] = elements[i];
                elements = BiggerArray;
            }
            elements[++tail] = value;
        }


        public void Clear()
        {
            tail = -1; head = 0;
            Array.Clear(elements);
        }


        public bool Contains(object o)
        {
            for (int i = 0; i <= tail; i++)
                if (elements[i].Equals(o))
                    return true;
            return false;
        }


        public bool ContainsAll(T[] a)
        {
            for (int i = 0; i <= tail; i++)
                if (!Contains(a[i]))
                    return false;
            return true;
        }


        public bool IsEmpty() => tail == -1;


        public void Remove(object o)
        {
            for (int i = 0; i <= tail; i++)
            {
                if (elements[i].Equals(o))
                {
                    if (i == tail)
                    {
                        tail--;
                        return;
                    }
                    for (int j = i; j < tail; j++)
                        elements[j] = elements[j + 1];
                    tail--;
                    return;
                }
            }

        }


        public void RemoveAll(T[] a)
        {
            foreach (T t in a)
                Remove(t);
        }


        public void RetainAll(T[] a)
        {
            for (int j = 0; j < elements.Length; j++)
            {
                for (int i = 0; i <= tail; i++)
                {
                    if (Contains(elements[i]) == false)
                        Remove(elements[i]);
                }
            }


            /*bool contains(T el)
            {
                for (int i = 0; i < a.Length; i++)
                    if (el.Equals(a[i]))
                        return true;
                return false;
            }*/
        }


        public int Size() { return tail + 1; }


        public T[] ToArray()
        {
            T[] RetArray = new T[tail + 1];
            for (int i = 0; i <= tail; i++)
            {
                RetArray[i] = elements[i];
            }
            return RetArray;
        }


        public T[] ToArray(T[] a)
        {
            if (tail == -1)
                throw new IndexOutOfRangeException();


            T[] RetArray = new T[a.Length + tail + 1];
            int i = 0;
            for (; i < a.Length; i++)
            {
                RetArray[i] = a[i];
            }
            for (int j = 0; j <= tail; j++, i++)
            {
                RetArray[i] = elements[j];

            }
            return RetArray;

        }


        public T Element()
        {
            return elements[head];
        }


        public bool Offer(object b)
        {
            if (tail == elements.Length - 1)
                return false;
            Add((T)b);
            return true;
        }


        public T? Peek()
        {
            if (tail == -1)
                return default(T);
            return elements[head];
        }


        public T? Poll()
        {
            if (tail == -1)
                return default(T);
            T el = elements[head];
            head++;
            return el;
        }


        public void AddFirst(T el)
        {
            if (tail + 1 == elements.Length)
                ReSize();
            elements[tail + 1] = elements[tail];
            for (int i = tail; i >= 1; i--)
            {
                elements[i] = elements[i - 1];

            }
            elements[0] = el;
            tail++;
        }


        public void AddLast(T el)
        {
            Add(el);
        }


        public T GetFirst()
        {
            return elements[head];

        }

        public T GetLast()
        {
            return elements[tail];
        }


        public bool OfferFirst(T obj)
        {
            if (tail + 1 > elements.Length)
                return false;
            Add(obj);
            return true;
        }


        public bool OfferLast(T obj)
        {
            if (tail + 1 > elements.Length)
                return false;
            AddFirst(obj);
            return true;
        }


        public T Pop()
        {
            T el = elements[head];
            for (int i = 0; i < tail; i++)
                elements[i] = elements[i + 1];
            return el;
        }


        public void Push(T el)
        {
            AddFirst(el);

        }


        public T PeekFirst()
        {
            if (tail == -1)
                return default(T);
            return elements[head];
        }


        public T PeekLast()
        {
            if (tail == -1)
                return default(T);
            return elements[tail];
        }

        public T PollLast()
        {
            if (tail == -1)
                return default(T);
            T el = elements[tail];
            tail--;
            return el;
        }

        public T RemoveLast()
        {
            if (tail == -1)
                throw new IndexOutOfRangeException();
            T el = elements[tail];
            tail--;
            return el;
        }

        public T PollFirst()
        {
            if (tail == -1)
                return default(T);
            T el = elements[head];
            for (int i = 0; i < tail; i++)
                elements[i] = elements[i + 1];
            return el;
        }

        public T RemoveFirst()
        {
            if (tail == -1)
                throw new IndexOutOfRangeException();
            T el = elements[head];
            for (int i = 0; i < tail; i++)
                elements[i] = elements[i + 1];
            return el;
        }


        public bool RemoveLastOccurence(object obj)
        {
            T el = (T)obj;

            int? remIndex = null;
            for (int i = 0; i <= tail; i++)
            {
                if (elements[i].Equals(el))
                    remIndex = i;
            }
            if (remIndex == null)
                return false;
            if (remIndex == tail)
            {
                tail--;
                return true;
            }


            for (int j = (int)remIndex; j < tail; j++)
            {
                elements[j] = elements[j + 1];

            }
            tail--;

            return true;
        }


        public bool RemoveFirstOccurence(object obj)
        {
            T el = (T)obj;
            for (int i = 0; i <= tail; i++)
                if (el.Equals(elements[i]))
                {
                    if (i == tail)
                    {
                        tail--;
                        return true;
                    }
                    for (int j = i; j < tail; j++)
                        elements[j] = elements[j + 1];
                    tail--;
                    return true;
                }

            return false;
        }

        private void ReSize()
        {
            T[] values = new T[elements.Length * 2];
            for (int i = 0; i <= tail; i++)
                values[i] = elements[i];
            elements = values;
        }


        public void Output()
        {
            for (int i = head; i <= tail; i++)
            {
                Console.Write($"{elements[i]} ");
            }
            Console.WriteLine();
        }

        public iMyIterator2<T> ListIterator()
        {
            return new MyItr(0, this);
        }

        internal class MyItr : iMyIterator2<T>
        {
            private int cursor;
            private MyArrayDeque<T> deque;
            public bool HasNext()
            {
                if (cursor < deque.Size()) return true;
                else return false;
            }

            public T Next()
            {
                if (!HasNext()) throw new InvalidOperationException("Нет следующего элемента.");
                return deque.elements[cursor++];
            }


            public void Remove()
            {
                deque.Remove(cursor);
            }


            public MyItr(int index, MyArrayDeque<T> deque)
            {
                cursor = index;
                this.deque = deque;
            }
        }
    }
}