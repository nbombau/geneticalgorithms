using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    /// <summary>
    /// Interfaz ISelection
    /// </summary>
    /// <remarks>
    /// Define un metodo de seleccion de cromosomas
    /// </remarks>
    public interface ISelection
    {
        /// <summary>
        /// Realiza una seleccion sobre la poblacion
        /// </summary>
        /// <param name="chromosomes">
        /// Lista de Cromosomas entre los cuales realizar la seleccion
        /// </param>
        /// <param name="size">
        /// Cantidad de cromosomas que se deben seleccionar
        /// </param>
        void Select(IList<IChromosome> chromosomes, int size);
    }
}
