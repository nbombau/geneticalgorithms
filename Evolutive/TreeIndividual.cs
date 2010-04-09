using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genetics;
using System.Collections;

namespace Evolutive
{
    /// <summary>
    /// Individuo que representa un arbol de nodos,
    /// cada nodo conteniendo un gen - operador, variable o constante
    /// </summary>
    public class TreeIndividual : IIndividual
    {
        // raiz
        private TreeNode root = new TreeNode();
        // aptitud del individuo
        protected double fitness = 0;


        // maximo nivel con el que se generan los arboles
        private static int maxInitialLevel = 3;
        // maximo nivel al que pueden llegar
        private static int maxLevel = 8;

        //numeros aleatorios
        protected static Random rand = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Valor de aptitud del individuo
        /// </summary>
        public double Fitness
        {
            get { return fitness; }
        }

        /// <summary>
        /// Maximo nivel inicial del arbol
        /// </summary>
        public static int MaxInitialLevel
        {
            get { return maxInitialLevel; }
            set { maxInitialLevel = Math.Max(1, Math.Min(25, value)); }
        }

        /// <summary>
        /// Maximo nivel del arbol
        /// </summary>
        public static int MaxLevel
        {
            get { return maxLevel; }
            set { maxLevel = Math.Max(1, Math.Min(50, value)); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TreeIndividual(ITreeGene ancestor)
        {
            // hacemos que el parametor sea la raiz temporaria
            root.Gene = ancestor;
            // llamamos a la funcion de generacion de arboles
            Generate();
        }

        /// <summary>
        /// Constructor de copia
        /// </summary>
        protected TreeIndividual(TreeIndividual source)
        {
            root = (TreeNode)source.root.Clone();
            fitness = source.fitness;
        }

        /// <summary>
        /// Obtener arbol en notacion polaca (posfija)
        /// </summary>
        public override string ToString()
        {
            return root.ToString();
        }

        /// <summary>
        /// Comparar dos individuos
        /// </summary>
        public int CompareTo(object o)
        {
            double f = ((TreeIndividual)o).fitness;

            return (fitness == f) ? 0 : (fitness < f) ? 1 : -1;
        }

        /// <summary>
        /// Generar arbol aleatorio
        /// </summary>
        public virtual void Generate()
        {
            // se aleatoriza la raiz
            root.Gene.Generate();
            // y se crean hijos
            if (root.Gene.ArgumentsCount != 0)
            {
                root.Children = new ArrayList();
                for (int i = 0; i < root.Gene.ArgumentsCount; i++)
                {
                    // nuevo hijo
                    TreeNode child = new TreeNode();
                    Generate(child, rand.Next(maxInitialLevel));
                    // se agrega al nuevo nodito
                    root.Children.Add(child);
                }
            }
        }

        /// <summary>
        /// Genera un subarbol del nivel especificado
        /// </summary>
        protected void Generate(TreeNode node, int level)
        {
            if (level == 0)
            {
                // si el nivel es 0, deberia ser un argumento, pues
                // una expresion no puede contener solo un operador
                node.Gene = root.Gene.CreateNew(GPGeneType.Argument);
            }
            else
            {
                // si level > 0, puede ser operador o variable
                node.Gene = root.Gene.CreateNew();
            }

            // agregar hijos
            if (node.Gene.ArgumentsCount != 0)
            {
                node.Children = new ArrayList();
                for (int i = 0; i < node.Gene.ArgumentsCount; i++)
                {
                    // nuevo hijo
                    TreeNode child = new TreeNode();
                    Generate(child, level - 1);
                    // agregar el hijo
                    node.Children.Add(child);
                }
            }
        }

        /// <summary>
        /// Crear nuevo individuo random
        /// </summary>
        public virtual IIndividual CreateRandomIndividual()
        {
            return new TreeIndividual(root.Gene.Clone());
        }

        /// <summary>
        /// Clonar individuo
        /// </summary>
        public virtual IIndividual Clone()
        {
            return new TreeIndividual(this);
        }

        /// <summary>
        /// Operador de mutacion
        /// </summary>
        public virtual void Mutate()
        {
            // nivel actual
            int currentLevel = 0;
            // nodo actual
            TreeNode node = root;

            for (; ; )
            {
                // regenerar nodo si no tiene hijos
                if (node.Children == null)
                {
                    if (currentLevel == maxLevel)
                    {
                        // si llegamos al maximo nivel, en las hojas
                        // solo puede haber variables
                        node.Gene.Generate(GPGeneType.Argument);
                    }
                    else
                    {
                        // no llegamos al maximo nivel, generar subarbol
                        Generate(node, rand.Next(maxLevel - currentLevel));
                    }
                    break;
                }
                // Si es un nodo operador, debemos elegir el punto de mutacion,
                // el nodo mismo o uno de sus hijos
                int r = rand.Next(node.Gene.ArgumentsCount + 1);

                if (r == node.Gene.ArgumentsCount)
                {
                    // el nodo mismo se regenera
                    node.Gene.Generate();

                    // chequeamos el tipo
                    if (node.Gene.GeneType == GPGeneType.Argument)
                    {
                        node.Children = null;
                    }
                    else
                    {
                        // crear lista de hijos si no tiene
                        if (node.Children == null)
                            node.Children = node.Children = new ArrayList();

                        // nos fijamos si le faltan argumentos al operador, 
                        //en tal caso agregamos hijos
                        if (node.Children.Count != node.Gene.ArgumentsCount)
                        {
                            if (node.Children.Count > node.Gene.ArgumentsCount)
                            {
                                // quitamos hijos de mas
                                node.Children.RemoveRange(node.Gene.ArgumentsCount, node.Children.Count - node.Gene.ArgumentsCount);
                            }
                            else
                            {
                                // agregamos hijos faltantes
                                for (int i = node.Children.Count; i < node.Gene.ArgumentsCount; i++)
                                {
                                    TreeNode child = new TreeNode();
                                    Generate(child, rand.Next(maxLevel - currentLevel));
                                    node.Children.Add(child);
                                }
                            }
                        }
                    }
                    break;
                }

                node = (TreeNode)node.Children[r];
                currentLevel++;
            }
        }

        /// <summary>
        /// Operador de cruce
        /// </summary>
        public virtual void Crossover(IIndividual pair)
        {
            TreeIndividual p = (TreeIndividual)pair;

            if (p != null)
            {
                // necesitamos cruzar en la raiz?
                if ((root.Children == null) || (rand.Next(maxLevel) == 0))
                {
                    // le damos la raiz al individuo, y lo usamos parte
                    // suya como nueva raiz (seria como un injerto)
                    root = p.RandomSwap(root);
                }
                else
                {
                    TreeNode node = root;

                    for (; ; )
                    {
                        // elegimos un hijo al azar
                        int r = rand.Next(node.Gene.ArgumentsCount);
                        TreeNode child = (TreeNode)node.Children[r];

                        // swappeamos en un nodo al azar
                        if ((child.Children == null) || (rand.Next(maxLevel) == 0))
                        {
                            // con el del individuo provisto
                            node.Children[r] = p.RandomSwap(child);
                            break;
                        }

                        // continuamos por el arbol
                        node = child;
                    }
                }
                // podamos un poco
                //TODO: Comentar???
                Trim(root, maxLevel);
                Trim(p.root, maxLevel);
            }
        }

        /// <summary>
        /// Rutina helper para el operador de cruce
        /// Selecciona un nodo random de un arbol y lo intercambia con otro 
        /// nodo random de otro arbol
        /// </summary>
        private TreeNode RandomSwap(TreeNode source)
        {
            TreeNode retNode = null;

            // swappeamos la raiz?
            if ((root.Children == null) || (rand.Next(maxLevel) == 0))
            {
                // intercambiamos
                retNode = root;
                root = source;
            }
            else
            {
                TreeNode node = root;

                for (; ; )
                {
                    // se elije un hijo random
                    int r = rand.Next(node.Gene.ArgumentsCount);
                    TreeNode child = (TreeNode)node.Children[r];

                    // swappeamos el hijo
                    if ((child.Children == null) || (rand.Next(maxLevel) == 0))
                    {
                        // con el nodo del amigo
                        retNode = child;
                        node.Children[r] = source;
                        break;
                    }

                    // seguimos recorriendo el arbol
                    node = child;
                }
            }
            return retNode;
        }

        /// <summary>
        /// Poda el arbol, para que no se pase del nivel provisto
        /// </summary>
        private static void Trim(TreeNode node, int level)
        {
            // checkear si tiene hijos
            if (node.Children != null)
            {
                if (level == 0)
                {
                    // se quitan todos
                    node.Children = null;
                    // se hace que el nodo sea hoja
                    node.Gene.Generate(GPGeneType.Argument);
                }
                else
                {
                    // recorriendo los hijos
                    foreach (TreeNode n in node.Children)
                    {
                        Trim(n, level - 1);
                    }
                }
            }
        }

        /// <summary>
        /// Evaluar el individuo con una funcion de aptitud provistas
        /// </summary>
        public void Evaluate(IFitnessFunction function)
        {
            fitness = function.Evaluate(this);
        }
    }
}
