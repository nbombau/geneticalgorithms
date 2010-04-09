using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Evolutive
{
    /// <summary>
    /// Representa el nodo del arbol de expresiones
    /// que contiene a los genes - operadores, valores y variables
    /// </summary>
    public class TreeNode : ICloneable
    {
        // gen del nodo
        public ITreeGene Gene;
        // hijos
        public ArrayList Children;

        /// <summary>
        /// Constructor
        /// </summary>
        public TreeNode() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public TreeNode(ITreeGene gene)
        {
            Gene = gene;
        }

        /// <summary>
        /// obtener representacion string del nodo
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (Children != null)
            {
                // recorremos los hijos
                foreach (TreeNode node in Children)
                {
                    sb.Append(node.ToString());
                }
            }

            sb.Append(Gene.ToString());
            sb.Append(" ");

            return sb.ToString();
        }

        /// <summary>
        /// Clonar el nodo
        /// </summary>
        public object Clone()
        {
            TreeNode clone = new TreeNode();

            clone.Gene = this.Gene.Clone();
            // se clonan los hijos tambien
            if (this.Children != null)
            {
                clone.Children = new ArrayList();
                // de cada hijo clonamos sus genes
                foreach (TreeNode node in Children)
                {
                    clone.Children.Add(node.Clone());
                }
            }
            return clone;
        }
    }
}
