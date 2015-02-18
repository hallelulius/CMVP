using System;
using AForge;
using Car;

namespace CMVP
{
    public partial class Controller
    {
        double psi;
        double theta;
        double delta;
        double beta;
        double betaTemp;
        double angleControl;
        double errorControl;
        double betaError;
        double angleError;

        // ----------------- Controller method -----------------NOT DONE YET
        /// <summary>RC car controller, bicycle model</summary>
        private int[] controller(double speed, DoublePoint heading, DoublePoint referenceHeading)// , double referenceSpeed, DoublePoint referenceHeading)
        {
            // calculate angle of front and reare wheels
            psi = Math.Atan2(heading.Y, heading.X);                         // vehicle angle in radians -pi<=psi<=pi
            theta = Math.Atan2(referenceHeading.Y, referenceHeading.X);     // reference angle in radians -pi<=theta<=pi
            if (Double.IsNaN(psi) || Double.IsNaN(theta))                    
            {
                delta = double.NaN;
            }
            else
            {
                beta = theta - psi;                                         //error of vehicle angle to ref angle
                // calculate angle beta for bicycle model (in radians) 
                betaTemp = ((Math.Acos(speedX / speed) + psi) + (Math.Asin(speedY / speed) + psi)) / 2; // Taking the average of the betaTemp;
                if (Math.Abs(beta) > Math.PI)                                 // reset beta when angle over 2PI
                {
                    beta = beta - Math.Sign(beta) * 2 * Math.PI;
                }

                // ------------------------------Controller implementation-------------------------------------
                // I-controller constants:
                double Ki_steer = 0.1689;
                double Ki_throttle = 0.1310;

                // Integral time constants:
                double Ti_steer = 2.3397;
                double Ti_throttle = 10.5179;

                // P-controller constants:
                double Kp_steer = Ki_steer / Ti_steer;
                double Kp_throttle = Ki_throttle / Ti_throttle;

                // Controller gains
                double steerGain = 75;
                //double steerGain = 1;
                // double throttleGain = 20;

                betaError = beta - betaTemp; //Calculating the angle error to be minimized

                steerIntegral = steerIntegral + betaError * sampleTime;
                //throttleIntegral = throttleIntegral + throttleError * sampleTime;

                angleError = Kp_steer * betaError + Ki_steer * steerIntegral;
                errorControl = angleError * steerGain;
                //throttleControl = Kp_throttle*throttleError + Ki_throttle*throttleIntegral;
                //throttleControl = throttleControl * throttleGain;*/

                // ------------------------------End of Controller implementation-------------------------------------
                //##########################################################################################################


                double deltaTemp = Math.Atan((Math.Tan(errorControl) * (lF + lR)) / lR);  // angle in radians -pi/2<=delta<=pi/2
                //double deltaTemp = Math.Atan((Math.Tan(beta) * (lF + lR)) / lR);  // angle in radians -pi/2<=delta<=pi/2
                if (Double.IsNaN(deltaTemp))
                {
                    delta = double.NaN;
                }
                else
                {
                    delta = deltaTemp;
                }
            }
        }
    }
}