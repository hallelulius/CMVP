
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

        [STAThread]
        public static void Main()
        {
            mainGUI mainFrame = new mainGUI();
            Application.Run(mainFrame);
        }
        public static bool isSimulating()
        {
            return simulating;
        }
    }
}
