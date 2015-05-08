using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.ComponentModel;


namespace CMVP
{
    /// <summary>
    /// This class updates all cars with the current states, sets the new reference point and send new values to the DAC according to the control strategy
    /// Also handles the performance analyzer
    /// </summary>
    class Brain
    {

        private List<Car> cars;
        private static EventWaitHandle whBrain;
        private static ManualResetEventSlim whDataUsed;
        public PerformanceAnalyzerWindow analyzer;
        private BackgroundWorker bw;
        private bool working;
        private Thread thread;

        public void start()
        {
            whBrain = Program.imageProcess.BRAIN_EVENT_HANDLE;
            whDataUsed = Program.imageProcess.DATA_USED;
            bw = new BackgroundWorker();
            bw.ProgressChanged += bw_ProgressChanged;
            bw.DoWork += bw_DoWork;
            bw.WorkerReportsProgress = true;
            bw.RunWorkerAsync();
            thread = new Thread(run);
            thread.Name = "Brain";
            thread.Start();
        }
        public void run()
        {
            cars = Program.cars;

            while (true)
            {
                whDataUsed.Reset();

                whBrain.WaitOne(); //wait for imageprocessing to finish
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
                if (working)
                {
                foreach (Car car in cars)
                {
                    car.getController().updateController();
                }
                    whDataUsed.Set();
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


                }

                else
                {
                    whBrain.WaitOne();
                    whDataUsed.Set();
                }
            }
        }

        public void StartWorking()
        {
            working = true;

        }
        public void StopWorking()
        {
            working = false;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
                }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch time = new Stopwatch();
            long dt = 0;
            long startTime;
            startTime = time.ElapsedMilliseconds;
            time.Start();
            while (true)
            {
                Thread.Sleep(0);
                if (working)
                {
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
                                sendDataThreadSafe(s + "throttle control signal", xValue, car.getController().getThrottle() * car.getMaxSpeed());
                            sendDataThreadSafe(s + "heading reference signal", xValue, car.getController().getRefHeading());
                            sendDataThreadSafe(s + "reference position X-axis", xValue, car.getController().getRefPoint().X);
                            sendDataThreadSafe(s + "reference position Y-axis", xValue, car.getController().getRefPoint().Y);
                            sendDataThreadSafe(s + "position X-axis", xValue, car.getPosition().X);
                                if (car.getController() != null)
                                {
                                    sendDataThreadSafe(s + "ref position X-axis", xValue, car.getController().getRefPoint().X);
                                    sendDataThreadSafe(s + "ref position Y-axis", xValue, car.getController().getRefPoint().Y);
                                }
                            sendDataThreadSafe(s + "position Y-axis", xValue, car.getPosition().Y);
                            sendDataThreadSafe(s + "found history", xValue, Convert.ToDouble((car.found)));
                                if (car.getControlStrategy().getStrategyName().Equals("Platooning")) { sendDataThreadSafe(s + "platooning error", xValue, ((ControlStrategies.Platooning)car.getControlStrategy()).controlError); }
                            // Add more sendDataThreadSafe calls here.
                        }
                        // Give other values to analyzer:
                           // if (cars.Count != 0)
                           // {
                            //    sendDataThreadSafe("FPS image processing", xValue, 1/(cars.First().getDeltaTime()));
                            //}
                            sendDataThreadSafe("FPS image processing", xValue, (1.0 / (Program.imageProcess.getDeltaTime())));
                        sendDataThreadSafe("Brain execution time", Convert.ToDouble(time.ElapsedMilliseconds) / 1000.0, Convert.ToDouble(dt) / 1000.0);
                        // Add more sendDataThreadSafe calls here.
                    }
                }
            }
        }

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
