using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLib;
namespace ComparatorLib
{
    internal class PriorityQueueCompare //: IComparer<object>
    {
        public int Compare(Application x, Application y)
        {

            if (x == null && y != null) return -1;
            if (x != null && y == null) return 1;
            if (x == null && y == null) return 0;

            double num1 = x.NumberOfPriority;
            double num2 = y.NumberOfPriority;

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
