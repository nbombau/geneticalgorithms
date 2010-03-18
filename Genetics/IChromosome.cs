using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    /// <summary>
    /// Interface IChromosome
    /// </summary>
    public interface IChromosome : IComparable
    {
        /// <summary>
        /// Genotipo del cromosoma
        /// </summary>
        ///List<bool> Genotype { get; }

        /// <summary>
        /// Fenotipo del cromosoma
        /// </summary>
        ///ulong Phenotype { get; }

        /// <summary>
        /// Valor de aptitud de cromosoma
        /// </summary>
        double Fitness { get; }

        /// <summary>
        /// Genera un cromosoma al azar
        /// </summary>
        void Generate();

        /// <summary>
        /// Crea un nuevo cromosoma al azar con los mismos parametros de tamanio, etc.
        /// </summary>
        IChromosome CreateRandomChromosome();

        /// <summary>
        /// Crea un clon del cromosoma
        /// </summary>
        IChromosome Clone();

        /// <summary>
        /// Operador de mutacion
        /// </summary>
        void Mutate();

        /// <summary>
        /// Operador de reproduccion
        /// </summary>
        void Crossover(IChromosome chromosome);

        /// <summary>
        /// Evalua la aptitud del cromosoma para la funcion
        /// de aptitud dada
        /// </summary>
        void Evaluate(IFitnessFunction function);
    }
}
