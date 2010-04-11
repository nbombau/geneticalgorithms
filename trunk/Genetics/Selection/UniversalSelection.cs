using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    /// <summary>
    /// Metodo de seleccion universal
    /// </summary>
    public class UniversalSelection : ISelection
    {
        private static Random rand = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Constructor
        /// </summary>
        public UniversalSelection() { }

        /// <summary>
        /// Seleccionar un subconjunto de la poblacion, los <size> mejores
        /// </summary>
        public void Select(IList<IIndividual> Individuals, int size)
        {
            double r = rand.NextDouble();
            double num = 0.0;
            IList<IIndividual> newIndividuals = new List<IIndividual>();

            int currentSize = Individuals.Count;

            double fitnessSum = 0;

            (Individuals as List<IIndividual>).ForEach(
                    i => fitnessSum += i.Fitness
                );

            double[] rangeMax = new double[currentSize];
            double s = 0;
            int k = 0;

            (Individuals as List<IIndividual>).ForEach(
                    i => {
                        s += (i.Fitness / fitnessSum);
                        rangeMax[k++] = s;
                    }
                );


            // seleccionar cromosomas de la vieja poblacion a la nueva
            for (int j = 0; j < size; j++)
            {
                // generamos el numero
                num = (r + j - 1) / size;
                for (int i = 0; i < currentSize; i++)
                {
                    if (num <= rangeMax[i])
                    {
                        // agregar a poblacion
                        newIndividuals.Add(((IIndividual)Individuals[i]).Clone());
                        break;
                    }
                }
            }

            // vaciar la poblacion anterior
            Individuals.Clear();

            (newIndividuals as List<IIndividual>).ForEach(
                    ind => Individuals.Add(ind)
                );
        }
    }
}
