using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CMVP
{
    public abstract class ControlStrategy
    {
        protected float[,] track;
        protected Car car;
        protected float scaleSpeed = 1;
        protected String strategyName;

        public ControlStrategy(Car car, float[,] track, String strategyName) // Constructor 
        {
            this.car = car;
            this.track = track;
            this.strategyName = strategyName;
        }

        public abstract void updateReferencePoint(); // Calculate next point for a car in reference signal

        protected void setReference(PointF refPoint, float speed) // Transform the reference points coordinate and the cars current coordinates to a reference signal (speed and angle) 
        {
            float refAngle = (float) Math.Atan2(refPoint.Y-car.getPosition().Y , refPoint.X-car.getPosition().X);
            car.getController().setHeading(refAngle);
            car.getController().setSpeed(speed);
        }

        public void setTrack(float[,] track)
        {
            this.track = track; 
        }

        public void setScaleSpeed(float scaleSpeed)
        {
            this.scaleSpeed = scaleSpeed;
        }

        public float getScaleSpeed()
        {
            return scaleSpeed;
        }

        public float [,] getTrack() 
        {
            return track;
        }
        public string getStrategyName()
        {
            return strategyName;
        }

    }
}
