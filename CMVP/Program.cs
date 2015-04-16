
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing; // Anvnänds då man skapar en egen bil i Program
using System.Runtime.InteropServices;
using AForge;

namespace CMVP
{
    class Program
    {
        //Global variables
        public static Communication com = new Communication();
        public static List<Car> cars = new List<Car>();
        //Simulation variables
        //public static float sampleTime = 1/150F;             // Iteration time, equal 1/(updating frequency)
        //private static bool simulating = false;
        public static PTGreyCamera videoStream;
        public static ImageProcessing imageProcess;

        [STAThread]
        public static void Main()
        {
            //cars.Add(new Car(1, new AForge.IntPoint(0, 0), new AForge.Point(1, 0))); // endast för att testa en imaginär bil 
            //cars.Add(new Car(2, new AForge.IntPoint(0, 0), new AForge.Point(1, 0))); // endast för att testa en imaginär bil 
            mainGUI mainFrame = new mainGUI();
            videoStream = new PTGreyCamera();
            videoStream.start();
            System.Threading.Thread.Sleep(1000);
            imageProcess = new ImageProcessing(videoStream, cars);
            Application.Run(mainFrame);
   
        }
       
        /*
        public static bool isSimulating()
        {
            return simulating;
        }
         */
    }
}
