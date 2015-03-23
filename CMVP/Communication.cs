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
        private const byte error = 230;

        //Variables used to debug and trim  Vref=3.3V
        private const byte max_throttle = 170;  //200     //output = 0.74V
        private const byte neutral_throttle = 90; //111  //output = 1.44V
        private const byte reverse_throttle = 0;  //output = 2.03V
        private const byte neutral_steering = 114;  //output = 1.47V
        private const byte left_steering = 218;     //output = 2.82V
        private const byte right_steering = 7;      //output = 0.09V
        private const byte voltage_cap = 230;       //voltage cap

        private SerialPort port;
        private bool portOpen = false;

        /// <summary>
        /// Takes the first COM port it find and opens up a serial communcation with it.
        /// May need to be smarter if any problems occur because of many devices are used at the same time 
        /// </summary>
        public Communication()
        {
            if (getFirstPort() != null)
            {
                port = new SerialPort(getFirstPort(), 115200); //remeber to sync baudrate with arduino
                portOpen = port.IsOpen;
                System.Threading.Thread.Sleep(1000);
                try
                {
                    port.Open();
                    System.Console.WriteLine("Communication OK");
                }
                catch (Exception e)
                {
                   System.Console.WriteLine("Could not open port");
                   Console.WriteLine(e.ToString());
                }
            }
            else
            {
                System.Console.WriteLine("Connect Arduino");
            }
        }

        ~Communication()
        {
            if (port != null)
            {
                if (port.IsOpen)
                {
                    port.Close();
                }
                port.Dispose();
                Console.WriteLine("Closing Communication");
            }
        }

        private byte convertCarID(int id, String mode)
        {
            byte DAC;
            if (id == 2 && mode.Equals("Throttle") )
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
      

        public void stopCar(int carID)
        {
            updateSteering(carID, neutral_steering);
            updateThrottle(carID, neutral_throttle);
        }

        public void updateThrottle(int carID, float value)
        {
            //Console.WriteLine("Updating Throttle...");
            float val = 0;
            if (value > 0)
            {
                val = neutral_throttle + value * (max_throttle - neutral_throttle);
            }
            else if (value < 0)
            {
                val = neutral_throttle + value * -(reverse_throttle - neutral_throttle);
            }   

            byte id = convertCarID(carID,"Throttle");
            sendThrottle(id,(byte) val);
        }

        

         public void updateSteering(int carID, float value)
        {
            //Console.WriteLine("Updating Steering...");
            float val = 0;
            if (value > 0)
            {
                val = neutral_steering + value * -(right_steering - neutral_steering);
            }
            else if (value < 0)
            {
                val = neutral_steering + value * (left_steering - neutral_steering);
            }
            
            byte id = convertCarID(carID,"Steering");
            sendSteering(id, (byte) val);

            
        }

        private void sendSteering(byte carID, byte value)
        {
            if (port != null && carID < error && value < voltage_cap ) 
            {
                byte[] bits = {carID, value };
                port.Write(bits, 0, 2);
                //System.Console.WriteLine("Updated steering! DAC: "+ carID + " Value= " + value);
            }
            else
            {
                //System.Console.WriteLine("Error in steering");
            }
        }

        private void sendThrottle(byte carID, byte value)
        {
            if (port != null && carID < error && value < voltage_cap )
            {

                    byte[] bits = { carID, value };
                    port.Write(bits, 0, 2);
                    //System.Console.WriteLine("Updated throttle! DAC: " + carID + " Value= " + value);
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
                System.Console.WriteLine("No COMs found! Please connect the Arduino to the PC.");
                return null;
            }
        }

    
         
         public void reverseSetting(int carID, String mode, bool b)
         {
            if (b && mode.Equals("Throttle"))
            {
                sendThrottle(convertCarID(carID,mode), max_throttle);
                Console.WriteLine("Press and hold throttle trim. Hold for at least 3 seconds.");
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }
            else if (!b && mode.Equals("Throttle"))
            {
                sendThrottle(convertCarID(carID, mode), reverse_throttle);
                Console.WriteLine("Press and hold throttle trim. Hold for at least 3 seconds.");
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }
            else if (b && mode.Equals("Steering"))
            {
                sendSteering(convertCarID(carID, mode), left_steering);
                Console.WriteLine("Press and hold steering trim. Hold for at least 3 seconds.");
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }
            else if (!b && mode.Equals("Throttle"))
            {
                sendSteering(convertCarID(carID, mode), right_steering);
                Console.WriteLine("Press and hold steering trim. Hold for at least 3 seconds.");
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }

        }
        public bool isActive()
        {
            return port.IsOpen;
        }

    }
}