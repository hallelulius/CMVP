
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace CMVP
{
    class Program
    {
        public static void Main()
        {
            mainGUI mainFrame = new mainGUI();
            Application.Run(mainFrame);
            Communication communication = new Communication();
            communication.updateSteering(0, 255);
            Thread.Sleep(1000);
            communication.updateSteering(0, 0);  
        }

    }
}
