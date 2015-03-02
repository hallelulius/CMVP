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
        private double test = 0;

        public void run()
        {
            cars = Program.cars;
            Stopwatch totalTime = new Stopwatch();
            Stopwatch localTime = new Stopwatch();
            long dt = 0;
            long lastTime;

            localTime.Start();
            while (true)
            {
                lastTime = localTime.ElapsedMilliseconds;
             
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
                //Thread.Sleep(10);
                dt = localTime.ElapsedMilliseconds - lastTime;
                //localTime.Stop();
                if (analyzer != null)
                {
                    sendDataThreadSafe("Brain execution time", 0.0, Convert.ToDouble(dt) / 1000.0); // totalTime.ElapsedMilliseconds / 1000
                    Console.WriteLine(Convert.ToDouble(dt) / 1000.0);
                    //MessageBox.Show("Sent (" + test + ", " + ((double)localTime.ElapsedMilliseconds / 1000) + ") to the performance analyzer.");
                }
                Thread.Sleep(1000);
            }
        }

        private void sendDataThreadSafe(string reciever, double x, double y)
        {
            if (analyzer.InvokeRequired)
            {
                //sendDataCallback d = new sendDataCallback(sendDataThreadSafe);
                analyzer.Invoke(analyzer.myDelegate, new object[] { reciever, x, y });
            }
            else
            {
                analyzer.addData("Brain execution time", x, y);
            }
        }
    }
}
