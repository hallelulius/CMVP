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
        public JustFollow(Car car) : base(car,null, "Follow track")
        {

        }

        public override void updateReferencePoint() // Find the next point in hte reference signal 
        {

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
                    if (Math.Acos(Math.Abs(scalarProduct)) < Math.PI / 6)
                    {
                        //float currentLength = Norm(Subtract(tempPoint, car.getPosition()));
                        float currentLength = ((new Point(track.m[0, i], track.m[1, i])) - car.getPosition()).EuclideanNorm();
                        if (currentLength < shortestLength && currentLength > 10)
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
            }
        }
    }
}
