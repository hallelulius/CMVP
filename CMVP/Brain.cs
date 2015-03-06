using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace CMVP
{
    class Brain
    {
        
        private List<Car> cars;
        public PerformanceAnalyzerWindow analyzer;

        public void run()
        {
            cars = Program.cars;
            Stopwatch time = new Stopwatch();
            long dt = 0;
            long startTime;

            time.Start();
            while (true)
            {
                startTime = time.ElapsedMilliseconds;
             
                foreach(Car car in cars)
                {
                    car.updateState();
                }
                foreach (Car car in cars)
                {
                    if (car.getControlStrategy() != null)
                    {
                        car.getControlStrategy().updateReferencePoint();
                    }
                }
                foreach (Car car in cars)
                {
                    car.getController().updateController();
                }
                foreach (Car car in cars)
                {
                    car.send();
                }
                
                dt = time.ElapsedMilliseconds - startTime;

                // Give values to analyzer
                if (analyzer != null) // Check if the analyzer is created. OBS: If it is created and destroyed it is not garantued to be null, thus the next if-statement.
                {
                    if (!analyzer.IsDisposed)
                    {
                        // Give car values to analyzer:
                        foreach (Car car in cars)
                        {
                            string s = "Car " + car.ID + " ";
                            sendDataThreadSafe(s + "velocity", Convert.ToDouble(time.ElapsedMilliseconds) / 1000, cars.Find(x => x.ID == car.ID).getSpeed());
                            // Add more sendDataThreadSafe calls here.
                        }

                        // Give other values to analyzer:
                        sendDataThreadSafe("Brain execution time", Convert.ToDouble(time.ElapsedMilliseconds) / 1000.0, Convert.ToDouble(dt) / 1000.0);
                        // Add more sendDataThreadSafe calls here.
                    }
                }

                Thread.Sleep(3);
            }
        }

        private void sendDataThreadSafe(string reciever, double x, double y)
        {
            if(analyzer != null)
            {
                if (!analyzer.IsDisposed)
                {
                    if (analyzer.InvokeRequired)
                    {
                        try
                        {
                            analyzer.Invoke(analyzer.myDelegate, new object[] { reciever, x, y });
                        }
                        catch (Exception e)
                        {

                        }
                    }
                    else
                    {
                        analyzer.addData(reciever, x, y);
                    }
                }
            }
        }
    }
}
