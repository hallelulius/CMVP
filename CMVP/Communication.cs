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
        private const byte error = 220;

        //Variables used to debug and trim  Vref=3.3V
        private const byte max_throttle = 57;      //output = 0.74V
        private const byte neutral_throttle = 111;  //output = 1.44V
        private const byte reverse_throttle = 157;  //output = 2.03V
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
                try
                {
                    port.Close();
                }
                catch (Exception e)
                {

                }
                System.Threading.Thread.Sleep(1000);
                try
                {
                    port.Open();
                }
                catch (Exception e)
                {

                }
            }
            else
            {
                System.Console.WriteLine("Connect Arduino");
            }
        }

        ~Communication()
        {
            port.Close();
            Console.WriteLine("Closing Communication");
        }

        private byte convertCarID(int id, String mode)
        {
            byte DAC;
            if (id == 1 && mode.Equals("Throttle") )
            {
                DAC = throttleA;
            }
            else if (id == 1 && mode.Equals("Steering"))
            {
                DAC = steeringA;
            }
            else if (id == 2 && mode.Equals("Throttle"))
            {
                DAC = throttleB;
            }
            else if (id == 2 && mode.Equals("Steering"))
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

        private byte convertValue(int value, String mode)
        {
            byte val;
            if (value > error || value < 0)
                value = error;
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
            updateCar(carID, neutral_steering, "Steering");
            updateCar(carID, neutral_throttle, "Throttle");
        }

        public void updateCar(int carID, float value1, String mode)
        {
            Console.WriteLine("Updateing Car...");
            int value = (int)value1;  //ÄNDRA DETTA!
            byte val = convertValue(value,mode);
            byte id = convertCarID(carID, mode);
            
            switch (mode){
                case "Throttle":
                    updateThrottle(id, val);
                    break;
                case "Steering":
                    updateSteering(id, val);
                    break;
                default:
                    System.Console.WriteLine("Didn't update " +carID +" "+ mode);
                    break;
            }
        }

        private void updateSteering(byte carID, byte value)
        {
            if (port != null && carID < error && value < error ) 
            {
                //port.Open();
                byte[] bits = {carID, value };
                port.Write(bits, 0, 2);
                //port.Close();
                System.Console.WriteLine("Updated steering! DAC: "+ carID + " Value= " + value);
            }
            else
            {
                System.Console.WriteLine("Error in steering");
            }
        }

        private void updateThrottle(byte carID, byte value)
        {
            if (port != null && carID < error && value < error)
            {
                //Console.WriteLine("1");
                //try
                //{
                    //port.Open();
                    //Console.WriteLine("2");
                    byte[] bits = { carID, value };
                    //Console.WriteLine("3");
                    port.Write(bits, 0, 2);
                    //Console.WriteLine("4");
                    //port.Close();
                    System.Console.WriteLine("Updated throttle! DAC: " + carID + " Value= " + value);
                //}
                //catch (UnauthorizedAccessException e)
                //{

                //}
            }
            else
            {
                System.Console.WriteLine("Error in throttle");
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

        public void reverse(int carID, String mode)
        {

        }

        public void reverseSetting(int carID, String mode, bool b)
        {
            if (b && mode.Equals("Throttle"))
            {
                updateCar(carID, max_throttle, mode);
                Console.WriteLine("Press and hold throttle trim. Hold for at least 3 seconds.");
                Console.WriteLine("Press any key");
                Console.ReadKey();
                
            }
            else if (!b && mode.Equals("Throttle"))
            {
                updateCar(carID, reverse_throttle, mode);
                Console.WriteLine("Press and hold throttle trim. Hold for at least 3 seconds.");
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }
            else if (b && mode.Equals("Steering"))
            {
                updateCar(carID, left_steering, mode);
                Console.WriteLine("Press and hold steering trim. Hold for at least 3 seconds.");
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }
            else if (!b && mode.Equals("Throttle"))
            {
                updateCar(carID, right_steering, mode);
                Console.WriteLine("Press and hold steering trim. Hold for at least 3 seconds.");
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }

        }
        public bool isActive()
        {
            return active;
        }

    }
}