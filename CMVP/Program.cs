
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge;

namespace CMVP
{
    class Program
    {
        
        //Global variables
        //Simulation variables
        public static float sampleTime = 1 / 150;             // Iteration time, equal 1/(updating frequency)
        private static bool simulating = false;
        public static VideoStream videoStream;
        public static VideoStream imageProcess;
        public static List<Car> carList = new List<Car>();

        public static void Main()
        {
            mainGUI mainFrame = new mainGUI(); 
            videoStream = new Camera();
            imageProcess = new ImageProcessing(videoStream, carList);
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
