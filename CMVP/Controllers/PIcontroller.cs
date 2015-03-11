﻿using System;
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
            Ki_throttle = 0.1310F;
            // Integral time constants:
            Ti_steer = 2.3397F;
            Ti_throttle = 10.5179F;
            // P-controller constants:
            Kp_steer = Ki_steer / Ti_steer;
            Kp_throttle = Ki_throttle / Ti_throttle;
            // Set variables 
            throttleIntegratorSum = 0;
            steerIntegratorSum = 0;
            // Set controler name:
            controllerName = "PID";
        }

        public override void updateController()      //PI-controller 
        {
            outThrottle = 0;
            float errorSpeed = refSpeed - speed;
            outThrottle += Kp_throttle * errorSpeed * Program.sampleTime;
            throttleIntegratorSum += errorSpeed;
            outThrottle += Ki_throttle * throttleIntegratorSum * Program.sampleTime;

            outSteer = 0;
            float errorHeading = refHeading - heading;
            outSteer += errorHeading * 1.33f; //Kp_steer * errorHeading* Program.sampleTime;
            steerIntegratorSum += errorHeading;
            //outSteer += Ki_steer * steerIntegratorSum * Program.sampleTime;

            if (outThrottle > 1)outThrottle = 1;
            if (outThrottle < -1) outThrottle = -1;
            if (outSteer > 1) outSteer = 1;
            if (outSteer < -1) outSteer = -1;
        }
    }
}