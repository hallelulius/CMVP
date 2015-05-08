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

        public PIDController(Car car)
            : base(car)
        {
            // I-controller constants:
            Ki_steer = 0.0000F;
            Ki_throttle = 0.001F;
            // P-controller constants:
            Kp_steer = 0.6f;        //Ki_steer / Ti_steer
            Kp_throttle = 0.1f;     // Ki_throttle / Ti_throttle
            // D-controller constants:
            Kd_steer = 0.01f;
            Kd_throttle = 0.0f;
            // Set variables 
            throttleIntegratorSum = 0;
            steerIntegratorSum = 0;
            // Set controler name:
            controllerName = "PID";
            prevHeadingError = 0;
            prevSpeedError = 0;
        }

        public override void updateController()
        {
            //Throttle part
            outThrottle = 0;
            float dT = (float)car.getDeltaTime();
            float errorSpeed = refSpeed - speed / maxSpeed;
                outThrottle += Kp_throttle * errorSpeed;
            throttleIntegratorSum += errorSpeed * dT;
                outThrottle += throttleIntegratorSum * Ki_throttle;

                //derivative part here, not fully tested but seems to work 
                derivativeThrottle = (errorSpeed - prevSpeedError) / dT;
            outThrottle += Kd_throttle * derivativeThrottle;
                prevSpeedError = errorSpeed;

            



            //Steering part
            outSteer = 0;
            float errorHeading = refHeading - heading;
            if (errorHeading > Math.PI)
                errorHeading -= 2f * (float)Math.PI;
            else if (errorHeading < -Math.PI)
                errorHeading += 2f * (float)Math.PI;
            outSteer += -Kp_steer * errorHeading;
            steerIntegratorSum += errorHeading * dT;
            outSteer += -Ki_steer * steerIntegratorSum;

            //derivative part here, not fully tested but seems to work 
            derivativeSteer = (errorHeading - prevHeadingError) / dT;
            outSteer += Kd_steer * derivativeSteer;
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