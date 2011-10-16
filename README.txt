-----------------------------------README FILE OF THE GENETIC ENGINE PROJECT ----------------------------------------------
Team
Rohit Gopalan - UI Developer/Project Leader
Brian Marshall - API Developer
John Hodge - API Developer
Alwyn Kyi - Tester
Antriksh Srivastava - Tester

Project Overview
The project is all about creating an Application Programming Interface (which is termed a Genetic Engine) 
for implementing Genetic Algorithms. This Engine, will have access to all of the interface classes that represent 
the populator, evaluator, terminator, genetic operator, outputter and the generation factory. This engine also 
includes classes that read from and write to XML files. The project also has files which creates the visual 
interpretation of the main functions as well as the test classes which test all of the functions in this Engine.
More about this later. 

Project Source Directory Layout
All of the Code is going to be in the GeneticEnginePrototype Folder. There is another folder which is the examplexml
folder which stores all of the XML files that are used for loading by the Interface programs in the code directory. 
These are the main directories within DelC_GroupJ.zip folder.
In the GeneticEnginePrototype folder, the 6 sub-folders are
	i. GeneticEngineCore
	ii. GeneticEnginePlugin
	iii. RoadNetworkDisplay
	iv. RoadNetworkGUI
	v. RoadNetworkSolver
	vi. GeneticEngineTesting

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