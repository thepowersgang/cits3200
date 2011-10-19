-----------------------------------README FILE OF THE GENETIC ENGINE PROJECT ----------------------------------------------
Team
Rohit Gopalan - UI Developer/Project Leader
Brian Marshall - API Developer
John Hodge - API Developer
Alwyn Kyi - Tester
Antriksh Srivastava - Tester


Project Overview

The core of the project is a .Net API (Application Programming Interface) which provides a framework for programming
genetic algorithms. This is build around the GeneticEngine class which accepts plug-in classes defining the various
parts of the algorithm:
  - Populating the initial set of individuals
  - Evaluating each individual (fitness function)
  - Determining if the algorithm is finished (termination condition)
  - Using the individuals of the current generation (and their fitness values) to produce the next set of individuals.
  - Outputting the individuals, fitness values and generation statistics.

The PluginLoader allows plug-in classes to be loaded from user selected class libraries (.dll files) at runtime.

There are also a number of support classes to facilitate the rapid development of plug-ins.

The primary goal of the project is flexibility. To this end the GeneticEngine allows any class to be used to represent
individuals. The GeneticEngine only deals with individuals as instances of "object". It is up to the plug-ins how these
are to be interpreted or manipulated. A default generation class is supplied which will hold the individuals sorted by
their fitness values. This should be flexible enough for most purposes but it is also possible to override this by
supplying an alternative generation class.

In addition to the core API there is a set of example plug-ins. When used with the GeneticEngine these attempt to
optimize a network of roads based on a number of constraints.

Finally, there is a GUI for loading the example plugins, running the GeneticEngine, and displaying the results.


Source Directory Layout

The overall project is divided into 7 C# "projects" Each resides in its own project directory. They are all managed
within one Visual Studio "solution" (GeneticEngine.sln).
  - GeneticEngineCore: Contains the GeneticEngine and PluginLoader classes.
  - GeneticEnginePlugin: Contains the Plug-in interfaces which plug-in classes must implement along with the support
                         classes.
  - RoadNetwork: Contains the RoadNetwork class which is used as the individuals in the sample plug-ins.
  - RoadNetworkSolver: Contains the sample plug-ins.
  - RoadNetworkDisplay: Contains a custom control which can be used in GUI projects to display RoadNetworks
  - RoadNetworkGUI: Contains the GUI tools 
  - GeneticEngineTesting: Contains the unit tests for the entire solution.

How to run the program 
Installation : refer to INSTALL.txt
To run the tests and run the visual interfaces
1. Right click on the Project GeneticEngineTesting and select 'Set as StartUp Project'
2. Select F5 to debug the solution (which will build the entire solution as well) and run the unit tests. 
   The tests are run from TestClass.cs
3. Once the unit tests are completed, right click on the project RoadNetworkGUI and select 'Set as StartUp Project'
4. Select F5 to debug and build the entire solution. Upon successfully building the project, the RoadNetworkFinder 
   Interface loads.
5. Once the RoadNetworkFinder interface is closed, the RoadNetworkVisualiser Interface begins to load. 
6. For a guide on how to use the two interfaces, please refer to the GUI User Manual
-------------------------------------------------------------------------------------------------------------------