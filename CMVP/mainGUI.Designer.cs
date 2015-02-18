namespace CMVP
{
    partial class mainGUI
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
            this.startSimulationButton = new System.Windows.Forms.Button();
            this.stopSimulationButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.simulationPanelLabel = new System.Windows.Forms.Label();
            this.trackPanelLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.trafficControlPanelLabel = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // startSimulationButton
            // 
            this.startSimulationButton.BackColor = System.Drawing.Color.GreenYellow;
            this.startSimulationButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.startSimulationButton.FlatAppearance.BorderColor = System.Drawing.Color.GreenYellow;
            this.startSimulationButton.FlatAppearance.BorderSize = 0;
            this.startSimulationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startSimulationButton.Location = new System.Drawing.Point(12, 12);
            this.startSimulationButton.Name = "startSimulationButton";
            this.startSimulationButton.Size = new System.Drawing.Size(135, 38);
            this.startSimulationButton.TabIndex = 0;
            this.startSimulationButton.Text = "Start Simulation";
            this.startSimulationButton.UseVisualStyleBackColor = false;
            this.startSimulationButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // stopSimulationButton
            // 
            this.stopSimulationButton.BackColor = System.Drawing.Color.Crimson;
            this.stopSimulationButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.stopSimulationButton.FlatAppearance.BorderColor = System.Drawing.Color.GreenYellow;
            this.stopSimulationButton.FlatAppearance.BorderSize = 0;
            this.stopSimulationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopSimulationButton.Location = new System.Drawing.Point(153, 12);
            this.stopSimulationButton.Name = "stopSimulationButton";
            this.stopSimulationButton.Size = new System.Drawing.Size(135, 38);
            this.stopSimulationButton.TabIndex = 1;
            this.stopSimulationButton.Text = "Stop Simulation";
            this.stopSimulationButton.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(12, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 276);
            this.panel1.TabIndex = 2;
            // 
            // simulationPanelLabel
            // 
            this.simulationPanelLabel.AutoSize = true;
            this.simulationPanelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simulationPanelLabel.Location = new System.Drawing.Point(16, 102);
            this.simulationPanelLabel.Name = "simulationPanelLabel";
            this.simulationPanelLabel.Size = new System.Drawing.Size(73, 17);
            this.simulationPanelLabel.TabIndex = 3;
            this.simulationPanelLabel.Text = "Simulation";
            // 
            // trackPanelLabel
            // 
            this.trackPanelLabel.AutoSize = true;
            this.trackPanelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackPanelLabel.Location = new System.Drawing.Point(222, 102);
            this.trackPanelLabel.Name = "trackPanelLabel";
            this.trackPanelLabel.Size = new System.Drawing.Size(44, 17);
            this.trackPanelLabel.TabIndex = 5;
            this.trackPanelLabel.Text = "Track";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(218, 111);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 276);
            this.panel2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(428, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Controller";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(424, 111);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 276);
            this.panel3.TabIndex = 4;
            // 
            // trafficControlPanelLabel
            // 
            this.trafficControlPanelLabel.AutoSize = true;
            this.trafficControlPanelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trafficControlPanelLabel.Location = new System.Drawing.Point(634, 102);
            this.trafficControlPanelLabel.Name = "trafficControlPanelLabel";
            this.trafficControlPanelLabel.Size = new System.Drawing.Size(97, 17);
            this.trafficControlPanelLabel.TabIndex = 5;
            this.trafficControlPanelLabel.Text = "Traffic Control";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(630, 111);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 276);
            this.panel4.TabIndex = 4;
            // 
            // mainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 500);
            this.Controls.Add(this.trafficControlPanelLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.trackPanelLabel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.simulationPanelLabel);
            this.Controls.Add(this.stopSimulationButton);
            this.Controls.Add(this.startSimulationButton);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "mainGUI";
            this.Text = "CMVP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startSimulationButton;
        private System.Windows.Forms.Button stopSimulationButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label simulationPanelLabel;
        private System.Windows.Forms.Label trackPanelLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label trafficControlPanelLabel;
        private System.Windows.Forms.Panel panel4;
    }
}