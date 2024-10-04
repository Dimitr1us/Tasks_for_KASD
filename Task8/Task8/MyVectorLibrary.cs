using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVectorLibrary
{
    internal class MyVector<T>
    {
        private T[] elementData;
        private int elementCount;
        private int capacityIncrement;

        private void ShiftArray_ToLeft(int index = 0)
        {
            for (int i = index; i < elementCount - 1; i++) elementData[i] = elementData[i + 1];
            elementCount = elementCount - 1;
        }

        private void ShiftArray_ToRight(int index = 0)
        {
            elementCount = elementCount + 1;
            for (int i = elementCount - 1; i > index; i--) elementData[i] = elementData[i - 1];
        }

        private void get_bigger(){
            T[] newArray = new T[elementCount];
            for (int i = 0; i < elementCount; i++) newArray[i] = elementData[i];
            if (capacityIncrement != 0)
            {
                elementData = new T[Convert.ToInt32(elementCount) + elementCount];
            }
            else
            {
                elementData = new T[Convert.ToInt32(elementCount * 2)+1];
            }
            for (int i = 0; i < elementCount; i++) elementData[i] = newArray[i];
        }

        public MyVector(int initialCapacity,int capacityIncrement)
        {
            elementCount = 0;
            elementData = new T[initialCapacity];
        }

        public MyVector(int initialCapacity)
        {
            elementCount = 0;
            elementData = new T[initialCapacity];
            capacityIncrement = 0;
        }

        public MyVector()
        {
            elementCount = 0;
            elementData = new T[10];
            capacityIncrement = 0;
        }

        public MyVector(T[] array)
        {
            elementData = new T[array.Length];
            elementCount = array.Length;
            for (int i = 0; i < elementCount; i++)
            {
                elementData[i] = array[i];
            }
            capacityIncrement = 0;
        }



        public void add(T value)
        {
            if (elementData.Length == elementCount)
            {
                get_bigger();
            }
            elementData[elementCount] = value;
            elementCount++;
        }

        public void addAll(T[] array)
        {
            int size2 = array.Length;
            while (size2 + elementCount>=elementData.Length){ get_bigger(); }
            if (size2 == 0) Console.WriteLine("Массив пуст");
            else
            {
                {
                    for (int i = elementCount; i < elementCount + size2; i++) elementData[i] = array[i - elementCount];
                }
                elementCount = elementCount + size2;
            }
        }

        public void clear()
        {
            int capacity = elementData.Length;
            elementData = new T[capacity];
            elementCount = 0;
        }

        public bool contains(T value)
        {
            for (int i = 0; i < elementCount; i++)
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
                for (int j = 0; j < elementCount; j++)
                {
                    if (elementData[j].Equals(array[i])) contain[i] = true;
                }
            }
            for (int i = 0; i < contain.Length; i++) if (contain[i] == false) return false;
            return true;
        }

        public bool isEmpty()
        {
            if (elementCount == 0) return true;
            else return false;
        }

        public void remove(T value)
        {
            for (int i = 0; i < elementCount; i++)
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
            for (int i = 0; i < elementCount; i++)
            {
                bool y = false;
                for (int j = 0; j < array.Length; j++)
                {
                    if (elementData[i].Equals(array[j])) y = true;
                }
                if (y == false) ShiftArray_ToLeft(i);
            }
        }

        public int Size() { return elementCount; }

        public T[] toArray()
        {
            T[] array = new T[elementCount];
            for (int i = 0; i < elementCount; i++) array[i] = elementData[i];
            return array;
        }

        public T[] toArray(T[] array)
        {
            if (array == null)
            {
                array = new T[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    array[i] = elementData[i];
                }
                return array;
            }
            else
            {
                if (array.Length < elementCount)
                {
                    Console.WriteLine("Недостаточно места для всех элементов массива.");
                    return array;
                }
                else
                {
                    for (int i = 0; i < elementCount; i++) array[i] = elementData[i];
                    return array;
                }
            }
        }

        public void add(int index, T value)
        {
            if (elementCount == elementData.Length)
                {
                get_bigger();
                }
            if (index > elementCount)
                {
                    Console.WriteLine("Индекс за пределами массива");
                    return;
                }
            
            ShiftArray_ToRight(index);
            elementData[index] = value;
        }

        public void addAll(int index, T[] array)
        {
            while (elementCount + array.Length > elementData.Length)
                {
                get_bigger();
                }
            if (index > elementCount)
            {
                Console.WriteLine("Индекс за пределами массива");
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
            for (int i = 0; i < elementCount; i++)
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
            for (int i = 0; i < elementCount; i++)
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
            if (index >= elementCount)
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
            if (index >= elementCount)
            {
                Console.WriteLine("Индекс за пределами массива");
                return;
            }
            elementData[index] = value;
        }

        public T[] subList(int fromIndex, int toIndex)
        {
            if (fromIndex >= elementCount || fromIndex < 0 || toIndex <= fromIndex || toIndex < 0 || toIndex > elementCount)
            {
                Console.WriteLine("Ошибка индекса.");
                return null;
            }
            T[] array = new T[toIndex - fromIndex];
            for (int i = fromIndex; i < toIndex; i++) { array[i - fromIndex] = elementData[i]; }
            return array;
        }

        public T firstElement()
        {
            if (elementCount > 0)
            {
                return elementData[0];
            }
            else
            {
                Console.WriteLine("Не существует первого элемента");
                return default(T);
            }
        }

        public T lastElement()
        {
            if (elementCount > 0)
            {
                return elementData[elementCount-1];
            }
            else
            {
                Console.WriteLine("Не существует последнего элемента");
                return default(T);
            }
        }

        public void removeElementAt(int index)
        {
            if (index >= elementCount)
            {
                Console.WriteLine("Индекс за пределами массива.");
                return;
            }
            ShiftArray_ToLeft(index);
            return;
        }

        public void removeRange(int fromIndex, int toIndex)
        {
            if (fromIndex >= elementCount || fromIndex < 0 || toIndex <= fromIndex || toIndex < 0 || toIndex > elementCount)
            {
                Console.WriteLine("Ошибка индекса.");
                return;
            }
            for (int i = fromIndex; i < toIndex; i++) { ShiftArray_ToLeft(i); }
            return;
        }
    }
}
