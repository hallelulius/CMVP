using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Drawing;


using AForge.Imaging;
using AForge.Math.Geometry;
using AForge.Math;
using AForge;
using System.Diagnostics;

//using Math;

namespace CMVP
{
    public  partial class Car : Item
    {
        
        //The first element in the lists is the last one logged, ie. the current one.
        private List<AForge.IntPoint> position; //Position of the car as two integers.
        private List<IntPoint> lastPositions; //A list to prevent flickering.
        private List<bool> foundList; //A list that stores if the car is found or not
        private List<AForge.Point> direction; //The direction of the car as a normalized 2D vector.
        private List<float> angles;//angles
        private List<double> speed; //Velocity of the car in cm/s.
        private List<double> deltaTime; //time between updates
        private List<double> acceleration; //Acceleration of the car calculated as the difference in velocity between the last velocity and the current velocity.
        private float maxSpeed;
        
        private ControlStrategy controlStrategy; // This specific cars control strategy
        private int id; public int ID { get { return id; } } //Identification number of the car.
        public bool found;
        private Controller controller; // This cars controller 
        

        //Const settings:
        private const int DATA_HISTORY_LENGTH = 50; public int HISTORY_LENGTH { get { return DATA_HISTORY_LENGTH; } } //Decides how many elements will be stored in the position, direction, speed, acceleration and found lists.
        private static float PIXEL_SIZE = 1 / 3.84F; // used to get the right unit for the speed 

        /// <summary>
        /// The "Car" class is the virtual representation of the real world RC car.
        /// </summary>
        /// <param name="id"> Identification number of the car. </param>
        /// <param name="pos"> The starting position of the car. </param>
        public Car(int id, AForge.IntPoint pos, AForge.Point dir,int size) : base(pos,size)
        {
            controller = new PIDController(this);
            this.id = id;
            this.direction = new List<AForge.Point>();
            this.position = new List<AForge.IntPoint>();
            this.angles = new List<float>();
            this.speed = new List<double>();
            this.deltaTime = new List<double>();
            this.acceleration = new List<double>();
            this.lastPositions = new List<IntPoint>();
            this.foundList = new List<bool>();
            this.controlStrategy = new ControlStrategies.JustFollow(this);
            setMaxSpeed(150F);
            for (int i = 0; i < DATA_HISTORY_LENGTH; i++)
            {
                this.direction.Add(dir);
                this.position.Add(pos);
                this.speed.Add(1.0);
                this.acceleration.Add(0);
                this.angles.Add(0);
                this.deltaTime.Add(0.001F);
                this.lastPositions.Add(pos);
                this.foundList.Add(true);
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
            lock (this)
            {
                double tempSpeed = (double)((Math.Sqrt((dx * dx) + (dy * dy))) / deltaTime.First()) * PIXEL_SIZE;
                speed.Insert(0, tempSpeed);
                //Remove oldest element.
                speed.Remove(speed.Last());
                //speed.RemoveAt(speed.Count-1); 
            }
            
            //Calculate acceleration
            acceleration.Insert(0,(speed.ElementAt(1) - speed.ElementAt(0))/deltaTime.First());
            //Remove oldest element.
           acceleration.Remove(acceleration.Last()); //We tried another way to remove last object. Se below
            //acceleration.RemoveAt(acceleration.Count-1);
           
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
                position.Insert(0, position.First());
                position.Remove(position.Last());
            }
            foundList.Insert(0, found);
            foundList.Remove(foundList.Last());

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
        public void send()
        {
            Program.com.updateThrottle(id, controller.getThrottle());
            Program.com.updateSteering(id, controller.getSteer());
        }
        public override AForge.IntPoint getPosition() // Return the cars current position 
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
            double tempSpeed = 0;
            lock (this)
            {
                List<double> tempSpeedList = new List<double>(speed); //Copy to prevent exceptions in foreach 

                foreach (double s in tempSpeedList)
                {
                    tempSpeed += s;
                }
                double tempSpeed2 = (double)tempSpeed / (tempSpeedList.Count);

                if (tempSpeed2 > 0.01F)
                {
                    return tempSpeed2;
                }
                else
                {
                    return 0;
                }
            }
        }
        public float getMaxSpeed()
        {
            return maxSpeed;
        }
        public void setMaxSpeed(float maxSpeed)
        {
            this.maxSpeed = maxSpeed;
            controller.setMaxSpeed(maxSpeed);
        }
        public List<AForge.IntPoint> getPositionHistory()
        {
            return new List<IntPoint>(position);
        }
        public List<bool> getFoundList()
        {
            lock (this) { 
            return new List<bool>(foundList);
            }
        }
        public double getDeltaTime()
        {
            return deltaTime.First<double>();
        }
        public void stop()
        {
            Program.com.stopCar(id);
        }
        /*
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
        */
    }
}
