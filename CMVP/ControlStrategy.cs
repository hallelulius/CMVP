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

namespace CMVP
{
    public abstract class ControlStrategy
    {
        protected Track track;
        protected Car car;
        protected float scaleSpeed = 0f; //Is not
        protected String strategyName;

        public ControlStrategy(Car car, Track track, String strategyName) // Constructor 
        {
            this.car = car;
            this.track = track;
            this.strategyName = strategyName;
        }

        public abstract void updateReferencePoint(); // Calculate next point for a car in reference signal

        protected void setReference(IntPoint refPoint, float speed) // Transform the reference points coordinate and the cars current coordinates to a reference signal (speed and angle) 
        {
            float refAngle = (float) Math.Atan2(refPoint.Y-car.getPosition().Y , refPoint.X-car.getPosition().X);
            car.getController().setRefHeading(refAngle); //sets controllers
            car.getController().setRefSpeed(speed);
            car.getController().setRefPoint(refPoint);
        }

        public void setTrack(Track track)
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

        public Track getTrack() 
        {
            return track;
        }
        public string getStrategyName()
        {
            return strategyName;
        }

    }
}
 
