using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3_3;
using SortingLib;
namespace RandomLib
{
    public static class GenerateArrays
    {
        public static Random random = new Random();
        public static int[] GenerateGroup1(int length)
        {
            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(1000);
            }
            return array;
        }
        public static int[] GenerateGroup2(int length)
        {
            int[] finalArray = new int[length];
            int curIndex = 0;
            int modulSubArray = (int)Math.Pow(10, random.Next(1, (int)Math.Log(length, 10)) + 1);
            while (length > 0)
            {
                int curSubLenth = random.Next(1, modulSubArray);
                if (length - curSubLenth < 0) curSubLenth = length;
                int[] subArray = new int[curSubLenth];
                subArray = GenerateGroup1(curSubLenth);
                Sorting.DoQuickSort.Sort(subArray);

                for (int i = 0; i < curSubLenth; i++)
                {
                    finalArray[curIndex++] = subArray[i];
                }
                length -= curSubLenth;
            }
            return finalArray;
        }
        public static int[] GenerateGroup3(int length)
        {
            int[] finalArray = new int[length];
            finalArray = GenerateGroup1(length);
            Array.Sort(finalArray);

            int k = random.Next(1, length / 2);
            for (int m = 0; m < k; m++)
            {
                int i = random.Next(0, length);
                int j = random.Next(0, length);
                Sorting.Swap(ref finalArray, i, j);
            }
            return finalArray;
        }
        public static int[] GenerateGroup4(int length)
        {
            int[] finalArray = new int[length];
            finalArray = GenerateGroup1(length);
            int repeatNum = random.Next(1000);
            int repeating = ((random.Next(10, 91)) * length) / 100;
            for (int i = 0; i < repeating; i++) finalArray[i] = repeatNum;
            return finalArray;
        }
    }
}
