namespace CMVP
{
    partial class PIControlPanel
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
            this.pNumeric = new System.Windows.Forms.NumericUpDown();
            this.pLabel = new System.Windows.Forms.Label();
            this.iNumeric = new System.Windows.Forms.NumericUpDown();
            this.iLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // pNumeric
            // 
            this.pNumeric.DecimalPlaces = 3;
            this.pNumeric.Location = new System.Drawing.Point(91, 2);
            this.pNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.pNumeric.Name = "pNumeric";
            this.pNumeric.Size = new System.Drawing.Size(97, 20);
            this.pNumeric.TabIndex = 3;
            // 
            // pLabel
            // 
            this.pLabel.AutoSize = true;
            this.pLabel.Location = new System.Drawing.Point(4, 4);
            this.pLabel.Name = "pLabel";
            this.pLabel.Size = new System.Drawing.Size(81, 13);
            this.pLabel.TabIndex = 2;
            this.pLabel.Text = "P (proportional):";
            // 
            // iNumeric
            // 
            this.iNumeric.DecimalPlaces = 3;
            this.iNumeric.Location = new System.Drawing.Point(91, 23);
            this.iNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.iNumeric.Name = "iNumeric";
            this.iNumeric.Size = new System.Drawing.Size(97, 20);
            this.iNumeric.TabIndex = 5;
            // 
            // iLabel
            // 
            this.iLabel.AutoSize = true;
            this.iLabel.Location = new System.Drawing.Point(4, 25);
            this.iLabel.Name = "iLabel";
            this.iLabel.Size = new System.Drawing.Size(56, 13);
            this.iLabel.TabIndex = 4;
            this.iLabel.Text = "I (integral):";

            // 
            // PIDControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dLabel);
            this.Controls.Add(this.dNumeric);
            this.Controls.Add(this.iNumeric);
            this.Controls.Add(this.iLabel);
            this.Controls.Add(this.pNumeric);
            this.Controls.Add(this.pLabel);
            this.Name = "PIDControlPanel";
            this.Size = new System.Drawing.Size(200, 114);
            ((System.ComponentModel.ISupportInitialize)(this.pNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown pNumeric;
        private System.Windows.Forms.Label pLabel;
        private System.Windows.Forms.NumericUpDown iNumeric;
        private System.Windows.Forms.Label iLabel;
        private System.Windows.Forms.NumericUpDown dNumeric;
        private System.Windows.Forms.Label dLabel;
    }
}
