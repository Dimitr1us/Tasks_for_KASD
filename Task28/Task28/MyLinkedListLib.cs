using System;
using MyArrayListLibrary;
using MyInterFaceLib;
using MyIteratorLib;

namespace MyLinkedListLib
{
    internal class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Previous { get; set; }
        public Node<T> Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Previous = null;
            Next = null;
        }

        public Node() : this(default(T)) { }
    }

    internal class MyLinkedList<T> : MyList<T>
    {
        private int Size;
        private Node<T> first;
        private Node<T> last;

        public MyLinkedList()
        {
            Size = 0;
            first = null;
            last = null;
        }

        public MyLinkedList( MyCollection<T> c) : this()
        {
            addAll(c);
        }

        public void add(T e)
        {
            Node<T> newNode = new Node<T>(e);
            if (last != null)
            {
                newNode.Previous = last;
                last.Next = newNode;
                last = newNode;
            }
            else
            {
                first = newNode;
                last = newNode;
            }
            Size++;
        }

        public void addAll(MyCollection<T> C)
        {
            T[] a = C.toArray();
            foreach (T item in a)
            {
                add(item);
            }
        }

        public void clear()
        {
            first = null;
            last = null;
            Size = 0;
        }

        public bool contains(T a)
        {
            Node<T> current = first;
            while (current != null)
            {
                if (current.Data.Equals(a))
                    return true;
                current = current.Next;
            }
            return false;
        }

        public bool containsAll(T[] a)
        {
            foreach (T element in a)
            {
                if (!contains(element))
                    return false;
            }
            return true;
        }

        public bool isEmpty() => Size == 0;

        public void remove(T a)
        {
            Node<T> current = first;
            T found=default(T);
            while (current != null)
            {
                if (current.Data.Equals(a))
                {
                    if (current.Previous != null)
                    {
                        current.Previous.Next = current.Next;
                        found = current.Data;
                    }
                    else
                    {
                        first = current.Next; // Удаляем первый элемент
                        found = current.Data;
                    }

                    if (current.Next != null)
                    {
                        current.Next.Previous = current.Previous;
                        found = current.Data;
                    }
                    else
                    {
                        last = current.Previous; // Удаляем последний элемент
                        found = current.Data;
                    }

                    Size--;
                }
                current = current.Next;
            }
        }

        public void removeAll(MyCollection<T> C)
        {
            T[] a = C.toArray();
            foreach (T item in a)
            {
                remove(item);
            }
        }

        public void retainAll(MyCollection<T> C)
        {
            T[] a = C.toArray();
            Node<T> current = first;
            while (current != null)
            {
                if (!Array.Exists(a, element => element.Equals(current.Data)))
                {
                    remove(current.Data);
                }
                current = current.Next;
            }
        }

        public int size() => Size;

        public T[] toArray()
        {
            T[] array = new T[Size];
            Node<T> current = first;
            int i = 0;
            while (current != null)
            {
                array[i++] = current.Data;
                current = current.Next;
            }
            return array;
        }

        public T[] toArray(T[] a)
        {
            if (a == null || a.Length < Size)
            {
                a = new T[Size]; // Создаем новый массив, если a равно null или недостаточной длины
            }

            Node<T> current = first;
            int i = 0;
            while (current != null)
            {
                a[i++] = current.Data;
                current = current.Next;
            }
            return a;
        }

        public void add(int index, T e)
        {
            if (index < 0 || index > Size)
                throw new ArgumentOutOfRangeException();

            if (index == 0)
            {
                AddFirst(e);
                return;
            }
            if (index == Size)
            {
                add(e);
                return;
            }

            Node<T> current = first;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            Node<T> newNode = new Node<T>(e);
            newNode.Previous = current.Previous;
            newNode.Next = current;

            if (current.Previous != null)
            {
                current.Previous.Next = newNode;
            }
            current.Previous = newNode;

            if (index == 0) first = newNode;

            Size++;
        }

        public void addAll(int index, MyCollection<T> C)
        {
            T[] a=C.toArray();
            foreach (T item in a)
            {
                add(index++, item);
            }
        }

        public T get(int index)
        {
            if (index < 0 || index >= Size)
                throw new ArgumentOutOfRangeException();

            Node<T> current = first;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current.Data;
        }

        public int indexOf(T a)
        {
            Node<T> current = first;
            int index = 0;
            while (current != null)
            {
                if (current.Data.Equals(a))
                    return index;
                current = current.Next;
                index++;
            }
            return -1;
        }

        public int lastIndexOf(T a)
        {
            Node<T> current = last;
            int index = Size - 1;
            while (current != null)
            {
                if (current.Data.Equals(a))
                    return index;
                current = current.Previous;
                index--;
            }
            return -1;
        }

        public T remove(int index)
        {
            if (index < 0 || index >= Size)
                throw new ArgumentOutOfRangeException();

            Node<T> current = first;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            T data = current.Data;
            remove(data); // Используем метод Remove для удаления
            return data;
        }

        public void set(int index, T e)
        {
            if (index < 0 || index >= Size)
                throw new ArgumentOutOfRangeException();

            Node<T> current = first;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            current.Data = e;
        }

        public T[] subList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex > Size || fromIndex > toIndex)
                throw new ArgumentOutOfRangeException();

            T[] subList= new T[0];
            Node<T> current = first;

            for (int i = 0; i < fromIndex; i++)
            {
                current = current.Next;
            }

            for (int i = fromIndex; i < toIndex; i++)
            {
                subList.Append(current.Data);
                current = current.Next;
}

            return subList;
        }

        public T element()
        {
            if (isEmpty())
                throw new InvalidOperationException("List is empty.");
            return first.Data;
        }

        public bool offer(T obj)
        {
            try
            {
                add(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public T peek()
        {
            return isEmpty() ? default(T) : first.Data;
        }

        public T poll()
        {
            if (isEmpty())
                return default(T);

            T data = first.Data;
            remove(data);
            return data;
        }

        public void AddFirst(T obj)
        {
            Node<T> newNode = new Node<T>(obj);
            if (first == null)
            {
                first = newNode;
                last = newNode;
            }
            else
            {
                newNode.Next = first;
                first.Previous = newNode;
                first = newNode;
            }
            Size++;
        }

        public void AddLast(T obj)
        {
            add(obj);
        }

        public T GetFirst()
        {
            if (isEmpty())
                throw new InvalidOperationException("List is empty.");
            return first.Data;
        }

        public T GetLast()
        {
            if (isEmpty())
                throw new InvalidOperationException("List is empty.");
            return last.Data;
        }

        public bool OfferFirst(T obj)
        {
            try
            {
                AddFirst(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool OfferLast(T obj)
        {
            return offer(obj);
        }

        public T pop()
        {
            return poll();
        }

        public void Push(T obj)
        {
            AddFirst(obj);
        }

        public T PeekFirst()
        {
            return peek();
        }

        public T PeekLast()
        {
            if (isEmpty())
                return default(T);
            return last.Data;
        }

        public T PollFirst()
        {
            return poll();
        }

        public T PollLast()
        {
            if (isEmpty())
                return default(T);

            T data = last.Data;
            remove(data);
            return data;
        }

        public T removeLast()
        {
            if (isEmpty())
                throw new InvalidOperationException("List is empty.");
            return PollLast();
        }

        public T removeFirst()
        {
            if (isEmpty())
                throw new InvalidOperationException("List is empty.");
            return PollFirst();
        }

        public bool RemoveLastOccurrence(object obj)
        {
            Node<T> current = last;
            while (current != null)
            {
                if (current.Data.Equals(obj))
                {
                    remove(current.Data);
                    return true;
                }
                current = current.Previous;
            }
            return false;
        }

        public bool RemoveFirstOccurrence(object obj)
        {
            Node<T> current = first;
            while (current != null)
            {
                if (current.Data.Equals(obj))
                {
                    remove(current.Data);
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public iMyIter<T> listIterator()
        {
            return new MyItr(0, this);
        }

        public iMyIter<T> listIterator(int index)
        {
            return new MyItr(index, this);
        }

        internal class MyItr : iMyIterator<T>
        {
            private int cursor;
            private MyLinkedList<T> list;
            public bool HasNext()
            {
                if (cursor < list.Size) return true;
                else return false;
            }

            public T Next()
            {
                cursor++;
                return list.get(cursor - 1);
            }

            public bool HasPrevious()
            {
                return cursor > 0;
            }

            public T Previous()
            {
                if (!HasPrevious())
                {
                    throw new InvalidOperationException();
                }
                --cursor;
                return list.get(cursor);
            }

            public int NextIndex()
            {
                return cursor + 1;
            }

            public int PreviousIndex()
            {
                return cursor - 1;
            }

            public void Remove()
            {
                list.remove(cursor);
            }

            public void Set(T element)
            {
                list.set(cursor, element);
            }

            public void Add(T element)
            {
                list.add(cursor, element);
            }

            public MyItr(int index, MyLinkedList<T> list)
            {
                cursor = index;
                this.list = list;
            }
        }

    }
}