using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLib
{
    internal class Application
    {
        private int numberOfApplication;
        private int numberOfPriority;
        private int numberOfStep;

        public int NumberOfApplication
        {
            get { return numberOfApplication; } 
            set { numberOfApplication = value; }
        }
        public int NumberOfPriority
        {
            get { return numberOfPriority; }
            set { numberOfPriority = value; }
        }
        public int NumberOfStep
        {
            get { return numberOfStep; }
            set { numberOfStep = value; }
        }
    }
}
