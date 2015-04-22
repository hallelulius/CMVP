using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace CMVP
{
    /// <summary>
    /// This class sends the right steering and throttle parametres to the arduino 
    /// </summary>
    /// 
    class Communication
    {
        //addresses for the DACs
        // DO NOT CHANGE!
        private const byte throttleA = 0;    // DAC A gain 1
        private const byte throttleB = 2;    // DAC B gain 1
        private const byte steeringA = 4;    // DAC C gain 1
        private const byte steeringB = 6;    // DAC D gain 1
        private const byte ERROR_CODE = 230;

        //Constants used to debug and trim  Vref=3.3V 
        // DO NOT CHANGE! 
        private const byte MAX_THROTTLE = 200;               //output = 2.58V
        private const byte NEUTRAL_THROTTLE = 111;            //output = 1.44V
        private const byte MIN_THROTTLE = 7;                 //output = 0.09V
        private const byte NEUTRAL_STEERING = 114;           //output = 1.47V
        private const byte LEFT_STEERING = 218;              //output = 2.82V
        private const byte RIGHT_STEERING = 7;               //output = 0.09V
        private const byte VOLTAGE_CAP = 230;               //to prevent high voltages to the controller


        private SerialPort port;
        private bool portOpen = false;
        private byte lastThrottle = NEUTRAL_THROTTLE;

        /// <summary>
        /// Takes the first COM port it finds and opens up a serial communcation with it.
        /// Handles all the communication between the computer and the arduino
        /// May need to be smarter if any problems occur if many COM-devices are used at the same time 
        /// </summary>
        public Communication()
        {
            if (getFirstPort() != null)
            {
                port = new SerialPort(getFirstPort(), 115200);          //remember to sync baudrate with arduino sketch
                portOpen = port.IsOpen;
                System.Threading.Thread.Sleep(1000);
                try
                {
                    port.Open();
                    //port.bu
                    System.Console.WriteLine("Communication OK");
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Could not open port");
                    Console.WriteLine(e.ToString());
                }
            }
            // to ensure the settings on the car wont change
            stopCars();

        }

        ~Communication()
        {
            stopCars();
            if (port != null)
            {
                if (isActive())
                {
                    port.Close();
                }
                port.Dispose();
                Console.WriteLine("Closing Communication");
            }
        }

        /// <summary>
        /// Converts a Car ID and its mode and return its corresponding DAC address
        /// If no DAC address is found it returns an error code
        /// Modes are "Steering" and "Throttle"
        /// </summary>
        /// <param name="id">The ID of the Car that is used</param>
        /// <param name="mode">If it should find DAC address for throttle or steering</param>
        /// <returns>The correspnding DAC address for the id and the mode</returns>
        private byte convertCarIDToDAC(int id, String mode)
        {
            byte DAC;
            if (id == 1 && mode.Equals("Throttle"))
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
                DAC = ERROR_CODE;
            }
            return DAC;
        }


        public void stopCar(int carID)
        {
            if (lastThrottle <= NEUTRAL_THROTTLE)
            {
                for (byte i = lastThrottle; i < NEUTRAL_THROTTLE; i++)
                {
                    sendThrottle(convertCarIDToDAC(carID, "Throttle"), NEUTRAL_THROTTLE);
                }
            }
            else
            {
                sendThrottle(convertCarIDToDAC(carID, "Throttle"), NEUTRAL_STEERING);    
            }
            sendSteering(convertCarIDToDAC(carID, "Steering"), LEFT_STEERING);
            Console.WriteLine("Car " + carID + " stopped");
        }

        /// <summary>
        /// Used for setting throttle values on a car
        /// </summary>
        /// <param name="carID">The car that should be updated</param>
        /// <param name="value">Throttle value</param>
        public void updateThrottle(int carID, float value)
        {
            float val = 0;
            if (value >= 0)
            {
                val = NEUTRAL_THROTTLE + value * (MAX_THROTTLE - NEUTRAL_THROTTLE);
            }
            else if (value < 0)
            {
                val = NEUTRAL_THROTTLE + value * -(MIN_THROTTLE - NEUTRAL_THROTTLE);
            }

            byte id = convertCarIDToDAC(carID, "Throttle");
            sendThrottle(id, (byte)val);
        }


        /// <summary>
        /// Used for setting steering angle on a car
        /// </summary>
        /// <param name="carID">The car that should be updated</param>
        /// <param name="value">Steering value</param>
        public void updateSteering(int carID, float value)
        {
            float val = 0;
            if (value >= 0)
            {
                val = NEUTRAL_STEERING + value * (RIGHT_STEERING - NEUTRAL_STEERING);
            }
            else if (value < 0)
            {
                val = NEUTRAL_STEERING + value * -(LEFT_STEERING - NEUTRAL_STEERING);
            }

            byte id = convertCarIDToDAC(carID, "Steering");
            sendSteering(id, (byte)val);

        }
        /// <summary>
        /// Sends the steering value of a car to the arduino
        /// </summary>
        /// <param name="carID">The car that should be updated</param>
        /// <param name="value">Steering value</param>
        private void sendSteering(byte DAC, byte value)
        {
            if (port != null && DAC != ERROR_CODE && value < VOLTAGE_CAP)
            {
                byte[] bits = { DAC, value };
                try
                {
                    port.Write(bits, 0, 2);
                }
                catch (InvalidOperationException e){
                    Console.WriteLine("Port is closed");
                    Debug.WriteLine(e);
                }
            }
            else
            {
                Console.WriteLine("Error in steering");
            }
        }
        /// <summary>
        ///  Sends the throttle value of a car to the arduino
        /// </summary>
        /// <param name="carID">The car that should be updated</param>
        /// <param name="value">throttleValue</param>
        private void sendThrottle(byte DAC, byte value)
        {
            if (port != null && DAC != ERROR_CODE && value < VOLTAGE_CAP)
            {
                byte[] bits = { DAC, value };
                port.Write(bits, 0, 2);
                lastThrottle = value;
            }
            else
            {
                System.Console.WriteLine("Error in throttle");
            }
        }

        /// <summary>
        /// Gets the first COM-port on the computer
        /// </summary>
        /// <returns>Name of the COM-port</returns>
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
                Debug.WriteLine(e.Message);
                return null;
            }
        }


        /// <summary>
        /// Reverse steering or throttle i.e., making 0 to full throttle instead of 255 and vice versa
        /// See the KT-18 Perfex manual for more info
        /// Modes are "Steering" or "Throttle"
        /// </summary>
        /// <param name="carID">The car that should be updated</param>
        /// <param name="mode">If steering or throttle should be updated</param>
        /// <param name="b">Decides which reverse setting that should be used</param>
        public void reverseSetting(int carID, String mode)
        {
            if (mode.Equals("Throttle"))
            {
                sendThrottle(convertCarIDToDAC(carID, mode), MIN_THROTTLE);
                MessageBox.Show("Press and hold one of the throttle trim button. Hold for at least 3 seconds.");
            }

            else if (mode.Equals("Steering"))
            {
                sendSteering(convertCarIDToDAC(carID, mode), LEFT_STEERING);
                MessageBox.Show("Press and hold one of the steering trim button. Hold for at least 3 seconds.");
            }

        }
        public bool isActive()
        {
            return port.IsOpen;
        }
        public void calibrationMode(int carId)
        {
            sendThrottle(convertCarIDToDAC(carId, "Throttle"), NEUTRAL_THROTTLE);
            sendSteering(convertCarIDToDAC(carId, "Steering"), NEUTRAL_STEERING);
        }

        internal void stopCars()
        {
            if (lastThrottle <= NEUTRAL_THROTTLE)
            {
                for (byte i = lastThrottle; i < NEUTRAL_THROTTLE; i++)
                {
                    sendThrottle(throttleA, NEUTRAL_THROTTLE);
                    sendThrottle(throttleB, NEUTRAL_THROTTLE);
                }
            }
            else
            {
                sendThrottle(throttleA, NEUTRAL_THROTTLE);
                sendThrottle(throttleB, NEUTRAL_THROTTLE);
            }
            sendSteering(steeringA, NEUTRAL_STEERING);
            sendSteering(steeringB, NEUTRAL_STEERING);
            
        }
    }
}