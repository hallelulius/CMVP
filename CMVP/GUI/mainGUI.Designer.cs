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
            this.simulationBasePanel = new System.Windows.Forms.Panel();
            this.openPerformanceAnalyzerButton = new System.Windows.Forms.Button();
            this.openCameraControlButton = new System.Windows.Forms.Button();
            this.simulationPanelLabel = new System.Windows.Forms.Label();
            this.trackPanelLabel = new System.Windows.Forms.Label();
            this.trackBasePanel = new System.Windows.Forms.Panel();
            this.trackCancelButton = new System.Windows.Forms.Button();
            this.trackApplyButton = new System.Windows.Forms.Button();
            this.trackCarIDLabel = new System.Windows.Forms.Label();
            this.trackCarIDDropDown = new System.Windows.Forms.ComboBox();
            this.importTrackButton = new System.Windows.Forms.Button();
            this.trackTrackLabel = new System.Windows.Forms.Label();
            this.tracksDropDown = new System.Windows.Forms.ComboBox();
            this.trafficMaxSpeedNumeric = new System.Windows.Forms.NumericUpDown();
            this.trafficMaxSpeedLabel = new System.Windows.Forms.Label();
            this.controlStrategyTypePanel = new System.Windows.Forms.Panel();
            this.controlStrategyControlStrategyLabel = new System.Windows.Forms.Label();
            this.controlStrategyControlStrategyDropDown = new System.Windows.Forms.ComboBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.CarID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Xpos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ypos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Velocity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Heading = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SteeringControlSignal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThrottleControlSignal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stopSimulationButton = new System.Windows.Forms.Button();
            this.Initiate = new System.Windows.Forms.Button();
            this.dataGridTimeNumeric = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.deltaTimeLabel = new System.Windows.Forms.Label();
            this.calibration = new System.Windows.Forms.Button();
            this.controllerTypeDropDown = new System.Windows.Forms.ComboBox();
            this.controllerTypeLabel = new System.Windows.Forms.Label();
            this.controllerTypePanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.simulationBasePanel.SuspendLayout();
            this.trackBasePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trafficMaxSpeedNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTimeNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // startSimulationButton
            // 
            this.startSimulationButton.BackColor = System.Drawing.Color.GreenYellow;
            this.startSimulationButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.startSimulationButton.Enabled = false;
            this.startSimulationButton.FlatAppearance.BorderColor = System.Drawing.Color.GreenYellow;
            this.startSimulationButton.FlatAppearance.BorderSize = 0;
            this.startSimulationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startSimulationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startSimulationButton.Location = new System.Drawing.Point(12, 12);
            this.startSimulationButton.Name = "startSimulationButton";
            this.startSimulationButton.Size = new System.Drawing.Size(135, 38);
            this.startSimulationButton.TabIndex = 0;
            this.startSimulationButton.Text = "Start";
            this.startSimulationButton.UseVisualStyleBackColor = false;
            this.startSimulationButton.Click += new System.EventHandler(this.startSimulationButton_Click);
            // 
            // simulationBasePanel
            // 
            this.simulationBasePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.simulationBasePanel.Controls.Add(this.openPerformanceAnalyzerButton);
            this.simulationBasePanel.Controls.Add(this.openCameraControlButton);
            this.simulationBasePanel.Location = new System.Drawing.Point(12, 111);
            this.simulationBasePanel.Name = "simulationBasePanel";
            this.simulationBasePanel.Size = new System.Drawing.Size(200, 276);
            this.simulationBasePanel.TabIndex = 2;
            // 
            // openPerformanceAnalyzerButton
            // 
            this.openPerformanceAnalyzerButton.Location = new System.Drawing.Point(16, 40);
            this.openPerformanceAnalyzerButton.Name = "openPerformanceAnalyzerButton";
            this.openPerformanceAnalyzerButton.Size = new System.Drawing.Size(157, 23);
            this.openPerformanceAnalyzerButton.TabIndex = 8;
            this.openPerformanceAnalyzerButton.Text = "Open performance analyzer";
            this.openPerformanceAnalyzerButton.UseVisualStyleBackColor = true;
            this.openPerformanceAnalyzerButton.Click += new System.EventHandler(this.openPerformanceAnalyzerButton_Click);
            // 
            // openCameraControlButton
            // 
            this.openCameraControlButton.Location = new System.Drawing.Point(33, 13);
            this.openCameraControlButton.Name = "openCameraControlButton";
            this.openCameraControlButton.Size = new System.Drawing.Size(122, 23);
            this.openCameraControlButton.TabIndex = 7;
            this.openCameraControlButton.Text = "Open camera control";
            this.openCameraControlButton.UseVisualStyleBackColor = true;
            this.openCameraControlButton.Click += new System.EventHandler(this.openCameraControlButton_Click);
            // 
            // simulationPanelLabel
            // 
            this.simulationPanelLabel.AutoSize = true;
            this.simulationPanelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simulationPanelLabel.Location = new System.Drawing.Point(16, 102);
            this.simulationPanelLabel.Name = "simulationPanelLabel";
            this.simulationPanelLabel.Size = new System.Drawing.Size(116, 17);
            this.simulationPanelLabel.TabIndex = 3;
            this.simulationPanelLabel.Text = "System Overview";
            // 
            // trackPanelLabel
            // 
            this.trackPanelLabel.AutoSize = true;
            this.trackPanelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackPanelLabel.Location = new System.Drawing.Point(222, 102);
            this.trackPanelLabel.Name = "trackPanelLabel";
            this.trackPanelLabel.Size = new System.Drawing.Size(85, 17);
            this.trackPanelLabel.TabIndex = 5;
            this.trackPanelLabel.Text = "Car Settings";
            // 
            // trackBasePanel
            // 
            this.trackBasePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trackBasePanel.Controls.Add(this.button1);
            this.trackBasePanel.Controls.Add(this.panel1);
            this.trackBasePanel.Controls.Add(this.trackCancelButton);
            this.trackBasePanel.Controls.Add(this.controllerTypePanel);
            this.trackBasePanel.Controls.Add(this.trackApplyButton);
            this.trackBasePanel.Controls.Add(this.controllerTypeLabel);
            this.trackBasePanel.Controls.Add(this.trackCarIDLabel);
            this.trackBasePanel.Controls.Add(this.controllerTypeDropDown);
            this.trackBasePanel.Controls.Add(this.controlStrategyTypePanel);
            this.trackBasePanel.Controls.Add(this.trackCarIDDropDown);
            this.trackBasePanel.Controls.Add(this.trafficMaxSpeedNumeric);
            this.trackBasePanel.Controls.Add(this.controlStrategyControlStrategyLabel);
            this.trackBasePanel.Controls.Add(this.trafficMaxSpeedLabel);
            this.trackBasePanel.Controls.Add(this.controlStrategyControlStrategyDropDown);
            this.trackBasePanel.Controls.Add(this.importTrackButton);
            this.trackBasePanel.Controls.Add(this.trackTrackLabel);
            this.trackBasePanel.Controls.Add(this.tracksDropDown);
            this.trackBasePanel.Location = new System.Drawing.Point(218, 111);
            this.trackBasePanel.Name = "trackBasePanel";
            this.trackBasePanel.Size = new System.Drawing.Size(409, 276);
            this.trackBasePanel.TabIndex = 4;
            // 
            // trackCancelButton
            // 
            this.trackCancelButton.Location = new System.Drawing.Point(278, 248);
            this.trackCancelButton.Name = "trackCancelButton";
            this.trackCancelButton.Size = new System.Drawing.Size(60, 23);
            this.trackCancelButton.TabIndex = 10;
            this.trackCancelButton.Text = "Clear";
            this.trackCancelButton.UseVisualStyleBackColor = true;
            this.trackCancelButton.Click += new System.EventHandler(this.trackCancelButton_Click);
            // 
            // trackApplyButton
            // 
            this.trackApplyButton.Location = new System.Drawing.Point(344, 248);
            this.trackApplyButton.Name = "trackApplyButton";
            this.trackApplyButton.Size = new System.Drawing.Size(60, 23);
            this.trackApplyButton.TabIndex = 9;
            this.trackApplyButton.Text = "Apply";
            this.trackApplyButton.UseVisualStyleBackColor = true;
            this.trackApplyButton.Click += new System.EventHandler(this.trackApplyButton_Click);
            // 
            // trackCarIDLabel
            // 
            this.trackCarIDLabel.AutoSize = true;
            this.trackCarIDLabel.Location = new System.Drawing.Point(3, 17);
            this.trackCarIDLabel.Name = "trackCarIDLabel";
            this.trackCarIDLabel.Size = new System.Drawing.Size(40, 13);
            this.trackCarIDLabel.TabIndex = 6;
            this.trackCarIDLabel.Text = "Car ID:";
            // 
            // trackCarIDDropDown
            // 
            this.trackCarIDDropDown.FormattingEnabled = true;
            this.trackCarIDDropDown.Location = new System.Drawing.Point(49, 14);
            this.trackCarIDDropDown.Name = "trackCarIDDropDown";
            this.trackCarIDDropDown.Size = new System.Drawing.Size(42, 21);
            this.trackCarIDDropDown.TabIndex = 5;
            this.trackCarIDDropDown.DropDown += new System.EventHandler(this.trackCarIDDropDown_DropDown);
            // 
            // importTrackButton
            // 
            this.importTrackButton.Location = new System.Drawing.Point(148, 44);
            this.importTrackButton.Name = "importTrackButton";
            this.importTrackButton.Size = new System.Drawing.Size(47, 23);
            this.importTrackButton.TabIndex = 4;
            this.importTrackButton.Text = "Import";
            this.importTrackButton.UseVisualStyleBackColor = true;
            this.importTrackButton.Click += new System.EventHandler(this.importTrackButton_Click);
            // 
            // trackTrackLabel
            // 
            this.trackTrackLabel.AutoSize = true;
            this.trackTrackLabel.Location = new System.Drawing.Point(3, 48);
            this.trackTrackLabel.Name = "trackTrackLabel";
            this.trackTrackLabel.Size = new System.Drawing.Size(38, 13);
            this.trackTrackLabel.TabIndex = 3;
            this.trackTrackLabel.Text = "Track:";
            // 
            // tracksDropDown
            // 
            this.tracksDropDown.FormattingEnabled = true;
            this.tracksDropDown.Location = new System.Drawing.Point(47, 45);
            this.tracksDropDown.Name = "tracksDropDown";
            this.tracksDropDown.Size = new System.Drawing.Size(95, 21);
            this.tracksDropDown.TabIndex = 2;
            // 
            // trafficMaxSpeedNumeric
            // 
            this.trafficMaxSpeedNumeric.DecimalPlaces = 1;
            this.trafficMaxSpeedNumeric.Location = new System.Drawing.Point(71, 73);
            this.trafficMaxSpeedNumeric.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.trafficMaxSpeedNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.trafficMaxSpeedNumeric.Name = "trafficMaxSpeedNumeric";
            this.trafficMaxSpeedNumeric.Size = new System.Drawing.Size(48, 20);
            this.trafficMaxSpeedNumeric.TabIndex = 9;
            this.trafficMaxSpeedNumeric.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.trafficMaxSpeedNumeric.ValueChanged += new System.EventHandler(this.trafficMaxSpeedNumeric_ValueChanged);
            // 
            // trafficMaxSpeedLabel
            // 
            this.trafficMaxSpeedLabel.AutoSize = true;
            this.trafficMaxSpeedLabel.Location = new System.Drawing.Point(3, 75);
            this.trafficMaxSpeedLabel.Name = "trafficMaxSpeedLabel";
            this.trafficMaxSpeedLabel.Size = new System.Drawing.Size(62, 13);
            this.trafficMaxSpeedLabel.TabIndex = 8;
            this.trafficMaxSpeedLabel.Text = "Max speed:";
            // 
            // controlStrategyTypePanel
            // 
            this.controlStrategyTypePanel.Location = new System.Drawing.Point(0, 126);
            this.controlStrategyTypePanel.Name = "controlStrategyTypePanel";
            this.controlStrategyTypePanel.Size = new System.Drawing.Size(200, 138);
            this.controlStrategyTypePanel.TabIndex = 7;
            // 
            // controlStrategyControlStrategyLabel
            // 
            this.controlStrategyControlStrategyLabel.AutoSize = true;
            this.controlStrategyControlStrategyLabel.Location = new System.Drawing.Point(3, 102);
            this.controlStrategyControlStrategyLabel.Name = "controlStrategyControlStrategyLabel";
            this.controlStrategyControlStrategyLabel.Size = new System.Drawing.Size(85, 13);
            this.controlStrategyControlStrategyLabel.TabIndex = 5;
            this.controlStrategyControlStrategyLabel.Text = "Control Strategy:";
            // 
            // controlStrategyControlStrategyDropDown
            // 
            this.controlStrategyControlStrategyDropDown.FormattingEnabled = true;
            this.controlStrategyControlStrategyDropDown.Items.AddRange(new object[] {
            "Stand still",
            "Follow track",
            "Overtaking",
            "Step response"});
            this.controlStrategyControlStrategyDropDown.Location = new System.Drawing.Point(94, 99);
            this.controlStrategyControlStrategyDropDown.Name = "controlStrategyControlStrategyDropDown";
            this.controlStrategyControlStrategyDropDown.Size = new System.Drawing.Size(101, 21);
            this.controlStrategyControlStrategyDropDown.TabIndex = 4;
            this.controlStrategyControlStrategyDropDown.SelectedIndexChanged += new System.EventHandler(this.controlStrategyControlStrategyDropDown_SelectedIndexChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "track.txt";
            this.openFileDialog.Filter = "Text files (*.txt)|*.txt;";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CarID,
            this.Xpos,
            this.Ypos,
            this.Velocity,
            this.Heading,
            this.SteeringControlSignal,
            this.ThrottleControlSignal});
            this.dataGridView.Location = new System.Drawing.Point(11, 419);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(819, 153);
            this.dataGridView.TabIndex = 6;
            // 
            // CarID
            // 
            this.CarID.HeaderText = "Car ID";
            this.CarID.Name = "CarID";
            // 
            // Xpos
            // 
            this.Xpos.HeaderText = "X pos";
            this.Xpos.Name = "Xpos";
            // 
            // Ypos
            // 
            this.Ypos.HeaderText = "Y pos";
            this.Ypos.Name = "Ypos";
            // 
            // Velocity
            // 
            this.Velocity.HeaderText = "Velocity";
            this.Velocity.Name = "Velocity";
            // 
            // Heading
            // 
            this.Heading.HeaderText = "Heading (degrees)";
            this.Heading.Name = "Heading";
            // 
            // SteeringControlSignal
            // 
            this.SteeringControlSignal.HeaderText = "Steering Control Signal";
            this.SteeringControlSignal.Name = "SteeringControlSignal";
            // 
            // ThrottleControlSignal
            // 
            this.ThrottleControlSignal.HeaderText = "Throttle control signal";
            this.ThrottleControlSignal.Name = "ThrottleControlSignal";
            // 
            // stopSimulationButton
            // 
            this.stopSimulationButton.BackColor = System.Drawing.Color.Crimson;
            this.stopSimulationButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.stopSimulationButton.FlatAppearance.BorderColor = System.Drawing.Color.GreenYellow;
            this.stopSimulationButton.FlatAppearance.BorderSize = 0;
            this.stopSimulationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopSimulationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopSimulationButton.Location = new System.Drawing.Point(153, 12);
            this.stopSimulationButton.Name = "stopSimulationButton";
            this.stopSimulationButton.Size = new System.Drawing.Size(135, 38);
            this.stopSimulationButton.TabIndex = 1;
            this.stopSimulationButton.Text = "Stop";
            this.stopSimulationButton.UseVisualStyleBackColor = false;
            this.stopSimulationButton.Click += new System.EventHandler(this.stopSimulationButton_Click);
            // 
            // Initiate
            // 
            this.Initiate.BackColor = System.Drawing.Color.Gold;
            this.Initiate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Initiate.FlatAppearance.BorderColor = System.Drawing.Color.GreenYellow;
            this.Initiate.FlatAppearance.BorderSize = 0;
            this.Initiate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Initiate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Initiate.Location = new System.Drawing.Point(294, 12);
            this.Initiate.Name = "Initiate";
            this.Initiate.Size = new System.Drawing.Size(135, 38);
            this.Initiate.TabIndex = 7;
            this.Initiate.Text = "Initiate";
            this.Initiate.UseVisualStyleBackColor = false;
            this.Initiate.Click += new System.EventHandler(this.Initiate_Click);
            // 
            // dataGridTimeNumeric
            // 
            this.dataGridTimeNumeric.Location = new System.Drawing.Point(147, 393);
            this.dataGridTimeNumeric.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.dataGridTimeNumeric.Name = "dataGridTimeNumeric";
            this.dataGridTimeNumeric.Size = new System.Drawing.Size(64, 20);
            this.dataGridTimeNumeric.TabIndex = 9;
            this.dataGridTimeNumeric.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.dataGridTimeNumeric.ValueChanged += new System.EventHandler(this.dataGridTimeNumeric_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 395);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Data grid update time (ms):";
            // 
            // deltaTimeLabel
            // 
            this.deltaTimeLabel.AutoSize = true;
            this.deltaTimeLabel.Location = new System.Drawing.Point(225, 395);
            this.deltaTimeLabel.Name = "deltaTimeLabel";
            this.deltaTimeLabel.Size = new System.Drawing.Size(69, 13);
            this.deltaTimeLabel.TabIndex = 13;
            this.deltaTimeLabel.Text = "Delta time : 0";
            // 
            // calibration
            // 
            this.calibration.BackColor = System.Drawing.Color.DodgerBlue;
            this.calibration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.calibration.FlatAppearance.BorderColor = System.Drawing.Color.GreenYellow;
            this.calibration.FlatAppearance.BorderSize = 0;
            this.calibration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calibration.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calibration.Location = new System.Drawing.Point(435, 12);
            this.calibration.Name = "calibration";
            this.calibration.Size = new System.Drawing.Size(135, 38);
            this.calibration.TabIndex = 14;
            this.calibration.Text = "Calibration";
            this.calibration.UseVisualStyleBackColor = false;
            this.calibration.Click += new System.EventHandler(this.calibration_Click);
            // 
            // controllerTypeDropDown
            // 
            this.controllerTypeDropDown.FormattingEnabled = true;
            this.controllerTypeDropDown.Items.AddRange(new object[] {
            "PID",
            "Manual keyboard"});
            this.controllerTypeDropDown.Location = new System.Drawing.Point(293, 13);
            this.controllerTypeDropDown.Name = "controllerTypeDropDown";
            this.controllerTypeDropDown.Size = new System.Drawing.Size(108, 21);
            this.controllerTypeDropDown.TabIndex = 4;
            this.controllerTypeDropDown.SelectedIndexChanged += new System.EventHandler(this.controllerTypeDropDown_SelectedIndexChanged);
            // 
            // controllerTypeLabel
            // 
            this.controllerTypeLabel.AutoSize = true;
            this.controllerTypeLabel.Location = new System.Drawing.Point(210, 16);
            this.controllerTypeLabel.Name = "controllerTypeLabel";
            this.controllerTypeLabel.Size = new System.Drawing.Size(77, 13);
            this.controllerTypeLabel.TabIndex = 5;
            this.controllerTypeLabel.Text = "Controller type:";
            // 
            // controllerTypePanel
            // 
            this.controllerTypePanel.Location = new System.Drawing.Point(206, 40);
            this.controllerTypePanel.Name = "controllerTypePanel";
            this.controllerTypePanel.Size = new System.Drawing.Size(200, 159);
            this.controllerTypePanel.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(205, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 248);
            this.panel1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(125, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // mainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 584);
            this.Controls.Add(this.calibration);
            this.Controls.Add(this.deltaTimeLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridTimeNumeric);
            this.Controls.Add(this.Initiate);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.trackPanelLabel);
            this.Controls.Add(this.trackBasePanel);
            this.Controls.Add(this.simulationPanelLabel);
            this.Controls.Add(this.stopSimulationButton);
            this.Controls.Add(this.startSimulationButton);
            this.Controls.Add(this.simulationBasePanel);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "mainGUI";
            this.Text = "CMVP";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainGUI_FormClosed);
            this.simulationBasePanel.ResumeLayout(false);
            this.trackBasePanel.ResumeLayout(false);
            this.trackBasePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trafficMaxSpeedNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTimeNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Button startSimulationButton;
        private System.Windows.Forms.Panel simulationBasePanel;
        private System.Windows.Forms.Label simulationPanelLabel;
        private System.Windows.Forms.Label trackPanelLabel;
        private System.Windows.Forms.Panel trackBasePanel;
        private System.Windows.Forms.Label trackTrackLabel;
        private System.Windows.Forms.ComboBox tracksDropDown;
        private System.Windows.Forms.Panel controlStrategyTypePanel;
        private System.Windows.Forms.Label controlStrategyControlStrategyLabel;
        private System.Windows.Forms.ComboBox controlStrategyControlStrategyDropDown;
        private System.Windows.Forms.Button openCameraControlButton;
        private System.Windows.Forms.NumericUpDown trafficMaxSpeedNumeric;
        private System.Windows.Forms.Label trafficMaxSpeedLabel;
        private System.Windows.Forms.Button importTrackButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label trackCarIDLabel;
        private System.Windows.Forms.ComboBox trackCarIDDropDown;
        private System.Windows.Forms.Button trackCancelButton;
        private System.Windows.Forms.Button trackApplyButton;
        private System.Windows.Forms.Button openPerformanceAnalyzerButton;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button stopSimulationButton;
        private System.Windows.Forms.Button Initiate;
        private System.Windows.Forms.NumericUpDown dataGridTimeNumeric;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Xpos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ypos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Velocity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Heading;
        private System.Windows.Forms.DataGridViewTextBoxColumn SteeringControlSignal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThrottleControlSignal;
        private System.Windows.Forms.Label deltaTimeLabel;
        private System.Windows.Forms.Button calibration;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel controllerTypePanel;
        private System.Windows.Forms.Label controllerTypeLabel;
        private System.Windows.Forms.ComboBox controllerTypeDropDown;
        private System.Windows.Forms.Button button1;
    }
}