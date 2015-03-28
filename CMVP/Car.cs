using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


using AForge.Imaging;
using AForge.Math.Geometry;
using AForge.Math;

//using Math;

namespace CMVP
{
    public class Car
    {
        //The first element in the lists is the last one logged, ie. the current one.
        private List<AForge.IntPoint> position; //Position of the car as two integers.
        private List<AForge.Point> direction; //The direction of the car as a normalized 2D vector.
        private List<float> angles;//angles
        private List<double> speed; //Velocity of the car in cm/s.
        private List<double> deltaTime; //delta between updates
        private List<double> acceleration; //Acceleration of the car calculated as the difference in velocity between the last velocity and the current velocity.
        private List<bool> found; //Is true if the car is found by the image processing.

        
        private ControlStrategy controlStrategy; // This specific cars control strategy
        private int id; public int ID { get { return id; } } //Identification number of the car.
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
            }
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
            
            double temp = tempSpeed;
            foreach (double s in speed)
            {
                temp += s;
            }
            speed.Insert(0, temp/(speed.Count+1));
            
            //Remove oldest element.
            double xspeed = speed.Last();
            if (xspeed == null)
                Console.WriteLine("Null speed");
            speed.Remove(speed.Last());

            //Calculate acceleration
            acceleration.Insert(0,(speed.ElementAt(1) - speed.ElementAt(0))/deltaTime.ElementAt(0));
            //Remove oldest element.
            acceleration.Remove(acceleration.Last());
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
            position.Insert(0,pos);
            position.Remove(position.Last());
            direction.Insert(0,dir);
            direction.Remove(direction.Last());
            this.deltaTime.Insert(0, deltaTime);
            this.deltaTime.Remove(this.deltaTime.Last());
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
        /// <summary>
        /// Is true if the car was found by the image processing.
        /// </summary>
        public bool isFound()
        {
            return found.ElementAt(DATA_HISTORY_LENGTH);
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
