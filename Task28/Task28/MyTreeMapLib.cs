using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInterFaceLib;
using MyVectorLibrary;
namespace MyTreeMapLib
{

    public interface Comparator<T> where T : IComparable<T>
    {
        int CompareTo(T x, T y);
    }
    internal class MyTreeMap<T, G>: MyNavigableMap<T,G> where T : IComparable<T>
    {


        private class Node
        {
            public T Key { get; set; }
            public G Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(T key, G value)
            {
                Key = key;
                Value = value;
                Left = null;
                Right = null;
            }
        }


        private Comparator<T> comparator;
        private Node root;
        private int Size;

        private int Compare(T a, T b)
        {
            if (comparator == null)
            {
                if (a.CompareTo(b) > 0) return 1;
                else if (a.CompareTo(b) < 0) return -1;
                else return 0;
            }
            else
            {
                return comparator.CompareTo(a, b);
            }
        }


        public MyTreeMap()
        {
            Size = 0;
            comparator = null;
            root = null;
        }

        public MyTreeMap(Comparator<T> comp)
        {
            comparator = comp;
            Size = 0;
            root = null;
        }

        public MyTreeMap(MyMap<T,G> m)
        {
            Size = 0;
            comparator = null;
            root = null;
            List<(T,G)> list = m.entrySet();
            foreach((T,G) pair in list)
            {
                put(pair.Item1, pair.Item2);
            }
        }

        public MyTreeMap(MySortedMap<T, G> sm)
        {
            Size = 0;
            comparator = null;
            root = null;
            List<(T, G)> list = sm.entrySet();
            foreach ((T, G) pair in list)
            {
                put(pair.Item1, pair.Item2);
            }
        }

        public void putAll(MyMap<T, G> m)
        {
            List<(T, G)> list = m.entrySet();
            foreach ((T, G) pair in list)
            {
                put(pair.Item1, pair.Item2);
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

        public void Clear()
        {
            Size = 0;
            root = null;
        }

        public bool containsKey(T key) {
            if (root == null) { return false; }
            else
            {
                return ContainKey(key, root) == 1;
            }
        }

        private int ContainKey(T key, Node node)
        {
            if (Compare(key, node.Key) > 0 && node.Right != null)
            {
                return 0 + ContainKey((T)key, node.Right);
            }
            if (Compare(key, node.Key) < 0 && node.Left != null)
            {
                return 0 + ContainKey((T)key, node.Left);
            }
            if (Compare(key, node.Key) == 0)
            {
                return 1;
            }
            return 0;
        }

        public bool containsValue(G value) {
            if (root == null) { return false; }
            else
            {
                return (containsValue(value, root));
            }
        }

        private bool containsValue(G value, Node node) {
            if (value.Equals(node.Value))
            {
                return true;
            }
            if (node.Left != null)
            {
                containsValue(value, node.Left);
            }
            if (node.Right != null)
            {
                containsValue(value, node.Left);
            }
            return false;
        }

        public List<(T,G)> entrySet()
        {
            if (root == null) { throw new InvalidOperationException("Tree is empty."); }
            else
            {
                List<(T key, G value)> list = new List<(T key, G value)>();
                EntrySet(root, ref list);
                return list;
            }
        }

        private void EntrySet(Node current, ref List<(T key, G value)> list)
        {
            list.Add((current.Key, current.Value));
            if (current.Left != null) {
                EntrySet(current.Left, ref list);
            }
            if (current.Right != null)
            {
                EntrySet(current.Right, ref list);
            }
        }

        public G? get(T key)
        {
            if (containsKey(key)) { return get(key, root); }
            else return default(G);
        }

        private G get(T key, Node node)
        {
            if (Compare(key, node.Key) == 0) return node.Value;
            if (node.Left != null) {
                get(key, node.Left);
            }
            if (node.Right != null)
            {
                get(key, node.Right);
            }
            return default(G);
        }




        public bool isEmpty()
        {
            return Size == 0;
        }

        public List<T> keySet()
        {
            if (root == null) { throw new InvalidOperationException("Tree is empty."); }
            else
            {
                List<T> list = new List<T>();
                keySet(root, ref list);
                return list;
            }
        }

        private void keySet(Node current, ref List<T> list)
        {
            list.Add(current.Key);
            if (current.Left != null)
            {
                keySet(current.Left, ref list);
            }
            if (current.Right != null)
            {
                keySet(current.Right, ref list);
            }
        }
        public void put(T key, G value) {
            Size++;
            if (root == null) {
                root = new Node(key, value);
            }
            else {
                Add(key, value, root);
            }
        }
        private void Add(T key, G value, Node r)
        {
            if (Compare(key, r.Key) > 0)
            {
                if (r.Right != null)
                {
                    Add(key, value, r.Right);
                }
                else
                {
                    Node n = new Node(key, value);
                    r.Right = n;
                }
            }
            else if (Compare(key, r.Key) < 0)
            {
                if (r.Left != null)
                {
                    Add(key, value, r.Left);
                }
                else
                {
                    Node n = new Node(key, value);
                    r.Left = n;
                }
            }
        }

        public void remove(T key)
        {
            root = Remove(key, root);
            Size--;
        }
        private Node Remove(T key, Node node)
        {
            if (node == null)
            {
                return null;
            }

            if (key.CompareTo(node.Key) < 0)
            {
                node.Left = Remove(key, node.Left);
            }
            else if (key.CompareTo(node.Key) > 0)
            {
                node.Right = Remove(key, node.Right);
            }
            else
            {
                if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }
                else
                {
                    node.Key = FindMinKey(node.Right);
                    node.Right = Remove(node.Key, node.Right);
                }
            }

            return node;
        }
        private T FindMinKey(Node node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node.Key;
        }

        public int size()
        {
            return Size;
        }
        public T firstKey()
        {
            if (root == null)
            {
                throw new InvalidOperationException("Tree is empty.");
            }

            Node current = root;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current.Key;
        }

        public T lastKey()
        {
            if (root == null)
            {
                throw new InvalidOperationException("Tree is empty.");
            }

            Node current = root;
            while (current.Right != null)
            {
                current = current.Right;
            }
            return current.Key;
        }

        public MySortedMap<T, G> headMap(T end)
        {
            if (root == null) { throw new InvalidOperationException("Tree is empty."); }
            else
            {
                MyTreeMap<T, G> map = new MyTreeMap<T, G>(comparator);
                headMap(end, root, ref map);
                return map;
            }
        }

        private void headMap(T end, Node node, ref MyTreeMap<T, G> map)
        {
            if (Compare(node.Key, end) < 0) map.put(node.Key, node.Value);
            if (node.Left != null) headMap(end, node.Left, ref map);
            if (node.Right != null) headMap(end, node.Right, ref map);
        }

        public MySortedMap<T,G> subMap(T start, T end)
        {
            if (root == null) { throw new InvalidOperationException("Tree is empty."); }
            else
            {
                if (Compare(start, end) > 0) throw new InvalidOperationException("Start must be lower than end.");
                else
                {
                    MyTreeMap<T, G> map = new MyTreeMap<T, G>(comparator);
                    subMap(start, end, root, ref map);
                    return map;
                }
            }
        }
        private void subMap(T start, T end, Node node, ref MyTreeMap<T, G> map)
            {
                if (Compare(node.Key, end) < 0 && Compare(node.Key, start) >= 0) map.put(node.Key, node.Value);
                if (node.Left != null) headMap(end, node.Left, ref map);
                if (node.Right != null) headMap(end, node.Right, ref map);
            }


        public MySortedMap<T, G> tailMap(T start)
        {
            if (root == null) { throw new InvalidOperationException("Tree is empty."); }
            else
            {
                MyTreeMap<T, G> map = new MyTreeMap<T, G>(comparator);
                tailMap(start, root, ref map);
                return map;
            }
        }

        private void tailMap(T start, Node node, ref MyTreeMap<T, G> map)
        {
            if (Compare(node.Key, start) > 0) map.put(node.Key, node.Value);
            if (node.Left != null) headMap(start, node.Left, ref map);
            if (node.Right != null) headMap(start, node.Right, ref map);
        }
        public List<(T key, G value)> lowerEntry(T key)
        {
            if (root == null) { throw new InvalidOperationException("Tree is empty."); }
            else
            {
                List<(T key, G value)> list = new List<(T key, G value)>();
                lowerEntry(root, ref list, key);
                return list;
            }
        }

        private void lowerEntry(Node current, ref List<(T key, G value)> list, T key)
        {
            if (Compare(key,current.Key)<0) list.Add((current.Key, current.Value));
            if (current.Left != null)
            {
                lowerEntry(current.Left, ref list,key);
            }
            if (current.Right != null)
            {
                lowerEntry(current.Right, ref list,key);
            }
        }

        public List<(T key, G value)> floorEntry(T key)
        {
            if (root == null) { throw new InvalidOperationException("Tree is empty."); }
            else
            {
                List<(T key, G value)> list = new List<(T key, G value)>();
                floorEntry(root, ref list, key);
                return list;
            }
        }

        private void floorEntry(Node current, ref List<(T key, G value)> list, T key)
        {
            if (Compare(key, current.Key) <= 0) list.Add((current.Key, current.Value));
            if (current.Left != null)
            {
                floorEntry(current.Left, ref list, key);
            }
            if (current.Right != null)
            {
                floorEntry(current.Right, ref list, key);
            }
        }

        public List<(T key, G value)> higherEntry(T key)
        {
            if (root == null) { throw new InvalidOperationException("Tree is empty."); }
            else
            {
                List<(T key, G value)> list = new List<(T key, G value)>();
                higherEntry(root, ref list, key);
                return list;
            }
        }

        private void higherEntry(Node current, ref List<(T key, G value)> list, T key)
        {
            if (Compare(key, current.Key) > 0) list.Add((current.Key, current.Value));
            if (current.Left != null)
            {
                higherEntry(current.Left, ref list, key);
            }
            if (current.Right != null)
            {
                higherEntry(current.Right, ref list, key);
            }
        }

        public List<(T key, G value)> ceilingEntry(T key)
        {
            if (root == null) { throw new InvalidOperationException("Tree is empty."); }
            else
            {
                List<(T key, G value)> list = new List<(T key, G value)>();
                ceilingEntry(root, ref list, key);
                return list;
            }
        }

        private void ceilingEntry(Node current, ref List<(T key, G value)> list, T key)
        {
            if (Compare(key, current.Key) >= 0) list.Add((current.Key, current.Value));
            if (current.Left != null)
            {
                ceilingEntry(current.Left, ref list, key);
            }
            if (current.Right != null)
            {
                ceilingEntry(current.Right, ref list, key);
            }
        }

        public List<T> lowerKey(T key)
        {
            if (root == null) { throw new InvalidOperationException("Tree is empty."); }
            else
            {
                List<T> list = new List<T>();
                lowerKey(root, ref list, key);
                return list;
            }
        }

        private void lowerKey(Node current, ref List<T> list, T key)
        {
            if (Compare(key, current.Key) < 0) list.Add(current.Key);
            if (current.Left != null)
            {
                lowerKey(current.Left, ref list, key);
            }
            if (current.Right != null)
            {
                lowerKey(current.Right, ref list, key);
            }
        }

        public List<T> floorKey(T key)
        {
            if (root == null) { throw new InvalidOperationException("Tree is empty."); }
            else
            {
                List<T> list = new List<T>();
                floorKey(root, ref list, key);
                return list;
            }
        }

        private void floorKey(Node current, ref List<T> list, T key)
        {
            if (Compare(key, current.Key) <= 0) list.Add(current.Key);
            if (current.Left != null)
            {
                floorKey(current.Left, ref list, key);
            }
            if (current.Right != null)
            {
                floorKey(current.Right, ref list, key);
            }
        }

        public List<T> higherKey(T key)
        {
            if (root == null) { throw new InvalidOperationException("Tree is empty."); }
            else
            {
                List<T> list = new List<T>();
                higherKey(root, ref list, key);
                return list;
            }
        }

        private void higherKey(Node current, ref List<T> list, T key)
        {
            if (Compare(key, current.Key) > 0) list.Add(current.Key);
            if (current.Left != null)
            {
                higherKey(current.Left, ref list, key);
            }
            if (current.Right != null)
            {
                higherKey(current.Right, ref list, key);
            }
        }

        public List<T> ceilingKey(T key)
        {
            if (root == null) { throw new InvalidOperationException("Tree is empty."); }
            else
            {
                List<T> list = new List<T>();
                ceilingKey(root, ref list, key);
                return list;
            }
        }

        private void ceilingKey(Node current, ref List<T> list, T key)
        {
            if (Compare(key, current.Key) >= 0) list.Add(current.Key);
            if (current.Left != null)
            {
                ceilingKey(current.Left, ref list, key);
            }
            if (current.Right != null)
            {
                ceilingKey(current.Right, ref list, key);
            }
        }

        public T pollFirstEntry()
        {
            if (root == null)
            {
                throw new InvalidOperationException("Tree is empty.");
            }

            Node current = root;
            while (current.Left != null)
            {
                current = current.Left;
            }
            remove(current.Key);
            return current.Key;
        }
        public T pollLastEntry()
        {
            if (root == null)
            {
                throw new InvalidOperationException("Tree is empty.");
            }

            Node current = root;
            while (current.Right != null)
            {
                current = current.Right;
            }
            remove(current.Key);
            return current.Key;
        }

        public (T,G) firstEntry()
        {
            if (root == null)
            {
                throw new InvalidOperationException("Tree is empty.");
            }

            Node current = root;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return (current.Key,current.Value);
        }
        public (T, G) lastEntry()
        {
            if (root == null)
            {
                throw new InvalidOperationException("Tree is empty.");
            }

            Node current = root;
            while (current.Right != null)
            {
                current = current.Right;
            }
            return (current.Key, current.Value);
        }
    }
}