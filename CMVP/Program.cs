
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMVP
{
    class Program
    {
        public static void Main()
        {
            mainGUI mainFrame = new mainGUI();
            Application.Run(mainFrame);
        }
        
        /**
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
        }
        
    }
}
