using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genetics;

namespace Optimization
{
    /// <summary>
    /// Cromosoma binario
    /// </summary>
    public class BinaryIndividual : IIndividual
    {
        protected int length = 9;			
        protected ulong val = 0;		 
        protected double fitness = 0;	

        protected static Random rand = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Longitud maxima de cromosoma
        /// </summary>
        public const int MaxLength = 9;

        /// <summary>
        /// Longitud de cromosoma
        /// </summary>
        public int Length
        {
            get { return length; }
        }

        /// <summary>
        /// Valor del cromosoma
        /// </summary>
        public ulong Value
        {
            get { return val & (0xFFFFFFFFFFFFFFFF >> (64 - length)); }
        }

        /// <summary>
        /// maximo valor del cromosoma
        /// </summary>
        public ulong MaxValue
        {
            get { return 0xFFFFFFFFFFFFFFFF >> (64 - length); }
        }

        /// <summary>
        /// Valor de aptitud del cromosoma
        /// </summary>
        public double Fitness
        {
            get { return fitness; }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public BinaryIndividual(int length)
        {
            this.length = Math.Max(2, Math.Min(MaxLength, length));
            Generate();
        }

        /// <summary>
        /// Constructor de copia
        /// </summary>
        protected BinaryIndividual(BinaryIndividual source)
        {
            length = source.length;
            val = source.val;
            fitness = source.fitness;
        }

        /// <summary>
        /// Representacion string del cromosoma
        /// </summary>
        public override string ToString()
        {
            ulong tval = val;
            char[] chars = new char[length];

            for (int i = length - 1; i >= 0; i--)
            {
                chars[i] = (char)((tval & 1) + '0');
                tval >>= 1;
            }

            return new string(chars);
        }

        /// <summary>
        /// Comparacion de cromosomas
        /// </summary>
        public int CompareTo(object o)
        {
            double f = ((BinaryIndividual)o).fitness;

            return (fitness == f) ? 0 : (fitness < f) ? 1 : -1;
        }

        /// <summary>
        /// Genera cromosoma random
        /// </summary>
        public virtual void Generate()
        {
            byte[] bytes = new byte[8];

            // generate value
            rand.NextBytes(bytes);
            val = BitConverter.ToUInt64(bytes, 0);
        }

        /// <summary>
        /// Crea nuevo cromosoma random con mismos parametros
        /// </summary>
        public virtual IIndividual CreateRandomIndividual()
        {
            return new BinaryIndividual(length);
        }

        /// <summary>
        /// Clona el cromosoma
        /// </summary>
        public virtual IIndividual Clone()
        {
            return new BinaryIndividual(this);
        }

        /// <summary>
        /// Operacion de mutacion
        /// </summary>
        public virtual void Mutate()
        {
            val ^= ((ulong)1 << rand.Next(length));
        }

        /// <summary>
        /// Operacion de reproduccion
        /// </summary>
        public virtual void Crossover(IIndividual pair)
        {
            BinaryIndividual p = (BinaryIndividual)pair;

            if ((p != null) && (p.length == length))
            {
                int crossOverPoint = 63 - rand.Next(length - 1);
                ulong mask1 = 0xFFFFFFFFFFFFFFFF >> crossOverPoint;
                ulong mask2 = ~mask1;

                ulong v1 = val;
                ulong v2 = p.val;

                val = (v1 & mask1) | (v2 & mask2);
                p.val = (v2 & mask1) | (v1 & mask2);
            }
        }

        /// <summary>
        /// Evaluar el cromosoma con una determinada funcion de aptitud
        /// </summary>
        public void Evaluate(IFitnessFunction function)
        {
            fitness = function.Evaluate(this);
        }
    }
}
