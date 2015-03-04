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
    //delegate void sendDataCallback(string text, double x, double y);

    class Brain
    {
        
        private List<Car> cars;
        public PerformanceAnalyzerWindow analyzer;
        private string[] analyzedValues =  
        {
            "Car 0 velocity",
            "Car 0 velocity reference signal",
            "Car 0 control signal",
            "Car 1 velocity",
            "Car 1 velocity reference signal",
            "Car 1 control signal",
            "Car 2 velocity",
            "Car 2 velocity reference signal",
            "Car 2 control signal",
            "Brain execution time"
        };

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

                // Give car values to analyzer:
                foreach (Car car in cars)
                {
                    string s = "Car " + car.ID + " ";
                    sendDataThreadSafe(s + "velocity", Convert.ToDouble(time.ElapsedMilliseconds) / 1000, cars.Find(x => x.ID == car.ID).getSpeed());
                }

                //Give other values to analyzer:
                sendDataThreadSafe("Brain execution time", Convert.ToDouble(time.ElapsedMilliseconds) / 1000.0, Convert.ToDouble(dt) / 1000.0); 
                
                Thread.Sleep(1000);
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
                        analyzer.Invoke(analyzer.myDelegate, new object[] { reciever, x, y });
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
