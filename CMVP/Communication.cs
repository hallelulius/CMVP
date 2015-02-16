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
        private string portName;

        public Communication(String portName)
        {
            //TODO
            port = new SerialPort();
            port.BaudRate = 9600; //set to higher later and remeber to sync with arduino
            port.PortName = portName;
            this.portName = portName;
            port.WriteLine("Connected");
        }
    }
}
