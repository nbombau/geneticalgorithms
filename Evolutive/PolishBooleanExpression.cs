using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Evolutive
{

    /// <summary>
    /// Evaluador de expresiones en notacion polaca
    /// </summary>
    
    public class PolishBooleanExpression
    {
        /// <summary>
        /// Constructor
        /// </summary>
        private PolishBooleanExpression() { }

        /// <summary>
        /// Evalua la expresion pedida
        /// </summary>        ///
        /// <param name="expression">
        /// Expresion escrita en notacion polaca posfija
        /// </param>
        /// <param name="variables">Variables de entrada</param>
        /// <returns>Valor evaluado de la expresion</returns>
        public static bool Evaluate(string expression, bool[] variables)
        {
            // Se divide la expresion en tokens
            string[] tokens = expression.Trim().Split(' ');
            // stack de argumentos
            Stack arguments = new Stack();

            // para cada token
            foreach (string token in tokens)
            {
                // si es un valor booleano
                if (bool.FalseString == token || bool.TrueString == token)
                {
                    // se agrega a la pila
                    arguments.Push(bool.Parse(token));
                }
                else if (token[0] == '$')
                {
                    // si es una variable
                    arguments.Push(variables[int.Parse(token.Substring(1))]);
                }
                // si es un operador
                else
                {
                    // cada operador recibe al menos un argumento,
                    // se saca el de arriba de la pila
                    bool v = (bool)arguments.Pop();

                    switch (token)
                    {
                        case "&":			// AND
                            arguments.Push((bool)arguments.Pop() & v);
                            break;

                        case "|":			// OR
                            arguments.Push((bool)arguments.Pop() | v);
                            break;
                        case "!":			// NOT
                            arguments.Push(!v);
                            break;
                            
                        case "^":			// XOR
                            arguments.Push((bool)arguments.Pop() ^ v);
                            break;

                        default:
                            // Operador no implementado
                            throw new ArgumentException("Operador indefinido: " + token);
                    }
                }
            }

            // chequeamos el valor del stack, deberia haber un solo valor,
            // que es el resultado de la evaluacion
            if (arguments.Count != 1)
            {
                throw new ArgumentException("Expresion incorrecta");
            }

            // retornamos el unico valor que queda en el stack
            return (bool)arguments.Pop();
        }
    }
}
