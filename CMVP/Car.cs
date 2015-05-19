using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge.Imaging;
using AForge.Math.Geometry;
using AForge.Math;
using AForge;
using System.Diagnostics;
using System.Collections.Concurrent;

//using Math;

namespace CMVP
{
    public partial class Car : Item
    {
        //car settings
        private int id; public int ID { get { return id; } } //Identification number of the car.
        private ControlStrategy controlStrategy;             // This specific cars control strategy
        private Controller controller;                       // This cars controller 
        private float maxSpeed;
        
        //current state variables
        private IntPoint position;
        private IntPoint lastPosition;
        private Point direction;
        private double angle;
        private double speed;
        private double deltaTime;
        
        //states history
        private ConcurrentQueue<double> speeds;
        private ConcurrentQueue<double> angles2;
        private ConcurrentQueue<double> deltaTimes; //time between updates
        private double s, a, d; // output from dequeue, values not used right now
        //The first element in the lists is the last one logged, ie. the current one. (Except speed and angles which is vice versa)
        private List<IntPoint> positions; //Position of the car as two integers.
        private List<IntPoint> lastPositions; //A list to prevent flickering.
        private List<bool> foundList; //A list that stores if the car is found or not
        private List<Point> directions; //The direction of the car as a normalized 2D vector.
        private List<float> angles;//angles
        
        //imageprocessing varibles
        private IntPoint posIP;
        private Point dirIP;
        private double deltaTimeIP;
        public bool found;
        
        //Const settings:
        private const int DATA_HISTORY_LENGTH = 50; public int HISTORY_LENGTH { get { return DATA_HISTORY_LENGTH; } } //Decides how many elements will be stored in the position, direction, speed, acceleration and found lists.
        private static float PIXEL_SIZE = 1 / 3.84F; // used to get the right unit for the speed 

        /// <summary>
        /// The "Car" class is the virtual representation of the real world RC car.
        /// </summary>
        /// <param name="id"> Identification number of the car. </param>
        /// <param name="pos"> The starting position of the car. </param>
        public Car(int id, IntPoint pos, Point dir, int size)
            : base(pos, size)
        {
            this.id = id;
            pos = posIP;
            dir = dirIP;
            this.controlStrategy = new ControlStrategies.JustFollow(this);
            controller = new PIDController(this);
            this.directions = new List<AForge.Point>();
            this.positions = new List<AForge.IntPoint>();
            this.angles = new List<float>();
            this.speeds = new ConcurrentQueue<double>();
            this.angles2 = new ConcurrentQueue<double>();
            this.deltaTimes = new ConcurrentQueue<double>();
            this.lastPositions = new List<IntPoint>();
            this.foundList = new List<bool>();
            

            setMaxSpeed(120F);
            for (int i = 0; i < DATA_HISTORY_LENGTH; i++)
            {
                this.directions.Add(dir);
                this.positions.Add(pos);
                this.angles.Add(0);
                this.lastPositions.Add(pos);
                this.foundList.Add(true);
                //this.speeds.Enqueue(1.0);
                this.deltaTimes.Enqueue(0.001F);
                this.angles2.Enqueue(0);
            }
            for (int i = 0; i < 50; i++)
            {
                this.speeds.Enqueue(1.0);
        }
                found = true;
        }
        /// <summary>
        /// Update the state of the car. Only call this once for every car in each program loop.
        /// </summary>
        public void updateState()
        {
            //calculate state and insert to history lists

            //position
            //to prevent flickering between two pixels
            if (lastPositions.TrueForAll(x => !(x.Equals(posIP))))
            {
                if (posIP == positions.ElementAt(0))
                {
                    positions.Insert(0, posIP);
                    positions.Remove(positions.Last());
                }
                else
                {
                    lastPositions.Insert(0, positions.ElementAt(0));
                    lastPositions.RemoveAt(lastPositions.Count - 1);
                    positions.Insert(0, posIP);
                    positions.Remove(positions.Last());

                }
            }
            else
            {
                positions.Insert(0, positions.First());
                positions.Remove(positions.Last());
            }

            //found, direction and deltaTime
            foundList.Insert(0, found);
            foundList.Remove(foundList.Last());
            directions.Insert(0, dirIP);
            directions.Remove(directions.Last());
            deltaTimes.Enqueue(deltaTimeIP);
            deltaTimes.TryDequeue(out d);


            //angle
            double tempAngle = Math.Atan2(dirIP.Y, dirIP.X);
            //angles.Insert(0, tempAngle);
            //angles.Remove(angles.Last());
            angles2.Enqueue(tempAngle);
            angles2.TryDequeue(out a);

            //speed
            //Calculate horizontal and vertical movement using the last two elements in the position list.
            double dx = positions.ElementAt(1).X - positions.ElementAt(0).X;
            double dy = positions.ElementAt(1).Y - positions.ElementAt(0).Y;
            double tempSpeed = (double)((Math.Sqrt((dx * dx) + (dy * dy))) / deltaTimes.First()) * PIXEL_SIZE;
            speeds.Enqueue(tempSpeed);
            speeds.TryDequeue(out s);

            //update state
            position = positions.First();
            lastPosition = lastPositions.First();
            direction = directions.Last();
            deltaTime = deltaTimes.Last();
            angle = angles2.Last();
            speed = speeds.Last();

            if (controller != null)
            {
                //fixes here!
                controller.setHeading((float)angle);
                controller.setSpeed((float)getSpeed());
                
            }
        }
        /// <summary>
        /// Set the cars position and orientation. Used by the imageprocessing
        /// </summary>
        /// <param name="pos"> The new postition of the car. </param>
        /// <param name="dir"> The new direction of the car. </param>
        public void setPositionAndOrientation(IntPoint pos, Point dir, double deltaTime)
        {
                posIP = pos;
                dirIP = dir;
                deltaTimeIP = deltaTime;
        }
        public void stop()
        {
            Program.com.stopCar(id);
        }
        public void send()
        {
            Program.com.updateThrottle(id, controller.getThrottle());
            Program.com.updateSteering(id, controller.getSteer());
        }

        ///
        ///getters and setters
        ///

        public float getAngle()
        {
            return (float)angle;
        }

        public override IntPoint getPosition() // Return the cars current position 
        {
            return position;
        }
        public Controller getController() // Return the cars controller
        {
            return controller;
        }
        public Point getDirection()
        {
            return direction;
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
                ConcurrentQueue<double> tempSpeeds = new ConcurrentQueue<double>(speeds); //Copy to prevent exceptions in foreach 

                foreach (double s in tempSpeeds)
                {
                    tempSpeed += s;
                }
                double tempSpeed2 = (double)tempSpeed / (tempSpeeds.Count);

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
        public List<IntPoint> getPositionHistory()
        {
            return new List<IntPoint>(lastPositions);
        }
        public List<bool> getFoundList()
        { 
            return new List<bool>(foundList);
        }
        public double getDeltaTime()
        {
            return deltaTime;
        }

    }
}
