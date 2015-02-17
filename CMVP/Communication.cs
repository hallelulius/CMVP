using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMVP
{

       /**
        * This class sends the right steering and throttle parametres to the arduino
        * 
        **/

    class Communication
    {
        private SerialPort port;
        private bool active = false;

        /**
         * Takes the first COM port it find and opens up a serial communcation with it.
         * May need to do smarter if any problems because of many devices at the same time
         * */
        public Communication()
        {
            if (getFirstPort() != null)
            {
                port = new SerialPort(getFirstPort(), 115200); //remeber to sync baudrate with arduino
                active = true;
            }
        }

        
        public void updateSteering(int carID, int value)
        {
            if (port != null) 
            {
                port.Open();
                byte[] bits = { (byte)carID, (byte)value };
                port.Write(bits, 0, 2);
                port.Close();
            }
        }

        public void updateThrottle(int carID, int value)
        {
            if (port != null)
            {
                carID += 2; // change to throttle DAC
                port.Open();
                byte[] bits = { (byte)carID, (byte)value };
                port.Write(bits, 0, 2);
                port.Close();
            }
        }

        public bool isActive()
        {
            return active;
        }

        private String getFirstPort()
        {
            List<String> allPorts = new List<String>();
            foreach (String portName in System.IO.Ports.SerialPort.GetPortNames())
            {
                allPorts.Add(portName);
            }
            try
            {
                return allPorts[0];
            }
            catch (System.ArgumentOutOfRangeException e)        
            {
                System.Console.WriteLine("No COMs found! Please connect the Arduino to the PC. \n");
                System.Console.WriteLine(e.ToString());
                //System.Console.ReadKey();
                return null;
            }
        }

    }
}