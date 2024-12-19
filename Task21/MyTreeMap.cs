using System;
namespace MyTreeMapLib {

    interface Comparator<G> where G : IComparable<G>
    {
        int CompareTo(G x, G y);
    }
    public class MyTreeMap<T, G> where G : IComparable<G>
    {


        private class Node
        {
            public T Key { get; set; }
            public G Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(K key, V value)
            {
                Key = key;
                Value = value;
            }


            private Comparator<G> comparator;
            private Node root;
            private int size;

            private Compare(G a, G b)
            {
                if (comparator == null) {
                    if (a == b) return 0;
                    else if (a > b) return 1;
                    else return -1;
                }
                else
                {
                    return comparator.CompareTo(G a, G b)
                }
            }

            public MyTreeMap()
            {
                size = 0;
                comparator = null;
                root = null;
            }

            public MyTreeMap(Comparator<G> comp)
            {
                this.comparator = comp;
                size = 0;
                root = null;
            }
        }
    }
}
