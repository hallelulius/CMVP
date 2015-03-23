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
            this.tiSteerNumeric = new System.Windows.Forms.NumericUpDown();
            this.tiSteerLabel = new System.Windows.Forms.Label();
            this.tdSteerNumeric = new System.Windows.Forms.NumericUpDown();
            this.tiThrottleNumeric = new System.Windows.Forms.NumericUpDown();
            this.tdThrottleNumeric = new System.Windows.Forms.NumericUpDown();
            this.tdSteerLabel = new System.Windows.Forms.Label();
            this.tiThrottleLabel = new System.Windows.Forms.Label();
            this.tdThrottleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.kpSteerNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiSteerNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdSteerNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdThrottleNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiThrottleNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpThrottleNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tiSteerNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tdSteerNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tiThrottleNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tdThrottleNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // kpSteerNumeric
            // 
            this.kpSteerNumeric.DecimalPlaces = 4;
            this.kpSteerNumeric.Location = new System.Drawing.Point(26, 23);
            this.kpSteerNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.kpSteerNumeric.Name = "kpSteerNumeric";
            this.kpSteerNumeric.Size = new System.Drawing.Size(70, 20);
            this.kpSteerNumeric.TabIndex = 3;
            this.kpSteerNumeric.Value = new decimal(new int[] {
            722,
            0,
            0,
            262144});
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
            this.kiSteerNumeric.Location = new System.Drawing.Point(26, 44);
            this.kiSteerNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.kiSteerNumeric.Name = "kiSteerNumeric";
            this.kiSteerNumeric.Size = new System.Drawing.Size(70, 20);
            this.kiSteerNumeric.TabIndex = 5;
            this.kiSteerNumeric.Value = new decimal(new int[] {
            1689,
            0,
            0,
            262144});
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
            this.kdSteerNumeric.Enabled = false;
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
            this.kdThrottleNumeric.Enabled = false;
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
            this.kiThrottleNumeric.Location = new System.Drawing.Point(127, 44);
            this.kiThrottleNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.kiThrottleNumeric.Name = "kiThrottleNumeric";
            this.kiThrottleNumeric.Size = new System.Drawing.Size(70, 20);
            this.kiThrottleNumeric.TabIndex = 11;
            this.kiThrottleNumeric.Value = new decimal(new int[] {
            131,
            0,
            0,
            196608});
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
            this.kpThrottleNumeric.Location = new System.Drawing.Point(127, 23);
            this.kpThrottleNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.kpThrottleNumeric.Name = "kpThrottleNumeric";
            this.kpThrottleNumeric.Size = new System.Drawing.Size(70, 20);
            this.kpThrottleNumeric.TabIndex = 9;
            this.kpThrottleNumeric.Value = new decimal(new int[] {
            125,
            0,
            0,
            262144});
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
            // tiSteerNumeric
            // 
            this.tiSteerNumeric.DecimalPlaces = 4;
            this.tiSteerNumeric.Location = new System.Drawing.Point(26, 86);
            this.tiSteerNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.tiSteerNumeric.Name = "tiSteerNumeric";
            this.tiSteerNumeric.Size = new System.Drawing.Size(70, 20);
            this.tiSteerNumeric.TabIndex = 17;
            this.tiSteerNumeric.Value = new decimal(new int[] {
            23397,
            0,
            0,
            262144});
            this.tiSteerNumeric.ValueChanged += new System.EventHandler(this.numericValueChanged);
            // 
            // tiSteerLabel
            // 
            this.tiSteerLabel.AutoSize = true;
            this.tiSteerLabel.Location = new System.Drawing.Point(3, 88);
            this.tiSteerLabel.Name = "tiSteerLabel";
            this.tiSteerLabel.Size = new System.Drawing.Size(19, 13);
            this.tiSteerLabel.TabIndex = 16;
            this.tiSteerLabel.Text = "Ti:";
            // 
            // tdSteerNumeric
            // 
            this.tdSteerNumeric.DecimalPlaces = 4;
            this.tdSteerNumeric.Enabled = false;
            this.tdSteerNumeric.Location = new System.Drawing.Point(26, 107);
            this.tdSteerNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.tdSteerNumeric.Name = "tdSteerNumeric";
            this.tdSteerNumeric.Size = new System.Drawing.Size(70, 20);
            this.tdSteerNumeric.TabIndex = 18;
            this.tdSteerNumeric.ValueChanged += new System.EventHandler(this.numericValueChanged);
            // 
            // tiThrottleNumeric
            // 
            this.tiThrottleNumeric.DecimalPlaces = 4;
            this.tiThrottleNumeric.Location = new System.Drawing.Point(127, 86);
            this.tiThrottleNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.tiThrottleNumeric.Name = "tiThrottleNumeric";
            this.tiThrottleNumeric.Size = new System.Drawing.Size(70, 20);
            this.tiThrottleNumeric.TabIndex = 19;
            this.tiThrottleNumeric.Value = new decimal(new int[] {
            105179,
            0,
            0,
            262144});
            this.tiThrottleNumeric.ValueChanged += new System.EventHandler(this.numericValueChanged);
            // 
            // tdThrottleNumeric
            // 
            this.tdThrottleNumeric.DecimalPlaces = 4;
            this.tdThrottleNumeric.Enabled = false;
            this.tdThrottleNumeric.Location = new System.Drawing.Point(127, 107);
            this.tdThrottleNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.tdThrottleNumeric.Name = "tdThrottleNumeric";
            this.tdThrottleNumeric.Size = new System.Drawing.Size(70, 20);
            this.tdThrottleNumeric.TabIndex = 20;
            this.tdThrottleNumeric.ValueChanged += new System.EventHandler(this.numericValueChanged);
            // 
            // tdSteerLabel
            // 
            this.tdSteerLabel.AutoSize = true;
            this.tdSteerLabel.Location = new System.Drawing.Point(3, 109);
            this.tdSteerLabel.Name = "tdSteerLabel";
            this.tdSteerLabel.Size = new System.Drawing.Size(23, 13);
            this.tdSteerLabel.TabIndex = 21;
            this.tdSteerLabel.Text = "Td:";
            // 
            // tiThrottleLabel
            // 
            this.tiThrottleLabel.AutoSize = true;
            this.tiThrottleLabel.Location = new System.Drawing.Point(104, 88);
            this.tiThrottleLabel.Name = "tiThrottleLabel";
            this.tiThrottleLabel.Size = new System.Drawing.Size(19, 13);
            this.tiThrottleLabel.TabIndex = 22;
            this.tiThrottleLabel.Text = "Ti:";
            // 
            // tdThrottleLabel
            // 
            this.tdThrottleLabel.AutoSize = true;
            this.tdThrottleLabel.Location = new System.Drawing.Point(104, 109);
            this.tdThrottleLabel.Name = "tdThrottleLabel";
            this.tdThrottleLabel.Size = new System.Drawing.Size(23, 13);
            this.tdThrottleLabel.TabIndex = 23;
            this.tdThrottleLabel.Text = "Td:";
            // 
            // PIDControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tdThrottleLabel);
            this.Controls.Add(this.tiThrottleLabel);
            this.Controls.Add(this.tdSteerLabel);
            this.Controls.Add(this.tdThrottleNumeric);
            this.Controls.Add(this.tiThrottleNumeric);
            this.Controls.Add(this.tdSteerNumeric);
            this.Controls.Add(this.tiSteerNumeric);
            this.Controls.Add(this.tiSteerLabel);
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
            ((System.ComponentModel.ISupportInitialize)(this.kpSteerNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiSteerNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdSteerNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdThrottleNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiThrottleNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpThrottleNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tiSteerNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tdSteerNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tiThrottleNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tdThrottleNumeric)).EndInit();
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
        private System.Windows.Forms.NumericUpDown tiSteerNumeric;
        private System.Windows.Forms.Label tiSteerLabel;
        private System.Windows.Forms.NumericUpDown tdSteerNumeric;
        private System.Windows.Forms.NumericUpDown tiThrottleNumeric;
        private System.Windows.Forms.NumericUpDown tdThrottleNumeric;
        private System.Windows.Forms.Label tdSteerLabel;
        private System.Windows.Forms.Label tiThrottleLabel;
        private System.Windows.Forms.Label tdThrottleLabel;
    }
}
