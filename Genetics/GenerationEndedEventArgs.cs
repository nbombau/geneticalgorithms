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

        /*private IChromosome generationBestChromosome;
        public IChromosome GenerationBestChromosome { get { return generationBestChromosome; } }

        private IChromosome bestChromosome;
        public IChromosome BestChromosome { get { return generationBestChromosome; } }*/

        public GenerationEndedEventArgs(Population p, int genNum)
        {
            population = p;
            generationNumber = genNum;
        }
    }
}
