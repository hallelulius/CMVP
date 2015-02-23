
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

using AForge;

namespace CMVP
{
    class Program
    {
        
        
        //Global variables
        //Simulation variables
        public static float sampleTime = 1 / 150;             // Iteration time, equal 1/(updating frequency)
        private static bool simulating = false;

        public static void Main()
        {
            Communication com = new Communication();
            //com.reverseSetting(1, "Throttle", true);
            while (true)
            {
                Console.WriteLine("Skriv in styrning:");
                String steer = Console.ReadLine();
                com.updateCar(1, Int32.Parse(steer), "Steering");
                //Console.WriteLine("Skriv in gas: (111 är neutral)");
                //String throttle = Console.ReadLine();
                //com.updateCar(1, Int32.Parse(throttle), "Throttle");
            }
            //mainGUI mainFrame = new mainGUI();
            //Application.Run(mainFrame);
        }
        public static bool isSimulating()
        {
            return simulating;
        }
    }
}
