using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
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
        public void Select(IList<IIndividual> Individuals, int size)
        {
            IList<IIndividual> newPopulation = new List<IIndividual>();
         
            int currentSize = Individuals.Count;

            double fitnessSum = 0;
            foreach (IIndividual c in Individuals)
            {
                fitnessSum += c.Fitness;
            }

            double[] rangeMax = new double[currentSize];
            double s = 0;
            int k = 0;

            foreach (IIndividual c in Individuals)
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
                        newPopulation.Add(((IIndividual)Individuals[i]).Clone());
                        break;
                    }
                }
            }

            // vaciar la poblacion anterior
            Individuals.Clear();

            // move elements from new to current population
            // !!! moving is done to reduce objects cloning
            for (int i = 0; i < size; i++)
            {
                Individuals.Add(newPopulation[0]);
                newPopulation.RemoveAt(0);
            }
        }
    }
}
