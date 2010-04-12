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
        private ISelection Replacement { get; set; }
        private List<IIndividual> individuals;
        private List<IIndividual> Individuals
        {
            get
            {
                if (individuals == default(List<IIndividual>))
                {
                    individuals = new List<IIndividual>();
                }
                return individuals;
            }
            set
            {
                Individuals = value;
            }

        }

        private int iterations;
        private int populationSize;
        private double randomSelectionPortion = 0.0;
        private int firstSelectionCount;
        private int secondSelectionCount;
        private int firstReplacementCount;
        private int secondReplacementCount;
        // Parametros de la poblacion
        private double CrossOverRate
        {
            get { return 0.6; }
        }

        private double MutationRate 
        {
            get { return mutationRate; }
            set { mutationRate = value; }
        }

        private double fitnessMax = 0;
        private double fitnessSum = 0;
        private double fitnessAvg = 0;
        private double mutationRate = 0.005;
        private IIndividual bestIndividual = default(IIndividual);
        private IIndividual bestSolution = default(IIndividual);

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
        public IIndividual BestIndividual
        {
            get { return bestIndividual; }
        }

        /// <sumary>
        /// Solucion
        /// </sumary>
        public IIndividual BestSolution
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

        public void OnGenerationEnded(GenerationEndedEventArgs e)
        {
            GenerationEnded(this, e);
        }

        #endregion Eventos

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        public Population(int size,
                            IIndividual ancestor,
                            IFitnessFunction fitnessFunction,
                            ISelection selectionMethod,
                            int numberIterations)
        {
            FitnessFunction = fitnessFunction;
            Selection = Replacement = selectionMethod;
            PopulationSize = size;
            firstSelectionCount = size;
            firstReplacementCount = ((int)(size / 2)) % 2 == 0 ? (int)(size / 2) : (int)(size / 2) + 1;
            iterations = numberIterations;
            // Agregar el ancestro a la poblacion
            ancestor.Evaluate(fitnessFunction);
            Individuals.Add(ancestor);
            // Se agregan mas cromosomas a la poblacion
            for (int i = 1; i < size; i++)
            {
                // Se crea un nuevo cromosoma al azar
                IIndividual c = ancestor.CreateRandomIndividual();
                // se calcula su aptitud
                c.Evaluate(fitnessFunction);
                // Se lo agrega a la poblacion
                Individuals.Add(c);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Population(int size,
            IIndividual ancestor,
            IFitnessFunction fitnessFunction,
            ISelection selectionMethod,
            double randomSelectionPortion,
            int iterations)
            : this(size, ancestor, fitnessFunction, selectionMethod, iterations)
        {
            randomSelectionPortion = Math.Max(0, Math.Min(0.5, randomSelectionPortion));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Population(int size,
            IIndividual ancestor,
            IFitnessFunction fitnessFunction,
            ISelection selectionMethod,
            int iterations,
            double mutationRate)
            : this(size, ancestor, fitnessFunction, selectionMethod, iterations)
        {
            MutationRate = mutationRate;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Population(int size,
                            IIndividual ancestor,
                            IFitnessFunction fitnessFunction,
                            ISelection selectionMethod,
                            ISelection replacementMethod,
                            int numberIterations,
                            double mutationRate,
                            int selectionCount,
                            int replacementCount)
        {
            //firstSelectionCount = ((int)(selectionCount )) % 2 == 0 ? (int)(selectionCount ) : (int)(selectionCount ) + 1;
            //firstReplacementCount = ((int)(replacementCount)) % 2 == 0 ? (int)(replacementCount) : (int)(replacementCount) + 1;
            firstSelectionCount = selectionCount;
            firstReplacementCount = replacementCount;
            FitnessFunction = fitnessFunction;
            Selection = selectionMethod;
            Replacement = replacementMethod ;
            PopulationSize = size;
            iterations = numberIterations;
            MutationRate = mutationRate;
            // Agregar el ancestro a la poblacion
            ancestor.Evaluate(fitnessFunction);
            Individuals.Add(ancestor);
            // Se agregan mas cromosomas a la poblacion
            for (int i = 1; i < size; i++)
            {
                // Se crea un nuevo cromosoma al azar
                IIndividual c = ancestor.CreateRandomIndividual();
                // se calcula su aptitud
                c.Evaluate(fitnessFunction);
                // Se lo agrega a la poblacion
                Individuals.Add(c);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Population(int size,
                            IIndividual ancestor,
                            IFitnessFunction fitnessFunction,
                            ISelection selectionMethod,
                            ISelection replacementMethod,
                            int numberIterations,
                            double mutationRate,
                            int selectionCount,
                            int replacementCount,
                            int secondSelCount,
                            int secondReplCount)
            : this(size, ancestor, fitnessFunction, selectionMethod, replacementMethod, numberIterations,
                        mutationRate, selectionCount, replacementCount
        )
        {
            //secondSelectionCount = ((int)(secondSelCount)) % 2 == 0 ? (int)(secondSelCount) : (int)(secondSelCount) + 1;
            //secondReplacementCount = ((int)(secondReplCount)) % 2 == 0 ? (int)(secondReplCount) : (int)(secondReplCount) + 1;
            secondReplacementCount = secondReplCount;
            secondSelectionCount = secondSelCount;
            if (firstReplacementCount + secondReplCount != size) 
            {
                throw new ApplicationException("La suma de ambos reemplazos debe ser igual al tamaño de la poblacion.");
            }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Se regenera la poblacion llenandola de cromosomas al azar
        /// </summary>
        public void Regenerate()
        {
            IIndividual ancestor = Individuals.ElementAt(0) as IIndividual;

            // Limpiar la poblacion
            Individuals.Clear();
            // Agregar cromosomas a la misma
            for (int i = 0; i < PopulationSize; i++)
            {
                IIndividual c = ancestor.CreateRandomIndividual();
                c.Evaluate(FitnessFunction);
                Individuals.Add(c);
            }
        }

        /// <summary>
        /// Reproduccion sobre la poblacion
        /// </summary>
        public virtual void Crossover()
        {
            List<IIndividual> individualsToCrossover = new List<IIndividual>();
            // Creo una copia de los individuos
            Individuals.ForEach(i => individualsToCrossover.Add(i.Clone()));
            // Realizar la seleccion
            if (Selection is IMixedSelection)
            {
                (Selection as IMixedSelection)
                    .Select(individualsToCrossover, firstSelectionCount, secondSelectionCount == default(int) ? 0 : secondReplacementCount);
            }
            else
            {
                Selection.Select(individualsToCrossover, firstSelectionCount);
            }    

            individualsToCrossover = individualsToCrossover.Except(Individuals).ToList();

            // Genero pares en base a los individuos seleccionados
            var pairs = individualsToCrossover.Where(
                    (i, index) => index % 2 == 0 && individualsToCrossover.ElementAt(index + 1 ) != default(IIndividual)
                ).Select(
                    (j, jIndex) => new { parent1 = j, parent2 = individualsToCrossover.ElementAt(jIndex + 1)}
            );

            // Cruzo cada par, y lo agrego a la poblacion
            pairs.ToList().ForEach(pair =>
                {
                    pair.parent1.Crossover(pair.parent2);
                    // evaluar los individuis del par
                    pair.parent1.Evaluate(FitnessFunction);
                    pair.parent2.Evaluate(FitnessFunction);
                    // agregarlos a la poblacion
                    Individuals.Add(pair.parent2);
                    Individuals.Add(pair.parent1);
                }
            );
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
                    IIndividual c = Individuals.ElementAt(i).Clone() as IIndividual;
                    // se muta el cromosoma
                    c.Mutate();
                    // se calcula la aptitud del mutante
                    c.Evaluate(FitnessFunction);
                    // se lo agrega a la poblacion
                    Individuals.Add(c);
                }
            }
        }

        /// <summary>
        /// Seleccion
        /// </summary>
        public virtual void Select()
        {
            // Realizar el reemplazo
            if (Replacement is IMixedSelection)
            {
                (Replacement as IMixedSelection)
                    .Select(Individuals, firstReplacementCount, secondReplacementCount == default(int) ? 0 : secondReplacementCount);
            }
            else 
            {
                Replacement.Select(Individuals, PopulationSize);
            }        

            // Encontrar el mejor
            fitnessMax = 0;
            fitnessSum = 0;

            foreach (IIndividual c in Individuals)
            { 
                double fitness = c.Fitness;

                // Acumulamos el valor
                fitnessSum += fitness;

                // buscamos el mayor
                if (fitness > fitnessMax)
                {
                    fitnessMax = fitness;
                    bestIndividual = c;
                    //me fijo si es mejor que la solucion global

                }
                if (bestSolution == default(IIndividual) || c.Fitness > bestSolution.Fitness)
                {
                    bestSolution = c.Clone();
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
        public IIndividual ElementAt(int index)
        {
            return Individuals.ElementAt(index) as IIndividual;
        }

        #endregion Metodos
    }
}
