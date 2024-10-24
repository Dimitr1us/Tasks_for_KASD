using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparatorLib
{
    public interface IComparator<T>
    {
        int Compare(T x, T y);
    }

    public class Comparator : IComparator<int>
    {
        public int Compare(int x, int y)
        {
            string strX = x.ToString();
            string strY = y.ToString();

            // Сравниваем длины строк
            if (strX.Length > strY.Length)
            {
                return 1; // x длиннее, возвращаем 1
            }
            else if (strX.Length < strY.Length)
            {
                return -1; // y длиннее, возвращаем -1
            }
            else
            {
                return 0; // Длину одинаковая, возвращаем 0
            }
        }
    }
}
