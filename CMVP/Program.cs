
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing; // Anvnänds då man skapar en egen bil i Program

namespace CMVP
{
    class Program
    {
        //Global variables
        public static Communication com = new Communication();
        public static List<Car> cars = new List<Car>();
        public static List<Item> obstacle = new List<Item>();

        public static PTGreyCamera videoStream;
        public static ImageProcessing imageProcess;
        public static ImageProcessingGraphics ipd;

        [STAThread]
        public static void Main()
        {
            //cars.Add(new Car(1, new AForge.IntPoint(0, 0), new AForge.Point(1, 0), 1)); // endast för att testa en imaginär bil 
            //cars.Add(new Car(2, new AForge.IntPoint(0, 0), new AForge.Point(1, 0), 1)); // endast för att testa en imaginär bil 
            //Program.com.reverseSetting(2, "Throttle");
            videoStream = new PTGreyCamera();
            videoStream.start();
            imageProcess = new ImageProcessing(videoStream, cars);
            ipd = new ImageProcessingGraphics(imageProcess);
            imageProcess.start();
            mainGUI mainFrame = new mainGUI();
            Application.Run(mainFrame);
        }
    }

}

