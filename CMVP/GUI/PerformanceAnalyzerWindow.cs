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

        public PerformanceAnalyzerWindow()
        {
            InitializeComponent();
            myDelegate = new sendDataDelegate(addData);
        }

        public void addData(string reciever, double x, double y)
        {
            Series s = performanceChart.Series.FindByName(reciever); // Att lägga till i Brain: en lista med strings, där varjer string motsvarar en serie. Listan uppdateras löpande.
            if (s != null)
            {
                s.Points.AddXY(x, y);
                Console.WriteLine("Adding data point: " + y);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Car tempCar = Program.cars.Find(car => car.ID == 2);
            performanceChart.Series.Add("Car 1 position");
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

                performanceChart.Series.Add(name);
                performanceChart.Series.FindByName(name).ChartType = SeriesChartType.FastLine;
                SeriesControl sc = new SeriesControl(name, performanceChart);
                sc.Location = new Point(0, sc.Size.Height * seriesPanel.Controls.Count);
                seriesPanel.Controls.Add(sc);

                if(name == "Car 0 velocity")
                {
                    Series s = performanceChart.Series.FindByName(name);
                    s.Points.AddXY(0, 0.05);
                    s.Points.AddXY(0, 0.04);
                    s.Points.AddXY(0, 0.07);
                    s.Points.AddXY(0, 0.08);
                    s.Points.AddXY(0, 0.03);
                    s.Points.AddXY(0, 0.03);
                    s.Points.AddXY(0, 0.01);
                }

                if(name == "Car 1 velocity")
                {
                    Series s = performanceChart.Series.FindByName(name);
                    s.Points.AddXY(0, 0.05);
                    s.Points.AddXY(0, 0.16);
                    s.Points.AddXY(0, 0.03);
                    s.Points.AddXY(0, 0.05);
                    s.Points.AddXY(0, 0.08);
                    s.Points.AddXY(0, 0.09);
                    s.Points.AddXY(0, 0.11);
                }
                /*
                switch (addSeriesDropDown.SelectedItem.ToString())
                {

                    case "Car 0 velocity":
                        performanceChart.Series.Add("Car 0 velocity");
                        performanceChart.Series.FindByName("Car 0 velocity").ChartType = SeriesChartType.FastLine;
                        sc = new SeriesControl("Car 0 velocity", performanceChart);
                        sc.Location = new Point(0, sc.Size.Height * seriesPanel.Controls.Count);
                        seriesPanel.Controls.Add(sc);
                        break;

                    case "Car 0 velocity reference signal":
                        performanceChart.Series.Add("Car 0 velocity reference signal");
                        sc = new SeriesControl("Car 0 velocity reference signal", performanceChart);
                        sc.Location = new Point(0, sc.Size.Height * seriesPanel.Controls.Count);
                        seriesPanel.Controls.Add(sc);
                        break;

                    case "Car 0 control signal":
                        performanceChart.Series.Add("Car 0 control signal");
                        sc = new SeriesControl("Car 0 control signal", performanceChart);
                        sc.Location = new Point(0, sc.Size.Height * seriesPanel.Controls.Count);
                        seriesPanel.Controls.Add(sc);
                        break;

                    case "Car 1 velocity":
                        performanceChart.Series.Add("Car 1 velocity");
                        sc = new SeriesControl("Car 1 velocity", performanceChart);
                        sc.Location = new Point(0, sc.Size.Height * seriesPanel.Controls.Count);
                        seriesPanel.Controls.Add(sc);
                        break;

                    case "Car 1 velocity reference signal":
                        performanceChart.Series.Add("Car 1 velocity reference signal");
                        sc = new SeriesControl("Car 1 velocity reference signal", performanceChart);
                        sc.Location = new Point(0, sc.Size.Height * seriesPanel.Controls.Count);
                        seriesPanel.Controls.Add(sc);
                        break;

                    case "Car 1 control signal":
                        performanceChart.Series.Add("Car 1 control signal");
                        sc = new SeriesControl("Car 1 control signal", performanceChart);
                        sc.Location = new Point(0, sc.Size.Height * seriesPanel.Controls.Count);
                        seriesPanel.Controls.Add(sc);
                        break;

                    case "Car 2 velocity":
                        performanceChart.Series.Add("Car 2 velocity");
                        sc = new SeriesControl("Car 2 velocity", performanceChart);
                        sc.Location = new Point(0, sc.Size.Height * seriesPanel.Controls.Count);
                        seriesPanel.Controls.Add(sc);
                        break;

                    case "Car 2 velocity reference signal":
                        performanceChart.Series.Add("Car 2 velocity reference signal");
                        sc = new SeriesControl("Car 2 velocity reference signal", performanceChart);
                        sc.Location = new Point(0, sc.Size.Height * seriesPanel.Controls.Count);
                        seriesPanel.Controls.Add(sc);
                        break;

                    case "Car 2 control signal":
                        performanceChart.Series.Add("Car 2 control signal");
                        sc = new SeriesControl("Car 2 control signal", performanceChart);
                        sc.Location = new Point(0, sc.Size.Height * seriesPanel.Controls.Count);
                        seriesPanel.Controls.Add(sc);
                        break;
                }*/
            }

            //addSeriesDropDown.SelectedIndex = -1;
        }

        private void PerformanceAnalyzerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}