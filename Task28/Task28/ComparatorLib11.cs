using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparatorLib
{
    internal class PriorityQueueCompare //: IComparer<object>
    {
        public int Compare(object x, object y)
        {

            if (x == null && y != null) return -1;
            if (x != null && y == null) return 1;
            if (x == null && y == null) return 0;

            // Преобразуем объекты к типу int
            string string1 = x.ToString();
            string string2 = y.ToString();
            int num1 = string1.Length;
            int num2 = string2.Length;



            // Сравниваем числа
            if (num1 < num2)
            {
                return -1;
            }
            else if (num1 == num2)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
