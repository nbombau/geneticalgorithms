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
using System.Configuration;

namespace EvolutivePresentation
{
    public partial class MainForm : Form
    {
        #region Propiedades 
        
        private Population population = default(Population);
        private Population Population
        {
            get { return population; }
            set { population = value; }
        }

        private ISelection selection = default(ISelection);
        private ISelection Selection
        {
            get 
            {
                selection = SelectionFactory.CreateSelection(
                    ConfigurationManager.AppSettings[ComboSelectedText]
                );
                if (selection == default(ISelection))
                {
                    selection = SelectionFactory.CreateSelection(ConfigurationManager.AppSettings[0]);
                }
                return selection;
            }
        }

        private ISelection replacement = default(ISelection);
        private ISelection Replacement
        {
            get
            {
                replacement = SelectionFactory.CreateSelection(
                    ConfigurationManager.AppSettings[ReplacementComboSelectedText]
                );
                if (replacement == default(ISelection))
                {
                    replacement = SelectionFactory.CreateSelection(ConfigurationManager.AppSettings[0]);
                }
                return replacement;
            }
        }

        private string ComboSelectedText
        {
            get
            {
                if (cmbSelectionMethod.SelectedItem != null && !String.IsNullOrEmpty(cmbSelectionMethod.SelectedItem.ToString()))
                {
                    return cmbSelectionMethod.SelectedItem.ToString();
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        private string ReplacementComboSelectedText
        {
            get
            {
                if (cmbReplacementMethod.SelectedItem != null && !String.IsNullOrEmpty(cmbReplacementMethod.SelectedItem.ToString()))
                {
                    return cmbReplacementMethod.SelectedItem.ToString();
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        private int Iterations 
        {
            get { return (int)numIterations.Value; }
        }

        private int Individuals
        {
            get { return (int)numIndividuals.Value; }
        }

        private double MutationRate
        {
            get 
            {
                try
                {
                    return (double)Double.Parse(txtMutation.Text) / 100;
                }
                catch (Exception)
                {
                    return 0.0005;
                }
            }
        }

        private int SelectionFirstCount
        {
            get { return (int)numSelectionCount1.Value; }
        }

        private int SelectionSecondCount
        {
            get { return (int)numSelectionCount2.Value; }
        }

        private int ReplacementFirstCount
        {
            get { return (int)numReplacementCount1.Value; }
        }

        private int ReplacementSecondCount
        {
            get { return (int)numReplacementCount2.Value; }
        }

        private int actual = 0;

        #endregion Propiedades

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            cmbReplacementMethod.SelectedIndex = 0;
            cmbSelectionMethod.SelectedIndex = 0;
           // UpdateChart();
        }

        #endregion Constructor

        #region Eventos

        private void MainForm_Load(object sender, EventArgs e) { }

        public void OnGenerationEnded(object sender, GenerationEndedEventArgs e)
        {
            try
            {
                double[,] data = new double[population.PopulationSize, 2];
                Population p = e.Population;
                for (int j = 0; j < p.PopulationSize; j++)
                {
                    data[j, 0] = j;
                    data[j, 1] = p.ElementAt(j).Fitness;
                }
              //  chart.UpdateDataSeries("Individuals", data);

                #if debug
                
                Trace(e.GenerationNumber);
                
                #endif
            }
            catch (Exception)
            {
                Reset();
                if (btnStart.Enabled == false) { ToggleControls(); }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validate())
                {
                    backgroundWorker1.RunWorkerAsync();
                    //Go(null, null);
                }           
            }
            catch (Exception)
            {
                Reset();
                if (btnStart.Enabled == false) { ToggleControls(); }
            }            
        }

        #endregion Eventos

        #region Metodos

        private void Go(object sender, DoWorkEventArgs e)
        {
            bool[,] iterationSolution;
            bool needToStop = false;
            for (int j = 4; j < 8; j++)
            {
                iterationSolution = GetIterationSolution(j);

                EvolutiveFitnessFunction fitness = new EvolutiveFitnessFunction(iterationSolution);

                ITreeGene gene = (ITreeGene)new BooleanFunction(4);

                if ((Selection is IMixedSelection) || (Replacement is IMixedSelection))
                {
                    Population = new Population(Individuals,
                            (IIndividual)new TreeIndividual(gene),
                        fitness, Selection, Replacement, Iterations,
                        MutationRate, SelectionFirstCount, ReplacementFirstCount,
                        SelectionSecondCount, ReplacementSecondCount);
                }
                else
                {
                    Population = new Population(Individuals,
                            (IIndividual)new TreeIndividual(gene),
                        fitness, Selection, Replacement, Iterations,
                        MutationRate, SelectionFirstCount, ReplacementFirstCount);
                }
                Population.GenerationEnded += new Population.GenerationEndedEventHandler(OnGenerationEnded);
                int i = 1;

                while (!needToStop)
                {
                    //corremos la simulacion para una generacion
                    population.RunEpoch();
                    population.OnGenerationEnded(new GenerationEndedEventArgs(population, i));
                    try
                    {
                        string bestFunction = population.BestIndividual.ToString();
                        OutputLine(bestFunction);
                    }
                    catch (Exception) { }
                    i++;
                    if ((Iterations != 0) && (i > Iterations))
                        needToStop = true;
                }
                needToStop = false;

                bool[] variables = new bool[4];
                OutputLine("bit " + (3 - (j - 4)).ToString());
                for (int k = 0; k < iterationSolution.GetLength(0); k++)
                {
                    variables[0] = iterationSolution[k, 0];
                    variables[1] = iterationSolution[k, 1];
                    variables[2] = iterationSolution[k, 2];
                    variables[3] = iterationSolution[k, 3];
                    OutputLine(iterationSolution[k, 0].ToString() + iterationSolution[k, 1].ToString() + iterationSolution[k, 2].ToString() + iterationSolution[k, 3].ToString() + " = " +
                            PolishBooleanExpression.Evaluate(population.BestSolution.ToString(), variables).ToString()
                        );
                }
                OutputLine(population.BestSolution.ToString());
                OutputLine(PolishBooleanExpression.ToInfix(population.BestSolution.ToString(), variables));
                OutputLine("Aptitud: " + population.BestSolution.Fitness);
                OutputLine("Es correcta: " + ((population.BestSolution.Fitness > 69000.00 - population.BestSolution.Fitness.ToString().Length) ? "Si" : "No"));
                SetSolution((3 - (j - 4)), PolishBooleanExpression.ToInfix(population.BestSolution.ToString(), variables));
            }
            lblStatus.Text = "Finalizado";
        }

        private void Reset()
        {
            population = default(Population);
        }

        private void SetSolution(int bit, string sol)
        {
            switch (bit)
            { 
                case 0:
                    lblXX0.Text = sol;
                    break;
                case 1:
                    lblX1.Text = sol;
                    break;
                case 2:
                    lblX2.Text = sol;
                    break;
                case 3:
                    lblX3.Text = sol;
                    break;
            }
        }

        private void OutputLine(string text)
        {
            txtOutput.Text += "\r\n" + text;
        }
        

        private void ToggleControls()
        {
            numIndividuals.Enabled = !numIndividuals.Enabled;
            numIterations.Enabled = !numIterations.Enabled;
            txtMutation.Enabled = !txtMutation.Enabled;
            btnStart.Enabled = !btnStart.Enabled;
            cmbSelectionMethod.Enabled = !cmbSelectionMethod.Enabled;
            cmbReplacementMethod.Enabled = !cmbReplacementMethod.Enabled;
            numReplacementCount1.Enabled = !numReplacementCount1.Enabled;
            numReplacementCount2.Enabled = !numReplacementCount2.Enabled;
            numSelectionCount1.Enabled = !numSelectionCount1.Enabled;
            numSelectionCount2.Enabled = !numSelectionCount2.Enabled;
        }

        private bool Validate()
        {
            if (!ReplacementComboSelectedText.Equals("ElitismoRuleta") && !ReplacementComboSelectedText.Equals("ElitismoUniversal"))
            {
                if (ReplacementFirstCount != Individuals)
                {
                    MessageBox.Show("En el reemplazo se seleccionara una cantidad de individuos igual al tamaño seteado para la poblacion.");
                }
            }
            else
            {
                if (ReplacementFirstCount + ReplacementSecondCount != Individuals)
                {
                    MessageBox.Show("La suma de individuos a seleccionar en los dos metodos de reemplazo debe ser igual a la cantidad de individuos");
                    return false;
                }
            }

            if (ComboSelectedText.Equals("ElitismoRuleta") || ComboSelectedText.Equals("ElitismoUniversal"))
            {
                if (SelectionFirstCount + SelectionSecondCount > Individuals)
                {
                    MessageBox.Show("La suma de individuos a seleccionar en los dos metodos de seleccion debe ser menor o igual cantidad de individuos");
                    return false;
                }
            }
            try
            {
                double mutRate = (double)Double.Parse(txtMutation.Text) / 100;
            }
            catch 
            {
                MessageBox.Show("Al no haber seleccionado probabilidad de mutacion, se tomara como 0.5% por defecto");
            }
            return true;
        }

        private bool[,] GetIterationSolution(int j)
        {
            return new bool[16, 5]{
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
        }

        #endregion Metodos

        private bool[,] truthTable = new bool[16, 8]{
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

        private void WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("fin");
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "";
            lblXX0.Text = "";
            lblX1.Text = "";
            lblX2.Text = "";
            lblX3.Text = "";
        }
    }
}
