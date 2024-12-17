using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyHashMapLib
{


    internal class MyHashMap<T,G>
    {

        internal class Node
        {
            public T Key { get; set; }
            public G Value { get; set; }
            public Node Next { get; set; }

            public Node(T key, G value)
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

        public MyHashMap() {
            size = 16;
            loadFactor = 0.75;
            table = new Node[16];
        }
        public MyHashMap(int initialCapacity)
        {
            size = initialCapacity;
            loadFactor = 0.75;
            table = new Node[initialCapacity];
        }
        public MyHashMap(int initialCapacity,double loadfactor)
        {
            size = initialCapacity;
            loadFactor = loadfactor;
            table = new Node[initialCapacity];
        }

        public void clear() {
            table = new Node[size];
            size = 0;
        }

        public bool containsKey(T key) {
            int hash=HashCode(key)%size;
            if (table[hash] != null)
            {
                Node node = table[hash];
                while (true)
                {
                    if (node.Key.Equals(key)) return true;
                    if (node.Next==null) return false;
                    node = node.Next;
                }
            }
            else { return false; }
        }
        public bool containsValue(G value)
        {
            for (int i = 0; i < size; i++) {
                Node current = table[i];
                if (current != null)
                {
                    while (true)
                    {
                        if (current.Value.Equals(value)) return true;
                        if (current.Next == null) break;
                        current = current.Next;
                    }
                }
            }
            return false;
        }

        public List<KeyValuePair<T,G>> entrySet()
        {
            List<KeyValuePair<T, G>> list = new List<KeyValuePair<T, G>>();
            for (int i = 0; i < size; i++)
            {
                Node current = table[i];
                while (true)
                {
                    list.Add(new KeyValuePair<T, G>(current.Key, current.Value));
                    if (current.Next == null) break;
                    current = current.Next;
                }
            }
            return list;
        }

        public G get(T key) {
            int hash = HashCode(key)%size;
            if (table[hash] != null)
            {
                Node node = table[hash];
                while (true)
                {
                    if (node.Key.Equals(key)) return node.Value;
                    if (node.Next == null) return default(G);
                    node = node.Next;
                }
            }
            else { return default(G); }
        }

        public bool isEmpty()
        {
            for (int i=0; i<size; i++)
            {
                if (table[i] != null) return false;
            }
            return true;
        }

        public List<T> keySet()
        {
            List<T> list = new List<T>();
            for (int i = 0; i < size; i++)
            {
                if (table[i] != null)
                {
                    Node current = table[i];
                    while (true)
                    {
                        list.Add(current.Key);
                        if (current.Next == null) break;
                        current = current.Next;
                    }
                }
            }
            return list;
        }

        public void Put(T key, G value)
        {
            int hash = HashCode(key) % table.Length; // Используем длину массива
            Node node = new Node(key, value);

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

        public void Remove(T key)
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


        public int Size()
        {
            return size;
        }
    }
}
