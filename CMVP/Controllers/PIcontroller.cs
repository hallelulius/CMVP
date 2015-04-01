using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

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
        private float Kd_steer;
        private float Kd_throttle;
        // Controller variables 
        private float throttleIntegratorSum;
        private float steerIntegratorSum;
        private float prevSpeedErrorAvg;
        private float prevHeadingErrorAvg;
        private List<float> prevSpeedError;
        private List<float> prevHeadingError;
        private const int DATA_HISTORY_LENGTH = 5;

        public PIController(Car car) : base (car)
        {
            // I-controller constants:
            Ki_steer = 0.1689F;
            Ki_throttle = 0.001F;
            // Integral time constants:
            Ti_steer = 2.3397F;
            Ti_throttle = 10.5179F;
            // P-controller constants:
            Kp_steer = 0.6f; //Ki_steer / Ti_steer;
            Kp_throttle = 0.1f; // Ki_throttle / Ti_throttle;
            // D-controller constants:
            Kd_steer = 0.01f; 
            Kd_throttle = 0.01f; 
            // Set variables 
            throttleIntegratorSum = 0;
            steerIntegratorSum = 0;
            // Set controler name:
            controllerName = "PI";
            prevHeadingError = new List<float>();
            prevSpeedError = new List<float>();
            for (int i = 0; i < DATA_HISTORY_LENGTH; i++)
            {
                this.prevHeadingError.Add(0);
                this.prevHeadingError.Add(0);
            }

        }

        public override void updateController()      //PI-controller 
        {
            outThrottle = 0;
            float errorSpeed = refSpeed - speed / maxSpeed;
            outThrottle += Kp_throttle * errorSpeed;
            throttleIntegratorSum += errorSpeed;
            outThrottle += throttleIntegratorSum * Ki_throttle;

            //derivative part here, not fully tested but seems to work 
            prevSpeedErrorAvg = 0;
            foreach (float err in prevHeadingError)
            {
                prevSpeedErrorAvg += err;
            }
            prevSpeedErrorAvg /= (float)prevSpeedError.Count;
            outThrottle += Kd_throttle * (errorSpeed - prevSpeedErrorAvg);
            prevSpeedError.Insert(0, errorSpeed);
            prevSpeedError.Remove(prevSpeedError.Last());


            /*
            outThrottle = 0.13f;
            if (speed <2  )
                throttleIntegratorSum += 0.00005f;
            else if (speed > 8)
            {
                throttleIntegratorSum -= 0.00005f;
            }
            outThrottle += refSpeed / 4;
            outThrottle += throttleIntegratorSum;
            */

            //float errorSpeed = refSpeed - speed/50;
            //outThrottle += Kp_throttle * errorSpeed;
            //throttleIntegratorSum += errorSpeed;
            //outThrottle += Ki_throttle * throttleIntegratorSum;




           outSteer = 0;
            float errorHeading = refHeading - heading;
            if (errorHeading > Math.PI)
                errorHeading -= 2f * (float)Math.PI;
            else if (errorHeading < -Math.PI)
                errorHeading += 2f * (float)Math.PI;
            outSteer += -Kp_steer * errorHeading;
            steerIntegratorSum += errorHeading;
            //outSteer += -Ki_steer * steerIntegratorSum ;
            //derivative part here, not fully tested but seems to work 
            prevHeadingErrorAvg=0;
            foreach (float err in prevHeadingError)
            {
                prevHeadingErrorAvg += err;
            }
            prevHeadingErrorAvg /= (float) prevHeadingError.Count;
            outSteer += Kd_steer * (errorHeading-prevHeadingErrorAvg);
            prevHeadingError.Insert(0, errorHeading);
            prevHeadingError.Remove(prevHeadingError.Last());

            outThrottle = capThrottleOutput(outThrottle);
            outSteer = capSteerOutput(outSteer);
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
        
        public override void resetController()
        {
            throttleIntegratorSum = 0;
            steerIntegratorSum = 0;
        }
    }
}