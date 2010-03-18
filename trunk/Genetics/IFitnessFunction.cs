using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    /// <summary>
    /// Interfaz IFitnessFunction
    /// </summary>
    /// 
    /// <remarks> Funcion para evaluar la aptitud de un cromosoma. 
    /// Las funciones que implementen la interfaz deberan retornar 
    /// numeros mayores a cero.
    /// </remarks>
    public interface IFitnessFunction
    {
        /// <summary>
        /// Evalua la aptitud del Cromosoma
        /// </summary>
        /// <param name="chromosome">Cromosoma a evaluar</param>
        /// <returns>Valor de aptitud del cromosoma</returns>
        double Evaluate(IChromosome chromosome);
    }
}
