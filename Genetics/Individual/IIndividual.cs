using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    /// <summary>
    /// Interface IIndividual
    /// </summary>
    public interface IIndividual : IComparable
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
        IIndividual CreateRandomIndividual();

        /// <summary>
        /// Crea un clon del cromosoma
        /// </summary>
        IIndividual Clone();

        /// <summary>
        /// Operador de mutacion
        /// </summary>
        void Mutate();

        /// <summary>
        /// Operador de reproduccion
        /// </summary>
        void Crossover(IIndividual Individual);

        /// <summary>
        /// Evalua la aptitud del cromosoma para la funcion
        /// de aptitud dada
        /// </summary>
        void Evaluate(IFitnessFunction function);
    }
}
