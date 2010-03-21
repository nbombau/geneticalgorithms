using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    /// <summary>
    /// Poblacion
    /// </summary>
    public class Population
    {
        #region Variables de Instancia Privadas

        private IFitnessFunction FitnessFunction { get; set; }
        private ISelection Selection { get; set; }
        
        private IList<IChromosome> chromosomes;
        private IList<IChromosome> Chromosomes
        {
            get
            {
                if (chromosomes == default(IList<IChromosome>))
                {
                    chromosomes = new List<IChromosome>();
                }
                return chromosomes;
            }
            set
            {
                chromosomes = value;
            }

        }

        private int iterations;
        private int populationSize;
        private double randomSelectionPortion = 0.0;

        // Parametros de la poblacion
        private double CrossOverRate
        {
            get { return 0.6; }
        }

        private double MutationRate 
        { 
            get { return 0.1; } 
        }

        private double fitnessMax = 0;
        private double fitnessSum = 0;
        private double fitnessAvg = 0;
        private IChromosome bestChromosome = default(IChromosome);
        private IChromosome bestSolution = default(IChromosome);

        #endregion

        #region Variables de Clase

        // Generador de numeros pseudo-aleatorios
        private static Random rand = new Random((int)DateTime.Now.Ticks);

        #endregion

        #region Propiedades

        /// <summary>
        /// Maxima aptitud de la poblacion
        /// </summary>
        public double FitnessMax
        {
            get { return fitnessMax; }
        }

        /// <summary>
        /// Suma de las aptitudes de la poblacion
        /// </summary>
        public double FitnessSum
        {
            get { return fitnessSum; }
        }

        /// <summary>
        /// Promedio de las aptitudes de la poblacion
        /// </summary>
        public double FitnessAvg
        {
            get { return fitnessAvg; }
        }

        /// <sumary>
        /// Cromosoma mas apto de la poblacion
        /// </sumary>
        public IChromosome BestChromosome
        {
            get { return bestChromosome; }
        }

        /// <sumary>
        /// Solucion
        /// </sumary>
        public IChromosome BestSolution
        {
            get { return bestSolution; }
        }

        /// <sumary>
        /// Tamanio de la poblacion TODO: ver de usar solo la lista y su count
        /// </sumary>
        public int PopulationSize
        {
            get { return populationSize; }
            private set { populationSize = value; }
        }

        #endregion

        #region Eventos

        public delegate void GenerationEndedEventHandler(object sender, GenerationEndedEventArgs e);
        public event GenerationEndedEventHandler GenerationEnded;

        protected void OnGenerationEnded(GenerationEndedEventArgs e)
        {
            GenerationEnded(this, e);
        }

        #endregion Eventos

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        public Population(int size,
                            IChromosome ancestor,
                            IFitnessFunction fitnessFunction,
                            ISelection selectionMethod,
                            int numberIterations)
        {
            FitnessFunction = fitnessFunction;
            Selection = selectionMethod;
            PopulationSize = size;
            iterations = numberIterations;
            // Agregar el ancestro a la poblacion
            ancestor.Evaluate(fitnessFunction);
            Chromosomes.Add(ancestor);
            // Se agregan mas cromosomas a la poblacion
            for (int i = 1; i < size; i++)
            {
                // Se crea un nuevo cromosoma al azar
                IChromosome c = ancestor.CreateRandomChromosome();
                // se calcula su aptitud
                c.Evaluate(fitnessFunction);
                // Se lo agrega a la poblacion
                Chromosomes.Add(c);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Population(int size,
            IChromosome ancestor,
            IFitnessFunction fitnessFunction,
            ISelection selectionMethod,
            double randomSelectionPortion,
            int iterations)
            : this(size, ancestor, fitnessFunction, selectionMethod, iterations)
        {
            randomSelectionPortion = Math.Max(0, Math.Min(0.5, randomSelectionPortion));
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Se regenera la poblacion llenandola de cromosomas al azar
        /// </summary>
        public void Regenerate()
        {
            IChromosome ancestor = Chromosomes.ElementAt(0) as IChromosome;

            // Limpiar la poblacion
            Chromosomes.Clear();
            // Agregar cromosomas a la misma
            for (int i = 0; i < PopulationSize; i++)
            {
                IChromosome c = ancestor.CreateRandomChromosome();
                c.Evaluate(FitnessFunction);
                Chromosomes.Add(c);
            }
        }

        /// <summary>
        /// Reproduccion sobre la poblacion
        /// </summary>
        public virtual void Crossover()
        {
            // Reproduccion
            for (int i = 1; i < PopulationSize; i += 2)
            {
                // Se genera un numero al azar para decidir si realizar la reproduccion
                if (rand.NextDouble() <= CrossOverRate)
                {
                    // Se crean clones de los padres
                    IChromosome c1 = Chromosomes.ElementAt(i - 1).Clone() as IChromosome;
                    IChromosome c2 = Chromosomes.ElementAt(i).Clone() as IChromosome;

                    // Cruzarlos
                    c1.Crossover(c2);

                    // Evaluar la aptitud de los cromosomas
                    c1.Evaluate(FitnessFunction);
                    c2.Evaluate(FitnessFunction);

                    // Agregarlos a la poblacion
                    Chromosomes.Add(c1);
                    Chromosomes.Add(c2);
                }
            }
        }

        /// <summary>
        /// Mutacion
        /// </summary>
        public virtual void Mutate()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                // se genera un numero aleatorio para decidir si mutar o no
                if (rand.NextDouble() <= MutationRate)
                {
                    // se clona el cromosoma
                    IChromosome c = Chromosomes.ElementAt(i).Clone() as IChromosome;
                    // se muta el cromosoma
                    c.Mutate();
                    // se calcula la aptitud del mutante
                    c.Evaluate(FitnessFunction);
                    // se lo agrega a la poblacion
                    Chromosomes.Add(c);
                }
            }
        }

        /// <summary>
        /// Seleccion
        /// </summary>
        public virtual void Select()
        {
            // Cantidad de cromosomas al azar en la nueva poblacion
            int randomAmount = (int)(randomSelectionPortion * PopulationSize);

            // Realizar la seleccion
            Selection.Select(Chromosomes, PopulationSize - randomAmount);

            // Agregar cromosomas random
            if (randomAmount > 0)
            {
                IChromosome ancestor = Chromosomes.ElementAt(0) as IChromosome;

                for (int i = 0; i < randomAmount; i++)
                {
                    IChromosome c = ancestor.CreateRandomChromosome();
                    c.Evaluate(FitnessFunction);
                    Chromosomes.Add(c);
                }
            }

            // Encontrar el mejor
            fitnessMax = 0;
            fitnessSum = 0;

            foreach (IChromosome c in Chromosomes)
            {
                double fitness = c.Fitness;

                // Acumulamos el valor
                fitnessSum += fitness;

                // buscamos el mayor
                if (fitness > fitnessMax)
                {
                    fitnessMax = fitness;
                    bestChromosome = c;
                    //me fijo si es mejor que la solucion global
                    if (bestSolution == default(IChromosome) || c.Fitness > bestSolution.Fitness)
                    {
                        bestSolution = c;
                    }
                }
            }
            fitnessAvg = fitnessSum / PopulationSize;
        }

        /// <summary>
        /// Corre una epoca de la poblacion - Reproduccion, mutacion y seleccion
        /// </summary>
        public void RunEpoch()
        {
            Crossover();
            Mutate();
            Select();
        }

        public void RunSimulation()
        { 
            int i;
            for (i = 0; i < iterations; i++)
            {
                RunEpoch();
                OnGenerationEnded(new GenerationEndedEventArgs(this, i + 1));
            }
        }

        /// <sumary>
        /// Obtener el Cromosoma en un indice determinado
        /// </sumary>
        public IChromosome ElementAt(int index)
        {
            return Chromosomes.ElementAt(index) as IChromosome;
        }


        public void Trace()
        {
            System.Diagnostics.Debug.WriteLine("Max = " + fitnessMax);
            System.Diagnostics.Debug.WriteLine("Sum = " + fitnessSum);
            System.Diagnostics.Debug.WriteLine("Avg = " + fitnessAvg);
            System.Diagnostics.Debug.WriteLine("--------------------------");
            foreach (IChromosome c in Chromosomes)
            {
                System.Diagnostics.Debug.WriteLine("genotype = " + c.ToString() +
                    //", phenotype = " +  +
                    " , fitness = " + c.Fitness);
            }
            System.Diagnostics.Debug.WriteLine("==========================");
        }

        #endregion Metodos
    }
}
