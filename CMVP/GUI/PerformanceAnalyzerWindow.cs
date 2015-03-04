using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CMVP
{
    public partial class PerformanceAnalyzerWindow : Form
    {
        public delegate void sendDataDelegate(string text, double x, double y);
        public sendDataDelegate myDelegate;
        private float uppdateTime = 1; // Time between uppdates.
        private const int maxValuesStored = 10;

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
                if (s.Points.Count >= maxValuesStored)
                    s.Points.Remove(s.Points.First());
                s.Points.AddXY(x, y);
                performanceChart.ChartAreas[0].RecalculateAxesScale();
                //Console.WriteLine("Adding data point: " + y);
            }
        }

        private void addSeriesDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check if the series is already present:
            bool alreadyPresent = false;
            foreach(Control c in seriesPanel.Controls)
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
                performanceChart.Series.Add(series);
                series.ChartType = SeriesChartType.FastLine;
                SeriesControl sc = new SeriesControl(name, performanceChart);
                sc.Location = new Point(0, sc.Size.Height * seriesPanel.Controls.Count);
                seriesPanel.Controls.Add(sc);
            }

            //addSeriesDropDown.SelectedIndex = -1;
        }

        private void PerformanceAnalyzerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}