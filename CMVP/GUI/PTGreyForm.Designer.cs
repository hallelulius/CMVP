namespace CMVP.GUI
{
    partial class PTGreyForm
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
            this.source = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.raw = new System.Windows.Forms.RadioButton();
            this.features = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.source.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // source
            // 
            this.source.Controls.Add(this.radioButton2);
            this.source.Controls.Add(this.raw);
            this.source.Location = new System.Drawing.Point(12, 12);
            this.source.Name = "source";
            this.source.Size = new System.Drawing.Size(200, 79);
            this.source.TabIndex = 2;
            this.source.TabStop = false;
            this.source.Text = "Source";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 39);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(105, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Processed Video";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // raw
            // 
            this.raw.AutoSize = true;
            this.raw.Location = new System.Drawing.Point(3, 16);
            this.raw.Name = "raw";
            this.raw.Size = new System.Drawing.Size(77, 17);
            this.raw.TabIndex = 0;
            this.raw.TabStop = true;
            this.raw.Text = "Raw Video";
            this.raw.UseVisualStyleBackColor = true;
            this.raw.CheckedChanged += new System.EventHandler(this.raw_CheckedChanged);
            // 
            // features
            // 
            this.features.Location = new System.Drawing.Point(12, 114);
            this.features.Name = "features";
            this.features.Size = new System.Drawing.Size(200, 100);
            this.features.TabIndex = 0;
            this.features.TabStop = false;
            this.features.Text = "Features";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Location = new System.Drawing.Point(218, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(588, 501);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // PTGreyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 501);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.features);
            this.Controls.Add(this.source);
            this.Name = "PTGreyForm";
            this.Text = "PTGreyForm";
            this.Load += new System.EventHandler(this.PTGreyForm_Load);
            this.source.ResumeLayout(false);
            this.source.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox source;
        private System.Windows.Forms.GroupBox features;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton raw;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}