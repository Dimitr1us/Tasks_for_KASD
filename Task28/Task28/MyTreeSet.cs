using MyInterFaceLib;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
namespace MyTreeSetLib
{
    public class MyComparator<T> : Comparer<T> where T : IComparable
    {
        public override int Compare(T? x, T? y)
        {
            return x.CompareTo(y);
            throw new NotImplementedException();

        }
    }


    public class MyReverseComparator<T> : Comparer<T> where T : IComparable
    {
        public override int Compare(T? x, T? y)
        {
            return x.CompareTo(y) * (-1);
            throw new NotImplementedException();

        }
    }


    internal class MyTreeSet<K>:MyNavigableSet<K> where K : IComparable
    {
        IComparer comparer = new MyComparator<K>();

        private protected TreeElement root = null;
        private TreeElement nil = new TreeElement();

        private int Size;

        public MyTreeSet()
        {
            Size = 0;
        }


        public MyTreeSet(IComparer comp)
        {
            comparer = comp;
        }

        public MyTreeSet(MyCollection<K> C)
        {
            K[] a = C.toArray();
            foreach (var el in a)
                add(el);
        }


        public MyTreeSet(SortedSet<K> a)
        {
            foreach (var el in a)
                add(el);
        }


        public void addAll(MyCollection<K> C)
        {
            K[] a = C.toArray();
            foreach (var el in a)
                add(el);
        }


        public void clear()
        {
            root = null;
            Size = 0;

        }

        public bool contains(K o)
        {
            TreeElement copy = root;
            while (copy != nil)
            {
                if (comparer.Compare(o, copy.Key) < 0)
                    copy = copy.left;
                else if (comparer.Compare(o, copy.Key) > 0)
                    copy = copy.right;
                else if (comparer.Compare(o, copy.Key) == 0)
                    return true;
            }
            return false;
        }



        public bool isEmpty() => Size == 0;


        public void removeAll(MyCollection<K> C)
        {
            K[] a =C.toArray();
            foreach (var el in a)
                remove(el);
        }


        public void retainAll(MyCollection<K> C)
        {
            K[] a =C.toArray();
            K[] array = new K[Size];
            int index = 0;
            foreach (K key in BFS())
            {
                array[index++] = key;
            }

            foreach (K key in array)
            {
                if (!a.Contains<K>(key))
                    remove(key);
            }
        }


        public bool containsAll(K[] a)
        {
            foreach (var el in a)
            {
                if (!contains(el))
                    return false;

            }
            return true;

        }



        public int size() => Size;


        public K[] toArray()
        {
            K[] array = new K[Size];
            int index = 0;
            foreach (K key in BFS())
            {
               array[index++] = key;
            }
            return array;
        }


        public K[] toArray(K[] a)
        {
            K[] array;
            int index = 0;
            if (a == null)
            {
                array = new K[Size];
            }
            else
            {
                array = new K[Size + a.Length];
                for (int i = 0; i < a.Length; i++)
                    array[index++] = a[i];
            }

            foreach (K key in BFS())
            {

                array[index++] = key;
            }
            return array;
        }


        public K first()
        {
            TreeElement copy = root;
            while (copy.left != nil)
                copy = copy.left;
            return copy.Key;

        }


        public K last()
        {
            TreeElement copy = root;
            while (copy.right != nil)
                copy = copy.right;
            return copy.Key;

        }



        


        public K[] subSet(K FromElement, K ToElement)
        {
            K[] a = new K[0];
            foreach (K el in BFS())
                if (el.CompareTo(FromElement) >= 0 && el.CompareTo(ToElement) <= 0)
                    a.Append(el);

            return a;
        }


        public K[] headSet(K ToElement)
        {
            K[] a = new K[0];
            MyTreeSet<K> Sub = new MyTreeSet<K>();
            foreach (K el in OrdesrPrint())
            {
                if (el.CompareTo(ToElement) <= 0)
                    a.Append(el);

            }
            return a;
        }


        public K[] tailSet(K FromElement)
        {
            K[] a = new K[0];
            MyTreeSet<K> Sub = new MyTreeSet<K>();
            foreach (K el in OrdesrPrint())
            {
                if (el.CompareTo(FromElement) >= 0)
                    a.Append(el);

            }
            return a;
        }


        public K ceilingKey(K obj)
        {
            foreach (K el in OrdesrPrint())
            {
                if (el.CompareTo(obj) >= 0)
                    return el;
            }
            return default(K);
        }

        public K floorKey(K obj)
        {
            foreach (K el in OrdesrPrint())
            {
                if (el.CompareTo(obj) <= 0)
                    return el;
            }
            return default(K);

        }


        public K higherKey(K obj)
        {
            foreach (K el in OrdesrPrint())
            {
                if (el.CompareTo(obj) > 0)
                    return el;
            }
            return default(K);
        }


        public K? lowerKey(K obj)
        {
            foreach (K el in OrdesrPrint())
            {
                if (el.CompareTo(obj) < 0)
                    return el;
            }
            return default(K);

        }



        public K pollLastEntry()
        {
            if (Size == 0)
                return default(K);

            TreeElement copy = root;
            while (copy.right != nil)
                copy = copy.right;
            remove(copy.Key);
            return copy.Key;
        }


        public K pollFirstEntry()
        {
            if (Size == 0)
                return default(K);

            TreeElement copy = root;
            while (copy.left != nil)
                copy = copy.left;
            remove(copy.Key);
            return copy.Key;
        }

        public virtual void add(K key)
        {
            if (root == null)
            {

                root = new TreeElement();
                root.Key = key;

                root.color = Color.Black;
                root.left = nil;
                root.right = nil;

                Size++;
                return;
            }
            TreeElement el = new TreeElement();
            el.Key = key;

            while (root.prev != null)
                root = root.prev;
            TreeAdd(root, el);
            while (root.prev != null)
                root = root.prev;



            void TreeAdd(TreeElement root, TreeElement AddVal)
            {
                int result = comparer.Compare(AddVal.Key, root.Key);
                while (true)
                {
                    if (comparer.Compare(AddVal.Key, root.Key) < 0)
                    {
                        if (root.left == nil)
                        {
                            AddVal.prev = root;
                            root.left = AddVal;
                            AddVal.color = Color.Red;
                            AddVal.right = nil;
                            AddVal.left = nil;
                            BalanceTree(AddVal);
                            Size++;
                            break;

                        }
                        else root = root.left;
                    }
                    else if (comparer.Compare(AddVal.Key, root.Key) > 0)
                    {
                        if (root.right == nil)
                        {
                            AddVal.prev = root;
                            root.right = AddVal;
                            AddVal.color = Color.Red;
                            AddVal.right = nil;
                            AddVal.left = nil;
                            BalanceTree(AddVal);
                            Size++;
                            break;

                        }
                        else root = root.right;
                    }
                    else if (root.Key.Equals(AddVal.Key))
                    {
                        root.Key = AddVal.Key;
                        return;
                    }
                }
            }
        }




        public virtual void remove(K AddVal)
        {

            while (root.prev != null)
                root = root.prev;
            TreeElement copy = root;
            while (true)
            {
                if (comparer.Compare(AddVal, copy.Key) < 0)
                {
                    if (copy.left == nil)
                    {
                        return;

                    }
                    else copy = copy.left;
                }
                else if (comparer.Compare(AddVal, copy.Key) > 0)
                {
                    if (copy.right == nil)
                    {
                        return;

                    }
                    else copy = copy.right;
                }
                else if (copy.Key.Equals(AddVal))
                {
                    Delete(copy);
                    Size--;
                    while (root.prev != null)
                        root = root.prev;
                    break;
                }
            }
        }
        private void Delete(TreeElement NodeDel)
        {
            if (NodeDel == null)
                return;
            if (Size == 1)
            {
                root = null;

                return;
            }
            TreeElement Parent = NodeDel.prev;
            if (NodeDel.color == Color.Red && NodeDel.right == nil && NodeDel.left == nil)
            {
                if (Parent.left == NodeDel)
                    Parent.left = nil;
                else if (Parent.right == NodeDel)
                    Parent.right = nil;
            }

            else if (NodeDel.left != nil && NodeDel.right != nil)
            {
                TreeElement MaxLeft = NodeDel.left;
                TreeElement MinRight = NodeDel.right;
                TreeElement ChosenCand = new TreeElement();
                while (MaxLeft.right != nil)
                    MaxLeft = MaxLeft.right;
                while (MinRight.left != nil)
                    MinRight = MinRight.left;
                if (MaxLeft.color == Color.Red && MinRight.color == Color.Red)
                    ChosenCand = MaxLeft;
                else if (MaxLeft.color == Color.Black && MinRight.color == Color.Red)
                    ChosenCand = MinRight;
                else if (MaxLeft.color == Color.Red && MinRight.color == Color.Black)
                    ChosenCand = MaxLeft;
                else if (MaxLeft.color == Color.Black && MinRight.color == Color.Black)
                    ChosenCand = MaxLeft;
                if (ChosenCand.color == Color.Red)
                {
                    K tmp = NodeDel.Key;
                    NodeDel.Key = ChosenCand.Key;
                    ChosenCand.Key = tmp;

                    NodeDel = ChosenCand;
                    TreeElement Par = NodeDel.prev;
                    if (NodeDel.color == Color.Red && NodeDel.right == nil && NodeDel.left == nil)
                    {
                        if (Par.left == NodeDel)
                            Par.left = nil;
                        else if (Par.right == NodeDel)
                            Par.right = nil;
                    }
                }
                else if (ChosenCand.color == Color.Black && ChosenCand.left != nil || ChosenCand.right != nil)
                {
                    K tmp = NodeDel.Key;
                    NodeDel.Key = ChosenCand.Key;
                    ChosenCand.Key = tmp;

                    NodeDel = ChosenCand;
                    if (NodeDel.left != nil)
                    {
                        NodeDel.Key = NodeDel.left.Key;
                        NodeDel.left = nil;

                    }
                    else if (NodeDel.right != nil)
                    {
                        NodeDel.Key = NodeDel.right.Key;
                        NodeDel.right = nil;
                    }
                }

                else if (ChosenCand.color == Color.Black && ChosenCand.left == nil && ChosenCand.right == nil)
                {
                    NodeDel.Key = ChosenCand.Key;
                    NodeDel = ChosenCand;
                    if (NodeDel.prev.left == NodeDel)
                    {
                        NodeDel.prev.left = nil;
                        DelBalance(NodeDel);

                    }
                    else if (NodeDel.prev.right == NodeDel)
                    {
                        NodeDel.prev.right = nil;
                        DelBalanceRight(NodeDel);
                    }

                }
     }

            else if (NodeDel.color == Color.Black && NodeDel.right == nil && NodeDel.left != nil || NodeDel.left == nil && NodeDel.right != nil)
            {
                if (NodeDel.left != nil)
                {
                    NodeDel.Key = NodeDel.left.Key;
                    NodeDel.left = nil;

                }
                else if (NodeDel.right != nil)
                {
                    NodeDel.Key = NodeDel.right.Key;
                    NodeDel.right = nil;
                }
            }

            else if (NodeDel.color == Color.Black && NodeDel.left == nil && NodeDel.right == nil)
            {
                if (NodeDel.prev.left == NodeDel)
                {
                    NodeDel.prev.left = nil;
                    DelBalance(NodeDel);
                }
                else if (NodeDel.prev.right == NodeDel)
                {
                    NodeDel.prev.right = nil;
                    DelBalanceRight(NodeDel);
                }
            }


            void DelBalance(TreeElement NodeD)
            {
                TreeElement Parent = NodeD.prev;
                if (Parent == null)
                {
                    root = new TreeElement();
                    return;
                }
                TreeElement Brother = Parent.right;



                if (Brother.color == Color.Black && Brother.right.color == Color.Red)
                {
                    Brother.color = Parent.color;
                    Parent.color = Color.Black;
                    Brother.right.color = Color.Black;
                    LeftRotate(Brother);
                }
                else if (Brother.color == Color.Black && Brother.left.color == Color.Red && Brother.right.color == Color.Black)
                {
                    Brother.left.color = Brother.color;
                    Brother.color = Color.Red;
                    TreeElement BrotherLeft = Brother.left;
                    RightRotate(BrotherLeft);
                    DelBalance(NodeD);
                    return;
                }

                else if (Brother.color == Color.Black && Brother.right.color == Color.Black && Brother.left.color == Color.Black)
                {
                    Brother.color = Color.Red;
                    if (Parent.color == Color.Red)
                        Parent.color = Color.Black;
                    else
                    {
                        Parent.color = Color.Black;
                        if (Parent.prev != null)
                        {
                            if (Parent.prev.left == Parent)
                                DelBalance(Parent);
                            else if (Parent.prev.right == Parent)
                                DelBalanceRight(Parent);
                        }
                    }
                }


                else if (Brother.color == Color.Red && Parent.color == Color.Black)
                {
                    Brother.color = Color.Black;
                    Parent.color = Color.Red;
                    LeftRotate(Brother);
                }
            }


            void DelBalanceRight(TreeElement NodeD)
            {
                TreeElement Parent = NodeD.prev;
                if (Parent == null)
                {
                    root = new TreeElement();
                    return;
                }
                TreeElement Brother = Parent.left;
                if (Brother.color == Color.Black && Brother.left.color == Color.Red)
                {
                    Brother.color = Parent.color;
                    Parent.color = Color.Black;
                    Brother.left.color = Color.Black;
                    RightRotate(Brother);
                }


                else if (Brother.color == Color.Black && Brother.left.color == Color.Black && Brother.right.color == Color.Red)
                {
                    Brother.right.color = Brother.color;
                    Brother.color = Color.Red;
                    TreeElement BrotherRight = Brother.right;
                    LeftRotate(BrotherRight);
                    DelBalanceRight(NodeD);
                    return;
                }


                else if (Brother.color == Color.Black && Brother.right.color == Color.Black && Brother.left.color == Color.Black)
                {
                    Brother.color = Color.Red;
                    if (Parent.color == Color.Red)
                        Parent.color = Color.Black;
                    else
                    {
                        Parent.color = Color.Black;
                        if (Parent.prev != null)
                        {
                            if (Parent.prev.left == Parent)
                                DelBalance(Parent);
                            else if (Parent.prev.right == Parent)
                                DelBalanceRight(Parent);
                        }
                    }
                }


                else if (Brother.color == Color.Red && Parent.color == Color.Black)
                {
                    Brother.color = Color.Black;
                    Parent.color = Color.Red;
                    LeftRotate(Brother);
                }


            }
        }

       private void BalanceTree(TreeElement Node)
        {
            if (Node.prev == null)
                Case1(Node);
            else if (Node.prev.color == Color.Red && Node.color == Color.Red)
            {
                Case1(Node);
                Case2(Node);
                Case3(Node);
            }

            void Case1(TreeElement BalanceNode)
            {
                if (BalanceNode.prev == null && BalanceNode.color == Color.Red)
                {
                    BalanceNode.color = Color.Black;
                    return;
                }
                if (BalanceNode.prev == null)
                    return;
                if (BalanceNode.prev.color == Color.Black && BalanceNode.color == Color.Red)
                    return;


                TreeElement Uncle;
                if (BalanceNode.prev.prev.right == BalanceNode.prev)
                {
                    Uncle = BalanceNode.prev.prev.left;
                }
                else
                {
                    Uncle = BalanceNode.prev.prev.right;
                }
                if (Uncle.color == Color.Red && BalanceNode.prev.color == Color.Red)
                {
                    Uncle.color = Color.Black;
                    BalanceNode.prev.color = Color.Black;
                    BalanceNode.prev.prev.color = Color.Red;
                    BalanceTree(BalanceNode.prev.prev);
                }
                else
                    return;
            }

            void Case2(TreeElement BalanceNode)
            {
                if (BalanceNode.prev == null && BalanceNode.color == Color.Red)
                {
                    BalanceNode.color = Color.Black;
                    return;
                }
                TreeElement Uncle;
                if (BalanceNode.prev.prev.right == BalanceNode.prev)
                {
                    Uncle = BalanceNode.prev.prev.left;
                }
                else
                {
                    Uncle = BalanceNode.prev.prev.right;
                }
                TreeElement Parent = BalanceNode.prev;
                TreeElement Grand = BalanceNode.prev.prev;
                if (Parent.color == Color.Red && BalanceNode.color == Color.Red)
                {
                    if (Uncle.color == Color.Black && Parent.right == BalanceNode && Grand.left == Parent)
                    {
                        Grand.left = BalanceNode;
                        Parent.right = BalanceNode.left;
                        BalanceNode.left.prev = Parent;
                        BalanceNode.left = Parent;
                        BalanceNode.prev = Grand;
                        Parent.prev = BalanceNode;

                        BalanceTree(Parent);
                        return;
                    }
                    if (Uncle.color == Color.Black && Parent.left == BalanceNode && Grand.right == Parent)
                    {
                        Grand.right = BalanceNode;
                        Parent.left = BalanceNode.right;
                        BalanceNode.right.prev = Parent;
                        BalanceNode.right = Parent;
                        Parent.prev = BalanceNode;

                        BalanceNode.prev = Grand;
                        BalanceTree(Parent);
                        return;
                    }
                }


            }


            void Case3(TreeElement BalanceNode)
            {
                if (BalanceNode.prev == null && BalanceNode.color == Color.Red)
                {
                    BalanceNode.color = Color.Black;
                    return;
                }
                if (BalanceNode.prev == null || BalanceNode.prev.prev == null)
                    return;
                TreeElement Uncle;
                if (BalanceNode.prev.prev.right == BalanceNode.prev)
                {
                    Uncle = BalanceNode.prev.prev.left;
                }
                else
                {
                    Uncle = BalanceNode.prev.prev.right;
                }
                TreeElement Parent = BalanceNode.prev;
                TreeElement Grand = BalanceNode.prev.prev;
                if (Parent.color == Color.Red && BalanceNode.color == Color.Red)
                {
                    if (Parent.left == BalanceNode && Grand.left == Parent && Uncle.color == Color.Black)
                    {
                        Grand.left = Parent.right;
                        Parent.right.prev = Grand;

                        Parent.right = Grand;
                        Parent.prev = Grand.prev;
                        if (Grand.prev != null && Grand.prev.right == Grand)
                            Grand.prev.right = Parent;
                        if (Grand.prev != null && Grand.prev.left == Grand)
                            Grand.prev.left = Parent;
                        Grand.prev = Parent;

                        Grand.color = Color.Red;
                        Parent.color = Color.Black;
                        BalanceTree(Parent);
                        return;
                    }

                    if (Parent.right == BalanceNode && Grand.right == Parent && Uncle.color == Color.Black)
                    {
                        Grand.right = Parent.left;
                        Parent.left.prev = Grand;

                        Parent.left = Grand;
                        Parent.prev = Grand.prev;
                        if (Grand.prev != null && Grand.prev.right == Grand)
                            Grand.prev.right = Parent;
                        if (Grand.prev != null && Grand.prev.left == Grand)
                            Grand.prev.left = Parent;
                        Grand.prev = Parent;
                        Grand.color = Color.Red;
                        Parent.color = Color.Black;
                        BalanceTree(Parent);
                        return;
                    }
                }

            }
        }

        private void LeftRotate(TreeElement RotateNode)
        {
            TreeElement Parent = RotateNode.prev;
            if (Parent == null)
                return;
            TreeElement Grand = Parent.prev;

            TreeElement tmp = RotateNode.left;

            RotateNode.left = Parent;
            Parent.right = tmp;
            RotateNode.prev = Parent.prev;
            Parent.prev = RotateNode;
            tmp.prev = Parent;
            if (Grand != null && Grand.left == Parent)
                Grand.left = RotateNode;
            else if (Grand != null && Grand.right == Parent)
                Grand.right = RotateNode;

        }


        private void RightRotate(TreeElement RotateNode)
        {
            TreeElement Parent = RotateNode.prev;
            if (Parent == null)
                return;


            TreeElement Grand = Parent.prev;

            TreeElement tmp = RotateNode.right;

            RotateNode.right = Parent;
            Parent.left = tmp;
            RotateNode.prev = Parent.prev;
            Parent.prev = RotateNode;
            tmp.prev = Parent;
            if (Grand != null && Grand.left == Parent)
                Grand.left = RotateNode;
            else if (Grand != null && Grand.right == Parent)
                Grand.right = RotateNode;

        }

        public void Print()
        {
            if (Size == 0)
                return;
            while (root.prev != null)
                root = root.prev;
            Pprint(root);
            void Pprint(TreeElement roo)
            {
                if (roo == nil)
                    return;
                else
                {

                    Console.WriteLine(roo.color.ToString() + " " + roo.Key.ToString());
                    Pprint(roo.left);
                    Pprint(roo.right);
                }
            }
        }


        public void OrderPrint()
        {
            if (Size == 0)
                return;
            while (root.prev != null)
                root = root.prev;
            Pprint(root);
            void Pprint(TreeElement roo)
            {
                if (roo == nil)
                    return;
                else
                {
                    Pprint(roo.left);
                    Console.WriteLine(roo.color.ToString() + " " + roo.Key.ToString());
                    Pprint(roo.right);
                }
            }


        }

        internal enum Color
        {
            Black,
            Red
        }

        internal class TreeElement
        {
            public TreeElement left = null;
            public TreeElement right = null;
            public TreeElement prev = null;

            public K Key;
            public Color color = Color.Black;
        }

        private IEnumerable<K> OrdesrPrint()
        {
            if (Size == 0)
                yield break;
            while (root.prev != null)
                root = root.prev;

            Stack<TreeElement> stack = new Stack<TreeElement>();
            TreeElement copy = root;

            while (copy != nil || stack.Count != 0)
            {
                while (copy != nil)
                {
                    stack.Push(copy);
                    copy = copy.left;
                }
                copy = stack.Pop();
                yield return copy.Key;
                copy = copy.right;
            }


        }

        private IEnumerable<K> BFS()
        {

            TreeElement copy = root;
            Queue<TreeElement> queue = new Queue<TreeElement>();
            queue.Enqueue(copy);
            while (queue.Count > 0)
            {
                copy = queue.Dequeue();
                K pair = copy.Key;
                yield return pair;
                if (copy.left != nil)
                    queue.Enqueue(copy.left);
                if (copy.right != nil)
                    queue.Enqueue(copy.right);
            }
        }
    }   
    }