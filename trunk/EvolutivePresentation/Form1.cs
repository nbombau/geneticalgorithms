using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Genetics;
using Evolutive;
using System.Diagnostics;

namespace EvolutivePresentation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Go();
        }



        private double[,] data = null;

        private int populationSize = 100;
        private int iterations = 1000;
        private int selectionMethod = 0;
        private int functionsSet = 0;
        private int geneticMethod = 0;


        private bool needToStop = false;

        private void Go()
        {
            bool[,] truthTable = new bool[16, 8]{
                {false,false,false,false,false,false,false,false},
                {false,false,false,true,false,false,false,false},
                {false,false,true,false,false,false,false,false},
                {false,false,true,true,false,false,false,false},
                {false,true,false,false,false,false,false,false},
                {false,true,false,true,false,false,false,true},
                {false,true,true,false,false,false,true,false},
                {false,true,true,true,false,false,true,true},
                {true,false,false,false,false,false,false,false},
                {true,false,false,true,false,false,true,false},
                {true,false,true,false,false,true,false,false},
                {true,false,true,true,false,true,true,false},
                {true,true,false,false,false,false,false,false},
                {true,true,false,true,false,false,true,true},
                {true,true,true,false,false,true,true,false},
                {true,true,true,true,true,false,false,true}
            };
            bool[,] iterationSolution;
            for (int j = 4; j < 8; j++)
            {
                iterationSolution = new bool[16, 5]{
                    {false,false,false,false,truthTable[0,j]},
                    {false,false,false,true,truthTable[1,j]},
                    {false,false,true,false,truthTable[2,j]},
                    {false,false,true,true,truthTable[3,j]},
                    {false,true,false,false,truthTable[4,j]},
                    {false,true,false,true,truthTable[5,j]},
                    {false,true,true,false,truthTable[6,j]},
                    {false,true,true,true,truthTable[7,j]},
                    {true,false,false,false,truthTable[8,j]},
                    {true,false,false,true,truthTable[9,j]},
                    {true,false,true,false,truthTable[10,j]},
                    {true,false,true,true,truthTable[11,j]},
                    {true,true,false,false,truthTable[12,j]},
                    {true,true,false,true,truthTable[13,j]},
                    {true,true,true,false,truthTable[14,j]},
                    {true,true,true,true,truthTable[15,j]}
                };

                SymbolicRegressionFitness fitness = new SymbolicRegressionFitness(iterationSolution);

                ITreeGene gene = (ITreeGene)new BooleanFunction(4);

                Population population = new Population(populationSize,
                        (IIndividual)new TreeIndividual(gene),
                    fitness, (ISelection)new WheelSelection(), 100);

                int i = 1;


                while (!needToStop)
                {

                    population.RunEpoch();

                    try
                    {

                        string bestFunction = population.BestIndividual.ToString();

                        Debug.WriteLine(bestFunction);

                    }
                    catch (Exception)
                    {

                    }

                    i++;

                    //
                    if ((iterations != 0) && (i > iterations))
                        break;
                }
                bool[] variables = new bool[4];
                Debug.WriteLine("solution " + j.ToString());
                for (int k = 0; k < iterationSolution.GetLength(0); k++)
                {
                    variables[0] = iterationSolution[k, 0];
                    variables[1] = iterationSolution[k, 1];
                    variables[2] = iterationSolution[k, 2];
                    variables[3] = iterationSolution[k, 3];
                    Debug.WriteLine(iterationSolution[k, 0].ToString() + iterationSolution[k, 1].ToString() + iterationSolution[k, 2].ToString() + iterationSolution[k, 3].ToString() + " = " +
                            PolishBooleanExpression.Evaluate(population.BestSolution.ToString(), variables).ToString()
                        );
                }

                Debug.WriteLine(population.BestIndividual.ToString());

            }         
        }
    }
}
