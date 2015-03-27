namespace CMVP
{
    partial class PerformanceAnalyzerWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.performanceChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.addSeriesDropDown = new System.Windows.Forms.ComboBox();
            this.addSeriesLabel = new System.Windows.Forms.Label();
            this.seriesPanel = new System.Windows.Forms.Panel();
            this.maxDataPointsLabel = new System.Windows.Forms.Label();
            this.dataPointsNumeric = new System.Windows.Forms.NumericUpDown();
            this.exportButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.performanceChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPointsNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // performanceChart
            // 
            chartArea4.Name = "ChartArea1";
            this.performanceChart.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.performanceChart.Legends.Add(legend4);
            this.performanceChart.Location = new System.Drawing.Point(295, 12);
            this.performanceChart.Name = "performanceChart";
            this.performanceChart.Size = new System.Drawing.Size(643, 535);
            this.performanceChart.TabIndex = 0;
            this.performanceChart.Text = "chart1";
            this.performanceChart.Click += new System.EventHandler(this.performanceChart_Click);
            // 
            // addSeriesDropDown
            // 
            this.addSeriesDropDown.FormattingEnabled = true;
            this.addSeriesDropDown.Items.AddRange(new object[] {
            "Car 0 velocity",
            "Car 0 velocity reference signal",
            "Car 0 steer control signal",
            "Car 0 throttle control signal",
            "Car 1 velocity",
            "Car 1 velocity reference signal",
            "Car 1 steer control signal",
            "Car 1 throttle control signal",
            "Car 2 velocity",
            "Car 2 velocity reference signal",
            "Car 2 steer control signal",
            "Car 2 throttle control signal",
            "Brain execution time"});
            this.addSeriesDropDown.Location = new System.Drawing.Point(78, 10);
            this.addSeriesDropDown.Name = "addSeriesDropDown";
            this.addSeriesDropDown.Size = new System.Drawing.Size(190, 21);
            this.addSeriesDropDown.TabIndex = 2;
            this.addSeriesDropDown.DropDownClosed += new System.EventHandler(this.addSeriesDropDown_SelectedIndexChanged);
            // 
            // addSeriesLabel
            // 
            this.addSeriesLabel.AutoSize = true;
            this.addSeriesLabel.Location = new System.Drawing.Point(13, 13);
            this.addSeriesLabel.Name = "addSeriesLabel";
            this.addSeriesLabel.Size = new System.Drawing.Size(59, 13);
            this.addSeriesLabel.TabIndex = 3;
            this.addSeriesLabel.Text = "Add series:";
            // 
            // seriesPanel
            // 
            this.seriesPanel.Location = new System.Drawing.Point(13, 97);
            this.seriesPanel.Name = "seriesPanel";
            this.seriesPanel.Size = new System.Drawing.Size(267, 380);
            this.seriesPanel.TabIndex = 4;
            // 
            // maxDataPointsLabel
            // 
            this.maxDataPointsLabel.AutoSize = true;
            this.maxDataPointsLabel.Location = new System.Drawing.Point(13, 43);
            this.maxDataPointsLabel.Name = "maxDataPointsLabel";
            this.maxDataPointsLabel.Size = new System.Drawing.Size(85, 13);
            this.maxDataPointsLabel.TabIndex = 5;
            this.maxDataPointsLabel.Text = "Max data points:";
            // 
            // dataPointsNumeric
            // 
            this.dataPointsNumeric.Location = new System.Drawing.Point(104, 41);
            this.dataPointsNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.dataPointsNumeric.Name = "dataPointsNumeric";
            this.dataPointsNumeric.Size = new System.Drawing.Size(164, 20);
            this.dataPointsNumeric.TabIndex = 6;
            this.dataPointsNumeric.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.dataPointsNumeric.ValueChanged += new System.EventHandler(this.dataPointsNumeric_ValueChanged);
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(135, 67);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(133, 23);
            this.exportButton.TabIndex = 7;
            this.exportButton.Text = "Export data as *.m";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // PerformanceAnalyzerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 559);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.dataPointsNumeric);
            this.Controls.Add(this.maxDataPointsLabel);
            this.Controls.Add(this.seriesPanel);
            this.Controls.Add(this.addSeriesLabel);
            this.Controls.Add(this.addSeriesDropDown);
            this.Controls.Add(this.performanceChart);
            this.Name = "PerformanceAnalyzerWindow";
            this.Text = "Performance analyzer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PerformanceAnalyzerWindow_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.performanceChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPointsNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart performanceChart;
        private System.Windows.Forms.ComboBox addSeriesDropDown;
        private System.Windows.Forms.Label addSeriesLabel;
        private System.Windows.Forms.Panel seriesPanel;
        private System.Windows.Forms.Label maxDataPointsLabel;
        private System.Windows.Forms.NumericUpDown dataPointsNumeric;
        private System.Windows.Forms.Button exportButton;


    }
}