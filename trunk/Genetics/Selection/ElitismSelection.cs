using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    /// <summary>
    /// Metodo de seleccion de elitismo
    /// </summary>
    public class ElitismSelection : ISelection
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ElitismSelection() { }

        /// <summary>
        /// Seleccionar un subconjunto de la poblacion, los <size> mejores
        /// </summary>
        public void Select(IList<IIndividual> Individuals, int size)
        {
            IList<IIndividual> newIndividuals = new List<IIndividual>();

            newIndividuals = Individuals.OrderByDescending(
                    i => i.Fitness
                ).Take(size).ToList();
            Individuals.Clear();

            (newIndividuals as List<IIndividual>).ForEach(
                    i => Individuals.Add(i)
                );
        }
    }
}
