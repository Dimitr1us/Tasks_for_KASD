using CharCellLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrientationLib;
namespace WordLib
{
    internal class Word
    {
        private CharCell [] cells;
        private int orientation;
        private int constWordCoord;
        private int length;

        private string word;

        public Word(string word, int orientation, int constWordCoord, int initialVariableCoord)
        {
            this.orientation = orientation;
            this.constWordCoord = constWordCoord;
            this.length = word.Length;
            cells = new CharCell[length];

            for (int i = 0; i < length; i++)
            {
                cells[i] = new CharCell(word[i], initialVariableCoord + i);
            }
            this.word=word;
        }

        public Word(Word word)
        {
            this.orientation = word.Orient();
            this.constWordCoord = word.constCoordination();
            this.length = word.Length();
            cells = new CharCell[length];
            this.word = word.GetWord();
            for (int i = 0; i < length; i++)
            {
                cells[i] = new CharCell(word.Get(i));
            }
        }

        public void ShowWord(Graphics g, Font font)
        {
            for (int i = 0; i < length; i++)
            {
                cells[i].ShowCharCell(g, font, orientation, constWordCoord);
            }
        } 

        public void IncreaseCoordinate(int minCoordX, int minCoordY)
        {
            if (orientation == Orientation.HORIZ)
            {
                constWordCoord -= minCoordY;
                for (int i = 0; i < length; i++)
                    cells[i].SetCoord(cells[i].Coord() - minCoordX);
            }
            else
            {
                constWordCoord -= minCoordX;
                for (int i = 0; i < length; i++)
                    cells[i].SetCoord(cells[i].Coord() - minCoordY);
            }
        }

        public CharCell Get(int i)
        {
            if (i >= 0 && i < length) return cells[i];
            else return new CharCell();
        }

        public int Orient() { return orientation; }
        public int constCoordination() { return constWordCoord; }
        public int Length() { return length; }
        public int First() { return cells[0].Coord(); }
        public int Last() { return cells[length-1].Coord(); }

        public string GetWord() { return this.word; }
    }
}
