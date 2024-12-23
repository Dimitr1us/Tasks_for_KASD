using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MyArrayDequeLib;
using MyIteratorLib;
using MyInterFaceLib;
namespace MyHashSetLib
{

    internal class MyHashSet<T>:MySet<T> where T : IComparable<T>
    {

        internal class Node
        {
            public T Key { get; set; }
            public object Value { get; set; }
            public Node Next { get; set; }

            public Node(T key, object value=null)
            {
                Key = key;
                Value = value;
            }
        }

        Node[] table;
        int Size;
        double loadFactor;
        int sizeOfTable;
        private int HashCode(T key)
        {
            string str = Convert.ToString(key);
            return str.Length;
        }

        public MyHashSet() {
            Size = 0;
            loadFactor = 0.75;
            table = new Node[16];
            sizeOfTable = 16;
            
        }

        public MyHashSet(MyCollection<T> C)
        {
            T[] a = C.toArray();
            Size = 0;
            loadFactor = 0.75;
            table = new Node[16];
            sizeOfTable = 16;
            foreach (T b in a)
            {
                add(b);
            }
        }

        public MyHashSet(int initialCapacity)
        {
            Size = initialCapacity;
            loadFactor = 0.75;
            table = new Node[initialCapacity];
            sizeOfTable = initialCapacity;
        }
        public MyHashSet(int initialCapacity, double loadfactor)
        {
            Size = initialCapacity;
            loadFactor = loadfactor;
            table = new Node[initialCapacity];
            sizeOfTable = initialCapacity;
        }

        public void clear() {
            int length = table.Length;
            table = new Node [length];
            Size = 0;
        }

        public bool contains(T key) {
            int hash = HashCode(key) % Size;
            if (table[hash] != null)
            {
                Node node = table[hash];
                while (true)
                {
                    if (node.Key.Equals(key)) return true;
                    if (node.Next == null) return false;
                    node = node.Next;
                }
            }
            else { return false; }
        }

        public bool containsAll(T[] a) {
            foreach (T b in a)
            {
                if (contains(b) == false) return false ;
            }
            return true;
        }
        
        public bool isEmpty()
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null) return false;
            }
            return true;
        }

        public void add(T key)
        {
            int hash = HashCode(key) % table.Length; // Используем длину массива
            Node node = new Node(key);
            Size++;
            if (table[hash] != null)
            {
                Node current = table[hash];
                while (true)
                {
                    if (current.Next == null)
                    {
                        current.Next = node; // Добавляем новый узел в конец цепочки
                        return;
                    }
                    current = current.Next;
                }
            }
            else
            {
                table[hash] = node; // Устанавливаем новый узел в хэш-таблицу
            }
        }

        public void addAll(MyCollection<T> C)
        {
            T[] a = C.toArray();
            foreach (T b in a)
            {
                add(b);
            }
        }

        public void remove(T key)
        {
            int hash = HashCode(key) % table.Length; // Используем длину массива
            Node current = table[hash];
            Node previous = null;

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    if (previous == null)
                    {
                        table[hash] = current.Next; // Удаляем первый узел
                    }
                    else
                    {
                        previous.Next = current.Next; // Соединяем предыдущий узел с следующим
                    }
                    Size--; // Уменьшаем размер
                    return;
                }
                previous = current;
                current = current.Next;
            }
        }

        public void removeAll(MyCollection<T> C)
        {
            T[] a = C.toArray();
            foreach (T b in a)
            {
                if (contains(b))
                {
                    remove(b);
                }
            }
        }

        public void retainAll(MyCollection<T> C)
        {
            T[] a = C.toArray();
            List<T> b = new List<T>();
            foreach (T c in a)
            {
                if(contains(c)) b.Add(c);
            }
            clear();
            foreach(T c in b) {
                add(c);
            }
        }

        public int size()
        {
            return Size;
        }

        public T[] toArray()
        {
            T[] a = new T[Size];
            int index = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    Node node = table[i];
                    while (true)
                    {
                        a[index] = node.Key;
                        index++;
                        if (node.Next == null) break;
                        node = node.Next;
                    }
                }
            }
            return a;
        }
        public T[] toArray(T[] a)
        {
            if (a == null) return toArray();
            T[] b = new T[a.Length];
            int index = 0;
            foreach (T c in a)
            {
                if (contains(c)) { b[index] = c; index++; }
            }
            return b;
        }

        public T first()
        {
            T min=default(T);
            bool used=false;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    Node node = table[i];
                    while (true)
                    {
                        if (used == false) { min = node.Key; used = true; }
                        else if (min.CompareTo(node.Key)>0) min= node.Key;
                        if (node.Next == null) break;
                        node = node.Next;
                    }
                }
            }
            return min;
        }

        public T last()
        {
            T max = default(T);
            bool used = false;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    Node node = table[i];
                    while (true)
                    {
                        if (used == false) { max = node.Key; used = true; }
                        else if (max.CompareTo(node.Key) < 0) max = node.Key;
                        if (node.Next == null) break;
                        node = node.Next;
                    }
                }
            }
            return max;
        }

        public T[] subSet(T fromElement, T toElement)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    Node node = table[i];
                    while (true)
                    {
                        if (node.Key.CompareTo(fromElement)>=0 && node.Key.CompareTo(toElement)<0) list.Add(node.Key);
                        if (node.Next == null) break;
                        node = node.Next;
                    }
                }
            }
            T[] a = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                a[i] = list[i];
            }
             return a;
        }
        public T[] headSet(T toElement)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    Node node = table[i];
                    while (true)
                    {
                        if (node.Key.CompareTo(toElement) < 0) list.Add(node.Key);
                        if (node.Next == null) break;
                        node = node.Next;
                    }
                }
            }
            T[] a = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                a[i] = list[i];
            }
            return a;
        }
        public T[] tailSet(T fromElement)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    Node node = table[i];
                    while (true)
                    {
                        if (node.Key.CompareTo(fromElement) >= 0) list.Add(node.Key);
                        if (node.Next == null) break;
                        node = node.Next;
                    }
                }
            }
            T[] a = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                a[i] = list[i];
            }
            return a;
        }

        public iMyIter<T> ListIterator()
        {
            return new MyItr(0, this);
        }

        internal class MyItr : iMyIter<T>
        {
            private int cursor;
            private MyHashSet<T> set;
            
            public bool HasNext()
            {
                if (cursor < set.sizeOfTable) return true;
                else return false;
            }

            public T Next()
            {
                if (!HasNext()) throw new InvalidOperationException("Нет следующего элемента.");
                return set.table[cursor].Key;
            }


            public void Remove()
            {
                set.table[cursor] = null;
            }


            public MyItr(int index, MyHashSet<T> set)
            {
                cursor = index;
                this.set = set;
            }
        }
    }
}
