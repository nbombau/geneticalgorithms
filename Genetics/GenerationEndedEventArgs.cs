using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    public class GenerationEndedEventArgs : EventArgs
    {
        private Population population;
        public Population Population { get { return population; } }

        private int generationNumber;
        public int GenerationNumber { get { return generationNumber; } }

        /*private IIndividual generationBestIndividual;
        public IIndividual GenerationBestIndividual { get { return generationBestIndividual; } }

        private IIndividual bestIndividual;
        public IIndividual BestIndividual { get { return generationBestIndividual; } }*/

        public GenerationEndedEventArgs(Population p, int genNum)
        {
            population = p;
            generationNumber = genNum;
        }
    }
}
