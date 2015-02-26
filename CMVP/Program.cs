
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
        public static Communication com = new Communication();
        public static List<Car> cars = new List<Car>();
        //Simulation variables
        public static float sampleTime = 1 / 150;             // Iteration time, equal 1/(updating frequency)
        private static bool simulating = false;
        public static VideoStream videoStream;
        public static VideoStream imageProcess;
        public static List<Car> carList = new List<Car>();

        [STAThread]
        public static void Main()
        {

            mainGUI mainFrame = new mainGUI();
            videoStream = new Camera();
            imageProcess = new ImageProcessing(videoStream, ref cars); // Changed to cars from carList for debugging purposes //Viktor I
            videoStream.start();
            imageProcess.start();
            
            Application.Run(mainFrame);

        }
        public static bool isSimulating()
        {
            return simulating;
        }
    }
}
