using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GeneticEngineCore;
using GenericPlugins;
using GeneticEngineSupport;
using RoadNetworkSolver;
using RoadNetworkDisplay;
using System.Xml;

namespace RoadNetworkGUI
{
    public partial class RoadNetworkFinder : Form
    {
        bool hasInitialised, hasCompleted;
        PluginLoader loader;
        GeneticEngine engine;
        List<string> populators, evaluators, geneticOperators, terminators, generationFactories, outputters;
        List<Coordinates> towns = new List<Coordinates>();
        Map map = new Map();
        //RoadNetwork network = new RoadNetwork();
        public RoadNetworkFinder()
        {
            InitializeComponent();
            initComponents();
            hasInitialised = false;
            hasCompleted = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            towns.Add(new Coordinates(0, 0));
            towns.Add(new Coordinates(55, 43));
            visualiser1.Network = new RoadNetwork();
            visualiser1.Towns = towns;
        }


        #region Action Events
        /*
         * Event where the load Library button is clicked
         * if the file has been successfully loaded, load plugins for each of the types
         * and populate these plugin names in the drop down lists
         */ 
        private void libraryLoaderButton_Click(object sender, EventArgs e)
        {
            if (isOK())
            {
                loader = new PluginLoader(openDialog.FileName);
                libraryLabel.Text = Path.GetFileName(openDialog.FileName);
                initEngineButton.Enabled = true;
                //populators = loader.GetPlugins(typeof(IPopulator));
                //evaluators = loader.GetPlugins(typeof(IEvaluator));
                //geneticOperators = loader.GetPlugins(typeof(IGeneticOperator));
                //terminators = loader.GetPlugins(typeof(ITerminator));
                //outputters = loader.GetPlugins(typeof(IOutputter));
                //generationFactories = loader.GetPlugins(typeof(IGenerationFactory));
            //initPluginDropDowns();
            }
        }

        private void MapFileSelectButton_Click(object sender, EventArgs e)
        {
            loadMapFile();
        }

        private void outputFileSelectButton_Click(object sender, EventArgs e)
        {
            loadOutputFile();
        }
        /*
         * Check if the plugins have been selected by the user, if not the revelant exceptions are thrown 
         * asking the user to make a choice.
         * So, if the populator,evaluators, geneticOperator and terminator choices are not chosen, then 
         * error messages are displayed to alert the user, that the choices for these four plugins cannot be null.
         * If the plugins are chosen, the engine object is created according to the user choices and it is initialised.
         * Once the engine is initialised, the rest of the buttons on the interface are activated and the fitness values
         * are displayed. 
         */ 
        private void initEngineButton_Click(object sender, EventArgs e)
        {
            string errorMsg = "";
            IPopulator populator;
            IEvaluator evaluator;
            IGeneticOperator geneticOperator;
            ITerminator terminator;
            IOutputter outputter;
            IGenerationFactory generationFactory;
            if (cbPopulator.SelectedItem == null) errorMsg += "Populator must not be null\n";
            else
            {
                string choice = getChoice(populators, cbPopulator);
            }
            if (cbEvaluator.SelectedItem == null) errorMsg += "Evaluator must not be null\n";
            else
            {
                string choice = getChoice(evaluators, cbEvaluator);
            }
            if (cbGeneticOperator.SelectedItem == null) errorMsg += "Genetic Operator must not be null\n";
            else
            {
                string choice = getChoice(geneticOperators, cbGeneticOperator);
            }
            if (cbTerminator.SelectedItem == null) errorMsg += "Terminator must not be null\n";
            else
            {
                string choice = getChoice(terminators, cbTerminator);
            }
            if (cbOutputter.SelectedItem != null)
            {
                string choice = getChoice(outputters, cbOutputter);
            }
            if (cbGenerationFactory.SelectedItem != null)
            {
                string choice = getChoice(generationFactories, cbGenerationFactory);
            }
            if (errorMsg != "") MessageBox.Show(errorMsg);
            else
            {
                //engine = new GeneticEngine(populator, evaluator, geneticOperator, terminator, outputter, generationFactory);
                //engine.Initialise();
                stepButton.Enabled = true;
                runButton.Enabled = true;
                runGenerationButton.Enabled = true;
                hasInitialised = true;
                setFitnessValues();
            }
        }
        /*
         * calls Step() of the engine only if it initialised. After the step method, the fitness values are updated
         * if the engine is not initialised, a message is displayed to initialise the engine.
         */ 
        private void stepButton_Click(object sender, EventArgs e)
        {
            if (hasInitialised)
            {
                engine.Step();
                setFitnessValues();
            }
            else
            {
                MessageBox.Show("Initialise Generation First\n");
            }
            if (engine.IsComplete)
            {
                hasCompleted = true;
                viewOutputFileButton.Enabled = true;
            }
        }

        /**
         * By clicking on the run button, Run() is executed from the engine if it was initialised. Afterwards, the fitness values
         * are updated on the interface. If the engine wasn't initialised, then a message is shown to the user to initialise the engine
         * first
         * If the engine's generation is terminated, the output file viewer button is activated for the user. 
         */ 
        private void runButton_Click(object sender, EventArgs e)
        {
            if (hasInitialised)
            {
                engine.Run();
                setFitnessValues();
            }
            else MessageBox.Show("Initialise Generation First\n");
            if (engine.IsComplete)
            {
                hasCompleted = true;
                viewOutputFileButton.Enabled = true;
            }
        }

        /**
         *  If the engine is initialised, the engine will call Repeat with the given n value provided by the scroller.
         *  If the value of the scroller item is null or 0 then a exception message is thrown to request the user to 
         *  choose a legitimiate value.
         *  Once repeat is called, the fitness values are recalculated and display on the interface.
         *  If the engine's generation has terminated after Repeat, then the view Output file button is activated.
         *  If the engine wasn't initialised, then the message is shown to initialise the engine before pressing the repeat button
         */
        private void runGenerationButton_Click(object sender, EventArgs e)
        {
            if (nScroller.SelectedItem == null) MessageBox.Show("Select a n value\n");
            else if (nScroller.SelectedIndex == 0) MessageBox.Show("Select a n value which is at least 1\n");
            else
            {
                if (hasInitialised)
                {
                    engine.Repeat(nScroller.SelectedIndex);
                    setFitnessValues();
                }
                else MessageBox.Show("Initialise Generation First\n");
                if (engine.IsComplete)
                {
                    hasCompleted = true;
                    viewOutputFileButton.Enabled = true;
                }
            }
        }

        /**
         * these values are obtained from the generation of the engine and are displayed on the interface. 
         */ 
        private void setFitnessValues()
        {
            maxFitnessValue.Text = engine.Generation.MaxFitness.ToString();
            averageFitnessValue.Text = engine.Generation.AverageFitness.ToString();
        }
        #endregion
        #region File Loading


        OpenFileDialog openDialog = new OpenFileDialog();
        /**
         * Check if the file is successfully chosen and opened. 
         */ 
        private bool isOK()
        {
            return (openDialog.ShowDialog() == DialogResult.OK);
        }

        /**
         * Load map file, if the file was successfully loaded by the OpenFileDialog Object 
         */
        private void loadMapFile()
        {
            if (isOK())
            {
                tbMapFile.Text = openDialog.FileName;
                string e = Path.GetExtension(tbMapFile.Text);
                if (String.Equals(e, ".xml"))
                {
                    XmlReader reader = XmlReader.Create(tbMapFile.Text);
                    map.ReadXml(reader);
                    //for (int i = 0; i < map.TownCount; i++)
                    //{
                    //    towns.Add(map.GetTown(i));
                    //}
                    //network.AddVertex(map.Start);
                    //network.AddVertex(map.End);
                }
                else
                {
                    MessageBox.Show("Map file should be in xml form\n");
                }
            }
        }

        /**
         * Load output file, if the file was successfully loaded by the OpenFileDialog object 
         */
        private void loadOutputFile()
        {
            if (isOK()) tbOutputFile.Text = openDialog.FileName;
        }

        /**
         * Populate the drop down list if each string list has at least one member. 
         * Display an error msg, if the populator, evaluator, geneticOperator and Terminator, string lists are empty. 
         * The reason here is, that no populator, evaluator, genetic Operator or Terminator should be null when we 
         * create the engine object
         */
        private void initPluginDropDowns()
        {
            for (int i = 0; i < populators.Count; i++)
            {
                cbPopulator.Items.Add(populators[i]);
            }
            for (int i = 0; i < evaluators.Count; i++)
            {
                cbEvaluator.Items.Add(evaluators[i]);
            }
            for (int i = 0; i < geneticOperators.Count; i++)
            {
                cbGeneticOperator.Items.Add(geneticOperators[i]);
            }
            for (int i = 0; i < terminators.Count; i++)
            {
                cbTerminator.Items.Add(terminators[i]);
            }
            for (int i = 0; i < outputters.Count; i++)
            {
                cbOutputter.Items.Add(outputters[i]);
            }
            for (int i = 0; i < generationFactories.Count; i++)
            {
                cbGenerationFactory.Items.Add(generationFactories[i]);
            }
        }
        /**
         * Populate the 'n'scroller and the target fitness scroller with values from 1 up to the maximum values
         * Furthermore deactivate all of the buttons from the user
         */ 
        private void initComponents()
        {
            int MAXFITNESS = 900;
            for (int i = 1; i <= MAXFITNESS; i++)
            {
                targetFitnessScroller.Items.Add(i.ToString());
            }
            int N = 900;
            for (int i = 1; i <= N; i++)
            {
               nScroller.Items.Add(i.ToString());
            }
            initEngineButton.Enabled = false;
            runButton.Enabled = false;
            runGenerationButton.Enabled = false;
            stepButton.Enabled = false;
            viewOutputFileButton.Enabled = false;
        }
        /**
         * Based on the selected index from a particular combo box, obtain the string from the index of the specified list.
         * This is used for getInstance() of the engine. 
         */ 
        private string getChoice(List<string> list, ComboBox cb)
        {
            return list[cb.SelectedIndex];
        }
        #endregion

        /**
         * If the generation has been completed and once the view Output file Button is clicked. 
         * Close the current interface and open up the Visualiser interface. 
         */ 
        private void viewOutputFileButton_Click(object sender, EventArgs e)
        {
            if (hasCompleted)
            {
                this.Dispose(false);
                Road_Network_Visualiser form = new Road_Network_Visualiser();
                form.Visible = true;
            }
        }
    }
 }
