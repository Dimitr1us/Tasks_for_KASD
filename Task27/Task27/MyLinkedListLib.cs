using System;
using MyArrayListLibrary;
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

    internal class MyLinkedList<T>
    {
        private int size;
        private Node<T> first;
        private Node<T> last;

        public MyLinkedList()
        {
            size = 0;
            first = null;
            last = null;
        }

        public MyLinkedList(T[] a) : this()
        {
            addAll(a);
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
            size++;
        }

        public void addAll(T[] a)
        {
            foreach (T item in a)
            {
                add(item);
            }
        }

        public void Clear()
        {
            first = null;
            last = null;
            size = 0;
        }

        public bool Contains(object o)
        {
            Node<T> current = first;
            while (current != null)
            {
                if (current.Data.Equals(o))
                    return true;
                current = current.Next;
            }
            return false;
        }

        public bool ContainsAll(T[] a)
        {
            foreach (T element in a)
            {
                if (!Contains(element))
                    return false;
            }
            return true;
        }

        public bool isEmpty() => size == 0;

        public bool Remove(object o)
        {
            Node<T> current = first;
            while (current != null)
            {
                if (current.Data.Equals(o))
                {
                    if (current.Previous != null)
                    {
                        current.Previous.Next = current.Next;
                    }
                    else
                    {
                        first = current.Next; // Удаляем первый элемент
                    }

                    if (current.Next != null)
                    {
                        current.Next.Previous = current.Previous;
                    }
                    else
                    {
                        last = current.Previous; // Удаляем последний элемент
                    }

                    size--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void RemoveAll(T[] a)
        {
            foreach (T item in a)
            {
                Remove(item);
            }
        }

        public void RetainAll(T[] a)
        {
            Node<T> current = first;
            while (current != null)
            {
                if (!Array.Exists(a, element => element.Equals(current.Data)))
                {
                    Remove(current.Data);
                }
                current = current.Next;
            }
        }

        public int Size() => size;

        public T[] ToArray()
        {
            T[] array = new T[size];
            Node<T> current = first;
            int i = 0;
            while (current != null)
            {
                array[i++] = current.Data;
                current = current.Next;
            }
            return array;
        }

        public T[] ToArray(T[] a)
        {
            if (a == null || a.Length < size)
            {
                a = new T[size]; // Создаем новый массив, если a равно null или недостаточной длины
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

        public void Add(int index, T e)
        {
            if (index < 0 || index > size)
                throw new ArgumentOutOfRangeException();

            if (index == 0)
            {
                AddFirst(e);
                return;
            }
            if (index == size)
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

            size++;
        }

        public void AddAll(int index, T[] a)
        {
            foreach (T item in a)
            {
                Add(index++, item);
            }
        }

        public T Get(int index)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException();

            Node<T> current = first;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current.Data;
        }

        public int IndexOf(object o)
        {
            Node<T> current = first;
            int index = 0;
            while (current != null)
            {
                if (current.Data.Equals(o))
                    return index;
                current = current.Next;
                index++;
            }
            return -1;
        }

        public int LastIndexOf(object o)
        {
            Node<T> current = last;
            int index = size - 1;
            while (current != null)
            {
                if (current.Data.Equals(o))
                    return index;
                current = current.Previous;
                index--;
            }
            return -1;
        }

        public T Remove(int index)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException();

            Node<T> current = first;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            T data = current.Data;
            Remove(data); // Используем метод Remove для удаления
            return data;
        }

        public void Set(int index, T e)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException();

            Node<T> current = first;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            current.Data = e;
        }

        public MyLinkedList<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex > size || fromIndex > toIndex)
                throw new ArgumentOutOfRangeException();

            MyLinkedList<T> subList = new MyLinkedList<T>();
            Node<T> current = first;

            for (int i = 0; i < fromIndex; i++)
            {
                current = current.Next;
            }

            for (int i = fromIndex; i < toIndex; i++)
            {
                subList.add(current.Data);
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
            Remove(data);
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
            size++;
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
            Remove(data);
            return data;
        }

        public T RemoveLast()
        {
            if (isEmpty())
                throw new InvalidOperationException("List is empty.");
            return PollLast();
        }

        public T RemoveFirst()
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
                    Remove(current.Data);
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
                    Remove(current.Data);
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public iMyIterator<T> ListIterator()
        {
            return new MyItr(0, this);
        }

        public iMyIterator<T> ListIterator(int index)
        {
            return new MyItr(index, this);
        }

        internal class MyItr : iMyIterator<T>
        {
            private int cursor;
            private MyLinkedList<T> list;
            public bool HasNext()
            {
                if (cursor < list.size) return true;
                else return false;
            }

            public T Next()
            {
                cursor++;
                return list.Get(cursor - 1);
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
                return list.Get(cursor);
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
                list.Remove(cursor);
            }

            public void Set(T element)
            {
                list.Set(cursor, element);
            }

            public void Add(T element)
            {
                list.Add(cursor, element);
            }

            public MyItr(int index, MyLinkedList<T> list)
            {
                cursor = index;
                this.list = list;
            }
        }

    }
}