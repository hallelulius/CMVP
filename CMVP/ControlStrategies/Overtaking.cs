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
    class Overtaking : ControlStrategy
    {
        int lastIndex = -1;


        
        float smallRadius = 50; //Should be adaptive
        float bigRadius = 180;  //Should be adaptive

        public Overtaking(Car car) : base(car, null, "Overtaking")
        {

        }

        public override void updateReferencePoint()
        {
            object[] obj = redirect();
            List<IntPoint> modifiedTrack = (List<IntPoint>) obj[0];
            List<float> modifiedSpeed = (List<float>)obj[1];


            int index = -1;
            float quality = 9999;
            if (modifiedTrack != null)
            {
                int trackLength = modifiedTrack.Count;
                for (int i = 0; i < trackLength; i++)
                {
                    Point point = modifiedTrack.ElementAt(i);

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
                setReference(modifiedTrack.ElementAt(index), modifiedSpeed.ElementAt(index));
                lastIndex = index;

            }
        }






        private object[] redirect()
        {
            List<IntPoint> modifiedTrack = track.getPoints();
            List<float> modifiedSpeed = track.getSpeeds();

            //Is car close to any object? Return if not
            List<Car> objects = new List<Car>();
            foreach (Car obj in Program.cars)
            {
                if (obj != car)
                {
                    if ((obj.getPosition() - car.getPosition()).EuclideanNorm() < bigRadius)
                    {
                        objects.Add(obj);
                    }
                }
            }
            if (objects.Count < 1)
            {
                return new object[] { modifiedTrack, track.getSpeeds() };
            }

            //Redirect for each object
            foreach (Car obj in objects)
            {

                //Find closest indexpoints
                int closestIndex = -1;
                float closestDistance = 9999;
                int secClosestIndex = -1;
                float secClosestDistance = 10000;

                int trackLength = track.getPoints().Count;
                for (int i = 0; i < trackLength; i++)
                {
                    IntPoint point = track.getPoints().ElementAt(i);
                    float distance = (point - obj.getPosition()).EuclideanNorm();

                    if (distance < closestDistance)
                    {
                        secClosestDistance = closestDistance;
                        secClosestIndex = closestIndex;
                        closestDistance = distance;
                        closestIndex = i;
                    }
                    else if (distance < secClosestDistance)
                    {
                        secClosestDistance = distance;
                        secClosestIndex = i;
                    }
                }

                //Find closest point along track, (x,y)=(a+b*t, c+d*t)
                float a = modifiedTrack.ElementAt(secClosestIndex).X;
                float c = modifiedTrack.ElementAt(secClosestIndex).Y;

                float b = modifiedTrack.ElementAt(closestIndex).X - modifiedTrack.ElementAt(secClosestIndex).X;
                float d = modifiedTrack.ElementAt(closestIndex).Y - modifiedTrack.ElementAt(secClosestIndex).Y;

                float x = obj.getPosition().X;
                float y = obj.getPosition().Y;

                float x0 = (a * d * d + b * b * x - b * c * d + b * d * y) / (b * b + d * d);
                float y0 = (-a * b * d + b * b * c + b * d * x + d * d * y) / (b * b + d * d);

                Point closestPoint = new Point(x0, y0);

                //Find midpoint
                Point normal = closestPoint - obj.getPosition();
                normal = normal / normal.EuclideanNorm();
                IntPoint midPoint = (obj.getPosition() + (normal * smallRadius)).Round();

                //Add points closeby
                IntPoint point1 = (midPoint + (new Point(normal.Y, -normal.X)) * smallRadius / 2).Round();
                IntPoint point2 = (midPoint + (new Point(-normal.Y, normal.X)) * smallRadius / 2).Round();

                //Find index of outer points, oterIndex1 > outerIndex2
                float distance1 = 9999;
                float distance2 = 10000;
                int outerIndex1 = -1;
                int outerIndex2 = -1;

                for (int i = 0; i < trackLength; i++)
                {
                    Point point = track.getPoints().ElementAt(i);
                    float tempDistance = (point - obj.getPosition()).EuclideanNorm();

                    if (tempDistance < distance1 && tempDistance > bigRadius)
                    {
                        distance2 = distance1;
                        outerIndex2 = outerIndex1;
                        distance1 = tempDistance;
                        outerIndex1 = i;
                    }
                    else if (tempDistance < distance2 && tempDistance > bigRadius)
                    {
                        distance2 = tempDistance;
                        outerIndex2 = i;
                    }
                }

                if (outerIndex1 < outerIndex2)
                {
                    int temp = outerIndex2;
                    outerIndex2 = outerIndex1;
                    outerIndex1 = temp;
                }

                //Set points inbetween
                IntPoint outerPoint1 = modifiedTrack.ElementAt(outerIndex1);
                IntPoint outerPoint2 = modifiedTrack.ElementAt(outerIndex2);

                IntPoint point3;
                IntPoint point4;
                if ((outerPoint1 - point1).EuclideanNorm() < (outerPoint1 - point2).EuclideanNorm())
                {
                    point3 = (outerPoint1 + point1) / 2;
                    point4 = (outerPoint2 + point2) / 2;
                }
                else
                {
                    point4 = (outerPoint1 + point1) / 2;
                    point3 = (outerPoint2 + point2) / 2;
                }

                //Calculate speed
                float speed = (modifiedSpeed.ElementAt(outerIndex1) + modifiedSpeed.ElementAt(outerIndex2)) / 2;

                //Remove old points and incert new
                List<IntPoint> tempTrack = new List<IntPoint>();
                if (Math.Abs(outerIndex1 - outerIndex2) < trackLength / 2)
                {
                    tempTrack = modifiedTrack.GetRange(0, outerIndex2);
                    tempTrack.Add(point1);
                    tempTrack.Add(point3);
                    tempTrack.Add(midPoint);
                    tempTrack.Add(point2);
                    tempTrack.Add(point4);
                    tempTrack.AddRange(modifiedTrack.GetRange(outerIndex1, modifiedTrack.Count - outerIndex1));

                    float tempSpeed = (track.getSpeeds().ElementAt(outerIndex2) + track.getSpeeds().ElementAt(outerIndex1)) / 2;
                    modifiedSpeed.RemoveRange(outerIndex2, outerIndex1 - outerIndex2);
                    for (int i = 0; i < 5; i++)
                    {
                        modifiedSpeed.Insert(outerIndex2, tempSpeed);
                    }
                }
                else
                {
                    tempTrack = modifiedTrack.GetRange(outerIndex2, outerIndex1-outerIndex2);
                    tempTrack.Add(point1);
                    tempTrack.Add(point3);
                    tempTrack.Add(midPoint);
                    tempTrack.Add(point2);
                    tempTrack.Add(point4);

                    float tempSpeed = (track.getSpeeds().ElementAt(outerIndex2) + track.getSpeeds().ElementAt(outerIndex1)) / 2;

                    modifiedSpeed.RemoveRange(outerIndex1, modifiedSpeed.Count - outerIndex1);
                    modifiedSpeed.RemoveRange(0,outerIndex2);
                    for (int i = 0; i < 5; i++)
                    {
                        modifiedSpeed.Add(tempSpeed);
                    }
                }

                modifiedTrack = tempTrack;
            }
            return new object[]{modifiedTrack,modifiedSpeed};
        }

    }
}
