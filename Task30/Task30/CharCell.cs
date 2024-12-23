using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrientationLib;
namespace CharCellLib
{

    public class CharCell
    {
        private char value;
        private int variableCoord;
        public const int CELL_SIZE = 30;

        public CharCell()
        {
            this.value = ' ';
            this.variableCoord = 0;
        }

        public CharCell(CharCell cell)
        {
            this.value = cell.Value();
            this.variableCoord = cell.Coord();
        }

        public CharCell(char value, int variableCoord)
        {
            this.value = value;
            this.variableCoord = variableCoord;
        }

        public void ShowCharCell(Graphics g, Font font, int orient, int constCoord)
        {
            int coordX;
            int coordY;

            if (orient == Orientation.HORIZ)
            {
                // Используем variableCoord для X, constCoord для Y
                coordX = variableCoord * CELL_SIZE;
                coordY = constCoord * CELL_SIZE;
            }
            else
            {
                // Используем constCoord для X, variableCoord для Y
                coordX = constCoord * CELL_SIZE;
                coordY = variableCoord * CELL_SIZE;
            }

            string s = value.ToString();
            SizeF bounds = g.MeasureString(s, font);
            float x = coordX + (CELL_SIZE - bounds.Width) / 2; // Центрируем по X
            float y = coordY + (CELL_SIZE - bounds.Height) / 2; // Центрируем по Y

            // Рисуем прямоугольник и текст
            g.DrawRectangle(Pens.Black, coordX, coordY, CELL_SIZE, CELL_SIZE);
            g.DrawString(s, font, Brushes.Black, x, y);
        }  

        public void SetValue(char value) { this.value = value; }
        public char Value() { return value; }

        public void SetCoord(int x) { variableCoord = x; }
        public int Coord() { return variableCoord; }
    }

}
