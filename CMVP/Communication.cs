using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMVP
{

    

       /// <summary>
       /// This class sends the right steering and throttle parametres to the arduino 
       /// </summary>

    class Communication
    {
        private SerialPort port;
        private bool active = false;

        /// <summary>
        /// Takes the first COM port it find and opens up a serial communcation with it.
        /// May need to be smarter if any problems occur because of many devices are used at the same time 
        /// </summary>
        public Communication()
        {
            if (getFirstPort() != null)
            {
                port = new SerialPort(getFirstPort(), 115200); //remeber to sync baudrate with arduino
                active = true;
            }
            else
            {
                System.Console.WriteLine("Connect Arduino");
            }
        }

        private byte convertCarID(int id)
        {
            //should convert it to proper binary values according to the transmitters voltage
            //depends on input from controller class
            return (byte) id;
        }

        private byte convertValue(int value)
        {
            //should convert it to proper binary values according to the transmitters voltage
            //depends on input from controller class
            return (byte) value;
        }

        public void updateSteering(int carID, int value)
        {
            byte id = convertCarID(carID);
            byte val = convertValue(value);
            updateSteering(id, val);
        }

        public void updateThrottle(int carID, int value)
        {
            byte id = convertCarID(carID);
            byte val = convertValue(value);
            updateThrottle(id, val);
        }

        private void updateSteering(byte carID, byte value)
        {
            if (port != null) 
            {
                port.Open();
                byte[] bits = {carID, value };
                port.Write(bits, 0, 2);
                port.Close();
            }
        }

        private void updateThrottle(byte carID, byte value)
        {
            if (port != null)
            {
                carID += 2; // change to throttle DAC
                port.Open();
                byte[] bits = { carID, value };
                port.Write(bits, 0, 2);
                port.Close();
            }
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

        public bool isActive()
        {
            return active;
        }

    }
}