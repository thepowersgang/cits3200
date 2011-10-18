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
        /// <summary>
        /// Flags to denote whether is engine has been initialised or completed
        /// </summary>
        private bool hasInitialised, hasCompleted;
        /// <summary>
        /// A flag to denote whether the engine is running or not
        /// </summary>
        private bool engineRunning;
        /// <summary>
        /// A string to specify the path of the currently loaded .dll file
        /// </summary>
        private string fullDLLPath = "";
        private IPopulator populator;
        private IEvaluator evaluator;
        private IGeneticOperator geneticOperator;
        private ITerminator terminator;
        private IGenerationFactory generationFactory;
        private PluginLoader loader;
        private GeneticEngine engine;
        /// <summary>
        /// Various lists of strings used to populate checkboxes. 
        /// </summary>
        private List<string> populators, evaluators, geneticOperators, terminators, generationFactories, outputters;
        private Map map;
        private DisplayOutputter displayOutputter;
        private IOutputter outputter;
        /// <summary>
        /// If no outputter method is provided by the .dll file, specify a default output option
        /// </summary>
        private string noOutputterString = "[none]";
        /// <summary>
        /// If no generation factory is provided by the .dll file, specify a default option
        /// </summary>
        private string defaultGenerationFactoryString = "[default]";

        /// <summary>
        /// Initialise a new RoadNetworkFinder
        /// </summary>
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
        /// <summary>
        /// Load a .dll file and get all the various 'options' of each plugin type if the loading of the plugin was successful. 
        /// Then load the dropdownlists.
        /// </summary>
        private void libraryLoaderButton_Click(object sender, EventArgs e)
        {
            LoadLibrary.Title = "Select A Plug-in Library";
            LoadLibrary.Filter = "Libraries (*.DLL)|*.dll";
            LoadLibrary.FilterIndex = 0;
            if (!String.IsNullOrWhiteSpace(fullDLLPath))
            {
                LoadLibrary.InitialDirectory = Path.GetDirectoryName(fullDLLPath);
            }

            if (LoadLibrary.ShowDialog() == DialogResult.OK)
            {
                loader = new PluginLoader();
                try
                {
                    loader.LoadDll(LoadLibrary.FileName);
                }
                catch (GeneticEngineException exception)
                {
                    MessageBox.Show(exception.Message);
                }

                fullDLLPath = LoadLibrary.FileName;
                libraryLabel.Text = Path.GetFileName(LoadLibrary.FileName);
                initEngineButton.Enabled = true;
                try
                {
                    populators = loader.GetPluginNames(typeof(IPopulator));
                }
                catch (GeneticEngineException exception)
                {
                    MessageBox.Show(exception.Message);
                }

                try
                {
                    evaluators = loader.GetPluginNames(typeof(IEvaluator));
                }
                catch (GeneticEngineException exception)
                {
                    MessageBox.Show(exception.Message);
                }
                try
                {
                    geneticOperators = loader.GetPluginNames(typeof(IGeneticOperator));
                }
                catch (GeneticEngineException exception)
                {
                    MessageBox.Show(exception.Message);
                }
                try
                {
                    terminators = loader.GetPluginNames(typeof(ITerminator));
                }
                catch (GeneticEngineException exception)
                {
                    MessageBox.Show(exception.Message);
                }
          
                outputters = loader.GetPluginNames(typeof(IOutputter));
                outputters.Add(noOutputterString);
                generationFactories = loader.GetPluginNames(typeof(IGenerationFactory));
                generationFactories.Add(defaultGenerationFactoryString);
                initPluginDropDowns();
            }
        }

        private void MapFileSelectButton_Click(object sender, EventArgs e)
        {
            loadMapFile();
        }

        private void outputFileSelectButton_Click(object sender, EventArgs e)
        {
            selectOutputFile();
        }
        
        /// <summary>
        ///Check if the plugins have been selected by the user, if not the revelant exceptions are thrown 
        ///asking the user to make a choice.
        ///So, if the populator,evaluators, geneticOperator and terminator choices are not chosen, then 
        ///error messages are displayed to alert the user, that the choices for these four plugins cannot be null.
        ///If the plugins are chosen, the engine object is created according to the user choices and it is initialised.
        ///Once the engine is initialised, the rest of the buttons on the interface are activated and the fitness values
        ///are displayed. 
        /// </summary>
        private void initEngineButton_Click(object sender, EventArgs e)
        {
            if (!engineRunning)
            {
                SetEngineRunning(true);

                populator = null;
                evaluator = null;
                geneticOperator = null;
                terminator = null;
                outputter = null;
                generationFactory = null;

                string errorMsg = "";
                if (cbPopulator.SelectedItem == null) errorMsg += "Populator must not be null\n";
                else
                {
                    string choice = getChoice(populators, cbPopulator);
                    try
                    {
                        populator = (IPopulator)loader.GetInstance(choice, (object)tbMapFile.Text);
                    }
                    catch (GeneticEngineException exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
                if (cbEvaluator.SelectedItem == null) errorMsg += "Evaluator must not be null\n";
                else
                {
                    string choice = getChoice(evaluators, cbEvaluator);
                    try
                    {
                        evaluator = (IEvaluator)loader.GetInstance(choice, null);
                    }
                    catch (GeneticEngineException exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
                if (cbGeneticOperator.SelectedItem == null) errorMsg += "Genetic Operator must not be null\n";
                else
                {
                    string choice = getChoice(geneticOperators, cbGeneticOperator);
                    try
                    {
                        geneticOperator = (IGeneticOperator)loader.GetInstance(choice, null);
                    }
                    catch (GeneticEngineException exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
                if (cbTerminator.SelectedItem == null) errorMsg += "Terminator must not be null\n";
                else
                {
                    string choice = getChoice(terminators, cbTerminator);
                    if ((int)targetFitness.Value == 0) errorMsg += "Provide a target fitness value greater than 1 for the terminator plug-in\n";
                    else
                    {
                        try
                        {
                            terminator = (ITerminator)loader.GetInstance(choice, (object)(uint)targetFitness.Value);
                        }
                        catch (GeneticEngineException exception)
                        {
                            MessageBox.Show(exception.Message);
                        }
                    }
                }
                if (cbOutputter.SelectedItem != null)
                {
                    string choice = getChoice(outputters, cbOutputter);
                    if (choice != noOutputterString)
                    {
                        if (tbOutputFile.Text == "")
                        {
                            MessageBox.Show("Select an output file for the outputter\n");
                        }
                        else
                        {
                            outputter = (IOutputter)loader.GetInstance(choice, (object)tbOutputFile.Text);
                        }
                    }
                }
                if (cbGenerationFactory.SelectedItem != null)
                {
                    string choice = getChoice(generationFactories, cbGenerationFactory);
                    if (choice != defaultGenerationFactoryString)
                    {
                        generationFactory = (IGenerationFactory)loader.GetInstance(choice, null);
                    }
                }
                if (errorMsg != "") MessageBox.Show(errorMsg + "Please make sure you have selected a populator, evaluator, genetic operator and terminator and then try pressing the button again\n");
                else
                {
                    try
                    {
                        displayOutputter = new DisplayOutputter(this, outputter);
                        engine = new GeneticEngine(populator, evaluator, geneticOperator, terminator, displayOutputter, generationFactory);
                        hasInitialised = true;
                    }
                    catch (GeneticEngineException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                SetEngineRunning(false);
            }
        }
         /// <summary>
         /// if the Engine is initialised and it is not running, deactivate buttons, and finish outputting generations to a file.
         /// Then activate the buttons. Only activate the View Output File button if the output file exists. 
         /// </summary>
        private void cleanupButton_Click(object sender, EventArgs e)
        {
            if (hasInitialised && !engineRunning)
            {
                try
                {
                    SetEngineRunning(true);
                    engine.FinishOutput();
                    SetEngineRunning(false);
                    hasCompleted = true;
                }
                catch (GeneticEngineException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                if (File.Exists(tbOutputFile.Text))
                {
                    viewOutputFileButton.Enabled = true;
                }
                else
                {
                    viewOutputFileButton.Enabled = false;
                }
            }
        }

        ///<summary>
        ///calls Step() of the engine only if it initialised. After the step method, the fitness values are updated
        ///if the engine is not initialised, a message is displayed to initialise the engine.
        /// If the engine is complete, convert network to xml form. 
        ///</summary> 
        private void stepButton_Click(object sender, EventArgs e)
        {
            if (hasInitialised && !engineRunning)
            {
                try
                {
                    SetEngineRunning(true);
                    engine.Step();
                    SetEngineRunning(false);
                }
                catch (GeneticEngineException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            else
            {
                MessageBox.Show("Initialise Generation First\n");
            }

        }

        /// <summary>
        /// Thread entrypoint for running the genetic engine 
        /// until the termination condition is met
        /// </summary>
        public void RunEngine()
        {            
            engine.Run();
            SetEngineRunning(false);
        }

        ///<summary>
        ///By clicking on the run button, Run() is executed from the engine if it was initialised. Afterwards, the fitness values
        ///are updated on the interface. If the engine wasn't initialised, then a message is shown to the user to initialise the engine
        ///first
        ///If the engine's generation is terminated, the output file viewer button is activated for the user and convert network to xml form. 
        ///</summary> 
        private void runButton_Click(object sender, EventArgs e)
        {
            if (hasInitialised)
            {
                if (!engineRunning)
                {
                    try
                    {
                        SetEngineRunning(true);
                        Thread runThead = new Thread(new ThreadStart(RunEngine));
                        runThead.Start();
                    }
                    catch (GeneticEngineException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else MessageBox.Show("Initialise Generation First\n");

        }

        /// <summary>
        /// Thread entrypoint for running the genetic engine 
        /// for n generations
        /// </summary>
        /// <param name="obj">An integer giving the number of generations to run.</param>
        public void RepeatEngine(object obj)
        {
            int n = (int)obj;
            engine.Repeat(n);
            SetEngineRunning(false);
        }

        ///<summary>
        ///  If the engine is initialised, the engine will call Repeat with the given n value provided by the scroller.
        /// If the value of the scroller item is null or 0 then a exception message is thrown to request the user to 
        /// choose a legitimiate value.
        /// Once repeat is called, the fitness values are recalculated and display on the interface.
        /// If the engine's generation has terminated after Repeat, then the view Output file button is activated.
        /// If the engine wasn't initialised, then the message is shown to initialise the engine before pressing the repeat button.
        ///  If the engine is completed, convert network to its Xml form.
        ///</summary>
        private void runGenerationButton_Click(object sender, EventArgs e)
        {
            if ((int) n.Value == 0) MessageBox.Show("Select a n value which is at least 1\n");
            else
            {
                if (hasInitialised)
                {
                    if (!engineRunning)
                    {
                        try
                        {
                            SetEngineRunning(true);
                            Thread repeatThread = new Thread(RepeatEngine);
                            repeatThread.Start((int)n.Value);
                        }
                        catch(GeneticEngineException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    cleanupButton.Enabled = true;
                }
                else MessageBox.Show("Initialise Generation First\n");

            }
        }
                
        #endregion
        #region File Loading


        OpenFileDialog SelectOutput = new OpenFileDialog();
        OpenFileDialog LoadLibrary = new OpenFileDialog();
        OpenFileDialog OpenMap = new OpenFileDialog();

        /// <summary>
        /// Load map file, if the file was successfully loaded by the OpenFileDialog Object.
        /// Check if it is an XML file. If not, ask user to select another file.  
        /// If a file is not selected, then the user is notified to select a map file. 
        /// </summary>
        private void loadMapFile()
        {
            OpenMap.Title = "Select Map File";
            OpenMap.Filter = "XML Files (*.xml)|*.xml";
            OpenMap.FilterIndex = 0;
            if (!String.IsNullOrWhiteSpace(tbMapFile.Text))
            {
                OpenMap.InitialDirectory = Path.GetDirectoryName(tbMapFile.Text);
            }

            if (OpenMap.ShowDialog() == DialogResult.OK)
            {
                tbMapFile.Text = OpenMap.FileName;
                string e = Path.GetExtension(tbMapFile.Text);
                if (String.Equals(e, ".xml"))
                {
                    map = Map.FromFile(tbMapFile.Text);
                    visualiser1.Network = new RoadNetwork(map);                   
                }
                else
                {
                    MessageBox.Show("Map file should be in xml form. Please select another file.\n");
                }
            }
            else if (OpenMap.ShowDialog() != DialogResult.OK && String.IsNullOrEmpty(tbMapFile.Text))
            {
                MessageBox.Show("Select a file so a map can be created on the visualiser\n");
            }
        }

        ///<summary>
        ///Load output file, if the file was successfully loaded by the OpenFileDialog object 
        /// otherwise select a new directory to create an output file.
        /// Check the file is an XML file. If not, report an error message to the user. 
        ///</summary>
        private void selectOutputFile()
        {
            SelectOutput.Title = "Select Output File";
            SelectOutput.Filter = "XML Files (*.xml)|*.xml";
            SelectOutput.FilterIndex = 0;
            SelectOutput.CheckFileExists = false;
            if (!String.IsNullOrWhiteSpace(tbOutputFile.Text))
            {
                SelectOutput.InitialDirectory = Path.GetDirectoryName(tbOutputFile.Text);
            }

            if (SelectOutput.ShowDialog() == DialogResult.OK)
            {
                    tbOutputFile.Text = SelectOutput.FileName;
                    string e = Path.GetExtension(tbOutputFile.Text);
                    if (!String.Equals(e, ".xml"))
                    {
                        MessageBox.Show("Output file should be an xml file.\n");
                    }
            }
        }

        /// <summary>
        /// Use the list of strings to populate a comboBox. 
        /// Automtatically show the first option from the combobox to the user. 
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="items"></param>
        private void PopulateComboBox(ComboBox comboBox, List<string> items)
        {
            object selectedItem = comboBox.SelectedItem;
            
            comboBox.Items.Clear();
            for (int i = 0; i < items.Count(); i++)
            {
                comboBox.Items.Add(items[i]);
            }

            if (selectedItem != null)
            {
                comboBox.SelectedItem = selectedItem;
            }

            if (comboBox.SelectedItem == null)
            {
                comboBox.SelectedIndex = 0;
            }

        }

        ///
        /// Populate the drop down list if each string list has at least one member. 
        /// Display an error msg, if the populator, evaluator, geneticOperator and Terminator, string lists are empty. 
        /// The reason here is, that no populator, evaluator, geneticOperator or Terminator should be null when we 
        /// create the engine object
        ///
        private void initPluginDropDowns()
        {
            String errorMsg = "";
            if (populators.Count > 0)
            {
                PopulateComboBox(cbPopulator, populators);
            }
            else errorMsg += ("No populators found in library. Please load another dll file.\n");
            if (evaluators.Count > 0)
            {
                PopulateComboBox(cbEvaluator, evaluators);
            }
            else errorMsg += ("No known evaluators can be loaded. Load another dll file\n");
            if (geneticOperators.Count > 0)
            {
                PopulateComboBox(cbGeneticOperator, geneticOperators);
            }
            else errorMsg += ("No known genetic operators can be loaded. Load another dll file\n");
            if (terminators.Count > 0)
            {
                PopulateComboBox(cbTerminator, terminators);
            }
            else errorMsg += ("No known terminators can be loaded. \n");
            if (outputters.Count > 0)
            {
                PopulateComboBox(cbOutputter, outputters);
            }
            if (generationFactories.Count > 0)
            {
                PopulateComboBox(cbGenerationFactory, generationFactories);
            }
            if (errorMsg != "") MessageBox.Show(errorMsg);
        }
    
        /// <summary>
        /// Deactivate all the buttons from the user until the dll plugins are properly loaded. 
        /// </summary>
        private void initComponents()
        {
            initEngineButton.Enabled = false;
            runButton.Enabled = false;
            runGenerationButton.Enabled = false;
            stepButton.Enabled = false;
            viewOutputFileButton.Enabled = false;
            cleanupButton.Enabled = false;
        }
        ///
        ///Based on the selected index from a particular combo box, obtain the string from the index of the specified list.
        /// This is used for getInstance() of the engine. 
        /// 
        private string getChoice(List<string> list, ComboBox cb)
        {
            return list[cb.SelectedIndex];
        }
        #endregion

        ///
        /// If the generation has been completed and once the view Output file Button is clicked,
        /// Close the current interface and open up the Visualiser interface with the current file. 
        /// 
        private void viewOutputFileButton_Click(object sender, EventArgs e)
        {
            if (hasCompleted && File.Exists(tbOutputFile.Text))
            {
                this.Dispose(false);
                Road_Network_Visualiser form = new Road_Network_Visualiser(true,tbOutputFile.Text);
                form.Visible = true;
            }
        }

        
        private delegate void ShowStatsCallback(uint maxFitness, float averageFitness);

        /// <summary>
        /// Display the statistics on the visualiser. 
        /// </summary>
        /// <param name="maxFitness">Max fitness value to display</param>
        /// <param name="averageFitness">Average Fitness value to display</param>
        private void ShowStats(uint maxFitness, float averageFitness)
        {
            maxFitnessValue.Text = maxFitness.ToString();
            averageFitnessValue.Text = averageFitness.ToString();
        }

        /// <summary>
        /// Show the best individual from the current generation visually and their statistics. 
        /// </summary>
        /// <param name="generation">Generation to be displayed</param>
        public void DisplayGeneration(IGeneration generation)
        {
            if (!this.IsDisposed)
            {
                if (generation.Count > 0)
                {
                    visualiser1.Network = (RoadNetwork)generation[0].Individual;
                }

                if (this.InvokeRequired)
                {
                    this.Invoke(new ShowStatsCallback(this.ShowStats), new object[] { generation.MaxFitness, generation.AverageFitness });
                }
                else
                {
                    ShowStats(generation.MaxFitness, generation.AverageFitness);
                }
            }
        }

        /// <summary>
        /// Stop the engine once the button is clicked 
        /// </summary>
        private void stopProcessButton_Click(object sender, EventArgs e)
        {
            engine.Stop();
        }

        private delegate void SetEngineRunningCallback(bool engineRunning);

        /// <summary>
        /// Activate or deactivate the buttons depending if the engine is running on the GUI
        /// </summary>
        /// <param name="engineRunning">A flag to specify whether the engine is running</param>
        private void SetEngineRunning(bool engineRunning) {

            if (!this.IsDisposed)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new SetEngineRunningCallback(SetEngineRunning), new object[] { engineRunning });
                }
                else
                {
                    this.engineRunning = engineRunning;

                    if (engineRunning)
                    {
                        initEngineButton.Enabled = false;
                        stepButton.Enabled = false;
                        runButton.Enabled = false;
                        runGenerationButton.Enabled = false;
                        cleanupButton.Enabled = false;
                        libraryLoaderButton.Enabled = false;
                        MapFileSelectButton.Enabled = false;
                        outputFileButton.Enabled = false;
                        stopProcessButton.Enabled = true;
                    }
                    else
                    {
                        initEngineButton.Enabled = true;
                        libraryLoaderButton.Enabled = true;
                        MapFileSelectButton.Enabled = true;
                        outputFileButton.Enabled = true;
                        if (hasInitialised)
                        {
                            stepButton.Enabled = true;
                            runButton.Enabled = true;
                            runGenerationButton.Enabled = true;
                            cleanupButton.Enabled = true;
                        }

                        stopProcessButton.Enabled = false;
                    }
                }
            }
        }
                
        /// <summary>
        /// If the engine is still running tell it to stop.
        /// </summary>
        private void RoadNetworkFinder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (engineRunning)
            {
                engine.Stop();
            }
        }
    }
 }
