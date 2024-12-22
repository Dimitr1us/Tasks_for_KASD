using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyHashSetLib
{


    internal class MyHashSet<T> where T : IComparable<T>
    {

        internal class Node
        {
            public T Key { get; set; }
            public object Value { get; set; }
            public Node Next { get; set; }

            public Node(T key, object value)
            {
                Key = key;
                Value = value;
            }
        }

        Node[] table;
        int size;
        double loadFactor;
        private int HashCode(T key)
        {
            string str = Convert.ToString(key);
            return str.Length;
        }

        public MyHashSet() {
            size = 0;
            loadFactor = 0.75;
            table = new Node[16];
        }

        public MyHashSet(T[] a)
        {
            size = 0;
            loadFactor = 0.75;
            table = new Node[16];
            foreach (T b in a)
            {
                add(b, null);
            }
        }

        public MyHashSet(int initialCapacity)
        {
            size = initialCapacity;
            loadFactor = 0.75;
            table = new Node[initialCapacity];
        }
        public MyHashSet(int initialCapacity, double loadfactor)
        {
            size = initialCapacity;
            loadFactor = loadfactor;
            table = new Node[initialCapacity];
        }

        public void clear() {
            int length = table.Length;
            table = new Node [length];
            size = 0;
        }

        public bool contains(T key) {
            int hash = HashCode(key) % table.Length;
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

        public void add(T key, object value)
        {
            int hash = HashCode(key) % table.Length; // Используем длину массива
            Node node = new Node(key, value);
            size++;
            if (table[hash] != null)
            {
                Node current = table[hash];
                while (true)
                {
                    if (current.Key.Equals(key))
                    {
                        current.Value = value; // Обновляем значение, если ключ уже существует
                        return;
                    }
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

        public void addAll(T[] a)
        {
            foreach (T b in a)
            {
                add(b, null);
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
                    size--; // Уменьшаем размер
                    return;
                }
                previous = current;
                current = current.Next;
            }
        }

        public void removeAll(T[] a)
        {
            foreach (T b in a)
            {
                if (contains(b))
                {
                    remove(b);
                }
            }
        }

        public void retainAll(T[] a)
        {
            List<T> b = new List<T>();
            foreach (T c in a)
            {
                if(contains(c)) b.Add(c);
            }
            clear();
            foreach(T c in b) {
                add(c,null);
            }
        }

        public int Size()
        {
            return size;
        }

        public T[] toArray()
        {
            T[] a = new T[size];
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

        public List<T> subSet(T fromElement, T toElement)
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
             return list;
        }
        public List<T> headSet(T toElement)
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
            return list;
        }
        public List<T> tailSet(T fromElement, T toElement)
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
            return list;
        }
    }
}
