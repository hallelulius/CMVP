using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Math;
using AForge.Math.Geometry;

// The platooning class is used to make a vehicle follow a track until it find a pre determined vehicle to follow. 
// Then it will set its speed and position steering to follow the leading vehicle at a dessired distance. 
namespace CMVP.ControlStrategies
{
    class Platooning : ControlStrategy
    {
        private Car followed_car;
        // private List<AForge.IntPoint> new_track = new List<AForge.IntPoint>();
        private bool following_leader = false;
        private int searchDistance = 56;
        private float desiredDistance = 73.0f; // Predetemined distance at pixels. (73 pixels = 13 cm)
        private int lastIndex = -1;
        private float control_error;

        // Control Parameters
        private float Kp = 10.0f;
        private float Ki = 0.005f;
        private float Kd = 0.7f;
        //private float Ti;
        //private float Td;
        // Control Variables
        private float integratorSum = 0;
        private float lastError = 0;

        public Platooning(Car car) : base(car, null, "Platooning")
        {

        }

        public override void updateReferencePoint()
        {
            //new_track.Add(followed_car.getPosition());

            if(!following_leader)  // decide which reference to follow. The old one or the platooning leader's
            {
                if(searchDistance * searchDistance >= 
                      ((followed_car.getPosition().X - car.getPosition().X) * (followed_car.getPosition().X - car.getPosition().X) 
                    + (followed_car.getPosition().Y - car.getPosition().Y) * (followed_car.getPosition().Y - car.getPosition().Y))) 
                    // Search if the leaders is close to current location 
                {
                    following_leader = true;
                    Console.WriteLine("Following leader!");
                    //System.Windows.Forms.MessageBox.Show("Following leader!");
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
                            // if (angleToPoint > Math.PI / 2)
                            //    tempQuality = 9999;
                            //else
                            tempQuality = 0.01f * lengthToPoint + angleToPoint + indexDistance;
                            if (tempQuality < quality && lengthToPoint > 45)
                            {
                                quality = tempQuality;
                                index = i;
                            }
                        }

                        if (index < 0)
                        {
                            //setReference(new PointF(0, 0), 0);
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
                IntPoint refPoint = new IntPoint();
                bool pointIsFound = false;
                // Start of JustFollow(). The code is adjusted to suit platooning but it use JustFollow() as a base. 
                int index = -1;
                float quality = 9999;
                if (followed_car.getPositionHistory() != null)
                {
                    int trackLength = followed_car.getPositionHistory().Count;
                    for (int i = 0; i < trackLength; i++)
                    {
                        Point point = followed_car.getPositionHistory().ElementAt(i);

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
                        // if (angleToPoint > Math.PI / 2)
                        //    tempQuality = 9999;
                        //else
                        tempQuality = 0.01f * lengthToPoint + angleToPoint + indexDistance;
                        if (tempQuality < quality && lengthToPoint > 45)
                        {
                            quality = tempQuality;
                            index = i;
                        }
                    }

                    if (index < 0)
                    {
                        //setReference(new PointF(0, 0), 0);
                        Console.WriteLine("No referencepoints found");
                        lastIndex = -1;
                        return;
                    }
                    // end of JustFollow().
                    else // The last loop of JustFollow() is adjusted to fit into platooning
                    {
                        // Calculate reference speed
                        float nx, ny, c, t;
                        nx = followed_car.getDirection().X;
                        ny = followed_car.getDirection().Y;
                        c = -(nx * followed_car.getPosition().X + ny * followed_car.getPosition().Y);
                        t = (nx * car.getPosition().X + ny * car.getPosition().Y + c) / (float)Math.Sqrt(nx * nx + ny * ny);
                        control_error = t - desiredDistance;

                        // Proportional gain
                        float controlSignal = control_error * Kp;

                        // Integrator gain
                        integratorSum += control_error;
                        controlSignal += Ki * integratorSum * (float)car.getDeltaTime();

                        // Derivative gain
                        controlSignal += Kd * (lastError - control_error) / (float)car.getDeltaTime();
                        lastError = control_error;

                        // Add leaders speed to reference signal
                        controlSignal += (float)followed_car.getSpeed();
                        Console.WriteLine("Platooning!");
                        setReference(new_track.ElementAt(index), controlSignal);
                        lastIndex = index;
                    }
                }
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

        //public List<AForge.IntPoint> newTrack
        //{
        //    get { return new_track; }
        //}
    }
}
