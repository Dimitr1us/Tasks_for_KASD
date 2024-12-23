using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInterFaceLib;
using MyIteratorLib;
using MyPriorityQueueLib;
namespace MyArrayDequeLib
{
    internal class MyArrayDeque<T>: MyList<T>, MyDeque<T>
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


        public MyArrayDeque(MyCollection<T> C)
        {
            T[] a = C.toArray();
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


        public void add(T value)
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

        public void addAll(MyCollection<T> C) {
            T[] a = C.toArray();
            foreach (T b in a)
            {
                add(b);
            }
        }

        public void clear()
        {
            tail = -1; head = 0;
            Array.Clear(elements);
        }


        public bool contains(T o)
        {
            for (int i = 0; i <= tail; i++)
                if (elements[i].Equals(o))
                    return true;
            return false;
        }


        public bool containsAll(T[] a)
        {
            for (int i = 0; i <= tail; i++)
                if (!contains(a[i]))
                    return false;
            return true;
        }


        public bool isEmpty() => tail == -1;


        public void remove(T o)
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

        public T remove(int index)
        {
            remove(elements[index]);
            return default(T);
        }

        public T[] subList(int from, int to)
        {
            T[] a= new T[0];
            int n = elements.Length;
            for (int i = 0; from < n; from++)
            {
                if (i>=from && i<to)  a.Append(elements[i]);
            }
            return a;
        } 

        public void removeAll(MyCollection<T> C)
        {
            T[] a = C.toArray();
            foreach (T t in a)
                remove(t);
        }

        public void set(int index, T o)
        {
            elements[index] = o;
        }

        public void retainAll(MyCollection<T> C)
        {
            T[] a =C.toArray();
            for (int j = 0; j < elements.Length; j++)
            {
                for (int i = 0; i <= tail; i++)
                {
                    if (contains(elements[i]) == false)
                        remove(elements[i]);
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


        public int size() { return tail + 1; }


        public T[] toArray()
        {
            T[] RetArray = new T[tail + 1];
            for (int i = 0; i <= tail; i++)
            {
                RetArray[i] = elements[i];
            }
            return RetArray;
        }


        public void add(int index,T a)
        {
            elements[index] = a;
        }

        public void addAll(int index, MyCollection<T> C)
        {
            T[] a = C.toArray();
            foreach (T t in a) add(t);
        }

        public T[] toArray(T[] a)
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


        public T element()
        {
            return elements[head];
        }


        public bool offer(object b)
        {
            if (tail == elements.Length - 1)
                return false;
            add((T)b);
            return true;
        }


        public T? peek()
        {
            if (tail == -1)
                return default(T);
            return elements[head];
        }


        public T? poll()
        {
            if (tail == -1)
                return default(T);
            T el = elements[head];
            head++;
            return el;
        }


        public void addFirst(T el)
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


        public void addLast(T el)
        {
            add(el);
        }

        public int indexOf(T a)
        {
            int index = 0;
            foreach (T t in elements)
            {
                
                if (t.Equals(a)) return index;
                index++;
            }
            return -1;
        }

        public int lastIndexOf(T a)
        {
            int index = 0;
            int found = -1;
            foreach (T t in elements)
            {

                if (t.Equals(a)) found = index;
                index++;
            }
            return found;
        }

        public T get(int index) {
            return elements[index];
        }

        public T getFirst()
        {
            return elements[head];

        }

        public T getLast()
        {
            return elements[tail];
        }


        public bool offerFirst(T obj)
        {
            if (tail + 1 > elements.Length)
                return false;
            add(obj);
            return true;
        }


        public bool offerLast(T obj)
        {
            if (tail + 1 > elements.Length)
                return false;
            addFirst(obj);
            return true;
        }


        public T pop()
        {
            T el = elements[head];
            for (int i = 0; i < tail; i++)
                elements[i] = elements[i + 1];
            return el;
        }


        public void push(T el)
        {
            addFirst(el);

        }


        public T peekFirst()
        {
            if (tail == -1)
                return default(T);
            return elements[head];
        }


        public T peekLast()
        {
            if (tail == -1)
                return default(T);
            return elements[tail];
        }

        public T pollLast()
        {
            if (tail == -1)
                return default(T);
            T el = elements[tail];
            tail--;
            return el;
        }

        public T removeLast()
        {
            if (tail == -1)
                throw new IndexOutOfRangeException();
            T el = elements[tail];
            tail--;
            return el;
        }

        public T pollFirst()
        {
            if (tail == -1)
                return default(T);
            T el = elements[head];
            for (int i = 0; i < tail; i++)
                elements[i] = elements[i + 1];
            return el;
        }

        public T removeFirst()
        {
            if (tail == -1)
                throw new IndexOutOfRangeException();
            T el = elements[head];
            for (int i = 0; i < tail; i++)
                elements[i] = elements[i + 1];
            return el;
        }


        public bool removeLastOccurence(T obj)
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


        public bool removeFirstOccurence(T obj)
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

        public iMyIter<T> listIterator()
        {
            return new MyItr(0, this);
        }

        internal class MyItr : iMyIter<T>
        {
            private int cursor;
            private MyArrayDeque<T> deque;
            public bool HasNext()
            {
                if (cursor < deque.size()) return true;
                else return false;
            }

            public T Next()
            {
                if (!HasNext()) throw new InvalidOperationException("Нет следующего элемента.");
                return deque.elements[cursor++];
            }


            public void Remove()
            {
                deque.remove(deque.elements[cursor++]);
            }


            public MyItr(int index, MyArrayDeque<T> deque)
            {
                cursor = index;
                this.deque = deque;
            }
        }
    }
}