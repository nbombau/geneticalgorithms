using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genetics;

namespace Optimization
{
    /// <summary>
    /// Metodo de seleccion de ruleta
    /// </summary>
    public class WheelSelection : ISelection
    {
        private static Random rand = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Constructor
        /// </summary>
        public WheelSelection() { }

        /// <summary>
        /// Seleccionar un subconjunto de la poblacion
        /// </summary>
        public void Select(IList<IChromosome> chromosomes, int size)
        {
            IList<IChromosome> newPopulation = new List<IChromosome>();
         
            int currentSize = chromosomes.Count;

            double fitnessSum = 0;
            foreach (IChromosome c in chromosomes)
            {
                fitnessSum += c.Fitness;
            }

            double[] rangeMax = new double[currentSize];
            double s = 0;
            int k = 0;

            foreach (IChromosome c in chromosomes)
            {
                s += (c.Fitness / fitnessSum);
                rangeMax[k++] = s;
            }

            // seleccionar cromosomas de la vieja poblacion a la nueva
            for (int j = 0; j < size; j++)
            {
                // obtener el valor de la ruleta
                double wheelValue = rand.NextDouble();
                // encontrar cromosoma correspondiente
                for (int i = 0; i < currentSize; i++)
                {
                    if (wheelValue <= rangeMax[i])
                    {
                        // agregar a poblacion
                        newPopulation.Add(((IChromosome)chromosomes[i]).Clone());
                        break;
                    }
                }
            }

            // vaciar la poblacion anterior
            chromosomes.Clear();

            // move elements from new to current population
            // !!! moving is done to reduce objects cloning
            for (int i = 0; i < size; i++)
            {
                chromosomes.Add(newPopulation[0]);
                newPopulation.RemoveAt(0);
            }
        }
    }
}
