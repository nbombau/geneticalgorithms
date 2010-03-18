using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Optimization
{

    public class DoubleRange
    {
        private double min, max;

        public double Min
        {
            get { return min; }
            set { min = value; }
        }

        public double Max
        {
            get { return max; }
            set { max = value; }
        }

        public double Length
        {
            get { return max - min; }
        }

        public DoubleRange(double min, double max)
        {
            this.min = min;
            this.max = max;
        }
    }

    public class IntRange
    {
        private int min, max;

        public int Min
        {
            get { return min; }
            set { min = value; }
        }

        public int Max
        {
            get { return max; }
            set { max = value; }
        }

        public int Length
        {
            get { return max - min; }
        }

        public IntRange(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
