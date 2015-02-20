
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
        //object of different modules
        public static Communication communication;  
        public static CameraController cameraController;    // controls all communication
        public static ImageProcessing imageProcessing;
        public static float sampleTime = 1/150;             // Iteration time, equal 1/(updating frequency)

        //Simulation variables
        private static bool simulating = false;

        public static void Main()
        {
            //cameraController = new CameraController();
            mainGUI mainFrame = new mainGUI();
            Application.Run(mainFrame);
        }
        public static bool isSimulating()
        {
            return simulating;
        }
    }
}
