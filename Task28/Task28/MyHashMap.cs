using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MyInterFaceLib;
using MyVectorLibrary;
namespace MyHashMapLib
{


    internal class MyHashMap<T,G>:MyMap<T,G>
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
        int Size;
        double loadFactor;
        private int HashCode(T key)
        {
            string str = Convert.ToString(key);
            return str.Length;
        }

        public MyHashMap() {
            Size = 0;
            loadFactor = 0.75;
            table = new Node[16];
        }
        public MyHashMap(int initialCapacity)
        {
            Size = initialCapacity;
            loadFactor = 0.75;
            table = new Node[initialCapacity];
        }
        public MyHashMap(int initialCapacity,double loadfactor)
        {
            Size = initialCapacity;
            loadFactor = loadfactor;
            table = new Node[initialCapacity];
        }

        public void clear() {
            table = new Node[Size];
            Size = 0;
        }

        public bool containsKey(T key) {
            int hash=HashCode(key)%Size;
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
            for (int i = 0; i < Size; i++) {
                Node current = table[i];
                while (true)
                {
                    if (current.Value.Equals(value)) return true;
                    if (current.Next == null) break;
                    current = current.Next;
                }
            }
            return false;
        }

        public List<(T,G)> entrySet()
        {
            List<(T, G)> list = new List<(T, G)>();
            for (int i = 0; i < Size; i++)
            {
                Node current = table[i];
                while (true)
                {
                    list.Add(new (current.Key, current.Value));
                    if (current.Next == null) break;
                    current = current.Next;
                }
            }
            return list;
        }

        public G get(T key) {
            int hash = HashCode(key)%Size;
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
            for (int i=0; i<Size; i++)
            {
                if (table[i] != null) return false;
            }
            return true;
        }

        public List<T> keySet()
        {
            List<T> list = new List<T>();
            for (int i = 0; i < Size; i++)
            {
                Node current = table[i];
                while (true)
                {
                    list.Add(current.Key);
                    if (current.Next == null) break;
                    current = current.Next;
                }
            }
            return list;
        }

        public void put(T key, G value)
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

        public void putAll(MyMap<T, G> map) {
            List<(T, G)> list = map.entrySet();
            foreach ((T,G) pair in list)
            {
                put(pair.Item1,pair.Item2);
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


        public MyCollection<T> values()
        {
            List<T> list = keySet();
            MyCollection<T> myCollection = new MyVector<T>();
            foreach (T item in list)
            {
                myCollection.add(item);
            }
            return myCollection;
        }

        public int size()
        {
            return Size;
        }
    }
}
