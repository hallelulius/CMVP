using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;
using AForge.Vision.Motion;
using AForge.Imaging;
using AForge.Math.Geometry;
using AForge.Math;

namespace CMVP.ControlStrategies
{
    class JustFollow : ControlStrategy
    {

        int lastIndex = -1;
        
        public JustFollow(Car car) : base(car,null, "Follow track")
        {

        }
        public override void updateReferencePoint() // Find the next point in hte reference signal 
        {
            int index = -1;
            float quality = 9999;
            if (track != null)
            {
                int trackLength = track.m.Length / 3;
                for (int i = 0; i < trackLength; i++)
                {
                    Point point = new AForge.Point(track.m[0,i], track.m[1, i]);

                    float lengthToPoint = (point - car.getPosition()).EuclideanNorm();

                    Point tempPoint = point - car.getPosition();
                    float scalarProduct = (car.getDirection().X * tempPoint.X + car.getDirection().Y * tempPoint.Y) / (car.getDirection().EuclideanNorm() * tempPoint.EuclideanNorm());
                    float angleToPoint = (float) Math.Acos(scalarProduct);

                    float indexDistance = 0;
                    if (lastIndex != -1)
                    {
                        int temp1 = Math.Abs(lastIndex-1);
                        int temp2 = Math.Abs(lastIndex - trackLength-1);
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
                        tempQuality = 0.01f*lengthToPoint +  angleToPoint + indexDistance;
                    if(tempQuality < quality && lengthToPoint > 30)
                    {
                        quality = tempQuality;
                        index = i;
                    }
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
                setReference(new IntPoint((int)track.m[0, index], (int)track.m[1, index]), track.m[2, index]);
                lastIndex = index;

            }


            /*
            float shortestLength = 2000; //Maximum search distance
            int index = -1;
            if (track != null)
            {
                for (int i = 0; i < track.m.Length/3; i++)
                {
                    AForge.Point tempPoint = new AForge.Point(track.m[0,i] - car.getPosition().X, track.m[1, i] - car.getPosition().Y);
                    float carNorm = car.getDirection().EuclideanNorm();
                    float trackNorm = tempPoint.EuclideanNorm();

                    //float scalarProduct = (car.getDirection().X * tempPoint.X + car.getDirection().Y * tempPoint.Y) / (Norm(car.getDirection()) * Norm(tempPoint));
                    float scalarProduct = (car.getDirection().X * tempPoint.X + car.getDirection().Y * tempPoint.Y) / (car.getDirection().EuclideanNorm() * tempPoint.EuclideanNorm());
                    if (Math.Acos(scalarProduct) < Math.PI / 3)
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
                    setReference(new IntPoint((int)track.m[0,index], (int)track.m[1, index]), track.m[2, index]);
                }
            }*/
        }
    }
}
