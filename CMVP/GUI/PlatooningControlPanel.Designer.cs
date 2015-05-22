namespace CMVP
{
    partial class PlatooningControlPanel
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
            this.carToFollowLabel = new System.Windows.Forms.Label();
            this.carToFollowIDDropDown = new System.Windows.Forms.ComboBox();
            this.distanceLabel = new System.Windows.Forms.Label();
            this.distanceNumeric = new System.Windows.Forms.NumericUpDown();
            this.kdNumeric = new System.Windows.Forms.NumericUpDown();
            this.kdLabel = new System.Windows.Forms.Label();
            this.kiNumeric = new System.Windows.Forms.NumericUpDown();
            this.kiLabel = new System.Windows.Forms.Label();
            this.kpNumeric = new System.Windows.Forms.NumericUpDown();
            this.kpLabel = new System.Windows.Forms.Label();
            this.controllerParametersLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.distanceNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // carToFollowLabel
            // 
            this.carToFollowLabel.AutoSize = true;
            this.carToFollowLabel.Location = new System.Drawing.Point(5, 7);
            this.carToFollowLabel.Name = "carToFollowLabel";
            this.carToFollowLabel.Size = new System.Drawing.Size(68, 13);
            this.carToFollowLabel.TabIndex = 0;
            this.carToFollowLabel.Text = "Car to follow:";
            // 
            // carToFollowIDDropDown
            // 
            this.carToFollowIDDropDown.FormattingEnabled = true;
            this.carToFollowIDDropDown.Location = new System.Drawing.Point(111, 5);
            this.carToFollowIDDropDown.Name = "carToFollowIDDropDown";
            this.carToFollowIDDropDown.Size = new System.Drawing.Size(86, 21);
            this.carToFollowIDDropDown.TabIndex = 1;
            this.carToFollowIDDropDown.DropDown += new System.EventHandler(this.carToFollowIDDropDown_DropDown);
            this.carToFollowIDDropDown.SelectedIndexChanged += new System.EventHandler(this.carToFollowIDDropDown_SelectedIndexChanged);
            // 
            // distanceLabel
            // 
            this.distanceLabel.AutoSize = true;
            this.distanceLabel.Location = new System.Drawing.Point(5, 34);
            this.distanceLabel.Name = "distanceLabel";
            this.distanceLabel.Size = new System.Drawing.Size(103, 13);
            this.distanceLabel.TabIndex = 2;
            this.distanceLabel.Text = "Platooning distance:";
            // 
            // distanceNumeric
            // 
            this.distanceNumeric.Location = new System.Drawing.Point(146, 32);
            this.distanceNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.distanceNumeric.Name = "distanceNumeric";
            this.distanceNumeric.Size = new System.Drawing.Size(51, 20);
            this.distanceNumeric.TabIndex = 3;
            // 
            // kdNumeric
            // 
            this.kdNumeric.DecimalPlaces = 4;
            this.kdNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.kdNumeric.Location = new System.Drawing.Point(127, 115);
            this.kdNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.kdNumeric.Name = "kdNumeric";
            this.kdNumeric.Size = new System.Drawing.Size(70, 20);
            this.kdNumeric.TabIndex = 13;
            // 
            // kdLabel
            // 
            this.kdLabel.AutoSize = true;
            this.kdLabel.Location = new System.Drawing.Point(104, 117);
            this.kdLabel.Name = "kdLabel";
            this.kdLabel.Size = new System.Drawing.Size(23, 13);
            this.kdLabel.TabIndex = 12;
            this.kdLabel.Text = "Kd:";
            // 
            // kiNumeric
            // 
            this.kiNumeric.DecimalPlaces = 4;
            this.kiNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.kiNumeric.Location = new System.Drawing.Point(127, 94);
            this.kiNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.kiNumeric.Name = "kiNumeric";
            this.kiNumeric.Size = new System.Drawing.Size(70, 20);
            this.kiNumeric.TabIndex = 11;
            // 
            // kiLabel
            // 
            this.kiLabel.AutoSize = true;
            this.kiLabel.Location = new System.Drawing.Point(104, 96);
            this.kiLabel.Name = "kiLabel";
            this.kiLabel.Size = new System.Drawing.Size(19, 13);
            this.kiLabel.TabIndex = 10;
            this.kiLabel.Text = "Ki:";
            // 
            // kpNumeric
            // 
            this.kpNumeric.DecimalPlaces = 4;
            this.kpNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.kpNumeric.Location = new System.Drawing.Point(128, 73);
            this.kpNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.kpNumeric.Name = "kpNumeric";
            this.kpNumeric.Size = new System.Drawing.Size(69, 20);
            this.kpNumeric.TabIndex = 9;
            // 
            // kpLabel
            // 
            this.kpLabel.AutoSize = true;
            this.kpLabel.Location = new System.Drawing.Point(104, 75);
            this.kpLabel.Name = "kpLabel";
            this.kpLabel.Size = new System.Drawing.Size(23, 13);
            this.kpLabel.TabIndex = 8;
            this.kpLabel.Text = "Kp:";
            // 
            // controllerParametersLabel
            // 
            this.controllerParametersLabel.AutoSize = true;
            this.controllerParametersLabel.Location = new System.Drawing.Point(5, 56);
            this.controllerParametersLabel.Name = "controllerParametersLabel";
            this.controllerParametersLabel.Size = new System.Drawing.Size(109, 13);
            this.controllerParametersLabel.TabIndex = 14;
            this.controllerParametersLabel.Text = "Controller parameters:";
            // 
            // PlatooningControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.controllerParametersLabel);
            this.Controls.Add(this.kdNumeric);
            this.Controls.Add(this.kdLabel);
            this.Controls.Add(this.kiNumeric);
            this.Controls.Add(this.kiLabel);
            this.Controls.Add(this.kpNumeric);
            this.Controls.Add(this.kpLabel);
            this.Controls.Add(this.distanceNumeric);
            this.Controls.Add(this.distanceLabel);
            this.Controls.Add(this.carToFollowIDDropDown);
            this.Controls.Add(this.carToFollowLabel);
            this.Name = "PlatooningControlPanel";
            this.Size = new System.Drawing.Size(200, 138);
            ((System.ComponentModel.ISupportInitialize)(this.distanceNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label carToFollowLabel;
        private System.Windows.Forms.ComboBox carToFollowIDDropDown;
        private System.Windows.Forms.Label distanceLabel;
        private System.Windows.Forms.NumericUpDown distanceNumeric;
        private System.Windows.Forms.NumericUpDown kdNumeric;
        private System.Windows.Forms.Label kdLabel;
        private System.Windows.Forms.NumericUpDown kiNumeric;
        private System.Windows.Forms.Label kiLabel;
        private System.Windows.Forms.NumericUpDown kpNumeric;
        private System.Windows.Forms.Label kpLabel;
        private System.Windows.Forms.Label controllerParametersLabel;
    }
}
