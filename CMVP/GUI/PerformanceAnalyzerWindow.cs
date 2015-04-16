using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CMVP
{
    public partial class PerformanceAnalyzerWindow : Form
    {
        public delegate void sendDataDelegate(string text, double x, double y);
        public sendDataDelegate myDelegate;
        private int maxValuesStored = 300;
        private Dictionary<Series, Series> seriesOfAllDataPoints;

        public PerformanceAnalyzerWindow()
        {
            InitializeComponent();
            myDelegate = new sendDataDelegate(addData);
            performanceChart.ChartAreas[0].AxisY.StripLines.Add(new StripLine());
            performanceChart.ChartAreas[0].AxisY.StripLines[0].Interval = 2;
        }

        public void addData(string reciever, double x, double y)
        {
            Series s = performanceChart.Series.FindByName(reciever); // Att lägga till i Brain: en lista med strings, där varjer string motsvarar en serie. Listan uppdateras löpande.
            if (s != null)
            {
                while (s.Points.Count >= maxValuesStored)
                    s.Points.Remove(s.Points.First());
                s.Points.AddXY(x, y);
                performanceChart.ChartAreas[0].RecalculateAxesScale();
                //Console.WriteLine("Adding data point: " + y);
                Series allDataPoints;
                if(seriesOfAllDataPoints.TryGetValue(s,out allDataPoints))
                {
                    allDataPoints.Points.AddXY(x, y);
                }
            }
        }

        private void addSeriesDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (addSeriesDropDown.SelectedIndex != -1)
            {
                //Check if the series is already present:
                bool alreadyPresent = false;
                foreach (Control c in seriesPanel.Controls)
                {
                    alreadyPresent = (c.ToString() == addSeriesDropDown.SelectedItem.ToString());
                    if (alreadyPresent) break;
                }

                //Add the right control:
                if (!alreadyPresent)
                {
                    //SeriesControl sc;
                    string name = addSeriesDropDown.SelectedItem.ToString();

                    Series series = new Series(name);
                    seriesOfAllDataPoints.Add(series, new Series(name));
                    performanceChart.Series.Add(series);
                    series.ChartType = SeriesChartType.FastLine;
                    SeriesControl sc = new SeriesControl(name, performanceChart);
                    sc.Location = new Point(0, sc.Size.Height * seriesPanel.Controls.Count);
                    seriesPanel.Controls.Add(sc);
                }

                //addSeriesDropDown.SelectedIndex = -1;
            }
        }

        private void PerformanceAnalyzerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void dataPointsNumeric_ValueChanged(object sender, EventArgs e)
        {
            maxValuesStored = (int)dataPointsNumeric.Value;
        }

        private void performanceChart_Click(object sender, EventArgs e)
        {

        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            FileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Matlab scripts (*.m;*.txt)|*.m;*.txt";
            dialog.ShowDialog();

            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(dialog.FileName);

                file.WriteLine("%% Script exported from MVP");
                file.WriteLine("hold on;");
                file.WriteLine("axis on;\n");

                int seriesCount = 0;
                StringBuilder legend = new StringBuilder();
                legend.Append("legend(");
                foreach (Control c in seriesPanel.Controls)
                {
                    seriesCount++;
                    file.WriteLine("% " + c.ToString() + ":");

                    StringBuilder xValues = new StringBuilder();
                    StringBuilder yValues = new StringBuilder();


                    Series allDataPoints;
                    if (seriesOfAllDataPoints.TryGetValue(performanceChart.Series.FindByName(c.ToString()), out allDataPoints))
                    {
                        foreach (DataPoint p in allDataPoints.Points)
                        {
                            xValues.Append(" " + p.XValue);
                            yValues.Append(" " + p.YValues[0]);
                        }
                    }
                    file.WriteLine("X" + seriesCount + " = [" + xValues.ToString().Replace(',', '.') + " ];");
                    file.WriteLine("Y" + seriesCount + " = [" + yValues.ToString().Replace(',', '.') + " ];");
                    file.WriteLine("plot(X" + seriesCount + ", Y" + seriesCount + ");\n");
                    legend.Append("'" + c.ToString() + "', ");
                }
                legend.Append("'Location', 'southeast');");
                file.WriteLine(legend.ToString());
                file.WriteLine("hold off");

                file.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /*
        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x0112) // WM_SYSCOMMAND
            {

                // Check your window state here
                if (m.WParam == new IntPtr(0xF030)) // Maximize event - SC_MAXIMIZE from Winuser.h
                {
                    // THe window is being maximized
                    //Rectangle rec = Screen.GetBounds;
                    Screen myScreen = Screen.FromControl(this);
                    int width = myScreen.Bounds.Width;
                    int height = myScreen.Bounds.Height;
                    performanceChart.Size.Width = (width - width / 2);
                    performanceChart.Size.Height= height - height / 2;

                }
                if (m.WParam == new IntPtr(0xF120)) // Maximize event - SC_RESTORE from Winuser.h
                {
                    // THe window is being maximized
                }
                if (m.WParam == new IntPtr(0XF020)) // Maximize event - SC_MINIMIZE from Winuser.h
                {
                    // THe window is being maximized
                }
            }
            base.WndProc(ref m);
        }
         * */
    }
}