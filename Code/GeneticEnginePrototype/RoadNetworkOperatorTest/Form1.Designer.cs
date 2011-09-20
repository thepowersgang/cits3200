namespace RoadNetworkOperatorTest
{
    partial class Form1
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
            this.child2 = new RoadNetworkDisplay.RoadNetworkPanel();
            this.cut2 = new RoadNetworkDisplay.RoadNetworkPanel();
            this.cut1 = new RoadNetworkDisplay.RoadNetworkPanel();
            this.child1 = new RoadNetworkDisplay.RoadNetworkPanel();
            this.parent2 = new RoadNetworkDisplay.RoadNetworkPanel();
            this.parent1 = new RoadNetworkDisplay.RoadNetworkPanel();
            this.SuspendLayout();
            // 
            // child2
            // 
            this.child2.BackColor = System.Drawing.Color.White;
            this.child2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.child2.Location = new System.Drawing.Point(536, 274);
            this.child2.Name = "child2";
            this.child2.Network = null;
            this.child2.Size = new System.Drawing.Size(256, 256);
            this.child2.TabIndex = 5;
            // 
            // cut2
            // 
            this.cut2.BackColor = System.Drawing.Color.White;
            this.cut2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cut2.Location = new System.Drawing.Point(274, 274);
            this.cut2.Name = "cut2";
            this.cut2.Network = null;
            this.cut2.Size = new System.Drawing.Size(256, 256);
            this.cut2.TabIndex = 4;
            // 
            // cut1
            // 
            this.cut1.BackColor = System.Drawing.Color.White;
            this.cut1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cut1.Location = new System.Drawing.Point(12, 274);
            this.cut1.Name = "cut1";
            this.cut1.Network = null;
            this.cut1.Size = new System.Drawing.Size(256, 256);
            this.cut1.TabIndex = 3;
            // 
            // child1
            // 
            this.child1.BackColor = System.Drawing.Color.White;
            this.child1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.child1.Location = new System.Drawing.Point(536, 12);
            this.child1.Name = "child1";
            this.child1.Network = null;
            this.child1.Size = new System.Drawing.Size(256, 256);
            this.child1.TabIndex = 2;
            // 
            // parent2
            // 
            this.parent2.BackColor = System.Drawing.Color.White;
            this.parent2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.parent2.Location = new System.Drawing.Point(274, 12);
            this.parent2.Name = "parent2";
            this.parent2.Network = null;
            this.parent2.Size = new System.Drawing.Size(256, 256);
            this.parent2.TabIndex = 1;
            // 
            // parent1
            // 
            this.parent1.BackColor = System.Drawing.Color.White;
            this.parent1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.parent1.Location = new System.Drawing.Point(12, 12);
            this.parent1.Name = "parent1";
            this.parent1.Network = null;
            this.parent1.Size = new System.Drawing.Size(256, 256);
            this.parent1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 541);
            this.Controls.Add(this.child2);
            this.Controls.Add(this.cut2);
            this.Controls.Add(this.cut1);
            this.Controls.Add(this.child1);
            this.Controls.Add(this.parent2);
            this.Controls.Add(this.parent1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RoadNetworkDisplay.RoadNetworkPanel parent1;
        private RoadNetworkDisplay.RoadNetworkPanel parent2;
        private RoadNetworkDisplay.RoadNetworkPanel child1;
        private RoadNetworkDisplay.RoadNetworkPanel child2;
        private RoadNetworkDisplay.RoadNetworkPanel cut2;
        private RoadNetworkDisplay.RoadNetworkPanel cut1;
    }
}

