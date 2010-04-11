using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    public class EliteWheelMixedSelection : IMixedSelection
    {
        public void Select(IList<IIndividual> Individuals, int firstSize, int secondSize)
        {
            ElitismSelection eliteSelection = new ElitismSelection();
            WheelSelection wheelSelection = new WheelSelection();

            IList<IIndividual> eliteIndividuals = new List<IIndividual>();
            IList<IIndividual> wheelIndividuals = new List<IIndividual>();

            (Individuals as List<IIndividual>).ForEach(
                    i => {
                        eliteIndividuals.Add(i.Clone());
                        wheelIndividuals.Add(i.Clone());
                    }
                );

            eliteSelection.Select(eliteIndividuals, firstSize);
            wheelSelection.Select(wheelIndividuals, secondSize);

            Individuals.Clear();

            (eliteIndividuals as List<IIndividual>).ForEach(
                    i => Individuals.Add(i)
                );
            (wheelIndividuals as List<IIndividual>).ForEach(
                    i => Individuals.Add(i)
                );
        }

        public void Select(IList<IIndividual> Individuals, int size)
        {
            throw new NotImplementedException();
        }
    }
}
