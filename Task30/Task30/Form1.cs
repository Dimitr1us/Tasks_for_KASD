using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordAreaLib;
using ArrayWordLib;
using WordLib;
using OrientationLib;
namespace Task30
{
    public partial class Form1 : Form
    {
        ArrayWord<Word> solution;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
              WordArea wordArea = new WordArea();
            solution = wordArea.Solution();
            this.Text = "Criss-Cross";
            this.Width = (solution.Width()+2) * 30;
            this.Height = (solution.Height() + 2) * 30;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            
            Graphics g2D = e.Graphics;
            Font font = new Font("TimesNewRoman", 14);

            int maxX=0;
            int maxY=0;

            foreach (Word word in solution)
            {
                if (word.Orient() == OrientationLib.Orientation.HORIZ)
                {
                     if (word.constCoordination()<maxY) maxY = word.constCoordination();
                     if (word.First()<maxX) maxX = word.First();
                }
                if (word.Orient() == OrientationLib.Orientation.VERTIC)
                {
                    if (word.constCoordination() < maxX) maxX = word.constCoordination();
                    if (word.First() < maxY) maxY = word.First();
                }
            }

            foreach (Word word in solution) {
                word.IncreaseCoordinate(maxX, maxY);
                word.ShowWord(g2D,font);
            }
        }
    }


}
