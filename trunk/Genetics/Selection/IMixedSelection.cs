using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    /// <summary>
    /// Interfaz IMixedSelection
    /// </summary>
    /// <remarks>
    /// Define un metodo de seleccion de individuos que selecciona
    /// cierta cantidad con un metodo, y otra cantidad con otro metodo.
    /// </remarks> 
    public interface IMixedSelection : ISelection
    {
        /// <summary>
        /// Realiza una seleccion sobre la poblacion
        /// </summary>
        /// <param name="Individuals">
        /// Lista de Cromosomas entre los cuales realizar la seleccion
        /// </param>
        /// <param name="firstSize">
        /// Cantidad de cromosomas que se deben seleccionar en primer metodo
        /// </param>
        /// <param name="secondSize">
        /// Cantidad de cromosomas que se deben seleccionar en segundo metodo
        /// </param>
        void Select(IList<IIndividual> Individuals, int firstSize, int secondSize);
    }
}
