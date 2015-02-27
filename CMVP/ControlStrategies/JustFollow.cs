using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CMVP.ControlStrategies
{
    class JustFollow : ControlStrategy
    {
        public JustFollow(Car car, float[,] track) : base(car,track,"JustFollow")
        {
        }

        public override void updateReferencePoint() // Find the next point in hte reference signal 
        {
            float shortestLength = 2000; //Maximum search distance
            int index = -1;
            for (int i = 0; i < track.Length; i++ )
            {
                PointF tempPoint = new PointF(track[1, i] - car.getPosition().X, track[2, i] - car.getPosition().Y);
                float scalarProduct = (car.getDirection().X*tempPoint.X + car.getDirection().Y*tempPoint.Y)/(Norm(car.getDirection())*Norm(tempPoint)); 
                if (Math.Acos(scalarProduct)<Math.PI/2)
                {
                    float currentLength = Norm(Subtract(tempPoint, car.getPosition()));
                    if (currentLength < shortestLength)
                    {
                        shortestLength = currentLength;
                        index = i; 
                    }
 
                }
            }
            
            if (index<0)
            {
                setReference(new PointF(0, 0), 0);
                return;
            }
            else
            {
                setReference(new PointF(track[1, index], track[2, index]), track[3, index]);
            } 
        }

        private PointF Subtract(PointF point1, PointF point2) // Subtract two ponts in 2 dimentions 
        {
            return new PointF(point1.X - point2.X, point1.Y - point2.Y);
        }

        private float Norm (PointF point) // Normalize a point in two dimentions
        {
            return (float) Math.Sqrt(point.X * point.X + point.Y * point.Y);
        }
    }
}
