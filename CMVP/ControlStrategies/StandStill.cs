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
    class StandStill : ControlStrategy
    {
        public StandStill(Car owner) : base(owner,null, "Stand still")
        {
        }

        public override void updateReferencePoint() // Find the next point in hte reference signal 
        {
            setReference(new IntPoint(car.getPosition().X, car.getPosition().Y), 0);
        }
    }
}
