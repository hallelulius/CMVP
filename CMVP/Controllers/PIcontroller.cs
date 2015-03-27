using System;
using System.Drawing;

namespace CMVP
{
    public partial class PIController : Controller
    {
        // Controller parameters 
        private float Ki_steer;
        private float Ki_throttle;
        private float Ti_steer;
        private float Ti_throttle;
        private float Kp_steer;
        private float Kp_throttle;
        // Controller variables 
        private float throttleIntegratorSum;
        private float steerIntegratorSum;

        public PIController() : base ()
        {
            // I-controller constants:
            Ki_steer = 0.1689F;
            Ki_throttle = 0.2F;
            // Integral time constants:
            Ti_steer = 2.3397F;
            Ti_throttle = 10.5179F;
            // P-controller constants:
            Kp_steer = 1.5f; //Ki_steer / Ti_steer;
            Kp_throttle = 0.04f; // Ki_throttle / Ti_throttle;
            // Set variables 
            throttleIntegratorSum = 0;
            steerIntegratorSum = 0;
            // Set controler name:
            controllerName = "PI";
        }

        public override void updateController()      //PI-controller 
        {
            outThrottle = 0;
            if (speed <=2 )
                throttleIntegratorSum += 0.001f;
            else
                throttleIntegratorSum -= 0.001f;
            //outThrottle += refSpeed / 4;
            outThrottle += throttleIntegratorSum;

            //float errorSpeed = refSpeed - speed/50;
            //outThrottle += Kp_throttle * errorSpeed;
            //throttleIntegratorSum += errorSpeed;
            //outThrottle += Ki_throttle * throttleIntegratorSum;




           outSteer = 0;
            float errorHeading = refHeading - heading;
            outSteer += -Kp_steer * errorHeading;
            steerIntegratorSum += errorHeading;
            //outSteer += -Ki_steer * steerIntegratorSum ;

            if (outThrottle > 0.25)outThrottle = 0.25f; //Todo change to 1
            if (outThrottle < -1) outThrottle = -1;
            if (outSteer > 1) outSteer = 1;
            if (outSteer < -1) outSteer = -1;
        }

        public float KiSteer
        {
            get { return Ki_steer; }
            set { Ki_steer = value; }
        }

        public float KiThrottle
        {
            get { return Ki_throttle; }
            set { Ki_throttle = value; }
        }

        public float KpSteer
        {
            get { return Kp_steer; }
            set { Kp_steer = value; }
        }

        public float KpThrottle
        {
            get { return Kp_throttle; }
            set { Kp_throttle = value; }
        }

        public float TiSteer
        {
            get { return Ti_steer; }
            set { Ti_steer = value; }
        }

        public float TiThrottle
        {
            get { return Ti_throttle; }
            set { Ti_throttle = value; }
        }
    }
}