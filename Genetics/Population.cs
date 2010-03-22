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
                            IIndividual ancestor,
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
            // Selecciono con el metodo de seleccion provisto n/2 individuos
            Selection.Select(individualsToCrossover, (int) PopulationSize / 2);
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
            // Cantidad de cromosomas al azar en la nueva poblacion
            int randomAmount = (int)(randomSelectionPortion * PopulationSize);

            // Realizar la seleccion
            Selection.Select(Individuals, PopulationSize - randomAmount);

            // Agregar cromosomas random
            if (randomAmount > 0)
            {
                IIndividual ancestor = Individuals.ElementAt(0) as IIndividual;

                for (int i = 0; i < randomAmount; i++)
                {
                    IIndividual c = ancestor.CreateRandomIndividual();
                    c.Evaluate(FitnessFunction);
                    Individuals.Add(c);
                }
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
                    if (bestSolution == default(IIndividual) || c.Fitness > bestSolution.Fitness)
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
        public IIndividual ElementAt(int index)
        {
            return Individuals.ElementAt(index) as IIndividual;
        }


        public void Trace()
        {
            System.Diagnostics.Debug.WriteLine("Max = " + fitnessMax);
            System.Diagnostics.Debug.WriteLine("Sum = " + fitnessSum);
            System.Diagnostics.Debug.WriteLine("Avg = " + fitnessAvg);
            System.Diagnostics.Debug.WriteLine("--------------------------");
            foreach (IIndividual c in Individuals)
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
