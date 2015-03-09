namespace CMVP
{
    partial class PIDControlPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pSteerNumeric = new System.Windows.Forms.NumericUpDown();
            this.pSteerLabel = new System.Windows.Forms.Label();
            this.iSteerNumeric = new System.Windows.Forms.NumericUpDown();
            this.iSteerLabel = new System.Windows.Forms.Label();
            this.dSteerNumeric = new System.Windows.Forms.NumericUpDown();
            this.dSteerLabel = new System.Windows.Forms.Label();
            this.dThrottleNumeric = new System.Windows.Forms.NumericUpDown();
            this.dThrottleLabel = new System.Windows.Forms.Label();
            this.iThrottleNumeric = new System.Windows.Forms.NumericUpDown();
            this.iThrottleLabel = new System.Windows.Forms.Label();
            this.pThrottleNumeric = new System.Windows.Forms.NumericUpDown();
            this.pThrottleLabel = new System.Windows.Forms.Label();
            this.steeringLabel = new System.Windows.Forms.Label();
            this.throttleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pSteerNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iSteerNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSteerNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dThrottleNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iThrottleNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pThrottleNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // pSteerNumeric
            // 
            this.pSteerNumeric.DecimalPlaces = 3;
            this.pSteerNumeric.Location = new System.Drawing.Point(26, 23);
            this.pSteerNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.pSteerNumeric.Name = "pSteerNumeric";
            this.pSteerNumeric.Size = new System.Drawing.Size(70, 20);
            this.pSteerNumeric.TabIndex = 3;
            // 
            // pSteerLabel
            // 
            this.pSteerLabel.AutoSize = true;
            this.pSteerLabel.Location = new System.Drawing.Point(3, 25);
            this.pSteerLabel.Name = "pSteerLabel";
            this.pSteerLabel.Size = new System.Drawing.Size(17, 13);
            this.pSteerLabel.TabIndex = 2;
            this.pSteerLabel.Text = "P:";
            // 
            // iSteerNumeric
            // 
            this.iSteerNumeric.DecimalPlaces = 3;
            this.iSteerNumeric.Location = new System.Drawing.Point(26, 44);
            this.iSteerNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.iSteerNumeric.Name = "iSteerNumeric";
            this.iSteerNumeric.Size = new System.Drawing.Size(70, 20);
            this.iSteerNumeric.TabIndex = 5;
            // 
            // iSteerLabel
            // 
            this.iSteerLabel.AutoSize = true;
            this.iSteerLabel.Location = new System.Drawing.Point(3, 46);
            this.iSteerLabel.Name = "iSteerLabel";
            this.iSteerLabel.Size = new System.Drawing.Size(13, 13);
            this.iSteerLabel.TabIndex = 4;
            this.iSteerLabel.Text = "I:";
            // 
            // dSteerNumeric
            // 
            this.dSteerNumeric.DecimalPlaces = 3;
            this.dSteerNumeric.Location = new System.Drawing.Point(26, 65);
            this.dSteerNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.dSteerNumeric.Name = "dSteerNumeric";
            this.dSteerNumeric.Size = new System.Drawing.Size(70, 20);
            this.dSteerNumeric.TabIndex = 7;
            this.dSteerNumeric.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // dSteerLabel
            // 
            this.dSteerLabel.AutoSize = true;
            this.dSteerLabel.Location = new System.Drawing.Point(2, 67);
            this.dSteerLabel.Name = "dSteerLabel";
            this.dSteerLabel.Size = new System.Drawing.Size(18, 13);
            this.dSteerLabel.TabIndex = 6;
            this.dSteerLabel.Text = "D:";
            // 
            // dThrottleNumeric
            // 
            this.dThrottleNumeric.DecimalPlaces = 3;
            this.dThrottleNumeric.Location = new System.Drawing.Point(127, 65);
            this.dThrottleNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.dThrottleNumeric.Name = "dThrottleNumeric";
            this.dThrottleNumeric.Size = new System.Drawing.Size(70, 20);
            this.dThrottleNumeric.TabIndex = 13;
            // 
            // dThrottleLabel
            // 
            this.dThrottleLabel.AutoSize = true;
            this.dThrottleLabel.Location = new System.Drawing.Point(103, 67);
            this.dThrottleLabel.Name = "dThrottleLabel";
            this.dThrottleLabel.Size = new System.Drawing.Size(18, 13);
            this.dThrottleLabel.TabIndex = 12;
            this.dThrottleLabel.Text = "D:";
            // 
            // iThrottleNumeric
            // 
            this.iThrottleNumeric.DecimalPlaces = 3;
            this.iThrottleNumeric.Location = new System.Drawing.Point(127, 44);
            this.iThrottleNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.iThrottleNumeric.Name = "iThrottleNumeric";
            this.iThrottleNumeric.Size = new System.Drawing.Size(70, 20);
            this.iThrottleNumeric.TabIndex = 11;
            // 
            // iThrottleLabel
            // 
            this.iThrottleLabel.AutoSize = true;
            this.iThrottleLabel.Location = new System.Drawing.Point(104, 46);
            this.iThrottleLabel.Name = "iThrottleLabel";
            this.iThrottleLabel.Size = new System.Drawing.Size(13, 13);
            this.iThrottleLabel.TabIndex = 10;
            this.iThrottleLabel.Text = "I:";
            // 
            // pThrottleNumeric
            // 
            this.pThrottleNumeric.DecimalPlaces = 3;
            this.pThrottleNumeric.Location = new System.Drawing.Point(127, 23);
            this.pThrottleNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.pThrottleNumeric.Name = "pThrottleNumeric";
            this.pThrottleNumeric.Size = new System.Drawing.Size(70, 20);
            this.pThrottleNumeric.TabIndex = 9;
            // 
            // pThrottleLabel
            // 
            this.pThrottleLabel.AutoSize = true;
            this.pThrottleLabel.Location = new System.Drawing.Point(104, 25);
            this.pThrottleLabel.Name = "pThrottleLabel";
            this.pThrottleLabel.Size = new System.Drawing.Size(17, 13);
            this.pThrottleLabel.TabIndex = 8;
            this.pThrottleLabel.Text = "P:";
            // 
            // steeringLabel
            // 
            this.steeringLabel.AutoSize = true;
            this.steeringLabel.Location = new System.Drawing.Point(2, 4);
            this.steeringLabel.Name = "steeringLabel";
            this.steeringLabel.Size = new System.Drawing.Size(46, 13);
            this.steeringLabel.TabIndex = 14;
            this.steeringLabel.Text = "Steering";
            // 
            // throttleLabel
            // 
            this.throttleLabel.AutoSize = true;
            this.throttleLabel.Location = new System.Drawing.Point(104, 4);
            this.throttleLabel.Name = "throttleLabel";
            this.throttleLabel.Size = new System.Drawing.Size(43, 13);
            this.throttleLabel.TabIndex = 15;
            this.throttleLabel.Text = "Throttle";
            // 
            // PIDControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.throttleLabel);
            this.Controls.Add(this.steeringLabel);
            this.Controls.Add(this.dThrottleNumeric);
            this.Controls.Add(this.dThrottleLabel);
            this.Controls.Add(this.iThrottleNumeric);
            this.Controls.Add(this.iThrottleLabel);
            this.Controls.Add(this.pThrottleNumeric);
            this.Controls.Add(this.pThrottleLabel);
            this.Controls.Add(this.dSteerNumeric);
            this.Controls.Add(this.dSteerLabel);
            this.Controls.Add(this.iSteerNumeric);
            this.Controls.Add(this.iSteerLabel);
            this.Controls.Add(this.pSteerNumeric);
            this.Controls.Add(this.pSteerLabel);
            this.Name = "PIDControlPanel";
            this.Size = new System.Drawing.Size(200, 159);
            ((System.ComponentModel.ISupportInitialize)(this.pSteerNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iSteerNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSteerNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dThrottleNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iThrottleNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pThrottleNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown pSteerNumeric;
        private System.Windows.Forms.Label pSteerLabel;
        private System.Windows.Forms.NumericUpDown iSteerNumeric;
        private System.Windows.Forms.Label iSteerLabel;
        private System.Windows.Forms.Label dSteerLabel;
        private System.Windows.Forms.NumericUpDown dSteerNumeric;
        private System.Windows.Forms.NumericUpDown dThrottleNumeric;
        private System.Windows.Forms.Label dThrottleLabel;
        private System.Windows.Forms.NumericUpDown iThrottleNumeric;
        private System.Windows.Forms.Label iThrottleLabel;
        private System.Windows.Forms.NumericUpDown pThrottleNumeric;
        private System.Windows.Forms.Label pThrottleLabel;
        private System.Windows.Forms.Label steeringLabel;
        private System.Windows.Forms.Label throttleLabel;
    }
}
