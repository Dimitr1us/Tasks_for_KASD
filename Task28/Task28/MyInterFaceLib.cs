using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyIteratorLib;
namespace MyInterFaceLib
{
    internal interface MyCollection<T>
    {
        void add(T item);
        void addAll(MyCollection<T> C);
        void clear();
        bool contains(T a);
        bool containsAll(T[] a);
        bool isEmpty();
        void remove(T a);
        void removeAll(MyCollection<T> C);
        void retainAll(MyCollection<T> C);
        int size();
        T[] toArray();
        T[] toArray(T[] a);
    }

    internal interface MyList<T>: MyCollection<T>
    {
        void add(int index, T a);
        void addAll(int index, MyCollection<T> C);

        T get(int index);
        int indexOf(T a);

        int lastIndexOf(T a);

        iMyIter<T> listIterator();


        T remove(int index);

        void set(int index, T a);

        T[] subList(int fromIndex,int toIndex); 
    }

    internal interface MyQueue<T> : MyCollection<T>
    {
        T element();
        bool offer(T a);

        T peek();
        T poll();
    }

    internal interface MyDeque<T>: MyCollection<T>
    {
         void addFirst(T a);
        void addLast(T a);
        T getFirst();
        T getLast();
        bool offerFirst(T a);
        bool offerLast(T a);
        T pop();
        void push(T a);
        T peekFirst();
        T peekLast();
        T pollFirst();
        T pollLast();
        T removeFirst();
        T removeLast();
        bool removeFirstOccurence(T a);
        bool removeLastOccurence(T a);
    }

    internal interface MySet<T>: MyCollection<T>
    {
        T first();
        T last();
        T[] subSet(T fromElement, T toElement);
        T[] headSet(T toElement);
        T[] tailSet(T fromElement);
    }

    internal interface MySortedSet<T>: MySet<T>
    {
        T first();
        T last();
    }

    internal interface MyNavigableSet<T>: MySortedSet<T>
    {
        T lowerKey(T key);
        T floorKey(T key);
        T higherKey(T key);
        T ceilingKey(T key);
        T pollFirstEntry();
        T pollLastEntry();     
    }

    internal interface MyMap<T,G>
    {
        int size();
        bool containsKey(T key);
        bool containsValue(G value);
        List<(T, G)> entrySet();
        G get(T key);
        bool isEmpty();
        List<T> keySet();
        void put(T key, G value);
        void remove(T key);
    }

    internal interface MySortedMap<T,G>: MyMap<T, G>
    {
        T firstKey();
        T lastKey();
        MySortedMap<T,G> headMap(T end);
        MySortedMap<T,G> tailMap(T start);
        MySortedMap<T,G> subMap(T start, T end);
    }
    internal interface MyNavigableMap<T,G>: MySortedMap<T, G>
    {
        List<(T key, G value)> lowerEntry(T key);
        List<(T key,G value)> floorEntry(T key);
        List<(T key,G value)> higherEntry(T key);
        List<(T key, G value)> ceilingEntry(T key);
        List<T> lowerKey(T key);
        List<T> floorKey(T key);
        List<T> higherKey(T key);
        List<T> ceilingKey(T key);
        T pollFirstEntry();
        T pollLastEntry();
        (T,G) firstEntry();
        (T,G) lastEntry();
    }
}
