using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace CMVP
{
    public partial class PIDController : Controller
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
        private float derivativeThrottle;
        private float derivativeSteer;
        private float prevSpeedError;
        private float prevHeadingError;
        private const int DATA_HISTORY_LENGTH = 5;

        public PIDController(Car car)
            : base(car)
        {
            // I-controller constants:
            Ki_steer = 0.0000F;
            Ki_throttle = 0.001F;
            // Integral time constants:
            Ti_steer = 2.3397F;
            Ti_throttle = 10.5179F;
            // P-controller constants:
            Kp_steer = 0.6f; //Ki_steer / Ti_steer;
            Kp_throttle = 0.1f; // Ki_throttle / Ti_throttle;
            // D-controller constants:
            Kd_steer = 0.01f;
            Kd_throttle = 0.0f;
            // Set variables 
            throttleIntegratorSum = -0.5f; // This is to prevent that the car will fly away. There is probably some problem in communication.
            steerIntegratorSum = 0;
            // Set controler name:
            controllerName = "PID";
            prevHeadingError = 0;
            prevSpeedError = 0;
        }

        public override void updateController()      //PI-controller 
        {
            //Throttle part
            outThrottle = 0;
            float dT = (float)car.getDeltaTime();
            float errorSpeed = refSpeed - speed / maxSpeed;
            outThrottle += Kp_throttle * errorSpeed;
            throttleIntegratorSum += errorSpeed;
            outThrottle += throttleIntegratorSum * Ki_throttle;
            
            //derivative part here, not fully tested but seems to work 
            derivativeThrottle = (errorSpeed - prevSpeedError) / dT;
            //outThrottle += Kd_throttle * derivativeThrottle;
            prevSpeedError = errorSpeed;



            //Steering part
            outSteer = 0;
            float errorHeading = refHeading - heading;
            if (errorHeading > Math.PI)
                errorHeading -= 2f * (float)Math.PI;
            else if (errorHeading < -Math.PI)
                errorHeading += 2f * (float)Math.PI;
            outSteer += -Kp_steer * errorHeading;
            steerIntegratorSum += errorHeading;
            outSteer += -Ki_steer * steerIntegratorSum *dT;

            //derivative part here, not fully tested but seems to work 
            derivativeSteer = (errorHeading - prevHeadingError) / dT;
            outSteer += Kd_throttle * derivativeSteer;
            prevHeadingError = errorHeading;


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
        public float KdSteer
        {
            get { return Kd_steer; }
            set { Kd_steer = value; }
        }

        public float KdThrottle
        {
            get { return Kd_throttle; }
            set { Kd_throttle = value; }
        }
        public override void resetController()
        {
            throttleIntegratorSum = 0;
            steerIntegratorSum = 0;
        }
    }
}