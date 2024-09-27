using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArrayListLibrary
{
    internal class MyArrayList<T>
    {
        private T[] elementData;
        private int size;

        private void ShiftArray_ToLeft(int index = 0)
        {
            for (int i = index; i < size - 1; i++) elementData[i] = elementData[i + 1];
            size = size - 1;
        }

        private void ShiftArray_ToRight(int index = 0)
        {
            size = size + 1;
            for (int i = size - 1; i > index; i--) elementData[i] = elementData[i - 1];
        }

        public MyArrayList()
        {
            size = 0;
            elementData = new T[0];
        }
        public MyArrayList(T[] array)
        {
            elementData = new T[array.Length];
            size = array.Length;
            for (int i = 0; i < size; i++)
            {
                elementData[i] = array[i];
            }
        }

        public MyArrayList(int capacity)
        {
            size = 0;
            elementData = new T[capacity];
        }

        public void add(T value)
        {
            if (elementData.Length == size)
            {
                T[] newArray = new T[size];
                for (int i = 0; i < size; i++) newArray[i] = elementData[i];
                elementData = new T[Convert.ToInt32(size * 1.5) + 1];
                for (int i = 0; i < size; i++) elementData[i] = newArray[i];
            }
            elementData[size] = value;
            size++;
        }

        public void addAll(T[] array)
        {
            int size2 = array.Length;
            if (size + size2 > elementData.Length) Console.WriteLine("Переполнение массива");
            else if (size2 == 0) Console.WriteLine("Массив пуст");
            else
            {
                {
                    for (int i = size; i < size + size2; i++) elementData[i] = array[i - size];
                }
                size = size + size2;
            }
        }

        public void clear()
        {
            int capacity = elementData.Length;
            elementData = new T[capacity];
            size = 0;
        }

        public bool contains(T value)
        {
            for (int i = 0; i < size; i++)
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
                for (int j = 0; j < size; j++)
                {
                    if (elementData[j].Equals(array[i])) contain[i] = true;
                }
            }
            for (int i = 0; i < contain.Length; i++) if (contain[i] == false) return false;
            return true;
        }

        public bool isEmpty()
        {
            if (size == 0) return true;
            else return false;
        }

        public void remove(T value)
        {
            for (int i = 0; i < size; i++)
            {
                if (elementData[i].Equals(value)) { ShiftArray_ToLeft(i); }
            }
        }

        public void removeAll(T[] array)
        {
            foreach (T value in array)
            {
                if (contains(value) == true) remove(value);
            }
        }

        public void retainAll(T[] array)
        {
            for (int i = 0; i < size; i++)
            {
                bool y = false;
                for (int j = 0; j < array.Length; j++)
                {
                    if (elementData[i].Equals(array[j])) y = true;
                }
                if (y == false) ShiftArray_ToLeft(i);
            }
        }

        public int Size() { return size; }

        public T[] toArray()
        {
            T[] array = new T[size];
            for (int i = 0; i < size; i++) array[i] = elementData[i];
            return array;
        }

        public T[] toArray(T[] array)
        {
            if (array == null)
            {
                array = new T[size];
                for (int i = 0; i < size; i++)
                {
                    array[i] = elementData[i];
                }
                return array;
            }
            else
            {
                if (array.Length < size)
                {
                    Console.WriteLine("Недостаточно места для всех элементов массива.");
                    return array;
                }
                else
                {
                    for (int i = 0; i < size; i++) array[i] = elementData[i];
                    return array;
                }
            }
        }

        public void add(int index, T value)
        {
            if (index > size)
            {
                Console.WriteLine("Индекс за пределами массива");
                return;
            }
            if (size == elementData.Length)
            {
                Console.WriteLine("В массиве недостаточно места для нового элемента.");
                return;
            }
            ShiftArray_ToRight(index);
            elementData[index] = value;
        }

        public void addAll(int index, T[] array)
        {
            if (index > size)
            {
                Console.WriteLine("Индекс за пределами массива");
                return;
            }
            if (size + array.Length > elementData.Length)
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
            for (int i = 0; i < size; i++)
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
            for (int i = 0; i < size; i++)
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
            if (index >= size)
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
            if (index >= size)
            {
                Console.WriteLine("Индекс за пределами массива");
                return;
            }
            elementData[index] = value;
        }

        public T[] subList(int fromIndex, int toIndex)
        {
            if (fromIndex >= size || fromIndex < 0 || toIndex <= fromIndex || toIndex < 0 || toIndex > size)
            {
                Console.WriteLine("Ошибка индекса.");
                return null;
            }
            T[] array = new T[toIndex - fromIndex];
            for (int i = fromIndex; i < toIndex; i++) { array[i - fromIndex] = elementData[i]; }
            return array;
        }
    }
}
