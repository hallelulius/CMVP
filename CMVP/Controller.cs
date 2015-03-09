using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMVP
{

    public abstract class Controller
    {
        protected float heading;          //heading in angle to x-axis 
        protected float refHeading;       //ref heading angle to x-axis
        protected float speed;
        protected float refSpeed;
        protected float outThrottle;
        protected float outSteer;
        protected String controllerName;

        public abstract void updateController();

        public void setHeading(float heading)
        {
            this.heading = heading;
        }
        public void setRefSpeed(float speed)
        {
            this.refSpeed = speed;
        }
        public float getThrottle()
        {
            return outThrottle;
        }
        public float getSteer()
        {
            return outSteer;
        }
        public void setThrottle(float outThrottle)
        {
            this.outThrottle = outThrottle;
        }
        public string getName()
        {
            return controllerName;
        }
        public float getRefSpeed()
        {
            return refSpeed;
        }
        public float getRefHeading()
        {
            return refHeading;
        }
    }
}
