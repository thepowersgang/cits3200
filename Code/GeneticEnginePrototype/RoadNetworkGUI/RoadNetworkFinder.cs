using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GeneticAlgorithm;
using GeneticAlgorithm.Plugin;
using GeneticAlgorithm.Util;
using RoadNetworkDefinition;
using RoadNetworkDisplay;
using GeneticAlgorithm.Plugin.Generic;
using System.Xml;
using System.Threading;

namespace RoadNetworkGUI
{
    public partial class RoadNetworkFinder : Form
    {
        bool hasInitialised, hasCompleted;
        string fullDLLPath = "";
        IPopulator populator;
        IEvaluator evaluator;
        IGeneticOperator geneticOperator;
        ITerminator terminator;
        IGenerationFactory generationFactory;
        PluginLoader loader;
        GeneticEngine engine;
        List<string> populators, evaluators, geneticOperators, terminators, generationFactories, outputters;
        List<Coordinates> towns = new List<Coordinates>();
        Map map;
        DisplayOutputter displayOutputter;
        IOutputter outputter;
        XmlOutputter xmlOutputter;

        public RoadNetworkFinder()
        {
            InitializeComponent();
            initComponents();
            hasInitialised = false;
            hasCompleted = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        #region Action Events
        /*
         * Event where the load Library button is clicked
         * if the file has been successfully loaded, load plugins for each of the types
         * and populate these plugin names in the drop down lists
         */ 
        private void libraryLoaderButton_Click(object sender, EventArgs e)
        {
            if (isOK(LoadLibrary,"library"))
            {
                loader = new PluginLoader();
                loader.LoadDll(LoadLibrary.FileName);
                fullDLLPath = LoadLibrary.FileName;
                libraryLabel.Text = Path.GetFileName(LoadLibrary.FileName);
                initEngineButton.Enabled = true;
                populators = loader.GetPluginNames(typeof(IPopulator));
                evaluators = loader.GetPluginNames(typeof(IEvaluator));
                geneticOperators = loader.GetPluginNames(typeof(IGeneticOperator));
                terminators = loader.GetPluginNames(typeof(ITerminator));
                outputters = loader.GetPluginNames(typeof(IOutputter));
                generationFactories = loader.GetPluginNames(typeof(IGenerationFactory));
                initPluginDropDowns();
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
            if (cbPopulator.SelectedItem == null) errorMsg += "Populator must not be null\n";
            else
            {
                string choice = getChoice(populators, cbPopulator);
                populator = (IPopulator) loader.GetInstance(choice, (object)tbMapFile.Text);
            }
            if (cbEvaluator.SelectedItem == null) errorMsg += "Evaluator must not be null\n";
            else
            {
                string choice = getChoice(evaluators, cbEvaluator);
                evaluator = (IEvaluator)loader.GetInstance(choice, null);
            }
            if (cbGeneticOperator.SelectedItem == null) errorMsg += "Genetic Operator must not be null\n";
            else
            {
                string choice = getChoice(geneticOperators, cbGeneticOperator);
                geneticOperator = (IGeneticOperator)loader.GetInstance(choice, null);
            }
            if (cbTerminator.SelectedItem == null) errorMsg += "Terminator must not be null\n";
            else
            {
                string choice = getChoice(terminators, cbTerminator);
                if((int) targetFitness.Value == 0) errorMsg += "Provide a target fitness value greater than 1 for the terminator plug-in\n";
                else terminator = (ITerminator) loader.GetInstance(choice, (object) (uint)targetFitness.Value);
            }
            if (cbOutputter.SelectedItem != null)
            {                
                string choice = getChoice(outputters, cbOutputter);
                if (tbOutputFile.Text == "")
                {
                    MessageBox.Show("Select an output file for the outputter\n");
                }
                else outputter = (IOutputter)loader.GetInstance(choice, (object)tbOutputFile.Text);                
            }
            if (cbGenerationFactory.SelectedItem != null)
            {
                string choice = getChoice(generationFactories, cbGenerationFactory);
                generationFactory = (IGenerationFactory)loader.GetInstance(choice, null);
            }
            if (errorMsg != "") MessageBox.Show(errorMsg + "Please make sure you have selected a populator, evaluator, genetic operator and terminator and then try pressing the button again\n" );
            else
            {
                displayOutputter = new DisplayOutputter(this, outputter);
                engine = new GeneticEngine(populator, evaluator, geneticOperator, terminator, displayOutputter, generationFactory);
                stepButton.Enabled = true;
                runButton.Enabled = true;
                runGenerationButton.Enabled = true;
                hasInitialised = true;
                //setFitnessValues();
            }
        }

        private void cleanupButton_Click(object sender, EventArgs e)
        {
            if (hasInitialised)
            {
                //displayOutputter.CloseOutput();
                engine.FinishOutput();
                cleanupButton.Enabled = false;
            }
        }

        public void StepEngine()
        {
            engine.Step();
        }

        /*
         * calls Step() of the engine only if it initialised. After the step method, the fitness values are updated
         * if the engine is not initialised, a message is displayed to initialise the engine.
         * If the engine is complete, convert network to xml form. 
         */ 
        private void stepButton_Click(object sender, EventArgs e)
        {
            if (hasInitialised)
            {
                //displayOutputter.OpenOutput();

                Thread stepThead = new Thread(new ThreadStart(StepEngine));
                stepThead.Start();

                //setFitnessValues();
                //displayOutputter.OutputGeneration(engine.Generation, engine.GenerationCount);
                cleanupButton.Enabled = true;
            }
            else
            {
                MessageBox.Show("Initialise Generation First\n");
            }
            if (engine.IsComplete)
            {
                hasCompleted = true;
                viewOutputFileButton.Enabled = true;
                //writeToXmlFile();
            }
        }

        public void RunEngine()
        {            
            engine.Run();
        }

        /**
         * By clicking on the run button, Run() is executed from the engine if it was initialised. Afterwards, the fitness values
         * are updated on the interface. If the engine wasn't initialised, then a message is shown to the user to initialise the engine
         * first
         * If the engine's generation is terminated, the output file viewer button is activated for the user and convert network to xml form. 
         */ 
        private void runButton_Click(object sender, EventArgs e)
        {
            if (hasInitialised)
            {
                //displayOutputter.OpenOutput();

                Thread runThead = new Thread(new ThreadStart(RunEngine));
                runThead.Start();

                //setFitnessValues();
                //displayOutputter.OutputGeneration(engine.Generation, engine.GenerationCount);
                cleanupButton.Enabled = true;
            }
            else MessageBox.Show("Initialise Generation First\n");
            if (engine.IsComplete)
            {
                hasCompleted = true;
                viewOutputFileButton.Enabled = true;
                //writeToXmlFile();
            }
        }

        public void RepeatEngine(object obj)
        {
            int n = (int)obj;
            engine.Repeat(n);
        }

        /**
         *  If the engine is initialised, the engine will call Repeat with the given n value provided by the scroller.
         *  If the value of the scroller item is null or 0 then a exception message is thrown to request the user to 
         *  choose a legitimiate value.
         *  Once repeat is called, the fitness values are recalculated and display on the interface.
         *  If the engine's generation has terminated after Repeat, then the view Output file button is activated.
         *  If the engine wasn't initialised, then the message is shown to initialise the engine before pressing the repeat button.
         *  If the engine is completed, convert network to its Xml form.
         */
        private void runGenerationButton_Click(object sender, EventArgs e)
        {
            if ((int) n.Value == 0) MessageBox.Show("Select a n value which is at least 1\n");
            else
            {
                if (hasInitialised)
                {
                    //displayOutputter.OpenOutput();
                    //engine.Repeat((int) n.Value);
                    Thread repeatThread = new Thread(RepeatEngine);
                    repeatThread.Start((int)n.Value);

                    //setFitnessValues();
                    //displayOutputter.OutputGeneration(engine.Generation, engine.GenerationCount);
                    cleanupButton.Enabled = true;
                }
                else MessageBox.Show("Initialise Generation First\n");
                if (engine.IsComplete)
                {
                    hasCompleted = true;
                    viewOutputFileButton.Enabled = true;
                    //writeToXmlFile();
                }
            }
        }

        /**
         * these values are obtained from the generation of the engine and are displayed on the interface. 
          
        private void setFitnessValues()
        {
            maxFitnessValue.Text = engine.Generation.MaxFitness.ToString();
            averageFitnessValue.Text = engine.Generation.AverageFitness.ToString();
        }
         */
        #endregion
        #region File Loading


        OpenFileDialog OpenOutput = new OpenFileDialog();
        OpenFileDialog LoadLibrary = new OpenFileDialog();
        OpenFileDialog OpenMap = new OpenFileDialog();
        /**
         * Check if the file is successfully chosen and opened. 
         */ 
        private bool isOK(OpenFileDialog openDialog, string type)
        {
            switch (type)
            {
                case "map":
                    if (!String.IsNullOrEmpty(tbMapFile.Text))
                        openDialog.InitialDirectory = Path.GetDirectoryName(tbMapFile.Text);
                    break;
                case "library":
                    if (!String.IsNullOrEmpty(fullDLLPath))
                        openDialog.InitialDirectory = Path.GetDirectoryName(fullDLLPath);
                    break;
                case "output":
                    if (!String.IsNullOrEmpty(tbOutputFile.Text))
                        openDialog.InitialDirectory = Path.GetDirectoryName(tbOutputFile.Text);
                    break;
            }
            return (openDialog.ShowDialog() == DialogResult.OK);
        }

        /**
         * Load map file, if the file was successfully loaded by the OpenFileDialog Object 
         */
        private void loadMapFile()
        {
            if (isOK(OpenMap,"map"))
            {
                tbMapFile.Text = OpenMap.FileName;
                string e = Path.GetExtension(tbMapFile.Text);
                if (String.Equals(e, ".xml"))
                {
                    //XmlTextReader reader = new XmlTextReader(tbMapFile.Text);
                    //map = new Map(reader);
                    map = Map.FromFile(tbMapFile.Text);
                    visualiser1.Network = new RoadNetwork(map);
                    //displayOutputter = new DisplayOutputter(visualiser1, outputter);                    
                }
                else
                {
                    MessageBox.Show("Map file should be in xml form. Please reload another file.\n");
                }
            }
            else if (!isOK(OpenMap,"map") && String.IsNullOrEmpty(tbMapFile.Text))
            {
                MessageBox.Show("Select a file so a map can be created on the visualiser\n");
            }
        }

        /**
         * Load output file, if the file was successfully loaded by the OpenFileDialog object 
         * otherwise select a new directory to create an output file
         */
        private void loadOutputFile()
        {
            if (isOK(OpenOutput,"output"))
            {
                    tbOutputFile.Text = OpenOutput.FileName;
                    string e = Path.GetExtension(tbOutputFile.Text);
                    if (!String.Equals(e, ".xml"))
                    {
                        MessageBox.Show("Output file should be in xml form. Please reload another file\n");
                    }
            }
        }

        /**
         * Populate the drop down list if each string list has at least one member. 
         * Display an error msg, if the populator, evaluator, geneticOperator and Terminator, string lists are empty. 
         * The reason here is, that no populator, evaluator, geneticOperator or Terminator should be null when we 
         * create the engine object
         */
        private void initPluginDropDowns()
        {
            String errorMsg = "";
            if (populators.Count > 0)
            {
                for (int i = 0; i < populators.Count; i++)
                {
                    cbPopulator.Items.Add(populators[i]);
                }
            }
            else errorMsg += ("No known populators can be loaded. Load another dll file\n");
            if (evaluators.Count > 0)
            {
                for (int i = 0; i < evaluators.Count; i++)
                {
                    cbEvaluator.Items.Add(evaluators[i]);
                }
            }
            else errorMsg += ("No known evaluators can be loaded. Load another dll file\n");
            if (geneticOperators.Count > 0)
            {
                for (int i = 0; i < geneticOperators.Count; i++)
                {
                    cbGeneticOperator.Items.Add(geneticOperators[i]);
                }
            }
            else errorMsg += ("No known genetic operators can be loaded. Load another dll file\n");
            if (terminators.Count > 0)
            {
                for (int i = 0; i < terminators.Count; i++)
                {
                    cbTerminator.Items.Add(terminators[i]);
                }
            }
            else errorMsg += ("No known terminators can be loaded. \n");
            if (outputters.Count > 0)
            {
                for (int i = 0; i < outputters.Count; i++)
                {
                    cbOutputter.Items.Add(outputters[i]);
                }
            }
            if (generationFactories.Count > 0)
            {
                for (int i = 0; i < generationFactories.Count; i++)
                {
                    cbGenerationFactory.Items.Add(generationFactories[i]);
                }
            }
            if (errorMsg != "") MessageBox.Show(errorMsg);
        }
        /**
         * Populate the 'n'scroller and the target fitness scroller with values from 1 up to the maximum values
         * Furthermore deactivate all of the buttons from the user
         */ 
        private void initComponents()
        {
            initEngineButton.Enabled = false;
            runButton.Enabled = false;
            runGenerationButton.Enabled = false;
            stepButton.Enabled = false;
            viewOutputFileButton.Enabled = false;
            cleanupButton.Enabled = false;
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
         * If the generation has been completed and once the view Output file Button is clicked,
         * Close the current interface and open up the Visualiser interface with the current file. 
         */ 
        private void viewOutputFileButton_Click(object sender, EventArgs e)
        {
            if (hasCompleted)
            {
                this.Dispose(false);
                Road_Network_Visualiser form = new Road_Network_Visualiser(true,tbOutputFile.Text);
                form.Visible = true;
            }
        }

        
        /**
        * Check if a file has been opened
        * If not, display an error message
        * otherwise check if the file is an xml one.
        * If not, display a different error message
        * otherwise, create instance of an XmlWriter and write xml stuff to it.  
        
        private void writeToXmlFile()
        {
            RoadNetwork network = new RoadNetwork(visualiser1.Network);
            if (tbOutputFile.Text == "")
            {
                MessageBox.Show("Should have opened up an XML file by now\n");
            }
            string e = Path.GetExtension(tbOutputFile.Text);
            if (e != ".xml")
            {
                MessageBox.Show("File is not a XML File\n");
            }
            else
            {
                XmlTextWriter writer = new XmlTextWriter(tbOutputFile.Text, Encoding.ASCII);
                network.WriteXml(writer);
            }
        }

        */

        private void tbOutputFile_TextChanged(object sender, EventArgs e)
        {

        }

        private delegate void ShowStatsCallback(uint maxFitness, float averageFitness);

        private void ShowStats(uint maxFitness, float averageFitness)
        {
            maxFitnessValue.Text = maxFitness.ToString();
            averageFitnessValue.Text = averageFitness.ToString();
        }

        public void DisplayGeneration(IGeneration generation)
        {
            if (generation.Count > 0)
            {
                visualiser1.Network = (RoadNetwork)generation[0].Individual;                
            }

            this.Invoke(new ShowStatsCallback(this.ShowStats),new object[]{generation.MaxFitness,generation.AverageFitness});
        }

    }
 }
