using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genetics;

namespace Evolutive
{
    /// <summary>
    /// Operadores Booleanos
    /// </summary>
    public class BooleanFunction : ITreeGene
    {
        // lista de operadores soportados
        protected enum Functions
        {
            And,
            Or,
            Xor,
            Not
        }

        protected const int FunctionsCount =4;

        // tipo de gen (argumento u operador)
        private GeneType type;
        // cantidad de variables
        private int variablesCount;
        private int val;

        // numeros aleatorios
        protected static Random rand = new Random((int)DateTime.Now.Ticks);


        /// <summary>
        /// tipo de gen (argumento u operador)
        /// </summary>
        public GeneType GeneType
        {
            get { return type; }
        }

        /// <summary>
        /// cantidad de argumentos
        /// </summary>
        public int ArgumentsCount
        {
            get { return (type == GeneType.Argument) ? 0 : ((Functions)val) == Functions.Not ? 1:2; }
        }

        /// <summary>
        /// maxima cantidad de argumentos
        /// </summary>
        public int MaxArgumentsCount
        {
            get { return 2; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BooleanFunction(int variablesCount) : this(variablesCount, true) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public BooleanFunction(int variablesCount, GeneType type)
        {
            this.variablesCount = variablesCount;
            // se genera el valor
            Generate(type);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        protected BooleanFunction(int variablesCount, bool random)
        {
            this.variablesCount = variablesCount;
            // generar el valor
            if (random)
                Generate();
        }

        /// <summary>
        /// obtener la representacion en string
        /// </summary>
        public override string ToString()
        {
            if (type == GeneType.Function)
            {
                switch ((Functions)val)
                {
                    case Functions.And:			// AND
                        return "&";

                    case Functions.Or:	// OR
                        return "|";
                    case Functions.Not:
                        return "!";     //NOT
                    case Functions.Xor:
                        return "^"; // XOR
                }
            }
            return string.Format("${0}", val);
        }

        /// <summary>
        /// Clonar
        /// </summary>
        public ITreeGene Clone()
        {
            // crear un nuevo nodo
            BooleanFunction clone = new BooleanFunction(variablesCount, false);
            clone.type = type;
            clone.val = val;

            return clone;
        }

        /// <summary>
        /// Generar un nodo aleatoriamente
        /// </summary>
        public void Generate()
        {
            // 0/1/2 es operador, 3 es argumento. 25% de chances de q sea argumento
            Generate((rand.Next(4) == 3) ? GeneType.Argument : GeneType.Function);
        }

        /// <summary>
        /// generar un nodo aleatoriamente de un cierto tipo
        /// </summary>
        public void Generate(GeneType type)
        {
            this.type = type;
            val = rand.Next((type == GeneType.Function) ? FunctionsCount : variablesCount);

        }

        /// <summary>
        /// crea un nuevo nodo de tipo aleatorio
        /// </summary>
        public ITreeGene CreateNew()
        {
            return new BooleanFunction(variablesCount);
        }

        /// <summary>
        /// crea un nuevo nodo de cierto tipo
        /// </summary>
        public ITreeGene CreateNew(GeneType type)
        {
            return new BooleanFunction(variablesCount, type);
        }
    }
}
