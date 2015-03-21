namespace CMVP
{
    partial class SeriesControl
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
            this.seriesTypeLabel = new System.Windows.Forms.Label();
            this.removeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // seriesTypeLabel
            // 
            this.seriesTypeLabel.AutoSize = true;
            this.seriesTypeLabel.Location = new System.Drawing.Point(3, 9);
            this.seriesTypeLabel.Name = "seriesTypeLabel";
            this.seriesTypeLabel.Size = new System.Drawing.Size(48, 13);
            this.seriesTypeLabel.TabIndex = 0;
            this.seriesTypeLabel.Text = "Dynamic";
            this.seriesTypeLabel.Click += new System.EventHandler(this.seriesTypeLabel_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(188, 4);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(76, 23);
            this.removeButton.TabIndex = 1;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // SeriesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.seriesTypeLabel);
            this.Name = "SeriesControl";
            this.Size = new System.Drawing.Size(267, 32);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label seriesTypeLabel;
        private System.Windows.Forms.Button removeButton;
    }
}
