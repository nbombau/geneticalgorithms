using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolutive
{
    /// <summary>
    /// Interface de gen, que representa el contenido un nodo
    /// </summary>
    public interface ITreeGene
    {
        /// <summary>
        /// Tipo de gen
        /// </summary>
        GeneType GeneType { get; }

        /// <summary>
        /// Cantidad de argumentos
        /// </summary>
        int ArgumentsCount { get; }

        /// <summary>
        /// Maxima cantidad de argumentos
        /// </summary>
        int MaxArgumentsCount { get; }

        /// <summary>
        /// Clona el gen
        /// </summary>
        ITreeGene Clone();

        /// <summary>
        /// Gen de tipo y valor aleatorio
        /// </summary>
        void Generate();

        /// <summary>
        /// Gen de valor aleatorio
        /// </summary>
        void Generate(GeneType type);

        /// <summary>
        /// Nuevo gen de tipo y valor aleatorio
        /// </summary>
        ITreeGene CreateNew();

        /// <summary>
        /// Nuevo gen de valor aleatorio
        /// </summary>
        ITreeGene CreateNew(GeneType type);
    }

    /// <summary>
    /// Tipos de gen posibles
    /// </summary>
    public enum GeneType
    {
        Function,
        Argument
    }
}
