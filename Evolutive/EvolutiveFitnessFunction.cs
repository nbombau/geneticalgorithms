using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genetics;

namespace Evolutive
{
    /// <summary>
    /// Funcion de aptitud para programacion evolutiva
    /// </summary>
    /// 
    /// <remarks>Calcula el valor de aptitud de un 
    /// <see cref="TreeIndividual">TreeIndividual</see> 
    /// </remarks>
    public class EvolutiveFitnessFunction : IFitnessFunction
    {
        // datos
        private bool[,] data;
        // varibles
        private bool[] variables;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Tabla de verdad</param>
        /// <remarks>
        /// El parametro representa el valor de verdad al que queremos llegar
        /// </remarks>
        public EvolutiveFitnessFunction(bool[,] data)
        {
            this.data = data;
            variables = new bool[4];
        }

        /// <summary>
        /// Evalua al individuo
        /// </summary>
        /// <param name="chromosome">
        /// Individuo a evaluar
        /// </param>
        /// <returns>
        /// Retorna el valor de aptitud del individuo
        /// </returns>
        public double Evaluate(IIndividual chromosome)
        {
            // obtener el individuo en notacion polaca
            string function = chromosome.ToString();
            double fitness = 0;
            //Se suma un error inicial proporcional a la longitud de la expresion
            //double error = ((double)function.Length)/1000;
            // para cada linea de la tabla de verdad
            for (int i = 0, n = data.GetLength(0); i < n; i++)
            {
                // cargamos el valor de los 4 bits de entrada
                variables[0] = data[i, 0];
                variables[1] = data[i, 1];
                variables[2] = data[i, 2];
                variables[3] = data[i, 3];
                try
                {
                    // evaluamos el arbol generado con estas variables
                    bool y = PolishBooleanExpression.Evaluate(function, variables);

                    // si el arbol generado difiere de la solucion 
                    //se agranda el error en 1 unidad
                    //error += (y == data[i,4]) ? 0 : 1 ;
                    fitness += (y == data[i, 4]) ? 1 : 0;
                    if (i == 15 && (y == data[i, 4]))
                        fitness += 0.25;
                }
                catch
                {
                    return 0;
                }
            }

            fitness = (fitness - 16.25 < double.Epsilon) ? Math.Pow(fitness, 4) : Math.Pow(fitness, 3);
            if (fitness > 0.0)
            {
                fitness -= ((double)function.Length); 
            }

            return fitness;
            //return data.GetLength(0) + 1/ (error + 1);
        }

        /// <summary>
        /// Traduce de genotipo a fenotiop
        /// </summary>
        public object Translate(IIndividual chromosome)
        {
            return TranslateNative(chromosome);
        }

        /// <summary>
        /// Traduce de genotipo a fenotiop
        /// </summary>
        public string TranslateNative(IIndividual chromosome)
        {
            //TODO: Retornar en notacion infija
            return chromosome.ToString();
        }
    }
}
