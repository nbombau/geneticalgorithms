using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Genetics;
using Optimization;
using System.Diagnostics;

namespace Presentation
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SearchSolution();
        }

        void SearchSolution()
        {
            OptimizationFunction f = new OptimizationFunction(new DoubleRange(0, 255)) { Mode = OptimizationFunction.OptimizationModes.Maximization };
            Population population = new Population(100,
                new BinaryChromosome(9),
                f,
                (ISelection) new WheelSelection()
                );

            int i = 1;
            while (i < 100)
            {
                population.RunEpoch();
                Debug.WriteLine("Generacion " + i.ToString());

                    for (int j = 0; j < population.PopulationSize; j++)
                    {
                        Debug.WriteLine(population.ElementAt(j).ToString() + "-"+f.Translate((population.ElementAt(j) as BinaryChromosome)));
                    }


                i++;
            }

        }
    }
}
