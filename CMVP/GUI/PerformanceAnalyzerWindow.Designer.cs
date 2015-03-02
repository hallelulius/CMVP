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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.performanceChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.addSeriesDropDown = new System.Windows.Forms.ComboBox();
            this.addSeriesLabel = new System.Windows.Forms.Label();
            this.seriesPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.performanceChart)).BeginInit();
            this.SuspendLayout();
            // 
            // performanceChart
            // 
            chartArea1.Name = "ChartArea1";
            this.performanceChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.performanceChart.Legends.Add(legend1);
            this.performanceChart.Location = new System.Drawing.Point(295, 12);
            this.performanceChart.Name = "performanceChart";
            this.performanceChart.Size = new System.Drawing.Size(643, 535);
            this.performanceChart.TabIndex = 0;
            this.performanceChart.Text = "chart1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(193, 483);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // addSeriesDropDown
            // 
            this.addSeriesDropDown.FormattingEnabled = true;
            this.addSeriesDropDown.Items.AddRange(new object[] {
            "Car 0 velocity",
            "Car 0 velocity reference signal",
            "Car 0 control signal",
            "Car 1 velocity",
            "Car 1 velocity reference signal",
            "Car 1 control signal",
            "Car 2 velocity",
            "Car 2 velocity reference signal",
            "Car 2 control signal",
            "Brain execution time"});
            this.addSeriesDropDown.Location = new System.Drawing.Point(78, 10);
            this.addSeriesDropDown.Name = "addSeriesDropDown";
            this.addSeriesDropDown.Size = new System.Drawing.Size(190, 21);
            this.addSeriesDropDown.TabIndex = 2;
            this.addSeriesDropDown.SelectedIndexChanged += new System.EventHandler(this.addSeriesDropDown_SelectedIndexChanged);
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
            this.seriesPanel.Location = new System.Drawing.Point(13, 41);
            this.seriesPanel.Name = "seriesPanel";
            this.seriesPanel.Size = new System.Drawing.Size(267, 436);
            this.seriesPanel.TabIndex = 4;
            // 
            // PerformanceAnalyzerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 559);
            this.Controls.Add(this.seriesPanel);
            this.Controls.Add(this.addSeriesLabel);
            this.Controls.Add(this.addSeriesDropDown);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.performanceChart);
            this.Name = "PerformanceAnalyzerWindow";
            this.Text = "Performance analyzer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PerformanceAnalyzerWindow_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.performanceChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart performanceChart;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox addSeriesDropDown;
        private System.Windows.Forms.Label addSeriesLabel;
        private System.Windows.Forms.Panel seriesPanel;


    }
}