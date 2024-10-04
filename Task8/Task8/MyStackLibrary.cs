using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyVectorLibrary;
namespace MyStackLibrary
{
    internal class MyStack<T> : MyVector<T> 
    {
        public void push(T item)
        {
            add(item);
        }

        public T pop()
        {
            int num = Size();
            if (num > 0)
            {
                T item = remove(num - 1);
                return item;
            }
            Console.WriteLine("Нет элементов");
            return default(T);
        }

        public T peek()
        {
            return lastElement();
        }

        public bool empty()
        {
            return isEmpty();
        }

        public int search (T item)
        {
            int num = indexOf(item);
            if (num == -1) return -1;
            return Size()-num;
        }
    }
}
