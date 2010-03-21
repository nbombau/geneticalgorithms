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
        private OptimizationFunction function = new OptimizationFunction(new DoubleRange(0,1));
        
        private Population population;
        private Population Population
        {
            get
            {
                if (population == default(Population))
                {
                    population = new Population(Individuals,
                        new BinaryChromosome(BitNumber),
                        function,
                        new WheelSelection() as ISelection,
                        Iterations);
                }
                return population;
            }
        }


        private int Iterations 
        {
            get { return (int)numIterations.Value; }
        }

        private int BitNumber
        {
            get { return (int)numBits.Value; }
        }

        private int Individuals
        {
            get { return (int)numIndividuals.Value; }
        }

        public MainForm()
        {
            InitializeComponent();


            // add data series to chart
            chart.AddDataSeries("function", Color.Red, Chart.SeriesType.Line, 1);
            chart.AddDataSeries("solution", Color.Blue, Chart.SeriesType.Dots, 5);
            UpdateChart();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void UpdateChart()
        {
            // update chart range
            chart.RangeX = function.Range;

            double[,] data = null;

            if (chart.RangeX.Length > 0)
            {
                // prepare data
                data = new double[501, 2];

                double minX = function.Range.Min;
                double length = function.Range.Length;

                for (int i = 0; i <= 500; i++)
                {
                    data[i, 0] = minX + length * i / 500;
                    data[i, 1] = function.Function.Compile().Invoke(data[i, 0]);
                }
            }

            // update chart series
            chart.UpdateDataSeries("function", data);
        }

        public void OnGenerationEnded(object sender, GenerationEndedEventArgs e)
        {
            double[,] data = new double[population.PopulationSize, 2];
            Population p = e.Population;
            for (int j = 0; j < p.PopulationSize; j++)
            {
                data[j, 0] = function.TranslateNative(p.ElementAt(j));
                data[j, 1] = function.Function.Compile().Invoke(data[j, 0]);
            }
            chart.UpdateDataSeries("solution", data);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Population.GenerationEnded += new Population.GenerationEndedEventHandler(OnGenerationEnded);
            ToggleControls();
            backgroundWorker1.RunWorkerAsync();
        }

        private void DoWorkOnBackground(object sender, DoWorkEventArgs e)
        {
            population.RunSimulation();
        }

        private void BackgroundWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtBest.Text = function.TranslateNative(population.BestSolution).ToString();
            Reset();
            ToggleControls();
        }

        private void Reset()
        {
            population = default(Population);
        }

        private void ToggleControls()
        {
            numBits.Enabled = !numBits.Enabled;
            numIndividuals.Enabled = !numIndividuals.Enabled;
            numIterations.Enabled = !numIterations.Enabled;
            btnStart.Enabled = !btnStart.Enabled;
            btnStop.Enabled = !btnStop.Enabled;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        } 
    }
}
