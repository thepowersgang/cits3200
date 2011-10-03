using System.IO;
using System.Windows.Forms;
namespace RoadNetworkGUI
{
    partial class RoadNetworkFinder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.populatorLabel = new System.Windows.Forms.Label();
            this.cbPopulator = new System.Windows.Forms.ComboBox();
            this.cbEvaluator = new System.Windows.Forms.ComboBox();
            this.cbGeneticOperator = new System.Windows.Forms.ComboBox();
            this.cbTerminator = new System.Windows.Forms.ComboBox();
            this.cbOutputter = new System.Windows.Forms.ComboBox();
            this.cbGenerationFactory = new System.Windows.Forms.ComboBox();
            this.evaluatorLabel = new System.Windows.Forms.Label();
            this.geneticOperatorLabel = new System.Windows.Forms.Label();
            this.TerminatorLabel = new System.Windows.Forms.Label();
            this.OutputterLabel = new System.Windows.Forms.Label();
            this.GenerationFactoryLabel = new System.Windows.Forms.Label();
            this.libraryLabel = new System.Windows.Forms.Label();
            this.gbMap = new System.Windows.Forms.GroupBox();
            this.visualiser1 = new RoadNetworkDisplay.RoadNetworkPanel();
            this.gbPlugins = new System.Windows.Forms.GroupBox();
            this.libraryLoaderButton = new System.Windows.Forms.Button();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.viewOutputFileButton = new System.Windows.Forms.Button();
            this.nScroller = new System.Windows.Forms.DomainUpDown();
            this.nlabel = new System.Windows.Forms.Label();
            this.runGenerationButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.stepButton = new System.Windows.Forms.Button();
            this.initEngineButton = new System.Windows.Forms.Button();
            this.outputFileSelectButton = new System.Windows.Forms.Button();
            this.tbOutputFile = new System.Windows.Forms.TextBox();
            this.outputFileButton = new System.Windows.Forms.Label();
            this.targetFitnessScroller = new System.Windows.Forms.DomainUpDown();
            this.TargetFitnessLabel = new System.Windows.Forms.Label();
            this.MapFileSelectButton = new System.Windows.Forms.Button();
            this.tbMapFile = new System.Windows.Forms.TextBox();
            this.MapFileLabel = new System.Windows.Forms.Label();
            this.gbGeneration = new System.Windows.Forms.GroupBox();
            this.averageFitnessValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.maxFitnessValue = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbMap.SuspendLayout();
            this.gbPlugins.SuspendLayout();
            this.gbSettings.SuspendLayout();
            this.gbGeneration.SuspendLayout();
            this.SuspendLayout();
            // 
            // populatorLabel
            // 
            this.populatorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.populatorLabel.Location = new System.Drawing.Point(46, 61);
            this.populatorLabel.Name = "populatorLabel";
            this.populatorLabel.Size = new System.Drawing.Size(74, 23);
            this.populatorLabel.TabIndex = 2;
            this.populatorLabel.Text = "Populator";
            // 
            // cbPopulator
            // 
            this.cbPopulator.FormattingEnabled = true;
            this.cbPopulator.Location = new System.Drawing.Point(245, 63);
            this.cbPopulator.Name = "cbPopulator";
            this.cbPopulator.Size = new System.Drawing.Size(144, 21);
            this.cbPopulator.TabIndex = 3;
            // 
            // cbEvaluator
            // 
            this.cbEvaluator.FormattingEnabled = true;
            this.cbEvaluator.Location = new System.Drawing.Point(245, 90);
            this.cbEvaluator.Name = "cbEvaluator";
            this.cbEvaluator.Size = new System.Drawing.Size(144, 21);
            this.cbEvaluator.TabIndex = 4;
            // 
            // cbGeneticOperator
            // 
            this.cbGeneticOperator.FormattingEnabled = true;
            this.cbGeneticOperator.Location = new System.Drawing.Point(245, 121);
            this.cbGeneticOperator.Name = "cbGeneticOperator";
            this.cbGeneticOperator.Size = new System.Drawing.Size(145, 21);
            this.cbGeneticOperator.TabIndex = 5;
            // 
            // cbTerminator
            // 
            this.cbTerminator.FormattingEnabled = true;
            this.cbTerminator.Location = new System.Drawing.Point(245, 147);
            this.cbTerminator.Name = "cbTerminator";
            this.cbTerminator.Size = new System.Drawing.Size(144, 21);
            this.cbTerminator.TabIndex = 6;
            // 
            // cbOutputter
            // 
            this.cbOutputter.FormattingEnabled = true;
            this.cbOutputter.Location = new System.Drawing.Point(245, 178);
            this.cbOutputter.Name = "cbOutputter";
            this.cbOutputter.Size = new System.Drawing.Size(145, 21);
            this.cbOutputter.TabIndex = 7;
            // 
            // cbGenerationFactory
            // 
            this.cbGenerationFactory.FormattingEnabled = true;
            this.cbGenerationFactory.Location = new System.Drawing.Point(246, 205);
            this.cbGenerationFactory.Name = "cbGenerationFactory";
            this.cbGenerationFactory.Size = new System.Drawing.Size(144, 21);
            this.cbGenerationFactory.TabIndex = 8;
            // 
            // evaluatorLabel
            // 
            this.evaluatorLabel.AutoSize = true;
            this.evaluatorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.evaluatorLabel.Location = new System.Drawing.Point(46, 90);
            this.evaluatorLabel.Name = "evaluatorLabel";
            this.evaluatorLabel.Size = new System.Drawing.Size(68, 17);
            this.evaluatorLabel.TabIndex = 9;
            this.evaluatorLabel.Text = "Evaluator";
            // 
            // geneticOperatorLabel
            // 
            this.geneticOperatorLabel.AutoSize = true;
            this.geneticOperatorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.geneticOperatorLabel.Location = new System.Drawing.Point(46, 125);
            this.geneticOperatorLabel.Name = "geneticOperatorLabel";
            this.geneticOperatorLabel.Size = new System.Drawing.Size(118, 17);
            this.geneticOperatorLabel.TabIndex = 10;
            this.geneticOperatorLabel.Text = "Genetic Operator";
            // 
            // TerminatorLabel
            // 
            this.TerminatorLabel.AutoSize = true;
            this.TerminatorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TerminatorLabel.Location = new System.Drawing.Point(46, 151);
            this.TerminatorLabel.Name = "TerminatorLabel";
            this.TerminatorLabel.Size = new System.Drawing.Size(77, 17);
            this.TerminatorLabel.TabIndex = 11;
            this.TerminatorLabel.Text = "Terminator";
            // 
            // OutputterLabel
            // 
            this.OutputterLabel.AutoSize = true;
            this.OutputterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputterLabel.Location = new System.Drawing.Point(46, 182);
            this.OutputterLabel.Name = "OutputterLabel";
            this.OutputterLabel.Size = new System.Drawing.Size(68, 17);
            this.OutputterLabel.TabIndex = 12;
            this.OutputterLabel.Text = "Outputter";
            // 
            // GenerationFactoryLabel
            // 
            this.GenerationFactoryLabel.AutoSize = true;
            this.GenerationFactoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerationFactoryLabel.Location = new System.Drawing.Point(46, 209);
            this.GenerationFactoryLabel.Name = "GenerationFactoryLabel";
            this.GenerationFactoryLabel.Size = new System.Drawing.Size(130, 17);
            this.GenerationFactoryLabel.TabIndex = 13;
            this.GenerationFactoryLabel.Text = "Generation Factory";
            // 
            // libraryLabel
            // 
            this.libraryLabel.AutoSize = true;
            this.libraryLabel.Location = new System.Drawing.Point(114, 25);
            this.libraryLabel.Name = "libraryLabel";
            this.libraryLabel.Size = new System.Drawing.Size(33, 13);
            this.libraryLabel.TabIndex = 14;
            this.libraryLabel.Text = "None";
            // 
            // gbMap
            // 
            this.gbMap.Controls.Add(this.visualiser1);
            this.gbMap.Location = new System.Drawing.Point(425, 13);
            this.gbMap.Name = "gbMap";
            this.gbMap.Size = new System.Drawing.Size(592, 472);
            this.gbMap.TabIndex = 16;
            this.gbMap.TabStop = false;
            this.gbMap.Text = "Current Best";
            // 
            // visualiser1
            // 
            this.visualiser1.BackColor = System.Drawing.Color.White;
            this.visualiser1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.visualiser1.Location = new System.Drawing.Point(20, 21);
            this.visualiser1.Name = "visualiser1";
            this.visualiser1.Network = null;
            this.visualiser1.Size = new System.Drawing.Size(550, 430);
            this.visualiser1.TabIndex = 1;
            // 
            // gbPlugins
            // 
            this.gbPlugins.Controls.Add(this.libraryLoaderButton);
            this.gbPlugins.Controls.Add(this.cbPopulator);
            this.gbPlugins.Controls.Add(this.GenerationFactoryLabel);
            this.gbPlugins.Controls.Add(this.libraryLabel);
            this.gbPlugins.Controls.Add(this.OutputterLabel);
            this.gbPlugins.Controls.Add(this.populatorLabel);
            this.gbPlugins.Controls.Add(this.TerminatorLabel);
            this.gbPlugins.Controls.Add(this.evaluatorLabel);
            this.gbPlugins.Controls.Add(this.geneticOperatorLabel);
            this.gbPlugins.Controls.Add(this.cbEvaluator);
            this.gbPlugins.Controls.Add(this.cbGenerationFactory);
            this.gbPlugins.Controls.Add(this.cbGeneticOperator);
            this.gbPlugins.Controls.Add(this.cbOutputter);
            this.gbPlugins.Controls.Add(this.cbTerminator);
            this.gbPlugins.Location = new System.Drawing.Point(12, 14);
            this.gbPlugins.Name = "gbPlugins";
            this.gbPlugins.Size = new System.Drawing.Size(406, 243);
            this.gbPlugins.TabIndex = 17;
            this.gbPlugins.TabStop = false;
            this.gbPlugins.Text = "Plug-ins";
            // 
            // libraryLoaderButton
            // 
            this.libraryLoaderButton.Location = new System.Drawing.Point(14, 20);
            this.libraryLoaderButton.Name = "libraryLoaderButton";
            this.libraryLoaderButton.Size = new System.Drawing.Size(86, 23);
            this.libraryLoaderButton.TabIndex = 15;
            this.libraryLoaderButton.Text = "Load Library";
            this.libraryLoaderButton.UseVisualStyleBackColor = true;
            this.libraryLoaderButton.Click += new System.EventHandler(this.libraryLoaderButton_Click);
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.viewOutputFileButton);
            this.gbSettings.Controls.Add(this.nScroller);
            this.gbSettings.Controls.Add(this.nlabel);
            this.gbSettings.Controls.Add(this.runGenerationButton);
            this.gbSettings.Controls.Add(this.runButton);
            this.gbSettings.Controls.Add(this.stepButton);
            this.gbSettings.Controls.Add(this.initEngineButton);
            this.gbSettings.Controls.Add(this.outputFileSelectButton);
            this.gbSettings.Controls.Add(this.tbOutputFile);
            this.gbSettings.Controls.Add(this.outputFileButton);
            this.gbSettings.Controls.Add(this.targetFitnessScroller);
            this.gbSettings.Controls.Add(this.TargetFitnessLabel);
            this.gbSettings.Controls.Add(this.MapFileSelectButton);
            this.gbSettings.Controls.Add(this.tbMapFile);
            this.gbSettings.Controls.Add(this.MapFileLabel);
            this.gbSettings.Location = new System.Drawing.Point(13, 263);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(406, 179);
            this.gbSettings.TabIndex = 18;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // viewOutputFileButton
            // 
            this.viewOutputFileButton.Location = new System.Drawing.Point(269, 150);
            this.viewOutputFileButton.Name = "viewOutputFileButton";
            this.viewOutputFileButton.Size = new System.Drawing.Size(119, 23);
            this.viewOutputFileButton.TabIndex = 14;
            this.viewOutputFileButton.Text = "View Output File";
            this.viewOutputFileButton.UseVisualStyleBackColor = true;
            this.viewOutputFileButton.Click += new System.EventHandler(this.viewOutputFileButton_Click);
            // 
            // nScroller
            // 
            this.nScroller.Location = new System.Drawing.Point(169, 150);
            this.nScroller.Name = "nScroller";
            this.nScroller.ReadOnly = true;
            this.nScroller.Size = new System.Drawing.Size(75, 20);
            this.nScroller.TabIndex = 13;
            // 
            // nlabel
            // 
            this.nlabel.AutoSize = true;
            this.nlabel.Location = new System.Drawing.Point(140, 153);
            this.nlabel.Name = "nlabel";
            this.nlabel.Size = new System.Drawing.Size(13, 13);
            this.nlabel.TabIndex = 12;
            this.nlabel.Text = "n";
            // 
            // runGenerationButton
            // 
            this.runGenerationButton.Location = new System.Drawing.Point(10, 148);
            this.runGenerationButton.Name = "runGenerationButton";
            this.runGenerationButton.Size = new System.Drawing.Size(112, 23);
            this.runGenerationButton.TabIndex = 11;
            this.runGenerationButton.Text = "Run n generations";
            this.runGenerationButton.UseVisualStyleBackColor = true;
            this.runGenerationButton.Click += new System.EventHandler(this.runGenerationButton_Click);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(267, 119);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(88, 23);
            this.runButton.TabIndex = 10;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // stepButton
            // 
            this.stepButton.Location = new System.Drawing.Point(147, 119);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(89, 23);
            this.stepButton.TabIndex = 9;
            this.stepButton.Text = "Step";
            this.stepButton.UseVisualStyleBackColor = true;
            this.stepButton.Click += new System.EventHandler(this.stepButton_Click);
            // 
            // initEngineButton
            // 
            this.initEngineButton.Location = new System.Drawing.Point(10, 119);
            this.initEngineButton.Name = "initEngineButton";
            this.initEngineButton.Size = new System.Drawing.Size(103, 23);
            this.initEngineButton.TabIndex = 8;
            this.initEngineButton.Text = "Initialise Engine";
            this.initEngineButton.UseVisualStyleBackColor = true;
            this.initEngineButton.Click += new System.EventHandler(this.initEngineButton_Click);
            // 
            // outputFileSelectButton
            // 
            this.outputFileSelectButton.Location = new System.Drawing.Point(269, 84);
            this.outputFileSelectButton.Name = "outputFileSelectButton";
            this.outputFileSelectButton.Size = new System.Drawing.Size(86, 20);
            this.outputFileSelectButton.TabIndex = 7;
            this.outputFileSelectButton.Text = "Select";
            this.outputFileSelectButton.UseVisualStyleBackColor = true;
            this.outputFileSelectButton.Click += new System.EventHandler(this.outputFileSelectButton_Click);
            // 
            // tbOutputFile
            // 
            this.tbOutputFile.Location = new System.Drawing.Point(86, 84);
            this.tbOutputFile.Name = "tbOutputFile";
            this.tbOutputFile.Size = new System.Drawing.Size(158, 20);
            this.tbOutputFile.TabIndex = 6;
            // 
            // outputFileButton
            // 
            this.outputFileButton.AutoSize = true;
            this.outputFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputFileButton.Location = new System.Drawing.Point(6, 84);
            this.outputFileButton.Name = "outputFileButton";
            this.outputFileButton.Size = new System.Drawing.Size(77, 17);
            this.outputFileButton.TabIndex = 5;
            this.outputFileButton.Text = "Output File";
            // 
            // targetFitnessScroller
            // 
            this.targetFitnessScroller.Location = new System.Drawing.Point(116, 52);
            this.targetFitnessScroller.Name = "targetFitnessScroller";
            this.targetFitnessScroller.ReadOnly = true;
            this.targetFitnessScroller.Size = new System.Drawing.Size(120, 20);
            this.targetFitnessScroller.TabIndex = 4;
            // 
            // TargetFitnessLabel
            // 
            this.TargetFitnessLabel.AutoSize = true;
            this.TargetFitnessLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TargetFitnessLabel.Location = new System.Drawing.Point(6, 52);
            this.TargetFitnessLabel.Name = "TargetFitnessLabel";
            this.TargetFitnessLabel.Size = new System.Drawing.Size(99, 17);
            this.TargetFitnessLabel.TabIndex = 3;
            this.TargetFitnessLabel.Text = "Target Fitness";
            // 
            // MapFileSelectButton
            // 
            this.MapFileSelectButton.Location = new System.Drawing.Point(269, 17);
            this.MapFileSelectButton.Name = "MapFileSelectButton";
            this.MapFileSelectButton.Size = new System.Drawing.Size(86, 23);
            this.MapFileSelectButton.TabIndex = 2;
            this.MapFileSelectButton.Text = "Select";
            this.MapFileSelectButton.UseVisualStyleBackColor = true;
            this.MapFileSelectButton.Click += new System.EventHandler(this.MapFileSelectButton_Click);
            // 
            // tbMapFile
            // 
            this.tbMapFile.Location = new System.Drawing.Point(86, 20);
            this.tbMapFile.Name = "tbMapFile";
            this.tbMapFile.Size = new System.Drawing.Size(158, 20);
            this.tbMapFile.TabIndex = 1;
            // 
            // MapFileLabel
            // 
            this.MapFileLabel.AutoSize = true;
            this.MapFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MapFileLabel.Location = new System.Drawing.Point(7, 20);
            this.MapFileLabel.Name = "MapFileLabel";
            this.MapFileLabel.Size = new System.Drawing.Size(61, 17);
            this.MapFileLabel.TabIndex = 0;
            this.MapFileLabel.Text = "Map File";
            // 
            // gbGeneration
            // 
            this.gbGeneration.Controls.Add(this.averageFitnessValue);
            this.gbGeneration.Controls.Add(this.label2);
            this.gbGeneration.Controls.Add(this.maxFitnessValue);
            this.gbGeneration.Controls.Add(this.label1);
            this.gbGeneration.Location = new System.Drawing.Point(13, 444);
            this.gbGeneration.Name = "gbGeneration";
            this.gbGeneration.Size = new System.Drawing.Size(405, 41);
            this.gbGeneration.TabIndex = 19;
            this.gbGeneration.TabStop = false;
            this.gbGeneration.Text = "Current Generation";
            // 
            // averageFitnessValue
            // 
            this.averageFitnessValue.AutoSize = true;
            this.averageFitnessValue.Location = new System.Drawing.Point(300, 22);
            this.averageFitnessValue.Name = "averageFitnessValue";
            this.averageFitnessValue.Size = new System.Drawing.Size(13, 13);
            this.averageFitnessValue.TabIndex = 3;
            this.averageFitnessValue.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Average Fitness:";
            // 
            // maxFitnessValue
            // 
            this.maxFitnessValue.AutoSize = true;
            this.maxFitnessValue.Location = new System.Drawing.Point(86, 21);
            this.maxFitnessValue.Name = "maxFitnessValue";
            this.maxFitnessValue.Size = new System.Drawing.Size(13, 13);
            this.maxFitnessValue.TabIndex = 1;
            this.maxFitnessValue.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Max Fitness:";
            // 
            // RoadNetworkFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 489);
            this.Controls.Add(this.gbGeneration);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.gbPlugins);
            this.Controls.Add(this.gbMap);
            this.Name = "RoadNetworkFinder";
            this.Text = "Road Network Finder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbMap.ResumeLayout(false);
            this.gbPlugins.ResumeLayout(false);
            this.gbPlugins.PerformLayout();
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.gbGeneration.ResumeLayout(false);
            this.gbGeneration.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label populatorLabel;
        private System.Windows.Forms.ComboBox cbPopulator;
        private System.Windows.Forms.ComboBox cbEvaluator;
        private System.Windows.Forms.ComboBox cbGeneticOperator;
        private System.Windows.Forms.ComboBox cbTerminator;
        private System.Windows.Forms.ComboBox cbOutputter;
        private System.Windows.Forms.ComboBox cbGenerationFactory;
        private System.Windows.Forms.Label evaluatorLabel;
        private System.Windows.Forms.Label geneticOperatorLabel;
        private System.Windows.Forms.Label TerminatorLabel;
        private System.Windows.Forms.Label OutputterLabel;
        private System.Windows.Forms.Label GenerationFactoryLabel;
        private System.Windows.Forms.Label libraryLabel;
        private System.Windows.Forms.GroupBox gbMap;
        private System.Windows.Forms.GroupBox gbPlugins;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.Label TargetFitnessLabel;
        private System.Windows.Forms.Button MapFileSelectButton;
        private System.Windows.Forms.TextBox tbMapFile;
        private System.Windows.Forms.Label MapFileLabel;
        private System.Windows.Forms.DomainUpDown targetFitnessScroller;
        private System.Windows.Forms.Button libraryLoaderButton;
        private System.Windows.Forms.Label outputFileButton;
        private System.Windows.Forms.TextBox tbOutputFile;
        private System.Windows.Forms.Button outputFileSelectButton;
        private System.Windows.Forms.Button initEngineButton;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button stepButton;
        private System.Windows.Forms.DomainUpDown nScroller;
        private System.Windows.Forms.Label nlabel;
        private System.Windows.Forms.Button runGenerationButton;
        private System.Windows.Forms.GroupBox gbGeneration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label maxFitnessValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label averageFitnessValue;
        private RoadNetworkDisplay.RoadNetworkPanel visualiser1;
        private Button viewOutputFileButton;
    }
}

