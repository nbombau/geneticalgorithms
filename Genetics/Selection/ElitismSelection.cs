using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    public class ElitismSelection : ISelection
    {
        public ElitismSelection() { }

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
