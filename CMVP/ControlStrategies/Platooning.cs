using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Math;
using AForge.Math.Geometry;

namespace CMVP.ControlStrategies
{
    class Platooning : ControlStrategy
    {
        private Car followed_car;
        private List<AForge.IntPoint> new_track = new List<AForge.IntPoint>();
        private bool following_leader = false;
        private int searchDistance = 56;
        private float desiredDistance = 73.0f;
        private int lastIndex = -1;

        // Control Parameters
        private float Kp = 10.0f;
        private float Ki = 0;
        private float Kd = 0.7f;
        //private float Ti;
        //private float Td;
        // Control Variables
        private float integratorSum = 0;
        private float lastError = 0;

        public Platooning(Car car) : base(car, null, "Platooning")
        {

        }

        public Car followedCar
        {
            get { return followed_car; }
            set { followed_car = value; }
        }

        public override void updateReferencePoint()
        {
            new_track.Add(followed_car.getPosition());

            if(!following_leader)  // decide which reference to follow. The old one or the platooning leader's
            {
                if(searchDistance * searchDistance >= 
                      ((new_track.First().X - car.getPosition().X) * (new_track.First().X - car.getPosition().X) 
                    + (new_track.First().Y - car.getPosition().Y) * (new_track.First().Y - car.getPosition().Y))) // Search if the leaders track is close to current location 
                {
                    following_leader = true;
                    Console.WriteLine("Following leader!");
                    //System.Windows.Forms.MessageBox.Show("Following leader!");
                }
                else
                {
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
                }
            }
            else
            {
                IntPoint refPoint = new IntPoint();
                bool pointIsFound = false;
                int index = -1;
                float quality = 9999;
                if (new_track != null)
                {
                    int trackLength = new_track.Count;
                    for (int i = 0; i < trackLength; i++)
                    {
                        Point point = new_track.ElementAt(i);

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
                        // Calculate reference speed
                        float controlError = (float)Math.Sqrt(
                            (car.getPosition().X - followed_car.getPosition().X) * (car.getPosition().X - followed_car.getPosition().X)
                            + (car.getPosition().Y - followed_car.getPosition().Y) * (car.getPosition().Y - followed_car.getPosition().Y))
                            - desiredDistance;

                        // Proportional gain
                        float controlSignal = controlError * Kp;

                        // Integrator gain
                        integratorSum += controlError;
                        controlSignal += Ki * integratorSum;

                        // Derivative gain
                        controlSignal += Kd * (lastError - controlError) / (float)car.getDeltaTime();
                        lastError = controlError;

                        // Add leaders speed to reference signal
                        controlSignal += (float)followed_car.getSpeed();

                        setReference(track.getPoints().ElementAt(index), controlSignal);
                        lastIndex = index;
                    }
                }
            }

            
        }

        public bool isFollowingLeader
        {
            get { return following_leader; }
        }

        public List<AForge.IntPoint> newTrack
        {
            get { return new_track; }
        }
    }
}
