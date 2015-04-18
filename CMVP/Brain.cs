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
    /// <summary>
    /// This class updates all cars with the current states, sets the new reference point and send new values to the DAC according to the control strategy
    /// Also handles the performance analyzer
    /// </summary>
    class Brain
    {

        private List<Car> cars;
        private static EventWaitHandle wh = new ManualResetEvent(false);
        public PerformanceAnalyzerWindow analyzer;


        public void start()
        {
            Thread thread = new Thread(run);
            thread.Start();
        }
        public void run()
        {
            cars = Program.cars;
            Stopwatch time = new Stopwatch();
            long dt = 0;
            long startTime;

            time.Start();
            while (true)
            {
                wh.WaitOne();
                startTime = time.ElapsedMilliseconds;

                foreach (Car car in cars)
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
                    if (car.found)
                    {
                        car.send();
                    }
                    else
                    {
                       // car.stop();
                        car.send();
                        Console.WriteLine("Car not found");
                    }
                }
                dt = time.ElapsedMilliseconds - startTime;

                // Give values to analyzer
                if (analyzer != null) // Check if the analyzer is created. OBS: If it is created and destroyed it is not garantued to be null, thus the next if-statement.
                {
                    if (!analyzer.IsDisposed)
                    {
                        // Give car values to analyzer:
                        double xValue = Convert.ToDouble(time.ElapsedMilliseconds) / 1000;
                        foreach (Car car in cars)
                        {
                            string s = "Car " + car.ID + " ";
                            sendDataThreadSafe(s + "velocity", xValue, car.getSpeed());
                            sendDataThreadSafe(s + "velocity reference signal", xValue, car.getController().getRefSpeed() * (double)car.getMaxSpeed());
                            sendDataThreadSafe(s + "steer control signal", xValue, car.getController().getSteer());
                            sendDataThreadSafe(s + "throttle control signal", xValue, car.getController().getThrottle());
                            sendDataThreadSafe(s + "heading reference signal", xValue, car.getController().getRefHeading());
                            sendDataThreadSafe(s + "reference position X-axis", xValue, car.getController().getRefPoint().X);
                            sendDataThreadSafe(s + "reference position Y-axis", xValue, car.getController().getRefPoint().Y);
                            sendDataThreadSafe(s + "position X-axis", xValue, car.getPosition().X);
                            sendDataThreadSafe(s + "position Y-axis", xValue, car.getPosition().Y);
                            sendDataThreadSafe(s + "found", xValue, Convert.ToDouble((car.found)));
                            
                            // Add more sendDataThreadSafe calls here.
                        }
                        // Give other values to analyzer:
                        if (cars.First() != null)
                        {
                            sendDataThreadSafe("FPS image processing", xValue, 1 / cars.First().getDeltaTime());
                        }
                        sendDataThreadSafe("Brain execution time", Convert.ToDouble(time.ElapsedMilliseconds) / 1000.0, Convert.ToDouble(dt) / 1000.0);
                        // Add more sendDataThreadSafe calls here.
                    }
                }
                Thread.Sleep(1);

            }
        }

        public void StartWorking()
        {
            wh.Set();
        }
        public void StopWorking()
        {
            wh.Reset();
        }

        private void sendDataThreadSafe(string reciever, double x, double y)
        {
            if (analyzer != null)
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
                            Debug.WriteLine(e);
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
