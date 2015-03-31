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

            if(!following_leader)  // decide which reference to follow. The old one or the platooning leaders
            {
                if(searchDistance * searchDistance >= 
                      ((new_track.First().X - car.getPosition().X) * (new_track.First().X - car.getPosition().X) 
                    + (new_track.First().Y - car.getPosition().Y) * (new_track.First().Y - car.getPosition().Y))) // Search if the leaders track is close to current location 
                {
                    following_leader = true;
                    Console.WriteLine("Following leader!");
                }
                else
                {
                    float shortestLength = 2000; //Maximum search distance
                    int index = -1;
                    if (track != null)
                    {
                        for (int i = 0; i < track.m.Length / 3; i++)
                        {
                            AForge.Point tempPoint = new AForge.Point(track.m[0, i] - car.getPosition().X, track.m[1, i] - car.getPosition().Y);
                            float carNorm = car.getDirection().EuclideanNorm();
                            float trackNorm = tempPoint.EuclideanNorm();

                            //float scalarProduct = (car.getDirection().X * tempPoint.X + car.getDirection().Y * tempPoint.Y) / (Norm(car.getDirection()) * Norm(tempPoint));
                            float scalarProduct = (car.getDirection().X * tempPoint.X + car.getDirection().Y * tempPoint.Y) / (car.getDirection().EuclideanNorm() * tempPoint.EuclideanNorm());
                            if (Math.Acos(scalarProduct) < Math.PI / 6)
                            {
                                //float currentLength = Norm(Subtract(tempPoint, car.getPosition()));
                                float currentLength = ((new Point(track.m[0, i], track.m[1, i])) - car.getPosition()).EuclideanNorm();
                                if (currentLength < shortestLength && currentLength > 50)
                                {
                                    shortestLength = currentLength;
                                    index = i;
                                }
                            }
                        }

                        if (index < 0)
                        {
                            //setReference(new PointF(0, 0), 0);
                            Console.WriteLine("No points found");
                            return;
                        }
                        else
                        {
                            setReference(new IntPoint((int)track.m[0, index], (int)track.m[1, index]), track.m[2, index]);
                        }
                    }
                }
            }
            else
            {
                float shortestLength = 2000; //Maximum search distance
                IntPoint refPoint = new IntPoint();
                bool pointIsFound = false;
                if (track != null)
                {
                    foreach (IntPoint point in new_track)
                    {
                        AForge.Point tempPoint = new AForge.Point(point.X - car.getPosition().X, point.Y - car.getPosition().Y);
                        float carNorm = car.getDirection().EuclideanNorm();
                        float trackNorm = tempPoint.EuclideanNorm();

                        //float scalarProduct = (car.getDirection().X * tempPoint.X + car.getDirection().Y * tempPoint.Y) / (Norm(car.getDirection()) * Norm(tempPoint));
                        float scalarProduct = (car.getDirection().X * tempPoint.X + car.getDirection().Y * tempPoint.Y) / (car.getDirection().EuclideanNorm() * tempPoint.EuclideanNorm());
                        if (Math.Acos(scalarProduct) < Math.PI / 6)
                        {
                            //float currentLength = Norm(Subtract(tempPoint, car.getPosition()));
                            float currentLength = ((new Point(point.X, point.Y)) - car.getPosition()).EuclideanNorm();
                            if (currentLength < shortestLength && currentLength > 50)
                            {
                                shortestLength = currentLength;
                                refPoint = new IntPoint(point.X, point.Y);
                                pointIsFound = true;
                            }
                        }
                    }

                    if (!pointIsFound)
                    {
                        //setReference(new PointF(0, 0), 0);
                        Console.WriteLine("No points found");
                        return;
                    }
                    else
                    {
                        IntPoint carPos = car.getPosition();
                        IntPoint leaderPos = followed_car.getPosition();
                        float distance = (float)Math.Sqrt(Convert.ToDouble((carPos.X - leaderPos.X) ^ 2 + (carPos.Y - leaderPos.Y) ^ 2));
                        setReference(new IntPoint(refPoint.X, refPoint.Y), desiredDistance - distance);
                    }
                }
            }

            
        }

        public bool isFollowingLeader
        {
            get { return following_leader; }
        }
    }
}
