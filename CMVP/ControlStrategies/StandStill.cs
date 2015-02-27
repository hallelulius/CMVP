﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CMVP.ControlStrategies
{
    class StandStill : ControlStrategy
    {
        public StandStill() : base(null,null,null)
        {
        }

        public override void updateReferencePoint() // Find the next point in hte reference signal 
        {
            setReference(new PointF(0, 0), 0);
        }
    }
}