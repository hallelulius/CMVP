namespace CMVP
{
    partial class PControlPanel
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
            this.pLabel = new System.Windows.Forms.Label();
            this.pNumeric = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // pLabel
            // 
            this.pLabel.AutoSize = true;
            this.pLabel.Location = new System.Drawing.Point(4, 4);
            this.pLabel.Name = "pLabel";
            this.pLabel.Size = new System.Drawing.Size(81, 13);
            this.pLabel.TabIndex = 0;
            this.pLabel.Text = "P (proportional):";
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
            this.pNumeric.Size = new System.Drawing.Size(95, 20);
            this.pNumeric.TabIndex = 1;
            // 
            // PControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pNumeric);
            this.Controls.Add(this.pLabel);
            this.Name = "PControlPanel";
            this.Size = new System.Drawing.Size(200, 114);
            ((System.ComponentModel.ISupportInitialize)(this.pNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pLabel;
        private System.Windows.Forms.NumericUpDown pNumeric;
    }
}
