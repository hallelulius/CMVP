
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMVP
{
    class test
    {
        public static void Main()
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
