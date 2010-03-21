using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using Genetics;

namespace Optimization
{
    /// <summary>
    /// Implementacion de IFitnessFunction para problemas de optimizacion
    /// </summary>
    public class OptimizationFunction : IFitnessFunction
    {
        public Expression<Func<double, double>> Function { get; set; }

        /// <summary>
        /// Modos de Optimizacion
        /// </summary>
        public enum OptimizationModes
        {
            /// <summary>
            /// Maximo de Funcion
            /// </summary>
            Maximization,
            /// <summary>
            /// Minimo de Funcion
            /// </summary>
            Minimization
        }

        // rango de optimizacion default
        private DoubleRange range = new DoubleRange(0, 1);
        // modo de optimizacion default
        private OptimizationModes mode = OptimizationModes.Maximization;

        /// <summary>
        /// Rango de Optimizacion
        /// </summary>
        public DoubleRange Range
        {
            get { return range; }
            set { range = value; }
        }

        /// <summary>
        /// Modo de Optimizacion
        public OptimizationModes Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public OptimizationFunction(DoubleRange range)
        {
            this.range = range;
            //TODO: esto va como parametro en realidad
            this.Function = (x => 0.1 + Math.Sin(x * Math.PI * 5)*0.1 + x*0.2);
        }

        /// <summary>
        /// Evalua un cromosoma
        /// </summary>
        public double Evaluate(IChromosome chromosome)
        {
            double functionValue = Function.Compile().Invoke((double)Translate((chromosome as BinaryChromosome)));
            return (mode == OptimizationModes.Maximization) ? functionValue : 1 / functionValue;
        }

        /// <summary>
        /// Traduce genotipo a fenotipo - por ahora, esto va en IChromosome
        /// </summary>
        public object Translate(IChromosome chromosome)
        {
            return TranslateNative(chromosome);
        }

        /// <summary>
        /// Traduce genotipo a fenotipo 
        /// </summary>
        public double TranslateNative(IChromosome chromosome)
        {
            double val = ((BinaryChromosome)chromosome).Value;
            double max = ((BinaryChromosome)chromosome).MaxValue;
            return val * range.Length / max + range.Min;
        }
    }
}
