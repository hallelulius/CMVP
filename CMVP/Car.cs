﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Drawing;


using AForge.Imaging;
using AForge.Math.Geometry;
using AForge.Math;
using AForge;

//using Math;

namespace CMVP
{
    public class Car
    {
        //The first element in the lists is the last one logged, ie. the current one.
        private List<AForge.IntPoint> position; //Position of the car as two integers.
        private List<IntPoint> lastPositions; //A list to prevent flickering.
        private List<AForge.Point> direction; //The direction of the car as a normalized 2D vector.
        private List<float> angles;//angles
        private List<double> speed; //Velocity of the car in cm/s.
        private List<double> deltaTime; //delta between updates
        private List<double> acceleration; //Acceleration of the car calculated as the difference in velocity between the last velocity and the current velocity.

        
        private ControlStrategy controlStrategy; // This specific cars control strategy
        private int id; public int ID { get { return id; } } //Identification number of the car.
        public bool found;
        private Controller controller = new KeyboardController(); // This cars controller 
        private float maxSpeed;
        private static float PIXEL_SIZE = 0.2F; // used to get the right unit for the speed
        
        private double throttle; //A number between 0 and 1, deciding the speed of the car.
        private double steer; //A number between -1 and 1, deciding the steering of the car. -1: max left. 1: max right.

        //Const settings:
        private const int DATA_HISTORY_LENGTH = 5; //Decides how many elements will be stored in the position, direction, speed, acceleration and found lists.


        /// <summary>
        /// The "Car" class is the virtual representation of the real world RC car.
        /// </summary>
        /// <param name="id"> Identification number of the car. </param>
        /// <param name="pos"> The starting position of the car. </param>
        public Car(int id, AForge.IntPoint pos, AForge.Point dir)
        {
            this.id = id;
            this.direction = new List<AForge.Point>();
            this.position = new List<AForge.IntPoint>();
            this.angles = new List<float>();
            this.speed = new List<double>();
            this.deltaTime = new List<double>();
            this.acceleration = new List<double>();
            this.lastPositions = new List<IntPoint>();
            this.controlStrategy = new ControlStrategies.StandStill(this);
            this.maxSpeed = 200;
            for (int i = 0; i < DATA_HISTORY_LENGTH; i++)
            {
                this.direction.Add(dir);
                this.position.Add(pos);
                this.speed.Add(1.0);
                this.acceleration.Add(0);
                this.angles.Add(0);
                this.deltaTime.Add(0.001F);
                this.lastPositions.Add(pos);
            }
            found=true;
        }
        /// <summary>
        /// Update the state of the car. Only call this once for every car in each program loop.
        /// </summary>
        public void updateState()
        {
            //Calculate horizontal and vertical movement using the last two elements in the position list.
            double dx = position.ElementAt(1).X - position.ElementAt(0).X;
            double dy = position.ElementAt(1).Y - position.ElementAt(0).Y;
            double tempSpeed = (double) ((Math.Sqrt((dx * dx) + (dy * dy)))/deltaTime.ElementAt(0))*PIXEL_SIZE;

            List<double> tempSpeedList = new List<double>(speed); //Copy to prevent exceptions in foreach 
            foreach (double s in tempSpeedList)
            {
                tempSpeed += s;
            }
            double tempSpeed2 = (double)tempSpeed / (speed.Count + 1);
            if (tempSpeed2 > 0.01F)
            {
                speed.Insert(0, tempSpeed2);
            }
            else
            {
                speed.Insert(0, 0);
            }
            
            
            //Remove oldest element.
            double xspeed = speed.ElementAt(speed.Count-1);
            if (xspeed == null)
                Console.WriteLine("Null speed");
            //speed.Remove(speed.Last()); se below
            speed.RemoveAt(speed.Count - 1);
            

            //Calculate acceleration
            acceleration.Insert(0,(speed.ElementAt(1) - speed.ElementAt(0))/deltaTime.ElementAt(0));
            //Remove oldest element.
           // acceleration.Remove(acceleration.Last()); We tried another way to remove last object. Se below
            acceleration.RemoveAt(acceleration.Count-1);
        }

        /// <summary>
        /// Set the cars position and orientation.
        /// </summary>
        /// <param name="pos"> The new postition of the car. </param>
        /// <param name="angle"> The new orientation of the car. </param>
        public void setPositionAndOrientation(AForge.IntPoint pos, double angle,double deltaTime)
        {
            AForge.Point dir = new AForge.Point((float)Math.Cos(angle),(float)Math.Sin(angle));
            this.setPositionAndOrientation(pos, dir, deltaTime);
        }

        /// <summary>
        /// Set the cars position and orientation.
        /// </summary>
        /// <param name="pos"> The new postition of the car. </param>
        /// <param name="dir"> The new direction of the car. </param>
        public void setPositionAndOrientation(AForge.IntPoint pos, AForge.Point dir, double deltaTime)
        {
            //to prevent flickering between two pixels
            if(lastPositions.TrueForAll(x => !(x.Equals(pos))))
            {
                if (pos == position.ElementAt(0))
                {
                    position.Insert(0, pos);
                    position.Remove(position.Last());
                }
                else
                {
                    lastPositions.Insert(0, position.ElementAt(0));
                    lastPositions.RemoveAt(lastPositions.Count - 1);
                    position.Insert(0, pos);
                    position.Remove(position.Last());

                }
            }
            else
            {
                position.Insert(0, position.ElementAt(0));
                position.Remove(position.Last());
            }

            /*if (pos != lastPos)
            {
                if (pos == position.ElementAt(0))
                {
                    position.Insert(0, pos);
                    position.Remove(position.Last());
                }
                else
                {
                    lastPos = position.ElementAt(0);
                    position.Insert(0, pos);
                    position.Remove(position.Last());
                }
            }
            else
            {
                position.Insert(0, position.ElementAt(0));
                position.Remove(position.Last());
            }
            */
            direction.Insert(0,dir);
            direction.Remove(direction.Last());
            if (deltaTime > 0)
            {
                this.deltaTime.Insert(0, deltaTime);
                this.deltaTime.Remove(this.deltaTime.Last());
            }
            float tempAngle = (float)Math.Atan2(dir.Y, dir.X);
            angles.Insert(0,tempAngle);
            angles.Remove(angles.Last());
            updateState();

            if (controller != null)
            {
                controller.setHeading(tempAngle);
                controller.setSpeed((float)this.speed.First());
                
            }
                
        }
        public float getAngle()
        {
            return angles.First();
        }

        /*/// <summary>
        /// Used to set the "found" value.
        /// </summary>
        /// <param name="found"> Set this to true when car is found. Else, set it to false. </param>
        public void setFound(bool found)
        {
            this.found.Add(found);
            this.found.RemoveAt(0);
        }

        /// <summary>
        /// Use this function to set the throttle value of the car. The value should be set between 0 and 1. Values outside of this range is set to the closest value within the range.
        /// </summary>
        /// <param name="t"> Only use values between 0 and 1. </param>
        public void setThrottle(double t)
        {
            //Check if t is in the specified range and act accordingly.
            if (t > 1)
                throttle = 1;
            else if (t < 0)
                throttle = 0;
            else
                throttle = t;
        }

        /// <summary>
        /// Use this function to set the steering of the car. The values should be set between -1 and 1. Values outside of this range will be clipped to this range. -1 is max steering to the left and 1 is max steering to the right.
        /// </summary>
        /// <param name="s"> Should be set between -1 and 1. </param>
        public void setSteering(double s)
        {
            if (s < -1)
                steer = -1;
            else if (s > 1)
                steer = 1;
            else 
                steer = s;
        }*/

        public void send()
        {
            Program.com.updateThrottle(id, controller.getThrottle());
            Program.com.updateSteering(id, controller.getSteer());
        }

        public AForge.IntPoint getPosition() // Return the cars current position 
        {
            return position.First();
        }
        public Controller getController() // Return the cars controller
        {
            return controller;
        }
        public AForge.Point getDirection()
        {
            return direction.First();
        }
        public ControlStrategy getControlStrategy()
        {
            return controlStrategy;
        }

        public void setControlStrategy(ControlStrategy cs)
        {
            controlStrategy = cs;
        }

        public void setController(Controller c)
        {
            controller = c;
        }

        public double getSpeed()
        {
            return speed.First();
        }
    }
}
