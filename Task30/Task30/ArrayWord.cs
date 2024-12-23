using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordLib;
namespace ArrayWordLib
{
    internal class ArrayWord<Word> : List<Word>
    {
        private int minCoordX = 0;
        private int minCoordY = 0;
        private int width = 0;
        private int height = 0;
        private int intersectCount = 0;

        public ArrayWord() : base() 
        {
        }

        public ArrayWord(int initialCapacity) : base(initialCapacity)
        {
        }

        public ArrayWord(ArrayWord<Word> arrayWord) : base(arrayWord.Count)
        {
            minCoordX = arrayWord.MinX();
            minCoordY = arrayWord.MinY();
            width = arrayWord.Width();
            height = arrayWord.Height();
            intersectCount = arrayWord.IntersectCount();

            
           foreach (Word word in arrayWord)
                {
                    Add(word);
                }
        }

        public void SetInterCount(int c)
        {
            intersectCount = c;
        }

        public void SetMinX(int x)
        {
            if (x < minCoordX) minCoordX = x;
        }

        public void SetMinY(int y)
        {
            if (y < minCoordY) minCoordY = y;
        }

        public void SetWidth(int w)
        {
            width = w;
        }

        public void SetHeight(int h)
        {
            height = h;
        }

        public int MinX() { return minCoordX; }
        public int MinY() { return minCoordY; }
        public int Width() { return width; }
        public int Height() { return height; }
        public int IntersectCount() { return intersectCount; }
        public void Reset() { minCoordX = 0; minCoordY = 0; }

        

    }
}
