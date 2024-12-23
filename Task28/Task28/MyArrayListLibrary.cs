using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyIteratorLib;
using MyInterFaceLib;
namespace MyArrayListLibrary
{
    internal class MyArrayList<T>: MyList<T>
    {
        private T[] elementData;
        private int Size;

        private void ShiftArray_ToLeft(int index = 0)
        {
            for (int i = index; i < Size - 1; i++) elementData[i] = elementData[i + 1];
            Size = Size - 1;
        }

        private void ShiftArray_ToRight(int index = 0)
        {
            Size = Size + 1;
            for (int i = Size - 1; i > index; i--) elementData[i] = elementData[i - 1];
        }

        public MyArrayList()
        {
            Size = 0;
            elementData = new T[0];
        }
        public MyArrayList(MyCollection<T> C)
        {
            T[] array = C.toArray();
            elementData = new T[array.Length];
            Size = array.Length;
            for (int i = 0; i < Size; i++)
            {
                elementData[i] = array[i];
            }
        }

        public MyArrayList(int capacity)
        {
            Size = 0;
            elementData = new T[capacity];
        }

        public void add(T value)
        {
            if (elementData.Length == Size)
            {
                T[] newArray = new T[Size];
                for (int i = 0; i < Size; i++) newArray[i] = elementData[i];
                elementData = new T[Convert.ToInt32(Size * 1.5) + 1];
                for (int i = 0; i < Size; i++) elementData[i] = newArray[i];
            }
            elementData[Size] = value;
            Size++;
        }

        public void addAll(MyCollection<T> C)
        {
            T[] array = C.toArray();
            int size2 = array.Length;
            if (Size + size2 > elementData.Length) Console.WriteLine("Переполнение массива");
            else if (size2 == 0) Console.WriteLine("Массив пуст");
            else
            {
                {
                    for (int i = Size; i < Size + size2; i++) elementData[i] = array[i - Size];
                }
                Size = Size + size2;
            }
        }

        public void clear()
        {
            int capacity = elementData.Length;
            elementData = new T[capacity];
            Size = 0;
        }

        public bool contains(T value)
        {
            for (int i = 0; i < Size; i++)
            {
                if (elementData[i].Equals(value)) return true;
            }
            return false;
        }

        public bool containsAll(T[] array)
        {
            bool[] contain = new bool[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (elementData[j].Equals(array[i])) contain[i] = true;
                }
            }
            for (int i = 0; i < contain.Length; i++) if (contain[i] == false) return false;
            return true;
        }

        public bool isEmpty()
        {
            if (Size == 0) return true;
            else return false;
        }

        public void remove(T value)
        {
            for (int i = 0; i < Size; i++)
            {
                if (elementData[i].Equals(value)) { ShiftArray_ToLeft(i); }
            }
        }

        public void removeAll(MyCollection<T> c)
        {
            T[] array = c.toArray();
            foreach (T value in array)
            {
                if (contains(value) == true) remove(value);
            }
        }

        public void retainAll(MyCollection<T>c)
        {
            T[] array = c.toArray();
            for (int i = 0; i < Size; i++)
            {
                bool y = false;
                for (int j = 0; j < array.Length; j++)
                {
                    if (elementData[i].Equals(array[j])) y = true;
                }
                if (y == false) ShiftArray_ToLeft(i);
            }
        }

        public int size() { return Size; }

        public T[] toArray()
        {
            T[] array = new T[Size];
            for (int i = 0; i < Size; i++) array[i] = elementData[i];
            return array;
        }

        public T[] toArray(T[] array)
        {
            if (array == null)
            {
                array = new T[Size];
                for (int i = 0; i < Size; i++)
                {
                    array[i] = elementData[i];
                }
                return array;
            }
            else
            {
                if (array.Length < Size)
                {
                    Console.WriteLine("Недостаточно места для всех элементов массива.");
                    return array;
                }
                else
                {
                    for (int i = 0; i < Size; i++) array[i] = elementData[i];
                    return array;
                }
            }
        }

        public void add(int index, T value)
        {
            if (index > Size)
            {
                Console.WriteLine("Индекс за пределами массива");
                return;
            }
            if (Size == elementData.Length)
            {
                Console.WriteLine("В массиве недостаточно места для нового элемента.");
                return;
            }
            ShiftArray_ToRight(index);
            elementData[index] = value;
        }

        public void addAll(int index, MyCollection<T> C)
        {
            T[] array =C.toArray();
            if (index > Size)
            {
                Console.WriteLine("Индекс за пределами массива");
                return;
            }
            if (Size + array.Length > elementData.Length)
            {
                Console.WriteLine("В массиве недостаточно места для новых элемента.");
                return;
            }
            for (int i = array.Length - 1; i >= 0; i--)
            {
                ShiftArray_ToRight(index);
                elementData[index] = array[i];
            }
        }

        public T get(int index)
        {
            try
            {
                return elementData[index];
            }
            catch
            {
                Console.WriteLine("Индекс за пределами массива.");
                return default(T);
            }
        }
        public int indexOf(T value)
        {
            int index = -1;
            for (int i = 0; i < Size; i++)
            {
                if (elementData[i].Equals(value))
                {
                    index = i;
                    return index;
                }
            }
            return index;
        }

        public int lastIndexOf(T value)
        {
            int index = -1;
            for (int i = 0; i < Size; i++)
            {
                if (elementData[i].Equals(value))
                {
                    index = i;
                }
            }
            return index;
        }

        public T remove(int index)
        {
            if (index >= Size)
            {
                Console.WriteLine("Индекс за пределами массива.");
                return default(T);
            }
            T value = elementData[index];
            ShiftArray_ToLeft(index);
            return value;
        }

        public void set(int index, T value)
        {
            if (index >= Size)
            {
                Console.WriteLine("Индекс за пределами массива");
                return;
            }
            elementData[index] = value;
        }

        public T[] subList(int fromIndex, int toIndex)
        {
            if (fromIndex >= Size || fromIndex < 0 || toIndex <= fromIndex || toIndex < 0 || toIndex > Size)
            {
                Console.WriteLine("Ошибка индекса.");
                return null;
            }
            T[] array = new T[toIndex - fromIndex];
            for (int i = fromIndex; i < toIndex; i++) { array[i - fromIndex] = elementData[i]; }
            return array;
        }

        public iMyIter<T> listIterator()
        {
            return new MyItr(0,this);
        }

        public iMyIterator<T> listIterator(int index)
        {
            return new MyItr(index,this);
        }

        internal class MyItr : iMyIterator<T> {
            private int cursor;
            private MyArrayList<T> list;
            public bool HasNext()
            {
                if (cursor< list.Size) return true;
                else return false;
            }

            public T Next()
            {
                cursor++;
                return list.get(cursor-1);
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
                return list.get(cursor);
            }

            public int NextIndex()
            {
                return cursor+1;
            }

            public int PreviousIndex()
            {
                return cursor - 1;
            }

            public void Remove()
            {
                list.remove(cursor);
            }

            public void Set(T element)
            {
                list.set(cursor, element);
            }

            public void Add(T element)
            {
                list.add(cursor, element);
            }

            public MyItr(int index, MyArrayList<T> list)
            {
                cursor = index;
                this.list = list;
            }  
        }
    }
}
