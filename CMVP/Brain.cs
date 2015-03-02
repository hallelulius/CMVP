using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;

namespace CMVP
{
    class Brain
    {
        
        private List<Car> cars;
        public PerformanceAnalyzerWindow analyzer;

        public void run()
        {
            cars = Program.cars;
            Stopwatch totalTime = new Stopwatch();
            Stopwatch localTime = new Stopwatch();

            totalTime.Start();
            while (true)
            {
                localTime.Start();
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

                localTime.Stop();
                analyzer.addData("Brain execution time", new DataPoint(totalTime.ElapsedMilliseconds, localTime.ElapsedMilliseconds));
                Thread.Sleep(3);
            }
        }
    }
}
