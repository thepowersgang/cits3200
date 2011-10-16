namespace RoadNetworkGUI
{
    partial class Road_Network_Visualiser
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
            this.gbFile = new System.Windows.Forms.GroupBox();
            this.visualiser2 = new RoadNetworkDisplay.RoadNetworkPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fitnessLabel = new System.Windows.Forms.Label();
            this.generation = new System.Windows.Forms.NumericUpDown();
            this.individual = new System.Windows.Forms.NumericUpDown();
            this.gbFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.generation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.individual)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFile
            // 
            this.gbFile.Controls.Add(this.visualiser2);
            this.gbFile.Location = new System.Drawing.Point(13, 13);
            this.gbFile.Name = "gbFile";
            this.gbFile.Size = new System.Drawing.Size(507, 469);
            this.gbFile.TabIndex = 0;
            this.gbFile.TabStop = false;
            this.gbFile.Text = "File";
            // 
            // visualiser2
            // 
            this.visualiser2.BackColor = System.Drawing.Color.White;
            this.visualiser2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.visualiser2.Location = new System.Drawing.Point(19, 19);
            this.visualiser2.Name = "visualiser2";
            this.visualiser2.Network = null;
            this.visualiser2.Size = new System.Drawing.Size(470, 440);
            this.visualiser2.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(564, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Generation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(571, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Individual";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(572, 460);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Fitness:";
            // 
            // fitnessLabel
            // 
            this.fitnessLabel.AutoSize = true;
            this.fitnessLabel.Location = new System.Drawing.Point(622, 460);
            this.fitnessLabel.Name = "fitnessLabel";
            this.fitnessLabel.Size = new System.Drawing.Size(13, 13);
            this.fitnessLabel.TabIndex = 6;
            this.fitnessLabel.Text = "0";
            // 
            // generation
            // 
            this.generation.Location = new System.Drawing.Point(664, 25);
            this.generation.Name = "generation";
            this.generation.Size = new System.Drawing.Size(73, 20);
            this.generation.TabIndex = 7;
            this.generation.ValueChanged += new System.EventHandler(this.generation_ValueChanged);
            // 
            // individual
            // 
            this.individual.Location = new System.Drawing.Point(664, 52);
            this.individual.Name = "individual";
            this.individual.Size = new System.Drawing.Size(73, 20);
            this.individual.TabIndex = 8;
            this.individual.ValueChanged += new System.EventHandler(this.individual_ValueChanged);
            // 
            // Road_Network_Visualiser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 494);
            this.Controls.Add(this.individual);
            this.Controls.Add(this.generation);
            this.Controls.Add(this.fitnessLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbFile);
            this.Name = "Road_Network_Visualiser";
            this.Text = "Road Network Visualiser";
            this.Load += new System.EventHandler(this.Road_Network_Visualiser_Load);
            this.gbFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.generation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.individual)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.GroupBox gbFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label fitnessLabel;
        private RoadNetworkDisplay.RoadNetworkPanel visualiser2;
        private System.Windows.Forms.NumericUpDown generation;
        private System.Windows.Forms.NumericUpDown individual;
    }
}