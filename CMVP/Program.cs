
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
        public static Communication communication; //communication protokoll
        public static CameraController cameraController;//controls all communication
        public static ImageProcessing imageProcessing;

        //Simulation variables
        private static bool simulating;
        public static void Main()
        {
            cameraController = new CameraController();
            mainGUI mainFrame = new mainGUI();
            Application.Run(mainFrame);
        }
        public static bool isSimulating()
        {
            return simulating;
        }
        /*
        public static void old()
        {
            System.Console.WriteLine("Hello World");
            System.Console.Out.WriteLine("Hej Viktor");
            System.Console.ReadLine();
            cout("C++ style");
            counter(10);
            System.Console.ReadLine();
        }
        public static void cout(String str){
            System.Console.WriteLine(str);
        }

        public static void counter(int i)
        {
            for(int j =0 ; j<=i; j++){
                System.Console.WriteLine(j);
            }
        }*/
        
    }
}
