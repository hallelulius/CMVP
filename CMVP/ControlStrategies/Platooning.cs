using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Math;
using AForge.Math.Geometry;

// The platooning class is used to make a vehicle follow a track until it find a pre determined vehicle to follow. 
// Then it will set its speed and position steering to follow the leading vehicle with a desired distance between the cars. 
namespace CMVP.ControlStrategies
{
    class Platooning : ControlStrategy
    {
        private Car followed_car;
        private bool following_leader = false;
        private float desiredDistance = 2*73.0f; // Predetemined distance desired between the car and the leader in pixels. (73 pixels = 13 cm)
        // private int searchDistance = 200;
        private int lastIndex = -1;
        private float control_error;

        // Control Parameters
        //private float Kp = 0.01f;
        //private float Ki = 0.0006f;
        //private float Kd = 0.0006f;
        private float Kp = 0.03f;
        private float Ki = 0.0000f;
        private float Kd = 0.0000f;
        

        // Control Variables
        private float integratorSum = 0;
        private float lastError = 0;

        public Platooning(Car car) : base(car, null, "Platooning")
        {

        }

        public override void updateReferencePoint()
        {

            if(!following_leader )  // decide which reference to follow. The old one or the platooning leader's
            {
                if(desiredDistance * desiredDistance >= 
                      ((followed_car.getPosition().X - car.getPosition().X) * (followed_car.getPosition().X - car.getPosition().X) 
                    + (followed_car.getPosition().Y - car.getPosition().Y) * (followed_car.getPosition().Y - car.getPosition().Y))) 
                    // Search if the leader is close to current location 
                {
                    following_leader = true;
                    Console.WriteLine("Following leader!");
                }
                else // The car will follow the original reference signal using same algorithms as JustFollow()
                {
                    // Start of JustFollow()
                    int index = -1;
                    float quality = 9999;
                    if (track != null)
                    {
                        int trackLength = track.getPoints().Count;
                        for (int i = 0; i < trackLength; i++)
                        {
                            Point point = track.getPoints().ElementAt(i);

                            float lengthToPoint = (point - car.getPosition()).EuclideanNorm();

                            Point tempPoint = point - car.getPosition();
                            float scalarProduct = (car.getDirection().X * tempPoint.X + car.getDirection().Y * tempPoint.Y) / (car.getDirection().EuclideanNorm() * tempPoint.EuclideanNorm());
                            float angleToPoint = (float)Math.Acos(scalarProduct);

                            float indexDistance = 0;
                            if (lastIndex != -1)
                            {
                                int temp1 = Math.Abs(lastIndex - 1);
                                int temp2 = Math.Abs(lastIndex - trackLength - 1);
                                if (temp1 < temp2)
                                {
                                    indexDistance = temp1;
                                }
                                else
                                {
                                    indexDistance = temp2;
                                }
                            }

                            float tempQuality;
                            tempQuality = 0.01f * lengthToPoint + angleToPoint + indexDistance;
                            if (tempQuality < quality && lengthToPoint > 45)
                            {
                                quality = tempQuality;
                                index = i;
                            }
                        }

                        if (index < 0)
                        {
                            Console.WriteLine("No referencepoints found");
                            lastIndex = -1;
                            return;
                        }
                        else
                        {

                            setReference(track.getPoints().ElementAt(index), track.getSpeeds().ElementAt(index));
                            lastIndex = index;
                        }
                    } 
                    // End of JustFollow()
                }
            }
            else // else the car will follow the platooning leader's track using JustFollow()
            {
                if (desiredDistance * desiredDistance*2 <=
                     ((followed_car.getPosition().X - car.getPosition().X) * (followed_car.getPosition().X - car.getPosition().X)
                   + (followed_car.getPosition().Y - car.getPosition().Y) * (followed_car.getPosition().Y - car.getPosition().Y)))
                // Search if the leader is close else start follow the track instead.   
                {
                    following_leader = false;
                    Console.WriteLine("Track follower!");
                }

                IntPoint refPoint = new IntPoint();
                bool pointIsFound = false;
                // Start of JustFollow(). The code is adjusted to suit platooning but it use JustFollow() as a base. 
                int index = -1;
                float quality = 9999;
                if (followed_car.getPositionHistory() != null)
                {
                    List < IntPoint > pos_history= followed_car.getPositionHistory();
                    int trackLength = pos_history.Count;
                    for (int i = 0; i < trackLength; i++)
                    {
                        Point point = pos_history.ElementAt(i);

                        float lengthToPoint = (point - car.getPosition()).EuclideanNorm();

                        Point tempPoint = point - car.getPosition();
                        float scalarProduct = (car.getDirection().X * tempPoint.X + car.getDirection().Y * tempPoint.Y) / (car.getDirection().EuclideanNorm() * tempPoint.EuclideanNorm());
                        float angleToPoint = (float)Math.Acos(scalarProduct);

                        float indexDistance = 0;
                        if (lastIndex != -1)
                        {
                            int temp1 = Math.Abs(lastIndex - 1);
                            int temp2 = Math.Abs(lastIndex - trackLength - 1);
                            if (temp1 < temp2)
                            {
                                indexDistance = temp1;
                            }
                            else
                            {
                                indexDistance = temp2;
                            }
                        }

                        float tempQuality;
                        tempQuality = 0.01f * lengthToPoint + angleToPoint + indexDistance;
                        if (tempQuality < quality && lengthToPoint > 45)
                        {
                            quality = tempQuality;
                            index = i;
                        }
                    }

                    if (index < 0)
                    {
                        Console.WriteLine("No referencepoints found");
                        lastIndex = -1;
                        return;
                    }
     
                    else // The last loop of JustFollow() is adjusted to fit into platooning
                    {
                        // Calculate reference speed
                        float nx, ny, c, t;
                        nx = followed_car.getDirection().X;
                        ny = followed_car.getDirection().Y;
                        c = -(nx * followed_car.getPosition().X + ny * followed_car.getPosition().Y);
                        t = Math.Abs((nx * car.getPosition().X + ny * car.getPosition().Y + c)) / (float)Math.Sqrt(nx * nx + ny * ny);
                        control_error = t - desiredDistance;

                        // Proportional gain
                        float controlSignal = control_error * Kp;

                        // Integrator gain
                        integratorSum += control_error;
                        controlSignal += Ki * integratorSum * (float)car.getDeltaTime();

                        // Derivative gain
                        controlSignal += Kd * (lastError - control_error) / (float)car.getDeltaTime();
                        
                        // Scale control error with reference vehicles speed 
                        //controlSignal += (float)followed_car.getSpeed() / followed_car.getMaxSpeed()*0.3F;
                        controlSignal += (float)followed_car.getController().getThrottle();

                        // Set minimum value of control signal 
                        if (controlSignal<-0.2)
                        {
                            controlSignal = 0;
                        }

                        // Update last error for the integrator i next execution 
                        lastError = control_error;

                        // Update the cars steering and velocity 
                        setReference(pos_history.ElementAt(index), controlSignal);
                        lastIndex = index;
                    }
                }
                // end of JustFollow()
            }            
        }

        public bool isFollowingLeader
        {
            get { return following_leader; }
        }
        public float controlError
        {
            get { return control_error; }
        }
        public Car followedCar
        {
            get { return followed_car; }
            set { followed_car = value; }
        }

        public float ki
        {
            get { return Ki; }
            set { Ki = value; }
        }

        public float kp
        {
            get { return Kp; }
            set { Kp = value; }
        }

        public float kd
        {
            get { return Kd; }
            set { Kd = value; }
        }

        public float distance
        {
            get { return desiredDistance; }
            set { desiredDistance = Math.Abs(value); }
        }
    }
}
