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
                int trackLength = track.getPoints().Count;
                for (int i = 0; i < trackLength; i++)
                {
                    Point point = track.getPoints().ElementAt(i);

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
                        tempQuality = 0.01f*lengthToPoint +  (float) Math.Pow(angleToPoint,2) + indexDistance;
                    if(tempQuality < quality && lengthToPoint > 60 )
                    {
                        quality = tempQuality;
                        index = i;
                    }
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
    }
}
