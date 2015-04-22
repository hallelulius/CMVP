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
            this.kpSteerNumeric = new System.Windows.Forms.NumericUpDown();
            this.kpSteerLabel = new System.Windows.Forms.Label();
            this.kiSteerNumeric = new System.Windows.Forms.NumericUpDown();
            this.kiSteerLabel = new System.Windows.Forms.Label();
            this.kdSteerNumeric = new System.Windows.Forms.NumericUpDown();
            this.kdSteerLabel = new System.Windows.Forms.Label();
            this.kdThrottleNumeric = new System.Windows.Forms.NumericUpDown();
            this.kdThrottleLabel = new System.Windows.Forms.Label();
            this.kiThrottleNumeric = new System.Windows.Forms.NumericUpDown();
            this.kiThrottleLabel = new System.Windows.Forms.Label();
            this.kpThrottleNumeric = new System.Windows.Forms.NumericUpDown();
            this.kpThrottleLabel = new System.Windows.Forms.Label();
            this.steeringLabel = new System.Windows.Forms.Label();
            this.throttleLabel = new System.Windows.Forms.Label();
            this.saveSettings = new System.Windows.Forms.Button();
            this.loadSettings = new System.Windows.Forms.Button();
            this.settingsText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.kpSteerNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiSteerNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdSteerNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdThrottleNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiThrottleNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpThrottleNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // kpSteerNumeric
            // 
            this.kpSteerNumeric.DecimalPlaces = 4;
            this.kpSteerNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.kpSteerNumeric.Location = new System.Drawing.Point(27, 23);
            this.kpSteerNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.kpSteerNumeric.Name = "kpSteerNumeric";
            this.kpSteerNumeric.Size = new System.Drawing.Size(70, 20);
            this.kpSteerNumeric.TabIndex = 3;
            this.kpSteerNumeric.ValueChanged += new System.EventHandler(this.numericValueChanged);
            // 
            // kpSteerLabel
            // 
            this.kpSteerLabel.AutoSize = true;
            this.kpSteerLabel.Location = new System.Drawing.Point(3, 25);
            this.kpSteerLabel.Name = "kpSteerLabel";
            this.kpSteerLabel.Size = new System.Drawing.Size(23, 13);
            this.kpSteerLabel.TabIndex = 2;
            this.kpSteerLabel.Text = "Kp:";
            // 
            // kiSteerNumeric
            // 
            this.kiSteerNumeric.DecimalPlaces = 4;
            this.kiSteerNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.kiSteerNumeric.Location = new System.Drawing.Point(26, 44);
            this.kiSteerNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.kiSteerNumeric.Name = "kiSteerNumeric";
            this.kiSteerNumeric.Size = new System.Drawing.Size(70, 20);
            this.kiSteerNumeric.TabIndex = 5;
            this.kiSteerNumeric.ValueChanged += new System.EventHandler(this.numericValueChanged);
            // 
            // kiSteerLabel
            // 
            this.kiSteerLabel.AutoSize = true;
            this.kiSteerLabel.Location = new System.Drawing.Point(3, 46);
            this.kiSteerLabel.Name = "kiSteerLabel";
            this.kiSteerLabel.Size = new System.Drawing.Size(19, 13);
            this.kiSteerLabel.TabIndex = 4;
            this.kiSteerLabel.Text = "Ki:";
            // 
            // kdSteerNumeric
            // 
            this.kdSteerNumeric.DecimalPlaces = 4;
            this.kdSteerNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.kdSteerNumeric.Location = new System.Drawing.Point(26, 65);
            this.kdSteerNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.kdSteerNumeric.Name = "kdSteerNumeric";
            this.kdSteerNumeric.Size = new System.Drawing.Size(70, 20);
            this.kdSteerNumeric.TabIndex = 7;
            this.kdSteerNumeric.ValueChanged += new System.EventHandler(this.numericValueChanged);
            // 
            // kdSteerLabel
            // 
            this.kdSteerLabel.AutoSize = true;
            this.kdSteerLabel.Location = new System.Drawing.Point(3, 67);
            this.kdSteerLabel.Name = "kdSteerLabel";
            this.kdSteerLabel.Size = new System.Drawing.Size(23, 13);
            this.kdSteerLabel.TabIndex = 6;
            this.kdSteerLabel.Text = "Kd:";
            // 
            // kdThrottleNumeric
            // 
            this.kdThrottleNumeric.DecimalPlaces = 4;
            this.kdThrottleNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.kdThrottleNumeric.Location = new System.Drawing.Point(127, 65);
            this.kdThrottleNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.kdThrottleNumeric.Name = "kdThrottleNumeric";
            this.kdThrottleNumeric.Size = new System.Drawing.Size(70, 20);
            this.kdThrottleNumeric.TabIndex = 13;
            this.kdThrottleNumeric.ValueChanged += new System.EventHandler(this.numericValueChanged);
            // 
            // kdThrottleLabel
            // 
            this.kdThrottleLabel.AutoSize = true;
            this.kdThrottleLabel.Location = new System.Drawing.Point(104, 67);
            this.kdThrottleLabel.Name = "kdThrottleLabel";
            this.kdThrottleLabel.Size = new System.Drawing.Size(23, 13);
            this.kdThrottleLabel.TabIndex = 12;
            this.kdThrottleLabel.Text = "Kd:";
            // 
            // kiThrottleNumeric
            // 
            this.kiThrottleNumeric.DecimalPlaces = 4;
            this.kiThrottleNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.kiThrottleNumeric.Location = new System.Drawing.Point(127, 44);
            this.kiThrottleNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.kiThrottleNumeric.Name = "kiThrottleNumeric";
            this.kiThrottleNumeric.Size = new System.Drawing.Size(70, 20);
            this.kiThrottleNumeric.TabIndex = 11;
            this.kiThrottleNumeric.ValueChanged += new System.EventHandler(this.numericValueChanged);
            // 
            // kiThrottleLabel
            // 
            this.kiThrottleLabel.AutoSize = true;
            this.kiThrottleLabel.Location = new System.Drawing.Point(104, 46);
            this.kiThrottleLabel.Name = "kiThrottleLabel";
            this.kiThrottleLabel.Size = new System.Drawing.Size(19, 13);
            this.kiThrottleLabel.TabIndex = 10;
            this.kiThrottleLabel.Text = "Ki:";
            // 
            // kpThrottleNumeric
            // 
            this.kpThrottleNumeric.DecimalPlaces = 4;
            this.kpThrottleNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.kpThrottleNumeric.Location = new System.Drawing.Point(127, 23);
            this.kpThrottleNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.kpThrottleNumeric.Name = "kpThrottleNumeric";
            this.kpThrottleNumeric.Size = new System.Drawing.Size(70, 20);
            this.kpThrottleNumeric.TabIndex = 9;
            this.kpThrottleNumeric.ValueChanged += new System.EventHandler(this.numericValueChanged);
            // 
            // kpThrottleLabel
            // 
            this.kpThrottleLabel.AutoSize = true;
            this.kpThrottleLabel.Location = new System.Drawing.Point(104, 25);
            this.kpThrottleLabel.Name = "kpThrottleLabel";
            this.kpThrottleLabel.Size = new System.Drawing.Size(23, 13);
            this.kpThrottleLabel.TabIndex = 8;
            this.kpThrottleLabel.Text = "Kp:";
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
            // saveSettings
            // 
            this.saveSettings.Location = new System.Drawing.Point(6, 123);
            this.saveSettings.Name = "saveSettings";
            this.saveSettings.Size = new System.Drawing.Size(75, 23);
            this.saveSettings.TabIndex = 23;
            this.saveSettings.Text = "Save";
            this.saveSettings.UseVisualStyleBackColor = true;
            this.saveSettings.Click += new System.EventHandler(this.saveSettings_Click);
            // 
            // loadSettings
            // 
            this.loadSettings.Location = new System.Drawing.Point(107, 123);
            this.loadSettings.Name = "loadSettings";
            this.loadSettings.Size = new System.Drawing.Size(75, 23);
            this.loadSettings.TabIndex = 24;
            this.loadSettings.Text = "Load";
            this.loadSettings.UseVisualStyleBackColor = true;
            this.loadSettings.Click += new System.EventHandler(this.loadSettings_Click);
            // 
            // settingsText
            // 
            this.settingsText.AutoSize = true;
            this.settingsText.Location = new System.Drawing.Point(9, 101);
            this.settingsText.Name = "settingsText";
            this.settingsText.Size = new System.Drawing.Size(165, 13);
            this.settingsText.TabIndex = 25;
            this.settingsText.Text = "Save and Load controller settings";
            // 
            // PIDControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.settingsText);
            this.Controls.Add(this.loadSettings);
            this.Controls.Add(this.saveSettings);
            this.Controls.Add(this.throttleLabel);
            this.Controls.Add(this.steeringLabel);
            this.Controls.Add(this.kdThrottleNumeric);
            this.Controls.Add(this.kdThrottleLabel);
            this.Controls.Add(this.kiThrottleNumeric);
            this.Controls.Add(this.kiThrottleLabel);
            this.Controls.Add(this.kpThrottleNumeric);
            this.Controls.Add(this.kpThrottleLabel);
            this.Controls.Add(this.kdSteerNumeric);
            this.Controls.Add(this.kdSteerLabel);
            this.Controls.Add(this.kiSteerNumeric);
            this.Controls.Add(this.kiSteerLabel);
            this.Controls.Add(this.kpSteerNumeric);
            this.Controls.Add(this.kpSteerLabel);
            this.Name = "PIDControlPanel";
            this.Size = new System.Drawing.Size(200, 159);
            this.Load += new System.EventHandler(this.PIDControlPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kpSteerNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiSteerNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdSteerNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdThrottleNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiThrottleNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpThrottleNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown kpSteerNumeric;
        private System.Windows.Forms.Label kpSteerLabel;
        private System.Windows.Forms.NumericUpDown kiSteerNumeric;
        private System.Windows.Forms.Label kiSteerLabel;
        private System.Windows.Forms.Label kdSteerLabel;
        private System.Windows.Forms.NumericUpDown kdSteerNumeric;
        private System.Windows.Forms.NumericUpDown kdThrottleNumeric;
        private System.Windows.Forms.Label kdThrottleLabel;
        private System.Windows.Forms.NumericUpDown kiThrottleNumeric;
        private System.Windows.Forms.Label kiThrottleLabel;
        private System.Windows.Forms.NumericUpDown kpThrottleNumeric;
        private System.Windows.Forms.Label kpThrottleLabel;
        private System.Windows.Forms.Label steeringLabel;
        private System.Windows.Forms.Label throttleLabel;
        private System.Windows.Forms.Button saveSettings;
        private System.Windows.Forms.Button loadSettings;
        private System.Windows.Forms.Label settingsText;
    }
}
