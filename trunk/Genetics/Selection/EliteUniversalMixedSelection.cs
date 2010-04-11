using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    public class EliteUniversalMixedSelection : IMixedSelection
    {
        public void Select(IList<IIndividual> Individuals, int firstSize, int secondSize)
        {
            ElitismSelection eliteSelection = new ElitismSelection();
            UniversalSelection universalSelection = new UniversalSelection();

            IList<IIndividual> eliteIndividuals = new List<IIndividual>();
            IList<IIndividual> universalIndividuals = new List<IIndividual>();

            (Individuals as List<IIndividual>).ForEach(
                    i =>
                    {
                        eliteIndividuals.Add(i.Clone());
                        universalIndividuals.Add(i.Clone());
                    }
                );

            eliteSelection.Select(eliteIndividuals, firstSize);
            universalSelection.Select(universalIndividuals, secondSize);

            Individuals.Clear();

            (eliteIndividuals as List<IIndividual>).ForEach(
                    i => Individuals.Add(i)
                );
            (universalIndividuals as List<IIndividual>).ForEach(
                    i => Individuals.Add(i)
                );
        }

        public void Select(IList<IIndividual> Individuals, int size)
        {
            throw new NotImplementedException();
        }
    }
}
