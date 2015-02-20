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
       /// 
     class Communication
     {
        //addresses for the DACs
        private const byte throttleA = 0;    // DAC A gain 1
        private const byte throttleB = 2;    // DAC B gain 1
        private const byte steeringA = 4;    // DAC C gain 1
        private const byte steeringB = 6;    // DAC D gain 1
        private const byte error = 255;

        //Variables used to debug and trim  Vref=3.3V
        private const byte max_throttle = 57;      //output = 0.74V
        private const byte neutral_throttle = 111;  //output = 1.44V
        private const byte reverse_rhrottle = 157;  //output = 2.03V
        private const byte neutral_steering = 114;  //output = 1.47V
        private const byte left_steering = 218;     //output = 2.82V
        private const byte right_steering = 7;    //output = 0.09V
        private const byte voltage_cap = 219;  //voltage cap

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

        private byte convertCarID(int id, char mode)
        {
            byte DAC;
            if (id == 1 && mode == 'T')
            {
                DAC = throttleA;
            }
            else if (id == 1 && mode == 'S')
            {
                DAC = steeringA;
            }
            else if (id == 2 && mode == 'T')
            {
                DAC = throttleB;
            }
            else if (id == 2 && mode == 'S')
            {
                DAC = steeringB;
            }
            else
            {
                System.Console.WriteLine("No valid id or mode");
                DAC = error;
            }
            return DAC;
        }

        private byte convertValue(int value, char mode)
        {
            byte val;
            if ((byte)value > voltage_cap)
            {
                val = error;
            }
            else
            {
                val = (byte)value;
            }
            return val;
        }

        public void stopCar(int carID)
        {
            updateCar(carID, neutral_steering, 'S');
            updateCar(carID, neutral_throttle, 'T');
        }

        public void updateCar(int carID, int value, char mode)
        {
            byte val = convertValue(value,mode);
            byte id = convertCarID(carID, mode);
            
            switch (mode){
                case 'T':
                    updateThrottle(id, val);
                    break;
                case 'S':
                    updateSteering(id, val);
                    break;
                default:
                    break;
            }
        }

        private void updateSteering(byte carID, byte value)
        {
            if (port != null || carID != error || value != error ) 
            {
                port.Open();
                byte[] bits = {carID, value };
                port.Write(bits, 0, 2);
                port.Close();
            }
        }

        private void updateThrottle(byte carID, byte value)
        {
            if (port != null || carID != -1 || value != -1)
            {
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
                return null;
            }
        }

        public bool isActive()
        {
            return active;
        }

    }
}