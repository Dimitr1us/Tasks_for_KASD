using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mylib
{
    public class MyArrayList<T>
    {
        private int size;
        private T[] elementData;
        public MyArrayList()
        {
            elementData = null;
            size = 0;
        }


        public MyArrayList(T[] array)
        {
            // Инициализируем массив elementData элементами из массива array
            elementData = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                elementData[i] = array[i];
            }
            // Устанавливаем размер массива size
            size = array.Length;
        }

        public MyArrayList(int capacity)
        {
            T[] array = new T[capacity];
            size = capacity;

        }

        //в конец
        public void Add(T e)
        {
            if (size == elementData.Length)
            {
                T[] array = new T[(int)(size * 1.5) + 1];
                for (int i = 0; i < size; i++) array[i] = elementData[i];
                elementData = array;
            }
            elementData[size] = e;
        }
        public void AddAll(T[] a)
        {
            int n = a.Length;
            for (int i = 0; i < n; i++) Add(a[i]);
        }
        //удаление всех элементов
        public void Clear() => size = 0;
        //проверка есть ли элемент в массиве
        public bool Contains(T o)
        {
            foreach (T t in elementData)
            {
                if (t.Equals(o) || (t == null && o == null)) return true;
            }
            return false;
        }

        //проверка есть ли массив в дин массиве
        public bool ContainsAll(T[] a)
        {
            int n = a.Length;
            for (int i = 0; i < elementData.Length; i++)
            {
                if (Contains(elementData[i])) return true;
            }
            return false;
        }
        public bool isEmpty()
        {
            if (size == 0) return true;
            else return false;
        }

        //удаление объектв из массива есть объект там есть
        public void Remove(T o)
        {
            for (int i = 0; i < size; i++)
            {
                if (Contains(o))
                {
                    T removedElement = elementData[i]; // Сохраняем удаляемый элемент
                    for (int j = i; j < size - 1; j++)
                    {
                        elementData[j] = elementData[j + 1]; // Сдвигаем элементы влево
                    }
                    size--; // Уменьшаем размер массива
                }
            }

        }
        //удаление указанного массива
        public void RemoveAll(T[] a)
        {
            int len = a.Length;
            for (int i = 0; i < len; i++) Remove(a[i]);
        }
        //оставить указанные элементы, а остальные удалить
        public void RetainALl(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                elementData[i] = a[i];
            }
            size = a.Length;
        }
        //размер динамического массива
        public int Size()
        {
            return size;
        }
        //возвращать массив, содержащего все элементы дин массива
        public void ToArray()
        {
            T[] array = new T[size];
            for (int i = 0; i < size; i++) array[i] = elementData[i];
        }
        //возвращать масиив, содержащего все элементы дин массива, если аргумент пустой, то создаем новый массив, в который копируется элементы
        public void ToArray(T[] a)
        {
            int n = a.Length;
            if (n <= size && n > 0)
            {
                T[] array = new T[n + size];
                for (int i = 0; i < n; i++) array[i] = a[i];
                for (int i = n; i < size; i++) array[i] = elementData[i];
                a = array;

            }
            else if (n < 0)
            {
                T[] arr = new T[size];
                for (int i = 0; i < size; i++) arr[i] = elementData[i];
                a = arr;
            }
        }

        //добавлене элемента на указанную позицию
        public void Add(int index, T e)
        {

            // Если массив заполнен, увеличиваем его размер
            if (size == elementData.Length)
            {

                T[] array = new T[(int)(size * 1.5) + 1];
                for (int i = 0; i < size; i++) array[i] = elementData[i];
                elementData = array;
            }

            // Сдвигаем элементы, начиная с указанного индекса, вправо
            for (int i = size; i > index; i--)
            {
                elementData[i] = elementData[i - 1];
            }

            // Вставляем значение на указанный индекс
            elementData[index] = e;
            size++;
        }

        //добавления элементОВ на указанную позицию
        public void Add(int e, T[] a)
        {
            for (int i = 0; i < a.Length; i++) Add(i, a[i]);
        }
        //возвращения элемента в указанной позиции
        public T Get(int index)
        {
            return elementData[index];
        }

        //возвращение индекса указанного объекта, если объекта нет, то return -1
        public int IndexOf(object o)
        {
            for (int i = 0; i < size; i++)
            {
                if (elementData[i].Equals(o))
                {
                    return i; // Возвращаем индекс первого найденного элемента
                }
            }
            return -1; // Если элемент не найден, возвращаем -1
        }
        //нахождения последнего вхождения элемента, если нет, return -1
        public int LastIndexOf(object o)
        {
            int lastIndex = -1;
            for (int i = 0; i < size; i++)
            {
                if (elementData[i].Equals(o))
                {
                    lastIndex = i; // Записываем индекс последнего найденного элемента
                }
            }
            return lastIndex; // Возвращаем индекс последнего найденного элемента
        }
        //удаление и возвращение элемента в указанной позиции
        public T Remove(int index)
        {

            T removedElement = elementData[index]; // Сохраняем удаляемый элемент
            for (int i = index; i < size - 1; i++)
            {
                elementData[i] = elementData[i + 1]; // Сдвигаем элементы влево
            }
            size--; // Уменьшаем размер массива
            return removedElement; // Возвращаем удаленный элемент
        }
        //замена элемента в уазанной позиции новым элементов
        public void Set(int index, T e)
        {


            elementData[index] = e;
        }
        //возвращение элементоа в диапазоне
        public T[] SubList(int fromIndex, int toIndex)
        {

            int length = toIndex - fromIndex; // Вычисляем размер подмассива
            T[] subArray = new T[length]; // Создаем новый массив для подмассива
            for (int i = 0; i < length; i++)
            {
                subArray[i] = elementData[fromIndex + i]; // Копируем элементы в подмассив
            }
            return subArray; // Возвращаем подмассив
        }
        public void Print()
        {
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(elementData[i] + " ");
            }
        }
    }

    public class Node<T>
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
    }

    public class MyLInkedList<T>
    {
        private Node<T> first;
        private Node<T> last;
        int size;
        public MyLInkedList()
        {
            first = null;
            last = null;
            size = 0;
        }
        public MyLInkedList(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Node<T> newNode = new Node<T>(a[i]);

                if (last == null)
                {
                    first = newNode;
                    last = newNode;
                }
                else
                {
                    newNode.Previous = last;
                    last.Next = newNode;
                    last = newNode;
                }
            }
            size = a.Length;
        }
        public void Add(T e)
        {
            Node<T> newNode = new Node<T>(e);

            if (last == null)
            {
                first = newNode;
                last = newNode;
            }
            else
            {
                newNode.Previous = last;
                last.Next = newNode;
                last = newNode;
            }
            size++;
        }
        public void AddAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++) Add(a[i]);
        }
        public void Clear() => size = 0;
        public bool Contains(object o)
        {
            Node<T> current = first;

            while (current != null)
            {
                if (current.Data.Equals(o))
                {
                    return true;
                }
                current = current.Next;
            }

            return false;
        }
        public bool ContainsAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (Contains(a[i])) return true;
            }
            return false;
        }
        public bool isEmpty() { return size == 0; }
        public void Remove(object o)
        {
            Node<T> node = first;
            if (node == null) return;
            if (node.Data.Equals(o))
            {
                first = node.Next;
                if (first != null) first.Previous = null;
                return;
            }
            while (node != null && node.Data.Equals(o) == false)
            {
                node = node.Previous;
            }
            if (node == null) return;
            if (node.Next != null) node.Next.Previous = node.Previous;

            if (node.Previous != null) node.Previous.Next = node.Next;

            size--;
        }
        public void RemoveAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++) Remove(a[i]);
        }
        public void RetainAll(T[] a)
        {
            Clear();
            for (int i = 0; i < a.Length; i++)
            {
                Node<T> node = new Node<T>(a[i]);
                if (first == null)
                {
                    first = node;
                    last = node;
                }
                else
                {
                    node.Previous = first;
                    first.Previous = node;
                    first = node;
                }
            }
            size = a.Length;
        }
        public int Size() { return size; }
        public void ToArray()
        {
            T[] array = new T[size];
            Node<T> node = first;
            int i = 0;
            while (first != null)
            {
                array[i] = node.Data;
                i++;
                node = node.Next;
            }
        }
        public void ToArray(T[] a)
        {
            T[] array = new T[size + a.Length];
            for (int j = 0; j < a.Length; j++) array[j] = a[j];
            Node<T> node = first;
            int i = a.Length;
            while (first != null)
            {
                array[i] = node.Data;
                i++;
                node = node.Next;
            }
        }
        public void Add(int index, T e)
        {
            Node<T> node = first;
            int i = 0;
            while (first != null)
            {
                if (i == index)
                {
                    node.Data = e;
                }
                i++;
                first = node.Next;
            }
        }
        public void AddAll(int index, T[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Add(index, a[i]);
                index++;
            }
        }
        public T Get(int index)
        {
            Node<T> node = first;
            int i = 0;
            T y;
            while (first != null)
            {
                if (i == index) return node.Data;
                i++;
                first = node.Next;
            }
            return default(T);
        }
        public int IndexOf(object o)
        {
            Node<T> node = first;
            int i = 0;
            int k = 0;
            while (first != null)
            {
                if (node.Data.Equals(o)) return i;
                i++;
                node = node.Next;
            }
            return -1;
        }
        public int LastIndexOf(object o)
        {
            Node<T> node = first;
            int i = 0;
            int k = -1;
            while (k != 1 || first != null)
            {
                if (node.Data.Equals(o)) k = i;
                i++;
                node = node.Next;
            }
            return k;
        }
        public T Remove(int index)
        {
            Node<T> node = first;
            int i = 0;
            T element = node.Data;
            while (first != null)
            {
                if (i == index) { Remove(node.Data); element = node.Data; }
                i++;
                node = node.Next;
            }
            return element;
        }
        public void Set(int index, T e)
        {
            Node<T> node = first;
            int i = 0;
            T element = node.Data;
            while (first != null)
            {
                if (i == index) node.Data = e;
                i++;
                node = node.Next;
            }
        }
        public void SubList(int fromIndex, int ToIndex)
        {
            Node<T> node = first;
            int i = 0;
            T[] array = new T[ToIndex - fromIndex + 1];

            while (first != null)
            {
                if (i == fromIndex)
                {
                    for (int j = 0; j < array.Length; j++)
                    {
                        array[j] = node.Data;
                        node = node.Next;
                    }
                }
                i++;
                node = node.Next;
            }
        }
        public T Element()
        {
            return first.Data;
        }
        public bool Offer(T obj)
        {
            Add(obj);
            if (Contains(obj)) return true;
            else return false;
        }
        public T Peek()
        {
            if (size == 0) return default(T);
            else return first.Data;
        }
        public T Poll()
        {
            if (size == 0) return default(T);
            else
            {
                T e = first.Data;
                Remove(e);
                return e;
            }
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
        }
        public void AddLast(T obj)
        {
            Node<T> newNode = new Node<T>(obj);

            if (last == null)
            {
                first = newNode;
                last = newNode;
            }
            else
            {
                newNode.Previous = last;
                last.Next = newNode;
                last = newNode;
            }
        }
        public T GetFirst()
        {
            return first.Data;
        }
        public T GetLast()
        {
            return last.Data;
        }
        public bool OfferFirst(T obj)
        {
            AddFirst(obj);
            if (Contains(obj)) return true;
            else return false;
        }
        public bool OfferLast(T obj)
        {
            AddLast(obj);
            if (Contains(obj)) return true;
            else return false;
        }
        public T Pop()
        {
            T e = first.Data;
            Remove(e);
            return e;
        }
        public void Push(T obj)
        {
            AddFirst(obj);
        }
        public T PeekFirst()
        {
            if (size == 0) return default(T);
            else return first.Data;
        }
        public T PeekLast()
        {
            if (size == 0) return default(T);
            else return last.Data;
        }
        public T PollFirst()
        {
            if (size == 0) return default(T);
            else
            {
                T e = first.Data;
                Remove(e);
                return e;
            }
        }
        public T PollLast()
        {
            if (size == 0) return default(T);
            else
            {
                T e = last.Data;
                Remove(e);
                return e;
            }
        }
        public T RemoveFirst()
        {
            T e = first.Data;
            Remove(e);
            return e;
        }
        public T RemoveLast()
        {
            T e = last.Data;
            Remove(e);
            return e;
        }
        public bool RemoveLastOccurrence(T obj)
        {
            Node<T> newNode = new Node<T>(obj);

            if (last == null)
            {
                first = newNode;
                last = newNode;
                return true;
            }
            else
            {
                newNode.Previous = last;
                last.Next = newNode;
                last = newNode;
                return true;
            }
            return false;
        }
        public bool RemoveFirstOccurrence(T obj)
        {
            Node<T> newNode = new Node<T>(obj);

            if (first == null)
            {
                first = newNode;
                last = newNode;
                return true;
            }
            else
            {
                newNode.Next = first;
                first.Previous = newNode;
                first = newNode;
                return true;
            }
            return false;
        }
        public void Print()
        {
            Console.WriteLine(this);
        }
        public override string ToString()
        {
            if (first == null)
            {
                return "Пустой список";
            }

            string result = "Список: ";
            Node<T> current = first;

            while (current != null)
            {
                result += current.Data + " ";
                current = current.Next;
            }

            return result;
        }
    }
}
